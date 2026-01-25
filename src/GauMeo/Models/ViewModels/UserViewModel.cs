using GauMeo.Models;

namespace GauMeo.Models.ViewModels
{
    public class UserViewModel
    {
        public ApplicationUser User { get; set; } = null!;
        public List<string> Roles { get; set; } = new List<string>();
    }
} 