using GauMeo.Models.Products;
using GauMeo.Models.ViewModels;

namespace GauMeo.Extensions
{
    public static class ProductMappingExtensions
    {
        public static ProductCreateViewModel ToCreateViewModel(this Product product)
        {
            return new ProductCreateViewModel
            {
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                OriginalPrice = product.OriginalPrice,
                DiscountPercent = product.DiscountPercent,
                StockQuantity = product.StockQuantity,
                IsActive = product.IsActive,
                IsFeatured = product.IsFeatured,
                IsOnSale = product.IsOnSale,
                FreeShipping = product.FreeShipping,
                PopularityScore = product.PopularityScore,
                AnimalType = product.AnimalType,
                Tags = product.Tags,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                ProductImages = product.ProductImages?.Select(img => new ProductImageViewModel
                {
                    Id = img.Id,
                    ImageUrl = img.ImageUrl,
                    AltText = img.AltText,
                    IsMain = img.IsMain,
                    DisplayOrder = img.DisplayOrder
                }).ToList() ?? new List<ProductImageViewModel>(),
                ProductDescriptions = product.ProductDescriptions?.Select(desc => new ProductDescriptionViewModel
                {
                    Id = desc.Id,
                    Title = desc.Title,
                    Content = desc.Content,
                    DisplayOrder = desc.DisplayOrder,
                    IsActive = desc.IsActive
                }).ToList() ?? new List<ProductDescriptionViewModel>()
            };
        }

        public static ProductEditViewModel ToEditViewModel(this Product product)
        {
            var createViewModel = product.ToCreateViewModel();
            return new ProductEditViewModel
            {
                Id = product.Id,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                Rating = product.Rating,
                ReviewCount = product.ReviewCount,
                
                // Copy all properties from CreateViewModel
                Name = createViewModel.Name,
                ShortDescription = createViewModel.ShortDescription,
                OriginalPrice = createViewModel.OriginalPrice,
                DiscountPercent = createViewModel.DiscountPercent,
                StockQuantity = createViewModel.StockQuantity,
                IsActive = createViewModel.IsActive,
                IsFeatured = createViewModel.IsFeatured,
                IsOnSale = createViewModel.IsOnSale,
                FreeShipping = createViewModel.FreeShipping,
                PopularityScore = createViewModel.PopularityScore,
                AnimalType = createViewModel.AnimalType,
                Tags = createViewModel.Tags,
                BrandId = createViewModel.BrandId,
                CategoryId = createViewModel.CategoryId,
                ProductImages = createViewModel.ProductImages,
                ProductDescriptions = createViewModel.ProductDescriptions
            };
        }

        public static Product ToProduct(this ProductCreateViewModel viewModel)
        {
            return new Product
            {
                Name = viewModel.Name,
                ShortDescription = viewModel.ShortDescription,
                OriginalPrice = viewModel.OriginalPrice,
                DiscountPercent = viewModel.DiscountPercent,
                StockQuantity = viewModel.StockQuantity,
                IsActive = viewModel.IsActive,
                IsFeatured = viewModel.IsFeatured,
                IsOnSale = viewModel.IsOnSale,
                FreeShipping = viewModel.FreeShipping,
                PopularityScore = viewModel.PopularityScore,
                AnimalType = viewModel.AnimalType,
                Tags = viewModel.Tags,
                BrandId = viewModel.BrandId,
                CategoryId = viewModel.CategoryId
            };
        }

        public static void UpdateProduct(this ProductEditViewModel viewModel, Product product)
        {
            product.Name = viewModel.Name;
            product.ShortDescription = viewModel.ShortDescription;
            product.OriginalPrice = viewModel.OriginalPrice;
            product.DiscountPercent = viewModel.DiscountPercent;
            product.StockQuantity = viewModel.StockQuantity;
            product.IsActive = viewModel.IsActive;
            product.IsFeatured = viewModel.IsFeatured;
            product.IsOnSale = viewModel.IsOnSale;
            product.FreeShipping = viewModel.FreeShipping;
            product.PopularityScore = viewModel.PopularityScore;
            product.AnimalType = viewModel.AnimalType;
            product.Tags = viewModel.Tags;
            product.BrandId = viewModel.BrandId;
            product.CategoryId = viewModel.CategoryId;
            product.UpdatedAt = DateTime.Now;
        }
    }
} 