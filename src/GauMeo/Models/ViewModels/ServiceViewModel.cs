using GauMeo.Models.Services;

namespace GauMeo.Models.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FullDescription { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string FaqImage { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new List<string>();
        
        // Pricing data grouped by pet type
        public List<ServicePricingViewModel> DogPricing { get; set; } = new List<ServicePricingViewModel>();
        public List<ServicePricingViewModel> CatPricing { get; set; } = new List<ServicePricingViewModel>();
        
        // Related data
        public List<ServiceAddonViewModel> Addons { get; set; } = new List<ServiceAddonViewModel>();
        public List<ServiceFAQViewModel> FAQ { get; set; } = new List<ServiceFAQViewModel>();
        public List<ServiceNoteViewModel> Notes { get; set; } = new List<ServiceNoteViewModel>();
        public List<ServiceImageViewModel> Images { get; set; } = new List<ServiceImageViewModel>();
    }

    public class ServicePricingViewModel
    {
        public string Package { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Features { get; set; } = string.Empty;
    }

    public class ServiceAddonViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class ServiceFAQViewModel
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }

    public class ServiceNoteViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string NoteType { get; set; } = string.Empty;
    }

    public class ServiceImageViewModel
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AltText { get; set; } = string.Empty;
        public bool IsMain { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class ServiceIndexViewModel
    {
        public List<ServiceViewModel> Services { get; set; } = new List<ServiceViewModel>();
        public ServiceViewModel? CurrentService { get; set; }
        public int CurrentServiceId { get; set; } = 1;
    }
} 