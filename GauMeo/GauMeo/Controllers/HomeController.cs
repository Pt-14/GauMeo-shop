using Microsoft.AspNetCore.Mvc;
using GauMeo.Services;
using GauMeo.Models.Categories;

namespace GauMeo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ICategoryService categoryService, IProductService productService, IBrandService brandService, ILogger<HomeController> logger)
        {
            _categoryService = categoryService;
            _productService = productService;
            _brandService = brandService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var dogCategories = await _categoryService.GetParentCategoriesAsync("dog");
            var catCategories = await _categoryService.GetParentCategoriesAsync("cat");
            var bestSellingProducts = await _productService.GetBestSellingProductsAsync(8);
            var promotionProducts = await _productService.GetPromotionProductsAsync(8);
            var featuredBrands = await _brandService.GetFeaturedBrandsAsync();

            ViewBag.DogCategories = dogCategories;
            ViewBag.CatCategories = catCategories;
            ViewBag.BestSellingProducts = bestSellingProducts;
            ViewBag.PromotionProducts = promotionProducts;
            ViewBag.FeaturedBrands = featuredBrands.Take(6);

            return View();
        }
    }
} 