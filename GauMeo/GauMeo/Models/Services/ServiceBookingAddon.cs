using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GauMeo.Models.Services
{
    public class ServiceBookingAddon
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ServiceBookingId { get; set; }

        [Required]
        public int ServiceAddonId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Giá tại thời điểm đặt

        public DateTime CreatedAt { get; set; } = DateTime.Now;        // Navigation Properties
        public virtual ServiceBooking ServiceBooking { get; set; } = null!;
        public virtual ServiceAddon ServiceAddon { get; set; } = null!;
    }
}
