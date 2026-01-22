using System.ComponentModel.DataAnnotations;
using GauMeo.Models.Products;

namespace GauMeo.Models
{
    public class WishlistItem
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        [Required] 
        public int ProductId { get; set; }
        
        public DateTime AddedAt { get; set; } = DateTime.Now;
        
        // Navigation Properties
        public virtual ApplicationUser User { get; set; }
        public virtual Product Product { get; set; }
    }
} 