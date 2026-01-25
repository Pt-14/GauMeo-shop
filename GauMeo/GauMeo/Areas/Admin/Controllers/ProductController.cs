using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GauMeo.Services;
using GauMeo.Models.Products;
using GauMeo.Models.ViewModels;
using GauMeo.Extensions;

namespace GauMeo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(
            IProductService productService,
            IBrandService brandService,
            ICategoryService categoryService,
            ILogger<ProductController> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Product
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Quản lý sản phẩm";
            
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading products in admin");
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách sản phẩm.";
                return View(new List<Product>());
            }
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var product = await _productService.GetProductByIdWithDetailsAsync(id.Value);
                if (product == null) return NotFound();

                ViewData["Title"] = $"Chi tiết sản phẩm: {product.Name}";
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading product details for id {ProductId}", id);
                return NotFound();
            }
        }

        // GET: Admin/Product/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Thêm sản phẩm mới";
            await LoadSelectLists();
            
            var viewModel = new ProductCreateViewModel();
            // Add default image and description entries
            viewModel.ProductImages.Add(new ProductImageViewModel { IsMain = true, DisplayOrder = 1 });
            viewModel.ProductDescriptions.Add(new ProductDescriptionViewModel { DisplayOrder = 1 });
            
            return View(viewModel);
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel viewModel)
        {
            // Debug ModelState
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid for product creation. Errors: {Errors}", 
                    string.Join(", ", ModelState.SelectMany(x => x.Value?.Errors ?? Enumerable.Empty<Microsoft.AspNetCore.Mvc.ModelBinding.ModelError>()).Select(x => x.ErrorMessage)));
                
                foreach (var modelError in ModelState)
                {
                    if (modelError.Value?.Errors.Count > 0)
                    {
                        _logger.LogWarning("Field: {Field}, Errors: {Errors}", 
                            modelError.Key, 
                            string.Join(", ", modelError.Value.Errors.Select(e => e.ErrorMessage)));
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = viewModel.ToProduct();
                    
                    // Convert ViewModels to entities
                    var images = viewModel.ProductImages
                        .Where(img => !string.IsNullOrEmpty(img.ImageUrl) && !img.IsDeleted)
                        .Select(img => new ProductImage
                        {
                            ImageUrl = img.ImageUrl,
                            AltText = img.AltText,
                            IsMain = img.IsMain,
                            DisplayOrder = img.DisplayOrder
                        }).ToList();

                    var descriptions = viewModel.ProductDescriptions
                        .Where(desc => !string.IsNullOrEmpty(desc.Title) && !string.IsNullOrEmpty(desc.Content) && !desc.IsDeleted)
                        .Select(desc => new ProductDescription
                        {
                            Title = desc.Title,
                            Content = desc.Content,
                            DisplayOrder = desc.DisplayOrder,
                            IsActive = desc.IsActive
                        }).ToList();

                    var createdProduct = await _productService.CreateProductWithDetailsAsync(product, images, descriptions);
                    if (createdProduct != null)
                    {
                        TempData["SuccessMessage"] = $"Thêm sản phẩm '{viewModel.Name}' thành công!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm sản phẩm.";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating product {ProductName}", viewModel.Name);
                    TempData["ErrorMessage"] = $"Có lỗi xảy ra khi thêm sản phẩm: {ex.Message}";
                }
            }
            
            ViewData["Title"] = "Thêm sản phẩm mới";
            await LoadSelectLists();
            return View(viewModel);
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var product = await _productService.GetProductByIdWithDetailsAsync(id.Value);
                if (product == null) return NotFound();

                var viewModel = product.ToEditViewModel();
                
                ViewData["Title"] = $"Chỉnh sửa sản phẩm: {product.Name}";
                await LoadSelectLists();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading product for edit, id {ProductId}", id);
                return NotFound();
            }
        }

        // POST: Admin/Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            // Debug ModelState
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid for product update. Errors: {Errors}", 
                    string.Join(", ", ModelState.SelectMany(x => x.Value?.Errors ?? Enumerable.Empty<Microsoft.AspNetCore.Mvc.ModelBinding.ModelError>()).Select(x => x.ErrorMessage)));
                
                foreach (var modelError in ModelState)
                {
                    if (modelError.Value?.Errors.Count > 0)
                    {
                        _logger.LogWarning("Field: {Field}, Errors: {Errors}", 
                            modelError.Key, 
                            string.Join(", ", modelError.Value.Errors.Select(e => e.ErrorMessage)));
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = viewModel.ToProduct();
                    product.Id = viewModel.Id; // Ensure ID is set
                    
                    // Convert ViewModels to entities
                    var images = viewModel.ProductImages
                        .Where(img => !string.IsNullOrEmpty(img.ImageUrl) && !img.IsDeleted)
                        .Select(img => new ProductImage
                        {
                            ImageUrl = img.ImageUrl,
                            AltText = img.AltText,
                            IsMain = img.IsMain,
                            DisplayOrder = img.DisplayOrder
                        }).ToList();

                    var descriptions = viewModel.ProductDescriptions
                        .Where(desc => !string.IsNullOrEmpty(desc.Title) && !string.IsNullOrEmpty(desc.Content) && !desc.IsDeleted)
                        .Select(desc => new ProductDescription
                        {
                            Title = desc.Title,
                            Content = desc.Content,
                            DisplayOrder = desc.DisplayOrder,
                            IsActive = desc.IsActive
                        }).ToList();

                    var updatedProduct = await _productService.UpdateProductWithDetailsAsync(product, images, descriptions);
                    if (updatedProduct != null)
                    {
                        TempData["SuccessMessage"] = $"Cập nhật sản phẩm '{viewModel.Name}' thành công!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật sản phẩm.";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating product {ProductId}", id);
                    TempData["ErrorMessage"] = $"Có lỗi xảy ra khi cập nhật sản phẩm: {ex.Message}";
                }
            }
            
            ViewData["Title"] = $"Chỉnh sửa sản phẩm: {viewModel.Name}";
            await LoadSelectLists();
            return View(viewModel);
        }

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var product = await _productService.GetProductByIdWithDetailsAsync(id.Value);
                if (product == null) return NotFound();

                ViewData["Title"] = $"Xóa sản phẩm: {product.Name}";
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading product for delete, id {ProductId}", id);
                return NotFound();
            }
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdWithDetailsAsync(id);
                if (product == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy sản phẩm cần xóa.";
                    return RedirectToAction(nameof(Index));
                }

                var result = await _productService.DeleteProductAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = $"Xóa sản phẩm '{product.Name}' thành công!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa sản phẩm.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product {ProductId}", id);
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa sản phẩm.";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Product/ToggleStatus
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var result = await _productService.ToggleProductStatusAsync(id);
                
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling product status {ProductId}", id);
                return Json(new { success = false, message = "Có lỗi xảy ra" });
            }
        }

        // POST: Admin/Product/UploadImage
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "Vui lòng chọn file ảnh" });
            }

            // Validate file type
            var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/webp" };
            if (!allowedTypes.Contains(file.ContentType.ToLower()))
            {
                return Json(new { success = false, message = "Chỉ chấp nhận file ảnh (JPEG, PNG, GIF, WebP)" });
            }

            // Validate file size (max 10MB)
            if (file.Length > 10 * 1024 * 1024)
            {
                return Json(new { success = false, message = "Kích thước file không được vượt quá 10MB" });
            }

            try
            {
                // Create products directory if it doesn't exist
                string productImagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                if (!Directory.Exists(productImagesPath))
                {
                    Directory.CreateDirectory(productImagesPath);
                }

                // Generate unique filename
                string fileExtension = Path.GetExtension(file.FileName);
                string fileName = $"product_{DateTime.Now.Ticks}{fileExtension}";
                string fullPath = Path.Combine(productImagesPath, fileName);

                // Save file
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Return the relative URL
                string imageUrl = $"/images/products/{fileName}";
                return Json(new { success = true, imageUrl = imageUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading product image");
                return Json(new { success = false, message = "Có lỗi xảy ra khi tải lên ảnh" });
            }
        }

        private async Task LoadSelectLists()
        {
            try
            {
                var brands = await _brandService.GetAllBrandsAsync();
                var allCategories = await _categoryService.GetAllCategoriesForAdminAsync();

                ViewBag.BrandId = new SelectList(brands.Where(b => b.IsActive), "Id", "Name");
                
                // Create hierarchical category list - only show level 3 categories (actual product categories)
                var level3Categories = allCategories
                    .Where(c => c.IsActive && c.Level == 3)
                    .Select(c => new
                    {
                        Id = c.Id,
                        Name = GetCategoryDisplayName(c, allCategories.ToList())
                    })
                    .OrderBy(c => c.Name);

                ViewBag.CategoryId = new SelectList(level3Categories, "Id", "Name");
                
                ViewBag.AnimalTypes = new SelectList(new[]
                {
                    new { Value = "dog", Text = "Chó" },
                    new { Value = "cat", Text = "Mèo" },
                    new { Value = "both", Text = "Cả hai" }
                }, "Value", "Text");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading select lists");
            }
        }

        private string GetCategoryDisplayName(Models.Categories.Category category, List<Models.Categories.Category> allCategories)
        {
            try
            {
                var parts = new List<string>();
                
                // Get parent hierarchy
                var current = category;
                while (current != null)
                {
                    parts.Insert(0, current.Name);
                    
                    if (current.ParentCategoryId.HasValue)
                    {
                        current = allCategories.FirstOrDefault(c => c.Id == current.ParentCategoryId.Value);
                    }
                    else
                    {
                        current = null;
                    }
                }
                
                return string.Join(" > ", parts);
            }
            catch
            {
                return category.Name;
            }
        }
    }
} 