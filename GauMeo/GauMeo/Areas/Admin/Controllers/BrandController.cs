using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GauMeo.Services;
using GauMeo.Models.Categories;

namespace GauMeo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly ILogger<BrandController> _logger;

        public BrandController(IBrandService brandService, ILogger<BrandController> logger)
        {
            _brandService = brandService;
            _logger = logger;
        }

        // GET: Admin/Brand
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Quản lý thương hiệu";
            
            try
            {
                var brands = await _brandService.GetAllBrandsAsync();
                return View(brands);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading brands in admin");
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách thương hiệu.";
                return View(new List<Brand>());
            }
        }

        // GET: Admin/Brand/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var brand = await _brandService.GetBrandByIdAsync(id.Value);
                if (brand == null) return NotFound();

                ViewData["Title"] = $"Chi tiết thương hiệu: {brand.Name}";
                return View(brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading brand details for id {BrandId}", id);
                return NotFound();
            }
        }

        // GET: Admin/Brand/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Thêm thương hiệu mới";
            return View();
        }

        // POST: Admin/Brand/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createdBrand = await _brandService.CreateBrandAsync(brand);
                    if (createdBrand != null)
                    {
                        TempData["SuccessMessage"] = $"Thêm thương hiệu '{brand.Name}' thành công!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm thương hiệu.";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating brand {BrandName}", brand.Name);
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm thương hiệu.";
                }
            }
            
            ViewData["Title"] = "Thêm thương hiệu mới";
            return View(brand);
        }

        // GET: Admin/Brand/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var brand = await _brandService.GetBrandByIdAsync(id.Value);
                if (brand == null) return NotFound();

                ViewData["Title"] = $"Chỉnh sửa thương hiệu: {brand.Name}";
                return View(brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading brand for edit, id {BrandId}", id);
                return NotFound();
            }
        }

        // POST: Admin/Brand/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Brand brand)
        {
            if (id != brand.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedBrand = await _brandService.UpdateBrandAsync(brand);
                    if (updatedBrand != null)
                    {
                        TempData["SuccessMessage"] = $"Cập nhật thương hiệu '{brand.Name}' thành công!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật thương hiệu.";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating brand {BrandId}", id);
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật thương hiệu.";
                }
            }
            
            ViewData["Title"] = $"Chỉnh sửa thương hiệu: {brand.Name}";
            return View(brand);
        }

        // GET: Admin/Brand/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var brand = await _brandService.GetBrandByIdAsync(id.Value);
                if (brand == null) return NotFound();

                ViewData["Title"] = $"Xóa thương hiệu: {brand.Name}";
                return View(brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading brand for delete, id {BrandId}", id);
                return NotFound();
            }
        }

        // POST: Admin/Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var brand = await _brandService.GetBrandByIdAsync(id);
                if (brand == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy thương hiệu cần xóa.";
                    return RedirectToAction(nameof(Index));
                }

                var result = await _brandService.DeleteBrandAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = $"Xóa thương hiệu '{brand.Name}' thành công!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa thương hiệu. Có thể đang có sản phẩm liên kết.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting brand {BrandId}", id);
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa thương hiệu.";
            }

            return RedirectToAction(nameof(Index));
        }

        // API endpoint để toggle status
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var brand = await _brandService.GetBrandByIdAsync(id);
                if (brand == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy thương hiệu." });
                }

                brand.IsActive = !brand.IsActive;
                brand.UpdatedAt = DateTime.Now;

                var updatedBrand = await _brandService.UpdateBrandAsync(brand);
                if (updatedBrand != null)
                {
                    return Json(new { 
                        success = true, 
                        message = $"Đã {(brand.IsActive ? "kích hoạt" : "vô hiệu hóa")} thương hiệu.",
                        isActive = brand.IsActive
                    });
                }
                else
                {
                    return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling brand status {BrandId}", id);
                return Json(new { success = false, message = "Có lỗi xảy ra." });
            }
        }
    }
} 