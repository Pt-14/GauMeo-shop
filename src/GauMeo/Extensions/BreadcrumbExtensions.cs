using GauMeo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GauMeo.Extensions
{
    public static class BreadcrumbExtensions
    {
        /// <summary>
        /// Creates a BreadcrumbViewModel from breadcrumb items.
        /// Last item (with null action/controller) will be marked as active.
        /// </summary>
        /// <param name="urlHelper">The IUrlHelper instance</param>
        /// <param name="items">Array of tuples: (name, action, controller). Use null for action/controller to mark as active.</param>
        /// <returns>BreadcrumbViewModel with customer CSS class</returns>
        public static BreadcrumbViewModel CreateBreadcrumb(
            this IUrlHelper urlHelper,
            params (string name, string? action, string? controller)[] items)
        {
            if (items == null || items.Length == 0)
            {
                return new BreadcrumbViewModel(); // CssClass defaults to "customer"
            }

            var breadcrumb = new BreadcrumbViewModel(); // CssClass defaults to "customer"
            
            foreach (var (name, action, controllerName) in items)
            {
                string? url = null;
                bool isActive = string.IsNullOrEmpty(action) || string.IsNullOrEmpty(controllerName);
                
                if (!isActive)
                {
                    url = urlHelper.Action(action, controllerName);
                }
                
                breadcrumb.Items.Add(new BreadcrumbItem
                {
                    Name = name,
                    Url = url,
                    IsActive = isActive
                });
            }
            
            return breadcrumb;
        }

        /// <summary>
        /// Converts a List&lt;object&gt; breadcrumb (from ViewBag) to BreadcrumbViewModel.
        /// Used for dynamic breadcrumbs generated from category hierarchy.
        /// </summary>
        /// <param name="urlHelper">The IUrlHelper instance</param>
        /// <param name="breadcrumbs">List of anonymous objects with Name and Url properties</param>
        /// <param name="fallbackTitle">Title to use if breadcrumbs list is empty</param>
        /// <returns>BreadcrumbViewModel with customer CSS class</returns>
        public static BreadcrumbViewModel ConvertToBreadcrumbViewModel(
            this IUrlHelper urlHelper,
            List<object>? breadcrumbs,
            string? fallbackTitle = null)
        {
            var breadcrumb = new BreadcrumbViewModel
            {
                Items = new List<BreadcrumbItem>
                {
                    new() { Name = "Trang chủ", Url = urlHelper.Action("Index", "Home") }
                }
            }; // CssClass defaults to "customer"
            
            if (breadcrumbs != null && breadcrumbs.Any())
            {
                foreach (var item in breadcrumbs)
                {
                    var name = item.GetType().GetProperty("Name")?.GetValue(item)?.ToString() ?? "";
                    var url = item.GetType().GetProperty("Url")?.GetValue(item)?.ToString();
                    var isLast = item == breadcrumbs.Last();
                    
                    breadcrumb.Items.Add(new BreadcrumbItem
                    {
                        Name = name,
                        Url = isLast ? null : url,
                        IsActive = isLast
                    });
                }
            }
            else if (!string.IsNullOrEmpty(fallbackTitle))
            {
                breadcrumb.Items.Add(new BreadcrumbItem
                {
                    Name = fallbackTitle,
                    IsActive = true
                });
            }
            
            return breadcrumb;
        }
    }
}
