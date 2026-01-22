using GauMeo.Models.Services;
using GauMeo.Models.ViewModels;
using System.Globalization;

namespace GauMeo.Extensions
{
    public static class ServiceMappingExtensions
    {
        public static ServiceViewModel ToViewModel(this Service service)
        {
            var viewModel = new ServiceViewModel
            {
                Id = service.Id,
                Name = service.Name,
                ShortName = service.ShortName,
                Description = service.Description ?? string.Empty,
                FullDescription = service.FullDescription ?? string.Empty,
                Image = service.Image ?? string.Empty,
                FaqImage = service.FaqImage ?? string.Empty,
                Features = service.GetFeaturesList()
            };

            // Map related data if loaded
            if (service.ServiceVariants?.Any() == true)
            {
                var dogVariants = service.ServiceVariants.Where(v => v.PetType == "dog").ToList();
                var catVariants = service.ServiceVariants.Where(v => v.PetType == "cat").ToList();

                viewModel.DogPricing = dogVariants.Select(v => v.ToPricingViewModel()).ToList();
                viewModel.CatPricing = catVariants.Select(v => v.ToPricingViewModel()).ToList();
            }

            if (service.ServiceAddons?.Any() == true)
            {
                viewModel.Addons = service.ServiceAddons.Select(a => a.ToViewModel()).ToList();
            }

            if (service.ServiceFAQs?.Any() == true)
            {
                viewModel.FAQ = service.ServiceFAQs.Select(f => f.ToViewModel()).ToList();
            }

            if (service.ServiceNotes?.Any() == true)
            {
                viewModel.Notes = service.ServiceNotes.Select(n => n.ToViewModel()).ToList();
            }

            if (service.ServiceImages?.Any() == true)
            {
                viewModel.Images = service.ServiceImages.Select(i => i.ToViewModel()).ToList();
            }

            return viewModel;
        }

        public static ServicePricingViewModel ToPricingViewModel(this ServiceVariant variant)
        {
            return new ServicePricingViewModel
            {
                Package = variant.Name,
                Size = GetSizeDisplayName(variant.PetSize),
                Price = variant.Price.ToString("N0", new CultureInfo("vi-VN")),
                Duration = variant.Duration ?? string.Empty,
                Features = variant.Description ?? string.Empty
            };
        }

        public static ServiceAddonViewModel ToViewModel(this ServiceAddon addon)
        {
            return new ServiceAddonViewModel
            {
                Name = addon.Name,
                Price = addon.Price.ToString("N0", new CultureInfo("vi-VN")) + " VNĐ",
                Description = addon.Description ?? string.Empty
            };
        }

        public static ServiceFAQViewModel ToViewModel(this ServiceFAQ faq)
        {
            return new ServiceFAQViewModel
            {
                Question = faq.Question,
                Answer = faq.Answer
            };
        }

        public static ServiceNoteViewModel ToViewModel(this ServiceNote note)
        {
            return new ServiceNoteViewModel
            {
                Title = note.Title ?? string.Empty,
                Content = note.Content,
                Icon = note.Icon ?? string.Empty,
                NoteType = note.NoteType ?? string.Empty
            };
        }

        public static ServiceImageViewModel ToViewModel(this ServiceImage image)
        {
            return new ServiceImageViewModel
            {
                ImageUrl = image.ImageUrl,
                Title = image.Title ?? string.Empty,
                Description = image.Description ?? string.Empty,
                AltText = image.AltText ?? string.Empty,
                IsMain = image.IsMain,
                DisplayOrder = image.DisplayOrder
            };
        }

        private static string GetSizeDisplayName(string petSize)
        {
            return petSize?.ToLower() switch
            {
                "small" => "Nhỏ",
                "medium" => "Vừa",
                "large" => "Lớn",
                _ => petSize ?? "Tất cả"
            };
        }
    }
} 