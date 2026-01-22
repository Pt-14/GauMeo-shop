using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GauMeo.Models.Promotions
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } // Tên chương trình khuyến mãi

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Type { get; set; } = "sale"; // "sale", "flash_sale", "seasonal"

        [Range(0, 100)]
        public decimal DiscountPercentage { get; set; } // Phần trăm giảm giá

        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountAmount { get; set; } // Số tiền giảm cố định

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsPublic { get; set; } = true; // Hiển thị công khai hay chỉ cho khách hàng có mã

        [StringLength(500)]
        public string ImageUrl { get; set; } // Banner khuyến mãi

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation Properties
        public virtual ICollection<PromotionProduct> PromotionProducts { get; set; } // Sản phẩm trong chương trình

        public Promotion()
        {
            PromotionProducts = new HashSet<PromotionProduct>();
        }

        // Helper method kiểm tra promotion có còn hiệu lực không
        public bool IsValid()
        {
            var now = DateTime.Now;
            return IsActive && StartDate <= now && EndDate >= now;
        }
    }
} 