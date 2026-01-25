using GauMeo.Data;
using GauMeo.Models.Products;
using GauMeo.Models.ViewModels;
using GauMeo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GauMeo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IReviewService _reviewService;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(
            ApplicationDbContext context,
            IReviewService reviewService,
            ILogger<ReviewController> logger)
        {
            _context = context;
            _reviewService = reviewService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? productId, string sortOrder, int? page)
        {
            try
            {
                int pageSize = 10;
                int pageNumber = page ?? 1;

                var query = _context.Reviews
                    .Include(r => r.Product)
                    .Include(r => r.ReviewImages)
                    .AsQueryable();

                if (productId.HasValue)
                {
                    query = query.Where(r => r.ProductId == productId.Value);
                }

                // Apply sorting
                switch (sortOrder)
                {
                    case "date_desc":
                        query = query.OrderByDescending(r => r.CreatedAt);
                        break;
                    case "date_asc":
                        query = query.OrderBy(r => r.CreatedAt);
                        break;
                    case "rating_desc":
                        query = query.OrderByDescending(r => r.Rating);
                        break;
                    case "rating_asc":
                        query = query.OrderBy(r => r.Rating);
                        break;
                    default:
                        query = query.OrderByDescending(r => r.CreatedAt);
                        break;
                }

                // Get statistics
                var totalReviews = await query.CountAsync();
                var approvedReviews = await query.CountAsync(r => r.IsApproved);
                var pendingReviews = await query.CountAsync(r => !r.IsApproved);
                var verifiedReviews = await query.CountAsync(r => r.IsVerifiedPurchase);
                var averageRating = await query.AverageAsync(r => r.Rating);
                var todayReviews = await query.CountAsync(r => r.CreatedAt.Date == DateTime.Today);

                ViewBag.Statistics = new
                {
                    TotalReviews = totalReviews,
                    ApprovedReviews = approvedReviews,
                    PendingReviews = pendingReviews,
                    VerifiedReviews = verifiedReviews,
                    AverageRating = Math.Round(averageRating, 1),
                    TodayReviews = todayReviews
                };

                var reviews = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var totalPages = (int)Math.Ceiling(totalReviews / (double)pageSize);

                ViewBag.CurrentSort = sortOrder;
                ViewBag.DateSortParam = sortOrder == "date_asc" ? "date_desc" : "date_asc";
                ViewBag.RatingSortParam = sortOrder == "rating_asc" ? "rating_desc" : "rating_asc";
                ViewBag.CurrentPage = pageNumber;
                ViewBag.TotalPages = totalPages;
                ViewBag.ProductId = productId;

                return View(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving reviews in admin panel");
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách đánh giá.";
                return View(new List<Review>());
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.ReviewImages)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }
    }
} 