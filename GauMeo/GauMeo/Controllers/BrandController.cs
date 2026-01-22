using Microsoft.AspNetCore.Mvc;
using GauMeo.Services;

namespace GauMeo.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IProductService _productService;
        private readonly ILogger<BrandController> _logger;

        public BrandController(IBrandService brandService, IProductService productService, ILogger<BrandController> logger)
        {
            _brandService = brandService;
            _productService = productService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // Khởi tạo ViewBag.Brands ngay từ đầu để tránh null
            ViewBag.Brands = new List<dynamic>();
            ViewData["Title"] = "Thương hiệu";

            try
            {
                _logger.LogInformation("Starting to load brands from database");
                
                // Kiểm tra service injection
                if (_brandService == null)
                {
                    _logger.LogError("BrandService is null - dependency injection failed");
                    TempData["ErrorMessage"] = "Lỗi hệ thống: Service không khả dụng.";
                    return View();
                }

                // Lấy dữ liệu từ database - chỉ active brands
                var brands = await _brandService.GetActiveBrandsAsync();
                _logger.LogInformation($"Retrieved {brands?.Count() ?? 0} brands from database");
                
                if (brands != null && brands.Any())
                {
                    // Chuyển đổi sang format mà view đang expect
                    var brandsData = brands.Select(b => new 
                    { 
                        Id = b.Id, 
                        Name = b.Name ?? "Unknown Brand", 
                        ShortName = b.ShortName ?? "UB",
                        Description = b.Description ?? "Mô tả đang cập nhật",
                        FullDescription = b.FullDescription ?? "Mô tả chi tiết đang cập nhật",
                        Features = b.GetFeaturesList() ?? new List<string>(),
                        Founded = b.Founded ?? "N/A",
                        Origin = b.Origin ?? "N/A",
                        Website = b.Website ?? "#",
                        Image = b.Image ?? "/images/brands/default.png"
                    }).ToList();

                    ViewBag.Brands = brandsData;
                    _logger.LogInformation($"Successfully converted {brandsData.Count} brands for view");
                }
                else
                {
                    _logger.LogWarning("No active brands found in database");
                    TempData["InfoMessage"] = "Hiện tại chưa có thương hiệu nào được kích hoạt.";
                }
                
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading brands index page: {ErrorMessage}", ex.Message);
                
                // Đảm bảo ViewBag.Brands không null và hiển thị thông báo lỗi
                ViewBag.Brands = new List<dynamic>();
                TempData["ErrorMessage"] = $"Có lỗi xảy ra khi tải danh sách thương hiệu: {ex.Message}";
                
                return View();
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            ViewData["Title"] = "Chi tiết thương hiệu";
            
            try
            {
                if (_brandService == null || _productService == null)
                {
                    _logger.LogError("Services are null in Detail action");
                    return NotFound();
                }

                var brand = await _brandService.GetBrandByIdAsync(id);
                
                if (brand == null)
                {
                    _logger.LogWarning("Brand with id {BrandId} not found", id);
                    return NotFound();
                }

                // Chuyển đổi sang format mà view expect
                var brandDetail = new 
                { 
                    Id = brand.Id,
                    Name = brand.Name ?? "Unknown Brand",
                    ShortName = brand.ShortName ?? "UB",
                    Description = brand.Description ?? "Mô tả đang cập nhật",
                    FullDescription = brand.FullDescription ?? "Mô tả chi tiết đang cập nhật",
                    Features = brand.GetFeaturesList() ?? new List<string>(),
                    Founded = brand.Founded ?? "N/A",
                    Origin = brand.Origin ?? "N/A",
                    Website = brand.Website ?? "#",
                    Image = brand.Image ?? "/images/brands/default.png",
                    ProductCount = await _brandService.GetBrandProductCountAsync(id)
                };

                ViewBag.Brand = brandDetail;
                ViewData["Title"] = $"Thương hiệu {brand.Name}";

                // Lấy dữ liệu sản phẩm, danh mục và khoảng giá cho brand detail
                _logger.LogInformation("Loading products, categories, and price ranges for brand {BrandId}", id);
                
                var products = await _productService.GetProductsByBrandAsync(id);
                var categories = await _productService.GetCategoriesByBrandAsync(id);
                var priceRanges = await _productService.GetPriceRangesByBrandAsync(id);

                ViewBag.Products = products ?? new List<dynamic>();
                ViewBag.Categories = categories ?? new List<dynamic>();
                ViewBag.PriceRanges = priceRanges ?? new List<dynamic>();
                
                _logger.LogInformation("Successfully loaded {ProductCount} products, {CategoryCount} categories, and {PriceRangeCount} price ranges for brand {BrandId}", 
                    products?.Count ?? 0, categories?.Count ?? 0, priceRanges?.Count ?? 0, id);
                
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading brand detail for id {BrandId}: {ErrorMessage}", id, ex.Message);
                return NotFound();
            }
        }
    }
} 