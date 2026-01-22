using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models
{
    public class Newsletter
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
} 