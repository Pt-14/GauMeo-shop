using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GauMeo.Data;
using GauMeo.Models.Promotions;

namespace GauMeo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PromotionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PromotionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Promotion
        public IActionResult Index()
        {
            ViewData["Title"] = "Quản lý khuyến mãi";
            
            // Using improved mock data for demo purposes
            var mockPromotions = GetMockPromotions();
            return View(mockPromotions);
        }

        // GET: Admin/Promotion/Active
        public IActionResult Active()
        {
            ViewData["Title"] = "Khuyến mãi đang chạy";
            
            var activePromotions = new List<Promotion>
            {
                new Promotion { Id = 1, Name = "Giảm giá mùa hè", Description = "Giảm 30% cho tất cả sản phẩm", DiscountPercentage = 30, StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(20), IsActive = true, CreatedAt = DateTime.Now.AddDays(-15) },
                new Promotion { Id = 2, Name = "Khuyến mãi Black Friday", Description = "Giảm 50% tối đa 500.000đ", DiscountPercentage = 50, StartDate = DateTime.Now.AddDays(-5), EndDate = DateTime.Now.AddDays(5), IsActive = true, CreatedAt = DateTime.Now.AddDays(-10) }
            };

            return View(activePromotions);
        }

        // GET: Admin/Promotion/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Tạo khuyến mãi mới";
            return View();
        }

        // POST: Admin/Promotion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                // Mock action - in real app, save to database
                TempData["SuccessMessage"] = "Đã tạo khuyến mãi mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(promotion);
        }

        // GET: Admin/Promotion/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var promotion = GetMockPromotions().FirstOrDefault(p => p.Id == id);
            if (promotion == null) return NotFound();

            ViewData["Title"] = $"Chỉnh sửa khuyến mãi: {promotion.Name}";
            return View(promotion);
        }

        // POST: Admin/Promotion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Promotion promotion)
        {
            if (id != promotion.Id) return NotFound();

            if (ModelState.IsValid)
            {
                // Mock update - in real app, update in database
                TempData["SuccessMessage"] = "Cập nhật khuyến mãi thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(promotion);
        }

        // GET: Admin/Promotion/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var promotion = GetMockPromotions().FirstOrDefault(p => p.Id == id);
            if (promotion == null) return NotFound();

            ViewData["Title"] = $"Chi tiết khuyến mãi: {promotion.Name}";
            return View(promotion);
        }

        // GET: Admin/Promotion/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var promotion = GetMockPromotions().FirstOrDefault(p => p.Id == id);
            if (promotion == null) return NotFound();

            ViewData["Title"] = $"Xóa khuyến mãi: {promotion.Name}";
            return View(promotion);
        }

        // POST: Admin/Promotion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Mock delete - in real app, delete from database
            TempData["SuccessMessage"] = "Xóa khuyến mãi thành công!";
            return RedirectToAction(nameof(Index));
        }

        // Mock data for demonstration
        private List<Promotion> GetMockPromotions()
        {
            return new List<Promotion>
            {
                new Promotion 
                { 
                    Id = 1, 
                    Name = "Giảm giá mùa hề", 
                    Description = "Giảm 30% cho tất cả sản phẩm mùa hè", 
                    DiscountPercentage = 30, 
                    StartDate = new DateTime(2025, 6, 9), 
                    EndDate = new DateTime(2025, 7, 9), 
                    IsActive = true, 
                    CreatedAt = DateTime.Now.AddDays(-15),
                    UpdatedAt = DateTime.Now.AddDays(-2)
                },
                new Promotion 
                { 
                    Id = 2, 
                    Name = "Khuyến mãi Black Friday", 
                    Description = "Giảm 50% tối đa 500.000đ cho đơn hàng từ 1.000.000đ", 
                    DiscountPercentage = 50, 
                    StartDate = new DateTime(2025, 6, 14), 
                    EndDate = new DateTime(2025, 6, 24), 
                    IsActive = true, 
                    CreatedAt = DateTime.Now.AddDays(-10),
                    UpdatedAt = DateTime.Now.AddDays(-1)
                },
                new Promotion 
                { 
                    Id = 3, 
                    Name = "Ưu đãi khách hàng mới", 
                    Description = "Giảm 20% cho khách hàng đăng ký lần đầu tiên", 
                    DiscountPercentage = 20, 
                    StartDate = new DateTime(2025, 5, 20), 
                    EndDate = new DateTime(2025, 8, 18), 
                    IsActive = true, 
                    CreatedAt = DateTime.Now.AddDays(-35),
                    UpdatedAt = DateTime.Now.AddDays(-5)
                },
                new Promotion 
                { 
                    Id = 4, 
                    Name = "Flash Sale cuối tuần", 
                    Description = "Giảm 40% trong 2 ngày cuối tuần", 
                    DiscountPercentage = 40, 
                    StartDate = new DateTime(2025, 6, 17), 
                    EndDate = new DateTime(2025, 6, 18), 
                    IsActive = false, 
                    CreatedAt = DateTime.Now.AddDays(-7),
                    UpdatedAt = DateTime.Now.AddDays(-1)
                },
                new Promotion 
                { 
                    Id = 5, 
                    Name = "Combo thú cưng", 
                    Description = "Mua 2 tặng 1 cho phụ kiện thú cưng", 
                    DiscountPercentage = 33, 
                    StartDate = new DateTime(2025, 6, 24), 
                    EndDate = new DateTime(2025, 7, 4), 
                    IsActive = true, 
                    CreatedAt = DateTime.Now.AddDays(-3),
                    UpdatedAt = DateTime.Now
                }
            };
        }
    }
} 