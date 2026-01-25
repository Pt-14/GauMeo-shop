using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GauMeo.Data;
using GauMeo.Models.Categories;
using GauMeo.Services;

namespace GauMeo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        // GET: Admin/Category
        public async Task<IActionResult> Index(string? search = null, bool? isActive = null)
        {
            try
            {
                ViewData["Title"] = "Quản lý danh mục";
                
                var categories = await _categoryService.GetCategoriesForAdminAsync(search, isActive);
                
                ViewBag.Search = search;
                ViewBag.IsActive = isActive;
                
                return View(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading categories for admin");
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách danh mục.";
                return View(new List<Category>());
            }
        }

        // GET: Admin/Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id.Value);
                if (category == null)
                {
                    return NotFound();
                }

                ViewData["Title"] = $"Chi tiết danh mục: {category.Name}";
                
                // Get child categories
                var childCategories = await _categoryService.GetDirectSubCategoriesAsync(id.Value);
                ViewBag.ChildCategories = childCategories;
                
                return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading category details for ID: {CategoryId}", id);
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải chi tiết danh mục.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/Category/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewData["Title"] = "Thêm danh mục mới";
                
                // Get possible parent categories
                var parentCategories = await _categoryService.GetPossibleParentCategoriesAsync();
                ViewBag.ParentCategories = parentCategories;
                
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create category page");
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải trang tạo danh mục.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Admin/Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Generate slug if empty
                    if (string.IsNullOrEmpty(category.Slug))
                    {
                        category.Slug = await _categoryService.GenerateSlugAsync(category.Name);
                    }

                    var result = await _categoryService.CreateCategoryAsync(category);
                    if (result != null)
                    {
                        TempData["SuccessMessage"] = $"Thêm danh mục '{category.Name}' thành công!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo danh mục. Vui lòng kiểm tra lại thông tin.";
                    }
                }

                // Reload parent categories if validation fails
                var parentCategories = await _categoryService.GetPossibleParentCategoriesAsync();
                ViewBag.ParentCategories = parentCategories;
                
                return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category: {CategoryName}", category.Name);
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo danh mục.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id.Value);
                if (category == null)
                {
                    return NotFound();
                }

                ViewData["Title"] = $"Chỉnh sửa danh mục: {category.Name}";
                
                // Get possible parent categories (excluding current category and its descendants)
                var parentCategories = await _categoryService.GetPossibleParentCategoriesAsync(id.Value);
                ViewBag.ParentCategories = parentCategories;
                
                return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading edit category page for ID: {CategoryId}", id);
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải trang chỉnh sửa danh mục.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id) return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryService.UpdateCategoryAsync(category);
                    if (result != null)
                    {
                        TempData["SuccessMessage"] = $"Cập nhật danh mục '{category.Name}' thành công!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật danh mục. Vui lòng kiểm tra lại thông tin.";
                    }
                }

                // Reload parent categories if validation fails
                var parentCategories = await _categoryService.GetPossibleParentCategoriesAsync(id);
                ViewBag.ParentCategories = parentCategories;
                
                return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category: {CategoryId}", id);
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật danh mục.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id.Value);
                if (category == null)
                {
                    return NotFound();
                }

                ViewData["Title"] = $"Xóa danh mục: {category.Name}";
                
                // Check if category can be deleted
                var hasChildren = await _categoryService.HasChildCategoriesAsync(id.Value);
                var hasProducts = await _categoryService.HasProductsAsync(id.Value);
                
                ViewBag.HasChildren = hasChildren;
                ViewBag.HasProducts = hasProducts;
                ViewBag.CanDelete = !hasChildren && !hasProducts;
                
                // Get child categories to show warning
                if (hasChildren)
                {
                    var childCategories = await _categoryService.GetDirectSubCategoriesAsync(id.Value);
                    ViewBag.ChildCategories = childCategories;
                }
                
                return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading delete category page for ID: {CategoryId}", id);
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải trang xóa danh mục.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa danh mục thành công!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa danh mục này vì còn có danh mục con hoặc sản phẩm liên quan!";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category: {CategoryId}", id);
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa danh mục.";
                return RedirectToAction(nameof(Index));
            }
        }

        // Helper API method for generating slug
        [HttpPost]
        public async Task<IActionResult> GenerateSlug([FromBody] string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return Json(new { success = false, message = "Tên danh mục không được để trống" });
                }

                var slug = await _categoryService.GenerateSlugAsync(name);
                return Json(new { success = true, slug = slug });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating slug for name: {Name}", name);
                return Json(new { success = false, message = "Có lỗi xảy ra khi tạo slug" });
            }
        }

        // Helper API method for checking slug uniqueness
        [HttpPost]
        public async Task<IActionResult> CheckSlug([FromBody] dynamic data)
        {
            try
            {
                string slug = data.slug;
                int? excludeId = data.excludeId;

                if (string.IsNullOrEmpty(slug))
                {
                    return Json(new { isUnique = false, message = "Slug không được để trống" });
                }

                var isUnique = await _categoryService.IsSlugUniqueAsync(slug, excludeId);
                return Json(new { isUnique = isUnique });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking slug uniqueness");
                return Json(new { isUnique = false, message = "Có lỗi xảy ra khi kiểm tra slug" });
            }
        }
    }
} 