using System;
using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.ViewModels
{
    public class ProfileViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Họ và tên")]
        public string? FullName { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Số điện thoại")]
        [Phone]
        public string? PhoneNumber { get; set; }

        public string? Avatar { get; set; }
    }
} 