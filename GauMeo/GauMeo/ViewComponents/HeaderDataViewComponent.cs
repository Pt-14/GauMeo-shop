using Microsoft.AspNetCore.Mvc;
using GauMeo.Services;
using GauMeo.Models.Categories;
using GauMeo.Models.Services;

namespace GauMeo.ViewComponents
{
    public class HeaderDataViewComponent : ViewComponent
    {
        private readonly IBrandService _brandService;
        private readonly IServiceService _serviceService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<HeaderDataViewComponent> _logger;

        public HeaderDataViewComponent(
            IBrandService brandService, 
            IServiceService serviceService,
            ICategoryService categoryService,
            ILogger<HeaderDataViewComponent> logger)
        {
            _brandService = brandService;
            _serviceService = serviceService;
            _categoryService = categoryService;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                // Load data sequentially to avoid DbContext threading issues
                var activeBrands = await _brandService.GetActiveBrandsAsync();
                var activeServices = await _serviceService.GetAllActiveServicesAsync();
                var dogCategories = await _categoryService.GetCategoriesHierarchyAsync("dog");
                var catCategories = await _categoryService.GetCategoriesHierarchyAsync("cat");
                var dogMainCategory = await _categoryService.GetMainCategoryAsync("dog");
                var catMainCategory = await _categoryService.GetMainCategoryAsync("cat");

                var headerData = new HeaderDataViewModel
                {
                    ActiveBrands = activeBrands,
                    ActiveServices = activeServices,
                    DogCategories = dogCategories,
                    CatCategories = catCategories,
                    DogMainCategory = dogMainCategory,
                    CatMainCategory = catMainCategory
                };

                return View(headerData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading header data");
                
                // Return empty data in case of error
                var emptyData = new HeaderDataViewModel
                {
                    ActiveBrands = new List<Brand>(),
                    ActiveServices = new List<Service>(),
                    DogCategories = new List<Category>(),
                    CatCategories = new List<Category>(),
                    DogMainCategory = null,
                    CatMainCategory = null
                };
                
                return View(emptyData);
            }
        }
    }

    public class HeaderDataViewModel
    {
        public IEnumerable<Brand> ActiveBrands { get; set; } = new List<Brand>();
        public IEnumerable<Service> ActiveServices { get; set; } = new List<Service>();
        public IEnumerable<Category> DogCategories { get; set; } = new List<Category>();
        public IEnumerable<Category> CatCategories { get; set; } = new List<Category>();
        public Category? DogMainCategory { get; set; }
        public Category? CatMainCategory { get; set; }
    }
} 