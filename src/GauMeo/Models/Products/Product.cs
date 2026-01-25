using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GauMeo.Models.Products
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? ShortDescription { get; set; } // Mô tả ngắn hiển thị trong danh sách

        // PHƯƠNG ÁN 1: Chỉ cần OriginalPrice + DiscountPercent
        // Giá cuối cùng được tính từ OriginalPrice và DiscountPercent
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; } // Giá gốc của sản phẩm

        [Range(0, 100)]
        public int DiscountPercent { get; set; } = 0; // % giảm giá

        // Computed Property: Giá cuối = OriginalPrice - (OriginalPrice * DiscountPercent / 100)
        [NotMapped]
        public decimal CurrentPrice => OriginalPrice * (100 - DiscountPercent) / 100;

        [Range(0, 5)]
        public double Rating { get; set; } = 0;

        public int ReviewCount { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public bool IsFeatured { get; set; } = false;

        public bool IsOnSale { get; set; } = false;

        public bool FreeShipping { get; set; } = false;

        [Range(0, int.MaxValue)]
        public int PopularityScore { get; set; } = 0;

        [Required(ErrorMessage = "Vui lòng chọn loại thú cưng")]
        [StringLength(100)]
        public string AnimalType { get; set; } = string.Empty; // "dog", "cat", "both"

        [StringLength(500)]
        public string? Tags { get; set; } // Lưu dạng string phân cách bằng dấu phẩy

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        [Required(ErrorMessage = "Vui lòng chọn thương hiệu")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn thương hiệu")]
        public int BrandId { get; set; }
        
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục")]
        public int CategoryId { get; set; }

        // Navigation Properties
        public virtual Categories.Brand? Brand { get; set; }
        public virtual Categories.Category? Category { get; set; }
        public virtual ICollection<ProductDescription> ProductDescriptions { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductVariant> ProductVariants { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Promotions.PromotionProduct> PromotionProducts { get; set; }
        public virtual ICollection<Orders.CartItem> CartItems { get; set; }
        public virtual ICollection<Orders.OrderItem> OrderItems { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }

        public Product()
        {
            ProductDescriptions = new HashSet<ProductDescription>();
            ProductImages = new HashSet<ProductImage>();
            ProductVariants = new HashSet<ProductVariant>();
            Reviews = new HashSet<Review>();
            PromotionProducts = new HashSet<Promotions.PromotionProduct>();
            CartItems = new HashSet<Orders.CartItem>();
            OrderItems = new HashSet<Orders.OrderItem>();
            WishlistItems = new HashSet<WishlistItem>();
        }
    }
} 