using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.ViewModels
{
    public class CreateReviewViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Vui lòng chọn số sao từ 1 đến 5")]
        public int Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Bình luận không được vượt quá 1000 ký tự")]
        public string? Comment { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(200, ErrorMessage = "Email không được vượt quá 200 ký tự")]
        public string CustomerEmail { get; set; } = string.Empty;

        public List<IFormFile>? Images { get; set; }
    }
}
