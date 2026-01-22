using GauMeo.Models.Categories;

namespace GauMeo.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetActiveCategoriesAsync();
        Task<IEnumerable<Category>> GetCategoriesByAnimalTypeAsync(string animalType);
        Task<IEnumerable<Category>> GetParentCategoriesAsync(string animalType);
        Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentId);
        Task<IEnumerable<Category>> GetCategoriesHierarchyAsync(string animalType);
        Task<Category?> GetMainCategoryAsync(string animalType);
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category?> GetCategoryBySlugAsync(string slug);
        Task<bool> CategoryExistsAsync(int id);
        Task<IEnumerable<Category>> GetDirectSubCategoriesAsync(int parentId);
        Task<IEnumerable<Category>> GetAllCategoriesForAdminAsync();
        Task<IEnumerable<Category>> GetCategoriesForAdminAsync(string? search = null, bool? isActive = null);
        Task<Category?> CreateCategoryAsync(Category category);
        Task<Category?> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
        Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null);
        Task<bool> HasChildCategoriesAsync(int categoryId);
        Task<bool> HasProductsAsync(int categoryId);
        Task<string> GenerateSlugAsync(string name);
        Task<IEnumerable<Category>> GetPossibleParentCategoriesAsync(int? categoryId = null);
        Task<bool> IsValidParentAsync(int categoryId, int? parentId);
    }
}