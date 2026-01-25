using Microsoft.AspNetCore.Identity;
using GauMeo.Models.Products;
using GauMeo.Models.Services;
using GauMeo.Models.Orders;

namespace GauMeo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public new string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Thêm các trường cho Google OAuth
        public string? GoogleId { get; set; }
        public string? LoginProvider { get; set; } // "Google" hoặc "Local"
        public bool IsExternalLogin { get; set; } = false;

        // Navigation Properties
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ServiceBooking> ServiceBookings { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }

        public ApplicationUser()
        {
            Reviews = new HashSet<Review>();
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
            ServiceBookings = new HashSet<ServiceBooking>();
            WishlistItems = new HashSet<WishlistItem>();
        }
    }
} 