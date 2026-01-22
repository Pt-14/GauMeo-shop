using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GauMeo.Models.Services
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string ShortName { get; set; } // Tên ngắn (VD: "Spa", "Grooming")

        [StringLength(500)]
        public string Description { get; set; } // Mô tả ngắn

        [StringLength(2000)]
        public string FullDescription { get; set; } // Mô tả đầy đủ

        [StringLength(100)]
        public string Price { get; set; } // Giá dạng string (VD: "150.000 - 300.000 VNĐ")

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MinPrice { get; set; } // Giá tối thiểu (dạng số)

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MaxPrice { get; set; } // Giá tối đa (dạng số)

        [StringLength(100)]
        public string Duration { get; set; } // Thời lượng (VD: "60 - 90 phút")

        [StringLength(500)]
        public string Image { get; set; } // Hình ảnh dịch vụ

        [StringLength(500)]
        public string FaqImage { get; set; } // Hình ảnh FAQ container

        [StringLength(3000)]
        public string Features { get; set; } // Đặc điểm dịch vụ (JSON array)

        public bool IsActive { get; set; } = true;

        public bool IsFeatured { get; set; } = false; // Dịch vụ nổi bật

        public int DisplayOrder { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation Properties
        public virtual ICollection<ServiceImage> ServiceImages { get; set; }
        public virtual ICollection<ServiceVariant> ServiceVariants { get; set; }
        public virtual ICollection<ServiceBooking> ServiceBookings { get; set; }
        public virtual ICollection<ServiceAddon> ServiceAddons { get; set; }
        public virtual ICollection<ServiceFAQ> ServiceFAQs { get; set; }
        public virtual ICollection<ServiceNote> ServiceNotes { get; set; }

        public Service()
        {
            ServiceImages = new HashSet<ServiceImage>();
            ServiceVariants = new HashSet<ServiceVariant>();
            ServiceBookings = new HashSet<ServiceBooking>();
            ServiceAddons = new HashSet<ServiceAddon>();
            ServiceFAQs = new HashSet<ServiceFAQ>();
            ServiceNotes = new HashSet<ServiceNote>();
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