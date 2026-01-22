using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.ViewModels
{
    public class ExternalLoginViewModel
    {
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string? ReturnUrl { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string? ReturnUrl { get; set; }
    }
} 