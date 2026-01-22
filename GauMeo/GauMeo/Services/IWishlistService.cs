using GauMeo.Models;
using GauMeo.Models.Products;

namespace GauMeo.Services
{
    public interface IWishlistService
    {
        Task<IEnumerable<WishlistItem>> GetWishlistItemsAsync(string userId);
        Task<bool> AddToWishlistAsync(string userId, int productId);
        Task<bool> RemoveFromWishlistAsync(string userId, int productId);
        Task<bool> IsInWishlistAsync(string userId, int productId);
        Task<int> GetWishlistCountAsync(string userId);
        Task<bool> ClearWishlistAsync(string userId);
    }
} 