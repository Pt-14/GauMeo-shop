using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GauMeo.Models.Services
{
    public class ServiceBooking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(15)]
        public string CustomerPhone { get; set; }

        [StringLength(200)]
        public string? CustomerEmail { get; set; }

        [Required]
        [StringLength(100)]
        public string PetName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string PetType { get; set; } = string.Empty; // "dog", "cat", "other"

        [StringLength(100)]
        public string? PetBreed { get; set; } // Giống

        [StringLength(20)]
        public string? PetSize { get; set; } // "small", "medium", "large"

        [StringLength(1000)]
        public string? SpecialRequests { get; set; } // Yêu cầu đặc biệt

        [Required]
        public DateTime BookingDate { get; set; } // Ngày đặt lịch

        [Required]
        [StringLength(10)]
        public string BookingTime { get; set; } = string.Empty; // Giờ đặt lịch (VD: "09:00")

        [Column(TypeName = "decimal(18,2)")]
        public decimal EstimatedPrice { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // "Pending", "Confirmed", "InProgress", "Completed", "Cancelled"

        [StringLength(1000)]
        public string? Notes { get; set; } // Ghi chú từ staff

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ConfirmedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        // Foreign Keys
        public int ServiceId { get; set; }
        public int ServiceVariantId { get; set; } // Loại dịch vụ cụ thể được đặt
        public string? UserId { get; set; } // Nullable nếu khách vãng lai

        // Navigation Properties
        public virtual Service Service { get; set; } = null!;
        public virtual ServiceVariant ServiceVariant { get; set; } = null!;
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<ServiceBookingAddon> ServiceBookingAddons { get; set; }

        public ServiceBooking()
        {
            ServiceBookingAddons = new HashSet<ServiceBookingAddon>();
        }
    }
} 