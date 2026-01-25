using GauMeo.Data;
using GauMeo.Models.Categories;
using Microsoft.EntityFrameworkCore;

namespace GauMeo.Services
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BrandService> _logger;

        public BrandService(ApplicationDbContext context, ILogger<BrandService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            try
            {
                return await _context.Brands
                    .OrderBy(b => b.DisplayOrder)
                    .ThenBy(b => b.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all brands");
                return new List<Brand>();
            }
        }

        public async Task<IEnumerable<Brand>> GetActiveBrandsAsync()
        {
            try
            {
                return await _context.Brands
                    .Where(b => b.IsActive)
                    .OrderBy(b => b.DisplayOrder)
                    .ThenBy(b => b.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active brands");
                return new List<Brand>();
            }
        }

        public async Task<IEnumerable<Brand>> GetFeaturedBrandsAsync()
        {
            try
            {
                return await _context.Brands
                    .Where(b => b.IsActive && b.IsFeatured)
                    .OrderBy(b => b.DisplayOrder)
                    .ThenBy(b => b.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting featured brands");
                return new List<Brand>();
            }
        }

        public async Task<Brand?> GetBrandByIdAsync(int id)
        {
            try
            {
                return await _context.Brands
                    .Include(b => b.Products.Where(p => p.IsActive))
                    .FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting brand by id {BrandId}", id);
                return null;
            }
        }

        public async Task<Brand?> CreateBrandAsync(Brand brand)
        {
            try
            {
                brand.CreatedAt = DateTime.Now;
                brand.UpdatedAt = DateTime.Now;
                
                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Brand {BrandName} created successfully with Id {BrandId}", brand.Name, brand.Id);
                return brand;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating brand {BrandName}", brand.Name);
                return null;
            }
        }

        public async Task<Brand?> UpdateBrandAsync(Brand brand)
        {
            try
            {
                var existingBrand = await _context.Brands.FindAsync(brand.Id);
                if (existingBrand == null)
                {
                    _logger.LogWarning("Brand with Id {BrandId} not found for update", brand.Id);
                    return null;
                }

                existingBrand.Name = brand.Name;
                existingBrand.ShortName = brand.ShortName;
                existingBrand.Description = brand.Description;
                existingBrand.FullDescription = brand.FullDescription;
                existingBrand.Founded = brand.Founded;
                existingBrand.Origin = brand.Origin;
                existingBrand.Website = brand.Website;
                existingBrand.Image = brand.Image;
                existingBrand.Features = brand.Features;
                existingBrand.DisplayOrder = brand.DisplayOrder;
                existingBrand.IsActive = brand.IsActive;
                existingBrand.IsFeatured = brand.IsFeatured;
                existingBrand.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Brand {BrandName} updated successfully", brand.Name);
                return existingBrand;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating brand {BrandId}", brand.Id);
                return null;
            }
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(id);
                if (brand == null)
                {
                    _logger.LogWarning("Brand with Id {BrandId} not found for deletion", id);
                    return false;
                }

                // Check if brand has products
                var hasProducts = await _context.Products.AnyAsync(p => p.BrandId == id);
                if (hasProducts)
                {
                    _logger.LogWarning("Cannot delete brand {BrandId} because it has associated products", id);
                    return false;
                }

                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Brand {BrandName} deleted successfully", brand.Name);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting brand {BrandId}", id);
                return false;
            }
        }

        public async Task<bool> BrandExistsAsync(int id)
        {
            try
            {
                return await _context.Brands.AnyAsync(b => b.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if brand {BrandId} exists", id);
                return false;
            }
        }

        public async Task<int> GetBrandProductCountAsync(int brandId)
        {
            try
            {
                return await _context.Products
                    .Where(p => p.BrandId == brandId && p.IsActive)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product count for brand {BrandId}", brandId);
                return 0;
            }
        }
    }
} 