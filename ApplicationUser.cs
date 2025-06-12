using Microsoft.AspNetCore.Identity;

namespace GauMeo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Thêm các trường cho Google OAuth
        public string? GoogleId { get; set; }
        public string? LoginProvider { get; set; } // "Google" hoặc "Local"
        public bool IsExternalLogin { get; set; } = false;
    }
} 