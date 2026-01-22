using System.ComponentModel.DataAnnotations;
using GauMeo.Models.Services;

namespace GauMeo.Models.ViewModels
{
    public class ServiceBookingManagementViewModel
    {
        public List<ServiceBookingListItem> Bookings { get; set; } = new();
        public ServiceBookingStatistics Statistics { get; set; } = new();
        public ServiceBookingFilters Filters { get; set; } = new();
    }

    public class ServiceBookingListItem
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string PetName { get; set; } = string.Empty;
        public string PetType { get; set; } = string.Empty;
        public string PetBreed { get; set; } = string.Empty;
        public string PetSize { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string VariantName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public string BookingTime { get; set; } = string.Empty;
        public decimal EstimatedPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public List<string> AddonsNames { get; set; } = new();
    }

    public class ServiceBookingStatistics
    {
        public int TotalBookings { get; set; }
        public int PendingBookings { get; set; }
        public int ConfirmedBookings { get; set; }
        public int InProgressBookings { get; set; }
        public int CompletedBookings { get; set; }
        public int CancelledBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal PendingRevenue { get; set; }
        public decimal CompletedRevenue { get; set; }
        public int TodayBookings { get; set; }
        public int WeekBookings { get; set; }
        public int MonthBookings { get; set; }
    }

    public class ServiceBookingFilters
    {
        public string? SearchTerm { get; set; }
        public string? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? ServiceId { get; set; }
        public string? PetType { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
} 