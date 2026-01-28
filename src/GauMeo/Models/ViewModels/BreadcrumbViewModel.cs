namespace GauMeo.Models.ViewModels
{
    /// <summary>
    /// Represents a single breadcrumb item in the navigation trail
    /// </summary>
    public class BreadcrumbItem
    {
        /// <summary>
        /// Display name of the breadcrumb item
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// URL for the breadcrumb item. Null or empty for the active (current) page
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Indicates if this is the active (current) page. Active items are not clickable
        /// </summary>
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// ViewModel for breadcrumb navigation component
    /// </summary>
    public class BreadcrumbViewModel
    {
        /// <summary>
        /// List of breadcrumb items in order from root to current page
        /// </summary>
        public List<BreadcrumbItem> Items { get; set; } = new();

        /// <summary>
        /// CSS class to apply to the breadcrumb container. 
        /// Default: "customer". Can be "admin" for admin area breadcrumbs.
        /// </summary>
        public string CssClass { get; set; } = "customer";
    }
}
