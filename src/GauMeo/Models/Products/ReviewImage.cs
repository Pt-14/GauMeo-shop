using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.Products
{
    public class ReviewImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        [StringLength(200)]
        public string AltText { get; set; } = string.Empty;

        public int DisplayOrder { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int ReviewId { get; set; }

        // Navigation Property
        public virtual Review Review { get; set; } = null!;
    }
} 