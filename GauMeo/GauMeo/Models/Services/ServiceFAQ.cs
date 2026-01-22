using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.Services
{
    public class ServiceFAQ
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Question { get; set; } // "Spa & Grooming có an toàn cho thú cưng không?"

        [Required]
        [StringLength(2000)]
        public string Answer { get; set; } // "Hoàn toàn an toàn. Chúng tôi sử dụng..."

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int ServiceId { get; set; }

        // Navigation Property
        public virtual Service Service { get; set; }
    }
} 