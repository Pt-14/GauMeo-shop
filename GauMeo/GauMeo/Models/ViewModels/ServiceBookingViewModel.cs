using System.ComponentModel.DataAnnotations;
using GauMeo.Models.Services;

namespace GauMeo.Models.ViewModels
{
    public class ServiceBookingViewModel
    {
        // Service Information
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public List<ServiceVariant> ServiceVariants { get; set; } = new List<ServiceVariant>();
        public List<ServiceAddon> ServiceAddons { get; set; } = new List<ServiceAddon>();
        public List<ServiceNote> ServiceNotes { get; set; } = new List<ServiceNote>();

        // Booking Information
        [Required(ErrorMessage = "Vui lòng chọn loại dịch vụ")]
        [Display(Name = "Loại dịch vụ")]
        public int ServiceVariantId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100, ErrorMessage = "Họ tên không được quá 100 ký tự")]
        [Display(Name = "Họ và tên")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được quá 15 ký tự")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Display(Name = "Số điện thoại")]
        public string CustomerPhone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(200, ErrorMessage = "Email không được quá 200 ký tự")]
        [Display(Name = "Email")]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên thú cưng")]
        [StringLength(100, ErrorMessage = "Tên thú cưng không được quá 100 ký tự")]
        [Display(Name = "Tên thú cưng")]
        public string PetName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn loại thú cưng")]
        [Display(Name = "Loại thú cưng")]
        public string PetType { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Giống thú cưng không được quá 100 ký tự")]
        [Display(Name = "Giống")]
        public string PetBreed { get; set; } = string.Empty;

        [Display(Name = "Kích thước")]
        public string PetSize { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn ngày đặt lịch")]
        [Display(Name = "Ngày đặt lịch")]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giờ đặt lịch")]
        [Display(Name = "Giờ đặt lịch")]
        public string BookingTime { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Yêu cầu đặc biệt không được quá 1000 ký tự")]
        [Display(Name = "Yêu cầu đặc biệt")]
        public string SpecialRequests { get; set; } = string.Empty;

        // Selected Addons
        public List<int> SelectedAddonIds { get; set; } = new List<int>();

        // Pricing Information
        public decimal BasePrice { get; set; }
        public decimal AddonsTotalPrice { get; set; }
        public decimal TotalPrice => BasePrice + AddonsTotalPrice;

        // Time Slots
        public List<string> AvailableTimeSlots { get; set; } = new List<string>
        {
            "08:00", "08:30", "09:00", "09:30", "10:00", "10:30",
            "11:00", "11:30", "13:00", "13:30", "14:00", "14:30",
            "15:00", "15:30", "16:00", "16:30", "17:00", "17:30"
        };

        // Pet Types and Sizes
        public List<SelectOption> PetTypes { get; set; } = new List<SelectOption>
        {
            new SelectOption { Value = "dog", Text = "Chó 🐕" },
            new SelectOption { Value = "cat", Text = "Mèo 🐱" },
            new SelectOption { Value = "other", Text = "Khác" }
        };

        public List<SelectOption> PetSizes { get; set; } = new List<SelectOption>
        {
            new SelectOption { Value = "small", Text = "Nhỏ (< 5kg)" },
            new SelectOption { Value = "medium", Text = "Trung bình (5-15kg)" },
            new SelectOption { Value = "large", Text = "Lớn (> 15kg)" }
        };

        public ServiceBookingViewModel()
        {
            BookingDate = DateTime.Today.AddDays(1);
        }
    }

    public class SelectOption
    {
        public string Value { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
} 