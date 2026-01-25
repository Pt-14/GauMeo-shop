using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GauMeo.Models.Products
{
    public class ProductVariant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty; // Tên biến thể (VD: "500g", "Vị gà")

        [StringLength(100)]
        public string Type { get; set; } = string.Empty; // Loại biến thể: "size" hoặc "flavor"

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PriceAdjustment { get; set; } // Điều chỉnh giá (+/-)

        public bool IsDefault { get; set; } = false; // Biến thể mặc định

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int ProductId { get; set; }

        // Navigation Property
        public virtual Product Product { get; set; } = null!;
    }
} 