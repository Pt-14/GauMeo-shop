namespace GauMeo.Models
{
    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsOnSale { get; set; }
        public bool HasVariants { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public Dictionary<string, List<ProductVariantDto>> AllVariants { get; set; } = new Dictionary<string, List<ProductVariantDto>>();
        public List<ProductReviewDto> Reviews { get; set; } = new List<ProductReviewDto>();
        public List<RelatedProduct> RelatedProducts { get; set; } = new List<RelatedProduct>();
        public List<ProductDescriptionDto> ProductDescriptions { get; set; } = new List<ProductDescriptionDto>();
        public string ReturnPolicy { get; set; } = string.Empty;
    }

    public class ProductVariantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // "size", "flavor", "color", etc.
        public decimal? PriceAdjustment { get; set; } // +/- price difference
        public decimal? Price { get; set; } // Calculated final price
        public decimal? OriginalPrice { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSelected { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class ProductReviewDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }

    public class ProductDescriptionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }

    public class RelatedProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsOnSale { get; set; }
        public bool HasVariants { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public string Url { get; set; } = string.Empty;
    }
} 