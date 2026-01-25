using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.Promotions
{
    public class PromotionProduct
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        public int PromotionId { get; set; }
        public int ProductId { get; set; }

        // Navigation Properties
        public virtual Promotion Promotion { get; set; } = null!;
        public virtual Products.Product Product { get; set; } = null!;
    }
} 