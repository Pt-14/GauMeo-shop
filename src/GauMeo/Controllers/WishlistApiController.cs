using GauMeo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GauMeo.Models;

namespace GauMeo.Controllers
{
    [ApiController]
    [Route("api/wishlist")]
    [Authorize]
    public class WishlistApiController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly UserManager<ApplicationUser> _userManager;

        public WishlistApiController(IWishlistService wishlistService, UserManager<ApplicationUser> userManager)
        {
            _wishlistService = wishlistService;
            _userManager = userManager;
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { success = false, message = "Vui lòng đăng nhập để sử dụng wishlist" });
            }

            var count = await _wishlistService.GetWishlistCountAsync(userId);
            return Ok(new { success = true, count = count });
        }

        [HttpGet("check/{productId}")]
        public async Task<IActionResult> Check(int productId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { success = false, message = "Vui lòng đăng nhập để sử dụng wishlist" });
            }

            var inWishlist = await _wishlistService.IsInWishlistAsync(userId, productId);
            return Ok(new { success = true, isInWishlist = inWishlist });
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] WishlistRequest request)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { success = false, message = "Vui lòng đăng nhập để sử dụng wishlist" });
            }

            var result = await _wishlistService.AddToWishlistAsync(userId, request.ProductId);
            if (result)
            {
                var count = await _wishlistService.GetWishlistCountAsync(userId);
                return Ok(new { success = true, message = "Đã thêm vào danh sách yêu thích", count = count });
            }

            return BadRequest(new { success = false, message = "Không thể thêm sản phẩm vào wishlist" });
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] WishlistRequest request)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { success = false, message = "Vui lòng đăng nhập để sử dụng wishlist" });
            }

            var result = await _wishlistService.RemoveFromWishlistAsync(userId, request.ProductId);
            if (result)
            {
                var count = await _wishlistService.GetWishlistCountAsync(userId);
                return Ok(new { success = true, message = "Đã xóa khỏi danh sách yêu thích", count = count });
            }

            return BadRequest(new { success = false, message = "Không thể xóa sản phẩm khỏi wishlist" });
        }
    }

    public class WishlistRequest
    {
        public int ProductId { get; set; }
    }
} 