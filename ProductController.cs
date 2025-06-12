using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GauMeo.Controllers
{
    public class ProductController : Controller
    {
        // Trang tổng quan sản phẩm cho chó
        public IActionResult Dog(string[] brand = null, string priceRange = "", string rating = "", string onSale = "", string freeShipping = "", string sort = "popular")
        {
            var dogProducts = GetDogProducts();
            var parentCategories = GetParentCategories("dog");
            
            // Apply filters
            dogProducts = ApplyFilters(dogProducts, brand, priceRange, rating, onSale, freeShipping);
            dogProducts = ApplySorting(dogProducts, sort);
            
            ViewBag.Products = dogProducts;
            ViewBag.Categories = parentCategories;
            ViewBag.Title = "Sản phẩm cho chó";
            ViewBag.AnimalType = "dog";
            ViewBag.AnimalPageUrl = "/Product/Dog";
            ViewBag.AnimalPageName = "Sản phẩm cho chó";
            ViewBag.ShowAllCategories = true;
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetDogProducts());
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, FreeShipping = freeShipping, Sort = sort };
            
            // Return partial view for AJAX requests
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductGrid", dogProducts);
            }
            
            return View("Index");
        }

        // Trang tổng quan sản phẩm cho mèo
        public IActionResult Cat(string[] brand = null, string priceRange = "", string rating = "", string onSale = "", string freeShipping = "", string sort = "popular")
        {
            var catProducts = GetCatProducts();
            var parentCategories = GetParentCategories("cat");
            
            // Apply filters
            catProducts = ApplyFilters(catProducts, brand, priceRange, rating, onSale, freeShipping);
            catProducts = ApplySorting(catProducts, sort);
            
            ViewBag.Products = catProducts;
            ViewBag.Categories = parentCategories;
            ViewBag.Title = "Sản phẩm cho mèo";
            ViewBag.AnimalType = "cat";
            ViewBag.AnimalPageUrl = "/Product/Cat";
            ViewBag.AnimalPageName = "Sản phẩm cho mèo";
            ViewBag.ShowAllCategories = true;
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetCatProducts());
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, FreeShipping = freeShipping, Sort = sort };
            
            // Return partial view for AJAX requests
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductGrid", catProducts);
            }
            
            return View("Index");
        }

        // Trang sản phẩm theo danh mục cho chó
        public IActionResult DogCategory(string category, string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var products = GetDogProductsByCategory(category);
            var categoryName = GetCategoryDisplayName(category, "dog");
            var parentInfo = GetParentCategoryInfo(category, "dog");
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.CategoryName = categoryName;
            ViewBag.Category = category;
            ViewBag.AnimalType = "dog";
            ViewBag.Title = $"{categoryName} cho chó";
            ViewBag.ParentCategoryName = parentInfo.Name;
            ViewBag.ParentCategoryUrl = parentInfo.Url;
            ViewBag.AnimalPageUrl = "/Product/Dog";
            ViewBag.AnimalPageName = "Sản phẩm cho chó";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetDogProductsByCategory(category));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Category");
        }

        // Trang sản phẩm theo danh mục cho mèo
        public IActionResult CatCategory(string category, string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var products = GetCatProductsByCategory(category);
            var categoryName = GetCategoryDisplayName(category, "cat");
            var parentInfo = GetParentCategoryInfo(category, "cat");
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.CategoryName = categoryName;
            ViewBag.Category = category;
            ViewBag.AnimalType = "cat";
            ViewBag.Title = $"{categoryName} cho mèo";
            ViewBag.ParentCategoryName = parentInfo.Name;
            ViewBag.ParentCategoryUrl = parentInfo.Url;
            ViewBag.AnimalPageUrl = "/Product/Cat";
            ViewBag.AnimalPageName = "Sản phẩm cho mèo";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetCatProductsByCategory(category));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Category");
        }

        // Trang thức ăn cho chó (tất cả loại thức ăn)
        public IActionResult DogFood(string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var foodCategories = new[] { "thuc-an-hat", "thuc-an-uot", "thuc-an-kho", "thuc-an-huu-co" };
            var products = GetDogProductsByCategories(foodCategories);
            var categories = GetDogCategories().Where(c => foodCategories.Contains((string)c.Id)).ToList();
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Title = "Thức ăn cho chó";
            ViewBag.AnimalType = "dog";
            ViewBag.CategoryGroup = "food";
            ViewBag.AnimalPageUrl = "/Product/Dog";
            ViewBag.AnimalPageName = "Sản phẩm cho chó";
            ViewBag.ShowAllCategories = false;
            ViewBag.CategoryGroupName = "Thức ăn cho chó";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetDogProductsByCategories(foodCategories));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Index");
        }

        // Trang thức ăn cho mèo (tất cả loại thức ăn)
        public IActionResult CatFood(string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var foodCategories = new[] { "thuc-an-hat", "thuc-an-uot", "thuc-an-kho", "thuc-an-huu-co" };
            var products = GetCatProductsByCategories(foodCategories);
            var categories = GetCatCategories().Where(c => foodCategories.Contains((string)c.Id)).ToList();
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Title = "Thức ăn cho mèo";
            ViewBag.AnimalType = "cat";
            ViewBag.CategoryGroup = "food";
            ViewBag.AnimalPageUrl = "/Product/Cat";
            ViewBag.AnimalPageName = "Sản phẩm cho mèo";
            ViewBag.ShowAllCategories = false;
            ViewBag.CategoryGroupName = "Thức ăn cho mèo";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetCatProductsByCategories(foodCategories));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Index");
        }

        // Trang Pate - Bánh thưởng cho chó
        public IActionResult DogTreats(string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var treatCategories = new[] { "pate", "thit-say-kho", "sup-thuong", "banh-quy" };
            var products = GetDogProductsByCategories(treatCategories);
            var categories = GetDogCategories().Where(c => treatCategories.Contains((string)c.Id)).ToList();
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Title = "Pate - Bánh thưởng cho chó";
            ViewBag.AnimalType = "dog";
            ViewBag.CategoryGroup = "treats";
            ViewBag.AnimalPageUrl = "/Product/Dog";
            ViewBag.AnimalPageName = "Sản phẩm cho chó";
            ViewBag.ShowAllCategories = false;
            ViewBag.CategoryGroupName = "Pate - Bánh thưởng";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetDogProductsByCategories(treatCategories));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Index");
        }

        // Trang Pate - Bánh thưởng cho mèo
        public IActionResult CatTreats(string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var treatCategories = new[] { "pate", "thit-say-kho", "sup-thuong", "banh-quy" };
            var products = GetCatProductsByCategories(treatCategories);
            var categories = GetCatCategories().Where(c => treatCategories.Contains((string)c.Id)).ToList();
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Title = "Pate - Bánh thưởng cho mèo";
            ViewBag.AnimalType = "cat";
            ViewBag.CategoryGroup = "treats";
            ViewBag.AnimalPageUrl = "/Product/Cat";
            ViewBag.AnimalPageName = "Sản phẩm cho mèo";
            ViewBag.ShowAllCategories = false;
            ViewBag.CategoryGroupName = "Pate - Bánh thưởng";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetCatProductsByCategories(treatCategories));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Index");
        }

        // Trang chăm sóc sức khỏe cho chó
        public IActionResult DogHealth(string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var healthCategories = new[] { "vitamin", "thuoc-nho-gay", "ho-tro-tieu-hoa" };
            var products = GetDogProductsByCategories(healthCategories);
            var categories = GetDogCategories().Where(c => healthCategories.Contains((string)c.Id)).ToList();
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Title = "Chăm sóc sức khỏe cho chó";
            ViewBag.AnimalType = "dog";
            ViewBag.CategoryGroup = "health";
            ViewBag.AnimalPageUrl = "/Product/Dog";
            ViewBag.AnimalPageName = "Sản phẩm cho chó";
            ViewBag.ShowAllCategories = false;
            ViewBag.CategoryGroupName = "Chăm sóc sức khỏe";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetDogProductsByCategories(healthCategories));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Index");
        }

        // Trang chăm sóc sức khỏe cho mèo
        public IActionResult CatHealth(string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var healthCategories = new[] { "vitamin", "thuoc-nho-gay", "ho-tro-tieu-hoa" };
            var products = GetCatProductsByCategories(healthCategories);
            var categories = GetCatCategories().Where(c => healthCategories.Contains((string)c.Id)).ToList();
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Title = "Chăm sóc sức khỏe cho mèo";
            ViewBag.AnimalType = "cat";
            ViewBag.CategoryGroup = "health";
            ViewBag.AnimalPageUrl = "/Product/Cat";
            ViewBag.AnimalPageName = "Sản phẩm cho mèo";
            ViewBag.ShowAllCategories = false;
            ViewBag.CategoryGroupName = "Chăm sóc sức khỏe";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetCatProductsByCategories(healthCategories));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Index");
        }

        // Trang chăm sóc vệ sinh cho chó
        public IActionResult DogGrooming(string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var groomingCategories = new[] { "sua-tam", "ban-chai", "khan-lau" };
            var products = GetDogProductsByCategories(groomingCategories);
            var categories = GetDogCategories().Where(c => groomingCategories.Contains((string)c.Id)).ToList();
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Title = "Chăm sóc vệ sinh cho chó";
            ViewBag.AnimalType = "dog";
            ViewBag.CategoryGroup = "grooming";
            ViewBag.AnimalPageUrl = "/Product/Dog";
            ViewBag.AnimalPageName = "Sản phẩm cho chó";
            ViewBag.ShowAllCategories = false;
            ViewBag.CategoryGroupName = "Chăm sóc vệ sinh";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetDogProductsByCategories(groomingCategories));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Index");
        }

        // Trang chăm sóc vệ sinh cho mèo
        public IActionResult CatGrooming(string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var groomingCategories = new[] { "sua-tam", "ban-chai", "khan-lau" };
            var products = GetCatProductsByCategories(groomingCategories);
            var categories = GetCatCategories().Where(c => groomingCategories.Contains((string)c.Id)).ToList();
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Title = "Chăm sóc vệ sinh cho mèo";
            ViewBag.AnimalType = "cat";
            ViewBag.CategoryGroup = "grooming";
            ViewBag.AnimalPageUrl = "/Product/Cat";
            ViewBag.AnimalPageName = "Sản phẩm cho mèo";
            ViewBag.ShowAllCategories = false;
            ViewBag.CategoryGroupName = "Chăm sóc vệ sinh";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetCatProductsByCategories(groomingCategories));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Index");
        }

        // Trang đồ chơi cho chó
        public IActionResult DogToys(string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var toyCategories = new[] { "bong", "xuong-gam", "do-choi-gam" };
            var products = GetDogProductsByCategories(toyCategories);
            var categories = GetDogCategories().Where(c => toyCategories.Contains((string)c.Id)).ToList();
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Title = "Đồ chơi cho chó";
            ViewBag.AnimalType = "dog";
            ViewBag.CategoryGroup = "toys";
            ViewBag.AnimalPageUrl = "/Product/Dog";
            ViewBag.AnimalPageName = "Sản phẩm cho chó";
            ViewBag.ShowAllCategories = false;
            ViewBag.CategoryGroupName = "Đồ chơi";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetDogProductsByCategories(toyCategories));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Index");
        }

        // Trang đồ chơi cho mèo
        public IActionResult CatToys(string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var toyCategories = new[] { "bong", "xuong-gam", "do-choi-gam" };
            var products = GetCatProductsByCategories(toyCategories);
            var categories = GetCatCategories().Where(c => toyCategories.Contains((string)c.Id)).ToList();
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Title = "Đồ chơi cho mèo";
            ViewBag.AnimalType = "cat";
            ViewBag.CategoryGroup = "toys";
            ViewBag.AnimalPageUrl = "/Product/Cat";
            ViewBag.AnimalPageName = "Sản phẩm cho mèo";
            ViewBag.ShowAllCategories = false;
            ViewBag.CategoryGroupName = "Đồ chơi";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetCatProductsByCategories(toyCategories));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Index");
        }

        // Trang phụ kiện khác cho chó
        public IActionResult DogAccessories(string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var accessoryCategories = new[] { "vong-co", "day-dat", "long-van-chuyen" };
            var products = GetDogProductsByCategories(accessoryCategories);
            var categories = GetDogCategories().Where(c => accessoryCategories.Contains((string)c.Id)).ToList();
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Title = "Phụ kiện khác cho chó";
            ViewBag.AnimalType = "dog";
            ViewBag.CategoryGroup = "accessories";
            ViewBag.AnimalPageUrl = "/Product/Dog";
            ViewBag.AnimalPageName = "Sản phẩm cho chó";
            ViewBag.ShowAllCategories = false;
            ViewBag.CategoryGroupName = "Phụ kiện khác";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetDogProductsByCategories(accessoryCategories));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Index");
        }

        // Trang phụ kiện khác cho mèo
        public IActionResult CatAccessories(string brand = "", string priceRange = "", string rating = "", string onSale = "", string sort = "popular")
        {
            var accessoryCategories = new[] { "vong-co", "day-dat", "long-van-chuyen" };
            var products = GetCatProductsByCategories(accessoryCategories);
            var categories = GetCatCategories().Where(c => accessoryCategories.Contains((string)c.Id)).ToList();
            
            // Apply filters
            products = ApplyFilters(products, brand, priceRange, rating, onSale);
            products = ApplySorting(products, sort);
            
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Title = "Phụ kiện khác cho mèo";
            ViewBag.AnimalType = "cat";
            ViewBag.CategoryGroup = "accessories";
            ViewBag.AnimalPageUrl = "/Product/Cat";
            ViewBag.AnimalPageName = "Sản phẩm cho mèo";
            ViewBag.ShowAllCategories = false;
            ViewBag.CategoryGroupName = "Phụ kiện khác";
            
            // Filter data for UI
            ViewBag.Brands = GetBrands(GetCatProductsByCategories(accessoryCategories));
            ViewBag.CurrentFilters = new { Brand = brand, PriceRange = priceRange, Rating = rating, OnSale = onSale, Sort = sort };
            
            return View("Index");
        }

        public IActionResult Detail()
        {
            return View();
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

        // Overload for backward compatibility
        private List<dynamic> ApplyFilters(List<dynamic> products, string brand, string priceRange, string rating, string onSale)
        {
            var brandArray = string.IsNullOrEmpty(brand) ? null : new string[] { brand };
            return ApplyFilters(products, brandArray, priceRange, rating, onSale, "");
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

        // Demo data methods
        private List<dynamic> GetDogProducts()
        {
            return new List<dynamic>
            {
                // Thức ăn hạt
                new { Id = 1, Name = "Royal Canin Golden Retriever", Price = 850000, Image = "/images/products/dog1.jpg", Category = "thuc-an-hat", Brand = "Royal Canin", IsOnSale = true, DiscountPercent = 15, Rating = 4.8 },
                new { Id = 2, Name = "Pedigree Adult Complete", Price = 320000, Image = "/images/products/dog2.jpg", Category = "thuc-an-hat", Brand = "Pedigree", IsOnSale = false, DiscountPercent = 0, Rating = 4.5 },
                new { Id = 3, Name = "Hill's Prescription Diet", Price = 1200000, Image = "/images/products/dog3.jpg", Category = "thuc-an-hat", Brand = "Hill's", IsOnSale = true, DiscountPercent = 10, Rating = 4.9 },
                new { Id = 24, Name = "Acana Wild Prairie", Price = 950000, Image = "/images/products/dog24.jpg", Category = "thuc-an-hat", Brand = "Acana", IsOnSale = false, DiscountPercent = 0, Rating = 4.7 },
                new { Id = 25, Name = "Taste of the Wild", Price = 780000, Image = "/images/products/dog25.jpg", Category = "thuc-an-hat", Brand = "Taste of the Wild", IsOnSale = true, DiscountPercent = 20, Rating = 4.6 },
                
                // Thức ăn ướt
                new { Id = 5, Name = "Royal Canin Wet Food", Price = 45000, Image = "/images/products/dog5.jpg", Category = "thuc-an-uot", Brand = "Royal Canin", IsOnSale = true, DiscountPercent = 20, Rating = 4.7 },
                new { Id = 26, Name = "Hill's i/d Digestive Care", Price = 65000, Image = "/images/products/dog26.jpg", Category = "thuc-an-uot", Brand = "Hill's", IsOnSale = false, DiscountPercent = 0, Rating = 4.8 },
                new { Id = 27, Name = "Pedigree Beef & Vegetable", Price = 35000, Image = "/images/products/dog27.jpg", Category = "thuc-an-uot", Brand = "Pedigree", IsOnSale = true, DiscountPercent = 15, Rating = 4.3 },
                
                // Thức ăn khô
                new { Id = 28, Name = "Jerky Treats Beef", Price = 120000, Image = "/images/products/dog28.jpg", Category = "thuc-an-kho", Brand = "Jerky", IsOnSale = false, DiscountPercent = 0, Rating = 4.5 },
                new { Id = 29, Name = "Freeze Dried Chicken", Price = 180000, Image = "/images/products/dog29.jpg", Category = "thuc-an-kho", Brand = "Ziwi Peak", IsOnSale = true, DiscountPercent = 25, Rating = 4.9 },
                
                // Pate
                new { Id = 30, Name = "Pedigree Pate Chicken", Price = 25000, Image = "/images/products/dog30.jpg", Category = "pate", Brand = "Pedigree", IsOnSale = false, DiscountPercent = 0, Rating = 4.2 },
                new { Id = 31, Name = "Royal Canin Digestive Pate", Price = 55000, Image = "/images/products/dog31.jpg", Category = "pate", Brand = "Royal Canin", IsOnSale = true, DiscountPercent = 10, Rating = 4.7 },
                
                // Bánh thưởng
                new { Id = 4, Name = "Pedigree Dentastix", Price = 85000, Image = "/images/products/dog4.jpg", Category = "banh-quy", Brand = "Pedigree", IsOnSale = false, DiscountPercent = 0, Rating = 4.6 },
                new { Id = 32, Name = "Greenies Dental Chews", Price = 150000, Image = "/images/products/dog32.jpg", Category = "banh-quy", Brand = "Greenies", IsOnSale = true, DiscountPercent = 20, Rating = 4.8 },
                new { Id = 33, Name = "Blue Buffalo Wilderness", Price = 95000, Image = "/images/products/dog33.jpg", Category = "banh-quy", Brand = "Blue Buffalo", IsOnSale = false, DiscountPercent = 0, Rating = 4.4 },
                
                // Vitamin & Sức khỏe
                new { Id = 6, Name = "Vitamin Multi cho chó", Price = 180000, Image = "/images/products/dog6.jpg", Category = "vitamin", Brand = "Nutri-Source", IsOnSale = false, DiscountPercent = 0, Rating = 4.4 },
                new { Id = 34, Name = "Omega-3 Fish Oil", Price = 220000, Image = "/images/products/dog34.jpg", Category = "vitamin", Brand = "Nordic Naturals", IsOnSale = true, DiscountPercent = 15, Rating = 4.7 },
                new { Id = 35, Name = "Probiotics for Dogs", Price = 280000, Image = "/images/products/dog35.jpg", Category = "ho-tro-tieu-hoa", Brand = "VetriScience", IsOnSale = false, DiscountPercent = 0, Rating = 4.6 },
                
                // Vệ sinh
                new { Id = 7, Name = "Sữa tắm Bio-Groom", Price = 220000, Image = "/images/products/dog7.jpg", Category = "sua-tam", Brand = "Bio-Groom", IsOnSale = true, DiscountPercent = 12, Rating = 4.8 },
                new { Id = 36, Name = "FURminator deShedding", Price = 350000, Image = "/images/products/dog36.jpg", Category = "ban-chai", Brand = "FURminator", IsOnSale = false, DiscountPercent = 0, Rating = 4.9 },
                new { Id = 37, Name = "Wet Wipes for Dogs", Price = 85000, Image = "/images/products/dog37.jpg", Category = "khan-lau", Brand = "Earthbath", IsOnSale = true, DiscountPercent = 10, Rating = 4.5 },
                
                // Đồ chơi
                new { Id = 8, Name = "Đồ chơi bóng tennis", Price = 65000, Image = "/images/products/dog8.jpg", Category = "bong", Brand = "Kong", IsOnSale = false, DiscountPercent = 0, Rating = 4.5 },
                new { Id = 38, Name = "Kong Classic Red", Price = 120000, Image = "/images/products/dog38.jpg", Category = "xuong-gam", Brand = "Kong", IsOnSale = true, DiscountPercent = 25, Rating = 4.9 },
                new { Id = 39, Name = "Rope Toy Natural", Price = 45000, Image = "/images/products/dog39.jpg", Category = "do-choi-gam", Brand = "Mammoth", IsOnSale = false, DiscountPercent = 0, Rating = 4.3 },
                
                // Phụ kiện
                new { Id = 40, Name = "Leather Collar Brown", Price = 180000, Image = "/images/products/dog40.jpg", Category = "vong-co", Brand = "Leather Bros", IsOnSale = true, DiscountPercent = 30, Rating = 4.6 },
                new { Id = 41, Name = "Retractable Leash 5m", Price = 250000, Image = "/images/products/dog41.jpg", Category = "day-dat", Brand = "Flexi", IsOnSale = false, DiscountPercent = 0, Rating = 4.4 },
                new { Id = 42, Name = "Travel Carrier Medium", Price = 850000, Image = "/images/products/dog42.jpg", Category = "long-van-chuyen", Brand = "Sherpa", IsOnSale = true, DiscountPercent = 15, Rating = 4.7 }
            };
        }

        private List<dynamic> GetCatProducts()
        {
            return new List<dynamic>
            {
                // Thức ăn hạt
                new { Id = 9, Name = "Whiskas Adult Tuna", Price = 280000, Image = "/images/products/cat1.jpg", Category = "thuc-an-hat", Brand = "Whiskas", IsOnSale = true, DiscountPercent = 18, Rating = 4.6 },
                new { Id = 10, Name = "Royal Canin Persian", Price = 950000, Image = "/images/products/cat2.jpg", Category = "thuc-an-hat", Brand = "Royal Canin", IsOnSale = false, DiscountPercent = 0, Rating = 4.9 },
                new { Id = 11, Name = "Purina Pro Plan Salmon", Price = 720000, Image = "/images/products/cat3.jpg", Category = "thuc-an-hat", Brand = "Purina", IsOnSale = true, DiscountPercent = 25, Rating = 4.7 },
                new { Id = 50, Name = "Hill's Science Diet", Price = 850000, Image = "/images/products/cat50.jpg", Category = "thuc-an-hat", Brand = "Hill's", IsOnSale = false, DiscountPercent = 0, Rating = 4.8 },
                new { Id = 51, Name = "Acana Wild Prairie Cat", Price = 980000, Image = "/images/products/cat51.jpg", Category = "thuc-an-hat", Brand = "Acana", IsOnSale = true, DiscountPercent = 15, Rating = 4.6 },
                
                // Thức ăn ướt
                new { Id = 13, Name = "Sheba Perfect Portions", Price = 55000, Image = "/images/products/cat5.jpg", Category = "thuc-an-uot", Brand = "Sheba", IsOnSale = true, DiscountPercent = 15, Rating = 4.8 },
                new { Id = 52, Name = "Royal Canin Instinctive", Price = 42000, Image = "/images/products/cat52.jpg", Category = "thuc-an-uot", Brand = "Royal Canin", IsOnSale = false, DiscountPercent = 0, Rating = 4.7 },
                new { Id = 53, Name = "Hill's i/d Feline", Price = 68000, Image = "/images/products/cat53.jpg", Category = "thuc-an-uot", Brand = "Hill's", IsOnSale = true, DiscountPercent = 20, Rating = 4.9 },
                
                // Thức ăn khô
                new { Id = 54, Name = "Freeze Dried Fish", Price = 220000, Image = "/images/products/cat54.jpg", Category = "thuc-an-kho", Brand = "Ziwi Peak", IsOnSale = false, DiscountPercent = 0, Rating = 4.8 },
                new { Id = 55, Name = "Salmon Jerky Treats", Price = 150000, Image = "/images/products/cat55.jpg", Category = "thuc-an-kho", Brand = "PureBites", IsOnSale = true, DiscountPercent = 25, Rating = 4.5 },
                
                // Pate
                new { Id = 12, Name = "Whiskas Pate Chicken", Price = 38000, Image = "/images/products/cat4.jpg", Category = "pate", Brand = "Whiskas", IsOnSale = false, DiscountPercent = 0, Rating = 4.5 },
                new { Id = 56, Name = "Royal Canin Recovery", Price = 75000, Image = "/images/products/cat56.jpg", Category = "pate", Brand = "Royal Canin", IsOnSale = true, DiscountPercent = 12, Rating = 4.8 },
                new { Id = 57, Name = "Hill's Prescription Pate", Price = 85000, Image = "/images/products/cat57.jpg", Category = "pate", Brand = "Hill's", IsOnSale = false, DiscountPercent = 0, Rating = 4.7 },
                
                // Bánh thưởng
                new { Id = 58, Name = "Temptations Chicken", Price = 65000, Image = "/images/products/cat58.jpg", Category = "banh-quy", Brand = "Temptations", IsOnSale = true, DiscountPercent = 18, Rating = 4.6 },
                new { Id = 59, Name = "Greenies Feline Treats", Price = 120000, Image = "/images/products/cat59.jpg", Category = "banh-quy", Brand = "Greenies", IsOnSale = false, DiscountPercent = 0, Rating = 4.4 },
                new { Id = 60, Name = "Blue Buffalo Treats", Price = 85000, Image = "/images/products/cat60.jpg", Category = "banh-quy", Brand = "Blue Buffalo", IsOnSale = true, DiscountPercent = 15, Rating = 4.5 },
                
                // Vitamin & Sức khỏe
                new { Id = 14, Name = "Vitamin Omega-3 cho mèo", Price = 165000, Image = "/images/products/cat6.jpg", Category = "vitamin", Brand = "Hill's", IsOnSale = false, DiscountPercent = 0, Rating = 4.4 },
                new { Id = 61, Name = "Hairball Control Supplement", Price = 180000, Image = "/images/products/cat61.jpg", Category = "vitamin", Brand = "VetriScience", IsOnSale = true, DiscountPercent = 20, Rating = 4.7 },
                new { Id = 62, Name = "Probiotics for Cats", Price = 250000, Image = "/images/products/cat62.jpg", Category = "ho-tro-tieu-hoa", Brand = "FortiFlora", IsOnSale = false, DiscountPercent = 0, Rating = 4.8 },
                
                // Vệ sinh
                new { Id = 15, Name = "Sữa tắm mèo Natural", Price = 190000, Image = "/images/products/cat7.jpg", Category = "sua-tam", Brand = "Natural", IsOnSale = true, DiscountPercent = 10, Rating = 4.6 },
                new { Id = 63, Name = "Cat Brush Slicker", Price = 120000, Image = "/images/products/cat63.jpg", Category = "ban-chai", Brand = "Hertzko", IsOnSale = false, DiscountPercent = 0, Rating = 4.5 },
                new { Id = 64, Name = "Grooming Wipes", Price = 75000, Image = "/images/products/cat64.jpg", Category = "khan-lau", Brand = "Earthbath", IsOnSale = true, DiscountPercent = 8, Rating = 4.4 },
                
                // Đồ chơi
                new { Id = 16, Name = "Chuột nhồi bông Catnip", Price = 45000, Image = "/images/products/cat8.jpg", Category = "bong", Brand = "Cat Toy", IsOnSale = false, DiscountPercent = 0, Rating = 4.7 },
                new { Id = 65, Name = "Feather Wand Toy", Price = 85000, Image = "/images/products/cat65.jpg", Category = "bong", Brand = "GoCat", IsOnSale = true, DiscountPercent = 22, Rating = 4.8 },
                new { Id = 66, Name = "Catnip Filled Fish", Price = 35000, Image = "/images/products/cat66.jpg", Category = "do-choi-gam", Brand = "Yeowww!", IsOnSale = false, DiscountPercent = 0, Rating = 4.6 },
                new { Id = 67, Name = "Interactive Puzzle", Price = 180000, Image = "/images/products/cat67.jpg", Category = "do-choi-gam", Brand = "Nina Ottosson", IsOnSale = true, DiscountPercent = 30, Rating = 4.9 },
                
                // Phụ kiện
                new { Id = 68, Name = "Breakaway Cat Collar", Price = 95000, Image = "/images/products/cat68.jpg", Category = "vong-co", Brand = "Safe Cat", IsOnSale = false, DiscountPercent = 0, Rating = 4.3 },
                new { Id = 69, Name = "Retractable Cat Leash", Price = 150000, Image = "/images/products/cat69.jpg", Category = "day-dat", Brand = "PetSafe", IsOnSale = true, DiscountPercent = 25, Rating = 4.2 },
                new { Id = 70, Name = "Soft Cat Carrier", Price = 650000, Image = "/images/products/cat70.jpg", Category = "long-van-chuyen", Brand = "Sherpa", IsOnSale = false, DiscountPercent = 0, Rating = 4.6 }
            };
        }

        private List<dynamic> GetDogCategories()
        {
            return new List<dynamic>
            {
                new { Id = "thuc-an-hat", Name = "Thức ăn hạt", Icon = "🥣", ProductCount = 15 },
                new { Id = "thuc-an-uot", Name = "Thức ăn ướt", Icon = "🥫", ProductCount = 8 },
                new { Id = "thuc-an-kho", Name = "Thức ăn khô", Icon = "🍖", ProductCount = 6 },
                new { Id = "thuc-an-huu-co", Name = "Thức ăn hữu cơ", Icon = "🌿", ProductCount = 4 },
                new { Id = "khac-thuc-an", Name = "Thức ăn khác", Icon = "🍲", ProductCount = 3 },
                new { Id = "pate", Name = "Pate", Icon = "🍽️", ProductCount = 12 },
                new { Id = "thit-say-kho", Name = "Thịt sấy khô", Icon = "🥩", ProductCount = 7 },
                new { Id = "sup-thuong", Name = "Súp thưởng", Icon = "🍲", ProductCount = 5 },
                new { Id = "banh-quy", Name = "Bánh quy", Icon = "🍪", ProductCount = 9 },
                new { Id = "khac-banh-thuong", Name = "Bánh thưởng khác", Icon = "🧁", ProductCount = 4 },
                new { Id = "vitamin", Name = "Vitamin", Icon = "💊", ProductCount = 11 },
                new { Id = "thuoc-nho-gay", Name = "Thuốc nhỏ gáy", Icon = "💉", ProductCount = 3 },
                new { Id = "ho-tro-tieu-hoa", Name = "Hỗ trợ tiêu hóa", Icon = "🌱", ProductCount = 6 },
                new { Id = "khac-suc-khoe", Name = "Sức khỏe khác", Icon = "🏥", ProductCount = 2 },
                new { Id = "sua-tam", Name = "Sữa tắm", Icon = "🧴", ProductCount = 8 },
                new { Id = "ban-chai", Name = "Bàn chải", Icon = "🪥", ProductCount = 5 },
                new { Id = "khan-lau", Name = "Khăn lau", Icon = "🧽", ProductCount = 4 },
                new { Id = "khac-ve-sinh", Name = "Vệ sinh khác", Icon = "🧼", ProductCount = 3 },
                new { Id = "bong", Name = "Bóng", Icon = "⚽", ProductCount = 6 },
                new { Id = "xuong-gam", Name = "Xương gặm", Icon = "🦴", ProductCount = 8 },
                new { Id = "do-choi-gam", Name = "Đồ chơi gặm", Icon = "🧸", ProductCount = 7 },
                new { Id = "khac-do-choi", Name = "Đồ chơi khác", Icon = "🎾", ProductCount = 5 },
                new { Id = "vong-co", Name = "Vòng cổ", Icon = "🔗", ProductCount = 12 },
                new { Id = "day-dat", Name = "Dây dắt", Icon = "🦮", ProductCount = 10 },
                new { Id = "long-van-chuyen", Name = "Lồng vận chuyển", Icon = "📦", ProductCount = 5 },
                new { Id = "khac-phu-kien", Name = "Phụ kiện khác", Icon = "🎒", ProductCount = 4 }
            };
        }

        private List<dynamic> GetCatCategories()
        {
            return new List<dynamic>
            {
                new { Id = "thuc-an-hat", Name = "Thức ăn hạt", Icon = "🥣", ProductCount = 18 },
                new { Id = "thuc-an-uot", Name = "Thức ăn ướt", Icon = "🥫", ProductCount = 10 },
                new { Id = "thuc-an-kho", Name = "Thức ăn khô", Icon = "🍖", ProductCount = 5 },
                new { Id = "thuc-an-huu-co", Name = "Thức ăn hữu cơ", Icon = "🌿", ProductCount = 6 },
                new { Id = "khac-thuc-an", Name = "Thức ăn khác", Icon = "🍲", ProductCount = 4 },
                new { Id = "pate", Name = "Pate", Icon = "🍽️", ProductCount = 15 },
                new { Id = "thit-say-kho", Name = "Thịt sấy khô", Icon = "🥩", ProductCount = 4 },
                new { Id = "sup-thuong", Name = "Súp thưởng", Icon = "🍲", ProductCount = 7 },
                new { Id = "banh-quy", Name = "Bánh quy", Icon = "🍪", ProductCount = 6 },
                new { Id = "khac-banh-thuong", Name = "Bánh thưởng khác", Icon = "🧁", ProductCount = 3 },
                new { Id = "vitamin", Name = "Vitamin", Icon = "💊", ProductCount = 9 },
                new { Id = "thuoc-nho-gay", Name = "Thuốc nhỏ gáy", Icon = "💉", ProductCount = 4 },
                new { Id = "ho-tro-tieu-hoa", Name = "Hỗ trợ tiêu hóa", Icon = "🌱", ProductCount = 5 },
                new { Id = "khac-suc-khoe", Name = "Sức khỏe khác", Icon = "🏥", ProductCount = 2 },
                new { Id = "sua-tam", Name = "Sữa tắm", Icon = "🧴", ProductCount = 6 },
                new { Id = "ban-chai", Name = "Bàn chải", Icon = "🪥", ProductCount = 4 },
                new { Id = "khan-lau", Name = "Khăn lau", Icon = "🧽", ProductCount = 3 },
                new { Id = "khac-ve-sinh", Name = "Vệ sinh khác", Icon = "🧼", ProductCount = 2 },
                new { Id = "bong", Name = "Bóng", Icon = "⚽", ProductCount = 8 },
                new { Id = "xuong-gam", Name = "Xương gặm", Icon = "🦴", ProductCount = 5 },
                new { Id = "do-choi-gam", Name = "Đồ chơi gặm", Icon = "🧸", ProductCount = 9 },
                new { Id = "khac-do-choi", Name = "Đồ chơi khác", Icon = "🎾", ProductCount = 4 },
                new { Id = "vong-co", Name = "Vòng cổ", Icon = "🔗", ProductCount = 8 },
                new { Id = "day-dat", Name = "Dây dắt", Icon = "🦮", ProductCount = 6 },
                new { Id = "long-van-chuyen", Name = "Lồng vận chuyển", Icon = "📦", ProductCount = 4 },
                new { Id = "khac-phu-kien", Name = "Phụ kiện khác", Icon = "🎒", ProductCount = 3 }
            };
        }

        private List<dynamic> GetDogProductsByCategory(string category)
        {
            var allProducts = GetDogProducts();
            return allProducts.Where(p => p.Category == category).ToList();
        }

        private List<dynamic> GetCatProductsByCategory(string category)
        {
            var allProducts = GetCatProducts();
            return allProducts.Where(p => p.Category == category).ToList();
        }

        private List<dynamic> GetDogProductsByCategories(string[] categories)
        {
            var allProducts = GetDogProducts();
            return allProducts.Where(p => categories.Contains((string)p.Category)).ToList();
        }

        private List<dynamic> GetCatProductsByCategories(string[] categories)
        {
            var allProducts = GetCatProducts();
            return allProducts.Where(p => categories.Contains((string)p.Category)).ToList();
        }

        private string GetCategoryDisplayName(string category, string animalType)
        {
            var categories = animalType == "dog" ? GetDogCategories() : GetCatCategories();
            var categoryItem = categories.FirstOrDefault(c => c.Id == category);
            return categoryItem?.Name ?? category;
        }

        private dynamic GetParentCategoryInfo(string category, string animalType)
        {
            var basePath = animalType == "dog" ? "/Product/Dog" : "/Product/Cat";
            var animalName = animalType == "dog" ? "chó" : "mèo";
            
            // Map categories to their parent groups
            var categoryGroupMap = new Dictionary<string, dynamic>
            {
                // Thức ăn group
                { "thuc-an-hat", new { Name = $"Thức ăn cho {animalName}", Url = $"{basePath}Food" } },
                { "thuc-an-uot", new { Name = $"Thức ăn cho {animalName}", Url = $"{basePath}Food" } },
                { "thuc-an-kho", new { Name = $"Thức ăn cho {animalName}", Url = $"{basePath}Food" } },
                { "thuc-an-huu-co", new { Name = $"Thức ăn cho {animalName}", Url = $"{basePath}Food" } },
                { "khac-thuc-an", new { Name = $"Thức ăn cho {animalName}", Url = $"{basePath}Food" } },
                
                // Pate - Bánh thưởng group  
                { "pate", new { Name = $"Pate - Bánh thưởng", Url = $"{basePath}Treats" } },
                { "thit-say-kho", new { Name = $"Pate - Bánh thưởng", Url = $"{basePath}Treats" } },
                { "sup-thuong", new { Name = $"Pate - Bánh thưởng", Url = $"{basePath}Treats" } },
                { "banh-quy", new { Name = $"Pate - Bánh thưởng", Url = $"{basePath}Treats" } },
                { "khac-banh-thuong", new { Name = $"Pate - Bánh thưởng", Url = $"{basePath}Treats" } },
                
                // Chăm sóc sức khỏe group
                { "vitamin", new { Name = $"Chăm sóc sức khỏe", Url = $"{basePath}Health" } },
                { "thuoc-nho-gay", new { Name = $"Chăm sóc sức khỏe", Url = $"{basePath}Health" } },
                { "ho-tro-tieu-hoa", new { Name = $"Chăm sóc sức khỏe", Url = $"{basePath}Health" } },
                { "khac-suc-khoe", new { Name = $"Chăm sóc sức khỏe", Url = $"{basePath}Health" } },
                
                // Chăm sóc vệ sinh group
                { "sua-tam", new { Name = $"Chăm sóc vệ sinh", Url = $"{basePath}Grooming" } },
                { "ban-chai", new { Name = $"Chăm sóc vệ sinh", Url = $"{basePath}Grooming" } },
                { "khan-lau", new { Name = $"Chăm sóc vệ sinh", Url = $"{basePath}Grooming" } },
                { "khac-ve-sinh", new { Name = $"Chăm sóc vệ sinh", Url = $"{basePath}Grooming" } },
                
                // Đồ chơi group
                { "bong", new { Name = $"Đồ chơi", Url = $"{basePath}Toys" } },
                { "xuong-gam", new { Name = $"Đồ chơi", Url = $"{basePath}Toys" } },
                { "do-choi-gam", new { Name = $"Đồ chơi", Url = $"{basePath}Toys" } },
                { "khac-do-choi", new { Name = $"Đồ chơi", Url = $"{basePath}Toys" } },
                
                // Phụ kiện khác group
                { "vong-co", new { Name = $"Phụ kiện khác", Url = $"{basePath}Accessories" } },
                { "day-dat", new { Name = $"Phụ kiện khác", Url = $"{basePath}Accessories" } },
                { "long-van-chuyen", new { Name = $"Phụ kiện khác", Url = $"{basePath}Accessories" } },
                { "khac-phu-kien", new { Name = $"Phụ kiện khác", Url = $"{basePath}Accessories" } }
            };
            
            return categoryGroupMap.TryGetValue(category, out var parentInfo) 
                ? parentInfo 
                : new { Name = $"Sản phẩm cho {animalName}", Url = basePath };
        }

        private List<dynamic> GetParentCategories(string animalType)
        {
            return new List<dynamic>
            {
                new { Id = "food", Name = $"Thức ăn cho {(animalType == "dog" ? "chó" : "mèo")}", Icon = "🥣", Url = $"/Product/{(animalType == "dog" ? "Dog" : "Cat")}Food" },
                new { Id = "treats", Name = "Pate - Bánh thưởng", Icon = "🍪", Url = $"/Product/{(animalType == "dog" ? "Dog" : "Cat")}Treats" },
                new { Id = "health", Name = "Chăm sóc sức khỏe", Icon = "💊", Url = $"/Product/{(animalType == "dog" ? "Dog" : "Cat")}Health" },
                new { Id = "grooming", Name = "Chăm sóc vệ sinh", Icon = "🧴", Url = $"/Product/{(animalType == "dog" ? "Dog" : "Cat")}Grooming" },
                new { Id = "toys", Name = "Đồ chơi", Icon = "🧸", Url = $"/Product/{(animalType == "dog" ? "Dog" : "Cat")}Toys" },
                new { Id = "accessories", Name = "Phụ kiện khác", Icon = "🎒", Url = $"/Product/{(animalType == "dog" ? "Dog" : "Cat")}Accessories" }
            };
        }
    }
} 