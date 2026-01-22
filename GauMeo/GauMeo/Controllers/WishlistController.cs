using GauMeo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GauMeo.Models;

namespace GauMeo.Controllers
{
    [Authorize] // Chỉ user đã login mới được dùng wishlist
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly UserManager<ApplicationUser> _userManager;

        public WishlistController(IWishlistService wishlistService, UserManager<ApplicationUser> userManager)
        {
            _wishlistService = wishlistService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập để sử dụng wishlist" });
            }

            var result = await _wishlistService.AddToWishlistAsync(userId, productId);
            if (result)
            {
                var count = await _wishlistService.GetWishlistCountAsync(userId);
                return Json(new { success = true, message = "Đã thêm vào danh sách yêu thích", count = count });
            }

            return Json(new { success = false, message = "Không thể thêm sản phẩm vào wishlist" });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int productId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập để sử dụng wishlist" });
            }

            var result = await _wishlistService.RemoveFromWishlistAsync(userId, productId);
            if (result)
            {
                var count = await _wishlistService.GetWishlistCountAsync(userId);
                return Json(new { success = true, message = "Đã xóa khỏi danh sách yêu thích", count = count });
            }

            return Json(new { success = false, message = "Không thể xóa sản phẩm khỏi wishlist" });
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, items = new object[] { } });
            }            var items = await _wishlistService.GetWishlistItemsAsync(userId);
            var result = items.Select(item => new
            {
                id = item.Id,
                productId = item.ProductId,
                productName = item.Product.Name,
                productPrice = item.Product.CurrentPrice,
                productImage = item.Product.ProductImages?.FirstOrDefault(img => img.IsMain)?.ImageUrl 
                            ?? item.Product.ProductImages?.FirstOrDefault()?.ImageUrl 
                            ?? "/images/products/default-product.jpg",
                brandName = item.Product.Brand?.Name,
                categoryName = item.Product.Category?.Name,
                hasVariants = item.Product.ProductVariants?.Any() == true,
                addedAt = item.AddedAt.ToString("dd/MM/yyyy")
            });

            return Json(new { success = true, items = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetCount()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { count = 0 });
            }

            var count = await _wishlistService.GetWishlistCountAsync(userId);
            return Json(new { count = count });
        }

        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập để sử dụng wishlist" });
            }

            var result = await _wishlistService.ClearWishlistAsync(userId);
            if (result)
            {
                return Json(new { success = true, message = "Đã xóa tất cả sản phẩm khỏi wishlist", count = 0 });
            }

            return Json(new { success = false, message = "Không thể xóa wishlist" });
        }

        [HttpGet]
        public async Task<IActionResult> Check(int productId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { inWishlist = false });
            }

            var inWishlist = await _wishlistService.IsInWishlistAsync(userId, productId);
            return Json(new { inWishlist = inWishlist });
        }
    }
} 