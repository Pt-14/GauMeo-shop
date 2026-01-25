using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GauMeo.Data;

namespace GauMeo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Theo yêu cầu: trang chính mặc định là quản lý sản phẩm
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Dashboard()
        {
            // Mock data để tránh lỗi database
            var stats = new
            {
                TotalProducts = 0, // Sẽ thay bằng: await _context.Products.CountAsync(),
                TotalOrders = 0,   // Sẽ thay bằng: await _context.Orders.CountAsync(),
                TotalUsers = 0,    // Sẽ thay bằng: await _context.Users.Where(u => !u.Roles.Any(r => r.RoleId == "Admin")).CountAsync(),
                PendingOrders = 0, // Sẽ thay bằng: await _context.Orders.Where(o => o.Status == "Pending").CountAsync(),
                TodayRevenue = 0   // Sẽ thay bằng: await _context.Orders.Where(...).SumAsync(o => o.TotalAmount)
            };

            ViewBag.Stats = stats;
            ViewData["Title"] = "Dashboard";
            return View("Index", stats);
        }

        public IActionResult Reports()
        {
            ViewData["Title"] = "Báo cáo thống kê";
            
            // Mock data cho báo cáo
            var monthlyRevenue = new[]
            {
                new { Month = 1, Revenue = 5000000 },
                new { Month = 2, Revenue = 7500000 },
                new { Month = 3, Revenue = 6200000 },
                new { Month = 4, Revenue = 8100000 },
                new { Month = 5, Revenue = 9300000 },
                new { Month = 6, Revenue = 7800000 }
            };

            var orderStats = new[]
            {
                new { Status = "Completed", Count = 65 },
                new { Status = "Processing", Count = 20 },
                new { Status = "Pending", Count = 10 },
                new { Status = "Cancelled", Count = 5 }
            };

            ViewBag.MonthlyRevenue = monthlyRevenue;
            ViewBag.OrderStats = orderStats;
            
            return View();
        }
    }
} 