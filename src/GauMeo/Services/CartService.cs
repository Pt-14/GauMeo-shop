using GauMeo.Data;
using GauMeo.Models;
using GauMeo.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace GauMeo.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CartService> _logger;

        public CartService(ApplicationDbContext context, ILogger<CartService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Cart> GetOrCreateCartAsync(string? userId, string? sessionId)
        {
            // Try to get existing cart
            var cart = await GetCartAsync(userId, sessionId);
            
            if (cart == null)
            {
                // Create new cart
                cart = new Cart
                {
                    UserId = userId,
                    SessionId = sessionId ?? Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        public async Task<Cart?> GetCartAsync(string? userId, string? sessionId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                // For logged-in users, find by UserId
                return await _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                            .ThenInclude(p => p.ProductImages.Where(img => img.IsMain))
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                            .ThenInclude(p => p.Brand)
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                            .ThenInclude(p => p.ProductVariants.Where(v => v.IsActive))
                    .FirstOrDefaultAsync(c => c.UserId == userId);
            }
            else if (!string.IsNullOrEmpty(sessionId))
            {
                // For guests, find by SessionId
                return await _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                            .ThenInclude(p => p.ProductImages.Where(img => img.IsMain))
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                            .ThenInclude(p => p.Brand)
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                            .ThenInclude(p => p.ProductVariants.Where(v => v.IsActive))
                    .FirstOrDefaultAsync(c => c.SessionId == sessionId && c.UserId == null);
            }

            return null;
        }

        public async Task<int> GetCartItemCountAsync(string? userId, string? sessionId)
        {
            var cart = await GetCartAsync(userId, sessionId);
            return cart?.GetTotalItems() ?? 0;
        }

        public async Task<decimal> GetCartTotalAsync(string? userId, string? sessionId)
        {
            var cart = await GetCartAsync(userId, sessionId);
            return cart?.GetTotalAmount() ?? 0;
        }

        public async Task<(bool Success, string? ErrorMessage)> AddToCartAsync(string? userId, string? sessionId, int productId, int quantity, Dictionary<string, string>? selectedVariants = null)
        {
            try
            {
                // Validate quantity
                if (quantity <= 0)
                {
                    return (false, "Số lượng phải lớn hơn 0");
                }

                // Validate product exists
                var product = await _context.Products
                    .Include(p => p.ProductVariants.Where(v => v.IsActive))
                    .FirstOrDefaultAsync(p => p.Id == productId);
                    
                if (product == null || !product.IsActive)
                {
                    _logger.LogWarning("Product {ProductId} not found or inactive", productId);
                    return (false, "Sản phẩm không tồn tại hoặc đã ngừng kinh doanh");
                }

                // Validate stock quantity
                if (product.StockQuantity <= 0)
                {
                    return (false, "Sản phẩm đã hết hàng");
                }

                // Get or create cart
                var cart = await GetOrCreateCartAsync(userId, sessionId);

                // Check if product with same variants already exists in cart
                var selectedVariantsJson = selectedVariants != null ? 
                    System.Text.Json.JsonSerializer.Serialize(selectedVariants) : "";
                
                var existingCartItem = await _context.CartItems
                    .Include(ci => ci.Product)
                    .FirstOrDefaultAsync(ci => ci.CartId == cart.Id && 
                                             ci.ProductId == productId && 
                                             ci.SelectedVariants == selectedVariantsJson);

                // Calculate total quantity after adding
                int totalQuantity = quantity;
                if (existingCartItem != null)
                {
                    totalQuantity += existingCartItem.Quantity;
                }

                // Validate total quantity against stock
                if (totalQuantity > product.StockQuantity)
                {
                    int availableQuantity = product.StockQuantity - (existingCartItem?.Quantity ?? 0);
                    if (availableQuantity <= 0)
                    {
                        return (false, $"Sản phẩm chỉ còn {product.StockQuantity} sản phẩm trong kho");
                    }
                    return (false, $"Số lượng vượt quá tồn kho. Chỉ còn {availableQuantity} sản phẩm có thể thêm vào giỏ hàng");
                }

                // Calculate unit price based on variants
                decimal unitPrice = product.OriginalPrice;
                
                // If has variants and selected variants, calculate price with variant adjustment
                if (selectedVariants != null && selectedVariants.Any() && 
                    product.ProductVariants != null && product.ProductVariants.Any())
                {
                    // Find variant by matching selected variant name
                    foreach (var selectedVariant in selectedVariants)
                    {
                        var variant = product.ProductVariants
                            .FirstOrDefault(v => v.Type?.ToLower() == selectedVariant.Key.ToLower() && 
                                               v.Name == selectedVariant.Value && v.IsActive);
                        
                        if (variant != null && variant.PriceAdjustment.HasValue)
                        {
                            unitPrice += variant.PriceAdjustment.Value;
                        }
                    }
                }
                
                // Apply discount if product is on sale
                if (product.IsOnSale && product.DiscountPercent > 0)
                {
                    unitPrice = unitPrice * (100 - product.DiscountPercent) / 100;
                }

                if (existingCartItem != null)
                {
                    // Update quantity
                    existingCartItem.Quantity = totalQuantity;
                    existingCartItem.UpdatedAt = DateTime.Now;
                }
                else
                {
                    // Create new cart item
                    var cartItem = new CartItem
                    {
                        CartId = cart.Id,
                        ProductId = productId,
                        Quantity = quantity,
                        UnitPrice = unitPrice, // Use calculated price
                        SelectedVariants = selectedVariantsJson,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    _context.CartItems.Add(cartItem);
                }

                // Update cart timestamp
                cart.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product {ProductId} to cart", productId);
                return (false, "Có lỗi xảy ra khi thêm sản phẩm vào giỏ hàng");
            }
        }

        public async Task<(bool Success, string? ErrorMessage)> UpdateCartItemAsync(int cartItemId, int quantity)
        {
            try
            {
                var cartItem = await _context.CartItems
                    .Include(ci => ci.Product)
                    .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
                    
                if (cartItem == null)
                    return (false, "Sản phẩm không tồn tại trong giỏ hàng");

                if (quantity <= 0)
                {
                    // Remove item if quantity is 0 or negative
                    var removed = await RemoveFromCartAsync(cartItemId);
                    return (removed, removed ? null : "Không thể xóa sản phẩm");
                }

                // Validate stock quantity
                var product = cartItem.Product;
                if (product == null || !product.IsActive)
                {
                    return (false, "Sản phẩm không tồn tại hoặc đã ngừng kinh doanh");
                }

                if (product.StockQuantity <= 0)
                {
                    return (false, "Sản phẩm đã hết hàng");
                }

                if (quantity > product.StockQuantity)
                {
                    return (false, $"Số lượng vượt quá tồn kho. Chỉ còn {product.StockQuantity} sản phẩm");
                }

                cartItem.Quantity = quantity;
                cartItem.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating cart item {CartItemId}", cartItemId);
                return (false, "Có lỗi xảy ra khi cập nhật giỏ hàng");
            }
        }

        public async Task<bool> RemoveFromCartAsync(int cartItemId)
        {
            try
            {
                var cartItem = await _context.CartItems.FindAsync(cartItemId);
                if (cartItem == null)
                    return true; // Already removed

                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cart item {CartItemId}", cartItemId);
                return false;
            }
        }

        public async Task<bool> ClearCartAsync(string? userId, string? sessionId)
        {
            try
            {
                var cart = await GetCartAsync(userId, sessionId);
                if (cart == null)
                    return true;

                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing cart for user {UserId} session {SessionId}", userId, sessionId);
                return false;
            }
        }

        public async Task<bool> MergeCartAsync(string sessionId, string userId)
        {
            try
            {
                // Get guest cart
                var guestCart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.SessionId == sessionId && c.UserId == null);

                if (guestCart == null || !guestCart.CartItems.Any())
                    return true; // No guest cart to merge

                // Get or create user cart
                var userCart = await GetOrCreateCartAsync(userId, null);

                // Merge items
                foreach (var guestItem in guestCart.CartItems)
                {
                    var existingItem = await _context.CartItems
                        .FirstOrDefaultAsync(ci => ci.CartId == userCart.Id && 
                                                  ci.ProductId == guestItem.ProductId &&
                                                  ci.SelectedVariants == guestItem.SelectedVariants);

                    if (existingItem != null)
                    {
                        // Merge quantities
                        existingItem.Quantity += guestItem.Quantity;
                        existingItem.UpdatedAt = DateTime.Now;
                    }
                    else
                    {
                        // Move item to user cart
                        guestItem.CartId = userCart.Id;
                        guestItem.UpdatedAt = DateTime.Now;
                    }
                }

                // Remove guest cart
                _context.Carts.Remove(guestCart);
                userCart.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error merging cart from session {SessionId} to user {UserId}", sessionId, userId);
                return false;
            }
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(string? userId, string? sessionId)
        {
            var cart = await GetCartAsync(userId, sessionId);
            if (cart == null)
                return Enumerable.Empty<CartItem>();

            return cart.CartItems.OrderByDescending(ci => ci.CreatedAt);
        }
    }
}
