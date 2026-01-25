using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GauMeo.Models.Orders
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; } = string.Empty; // Lưu tên sản phẩm tại thời điểm mua

        [StringLength(200)]
        public string ProductBrand { get; set; } = string.Empty; // Lưu thương hiệu tại thời điểm mua

        [StringLength(500)]
        public string ProductImageUrl { get; set; } = string.Empty; // Lưu ảnh tại thời điểm mua

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; } // Giá đơn vị tại thời điểm mua

        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; } // Giá gốc tại thời điểm mua

        [Range(0, 100)]
        public int DiscountPercent { get; set; } = 0; // % giảm giá tại thời điểm mua

        [StringLength(500)]
        public string SelectedVariants { get; set; } = string.Empty; // JSON string variants đã chọn

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; } // UnitPrice * Quantity

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        public int OrderId { get; set; }
        public int ProductId { get; set; } // Reference đến sản phẩm gốc

        // Navigation Properties
        public virtual Order Order { get; set; } = null!;
        public virtual Products.Product Product { get; set; } = null!;

        // Helper method để parse selected variants
        public Dictionary<string, string> GetSelectedVariants()
        {
            if (string.IsNullOrEmpty(SelectedVariants))
                return new Dictionary<string, string>();

            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(SelectedVariants) 
                       ?? new Dictionary<string, string>();
            }
            catch
            {
                return new Dictionary<string, string>();
            }
        }

        // Helper method để set selected variants
        public void SetSelectedVariants(Dictionary<string, string> variants)
        {
            SelectedVariants = System.Text.Json.JsonSerializer.Serialize(variants);
        }
    }
} 