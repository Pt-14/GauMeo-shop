using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GauMeo.Models.Orders
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; } // Giá tại thời điểm thêm vào giỏ

        [StringLength(500)]
        public string SelectedVariants { get; set; } // JSON string lưu variants đã chọn

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        public int CartId { get; set; }
        public int ProductId { get; set; }

        // Navigation Properties
        public virtual Cart Cart { get; set; }
        public virtual Products.Product Product { get; set; }

        // Helper methods
        public decimal GetSubTotal()
        {
            return UnitPrice * Quantity;
        }

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