using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.ViewModels
{
    public class ProductEditViewModel : ProductCreateViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Computed properties for display
        public decimal CurrentPrice => OriginalPrice * (100 - DiscountPercent) / 100;
        
        // Statistics for edit view
        public double Rating { get; set; } = 0;
        public int ReviewCount { get; set; } = 0;
    }
} 