using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GauMeo.Data;
using GauMeo.Models.Services;
using GauMeo.Models.ViewModels;

namespace GauMeo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Service
        public async Task<IActionResult> Index()
        {
            var services = await _context.Services
                .Include(s => s.ServiceImages)
                .Include(s => s.ServiceVariants)
                .Include(s => s.ServiceAddons)
                .Include(s => s.ServiceFAQs)
                .Include(s => s.ServiceNotes)
                .Include(s => s.ServiceBookings)
                .OrderBy(s => s.DisplayOrder)
                .ToListAsync();

            return View(services);
        }

        // GET: Admin/Service/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceImages)
                .Include(s => s.ServiceVariants)
                .Include(s => s.ServiceAddons)
                .Include(s => s.ServiceFAQs)
                .Include(s => s.ServiceNotes)
                .Include(s => s.ServiceBookings)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Admin/Service/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Service/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ShortName,Description,FullDescription,Price,MinPrice,MaxPrice,Duration,Image,FaqImage,Features,IsActive,IsFeatured,DisplayOrder")] Service service)
        {
            if (ModelState.IsValid)
            {
                service.CreatedAt = DateTime.Now;
                service.UpdatedAt = DateTime.Now;
                _context.Add(service);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Dịch vụ đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Admin/Service/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Admin/Service/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortName,Description,FullDescription,Price,MinPrice,MaxPrice,Duration,Image,FaqImage,Features,IsActive,IsFeatured,DisplayOrder,CreatedAt")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.UpdatedAt = DateTime.Now;
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Dịch vụ đã được cập nhật thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Admin/Service/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceImages)
                .Include(s => s.ServiceVariants)
                .Include(s => s.ServiceAddons)
                .Include(s => s.ServiceFAQs)
                .Include(s => s.ServiceNotes)
                .Include(s => s.ServiceBookings)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Admin/Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Dịch vụ đã được xóa thành công!";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Service/ToggleStatus/5
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return Json(new { success = false, message = "Không tìm thấy dịch vụ" });
            }

            service.IsActive = !service.IsActive;
            service.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Json(new { 
                success = true, 
                isActive = service.IsActive,
                message = service.IsActive ? "Đã kích hoạt dịch vụ" : "Đã tạm dừng dịch vụ"
            });
        }

        // POST: Admin/Service/BulkDelete
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] int[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                return Json(new { success = false, message = "Không có dịch vụ nào được chọn" });
            }

            var services = await _context.Services
                .Where(s => ids.Contains(s.Id))
                .ToListAsync();

            _context.Services.RemoveRange(services);
            await _context.SaveChangesAsync();

            return Json(new { 
                success = true, 
                message = $"Đã xóa {services.Count} dịch vụ thành công" 
            });
        }

        #region Service Images Management

        // GET: Admin/Service/Images/5
        public async Task<IActionResult> Images(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceImages)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Admin/Service/AddImage
        [HttpPost]
        public async Task<IActionResult> AddImage(int serviceId, string imageUrl, string altText, string title, string description, bool isMain, int displayOrder)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null)
            {
                return Json(new { success = false, message = "Không tìm thấy dịch vụ" });
            }

            var serviceImage = new ServiceImage
            {
                ServiceId = serviceId,
                ImageUrl = imageUrl,
                AltText = altText,
                Title = title,
                Description = description,
                IsMain = isMain,
                DisplayOrder = displayOrder,
                CreatedAt = DateTime.Now
            };

            _context.ServiceImages.Add(serviceImage);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã thêm hình ảnh thành công" });
        }

        // DELETE: Admin/Service/DeleteImage/5
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.ServiceImages.FindAsync(id);
            if (image == null)
            {
                return Json(new { success = false, message = "Không tìm thấy hình ảnh" });
            }

            _context.ServiceImages.Remove(image);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã xóa hình ảnh thành công" });
        }

        #endregion

        #region Service Variants Management

        // GET: Admin/Service/Variants/5
        public async Task<IActionResult> Variants(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceVariants)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Admin/Service/AddVariant
        [HttpPost]
        public async Task<IActionResult> AddVariant(int serviceId, string name, string description, string petType, string petSize, decimal price, string duration, bool isActive, int displayOrder)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null)
            {
                return Json(new { success = false, message = "Không tìm thấy dịch vụ" });
            }

            var serviceVariant = new ServiceVariant
            {
                ServiceId = serviceId,
                Name = name,
                Description = description,
                PetType = petType,
                PetSize = petSize,
                Price = price,
                Duration = duration,
                IsActive = isActive,
                DisplayOrder = displayOrder,
                CreatedAt = DateTime.Now
            };

            _context.ServiceVariants.Add(serviceVariant);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã thêm biến thể dịch vụ thành công" });
        }

        // DELETE: Admin/Service/DeleteVariant/5
        [HttpPost]
        public async Task<IActionResult> DeleteVariant(int id)
        {
            var variant = await _context.ServiceVariants.FindAsync(id);
            if (variant == null)
            {
                return Json(new { success = false, message = "Không tìm thấy biến thể" });
            }

            _context.ServiceVariants.Remove(variant);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã xóa biến thể thành công" });
        }

        #endregion

        #region Service Addons Management

        // GET: Admin/Service/Addons/5
        public async Task<IActionResult> Addons(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceAddons)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Admin/Service/AddAddon
        [HttpPost]
        public async Task<IActionResult> AddAddon(int serviceId, string name, decimal price, string description, bool isActive, int displayOrder)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null)
            {
                return Json(new { success = false, message = "Không tìm thấy dịch vụ" });
            }

            var serviceAddon = new ServiceAddon
            {
                ServiceId = serviceId,
                Name = name,
                Price = price,
                Description = description,
                IsActive = isActive,
                DisplayOrder = displayOrder,
                CreatedAt = DateTime.Now
            };

            _context.ServiceAddons.Add(serviceAddon);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã thêm addon thành công" });
        }

        // DELETE: Admin/Service/DeleteAddon/5
        [HttpPost]
        public async Task<IActionResult> DeleteAddon(int id)
        {
            var addon = await _context.ServiceAddons.FindAsync(id);
            if (addon == null)
            {
                return Json(new { success = false, message = "Không tìm thấy addon" });
            }

            _context.ServiceAddons.Remove(addon);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã xóa addon thành công" });
        }

        #endregion

        #region Service FAQs Management

        // GET: Admin/Service/FAQs/5
        public async Task<IActionResult> FAQs(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceFAQs)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Admin/Service/AddFAQ
        [HttpPost]
        public async Task<IActionResult> AddFAQ(int serviceId, string question, string answer, bool isActive, int displayOrder)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null)
            {
                return Json(new { success = false, message = "Không tìm thấy dịch vụ" });
            }

            var serviceFAQ = new ServiceFAQ
            {
                ServiceId = serviceId,
                Question = question,
                Answer = answer,
                IsActive = isActive,
                DisplayOrder = displayOrder,
                CreatedAt = DateTime.Now
            };

            _context.ServiceFAQs.Add(serviceFAQ);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã thêm FAQ thành công" });
        }

        // DELETE: Admin/Service/DeleteFAQ/5
        [HttpPost]
        public async Task<IActionResult> DeleteFAQ(int id)
        {
            var faq = await _context.ServiceFAQs.FindAsync(id);
            if (faq == null)
            {
                return Json(new { success = false, message = "Không tìm thấy FAQ" });
            }

            _context.ServiceFAQs.Remove(faq);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã xóa FAQ thành công" });
        }

        #endregion

        #region Service Notes Management

        // GET: Admin/Service/Notes/5
        public async Task<IActionResult> Notes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceNotes)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Admin/Service/AddNote
        [HttpPost]
        public async Task<IActionResult> AddNote(int serviceId, string title, string content, string icon, string noteType, bool isActive, int displayOrder)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null)
            {
                return Json(new { success = false, message = "Không tìm thấy dịch vụ" });
            }

            var serviceNote = new ServiceNote
            {
                ServiceId = serviceId,
                Title = title,
                Content = content,
                Icon = icon,
                NoteType = noteType,
                IsActive = isActive,
                DisplayOrder = displayOrder,
                CreatedAt = DateTime.Now
            };

            _context.ServiceNotes.Add(serviceNote);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã thêm ghi chú thành công" });
        }

        // DELETE: Admin/Service/DeleteNote/5
        [HttpPost]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _context.ServiceNotes.FindAsync(id);
            if (note == null)
            {
                return Json(new { success = false, message = "Không tìm thấy ghi chú" });
            }

            _context.ServiceNotes.Remove(note);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã xóa ghi chú thành công" });
        }

        #endregion

        #region Service Booking Management

        // GET: Admin/Service/Bookings
        public async Task<IActionResult> Bookings(ServiceBookingFilters filters)
        {
            var query = _context.ServiceBookings
                .Include(b => b.Service)
                .Include(b => b.ServiceVariant)
                .Include(b => b.ServiceBookingAddons)
                    .ThenInclude(ba => ba.ServiceAddon)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filters.SearchTerm))
            {
                query = query.Where(b =>
                    b.CustomerName.Contains(filters.SearchTerm) ||
                    b.CustomerPhone.Contains(filters.SearchTerm) ||
                    b.CustomerEmail.Contains(filters.SearchTerm) ||
                    b.PetName.Contains(filters.SearchTerm)
                );
            }

            if (!string.IsNullOrEmpty(filters.Status))
            {
                query = query.Where(b => b.Status == filters.Status);
            }

            if (filters.FromDate.HasValue)
            {
                query = query.Where(b => b.BookingDate >= filters.FromDate.Value.Date);
            }

            if (filters.ToDate.HasValue)
            {
                query = query.Where(b => b.BookingDate <= filters.ToDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(filters.ServiceId) && int.TryParse(filters.ServiceId, out int serviceId))
            {
                query = query.Where(b => b.ServiceId == serviceId);
            }

            if (!string.IsNullOrEmpty(filters.PetType))
            {
                query = query.Where(b => b.PetType == filters.PetType);
            }

            // Apply sorting
            query = filters.SortBy?.ToLower() switch
            {
                "date" => filters.SortOrder?.ToLower() == "desc" 
                    ? query.OrderByDescending(b => b.BookingDate)
                    : query.OrderBy(b => b.BookingDate),
                "status" => filters.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(b => b.Status)
                    : query.OrderBy(b => b.Status),
                "customer" => filters.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(b => b.CustomerName)
                    : query.OrderBy(b => b.CustomerName),
                _ => query.OrderByDescending(b => b.BookingDate)
            };

            // Calculate statistics
            var allBookings = await _context.ServiceBookings.ToListAsync();
            var statistics = new ServiceBookingStatistics
            {
                TotalBookings = allBookings.Count,
                PendingBookings = allBookings.Count(b => b.Status == "Pending"),
                ConfirmedBookings = allBookings.Count(b => b.Status == "Confirmed"),
                InProgressBookings = allBookings.Count(b => b.Status == "InProgress"),
                CompletedBookings = allBookings.Count(b => b.Status == "Completed"),
                CancelledBookings = allBookings.Count(b => b.Status == "Cancelled"),
                TotalRevenue = allBookings.Sum(b => b.EstimatedPrice),
                PendingRevenue = allBookings.Where(b => b.Status == "Pending").Sum(b => b.EstimatedPrice),
                CompletedRevenue = allBookings.Where(b => b.Status == "Completed").Sum(b => b.EstimatedPrice),
                TodayBookings = allBookings.Count(b => b.BookingDate.Date == DateTime.Today),
                WeekBookings = allBookings.Count(b => b.BookingDate.Date >= DateTime.Today.AddDays(-7)),
                MonthBookings = allBookings.Count(b => b.BookingDate.Date >= DateTime.Today.AddMonths(-1))
            };

            // Apply pagination
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)filters.PageSize);
            filters.PageNumber = Math.Max(1, Math.Min(filters.PageNumber, totalPages));

            var bookings = await query
                .Skip((filters.PageNumber - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .Select(b => new ServiceBookingListItem
                {
                    Id = b.Id,
                    CustomerName = b.CustomerName,
                    CustomerPhone = b.CustomerPhone,
                    CustomerEmail = b.CustomerEmail ?? "",
                    PetName = b.PetName,
                    PetType = b.PetType,
                    PetBreed = b.PetBreed ?? "",
                    PetSize = b.PetSize ?? "",
                    ServiceName = b.Service.Name,
                    VariantName = b.ServiceVariant.Name,
                    BookingDate = b.BookingDate,
                    BookingTime = b.BookingTime,
                    EstimatedPrice = b.EstimatedPrice,
                    Status = b.Status,
                    Notes = b.Notes ?? "",
                    CreatedAt = b.CreatedAt,
                    ConfirmedAt = b.ConfirmedAt,
                    CompletedAt = b.CompletedAt,
                    AddonsNames = b.ServiceBookingAddons.Select(ba => ba.ServiceAddon.Name).ToList()
                })
                .ToListAsync();

            var viewModel = new ServiceBookingManagementViewModel
            {
                Bookings = bookings,
                Statistics = statistics,
                Filters = filters
            };

            return View(viewModel);
        }

        // GET: Admin/Service/BookingDetails/5
        public async Task<IActionResult> BookingDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.ServiceBookings
                .Include(b => b.Service)
                .Include(b => b.ServiceVariant)
                .Include(b => b.ServiceBookingAddons)
                    .ThenInclude(ba => ba.ServiceAddon)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Admin/Service/UpdateBookingStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBookingStatus(int id, string status, string? notes)
        {
            var booking = await _context.ServiceBookings.FindAsync(id);
            if (booking == null)
            {
                return Json(new { success = false, message = "Không tìm thấy lịch đặt" });
            }

            // Validate status transition
            if (!IsValidStatusTransition(booking.Status, status))
            {
                return Json(new { success = false, message = "Trạng thái không hợp lệ" });
            }

            booking.Status = status;
            if (!string.IsNullOrEmpty(notes))
            {
                booking.Notes = notes;
            }

            // Update timestamps based on status
            switch (status)
            {
                case "Confirmed":
                    booking.ConfirmedAt = DateTime.Now;
                    break;
                case "Completed":
                    booking.CompletedAt = DateTime.Now;
                    break;
            }

            try
            {
                await _context.SaveChangesAsync();
                return Json(new { 
                    success = true, 
                    message = "Đã cập nhật trạng thái",
                    status = status,
                    confirmedAt = booking.ConfirmedAt?.ToString("dd/MM/yyyy HH:mm"),
                    completedAt = booking.CompletedAt?.ToString("dd/MM/yyyy HH:mm")
                });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật trạng thái" });
            }
        }

        // GET: Admin/Service/Calendar
        public async Task<IActionResult> Calendar()
        {
            var bookings = await _context.ServiceBookings
                .Include(b => b.Service)
                .Include(b => b.ServiceVariant)
                .Where(b => b.Status != "Cancelled")
                .Select(b => new
                {
                    id = b.Id,
                    title = $"{b.Service.Name} - {b.CustomerName}",
                    start = $"{b.BookingDate.ToString("yyyy-MM-dd")}T{b.BookingTime}",
                    end = $"{b.BookingDate.ToString("yyyy-MM-dd")}T{CalculateEndTime(b.BookingTime, b.ServiceVariant.Duration)}",
                    description = $"Khách hàng: {b.CustomerName}\nSDT: {b.CustomerPhone}\nDịch vụ: {b.Service.Name} - {b.ServiceVariant.Name}",
                    status = b.Status,
                    className = GetEventClassName(b.Status)
                })
                .ToListAsync();

            return View(bookings);
        }

        private bool IsValidStatusTransition(string currentStatus, string newStatus)
        {
            return (currentStatus, newStatus) switch
            {
                ("Pending", "Confirmed") => true,
                ("Pending", "Cancelled") => true,
                ("Confirmed", "InProgress") => true,
                ("Confirmed", "Cancelled") => true,
                ("InProgress", "Completed") => true,
                ("InProgress", "Cancelled") => true,
                _ => false
            };
        }

        private static string GetEventClassName(string status)
        {
            return status switch
            {
                "Pending" => "event-pending",
                "Confirmed" => "event-confirmed",
                "InProgress" => "event-in-progress",
                "Completed" => "event-completed",
                "Cancelled" => "event-cancelled",
                _ => "event-default"
            };
        }

        private static string CalculateEndTime(string startTime, string duration)
        {
            if (!TimeSpan.TryParse(startTime, out TimeSpan start))
                return startTime;

            // Parse duration (assuming format like "60 phút" or "90 phút")
            if (!int.TryParse(duration.Split(' ')[0], out int minutes))
                minutes = 60; // Default to 60 minutes if parsing fails

            return start.Add(TimeSpan.FromMinutes(minutes)).ToString(@"hh\:mm");
        }

        #endregion

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
} 