using System.ComponentModel.DataAnnotations;

namespace GauMeo.Models.Services
{
    public class ServiceNote
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; } // "Tiêm phòng", "Thú cưng già"

        [Required]
        [StringLength(1000)]
        public string Content { get; set; } // "Thú cưng cần được tiêm phòng đầy đủ..."

        [StringLength(10)]
        public string Icon { get; set; } // "⚠️", "ℹ️"

        [StringLength(20)]
        public string NoteType { get; set; } // "warning", "info", "special"

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int ServiceId { get; set; }

        // Navigation Property
        public virtual Service Service { get; set; }
    }
} 