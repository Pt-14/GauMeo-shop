using Microsoft.AspNetCore.Mvc;
using GauMeo.Data;
using GauMeo.Services;
using GauMeo.Models.ViewModels;
using GauMeo.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using GauMeo.Models.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using GauMeo.Models;

namespace GauMeo.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceService _serviceService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServiceController(ApplicationDbContext context, IServiceService serviceService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _serviceService = serviceService;
            _userManager = userManager;
        }

        // NEW DEBUG ACTION
        [HttpGet]
        public async Task<IActionResult> DebugImages()
        {
            var result = new List<object>();
            
            for (int serviceId = 1; serviceId <= 5; serviceId++)
            {
                var images = await _context.ServiceImages
                    .Where(si => si.ServiceId == serviceId)
                    .OrderBy(si => si.DisplayOrder)
                    .ThenBy(si => si.Id)
                    .Select(si => new {
                        si.Id,
                        si.ServiceId,
                        si.Title,
                        si.DisplayOrder,
                        si.IsMain,
                        si.ImageUrl
                    })
                    .ToListAsync();
                
                result.Add(new {
                    ServiceId = serviceId,
                    ImageCount = images.Count,
                    Images = images
                });
            }
            
            return Json(result);
        }

        // NEW RESEED ACTION
        [HttpPost]
        public async Task<IActionResult> ReseedImages()
        {
            try
            {
                // Clear existing images
                var existingImages = await _context.ServiceImages.ToListAsync();
                _context.ServiceImages.RemoveRange(existingImages);
                await _context.SaveChangesAsync();

                // Reseed with correct data
                var serviceImages = new List<Models.Services.ServiceImage>
                {
                    // SERVICE 1: SPA & GROOMING SLIDER IMAGES
                    new() { ImageUrl = "/images/servicepic/spa/spa.jpg", AltText = "Spa Thư Giãn cho thú cưng", IsMain = true, DisplayOrder = 1, ServiceId = 1, Description = "Dịch vụ spa cao cấp giúp thú cưng thư giãn hoàn toàn", Title = "Spa Thư Giãn", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/spa/1.jpg", AltText = "Cắt tỉa lông chuyên nghiệp", IsMain = false, DisplayOrder = 2, ServiceId = 1, Description = "Cắt tỉa lông chuyên nghiệp theo nhiều kiểu dáng hiện đại", Title = "Cắt Tỉa Lông", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/spa/2.jpg", AltText = "Tắm gội cao cấp cho thú cưng", IsMain = false, DisplayOrder = 3, ServiceId = 1, Description = "Sử dụng sản phẩm chăm sóc cao cấp an toàn cho da lông", Title = "Tắm Gội Cao Cấp", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/spa/3.jpg", AltText = "Chăm sóc móng cho thú cưng", IsMain = false, DisplayOrder = 4, ServiceId = 1, Description = "Cắt móng và chăm sóc đầy đủ cho thú cưng", Title = "Chăm Sóc Móng", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/spa/4.jpg", AltText = "Vệ sinh răng miệng thú cưng", IsMain = false, DisplayOrder = 5, ServiceId = 1, Description = "Chăm sóc răng miệng chuyên nghiệp, an toàn", Title = "Vệ Sinh Răng Miệng", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/spa/5.jpg", AltText = "Massage thư giãn cho thú cưng", IsMain = false, DisplayOrder = 6, ServiceId = 1, Description = "Massage chuyên nghiệp giúp giảm căng thẳng", Title = "Massage Thư Giãn", CreatedAt = DateTime.Now },

                    // SERVICE 2: PET HOTEL SLIDER IMAGES
                    new() { ImageUrl = "/images/servicepic/hotel/hotel.jpg", AltText = "Chăm sóc 24/7 cho thú cưng", IsMain = true, DisplayOrder = 1, ServiceId = 2, Description = "Đội ngũ chăm sóc chuyên nghiệp 24/7", Title = "Chăm Sóc 24/7", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/hotel/1.jpg", AltText = "Phòng Standard cho thú cưng", IsMain = false, DisplayOrder = 2, ServiceId = 2, Description = "Phòng ở tiêu chuẩn thoải mái và an toàn", Title = "Phòng Standard", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/hotel/2.jpg", AltText = "Phòng VIP cho thú cưng", IsMain = false, DisplayOrder = 3, ServiceId = 2, Description = "Phòng ở cao cấp với đầy đủ tiện nghi cho thú cưng", Title = "Phòng VIP", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/hotel/3.jpg", AltText = "Khu vui chơi cho thú cưng", IsMain = false, DisplayOrder = 4, ServiceId = 2, Description = "Không gian vui chơi rộng rãi cho thú cưng", Title = "Khu Vui Chơi", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/hotel/4.jpg", AltText = "Bữa ăn dinh dụng cho thú cưng", IsMain = false, DisplayOrder = 5, ServiceId = 2, Description = "Thức ăn dinh dưỡng được chuẩn bị tận tình", Title = "Bữa Ăn Dinh Dưỡng", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/hotel/5.jpg", AltText = "Dịch vụ đưa đón thú cưng", IsMain = false, DisplayOrder = 6, ServiceId = 2, Description = "Dịch vụ đưa đón thú cưng tận nhà", Title = "Đưa Đón Tận Nơi", CreatedAt = DateTime.Now },

                    // SERVICE 3: PET SWIMMING SLIDER IMAGES
                    new() { ImageUrl = "/images/servicepic/pool/pool.jpg", AltText = "Bể bơi chính cho chó", IsMain = true, DisplayOrder = 1, ServiceId = 3, Description = "Bể bơi rộng rãi, nước sạch được thay đổi thường xuyên", Title = "Bể Bơi Chính", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/pool/1.jpg", AltText = "Khu tập bơi cho chó", IsMain = false, DisplayOrder = 2, ServiceId = 3, Description = "Khu vực riêng dành cho tập luyện bơi lội", Title = "Khu Tập Bơi", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/pool/2.jpg", AltText = "Trang thiết bị bơi an toàn", IsMain = false, DisplayOrder = 3, ServiceId = 3, Description = "Đầy đủ trang thiết bị an toàn cho chó bơi", Title = "Trang Thiết Bị", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/pool/3.jpg", AltText = "Liệu pháp nước cho chó", IsMain = false, DisplayOrder = 4, ServiceId = 3, Description = "Liệu pháp phục hồi chức năng trong nước", Title = "Liệu Pháp Nước", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/pool/4.jpg", AltText = "Vệ sinh sau khi bơi", IsMain = false, DisplayOrder = 5, ServiceId = 3, Description = "Tắm rửa và sấy khô hoàn toàn sau khi bơi", Title = "Vệ Sinh Sau Bơi", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/pool/5.jpg", AltText = "Huấn luyện viên bơi chuyên nghiệp", IsMain = false, DisplayOrder = 6, ServiceId = 3, Description = "Đội ngũ huấn luyện viên chuyên nghiệp", Title = "Huấn Luyện Viên", CreatedAt = DateTime.Now },

                    // SERVICE 4: PET DAYCARE SLIDER IMAGES
                    new() { ImageUrl = "/images/servicepic/daycare/daycare.jpg", AltText = "Khu vui chơi daycare", IsMain = true, DisplayOrder = 1, ServiceId = 4, Description = "Không gian vui chơi an toàn và rộng rãi", Title = "Khu Vui Chơi", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/daycare/1.jpg", AltText = "Hoạt động tập thể daycare", IsMain = false, DisplayOrder = 2, ServiceId = 4, Description = "Các hoạt động giao lưu và xã hội hóa", Title = "Hoạt Động Tập Thể", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/daycare/2.jpg", AltText = "Bữa ăn dinh dưỡng daycare", IsMain = false, DisplayOrder = 3, ServiceId = 4, Description = "Chế độ ăn uống cân bằng và bổ dưỡng", Title = "Bữa Ăn Dinh Dưỡng", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/daycare/3.jpg", AltText = "Khu nghỉ ngơi daycare", IsMain = false, DisplayOrder = 4, ServiceId = 4, Description = "Không gian yên tĩnh để nghỉ ngơi thư giãn", Title = "Khu Nghỉ Ngơi", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/daycare/4.jpg", AltText = "Dịch vụ đưa đón daycare", IsMain = false, DisplayOrder = 5, ServiceId = 4, Description = "Dịch vụ đưa đón thuận tiện mỗi ngày", Title = "Đưa Đón Hàng Ngày", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/daycare/5.jpg", AltText = "Giám sát 24/7 daycare", IsMain = false, DisplayOrder = 6, ServiceId = 4, Description = "Hệ thống giám sát và chăm sóc toàn thời gian", Title = "Giám Sát 24/7", CreatedAt = DateTime.Now },

                    // SERVICE 5: PET TRAINING SLIDER IMAGES
                    new() { ImageUrl = "/images/servicepic/train/training.jpg", AltText = "Huấn luyện cơ bản cho thú cưng", IsMain = true, DisplayOrder = 1, ServiceId = 5, Description = "Dạy các kỹ năng cơ bản: ngồi, nằm, đứng", Title = "Huấn Luyện Cơ Bản", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/train/1.jpg", AltText = "Huấn luyện nâng cao cho thú cưng", IsMain = false, DisplayOrder = 2, ServiceId = 5, Description = "Các kỹ năng phức tạp và biểu diễn", Title = "Huấn Luyện Nâng Cao", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/train/2.jpg", AltText = "Sửa hành vi cho thú cưng", IsMain = false, DisplayOrder = 3, ServiceId = 5, Description = "Khắc phục các hành vi không mong muốn", Title = "Sửa Hành Vi", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/train/3.jpg", AltText = "Huấn luyện thể thao cho thú cưng", IsMain = false, DisplayOrder = 4, ServiceId = 5, Description = "Rèn luyện sự nhanh nhẹn và thể lực", Title = "Huấn Luyện Thể Thao", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/train/4.jpg", AltText = "Xã hội hóa cho thú cưng", IsMain = false, DisplayOrder = 5, ServiceId = 5, Description = "Học cách tương tác với người và động vật khác", Title = "Xã Hội Hóa", CreatedAt = DateTime.Now },
                    new() { ImageUrl = "/images/servicepic/train/5.jpg", AltText = "Huấn luyện tại nhà", IsMain = false, DisplayOrder = 6, ServiceId = 5, Description = "Dịch vụ huấn luyện tận nơi tiện lợi", Title = "Huấn Luyện Tại Nhà", CreatedAt = DateTime.Now }
                };

                _context.ServiceImages.AddRange(serviceImages);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "ServiceImages reseeded successfully!", count = serviceImages.Count });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Service/SeedTestData - Thêm dữ liệu test cho ServiceVariants và ServiceAddons
        [HttpPost]
        public async Task<IActionResult> SeedTestData()
        {
            try
            {
                // Clear existing test data
                var existingVariants = await _context.ServiceVariants.ToListAsync();
                var existingAddons = await _context.ServiceAddons.ToListAsync();
                var existingNotes = await _context.ServiceNotes.ToListAsync();
                
                _context.ServiceVariants.RemoveRange(existingVariants);
                _context.ServiceAddons.RemoveRange(existingAddons);
                _context.ServiceNotes.RemoveRange(existingNotes);
                await _context.SaveChangesAsync();

                // Add ServiceVariants for each service
                var serviceVariants = new List<ServiceVariant>
                {
                    // Service 1: Spa & Grooming
                    new() { Name = "Spa Cơ Bản", Description = "Tắm gội, cắt móng, vệ sinh tai", PetType = "both", PetSize = "all", Price = 150000, Duration = "1-2 giờ", IsActive = true, DisplayOrder = 1, ServiceId = 1, CreatedAt = DateTime.Now },
                    new() { Name = "Spa Cao Cấp", Description = "Tắm gội, cắt tỉa, massage, chăm sóc da lông", PetType = "both", PetSize = "all", Price = 250000, Duration = "2-3 giờ", IsActive = true, DisplayOrder = 2, ServiceId = 1, CreatedAt = DateTime.Now },
                    new() { Name = "Spa VIP", Description = "Dịch vụ spa hoàn chỉnh với chăm sóc cao cấp", PetType = "both", PetSize = "all", Price = 350000, Duration = "3-4 giờ", IsActive = true, DisplayOrder = 3, ServiceId = 1, CreatedAt = DateTime.Now },

                    // Service 2: Pet Hotel
                    new() { Name = "Phòng Standard", Description = "Phòng ở cơ bản với đầy đủ tiện nghi", PetType = "both", PetSize = "all", Price = 100000, Duration = "1 đêm", IsActive = true, DisplayOrder = 1, ServiceId = 2, CreatedAt = DateTime.Now },
                    new() { Name = "Phòng Deluxe", Description = "Phòng ở rộng rãi với không gian vui chơi", PetType = "both", PetSize = "all", Price = 150000, Duration = "1 đêm", IsActive = true, DisplayOrder = 2, ServiceId = 2, CreatedAt = DateTime.Now },
                    new() { Name = "Phòng VIP", Description = "Phòng ở cao cấp với chăm sóc đặc biệt", PetType = "both", PetSize = "all", Price = 200000, Duration = "1 đêm", IsActive = true, DisplayOrder = 3, ServiceId = 2, CreatedAt = DateTime.Now },

                    // Service 3: Pet Swimming
                    new() { Name = "Bơi Cơ Bản", Description = "1 buổi bơi với giám sát", PetType = "dog", PetSize = "all", Price = 80000, Duration = "30 phút", IsActive = true, DisplayOrder = 1, ServiceId = 3, CreatedAt = DateTime.Now },
                    new() { Name = "Bơi + Vệ Sinh", Description = "Bơi + tắm rửa và sấy khô", PetType = "dog", PetSize = "all", Price = 120000, Duration = "45 phút", IsActive = true, DisplayOrder = 2, ServiceId = 3, CreatedAt = DateTime.Now },

                    // Service 4: Pet Daycare
                    new() { Name = "Chăm Sóc Nửa Ngày", Description = "4 tiếng chăm sóc và vui chơi", PetType = "both", PetSize = "all", Price = 80000, Duration = "4 tiếng", IsActive = true, DisplayOrder = 1, ServiceId = 4, CreatedAt = DateTime.Now },
                    new() { Name = "Chăm Sóc Cả Ngày", Description = "8 tiếng chăm sóc toàn diện", PetType = "both", PetSize = "all", Price = 150000, Duration = "8 tiếng", IsActive = true, DisplayOrder = 2, ServiceId = 4, CreatedAt = DateTime.Now },

                    // Service 5: Pet Training
                    new() { Name = "Huấn Luyện Cơ Bản", Description = "Dạy các lệnh cơ bản: ngồi, nằm, đứng", PetType = "both", PetSize = "all", Price = 200000, Duration = "1 buổi", IsActive = true, DisplayOrder = 1, ServiceId = 5, CreatedAt = DateTime.Now },
                    new() { Name = "Huấn Luyện Nâng Cao", Description = "Dạy các kỹ năng phức tạp và sửa hành vi", PetType = "both", PetSize = "all", Price = 300000, Duration = "1 buổi", IsActive = true, DisplayOrder = 2, ServiceId = 5, CreatedAt = DateTime.Now }
                };

                _context.ServiceVariants.AddRange(serviceVariants);
                await _context.SaveChangesAsync();

                // Add ServiceAddons
                var serviceAddons = new List<ServiceAddon>
                {
                    // Spa addons
                    new() { Name = "Chăm Sóc Răng Miệng", Description = "Vệ sinh răng miệng chuyên nghiệp", Price = 50000, IsActive = true, DisplayOrder = 1, ServiceId = 1, CreatedAt = DateTime.Now },
                    new() { Name = "Cắt Tỉa Lông Nghệ Thuật", Description = "Tạo kiểu lông theo yêu cầu", Price = 80000, IsActive = true, DisplayOrder = 2, ServiceId = 1, CreatedAt = DateTime.Now },
                    
                    // Hotel addons
                    new() { Name = "Dịch Vụ Đưa Đón", Description = "Đưa đón thú cưng tận nhà", Price = 30000, IsActive = true, DisplayOrder = 1, ServiceId = 2, CreatedAt = DateTime.Now },
                    new() { Name = "Thức Ăn Cao Cấp", Description = "Thức ăn dinh dưỡng cao cấp", Price = 25000, IsActive = true, DisplayOrder = 2, ServiceId = 2, CreatedAt = DateTime.Now },
                    
                    // Swimming addons
                    new() { Name = "Liệu Pháp Nước", Description = "Massage và thư giãn trong nước", Price = 40000, IsActive = true, DisplayOrder = 1, ServiceId = 3, CreatedAt = DateTime.Now },
                    
                    // Daycare addons
                    new() { Name = "Bữa Ăn Bổ Sung", Description = "Bữa ăn dinh dưỡng trong ngày", Price = 20000, IsActive = true, DisplayOrder = 1, ServiceId = 4, CreatedAt = DateTime.Now },
                    new() { Name = "Hoạt Động Đặc Biệt", Description = "Các hoạt động vui chơi đặc biệt", Price = 35000, IsActive = true, DisplayOrder = 2, ServiceId = 4, CreatedAt = DateTime.Now },
                    
                    // Training addons
                    new() { Name = "Video Ghi Lại", Description = "Quay video quá trình huấn luyện", Price = 30000, IsActive = true, DisplayOrder = 1, ServiceId = 5, CreatedAt = DateTime.Now },
                    new() { Name = "Tài Liệu Hướng Dẫn", Description = "Tài liệu hướng dẫn tiếp tục tại nhà", Price = 20000, IsActive = true, DisplayOrder = 2, ServiceId = 5, CreatedAt = DateTime.Now }
                };

                _context.ServiceAddons.AddRange(serviceAddons);
                await _context.SaveChangesAsync();

                // Add ServiceNotes
                var serviceNotes = new List<ServiceNote>
                {
                    // General notes for all services
                    new() { Title = "Lưu Ý Quan Trọng", Content = "Thú cưng cần được tiêm phòng đầy đủ và khỏe mạnh", NoteType = "warning", Icon = "⚠️", IsActive = true, DisplayOrder = 1, ServiceId = 1, CreatedAt = DateTime.Now },
                    new() { Title = "Thời Gian", Content = "Vui lòng đến đúng giờ hẹn để đảm bảo chất lượng dịch vụ", NoteType = "info", Icon = "⏰", IsActive = true, DisplayOrder = 2, ServiceId = 1, CreatedAt = DateTime.Now },
                    
                    new() { Title = "Yêu Cầu Sức Khỏe", Content = "Thú cưng phải có sổ tiêm chủng đầy đủ", NoteType = "warning", Icon = "🏥", IsActive = true, DisplayOrder = 1, ServiceId = 2, CreatedAt = DateTime.Now },
                    
                    new() { Title = "An Toàn Bơi Lội", Content = "Chỉ dành cho chó biết bơi hoặc đã quen với nước", NoteType = "warning", Icon = "🏊", IsActive = true, DisplayOrder = 1, ServiceId = 3, CreatedAt = DateTime.Now },
                    
                    new() { Title = "Độ Tuổi Phù Hợp", Content = "Thú cưng từ 3 tháng tuổi trở lên", NoteType = "info", Icon = "🐾", IsActive = true, DisplayOrder = 1, ServiceId = 4, CreatedAt = DateTime.Now },
                    
                    new() { Title = "Thời Gian Huấn Luyện", Content = "Mỗi buổi huấn luyện kéo dài 45-60 phút", NoteType = "info", Icon = "📚", IsActive = true, DisplayOrder = 1, ServiceId = 5, CreatedAt = DateTime.Now }
                };

                _context.ServiceNotes.AddRange(serviceNotes);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Seed test data successfully!", 
                    variants = serviceVariants.Count, addons = serviceAddons.Count, notes = serviceNotes.Count });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> Index(int serviceId = 1)
        {
            try
            {
                // Get all services from database
                var services = await _serviceService.GetAllActiveServicesAsync();
                
                if (!services.Any())
                {
                    ViewBag.ErrorMessage = "Không có dịch vụ nào được tìm thấy.";
                    return View(new ServiceIndexViewModel());
                }

                // Get detailed data for all services
                var serviceViewModels = new List<ServiceViewModel>();
                
                foreach (var service in services)
                {
                    var serviceWithDetails = await _serviceService.GetServiceWithDetailsAsync(service.Id);
                    if (serviceWithDetails != null)
                    {
                        var serviceViewModel = serviceWithDetails.ToViewModel();
                        
                        // Debug: Log image count for troubleshooting
                        System.Diagnostics.Debug.WriteLine($"Service {service.Id} ({service.Name}): {serviceWithDetails.ServiceImages?.Count ?? 0} images");
                        
                        serviceViewModels.Add(serviceViewModel);
                    }
                }

                // Find current service or default to first
                var currentService = serviceViewModels.FirstOrDefault(s => s.Id == serviceId) 
                                   ?? serviceViewModels.First();

                var viewModel = new ServiceIndexViewModel
                {
                    Services = serviceViewModels,
                    CurrentService = currentService,
                    CurrentServiceId = currentService.Id
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Có lỗi xảy ra khi tải dữ liệu: {ex.Message}";
                return View(new ServiceIndexViewModel());
            }
        }

        #region Service Booking

        // GET: Service/Booking/5
        public async Task<IActionResult> Booking(int id)
        {
            System.Diagnostics.Debug.WriteLine($"=== BOOKING GET - ServiceId: {id} ===");
            
            var service = await _context.Services
                .Include(s => s.ServiceVariants.Where(sv => sv.IsActive))
                .Include(s => s.ServiceAddons.Where(sa => sa.IsActive))
                .Include(s => s.ServiceNotes.Where(sn => sn.IsActive))
                .FirstOrDefaultAsync(s => s.Id == id && s.IsActive);

            if (service == null)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: Service not found with ID {id}");
                TempData["ErrorMessage"] = "Dịch vụ không tồn tại hoặc đã ngừng hoạt động.";
                return RedirectToAction("Index");
            }

            System.Diagnostics.Debug.WriteLine($"Service found: {service.Name}");
            System.Diagnostics.Debug.WriteLine($"  Variants: {service.ServiceVariants?.Count ?? 0}");
            System.Diagnostics.Debug.WriteLine($"  Addons: {service.ServiceAddons?.Count ?? 0}");
            System.Diagnostics.Debug.WriteLine($"  Notes: {service.ServiceNotes?.Count ?? 0}");

            var viewModel = new ServiceBookingViewModel
            {
                ServiceId = service.Id,
                Service = service,
                ServiceVariants = service.ServiceVariants?.OrderBy(sv => sv.DisplayOrder).ToList() ?? new List<ServiceVariant>(),
                ServiceAddons = service.ServiceAddons?.OrderBy(sa => sa.DisplayOrder).ToList() ?? new List<ServiceAddon>(),
                ServiceNotes = service.ServiceNotes?.OrderBy(sn => sn.DisplayOrder).ToList() ?? new List<ServiceNote>()
            };

            // Pre-fill user info if logged in
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    viewModel.CustomerName = user.FullName ?? "";
                    viewModel.CustomerEmail = user.Email ?? "";
                    viewModel.CustomerPhone = user.PhoneNumber ?? "";
                    System.Diagnostics.Debug.WriteLine($"Pre-filled user info: {user.FullName}, {user.Email}");
                }
            }

            System.Diagnostics.Debug.WriteLine($"Booking viewModel created successfully");
            return View(viewModel);
        }

        // POST: Service/Booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Booking(ServiceBookingViewModel model)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== BOOKING POST STARTED ===");
                System.Diagnostics.Debug.WriteLine($"ServiceId: {model.ServiceId}");
                System.Diagnostics.Debug.WriteLine($"CustomerName: '{model.CustomerName}'");
                System.Diagnostics.Debug.WriteLine($"CustomerPhone: '{model.CustomerPhone}'");
                System.Diagnostics.Debug.WriteLine($"PetName: '{model.PetName}'");
                System.Diagnostics.Debug.WriteLine($"PetType: '{model.PetType}'");
                System.Diagnostics.Debug.WriteLine($"ServiceVariantId: {model.ServiceVariantId}");
                System.Diagnostics.Debug.WriteLine($"BookingDate: {model.BookingDate}");
                System.Diagnostics.Debug.WriteLine($"BookingTime: '{model.BookingTime}'");
                System.Diagnostics.Debug.WriteLine($"SpecialRequests: '{model.SpecialRequests}'");
                
                // Validate basic required fields manually to avoid complex ModelState issues
                var errors = new List<string>();
                
                if (string.IsNullOrWhiteSpace(model.CustomerName))
                    errors.Add("Vui lòng nhập họ tên khách hàng");
                
                if (string.IsNullOrWhiteSpace(model.CustomerPhone))
                    errors.Add("Vui lòng nhập số điện thoại");
                
                if (string.IsNullOrWhiteSpace(model.PetName))
                    errors.Add("Vui lòng nhập tên thú cưng");

                if (string.IsNullOrWhiteSpace(model.PetType))
                    errors.Add("Vui lòng chọn loại thú cưng");
                
                if (model.ServiceVariantId <= 0)
                    errors.Add("Vui lòng chọn loại dịch vụ");
                
                if (model.BookingDate == default(DateTime) || model.BookingDate <= DateTime.Today)
                    errors.Add("Vui lòng chọn ngày đặt lịch hợp lệ (từ ngày mai)");
                
                if (string.IsNullOrWhiteSpace(model.BookingTime))
                    errors.Add("Vui lòng chọn giờ đặt lịch");

                // If we have validation errors, reload data and return
                if (errors.Any())
                {
                    System.Diagnostics.Debug.WriteLine($"Validation errors: {string.Join(", ", errors)}");
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                        await LoadBookingData(model);
                        return View(model);
                    }
                    
                // Get service and variant - with enhanced validation
                var service = await _context.Services.FindAsync(model.ServiceId);
                var variant = await _context.ServiceVariants.FindAsync(model.ServiceVariantId);

                if (service == null || variant == null)
                {
                    ModelState.AddModelError("", "Dịch vụ hoặc loại dịch vụ không tồn tại.");
                    await LoadBookingData(model);
                    return View(model);
                }

                // Validate pet type matches service variant
                if (variant.PetType != "both" && variant.PetType != model.PetType)
                {
                    var petTypeName = model.PetType == "dog" ? "chó" : "mèo";
                    var variantPetTypeName = variant.PetType == "dog" ? "chó" : "mèo";
                    errors.Add($"Dịch vụ '{variant.Name}' chỉ dành cho {variantPetTypeName}, nhưng bạn đã chọn {petTypeName}.");
                    
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    await LoadBookingData(model);
                    return View(model);
                }

                // Validate pet size if specified
                if (!string.IsNullOrEmpty(variant.PetSize) && variant.PetSize != "all" && 
                    !string.IsNullOrEmpty(model.PetSize) && variant.PetSize != model.PetSize)
                {
                    var sizeNames = new Dictionary<string, string>
                    {
                        {"small", "nhỏ (< 5kg)"},
                        {"medium", "trung bình (5-15kg)"},
                        {"large", "lớn (> 15kg)"}
                    };
                    
                    var variantSizeName = sizeNames.ContainsKey(variant.PetSize) ? sizeNames[variant.PetSize] : variant.PetSize;
                    var petSizeName = sizeNames.ContainsKey(model.PetSize) ? sizeNames[model.PetSize] : model.PetSize;
                    
                    errors.Add($"Dịch vụ '{variant.Name}' chỉ dành cho thú cưng kích thước {variantSizeName}, nhưng bạn đã chọn {petSizeName}.");
                    
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    await LoadBookingData(model);
                    return View(model);
                }

                // Calculate total price
                decimal totalPrice = variant.Price;
                if (model.SelectedAddonIds?.Any() == true)
                {
                    var selectedAddons = await _context.ServiceAddons
                        .Where(sa => model.SelectedAddonIds.Contains(sa.Id))
                        .ToListAsync();
                totalPrice += selectedAddons.Sum(sa => sa.Price);
                }

                // Create booking - simplified
                var booking = new ServiceBooking
                {
                    ServiceId = model.ServiceId,
                    ServiceVariantId = model.ServiceVariantId,
                    CustomerName = model.CustomerName.Trim(),
                    CustomerPhone = model.CustomerPhone.Trim(),
                    CustomerEmail = string.IsNullOrWhiteSpace(model.CustomerEmail) ? null : model.CustomerEmail.Trim(),
                    PetName = model.PetName.Trim(),
                    PetType = model.PetType,
                    PetBreed = string.IsNullOrWhiteSpace(model.PetBreed) ? null : model.PetBreed.Trim(),
                    PetSize = string.IsNullOrWhiteSpace(model.PetSize) ? null : model.PetSize,
                    BookingDate = model.BookingDate,
                    BookingTime = model.BookingTime,
                    SpecialRequests = string.IsNullOrWhiteSpace(model.SpecialRequests) ? null : model.SpecialRequests.Trim(),
                    EstimatedPrice = totalPrice,
                    Status = "Pending",
                    UserId = User.Identity?.IsAuthenticated == true ? User.FindFirstValue(ClaimTypes.NameIdentifier) : null,
                    CreatedAt = DateTime.Now
                };

                System.Diagnostics.Debug.WriteLine("Saving booking to database...");
                _context.ServiceBookings.Add(booking);
                await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine($"Booking saved with ID: {booking.Id}");

                // Add selected addons if any
                if (model.SelectedAddonIds?.Any() == true)
                {
                    var selectedAddons = await _context.ServiceAddons
                        .Where(sa => model.SelectedAddonIds.Contains(sa.Id))
                        .ToListAsync();

                    var bookingAddons = selectedAddons.Select(addon => new ServiceBookingAddon
                    {
                        ServiceBookingId = booking.Id,
                        ServiceAddonId = addon.Id,
                        Price = addon.Price,
                        CreatedAt = DateTime.Now
                    }).ToList();

                    _context.ServiceBookingAddons.AddRange(bookingAddons);
                    await _context.SaveChangesAsync();
                    System.Diagnostics.Debug.WriteLine($"Added {bookingAddons.Count} booking addons");
                }

                System.Diagnostics.Debug.WriteLine("=== BOOKING SUCCESS ===");
                TempData["SuccessMessage"] = $"Đặt lịch thành công! Mã đặt lịch của bạn là #{booking.Id:D6}. Chúng tôi sẽ liên hệ xác nhận trong thời gian sớm nhất.";
                return RedirectToAction("BookingSuccess", new { id = booking.Id });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"=== BOOKING ERROR ===");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                ModelState.AddModelError("", "Có lỗi xảy ra khi đặt lịch. Vui lòng thử lại sau.");
                await LoadBookingData(model);
                return View(model);
            }
        }

        // GET: Service/BookingSuccess/5
        public async Task<IActionResult> BookingSuccess(int id)
        {
            var booking = await _context.ServiceBookings
                .Include(sb => sb.Service)
                .Include(sb => sb.ServiceVariant)
                .FirstOrDefaultAsync(sb => sb.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // Helper method to load booking data
        private async Task LoadBookingData(ServiceBookingViewModel model)
        {
            var service = await _context.Services
                .Include(s => s.ServiceVariants.Where(sv => sv.IsActive))
                .Include(s => s.ServiceAddons.Where(sa => sa.IsActive))
                .Include(s => s.ServiceNotes.Where(sn => sn.IsActive))
                .FirstOrDefaultAsync(s => s.Id == model.ServiceId);

            if (service != null)
            {
                model.Service = service;
                model.ServiceVariants = service.ServiceVariants?.OrderBy(sv => sv.DisplayOrder).ToList() ?? new List<ServiceVariant>();
                model.ServiceAddons = service.ServiceAddons?.OrderBy(sa => sa.DisplayOrder).ToList() ?? new List<ServiceAddon>();
                model.ServiceNotes = service.ServiceNotes?.OrderBy(sn => sn.DisplayOrder).ToList() ?? new List<ServiceNote>();
            }
        }

        // API Endpoints for AJAX
        [HttpGet]
        public async Task<IActionResult> GetVariantDetails(int variantId)
        {
            var variant = await _context.ServiceVariants.FindAsync(variantId);
            if (variant == null)
            {
                return NotFound();
            }

            return Json(new
            {
                id = variant.Id,
                name = variant.Name,
                description = variant.Description,
                price = variant.Price,
                duration = variant.Duration,
                petType = variant.PetType,
                petSize = variant.PetSize
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAddonDetails(int[] addonIds)
        {
            var addons = await _context.ServiceAddons
                .Where(sa => addonIds.Contains(sa.Id))
                .Select(sa => new
                {
                    id = sa.Id,
                    name = sa.Name,
                    description = sa.Description,
                    price = sa.Price
                })
                .ToListAsync();

            return Json(addons);
        }

        #endregion
    }
}
