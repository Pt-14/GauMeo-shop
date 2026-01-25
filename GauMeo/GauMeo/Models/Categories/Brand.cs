using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.Categories
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string ShortName { get; set; } = string.Empty; // Tên viết tắt (VD: "RC", "PD")

        [StringLength(500)]
        public string Description { get; set; } = string.Empty; // Mô tả ngắn

        [StringLength(2000)]
        public string FullDescription { get; set; } = string.Empty; // Mô tả đầy đủ cho trang detail

        [StringLength(50)]
        public string Founded { get; set; } = string.Empty; // Năm thành lập (VD: "1968")

        [StringLength(100)]
        public string Origin { get; set; } = string.Empty; // Xuất xứ (VD: "Pháp", "Mỹ")

        [StringLength(200)]
        public string Website { get; set; } = string.Empty; // Website chính thức

        [StringLength(500)]
        public string Image { get; set; } = string.Empty; // Logo thương hiệu

        [StringLength(3000)]
        public string Features { get; set; } = string.Empty; // Đặc điểm nổi bật (JSON array hoặc string phân cách)

        public int DisplayOrder { get; set; } = 0; // Thứ tự hiển thị

        public bool IsActive { get; set; } = true;

        public bool IsFeatured { get; set; } = false; // Thương hiệu nổi bật

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation Properties
        public virtual ICollection<Products.Product> Products { get; set; }

        public Brand()
        {
            Products = new HashSet<Products.Product>();
        }

        // Helper method để parse Features từ JSON string
        public List<string> GetFeaturesList()
        {
            if (string.IsNullOrEmpty(Features))
                return new List<string>();

            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<List<string>>(Features) ?? new List<string>();
            }
            catch
            {
                // Fallback: split by comma if not JSON
                return Features.Split(',', StringSplitOptions.RemoveEmptyEntries)
                             .Select(f => f.Trim())
                             .ToList();
            }
        }

        // Helper method để set Features từ List<string>
        public void SetFeaturesList(List<string> features)
        {
            Features = System.Text.Json.JsonSerializer.Serialize(features);
        }
    }
} 