using GauMeo.Models;
using GauMeo.Models.Orders;

namespace GauMeo.Services
{
    public interface ICartService
    {
        // Cart management
        Task<Cart> GetOrCreateCartAsync(string? userId, string? sessionId);
        Task<Cart?> GetCartAsync(string? userId, string? sessionId);
        Task<int> GetCartItemCountAsync(string? userId, string? sessionId);
        Task<decimal> GetCartTotalAsync(string? userId, string? sessionId);
        
        // Cart items management
        Task<bool> AddToCartAsync(string? userId, string? sessionId, int productId, int quantity, Dictionary<string, string>? selectedVariants = null);
        Task<bool> UpdateCartItemAsync(int cartItemId, int quantity);
        Task<bool> RemoveFromCartAsync(int cartItemId);
        Task<bool> ClearCartAsync(string? userId, string? sessionId);
        
        // Cart merging for guest to user
        Task<bool> MergeCartAsync(string sessionId, string userId);
        
        // Get cart items with product details
        Task<IEnumerable<CartItem>> GetCartItemsAsync(string? userId, string? sessionId);
    }
} 