using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GauMeo.Models.Services
{
    public class ServiceVariant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty; // Tên loại dịch vụ (VD: "Cắt tỉa cơ bản", "Cắt tỉa cao cấp")

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string PetType { get; set; } = string.Empty; // "dog", "cat", "both"

        [StringLength(20)]
        public string PetSize { get; set; } = string.Empty; // "small", "medium", "large", "all"

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(50)]
        public string Duration { get; set; } = string.Empty; // Thời gian (VD: "60 phút")

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int ServiceId { get; set; }

        // Navigation Property
        public virtual Service Service { get; set; } = null!;
        public virtual ICollection<ServiceBooking> ServiceBookings { get; set; }

        public ServiceVariant()
        {
            ServiceBookings = new HashSet<ServiceBooking>();
        }
    }
} 