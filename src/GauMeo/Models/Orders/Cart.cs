using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GauMeo.Models.Orders;

namespace GauMeo.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string? SessionId { get; set; } // Cho khách vãng lai

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public string? UserId { get; set; } // Nullable cho khách vãng lai

        // Navigation Properties
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        // Helper methods
        public decimal GetTotalAmount()
        {
            return CartItems.Sum(item => item.GetSubTotal());
        }

        public int GetTotalItems()
        {
            return CartItems.Sum(item => item.Quantity);
        }
    }
} 