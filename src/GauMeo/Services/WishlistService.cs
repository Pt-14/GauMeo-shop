using GauMeo.Data;
using GauMeo.Models;
using GauMeo.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace GauMeo.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly ApplicationDbContext _context;

        public WishlistService(ApplicationDbContext context)
        {
            _context = context;
        }        public async Task<IEnumerable<WishlistItem>> GetWishlistItemsAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return Enumerable.Empty<WishlistItem>();

            return await _context.WishlistItems
                .Include(w => w.Product)
                    .ThenInclude(p => p.Brand)
                .Include(w => w.Product)
                    .ThenInclude(p => p.Category)
                .Include(w => w.Product)
                    .ThenInclude(p => p.ProductImages)
                .Include(w => w.Product)
                    .ThenInclude(p => p.ProductVariants)
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.AddedAt)
                .ToListAsync();
        }

        public async Task<bool> AddToWishlistAsync(string userId, int productId)
        {
            if (string.IsNullOrEmpty(userId) || productId <= 0)
                return false;

            // Kiểm tra product có tồn tại
            var productExists = await _context.Products.AnyAsync(p => p.Id == productId);
            if (!productExists)
                return false;

            // Kiểm tra đã có trong wishlist chưa
            var existingItem = await _context.WishlistItems
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

            if (existingItem != null)
                return true; // Đã có rồi, return true

            // Thêm vào wishlist
            var wishlistItem = new WishlistItem
            {
                UserId = userId,
                ProductId = productId,
                AddedAt = DateTime.Now
            };

            _context.WishlistItems.Add(wishlistItem);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> RemoveFromWishlistAsync(string userId, int productId)
        {
            if (string.IsNullOrEmpty(userId) || productId <= 0)
                return false;

            var wishlistItem = await _context.WishlistItems
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

            if (wishlistItem == null)
                return true; // Không có thì cũng return true

            _context.WishlistItems.Remove(wishlistItem);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> IsInWishlistAsync(string userId, int productId)
        {
            if (string.IsNullOrEmpty(userId) || productId <= 0)
                return false;

            return await _context.WishlistItems
                .AnyAsync(w => w.UserId == userId && w.ProductId == productId);
        }

        public async Task<int> GetWishlistCountAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return 0;

            return await _context.WishlistItems
                .CountAsync(w => w.UserId == userId);
        }

        public async Task<bool> ClearWishlistAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return false;

            var wishlistItems = await _context.WishlistItems
                .Where(w => w.UserId == userId)
                .ToListAsync();

            if (!wishlistItems.Any())
                return true;

            _context.WishlistItems.RemoveRange(wishlistItems);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
} 