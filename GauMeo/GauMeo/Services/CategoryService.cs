using GauMeo.Data;
using GauMeo.Models.Categories;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace GauMeo.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ApplicationDbContext context, ILogger<CategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Category>> GetActiveCategoriesAsync()
        {
            try
            {
                return await _context.Categories
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.DisplayOrder)
                    .ThenBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all active categories");
                return new List<Category>();
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesByAnimalTypeAsync(string animalType)
        {
            try
            {
                return await _context.Categories
                    .Where(c => c.IsActive && (c.AnimalType == animalType || c.AnimalType == "both"))
                    .OrderBy(c => c.Level)
                    .ThenBy(c => c.DisplayOrder)
                    .ThenBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting categories by animal type {AnimalType}", animalType);
                return new List<Category>();
            }
        }

        public async Task<IEnumerable<Category>> GetParentCategoriesAsync(string animalType)
        {
            try
            {
                return await _context.Categories
                    .Where(c => c.IsActive && (c.AnimalType == animalType || c.AnimalType == "both") && c.Level == 2)
                    .OrderBy(c => c.DisplayOrder)
                    .ThenBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting parent categories for {AnimalType}", animalType);
                return new List<Category>();
            }
        }

        public async Task<Category?> GetMainCategoryAsync(string animalType)
        {
            try
            {
                return await _context.Categories
                    .FirstOrDefaultAsync(c => c.IsActive && (c.AnimalType == animalType || c.AnimalType == "both") && c.Level == 1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting main category for {AnimalType}", animalType);
                return null;
            }
        }

        public async Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentId)
        {
            try
            {
                return await _context.Categories
                    .Where(c => c.IsActive && c.ParentCategoryId == parentId && c.Level == 3)
                    .OrderBy(c => c.DisplayOrder)
                    .ThenBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sub categories for parent {ParentId}", parentId);
                return new List<Category>();
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesHierarchyAsync(string animalType)
        {
            try
            {
                return await _context.Categories
                    .Where(c => c.IsActive && (c.AnimalType == animalType || c.AnimalType == "both") && (c.Level == 2 || c.Level == 3))
                    .OrderBy(c => c.Level)
                    .ThenBy(c => c.DisplayOrder)
                    .ThenBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting categories hierarchy for {AnimalType}", animalType);
                return new List<Category>();
            }
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            try
            {
                return await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting category by id {CategoryId}", id);
                return null;
            }
        }

        public async Task<Category?> GetCategoryBySlugAsync(string slug)
        {
            try
            {
                return await _context.Categories
                    .FirstOrDefaultAsync(c => c.Slug == slug && c.IsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting category by slug {Slug}", slug);
                return null;
            }
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            try
            {
                return await _context.Categories
                    .AnyAsync(c => c.Id == id && c.IsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if category exists {CategoryId}", id);
                return false;
            }
        }

        public async Task<IEnumerable<Category>> GetDirectSubCategoriesAsync(int parentId)
        {
            try
            {
                return await _context.Categories
                    .Where(c => c.IsActive && c.ParentCategoryId == parentId)
                    .OrderBy(c => c.DisplayOrder)
                    .ThenBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting direct sub categories for parent {ParentId}", parentId);
                return new List<Category>();
            }
        }

        // NEW: Admin operations
        public async Task<IEnumerable<Category>> GetAllCategoriesForAdminAsync()
        {
            try
            {
                return await _context.Categories
                    .Include(c => c.ParentCategory)
                    .OrderBy(c => c.Level)
                    .ThenBy(c => c.ParentCategoryId ?? 0)
                    .ThenBy(c => c.DisplayOrder)
                    .ThenBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all categories for admin");
                return new List<Category>();
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesForAdminAsync(string? search = null, bool? isActive = null)
        {
            try
            {
                var query = _context.Categories.Include(c => c.ParentCategory).AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(c => c.Name.Contains(search) || 
                                           (c.Description != null && c.Description.Contains(search)) ||
                                           c.Slug.Contains(search));
                }

                if (isActive.HasValue)
                {
                    query = query.Where(c => c.IsActive == isActive.Value);
                }

                return await query
                    .OrderBy(c => c.Level)
                    .ThenBy(c => c.ParentCategoryId ?? 0)
                    .ThenBy(c => c.DisplayOrder)
                    .ThenBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting categories for admin with search: {Search}, isActive: {IsActive}", search, isActive);
                return new List<Category>();
            }
        }

        // NEW: CRUD operations
        public async Task<Category?> CreateCategoryAsync(Category category)
        {
            try
            {
                // Validate slug uniqueness
                if (!await IsSlugUniqueAsync(category.Slug))
                {
                    category.Slug = await GenerateSlugAsync(category.Name);
                }

                // Validate parent relationship (for new category, we don't need to check circular reference)
                if (category.ParentCategoryId.HasValue)
                {
                    var parent = await _context.Categories.FindAsync(category.ParentCategoryId.Value);
                    if (parent == null || !parent.IsActive)
                    {
                        _logger.LogWarning("Invalid parent category {ParentId} for category {CategoryName}", 
                            category.ParentCategoryId, category.Name);
                        return null;
                    }

                    // Level validation for new category
                    if (category.Level == 1 && category.ParentCategoryId.HasValue)
                    {
                        _logger.LogWarning("Level 1 category cannot have parent");
                        return null;
                    }
                    if (category.Level == 2 && parent.Level != 1)
                    {
                        _logger.LogWarning("Level 2 category can only have Level 1 parent");
                        return null;
                    }
                    if (category.Level == 3 && parent.Level != 2)
                    {
                        _logger.LogWarning("Level 3 category can only have Level 2 parent");
                        return null;
                    }
                }

                category.CreatedAt = DateTime.Now;
                category.UpdatedAt = DateTime.Now;

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Category {CategoryName} created successfully with Id {CategoryId}", 
                    category.Name, category.Id);
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category {CategoryName}", category.Name);
                return null;
            }
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            try
            {
                var existingCategory = await _context.Categories.FindAsync(category.Id);
                if (existingCategory == null)
                {
                    _logger.LogWarning("Category with Id {CategoryId} not found for update", category.Id);
                    return null;
                }

                // Validate slug uniqueness (excluding current category)
                if (existingCategory.Slug != category.Slug && !await IsSlugUniqueAsync(category.Slug, category.Id))
                {
                    _logger.LogWarning("Slug {Slug} already exists for another category", category.Slug);
                    return null;
                }

                // Validate parent relationship
                if (category.ParentCategoryId.HasValue)
                {
                    if (!await IsValidParentAsync(category.Id, category.ParentCategoryId))
                    {
                        _logger.LogWarning("Invalid parent category {ParentId} for category {CategoryId}", 
                            category.ParentCategoryId, category.Id);
                        return null;
                    }
                }

                // Update properties
                existingCategory.Name = category.Name;
                existingCategory.Slug = category.Slug;
                existingCategory.Description = category.Description;
                existingCategory.ImageUrl = category.ImageUrl;
                existingCategory.IconUrl = category.IconUrl;
                existingCategory.Icon = category.Icon;
                existingCategory.Level = category.Level;
                existingCategory.DisplayOrder = category.DisplayOrder;
                existingCategory.IsActive = category.IsActive;
                existingCategory.IsShowOnHome = category.IsShowOnHome;
                existingCategory.AnimalType = category.AnimalType;
                existingCategory.ParentCategoryId = category.ParentCategoryId;
                existingCategory.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Category {CategoryName} updated successfully", category.Name);
                return existingCategory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category {CategoryId}", category.Id);
                return null;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    _logger.LogWarning("Category with Id {CategoryId} not found for deletion", id);
                    return false;
                }

                // Check if category has child categories
                if (await HasChildCategoriesAsync(id))
                {
                    _logger.LogWarning("Cannot delete category {CategoryId} because it has child categories", id);
                    return false;
                }

                // Check if category has products
                if (await HasProductsAsync(id))
                {
                    _logger.LogWarning("Cannot delete category {CategoryId} because it has associated products", id);
                    return false;
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Category {CategoryName} deleted successfully", category.Name);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category {CategoryId}", id);
                return false;
            }
        }

        // NEW: Validation methods
        public async Task<bool> IsSlugUniqueAsync(string slug, int? excludeId = null)
        {
            try
            {
                var query = _context.Categories.Where(c => c.Slug == slug);
                
                if (excludeId.HasValue)
                {
                    query = query.Where(c => c.Id != excludeId.Value);
                }

                return !await query.AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking slug uniqueness for {Slug}", slug);
                return false;
            }
        }

        public async Task<bool> HasChildCategoriesAsync(int categoryId)
        {
            try
            {
                return await _context.Categories.AnyAsync(c => c.ParentCategoryId == categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking child categories for {CategoryId}", categoryId);
                return false;
            }
        }

        public async Task<bool> HasProductsAsync(int categoryId)
        {
            try
            {
                // Check if Products table exists and has the foreign key
                return await _context.Products.AnyAsync(p => p.CategoryId == categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking products for category {CategoryId}", categoryId);
                return false;
            }
        }

        public async Task<string> GenerateSlugAsync(string name)
        {
            try
            {
                // Convert to lowercase and replace special characters
                var slug = name.ToLowerInvariant();
                
                // Replace Vietnamese characters
                slug = slug.Replace("á", "a").Replace("à", "a").Replace("ả", "a").Replace("ã", "a").Replace("ạ", "a")
                          .Replace("ă", "a").Replace("ắ", "a").Replace("ằ", "a").Replace("ẳ", "a").Replace("ẵ", "a").Replace("ặ", "a")
                          .Replace("â", "a").Replace("ấ", "a").Replace("ầ", "a").Replace("ẩ", "a").Replace("ẫ", "a").Replace("ậ", "a")
                          .Replace("é", "e").Replace("è", "e").Replace("ẻ", "e").Replace("ẽ", "e").Replace("ẹ", "e")
                          .Replace("ê", "e").Replace("ế", "e").Replace("ề", "e").Replace("ể", "e").Replace("ễ", "e").Replace("ệ", "e")
                          .Replace("í", "i").Replace("ì", "i").Replace("ỉ", "i").Replace("ĩ", "i").Replace("ị", "i")
                          .Replace("ó", "o").Replace("ò", "o").Replace("ỏ", "o").Replace("õ", "o").Replace("ọ", "o")
                          .Replace("ô", "o").Replace("ố", "o").Replace("ồ", "o").Replace("ổ", "o").Replace("ỗ", "o").Replace("ộ", "o")
                          .Replace("ơ", "o").Replace("ớ", "o").Replace("ờ", "o").Replace("ở", "o").Replace("ỡ", "o").Replace("ợ", "o")
                          .Replace("ú", "u").Replace("ù", "u").Replace("ủ", "u").Replace("ũ", "u").Replace("ụ", "u")
                          .Replace("ư", "u").Replace("ứ", "u").Replace("ừ", "u").Replace("ử", "u").Replace("ữ", "u").Replace("ự", "u")
                          .Replace("ý", "y").Replace("ỳ", "y").Replace("ỷ", "y").Replace("ỹ", "y").Replace("ỵ", "y")
                          .Replace("đ", "d");

                // Replace spaces and special characters with hyphens
                slug = Regex.Replace(slug, @"[^a-z0-9]+", "-");
                
                // Remove leading and trailing hyphens
                slug = slug.Trim('-');

                // Ensure uniqueness
                var originalSlug = slug;
                var counter = 1;
                
                while (!await IsSlugUniqueAsync(slug))
                {
                    slug = $"{originalSlug}-{counter}";
                    counter++;
                }

                return slug;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating slug for {Name}", name);
                return Guid.NewGuid().ToString("N")[..8]; // Fallback to random string
            }
        }

        // NEW: Hierarchy helpers
        public async Task<IEnumerable<Category>> GetPossibleParentCategoriesAsync(int? categoryId = null)
        {
            try
            {
                var query = _context.Categories.Where(c => c.IsActive && c.Level < 3);

                // Exclude the category itself and its descendants
                if (categoryId.HasValue)
                {
                    // Get all descendant IDs to exclude
                    var excludeIds = new List<int> { categoryId.Value };
                    await GetDescendantIdsRecursive(categoryId.Value, excludeIds);
                    
                    query = query.Where(c => !excludeIds.Contains(c.Id));
                }

                return await query
                    .OrderBy(c => c.Level)
                    .ThenBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting possible parent categories for {CategoryId}", categoryId);
                return new List<Category>();
            }
        }

        public async Task<bool> IsValidParentAsync(int categoryId, int? parentId)
        {
            try
            {
                if (!parentId.HasValue) return true;

                var parent = await _context.Categories.FindAsync(parentId.Value);
                if (parent == null || !parent.IsActive) return false;

                // Cannot be parent of itself
                if (categoryId == parentId) return false;

                // Check if it would create a circular reference
                var currentParentId = parent.ParentCategoryId;
                while (currentParentId.HasValue)
                {
                    if (currentParentId == categoryId) return false;
                    
                    var nextParent = await _context.Categories.FindAsync(currentParentId.Value);
                    if (nextParent == null) break;
                    
                    currentParentId = nextParent.ParentCategoryId;
                }

                // Level validation: Level 3 categories can only have Level 2 parents
                // Level 2 categories can only have Level 1 parents
                // Level 1 categories cannot have parents
                var category = await _context.Categories.FindAsync(categoryId);
                if (category != null)
                {
                    if (category.Level == 1) return parentId == null;
                    if (category.Level == 2) return parent.Level == 1;
                    if (category.Level == 3) return parent.Level == 2;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating parent {ParentId} for category {CategoryId}", parentId, categoryId);
                return false;
            }
        }

        private async Task GetDescendantIdsRecursive(int parentId, List<int> descendantIds)
        {
            try
            {
                var children = await _context.Categories
                    .Where(c => c.ParentCategoryId == parentId)
                    .Select(c => c.Id)
                    .ToListAsync();

                foreach (var childId in children)
                {
                    if (!descendantIds.Contains(childId))
                    {
                        descendantIds.Add(childId);
                        await GetDescendantIdsRecursive(childId, descendantIds);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting descendant IDs for parent {ParentId}", parentId);
            }
        }
    }
}