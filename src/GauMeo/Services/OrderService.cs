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

                // Get cart items
                var cartItems = await _cartService.GetCartItemsAsync(userId, sessionId);
                if (!cartItems.Any())
                {
                    throw new InvalidOperationException("Giỏ hàng trống, không thể tạo đơn hàng");
                }

                // Calculate totals
                var subTotal = cartItems.Sum(item => item.GetSubTotal());
                var totalAmount = subTotal + request.ShippingFee - request.DiscountAmount;

                // Create order
                var order = new Order
                {
                    OrderNumber = Order.GenerateOrderNumber(),
                    CustomerName = request.CustomerName,
                    CustomerPhone = request.CustomerPhone,
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

                // Create order items from cart items
                foreach (var cartItem in cartItems)
                {
                    // Calculate original price with variants for order item
                    decimal originalPrice = cartItem.Product.OriginalPrice;
                    
                    // Add variant adjustments if any
                    if (!string.IsNullOrEmpty(cartItem.SelectedVariants))
                    {
                        var variants = cartItem.GetSelectedVariants();
                        foreach (var variant in variants)
                        {
                            var productVariant = cartItem.Product.ProductVariants?
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
                        ProductName = cartItem.Product.Name,
                        ProductBrand = cartItem.Product.Brand?.Name ?? "",
                        ProductImageUrl = cartItem.Product.ProductImages?.FirstOrDefault()?.ImageUrl ?? "/images/products/default-product.jpg",
                        Quantity = cartItem.Quantity,
                        UnitPrice = cartItem.UnitPrice,
                        OriginalPrice = originalPrice,
                        DiscountPercent = cartItem.Product.DiscountPercent,
                        SelectedVariants = cartItem.SelectedVariants,
                        SubTotal = cartItem.GetSubTotal(),
                        CreatedAt = DateTime.Now
                    };

                    _context.OrderItems.Add(orderItem);
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
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                    return false;

                var oldStatus = order.Status;
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

                _logger.LogInformation("Order {OrderId} status updated from {OldStatus} to {NewStatus}", 
                    orderId, oldStatus, status);

                return true;
            }
            catch (Exception ex)
            {
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

            var phoneRegex = new Regex(@"^(0|\+84)[0-9]{9,10}$");
            if (!phoneRegex.IsMatch(request.CustomerPhone.Replace(" ", "").Replace("-", "")))
            {
                errorMessage = "Số điện thoại không hợp lệ";
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
    }
} 