using GauMeo.Models.Categories;

namespace GauMeo.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<IEnumerable<Brand>> GetActiveBrandsAsync();
        Task<IEnumerable<Brand>> GetFeaturedBrandsAsync();
        Task<Brand?> GetBrandByIdAsync(int id);
        Task<Brand?> CreateBrandAsync(Brand brand);
        Task<Brand?> UpdateBrandAsync(Brand brand);
        Task<bool> DeleteBrandAsync(int id);
        Task<bool> BrandExistsAsync(int id);
        Task<int> GetBrandProductCountAsync(int brandId);
    }
} 