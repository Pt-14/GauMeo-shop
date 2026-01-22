using GauMeo.Data;
using GauMeo.Models.ViewModels;
using GauMeo.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace GauMeo.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReviewService> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReviewService(ApplicationDbContext context, ILogger<ReviewService> logger, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }        public async Task<ProductReviewsViewModel> GetProductReviewsAsync(int productId, int page = 1, int pageSize = 10, string sortBy = "newest")
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
                if (product == null)
                {
                    throw new ArgumentException($"Product with ID {productId} not found");
                }

                var totalReviews = await _context.Reviews
                    .Where(r => r.ProductId == productId && r.IsApproved)
                    .CountAsync();

                var query = _context.Reviews
                    .Include(r => r.ReviewImages)
                    .Where(r => r.ProductId == productId && r.IsApproved);

                // Apply sorting
                query = sortBy.ToLower() switch
                {
                    "oldest" => query.OrderBy(r => r.CreatedAt),
                    "highest_rating" => query.OrderByDescending(r => r.Rating).ThenByDescending(r => r.CreatedAt),
                    "lowest_rating" => query.OrderBy(r => r.Rating).ThenByDescending(r => r.CreatedAt),
                    _ => query.OrderByDescending(r => r.CreatedAt) // Default: newest
                };

                var reviews = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(r => new ReviewDisplayViewModel
                    {
                        Id = r.Id,
                        Rating = r.Rating,
                        Comment = r.Comment,
                        CustomerName = r.CustomerName,
                        IsVerifiedPurchase = r.IsVerifiedPurchase,
                        CreatedAt = r.CreatedAt,
                        ImageUrls = r.ReviewImages.OrderBy(ri => ri.DisplayOrder).Select(ri => ri.ImageUrl).ToList()
                    })
                    .ToListAsync();

                var statistics = await GetProductReviewStatisticsAsync(productId);

                return new ProductReviewsViewModel
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    Statistics = statistics,
                    Reviews = reviews,
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling((double)totalReviews / pageSize),
                    TotalReviews = totalReviews
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product reviews for product {ProductId}", productId);
                return new ProductReviewsViewModel();
            }
        }

        public async Task<ReviewStatisticsViewModel> GetProductReviewStatisticsAsync(int productId)
        {
            try
            {
                var reviews = await _context.Reviews
                    .Where(r => r.ProductId == productId && r.IsApproved)
                    .ToListAsync();

                var statistics = new ReviewStatisticsViewModel();

                if (reviews.Any())
                {
                    statistics.TotalReviews = reviews.Count;
                    statistics.AverageRating = Math.Round(reviews.Average(r => r.Rating), 1);

                    // Calculate rating breakdown
                    for (int i = 1; i <= 5; i++)
                    {
                        statistics.RatingBreakdown[i] = reviews.Count(r => r.Rating == i);
                    }
                }

                return statistics;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting review statistics for product {ProductId}", productId);
                return new ReviewStatisticsViewModel();
            }
        }

        public async Task<bool> CreateReviewAsync(CreateReviewViewModel model, string? userId)
        {
            try
            {
                // Check if user already reviewed this product
                if (!string.IsNullOrEmpty(userId))
                {
                    var existingReview = await _context.Reviews
                        .FirstOrDefaultAsync(r => r.ProductId == model.ProductId && r.UserId == userId);
                    
                    if (existingReview != null)
                    {
                        _logger.LogWarning("User {UserId} already reviewed product {ProductId}", userId, model.ProductId);
                        return false;
                    }
                }

                var review = new Review
                {
                    ProductId = model.ProductId,
                    Rating = model.Rating,
                    Comment = model.Comment,
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    UserId = userId,
                    IsVerifiedPurchase = await IsVerifiedPurchaseAsync(userId, model.ProductId),
                    IsApproved = true, // Auto-approve for now, can be changed later
                    CreatedAt = DateTime.Now
                };

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                // Save images if any
                if (model.Images != null && model.Images.Any())
                {
                    var imageUrls = await SaveReviewImagesAsync(model.Images, review.Id);
                    // Images are already saved in SaveReviewImagesAsync method
                }

                // Update product rating
                await UpdateProductRatingAsync(model.ProductId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating review for product {ProductId}", model.ProductId);
                return false;
            }
        }

        public async Task<bool> CanUserReviewProductAsync(string? userId, int productId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return true; // Guest can review
                }

                var existingReview = await _context.Reviews
                    .FirstOrDefaultAsync(r => r.ProductId == productId && r.UserId == userId);

                return existingReview == null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if user can review product {ProductId}", productId);
                return false;
            }
        }

        public async Task<bool> UpdateProductRatingAsync(int productId)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
                if (product == null) return false;

                var reviews = await _context.Reviews
                    .Where(r => r.ProductId == productId && r.IsApproved)
                    .ToListAsync();

                if (reviews.Any())
                {
                    product.Rating = Math.Round(reviews.Average(r => r.Rating), 1);
                    product.ReviewCount = reviews.Count;
                }
                else
                {
                    product.Rating = 0;
                    product.ReviewCount = 0;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product rating for product {ProductId}", productId);
                return false;
            }
        }

        public async Task<List<string>> SaveReviewImagesAsync(List<IFormFile> images, int reviewId)
        {
            var imageUrls = new List<string>();

            try
            {
                if (images == null || !images.Any()) return imageUrls;

                var reviewImagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "reviews", reviewId.ToString());
                Directory.CreateDirectory(reviewImagesPath);

                int displayOrder = 0;
                foreach (var image in images.Take(5)) // Limit to 5 images
                {
                    if (image.Length > 0 && image.Length <= 2 * 1024 * 1024) // Max 2MB per image
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                        var filePath = Path.Combine(reviewImagesPath, fileName);
                        var imageUrl = $"/images/reviews/{reviewId}/{fileName}";

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        var reviewImage = new ReviewImage
                        {
                            ReviewId = reviewId,
                            ImageUrl = imageUrl,
                            AltText = $"Review image {displayOrder + 1}",
                            DisplayOrder = displayOrder++,
                            CreatedAt = DateTime.Now
                        };

                        _context.ReviewImages.Add(reviewImage);
                        imageUrls.Add(imageUrl);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving review images for review {ReviewId}", reviewId);
            }

            return imageUrls;
        }

        private async Task<bool> IsVerifiedPurchaseAsync(string? userId, int productId)
        {
            if (string.IsNullOrEmpty(userId)) return false;

            try
            {
                // Check if user has purchased this product (simplified logic)
                var hasPurchased = await _context.OrderItems
                    .Include(oi => oi.Order)
                    .AnyAsync(oi => oi.ProductId == productId && 
                                   oi.Order.UserId == userId && 
                                   oi.Order.Status == "Delivered");

                return hasPurchased;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking verified purchase for user {UserId}, product {ProductId}", userId, productId);
                return false;
            }
        }
    }
}
