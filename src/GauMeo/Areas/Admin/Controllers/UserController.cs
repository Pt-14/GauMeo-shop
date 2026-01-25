using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GauMeo.Data;
using GauMeo.Models;
using GauMeo.Models.ViewModels;

namespace GauMeo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Admin/User
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Quản lý người dùng";
            
            var users = await _userManager.Users
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            var userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    User = user,
                    Roles = roles.ToList()
                });
            }

            return View(userViewModels);
        }

        // GET: Admin/User/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            var orders = await _context.Orders
                .Where(o => o.UserId == id)
                                        .OrderByDescending(o => o.CreatedAt)
                .Take(10)
                .ToListAsync();

            ViewData["Title"] = $"Chi tiết người dùng: {user.FullName ?? user.UserName}";
            ViewBag.UserRoles = roles;
            ViewBag.RecentOrders = orders;
            
            return View(user);
        }

        // GET: Admin/User/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Tạo tài khoản quản trị viên";
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra email đã tồn tại
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng");
                    return View(model);
                }

                // Kiểm tra username đã tồn tại
                var existingUsername = await _userManager.FindByNameAsync(model.UserName);
                if (existingUsername != null)
                {
                    ModelState.AddModelError("UserName", "Tên đăng nhập đã được sử dụng");
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    EmailConfirmed = true, // Admin được xác thực email ngay
                    CreatedAt = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    // Đảm bảo role Admin tồn tại
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                    // Gán role Admin cho user mới
                    await _userManager.AddToRoleAsync(user, "Admin");

                    TempData["SuccessMessage"] = $"Đã tạo tài khoản quản trị viên {user.FullName} thành công!";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewData["Title"] = "Tạo tài khoản quản trị viên";
            return View(model);
        }

        // GET: Admin/User/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Chỉ cho phép edit admin
            var userRoles = await _userManager.GetRolesAsync(user);
            if (!userRoles.Contains("Admin"))
            {
                TempData["ErrorMessage"] = "Chỉ có thể chỉnh sửa tài khoản quản trị viên!";
                return RedirectToAction(nameof(Index));
            }

            var model = new EditAdminViewModel
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                FullName = user.FullName ?? string.Empty,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };

            ViewData["Title"] = $"Chỉnh sửa quản trị viên: {user.FullName ?? user.UserName}";
            return View(model);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditAdminViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) return NotFound();

                // Chỉ cho phép edit admin
                var userRoles = await _userManager.GetRolesAsync(user);
                if (!userRoles.Contains("Admin"))
                {
                    TempData["ErrorMessage"] = "Chỉ có thể chỉnh sửa tài khoản quản trị viên!";
                    return RedirectToAction(nameof(Index));
                }

                // Kiểm tra email trùng (trừ user hiện tại)
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null && existingUser.Id != user.Id)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng");
                    ViewData["Title"] = $"Chỉnh sửa quản trị viên: {user.FullName ?? user.UserName}";
                    return View(model);
                }

                // Cập nhật thông tin
                user.Email = model.Email;
                user.FullName = model.FullName;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;

                var result = await _userManager.UpdateAsync(user);
                
                if (result.Succeeded)
                {
                    // Cập nhật mật khẩu nếu có
                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        await _userManager.ResetPasswordAsync(user, token, model.Password);
                    }

                    TempData["SuccessMessage"] = $"Đã cập nhật thông tin quản trị viên {user.FullName} thành công!";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewData["Title"] = $"Chỉnh sửa quản trị viên: {model.FullName ?? model.UserName}";
            return View(model);
        }

        // POST: Admin/User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Không cho phép xóa chính mình
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || currentUser.Id == user.Id)
            {
                if (currentUser == null)
                    return Json(new { success = false, message = "Không tìm thấy người dùng hiện tại!" });
                return Json(new { success = false, message = "Không thể xóa tài khoản của chính mình!" });
            }

            try
            {
                var result = await _userManager.DeleteAsync(user);
                
                if (result.Succeeded)
                {
                    return Json(new { success = true, message = $"Đã xóa người dùng {user.FullName ?? user.UserName ?? "N/A"} thành công!" });
                }
                else
                {
                    return Json(new { success = false, message = "Có lỗi xảy ra khi xóa người dùng!" });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Không thể xóa người dùng do có dữ liệu liên quan!" });
            }
        }

        // POST: Admin/User/ToggleStatus
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Không cho phép khóa chính mình
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || currentUser.Id == user.Id)
            {
                if (currentUser == null)
                    return Json(new { success = false, message = "Không tìm thấy người dùng hiện tại!" });
                return Json(new { success = false, message = "Không thể khóa tài khoản của chính mình!" });
            }

            try
            {
                if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.Now)
                {
                    // Mở khóa
                    user.LockoutEnd = null;
                    var message = $"Đã mở khóa tài khoản {user.FullName ?? user.UserName ?? "N/A"}";
                    await _userManager.UpdateAsync(user);
                    return Json(new { success = true, message = message, isLocked = false });
                }
                else
                {
                    // Khóa
                    user.LockoutEnd = DateTimeOffset.MaxValue;
                    var message = $"Đã khóa tài khoản {user.FullName ?? user.UserName ?? "N/A"}";
                    await _userManager.UpdateAsync(user);
                    return Json(new { success = true, message = message, isLocked = true });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật trạng thái!" });
            }
        }
    }
} 