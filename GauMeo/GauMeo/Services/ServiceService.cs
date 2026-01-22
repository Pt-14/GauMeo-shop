using GauMeo.Data;
using GauMeo.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace GauMeo.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ServiceService> _logger;

        public ServiceService(ApplicationDbContext context, ILogger<ServiceService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Service>> GetAllActiveServicesAsync()
        {
            try
            {
                return await _context.Services
                    .Where(s => s.IsActive)
                    .OrderBy(s => s.DisplayOrder)
                    .ThenBy(s => s.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all active services");
                return new List<Service>();
            }
        }

        public async Task<Service?> GetServiceByIdAsync(int id)
        {
            try
            {
                return await _context.Services
                    .FirstOrDefaultAsync(s => s.Id == id && s.IsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting service by id {ServiceId}", id);
                return null;
            }
        }

        public async Task<Service?> GetServiceWithDetailsAsync(int id)
        {
            try
            {
                var service = await _context.Services
                    .FirstOrDefaultAsync(s => s.Id == id && s.IsActive);

                if (service != null)
                {
                    // Load related data manually to ensure proper ordering
                    service.ServiceVariants = await _context.ServiceVariants
                        .Where(sv => sv.ServiceId == id && sv.IsActive)
                        .OrderBy(sv => sv.DisplayOrder)
                        .ToListAsync();

                    service.ServiceAddons = await _context.ServiceAddons
                        .Where(sa => sa.ServiceId == id && sa.IsActive)
                        .OrderBy(sa => sa.DisplayOrder)
                        .ToListAsync();

                    service.ServiceFAQs = await _context.ServiceFAQs
                        .Where(sf => sf.ServiceId == id && sf.IsActive)
                        .OrderBy(sf => sf.DisplayOrder)
                        .ToListAsync();

                    service.ServiceNotes = await _context.ServiceNotes
                        .Where(sn => sn.ServiceId == id && sn.IsActive)
                        .OrderBy(sn => sn.DisplayOrder)
                        .ToListAsync();

                    // Load and sort ServiceImages - this is critical
                    service.ServiceImages = await _context.ServiceImages
                        .Where(si => si.ServiceId == id)
                        .OrderBy(si => si.DisplayOrder)
                        .ThenBy(si => si.Id)
                        .ToListAsync();
                }

                return service;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting service with details for id {ServiceId}", id);
                return null;
            }
        }

        public async Task<IEnumerable<ServiceVariant>> GetServiceVariantsByServiceIdAsync(int serviceId)
        {
            try
            {
                return await _context.ServiceVariants
                    .Where(sv => sv.ServiceId == serviceId && sv.IsActive)
                    .OrderBy(sv => sv.DisplayOrder)
                    .ThenBy(sv => sv.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting service variants for service {ServiceId}", serviceId);
                return new List<ServiceVariant>();
            }
        }

        public async Task<IEnumerable<ServiceAddon>> GetServiceAddonsByServiceIdAsync(int serviceId)
        {
            try
            {
                return await _context.ServiceAddons
                    .Where(sa => sa.ServiceId == serviceId && sa.IsActive)
                    .OrderBy(sa => sa.DisplayOrder)
                    .ThenBy(sa => sa.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting service addons for service {ServiceId}", serviceId);
                return new List<ServiceAddon>();
            }
        }

        public async Task<IEnumerable<ServiceFAQ>> GetServiceFAQsByServiceIdAsync(int serviceId)
        {
            try
            {
                return await _context.ServiceFAQs
                    .Where(sf => sf.ServiceId == serviceId && sf.IsActive)
                    .OrderBy(sf => sf.DisplayOrder)
                    .ThenBy(sf => sf.Question)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting service FAQs for service {ServiceId}", serviceId);
                return new List<ServiceFAQ>();
            }
        }

        public async Task<IEnumerable<ServiceNote>> GetServiceNotesByServiceIdAsync(int serviceId)
        {
            try
            {
                return await _context.ServiceNotes
                    .Where(sn => sn.ServiceId == serviceId && sn.IsActive)
                    .OrderBy(sn => sn.DisplayOrder)
                    .ThenBy(sn => sn.Title)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting service notes for service {ServiceId}", serviceId);
                return new List<ServiceNote>();
            }
        }

        public async Task<IEnumerable<ServiceImage>> GetServiceImagesByServiceIdAsync(int serviceId)
        {
            try
            {
                return await _context.ServiceImages
                    .Where(si => si.ServiceId == serviceId)
                    .OrderBy(si => si.DisplayOrder)
                    .ThenBy(si => si.Id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting service images for service {ServiceId}", serviceId);
                return new List<ServiceImage>();
            }
        }
    }
} 