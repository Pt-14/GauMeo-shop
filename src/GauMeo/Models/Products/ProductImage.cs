using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.Products
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        [StringLength(200)]
        public string? AltText { get; set; }

        public bool IsMain { get; set; } = false; // Hình ảnh chính

        public int DisplayOrder { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int ProductId { get; set; }

        // Navigation Property
        public virtual Product? Product { get; set; }
    }
} 