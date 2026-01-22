using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.ViewModels
{
    public class ReviewDisplayViewModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public bool IsVerifiedPurchase { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }

    public class ReviewStatisticsViewModel
    {
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public Dictionary<int, int> RatingBreakdown { get; set; } = new Dictionary<int, int>();
        
        public ReviewStatisticsViewModel()
        {
            // Initialize rating breakdown with 0 counts
            for (int i = 1; i <= 5; i++)
            {
                RatingBreakdown[i] = 0;
            }
        }
    }    public class ProductReviewsViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public ReviewStatisticsViewModel Statistics { get; set; } = new ReviewStatisticsViewModel();
        public List<ReviewDisplayViewModel> Reviews { get; set; } = new List<ReviewDisplayViewModel>();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int TotalReviews { get; set; } = 0;
        public bool CanUserReview { get; set; } = true;
    }
}
