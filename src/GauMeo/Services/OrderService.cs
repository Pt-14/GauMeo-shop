using GauMeo.Data;
using GauMeo.Models.Orders;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace GauMeo.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;
        private readonly ILogger<OrderService> _logger;

        public OrderService(ApplicationDbContext context, ICartService cartService, ILogger<OrderService> logger)
        {
            _context = context;
            _cartService = cartService;
            _logger = logger;
        }

        public async Task<int> CreateOrderFromCartAsync(string? userId, string? sessionId, OrderCreateRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                // Validate request
                if (!ValidateOrderRequest(request, out string errorMessage))
                {
                    _logger.LogWarning("Invalid order request: {Error}", errorMessage);
                    throw new ArgumentException(errorMessage);
                }

                // Get cart items with product details
                var cartItems = await _cartService.GetCartItemsAsync(userId, sessionId);
                if (!cartItems.Any())
                {
                    throw new InvalidOperationException("Giỏ hàng trống, không thể tạo đơn hàng");
                }

                // Validate stock for all cart items before creating order
                var stockValidationErrors = new List<string>();
                foreach (var cartItem in cartItems)
                {
                    // Reload product to get latest stock
                    var product = await _context.Products.FindAsync(cartItem.ProductId);
                    if (product == null || !product.IsActive)
                    {
                        stockValidationErrors.Add($"Sản phẩm '{cartItem.Product?.Name ?? "N/A"}' không tồn tại hoặc đã ngừng kinh doanh");
                        continue;
                    }

                    if (product.StockQuantity <= 0)
                    {
                        stockValidationErrors.Add($"Sản phẩm '{product.Name}' đã hết hàng");
                        continue;
                    }

                    if (cartItem.Quantity > product.StockQuantity)
                    {
                        stockValidationErrors.Add($"Sản phẩm '{product.Name}' chỉ còn {product.StockQuantity} sản phẩm trong kho (bạn đang đặt {cartItem.Quantity})");
                    }
                }

                if (stockValidationErrors.Any())
                {
                    throw new InvalidOperationException(string.Join("; ", stockValidationErrors));
                }

                // Calculate totals
                var subTotal = cartItems.Sum(item => item.GetSubTotal());
                var totalAmount = subTotal + request.ShippingFee - request.DiscountAmount;

                // Normalize phone number before saving
                string normalizedPhone = NormalizePhoneNumber(request.CustomerPhone);

                // Create order
                var order = new Order
                {
                    OrderNumber = Order.GenerateOrderNumber(),
                    CustomerName = request.CustomerName,
                    CustomerPhone = normalizedPhone,
                    CustomerEmail = request.CustomerEmail ?? string.Empty,
                    ShippingAddress = request.ShippingAddress,
                    BillingAddress = request.BillingAddress ?? request.ShippingAddress,
                    SubTotal = subTotal,
                    ShippingFee = request.ShippingFee,
                    DiscountAmount = request.DiscountAmount,
                    TotalAmount = totalAmount,
                    Status = "Pending",
                    PaymentMethod = request.PaymentMethod,
                    PaymentStatus = "Pending",
                    Notes = request.Notes ?? string.Empty,
                    AdminNotes = string.Empty,
                    UserId = userId,
                    PromotionId = request.PromotionId,
                    CreatedAt = DateTime.Now
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Create order items from cart items and update stock
                foreach (var cartItem in cartItems)
                {
                    // Reload product to ensure we have latest data
                    var product = await _context.Products
                        .Include(p => p.ProductImages)
                        .Include(p => p.Brand)
                        .FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);

                    if (product == null)
                    {
                        throw new InvalidOperationException($"Không tìm thấy sản phẩm với ID {cartItem.ProductId}");
                    }

                    // Calculate original price with variants for order item
                    decimal originalPrice = product.OriginalPrice;
                    
                    // Add variant adjustments if any
                    if (!string.IsNullOrEmpty(cartItem.SelectedVariants))
                    {
                        var variants = cartItem.GetSelectedVariants();
                        var productVariants = await _context.ProductVariants
                            .Where(v => v.ProductId == product.Id && v.IsActive)
                            .ToListAsync();
                            
                        foreach (var variant in variants)
                        {
                            var productVariant = productVariants
                                .FirstOrDefault(v => v.Type?.ToLower() == variant.Key.ToLower() && 
                                                   v.Name == variant.Value && v.IsActive);
                            
                            if (productVariant?.PriceAdjustment.HasValue == true)
                            {
                                originalPrice += productVariant.PriceAdjustment.Value;
                            }
                        }
                    }

                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.ProductId,
                        ProductName = product.Name,
                        ProductBrand = product.Brand?.Name ?? "",
                        ProductImageUrl = product.ProductImages?.FirstOrDefault()?.ImageUrl ?? "/images/products/default-product.jpg",
                        Quantity = cartItem.Quantity,
                        UnitPrice = cartItem.UnitPrice,
                        OriginalPrice = originalPrice,
                        DiscountPercent = product.DiscountPercent,
                        SelectedVariants = cartItem.SelectedVariants,
                        SubTotal = cartItem.GetSubTotal(),
                        CreatedAt = DateTime.Now
                    };

                    _context.OrderItems.Add(orderItem);

                    // Deduct stock (already validated above)
                    product.StockQuantity -= cartItem.Quantity;
                    product.UpdatedAt = DateTime.Now;
                }

                await _context.SaveChangesAsync();

                // Clear cart after successful order creation
                await _cartService.ClearCartAsync(userId, sessionId);

                await transaction.CommitAsync();

                _logger.LogInformation("Order {OrderNumber} created successfully for user {UserId}", 
                    order.OrderNumber, userId ?? "Guest");

                return order.Id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating order for user {UserId}", userId ?? "Guest");
                throw;
            }
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .Include(o => o.Promotion)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order?> GetOrderByNumberAsync(string orderNumber)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .Include(o => o.Promotion)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(string userId, int page = 1, int pageSize = 10)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status, string? adminNotes = null)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == orderId);
                    
                if (order == null)
                    return false;

                var oldStatus = order.Status;
                var isCancelling = status.ToLower() == "cancelled" && oldStatus.ToLower() != "cancelled";
                var isRestoring = oldStatus.ToLower() == "cancelled" && status.ToLower() != "cancelled";

                // If cancelling order, restore stock
                if (isCancelling)
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        var product = await _context.Products.FindAsync(orderItem.ProductId);
                        if (product != null)
                        {
                            product.StockQuantity += orderItem.Quantity;
                            product.UpdatedAt = DateTime.Now;
                            _logger.LogInformation("Restored {Quantity} units of product {ProductId} to stock", 
                                orderItem.Quantity, orderItem.ProductId);
                        }
                    }
                }
                // If restoring from cancelled status, deduct stock again
                else if (isRestoring)
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        var product = await _context.Products.FindAsync(orderItem.ProductId);
                        if (product != null)
                        {
                            if (product.StockQuantity < orderItem.Quantity)
                            {
                                await transaction.RollbackAsync();
                                _logger.LogWarning("Cannot restore order {OrderId}: insufficient stock for product {ProductId}", 
                                    orderId, orderItem.ProductId);
                                return false;
                            }
                            product.StockQuantity -= orderItem.Quantity;
                            product.UpdatedAt = DateTime.Now;
                            _logger.LogInformation("Deducted {Quantity} units of product {ProductId} from stock", 
                                orderItem.Quantity, orderItem.ProductId);
                        }
                    }
                }

                order.Status = status;
                
                if (!string.IsNullOrEmpty(adminNotes))
                {
                    order.AdminNotes = adminNotes;
                }

                // Update timestamps based on status
                switch (status.ToLower())
                {
                    case "confirmed":
                        order.ConfirmedAt = DateTime.Now;
                        break;
                    case "shipping":
                        order.ShippedAt = DateTime.Now;
                        break;
                    case "delivered":
                        order.DeliveredAt = DateTime.Now;
                        order.PaymentStatus = "Paid"; // Auto mark as paid when delivered
                        break;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("Order {OrderId} status updated from {OldStatus} to {NewStatus}", 
                    orderId, oldStatus, status);

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error updating order {OrderId} status", orderId);
                return false;
            }
        }

        public async Task<bool> UpdatePaymentStatusAsync(int orderId, string paymentStatus)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                    return false;

                order.PaymentStatus = paymentStatus;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Order {OrderId} payment status updated to {PaymentStatus}", 
                    orderId, paymentStatus);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order {OrderId} payment status", orderId);
                return false;
            }
        }

        public async Task<OrderStatistics> GetOrderStatisticsAsync()
        {
            var today = DateTime.Today;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            var stats = new OrderStatistics
            {
                TotalOrders = await _context.Orders.CountAsync(),
                PendingOrders = await _context.Orders.CountAsync(o => o.Status == "Pending"),
                ConfirmedOrders = await _context.Orders.CountAsync(o => o.Status == "Confirmed"),
                ProcessingOrders = await _context.Orders.CountAsync(o => o.Status == "Processing"),
                ShippingOrders = await _context.Orders.CountAsync(o => o.Status == "Shipping"),
                DeliveredOrders = await _context.Orders.CountAsync(o => o.Status == "Delivered"),
                CancelledOrders = await _context.Orders.CountAsync(o => o.Status == "Cancelled"),
                TotalRevenue = await _context.Orders
                    .Where(o => o.Status == "Delivered")
                    .SumAsync(o => o.TotalAmount),
                TodayRevenue = await _context.Orders
                    .Where(o => o.Status == "Delivered" && o.CreatedAt >= today)
                    .SumAsync(o => o.TotalAmount),
                MonthRevenue = await _context.Orders
                    .Where(o => o.Status == "Delivered" && o.CreatedAt >= startOfMonth)
                    .SumAsync(o => o.TotalAmount)
            };

            return stats;
        }

        public bool ValidateOrderRequest(OrderCreateRequest request, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validate customer name
            if (string.IsNullOrWhiteSpace(request.CustomerName) || request.CustomerName.Length < 2)
            {
                errorMessage = "Tên khách hàng phải có ít nhất 2 ký tự";
                return false;
            }

            // Validate phone number (Vietnamese format)
            if (string.IsNullOrWhiteSpace(request.CustomerPhone))
            {
                errorMessage = "Số điện thoại không được để trống";
                return false;
            }

            string normalizedPhone = NormalizePhoneNumber(request.CustomerPhone);
            var phoneRegex = new Regex(@"^(0[0-9]{9,10})$");
            if (!phoneRegex.IsMatch(normalizedPhone))
            {
                errorMessage = "Số điện thoại không hợp lệ. Vui lòng nhập số điện thoại Việt Nam (10-11 chữ số, bắt đầu bằng 0)";
                return false;
            }

            if (!IsValidVietnamesePhonePrefix(normalizedPhone, out string prefixError))
            {
                errorMessage = prefixError;
                return false;
            }

            // Validate email if provided (email is optional)
            if (!string.IsNullOrWhiteSpace(request.CustomerEmail))
            {
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!emailRegex.IsMatch(request.CustomerEmail))
                {
                    errorMessage = "Email không hợp lệ";
                    return false;
                }
            }

            // Validate shipping address
            if (string.IsNullOrWhiteSpace(request.ShippingAddress) || request.ShippingAddress.Length < 10)
            {
                errorMessage = "Địa chỉ giao hàng phải có ít nhất 10 ký tự";
                return false;
            }

            // Validate payment method
            var validPaymentMethods = new[] { "COD", "BankTransfer", "EWallet" };
            if (!validPaymentMethods.Contains(request.PaymentMethod))
            {
                errorMessage = "Phương thức thanh toán không hợp lệ";
                return false;
            }

            return true;
        }

        public async Task<List<Order>> GetAllOrdersAsync(string? status = null, int page = 1, int pageSize = 20)
        {
            var query = _context.Orders
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status == status);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Normalize Vietnamese phone number: remove spaces/dashes, convert +84 to 0
        /// </summary>
        private string NormalizePhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return string.Empty;

            string normalized = phone
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", "")
                .Trim();

            // Convert +84 to 0
            if (normalized.StartsWith("+84"))
            {
                normalized = "0" + normalized.Substring(3);
            }

            return normalized;
        }

        /// <summary>
        /// Validate Vietnamese phone number prefix
        /// </summary>
        private bool IsValidVietnamesePhonePrefix(string phone, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (phone.Length < 10 || phone.Length > 11)
            {
                errorMessage = "Số điện thoại phải có 10 hoặc 11 chữ số";
                return false;
            }

            // Get first 3 digits for validation
            string prefix3 = phone.Length >= 3 ? phone.Substring(0, 3) : "";
            string prefix2 = phone.Length >= 2 ? phone.Substring(0, 2) : "";

            // Mobile numbers (10 digits) - Valid prefixes:
            // Viettel: 032, 033, 034, 035, 036, 037, 038, 039, 086, 096, 097, 098
            // VinaPhone: 081, 082, 083, 084, 085, 088, 091, 094
            // MobiFone: 070, 076, 077, 078, 079
            // Vietnamobile: 056, 058
            // Gmobile: 059
            var validMobilePrefixes = new[]
            {
                // Viettel
                "032", "033", "034", "035", "036", "037", "038", "039", "086", "096", "097", "098",
                // VinaPhone
                "081", "082", "083", "084", "085", "088", "091", "094",
                // MobiFone
                "070", "076", "077", "078", "079",
                // Vietnamobile
                "056", "058",
                // Gmobile
                "059"
            };

            // Landline numbers (10-11 digits)
            // Hà Nội: 024 (10 digits)
            // TP.HCM: 028 (10 digits)
            // Các tỉnh khác: mã vùng 3 chữ số (10-11 digits)
            // Mã vùng cũ: 02x (Hà Nội, Hải Phòng - 10 digits)
            var validLandlinePrefixes = new[]
            {
                "024", // Hà Nội
                "028", // TP.HCM
                "020", // Các tỉnh miền Bắc (mã vùng cũ)
                "021", // Vĩnh Phúc, Phú Thọ, Hòa Bình, Hà Giang
                "022", // Hải Phòng (mã vùng cũ)
                "023", // Quảng Ninh
                "025", // Hải Dương
                "026", // Hưng Yên
                "027", // Thái Bình
                "029", // Hà Nam, Nam Định, Ninh Bình
                "236", // Đà Nẵng
                "296"  // An Giang (ví dụ)
            };

            // Check if it's a valid mobile number (10 digits)
            if (phone.Length == 10)
            {
                if (validMobilePrefixes.Contains(prefix3))
                    return true;
                // Old landline format (02x): Hà Nội, Hải Phòng
                if (prefix2 == "02")
                    return true;

                errorMessage = "Đầu số không hợp lệ. Vui lòng kiểm tra lại số điện thoại";
                return false;
            }

            // Check if it's a valid landline number (11 digits)
            if (phone.Length == 11)
            {
                // New landline format: 0xx xxxx xxxx (mã vùng 3 chữ số)
                if (validLandlinePrefixes.Contains(prefix3))
                {
                    return true;
                }

                errorMessage = "Đầu số cố định không hợp lệ. Vui lòng kiểm tra lại số điện thoại";
                return false;
            }

            errorMessage = "Số điện thoại không hợp lệ";
            return false;
        }
    }
} 