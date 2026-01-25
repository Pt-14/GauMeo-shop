using System;
using System.ComponentModel.DataAnnotations;
using GauMeo.Models.Orders;

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

        [Display(Name = "Giới tính")]
        public string? Gender { get; set; }

        [Display(Name = "Số điện thoại")]
        [Phone]
        public string? PhoneNumber { get; set; }

        public string? Avatar { get; set; }

        // Lịch sử đơn hàng
        public List<Order> Orders { get; set; } = new();

        // External login information
        public bool HasExternalLogin { get; set; }
        public List<string> ExternalLoginProviders { get; set; } = new();
    }
} 