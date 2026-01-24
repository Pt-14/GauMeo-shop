using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using GauMeo.Models;
using GauMeo.Services;
using GauMeo.Models.Categories;
using GauMeo.Models.Products;
using GauMeo.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using GauMeo.Data;

namespace GauMeo.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IReviewService _reviewService;
        private readonly ILogger<ProductController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProductController(
            ICategoryService categoryService, 
            IProductService productService,
            IReviewService reviewService,
            ILogger<ProductController> logger,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _categoryService = categoryService;
            _productService = productService;
            _reviewService = reviewService;
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }
        // Backward compatibility - Trang tổng quan sản phẩm cho chó
        public async Task<IActionResult> Dog(string[]? brand = null, string priceRange = "", string rating = "", string onSale = "", string freeShipping = "", string sort = "popular")
        {
            try
            {
                var dogCategory = await _categoryService.GetMainCategoryAsync("dog");
                if (dogCategory == null)
                {
                    _logger.LogWarning("Dog main category not found in database");
                    return NotFound("Dog category not found");
                }
                
                return await Category(dogCategory.Slug, brand, priceRange, rating, onSale, freeShipping, sort);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Dog method");
                return StatusCode(500, "Internal server error");
            }
        }

        // Backward compatibility - Trang tổng quan sản phẩm cho mèo
        public async Task<IActionResult> Cat(string[]? brand = null, string priceRange = "", string rating = "", string onSale = "", string freeShipping = "", string sort = "popular")
        {
            try
            {
                var catCategory = await _categoryService.GetMainCategoryAsync("cat");
                if (catCategory == null)
                {
                    _logger.LogWarning("Cat main category not found in database");
                    return NotFound("Cat category not found");
                }
                
                return await Category(catCategory.Slug, brand, priceRange, rating, onSale, freeShipping, sort);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Cat method");
                return StatusCode(500, "Internal server error");
            }
        }













        public async Task<IActionResult> Detail(int id = 1)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found");
                }
                
                // Increase popularity score when someone views the product
                await _productService.IncreasePopularityScoreAsync(id);
                
                // Calculate price range if product has variants
                var hasVariants = product.ProductVariants != null && product.ProductVariants.Any(v => v.IsActive);
                decimal minPrice = product.OriginalPrice;
                decimal maxPrice = product.OriginalPrice;
                
                if (hasVariants && product.ProductVariants != null)
                {
                    var variantPrices = product.ProductVariants
                        .Where(v => v.IsActive)
                        .Select(v => product.OriginalPrice + (v.PriceAdjustment ?? 0))
                        .ToList();
                    
                    if (variantPrices.Any())
                    {
                        minPrice = variantPrices.Min();
                        maxPrice = variantPrices.Max();
                    }
                }

                // Build product detail view model from database
                var model = new ProductDetailViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.ShortDescription,
                    // CurrentPrice: giá hiện tại (đã discount) cho sản phẩm không có variants
                    CurrentPrice = hasVariants ? minPrice : product.CurrentPrice,
                    // OriginalPrice: giá gốc cho sản phẩm không có variants  
                    OriginalPrice = hasVariants ? maxPrice : product.OriginalPrice,
                    DiscountPercent = product.DiscountPercent,
                    IsOnSale = product.IsOnSale,
                    Rating = product.Rating,
                    ReviewCount = product.ReviewCount,
                    Brand = product.Brand.Name,
                    Origin = "Nhập khẩu", // Default value
                    StockQuantity = product.StockQuantity,
                    HasVariants = hasVariants,
                    MinPrice = hasVariants ? minPrice : product.OriginalPrice,
                    MaxPrice = hasVariants ? maxPrice : product.OriginalPrice,
                    
                    Images = product.ProductImages
                        .OrderBy(img => img.DisplayOrder)
                        .Select(img => img.ImageUrl)
                        .ToList(),

                    // Map ProductVariants from database - use AllVariants dictionary
                    AllVariants = new Dictionary<string, List<ProductVariantDto>>(),
                    
                    // Map ProductDescriptions từ database
                    ProductDescriptions = product.ProductDescriptions?
                        .Where(pd => pd.IsActive)
                        .OrderBy(pd => pd.DisplayOrder)
                        .Select(pd => new ProductDescriptionDto
                        {
                            Id = pd.Id,
                            Title = pd.Title,
                            Content = pd.Content,
                            DisplayOrder = pd.DisplayOrder
                        }).ToList() ?? new List<ProductDescriptionDto>(),
                    
                    // Use product reviews if available
                    Reviews = _context.Reviews
                        .Include(r => r.ReviewImages)
                        .Where(r => r.ProductId == product.Id)
                        .OrderByDescending(r => r.CreatedAt)
                        .Take(5)
                        .Select(r => new ProductReviewDto
                        {
                            Id = r.Id,
                            CustomerName = r.CustomerName,
                            Rating = r.Rating,
                            Comment = r.Comment,
                            CreatedAt = r.CreatedAt,
                            ImageUrls = r.ReviewImages.OrderBy(ri => ri.DisplayOrder).Select(ri => ri.ImageUrl).ToList()
                        }).ToList() ?? new List<ProductReviewDto>()
                };

                // Populate AllVariants dictionary for flexible display
                if (product.ProductVariants != null && product.ProductVariants.Any())
                {
                    var variantGroups = product.ProductVariants
                        .Where(v => v.IsActive)
                        .GroupBy(v => v.Type?.ToLower() ?? "unknown")
                        .ToDictionary(
                            g => g.Key,
                            g => g.OrderBy(v => v.DisplayOrder)
                                .Select(v => {
                                    var variantPrice = product.OriginalPrice + (v.PriceAdjustment ?? 0);
                                    var finalPrice = variantPrice;
                                    
                                    // Apply discount if product is on sale
                                    if (product.IsOnSale && product.DiscountPercent > 0)
                                    {
                                        finalPrice = variantPrice * (100 - product.DiscountPercent) / 100;
                                    }
                                    
                                    return new ProductVariantDto
                                    {
                                        Id = v.Id,
                                        Name = v.Name,
                                        Type = v.Type,
                                        PriceAdjustment = v.PriceAdjustment,
                                        Price = variantPrice, // Original price (before discount)
                                        OriginalPrice = variantPrice, // Same as Price for variants
                                        IsDefault = v.IsDefault,
                                        IsSelected = v.IsDefault,
                                        DisplayOrder = v.DisplayOrder
                                    };
                                }).ToList()
                        );
                    
                    model.AllVariants = variantGroups;
                }

                // Lấy related products
                var relatedProductsData = await _productService.GetRelatedProductsAsync(product.Id, 4);
                model.RelatedProducts = relatedProductsData.Select(rp => new RelatedProduct
                {
                    Id = (int)rp.Id,
                    Name = (string)rp.Name,
                    Image = (string)rp.ImageUrl,
                    Rating = (double)rp.Rating,
                    CurrentPrice = (decimal)rp.CurrentPrice,
                    OriginalPrice = (decimal)rp.OriginalPrice,
                    DiscountPercent = (int)rp.DiscountPercent,
                    IsOnSale = (bool)rp.IsOnSale,
                    HasVariants = (bool)rp.HasVariants,
                    ReviewCount = (int)rp.ReviewCount,
                    Url = $"/Product/Detail/{rp.Id}"
                }).ToList();
                
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading product detail with ID: {ProductId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("api/product/track-view/{id}")]
        public async Task<IActionResult> TrackProductView(int id)
        {
            try
            {
                await _productService.IncreasePopularityScoreAsync(id);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error tracking view for product {ProductId}", id);
                return Ok(new { success = false }); // Return success to not break frontend
            }
        }

        // Filter and Sorting Methods
        private List<dynamic> ApplyFilters(List<dynamic> products, string[] brand, string priceRange, string rating, string onSale, string freeShipping)
        {
            // Filter by brand
            if (brand != null && brand.Length > 0 && brand.Any(b => !string.IsNullOrEmpty(b)))
            {
                var validBrands = brand.Where(b => !string.IsNullOrEmpty(b)).Select(b => b.ToLower()).ToList();
                products = products.Where(p => validBrands.Contains(p.Brand.ToString().ToLower())).ToList();
            }

            // Filter by price range
            if (!string.IsNullOrEmpty(priceRange))
            {
                var (minPrice, maxPrice) = ParsePriceRange(priceRange);
                products = products.Where(p => {
                    var price = (int)p.Price;
                    var currentPrice = p.IsOnSale ? (int)(price * (100 - p.DiscountPercent) / 100) : price;
                    return currentPrice >= minPrice && (maxPrice == 0 || currentPrice <= maxPrice);
                }).ToList();
            }

            // Filter by rating
            if (!string.IsNullOrEmpty(rating) && double.TryParse(rating, out var minRating))
            {
                products = products.Where(p => p.Rating >= minRating).ToList();
            }

            // Filter by sale status
            if (onSale == "true")
            {
                products = products.Where(p => p.IsOnSale).ToList();
            }

            // Filter by free shipping (placeholder logic)
            if (freeShipping == "true")
            {
                products = products.Where(p => (int)p.Price >= 500000).ToList(); // Free shipping for orders over 500k
            }

            return products;
        }



        private List<dynamic> ApplySorting(List<dynamic> products, string sort)
        {
            return sort switch
            {
                "price-asc" => products.OrderBy(p => p.IsOnSale ? (int)(p.Price * (100 - p.DiscountPercent) / 100) : (int)p.Price).ToList(),
                "price-desc" => products.OrderByDescending(p => p.IsOnSale ? (int)(p.Price * (100 - p.DiscountPercent) / 100) : (int)p.Price).ToList(),
                "name-asc" => products.OrderBy(p => p.Name).ToList(),
                "name-desc" => products.OrderByDescending(p => p.Name).ToList(),
                "rating" => products.OrderByDescending(p => p.Rating).ToList(),
                _ => products.OrderByDescending(p => p.Rating).ThenByDescending(p => p.IsOnSale).ToList(), // popular
            };
        }

        private (int min, int max) ParsePriceRange(string priceRange)
        {
            return priceRange switch
            {
                "0-100000" => (0, 100000),
                "100000-300000" => (100000, 300000),
                "300000-500000" => (300000, 500000),
                "500000-1000000" => (500000, 1000000),
                "1000000-0" => (1000000, 0), // 1M+
                _ => (0, 0)
            };
        }

        private List<string> GetBrands(List<dynamic> products)
        {
            return products.Select(p => (string)p.Brand).Distinct().OrderBy(b => b).ToList();
        }

        private async Task<List<string>> GetBrandsByAnimalTypeAsync(string animalType)
        {
            return await _productService.GetBrandsByAnimalTypeAsync(animalType);
        }

        // NEW METHOD: Handle all category navigation using database
        public async Task<IActionResult> Category(string slug, string[]? brand = null, string priceRange = "", string rating = "", string onSale = "", string freeShipping = "", string sort = "popular")
        {
            try
            {
                // Get category by slug from database
                var category = await _categoryService.GetCategoryBySlugAsync(slug);
                if (category == null)
                {
                    return NotFound($"Category with slug '{slug}' not found");
                }

                // Build breadcrumb trail
                var breadcrumbs = await BuildBreadcrumbsAsync(category);

                // Handle different levels
                switch (category.Level)
                {
                    case 1: // Main category (chó/mèo) - Show level 2 categories
                        return await HandleLevel1Category(category, brand, priceRange, rating, onSale, freeShipping, sort, breadcrumbs);
                    
                    case 2: // Parent category (thức ăn, đồ chơi) - Show level 3 categories  
                        return await HandleLevel2Category(category, brand, priceRange, rating, onSale, freeShipping, sort, breadcrumbs);
                    
                    case 3: // Sub category (thức ăn hạt) - Show products
                        return await HandleLevel3Category(category, brand, priceRange, rating, onSale, freeShipping, sort, breadcrumbs);
                    
                    default:
                        return BadRequest($"Invalid category level: {category.Level}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading category with slug: {Slug}", slug);
                return StatusCode(500, "Internal server error");
            }
        }

        private async Task<List<object>> BuildBreadcrumbsAsync(Category category)
        {
            var breadcrumbs = new List<object>();
            var current = category;

            // Build breadcrumb trail from current to root
            while (current != null)
            {
                breadcrumbs.Insert(0, new { 
                    Name = current.Name, 
                    Url = $"/Product/Category/{current.Slug}",
                    Slug = current.Slug
                });

                if (current.ParentCategoryId.HasValue)
                {
                    current = await _categoryService.GetCategoryByIdAsync(current.ParentCategoryId.Value);
                }
                else
                {
                    current = null;
                }
            }

            return breadcrumbs;
        }

        private async Task<IActionResult> HandleLevel1Category(Category category, string[]? brand, string priceRange, string rating, string onSale, string freeShipping, string sort, List<object> breadcrumbs)
        {
            // Get level 2 categories (direct subcategories) from database
            var subCategories = await _categoryService.GetDirectSubCategoriesAsync(category.Id);
            
            // Get products using ProductService instead of mock methods
            var products = await _productService.GetProductsByAnimalTypeAsync(category.AnimalType ?? "dog");
            
            // Apply filters
            products = ApplyFilters(products, brand ?? new string[0], priceRange, rating, onSale, freeShipping);
            products = ApplySorting(products, sort);

            ViewBag.CurrentCategory = category;
            ViewBag.SubCategories = subCategories;
            ViewBag.Products = products;
            ViewBag.Title = category.Name;
            ViewBag.AnimalType = category.AnimalType;
            ViewBag.AnimalPageUrl = $"/Product/Category/{category.Slug}";
            ViewBag.AnimalPageName = category.Name;
            ViewBag.ShowAllCategories = true;
            ViewBag.Breadcrumbs = breadcrumbs;
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(products);
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, FreeShipping = freeShipping, Sort = sort };

            return View("Index");
        }

        private async Task<IActionResult> HandleLevel2Category(Category category, string[]? brand, string priceRange, string rating, string onSale, string freeShipping, string sort, List<object> breadcrumbs)
        {
            // Get level 3 categories (direct subcategories)
            var subCategories = await _categoryService.GetDirectSubCategoriesAsync(category.Id);
            
            // Get products for this category and all its sub-categories using new method
            var products = await _productService.GetProductsByParentCategoryAsync(category.Id, category.AnimalType ?? "dog");
            
            // Apply filters
            products = ApplyFilters(products, brand ?? new string[0], priceRange, rating, onSale, freeShipping);
            products = ApplySorting(products, sort);

            ViewBag.CurrentCategory = category;
            ViewBag.SubCategories = subCategories;
            ViewBag.Products = products;
            ViewBag.Title = category.Name;
            ViewBag.AnimalType = category.AnimalType;
            ViewBag.CategoryGroup = "level2";
            ViewBag.AnimalPageUrl = breadcrumbs.Count > 0 ? $"/Product/Category/{breadcrumbs[0].GetType().GetProperty("Slug")?.GetValue(breadcrumbs[0])}" : "/";
            ViewBag.AnimalPageName = breadcrumbs.Count > 0 ? breadcrumbs[0].GetType().GetProperty("Name")?.GetValue(breadcrumbs[0]) : category.Name;
            ViewBag.ShowAllCategories = false;
            ViewBag.CategoryGroupName = category.Name;
            ViewBag.Breadcrumbs = breadcrumbs;
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(products);
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, FreeShipping = freeShipping, Sort = sort };

            return View("Index");
        }

        private async Task<IActionResult> HandleLevel3Category(Category category, string[]? brand, string priceRange, string rating, string onSale, string freeShipping, string sort, List<object> breadcrumbs)
        {
            // Get products for this specific category using ProductService
            var products = await _productService.GetProductsByCategoryAsync(category.Id);
            
            // Apply filters
            products = ApplyFilters(products, brand ?? new string[0], priceRange, rating, onSale, freeShipping);
            products = ApplySorting(products, sort);

            // Get parent category info for breadcrumb
            var parentCategory = breadcrumbs.Count > 1 ? breadcrumbs[breadcrumbs.Count - 2] : breadcrumbs[0];

            ViewBag.CurrentCategory = category;
            ViewBag.Products = products;
            ViewBag.CategoryName = category.Name;
            ViewBag.Category = category.Slug;
            ViewBag.AnimalType = category.AnimalType;
            ViewBag.Title = $"{category.Name} cho {(category.AnimalType == "dog" ? "chó" : "mèo")}";
            ViewBag.ParentCategoryName = parentCategory.GetType().GetProperty("Name")?.GetValue(parentCategory);
            ViewBag.ParentCategoryUrl = parentCategory.GetType().GetProperty("Url")?.GetValue(parentCategory);
            ViewBag.AnimalPageUrl = breadcrumbs.Count > 0 ? $"/Product/Category/{breadcrumbs[0].GetType().GetProperty("Slug")?.GetValue(breadcrumbs[0])}" : "/";
            ViewBag.AnimalPageName = breadcrumbs.Count > 0 ? breadcrumbs[0].GetType().GetProperty("Name")?.GetValue(breadcrumbs[0]) : category.Name;
            ViewBag.Breadcrumbs = breadcrumbs;
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(products);
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, FreeShipping = freeShipping, Sort = sort };

            return View("Index");
        }

        // Helper method to map ProductVariants by type
        private List<ProductVariantDto> MapVariantsByType(ICollection<ProductVariant> variants, string type, decimal basePrice)
        {
            if (variants == null || !variants.Any())
                return new List<ProductVariantDto>();

            return variants
                .Where(v => v.Type?.ToLower() == type.ToLower() && v.IsActive)
                .OrderBy(v => v.DisplayOrder)
                .Select(v => new ProductVariantDto
                {
                    Id = v.Id,
                    Name = v.Name,
                    Type = v.Type,
                    PriceAdjustment = v.PriceAdjustment,
                    Price = basePrice + (v.PriceAdjustment ?? 0),
                    OriginalPrice = basePrice,
                    IsDefault = v.IsDefault,
                    IsSelected = v.IsDefault,
                    DisplayOrder = v.DisplayOrder
                })
                .ToList();
        }

        [HttpGet]
        [Route("api/product/{id}/reviews")]
        public async Task<IActionResult> LoadReviews(int id, int page = 1, int pageSize = 10, string sortBy = "newest")
        {
            try
            {
                var reviews = await _reviewService.GetProductReviewsAsync(id, page, pageSize, sortBy);
                var reviewStats = await _reviewService.GetProductReviewStatisticsAsync(id);
                
                return Json(new
                {
                    Reviews = reviews.Reviews,
                    TotalPages = reviews.TotalPages,
                    CurrentPage = reviews.CurrentPage,
                    Statistics = reviewStats,
                    TotalReviews = reviews.TotalReviews
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading reviews for product {ProductId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("api/product/{id}/reviews")]
        public async Task<IActionResult> SubmitReview(int id, CreateReviewViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string? userId = null;
                if (User.Identity.IsAuthenticated)
                {
                    userId = _userManager.GetUserId(User);
                }

                model.ProductId = id;
                var success = await _reviewService.CreateReviewAsync(model, userId);

                return Json(new { success = success, message = success ? "Đánh giá đã được gửi thành công!" : "Có lỗi xảy ra" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting review for product {ProductId}", id);
                return Json(new { success = false, message = "Có lỗi xảy ra khi gửi đánh giá" });
            }
        }

    }
}