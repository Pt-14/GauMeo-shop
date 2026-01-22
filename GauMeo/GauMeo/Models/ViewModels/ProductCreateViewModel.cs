using System.ComponentModel.DataAnnotations;
using GauMeo.Models.Products;

namespace GauMeo.Models.ViewModels
{
    public class ProductCreateViewModel
    {
        // Basic Product Information
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? ShortDescription { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá gốc")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá gốc phải lớn hơn 0")]
        public decimal OriginalPrice { get; set; }

        [Range(0, 100, ErrorMessage = "Phần trăm giảm giá phải từ 0 đến 100")]
        public int DiscountPercent { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho không được âm")]
        public int StockQuantity { get; set; } = 0;

        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        public bool IsOnSale { get; set; } = false;
        public bool FreeShipping { get; set; } = false;

        [Range(0, int.MaxValue)]
        public int PopularityScore { get; set; } = 0;

        [Required(ErrorMessage = "Vui lòng chọn loại thú cưng")]
        [StringLength(100)]
        public string AnimalType { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Tags { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thương hiệu")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn thương hiệu")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục")]
        public int CategoryId { get; set; }

        // Product Images
        public List<ProductImageViewModel> ProductImages { get; set; } = new();

        // Product Descriptions
        public List<ProductDescriptionViewModel> ProductDescriptions { get; set; } = new();
    }

    public class ProductImageViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập URL hình ảnh")]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        [StringLength(200)]
        public string? AltText { get; set; }

        public bool IsMain { get; set; } = false;

        [Range(0, int.MaxValue)]
        public int DisplayOrder { get; set; } = 0;

        public bool IsDeleted { get; set; } = false; // For tracking deletions during edit
    }

    public class ProductDescriptionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề mô tả")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập nội dung mô tả")]
        [StringLength(5000)]
        public string Content { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false; // For tracking deletions during edit
    }
} 