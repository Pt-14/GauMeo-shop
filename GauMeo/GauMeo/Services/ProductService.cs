using GauMeo.Data;
using GauMeo.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace GauMeo.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<dynamic>> GetProductsByAnimalTypeAsync(string animalType)
        {
            try
            {
                var products = await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages.Where(img => img.IsMain))
                    .Where(p => (p.AnimalType.ToLower() == animalType.ToLower() || p.AnimalType.ToLower() == "both") && p.IsActive)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        Price = p.CurrentPrice, // Giá cuối cùng (đã tính discount)
                        p.OriginalPrice,
                        p.DiscountPercent,
                        p.Rating,
                        p.ReviewCount,
                        p.IsOnSale,
                        p.FreeShipping,
                        p.PopularityScore,
                        Brand = p.Brand.Name,
                        Category = p.Category.Slug,
                        Image = p.ProductImages.FirstOrDefault(img => img.IsMain) != null 
                            ? p.ProductImages.FirstOrDefault(img => img.IsMain).ImageUrl 
                            : "/images/products/default-product.jpg"
                    })
                    .OrderByDescending(p => p.PopularityScore)
                    .ToListAsync();

                return products.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by animal type: {AnimalType}", animalType);
                return new List<dynamic>();
            }
        }

        public async Task<List<dynamic>> GetProductsByCategoryAsync(int categoryId)
        {
            try
            {
                var products = await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages.Where(img => img.IsMain))
                    .Where(p => p.CategoryId == categoryId && p.IsActive)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        Price = p.CurrentPrice,
                        p.OriginalPrice,
                        p.DiscountPercent,
                        p.Rating,
                        p.ReviewCount,
                        p.IsOnSale,
                        p.FreeShipping,
                        p.PopularityScore,
                        Brand = p.Brand.Name,
                        Category = p.Category.Slug,
                        Image = p.ProductImages.FirstOrDefault(img => img.IsMain) != null 
                            ? p.ProductImages.FirstOrDefault(img => img.IsMain).ImageUrl 
                            : "/images/products/default-product.jpg"
                    })
                    .OrderByDescending(p => p.PopularityScore)
                    .ToListAsync();

                return products.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by category: {CategoryId}", categoryId);
                return new List<dynamic>();
            }
        }

        public async Task<List<dynamic>> GetProductsByCategoryNameAsync(string categoryName, string animalType)
        {
            try
            {
                var query = _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages.Where(img => img.IsMain))
                    .Where(p => (p.AnimalType.ToLower() == animalType.ToLower() || p.AnimalType.ToLower() == "both") && p.IsActive);

                // Filter by category slug or name
                query = query.Where(p => p.Category.Slug.ToLower().Contains(categoryName.ToLower()) || 
                                        p.Category.Name.ToLower().Contains(categoryName.ToLower()));

                var products = await query
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        Price = p.CurrentPrice,
                        p.OriginalPrice,
                        p.DiscountPercent,
                        p.Rating,
                        p.ReviewCount,
                        p.IsOnSale,
                        p.FreeShipping,
                        p.PopularityScore,
                        Brand = p.Brand.Name,
                        Category = p.Category.Slug,
                        Image = p.ProductImages.FirstOrDefault(img => img.IsMain) != null 
                            ? p.ProductImages.FirstOrDefault(img => img.IsMain).ImageUrl 
                            : "/images/products/default-product.jpg"
                    })
                    .OrderByDescending(p => p.PopularityScore)
                    .ToListAsync();

                return products.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by category name: {CategoryName}, {AnimalType}", categoryName, animalType);
                return new List<dynamic>();
            }
        }

        public async Task<List<dynamic>> GetProductsByParentCategoryAsync(int parentCategoryId, string animalType)
        {
            try
            {
                // Get the parent category to check its AnimalType
                var parentCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == parentCategoryId);
                if (parentCategory == null)
                {
                    _logger.LogWarning("Parent category not found: {ParentCategoryId}", parentCategoryId);
                    return new List<dynamic>();
                }

                // Use the parent category's AnimalType if it exists, otherwise use the provided animalType
                var effectiveAnimalType = parentCategory.AnimalType ?? animalType;

                // Get all sub-category IDs under this parent category
                var subCategoryIds = await _context.Categories
                    .Where(c => c.ParentCategoryId == parentCategoryId && c.IsActive)
                    .Select(c => c.Id)
                    .ToListAsync();

                // Include the parent category itself in case there are products directly assigned
                subCategoryIds.Add(parentCategoryId);

                // Get products from all these categories
                var products = await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages.Where(img => img.IsMain))
                    .Where(p => subCategoryIds.Contains(p.CategoryId) && 
                               (p.AnimalType.ToLower() == effectiveAnimalType.ToLower() || p.AnimalType.ToLower() == "both") && 
                               p.IsActive)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        Price = p.CurrentPrice,
                        p.OriginalPrice,
                        p.DiscountPercent,
                        p.Rating,
                        p.ReviewCount,
                        p.IsOnSale,
                        p.FreeShipping,
                        p.PopularityScore,
                        Brand = p.Brand.Name,
                        Category = p.Category.Slug,
                        Image = p.ProductImages.FirstOrDefault(img => img.IsMain) != null 
                            ? p.ProductImages.FirstOrDefault(img => img.IsMain).ImageUrl 
                            : "/images/products/default-product.jpg"
                    })
                    .OrderByDescending(p => p.PopularityScore)
                    .ToListAsync();

                return products.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by parent category: {ParentCategoryId}, {AnimalType}", parentCategoryId, animalType);
                return new List<dynamic>();
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductDescriptions)
                    .Include(p => p.Reviews)
                    .Include(p => p.ProductVariants.Where(v => v.IsActive).OrderBy(v => v.DisplayOrder))
                    .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product by ID: {ProductId}", id);
                return null;
            }
        }

        public async Task<List<string>> GetBrandsByAnimalTypeAsync(string animalType)
        {
            try
            {
                var brands = await _context.Products
                    .Include(p => p.Brand)
                    .Where(p => (p.AnimalType.ToLower() == animalType.ToLower() || p.AnimalType.ToLower() == "both") && p.IsActive)
                    .Select(p => p.Brand.Name)
                    .Distinct()
                    .OrderBy(b => b)
                    .ToListAsync();

                return brands;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting brands by animal type: {AnimalType}", animalType);
                return new List<string>();
            }
        }

        public async Task<int> GetProductCountByAnimalTypeAsync(string animalType)
        {
            try
            {
                var count = await _context.Products
                    .Where(p => (p.AnimalType.ToLower() == animalType.ToLower() || p.AnimalType.ToLower() == "both") && p.IsActive)
                    .CountAsync();

                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product count by animal type: {AnimalType}", animalType);
                return 0;
            }
        }

        public async Task<List<dynamic>> GetRelatedProductsAsync(int productId, int limit = 4)
        {
            try
            {
                // Lấy sản phẩm hiện tại để biết brand
                var currentProduct = await _context.Products
                    .Include(p => p.Brand)
                    .FirstOrDefaultAsync(p => p.Id == productId);

                if (currentProduct == null)
                    return new List<dynamic>();

                // Lấy sản phẩm cùng hãng
                var relatedProducts = await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.ProductImages)
                    .Where(p => p.Id != productId && p.IsActive &&
                               p.BrandId == currentProduct.BrandId)
                    .OrderByDescending(p => p.PopularityScore)
                    .Take(limit)
                    .Select(p => new
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CurrentPrice = p.IsOnSale ? p.OriginalPrice * (100 - p.DiscountPercent) / 100 : p.OriginalPrice,
                        OriginalPrice = p.OriginalPrice,
                        Rating = p.Rating,
                        ReviewCount = p.ReviewCount,
                        ImageUrl = p.ProductImages.OrderBy(pi => pi.DisplayOrder).FirstOrDefault() != null 
                                   ? p.ProductImages.OrderBy(pi => pi.DisplayOrder).FirstOrDefault()!.ImageUrl 
                                   : "/images/products/default-product.jpg",
                        IsOnSale = p.IsOnSale,
                        DiscountPercent = p.DiscountPercent,
                        HasVariants = p.ProductVariants.Any(v => v.IsActive)
                    })
                    .ToListAsync();

                return relatedProducts.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting related products for product ID: {ProductId}", productId);
                return new List<dynamic>();
            }
        }

        public async Task<List<dynamic>> GetProductsByBrandAsync(int brandId)
        {
            try
            {
                var products = await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages.Where(img => img.IsMain))
                    .Where(p => p.BrandId == brandId && p.IsActive)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        Price = p.CurrentPrice,
                        p.OriginalPrice,
                        p.DiscountPercent,
                        p.Rating,
                        p.ReviewCount,
                        p.IsOnSale,
                        p.FreeShipping,
                        p.PopularityScore,
                        p.StockQuantity,
                        Brand = p.Brand.Name,
                        Category = p.Category.Name,
                        CategoryId = p.CategoryId,
                        IsNew = p.CreatedAt >= DateTime.Now.AddDays(-30), // Products created in last 30 days are "new"
                        Image = p.ProductImages.FirstOrDefault(img => img.IsMain) != null 
                            ? p.ProductImages.FirstOrDefault(img => img.IsMain).ImageUrl 
                            : "/images/products/default-product.jpg"
                    })
                    .OrderByDescending(p => p.PopularityScore)
                    .ToListAsync();

                return products.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by brand: {BrandId}", brandId);
                return new List<dynamic>();
            }
        }

        public async Task<List<dynamic>> GetCategoriesByBrandAsync(int brandId)
        {
            try
            {
                var categories = await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.BrandId == brandId && p.IsActive)
                    .GroupBy(p => new { p.CategoryId, p.Category.Name })
                    .Select(g => new
                    {
                        Id = g.Key.CategoryId,
                        Name = g.Key.Name,
                        Count = g.Count()
                    })
                    .OrderBy(c => c.Name)
                    .ToListAsync();

                return categories.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting categories by brand: {BrandId}", brandId);
                return new List<dynamic>();
            }
        }

        public async Task<List<dynamic>> GetPriceRangesByBrandAsync(int brandId)
        {
            try
            {
                var productPrices = await _context.Products
                    .Where(p => p.BrandId == brandId && p.IsActive)
                    .Select(p => p.CurrentPrice)
                    .ToListAsync();

                if (!productPrices.Any())
                    return new List<dynamic>();

                var priceRanges = new List<dynamic>
                {
                    new { Value = "0-100000", Label = "Dưới 100,000đ", Count = productPrices.Count(p => p < 100000) },
                    new { Value = "100000-300000", Label = "100,000đ - 300,000đ", Count = productPrices.Count(p => p >= 100000 && p < 300000) },
                    new { Value = "300000-500000", Label = "300,000đ - 500,000đ", Count = productPrices.Count(p => p >= 300000 && p < 500000) },
                    new { Value = "500000-1000000", Label = "500,000đ - 1,000,000đ", Count = productPrices.Count(p => p >= 500000 && p < 1000000) },
                    new { Value = "1000000-", Label = "Trên 1,000,000đ", Count = productPrices.Count(p => p >= 1000000) }
                };

                // Only return ranges that have products
                return priceRanges.Where(r => ((dynamic)r).Count > 0).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting price ranges by brand: {BrandId}", brandId);
                return new List<dynamic>();
            }
        }

        public async Task IncreasePopularityScoreAsync(int productId)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);
                if (product != null)
                {
                    product.PopularityScore += 1;
                    product.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error increasing popularity score for product: {ProductId}", productId);
                // Don't throw exception as this is not critical functionality
            }
        }

        public async Task<List<dynamic>> GetBestSellingProductsAsync(int limit = 8)
        {
            try
            {
                var products = await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages.Where(img => img.IsMain))
                    .Include(p => p.ProductVariants.Where(v => v.IsActive))
            
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        CurrentPrice = p.OriginalPrice * (100 - p.DiscountPercent) / 100,
                        p.OriginalPrice,
                        p.DiscountPercent,
                        p.Rating,
                        p.ReviewCount,
                        p.IsOnSale,
                        p.FreeShipping,
                        p.PopularityScore,
                        Brand = p.Brand != null ? p.Brand.Name : "",
                        Category = p.Category != null ? p.Category.Slug : "",
                        Image = p.ProductImages.FirstOrDefault(img => img.IsMain) != null 
                            ? p.ProductImages.FirstOrDefault(img => img.IsMain)!.ImageUrl 
                            : "/images/products/default-product.jpg",
                        HasVariants = p.ProductVariants.Any(v => v.IsActive)
                    })
                    .OrderByDescending(p => p.PopularityScore)
                    .Take(limit)
                    .ToListAsync();

                return products.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting best selling products");
                return new List<dynamic>();
            }
        }

        public async Task<List<dynamic>> GetPromotionProductsAsync(int limit = 8)
        {
            try
            {
                var products = await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages.Where(img => img.IsMain))
                    .Include(p => p.ProductVariants.Where(v => v.IsActive))
                    .Where(p => p.IsOnSale && p.DiscountPercent > 0 && p.IsActive)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        CurrentPrice = p.OriginalPrice * (100 - p.DiscountPercent) / 100,
                        p.OriginalPrice,
                        p.DiscountPercent,
                        p.Rating,
                        p.ReviewCount,
                        p.IsOnSale,
                        p.FreeShipping,
                        p.PopularityScore,
                        Brand = p.Brand != null ? p.Brand.Name : "",
                        Category = p.Category != null ? p.Category.Slug : "",
                        Image = p.ProductImages.FirstOrDefault(img => img.IsMain) != null 
                            ? p.ProductImages.FirstOrDefault(img => img.IsMain)!.ImageUrl 
                            : "/images/products/default-product.jpg",
                        HasVariants = p.ProductVariants.Any(v => v.IsActive)
                    })
                    .OrderByDescending(p => p.DiscountPercent) // Ưu tiên sản phẩm giảm giá nhiều nhất
                    .ThenByDescending(p => p.PopularityScore) // Sau đó ưu tiên sản phẩm phổ biến
                    .Take(limit)
                    .ToListAsync();

                return products.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting promotion products");
                return new List<dynamic>();
            }
        }

        // ADMIN CRUD Methods Implementation
        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages.Where(img => img.IsMain))
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all products for admin");
                return new List<Product>();
            }
        }

        public async Task<Product?> GetProductByIdWithDetailsAsync(int id)
        {
            try
            {
                return await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductDescriptions)
                    .Include(p => p.ProductVariants)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product by ID with details: {ProductId}", id);
                return null;
            }
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product: {ProductName}", product.Name);
                throw;
            }
        }

        public async Task<Product> CreateProductWithDetailsAsync(Product product, List<ProductImage> images, List<ProductDescription> descriptions)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                // Add Images
                foreach (var image in images)
                {
                    image.ProductId = product.Id;
                    image.CreatedAt = DateTime.Now;
                    _context.ProductImages.Add(image);
                }

                // Add Descriptions
                foreach (var description in descriptions)
                {
                    description.ProductId = product.Id;
                    description.CreatedAt = DateTime.Now;
                    _context.ProductDescriptions.Add(description);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Return product with includes
                return await GetProductByIdWithDetailsAsync(product.Id) ?? product;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating product with details: {ProductName}", product.Name);
                throw;
            }
        }

        public async Task<Product?> UpdateProductAsync(Product product)
        {
            try
            {
                var existingProduct = await _context.Products.FindAsync(product.Id);
                if (existingProduct == null)
                {
                    return null;
                }

                // Update properties
                existingProduct.Name = product.Name;
                existingProduct.ShortDescription = product.ShortDescription;
                existingProduct.OriginalPrice = product.OriginalPrice;
                existingProduct.DiscountPercent = product.DiscountPercent;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.IsActive = product.IsActive;
                existingProduct.IsFeatured = product.IsFeatured;
                existingProduct.IsOnSale = product.IsOnSale;
                existingProduct.FreeShipping = product.FreeShipping;
                existingProduct.AnimalType = product.AnimalType;
                existingProduct.Tags = product.Tags;
                existingProduct.BrandId = product.BrandId;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return existingProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product: {ProductId}", product.Id);
                return null;
            }
        }

        public async Task<Product?> UpdateProductWithDetailsAsync(Product product, List<ProductImage> images, List<ProductDescription> descriptions)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingProduct = await _context.Products
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductDescriptions)
                    .FirstOrDefaultAsync(p => p.Id == product.Id);

                if (existingProduct == null)
                {
                    return null;
                }

                // Update basic product properties
                existingProduct.Name = product.Name;
                existingProduct.ShortDescription = product.ShortDescription;
                existingProduct.OriginalPrice = product.OriginalPrice;
                existingProduct.DiscountPercent = product.DiscountPercent;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.IsActive = product.IsActive;
                existingProduct.IsFeatured = product.IsFeatured;
                existingProduct.IsOnSale = product.IsOnSale;
                existingProduct.FreeShipping = product.FreeShipping;
                existingProduct.AnimalType = product.AnimalType;
                existingProduct.Tags = product.Tags;
                existingProduct.BrandId = product.BrandId;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.UpdatedAt = DateTime.Now;

                // Update Images
                _context.ProductImages.RemoveRange(existingProduct.ProductImages);
                foreach (var image in images)
                {
                    image.ProductId = existingProduct.Id;
                    image.CreatedAt = DateTime.Now;
                    _context.ProductImages.Add(image);
                }

                // Update Descriptions
                _context.ProductDescriptions.RemoveRange(existingProduct.ProductDescriptions);
                foreach (var description in descriptions)
                {
                    description.ProductId = existingProduct.Id;
                    description.CreatedAt = DateTime.Now;
                    _context.ProductDescriptions.Add(description);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Return updated product with includes
                return await GetProductByIdWithDetailsAsync(existingProduct.Id) ?? existingProduct;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error updating product with details: {ProductId}", product.Id);
                return null;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return false;
                }

                // Soft delete by setting IsActive to false
                product.IsActive = false;
                product.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product: {ProductId}", id);
                return false;
            }
        }

        public async Task<bool> ToggleProductStatusAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return false;
                }

                product.IsActive = !product.IsActive;
                product.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling product status: {ProductId}", id);
                return false;
            }
        }
    }
}