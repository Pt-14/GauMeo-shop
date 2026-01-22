using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GauMeo.Models;
using GauMeo.Models.ViewModels;
using GauMeo.Services;
using System.Security.Claims;

namespace GauMeo.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(
            IReviewService reviewService,
            UserManager<ApplicationUser> userManager,
            ILogger<ReviewController> logger)
        {
            _reviewService = reviewService;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { 
                        success = false, 
                        message = "Dữ liệu không hợp lệ",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                // Kiểm tra user có thể review không
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    var canReview = await _reviewService.CanUserReviewProductAsync(userId, model.ProductId);
                    if (!canReview)
                    {
                        return Json(new { 
                            success = false, 
                            message = "Bạn đã đánh giá sản phẩm này rồi hoặc chưa mua sản phẩm." 
                        });
                    }
                }

                var reviewId = await _reviewService.CreateReviewAsync(model, userId);

                // Cập nhật rating trung bình của sản phẩm
                await _reviewService.UpdateProductRatingAsync(model.ProductId);

                return Json(new { 
                    success = true, 
                    message = "Đánh giá của bạn đã được gửi thành công!",
                    reviewId = reviewId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating review for product {ProductId}", model.ProductId);
                return Json(new { 
                    success = false, 
                    message = "Có lỗi xảy ra khi gửi đánh giá. Vui lòng thử lại." 
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProductReviews(int productId, int page = 1, int pageSize = 5, string sortBy = "newest")
        {
            try
            {
                var reviews = await _reviewService.GetProductReviewsAsync(productId, page, pageSize, sortBy);
                var statistics = await _reviewService.GetProductReviewStatisticsAsync(productId);

                var result = new ProductReviewsViewModel
                {
                    ProductId = productId,
                    Reviews = reviews.Reviews,
                    TotalReviews = reviews.TotalReviews,
                    CurrentPage = reviews.CurrentPage,
                    TotalPages = reviews.TotalPages,
                    Statistics = statistics
                };

                return PartialView("_ProductReviews", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting reviews for product {ProductId}", productId);
                return Json(new { 
                    success = false, 
                    message = "Có lỗi xảy ra khi tải đánh giá." 
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        return Json(new
                        {
                            isAuthenticated = true,
                            fullName = user.FullName,
                            email = user.Email
                        });
                    }
                }

                return Json(new { isAuthenticated = false });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user info for review form");
                return Json(new { isAuthenticated = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            try
            {
                if (image == null || image.Length == 0)
                {
                    return Json(new { success = false, message = "Không có file được tải lên." });
                }

                // Kiểm tra định dạng file
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
                
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return Json(new { 
                        success = false, 
                        message = "Chỉ chấp nhận file ảnh (jpg, jpeg, png, gif)." 
                    });
                }

                // Kiểm tra kích thước file (max 5MB)
                if (image.Length > 5 * 1024 * 1024)
                {
                    return Json(new { 
                        success = false, 
                        message = "File ảnh không được vượt quá 5MB." 
                    });
                }

                // Tạo tên file unique
                var fileName = $"review_{Guid.NewGuid()}{fileExtension}";
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "reviews");
                
                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, fileName);
                
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                var imageUrl = $"/images/reviews/{fileName}";

                return Json(new { 
                    success = true, 
                    imageUrl = imageUrl,
                    fileName = fileName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading review image");
                return Json(new { 
                    success = false, 
                    message = "Có lỗi xảy ra khi tải ảnh lên." 
                });
            }
        }
    }
}
