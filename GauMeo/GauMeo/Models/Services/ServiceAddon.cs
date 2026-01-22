using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GauMeo.Models.Services
{
    public class ServiceAddon
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } // "Cắt Móng Tay", "Vệ Sinh Tai"

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // 50000

        [StringLength(500)]
        public string Description { get; set; } // "Cắt và mài móng chuyên nghiệp, an toàn"

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int ServiceId { get; set; }

        // Navigation Property
        public virtual Service Service { get; set; }
    }
} 