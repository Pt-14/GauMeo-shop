using GauMeo.Models.Products;

namespace GauMeo.Services
{
    public interface IProductService
    {
        // Basic product retrieval methods
        Task<List<dynamic>> GetProductsByAnimalTypeAsync(string animalType);
        Task<List<dynamic>> GetProductsByCategoryAsync(int categoryId);
        Task<List<dynamic>> GetProductsByCategoryNameAsync(string categoryName, string animalType);
        
        // NEW: Get products by parent category (includes all sub-categories)
        Task<List<dynamic>> GetProductsByParentCategoryAsync(int parentCategoryId, string animalType);
        
        // NEW: Get products by brand
        Task<List<dynamic>> GetProductsByBrandAsync(int brandId);
        
        // Product detail
        Task<Product?> GetProductByIdAsync(int id);
        
        // Brands from products (for filters)
        Task<List<string>> GetBrandsByAnimalTypeAsync(string animalType);
        
        // NEW: Get categories and price ranges for brand detail
        Task<List<dynamic>> GetCategoriesByBrandAsync(int brandId);
        Task<List<dynamic>> GetPriceRangesByBrandAsync(int brandId);
        
        // Product count
        Task<int> GetProductCountByAnimalTypeAsync(string animalType);
        
        // Related products
        Task<List<dynamic>> GetRelatedProductsAsync(int productId, int limit = 4);
        
        // Best selling products (for homepage)
        Task<List<dynamic>> GetBestSellingProductsAsync(int limit = 8);
        
        // NEW: Promotion products (for homepage)
        Task<List<dynamic>> GetPromotionProductsAsync(int limit = 8);
        
        // Popularity tracking
        Task IncreasePopularityScoreAsync(int productId);

        // ADMIN CRUD Methods
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdWithDetailsAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> CreateProductWithDetailsAsync(Product product, List<ProductImage> images, List<ProductDescription> descriptions);
        Task<Product?> UpdateProductAsync(Product product);
        Task<Product?> UpdateProductWithDetailsAsync(Product product, List<ProductImage> images, List<ProductDescription> descriptions);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> ToggleProductStatusAsync(int id);
    }
}