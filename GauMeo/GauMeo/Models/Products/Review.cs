using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.Products
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string Comment { get; set; } // Đánh giá bằng text

        [StringLength(100)]
        public string CustomerName { get; set; }

        [StringLength(200)]
        public string CustomerEmail { get; set; }

        public bool IsVerifiedPurchase { get; set; } = false; // Đã mua hàng hay chưa

        public bool IsApproved { get; set; } = false; // Được duyệt hiển thị hay chưa

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ApprovedAt { get; set; }

        // Foreign Keys
        public int ProductId { get; set; }
        public string? UserId { get; set; } // Nullable nếu khách vãng lai

        // Navigation Properties
        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<ReviewImage> ReviewImages { get; set; } // Hình ảnh đánh giá

        public Review()
        {
            ReviewImages = new HashSet<ReviewImage>();
        }
    }
} 