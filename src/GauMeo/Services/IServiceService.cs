using GauMeo.Models.Services;

namespace GauMeo.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetAllActiveServicesAsync();
        Task<Service?> GetServiceByIdAsync(int id);
        Task<Service?> GetServiceWithDetailsAsync(int id);
        Task<IEnumerable<ServiceVariant>> GetServiceVariantsByServiceIdAsync(int serviceId);
        Task<IEnumerable<ServiceAddon>> GetServiceAddonsByServiceIdAsync(int serviceId);
        Task<IEnumerable<ServiceFAQ>> GetServiceFAQsByServiceIdAsync(int serviceId);
        Task<IEnumerable<ServiceNote>> GetServiceNotesByServiceIdAsync(int serviceId);
        Task<IEnumerable<ServiceImage>> GetServiceImagesByServiceIdAsync(int serviceId);
    }
} 