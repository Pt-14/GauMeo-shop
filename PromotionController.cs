using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GauMeo.Controllers
{
    public class PromotionController : Controller
    {
        public IActionResult Index(string brand = "", string priceRange = "", string rating = "", string sort = "popular")
        {
            var products = GetPromotionProducts();
            
            // Apply filters
            if (!string.IsNullOrEmpty(brand))
            {
                products = products.Where(p => p.Brand.ToLower() == brand.ToLower()).ToList();
            }

            if (!string.IsNullOrEmpty(priceRange))
            {
                var range = priceRange.Split('-');
                if (range.Length == 2)
                {
                    var min = decimal.Parse(range[0]);
                    var max = decimal.Parse(range[1]);
                    products = products.Where(p => p.Price >= min && p.Price <= max).ToList();
                }
            }

            if (!string.IsNullOrEmpty(rating))
            {
                var ratingValue = int.Parse(rating);
                products = products.Where(p => p.Rating >= ratingValue).ToList();
            }

            // Apply sorting
            products = sort switch
            {
                "price-asc" => products.OrderBy(p => p.Price).ToList(),
                "price-desc" => products.OrderByDescending(p => p.Price).ToList(),
                "name-asc" => products.OrderBy(p => p.Name).ToList(),
                "name-desc" => products.OrderByDescending(p => p.Name).ToList(),
                "rating-desc" => products.OrderByDescending(p => p.Rating).ToList(),
                _ => products.OrderByDescending(p => p.PopularityScore).ToList() // "popular" is default
            };

            ViewBag.Products = products;
            ViewBag.Title = "Sản phẩm khuyến mãi";
            ViewBag.Brands = GetBrands();
            
            return View();
        }

        private List<dynamic> GetPromotionProducts()
        {
            // Sample promotion products data
            return new List<dynamic>
            {
                new { Id = 1, Name = "Royal Canin Mini Adult", Brand = "Royal Canin", Price = 300000M, Rating = 5, Image = "/images/products/royal-canin-mini-adult.png", PopularityScore = 95, DiscountPercent = 15, IsOnSale = true },
                new { Id = 2, Name = "Pedigree Adult Complete Nutrition", Brand = "Pedigree", Price = 400000M, Rating = 4, Image = "/images/products/pedigree-adult.png", PopularityScore = 88, DiscountPercent = 15, IsOnSale = true },
                new { Id = 3, Name = "Whiskas Adult Ocean Fish", Brand = "Whiskas", Price = 150000M, Rating = 5, Image = "/images/products/whiskas-adult.png", PopularityScore = 92, DiscountPercent = 15, IsOnSale = true },
                new { Id = 4, Name = "Hill's Science Diet Adult", Brand = "Hill's", Price = 700000M, Rating = 5, Image = "/images/products/hills-adult.png", PopularityScore = 85, DiscountPercent = 15, IsOnSale = true },
                new { Id = 5, Name = "Nutri Source Adult Chicken", Brand = "Nutri-Source", Price = 550000M, Rating = 4, Image = "/images/products/nutrisource-adult.png", PopularityScore = 78, DiscountPercent = 15, IsOnSale = true },
                new { Id = 6, Name = "Acana Wild Coast", Brand = "Acana", Price = 1050000M, Rating = 5, Image = "/images/products/acana-wild-coast.png", PopularityScore = 90, DiscountPercent = 15, IsOnSale = true },
                new { Id = 7, Name = "Orijen Original", Brand = "Orijen", Price = 1200000M, Rating = 5, Image = "/images/products/orijen-original.png", PopularityScore = 87, DiscountPercent = 15, IsOnSale = true },
                new { Id = 8, Name = "Natural Core Eco7", Brand = "Natural Core", Price = 650000M, Rating = 4, Image = "/images/products/natural-core-eco7.png", PopularityScore = 82, DiscountPercent = 15, IsOnSale = true }
            };
        }

        private List<string> GetBrands()
        {
            return new List<string>
            {
                "Royal Canin",
                "Pedigree",
                "Whiskas",
                "Hill's",
                "Nutri-Source",
                "Acana",
                "Orijen",
                "Natural Core"
            };
        }
    }
} 