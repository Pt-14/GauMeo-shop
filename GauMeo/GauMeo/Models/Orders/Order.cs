using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GauMeo.Models.Promotions;

namespace GauMeo.Models.Orders
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string OrderNumber { get; set; } = string.Empty; // Mã đơn hàng

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        public string CustomerPhone { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string BillingAddress { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; } // Tổng tiền hàng

        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingFee { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; } = 0; // Số tiền giảm

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } // Tổng thanh toán

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // "Pending", "Confirmed", "Processing", "Shipping", "Delivered", "Cancelled"

        [Required]
        [StringLength(100)]
        public string PaymentMethod { get; set; } = "COD"; // "COD", "Bank Transfer", "E-Wallet"

        [Required]
        [StringLength(50)]
        public string PaymentStatus { get; set; } = "Pending"; // "Pending", "Paid", "Failed"

        [Required]
        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty; // Ghi chú từ khách hàng

        [Required]
        [StringLength(1000)]
        public string AdminNotes { get; set; } = string.Empty; // Ghi chú từ admin

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ConfirmedAt { get; set; }

        public DateTime? ShippedAt { get; set; }

        public DateTime? DeliveredAt { get; set; }

        // Foreign Keys
        public string? UserId { get; set; }
        public int? PromotionId { get; set; }

        // Navigation Properties
        public virtual ApplicationUser? User { get; set; }
        public virtual Promotion? Promotion { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Helper method to generate order number
        public static string GenerateOrderNumber()
        {
            return $"GM{DateTime.Now:yyyyMMdd}{DateTime.Now.Ticks % 10000:D4}";
        }
    }
}