using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.Categories
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Slug { get; set; } // URL-friendly name (VD: "thuc-an-hat")

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(500)]
        public string? ImageUrl { get; set; }

        [StringLength(500)]
        public string? IconUrl { get; set; }

        [StringLength(10)]
        public string? Icon { get; set; } // Emoji icon (VD: "🥣", "🥫", "🍖")

        public int Level { get; set; } = 1; // 1=Main (Chó/Mèo), 2=Parent (Thức ăn), 3=Sub (Thức ăn hạt)

        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public bool IsShowOnHome { get; set; } = false; // Hiển thị trên trang chủ

        [StringLength(100)]
        public string? AnimalType { get; set; } // "dog", "cat", "both" - Chỉ áp dụng cho Level 1

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Hierarchical Structure (Cấu trúc cây)
        public int? ParentCategoryId { get; set; }

        // Navigation Properties
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual ICollection<Products.Product> Products { get; set; }

        public Category()
        {
            SubCategories = new HashSet<Category>();
            Products = new HashSet<Products.Product>();
        }
    }
} 