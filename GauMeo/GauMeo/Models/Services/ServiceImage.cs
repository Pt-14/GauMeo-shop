using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.Services
{
    public class ServiceImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; }

        [StringLength(200)]
        public string AltText { get; set; }

        [StringLength(200)]
        public string Title { get; set; } // slide-name: "Spa Thư Giãn", "Cắt Tỉa Lông"

        [StringLength(500)]
        public string Description { get; set; } // slide-description: "Dịch vụ spa cao cấp..."

        public bool IsMain { get; set; } = false; // Hình ảnh chính

        public int DisplayOrder { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int ServiceId { get; set; }

        // Navigation Property
        public virtual Service Service { get; set; }
    }
} 