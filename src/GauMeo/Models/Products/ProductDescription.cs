using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.Products
{
    public class ProductDescription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty; // Tên của phần mô tả (VD: "Thành phần", "Hướng dẫn sử dụng", "Lợi ích")

        [Required]
        [StringLength(5000)]
        public string Content { get; set; } = string.Empty; // Nội dung mô tả

        public int DisplayOrder { get; set; } = 0; // Thứ tự hiển thị

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int ProductId { get; set; }

        // Navigation Property
        public virtual Product? Product { get; set; }
    }
} 