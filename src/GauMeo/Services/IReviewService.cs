using GauMeo.Models.ViewModels;
using GauMeo.Models.Products;

namespace GauMeo.Services
{
    public interface IReviewService
    {
        Task<ProductReviewsViewModel> GetProductReviewsAsync(int productId, int page = 1, int pageSize = 10, string sortBy = "newest");
        Task<ReviewStatisticsViewModel> GetProductReviewStatisticsAsync(int productId);
        Task<bool> CreateReviewAsync(CreateReviewViewModel model, string? userId);
        Task<bool> CanUserReviewProductAsync(string? userId, int productId);
        Task<bool> UpdateProductRatingAsync(int productId);
        Task<List<string>> SaveReviewImagesAsync(List<IFormFile> images, int reviewId);
    }
}
