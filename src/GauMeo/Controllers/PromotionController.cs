using Microsoft.AspNetCore.Mvc;
using GauMeo.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GauMeo.Controllers
{
    public class PromotionController : Controller
    {
        private readonly IProductService _productService;

        public PromotionController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(string brand = "", string priceRange = "", string rating = "", string sort = "popular")
        {
            // Lấy tất cả sản phẩm khuyến mãi từ database
            var products = await _productService.GetPromotionProductsAsync(int.MaxValue);
            
            // Apply filters
            if (!string.IsNullOrEmpty(brand))
            {
                products = products.Where(p => ((dynamic)p).Brand.ToString().ToLower().Contains(brand.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(priceRange))
            {
                var range = priceRange.Split('-');
                if (range.Length == 2 && decimal.TryParse(range[0], out var min) && decimal.TryParse(range[1], out var max))
                {
                    products = products.Where(p => ((dynamic)p).CurrentPrice >= min && ((dynamic)p).CurrentPrice <= max).ToList();
                }
                else if (priceRange.EndsWith("-") && decimal.TryParse(range[0], out var minOnly))
                {
                    products = products.Where(p => ((dynamic)p).CurrentPrice >= minOnly).ToList();
                }
            }

            if (!string.IsNullOrEmpty(rating) && int.TryParse(rating, out var ratingValue))
            {
                products = products.Where(p => ((dynamic)p).Rating >= ratingValue).ToList();
            }

            // Apply sorting
            products = sort switch
            {
                "price-asc" => products.OrderBy(p => ((dynamic)p).CurrentPrice).ToList(),
                "price-desc" => products.OrderByDescending(p => ((dynamic)p).CurrentPrice).ToList(),
                "name-asc" => products.OrderBy(p => ((dynamic)p).Name).ToList(),
                "name-desc" => products.OrderByDescending(p => ((dynamic)p).Name).ToList(),
                "rating-desc" => products.OrderByDescending(p => ((dynamic)p).Rating).ToList(),
                "discount-desc" => products.OrderByDescending(p => ((dynamic)p).DiscountPercent).ToList(),
                _ => products.OrderByDescending(p => ((dynamic)p).PopularityScore).ToList() // "popular" is default
            };

            ViewBag.Products = products;
            ViewBag.Title = "Sản phẩm khuyến mãi";
            ViewBag.Brands = GetBrandsFromProducts(products);
            
            return View();
        }        private List<string> GetBrandsFromProducts(List<dynamic> products)
        {
            var brands = new List<string>();
            foreach (var product in products)
            {
                var brandName = ((dynamic)product).Brand?.ToString();
                if (!string.IsNullOrEmpty(brandName) && !brands.Contains(brandName))
                {
                    brands.Add(brandName);
                }
            }
            return brands.OrderBy(b => b).ToList();
        }
    }
}