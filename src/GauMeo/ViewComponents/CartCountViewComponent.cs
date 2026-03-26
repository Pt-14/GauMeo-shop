using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GauMeo.ViewComponents
{
    public class CartCountViewComponent : ViewComponent
    {
        private readonly Services.ICartService _cartService;

        public CartCountViewComponent(Services.ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var (userId, sessionId) = GetUserAndSessionId();
            var count = await _cartService.GetCartItemCountAsync(userId, sessionId);
            var displayText = count > 99 ? "99+" : count.ToString();
            return View(model: new CartCountViewModel { Count = count, DisplayText = displayText });
        }

        private (string? userId, string? sessionId) GetUserAndSessionId()
        {
            string? userId = null;
            string? sessionId = null;

            if (HttpContext.User.Identity?.IsAuthenticated == true)
            {
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            else
            {
                sessionId = HttpContext.Session.GetString("CartSessionId");
                if (string.IsNullOrEmpty(sessionId))
                {
                    sessionId = Guid.NewGuid().ToString();
                    HttpContext.Session.SetString("CartSessionId", sessionId);
                }
            }

            return (userId, sessionId);
        }
    }

    public class CartCountViewModel
    {
        public int Count { get; set; }
        public string DisplayText { get; set; } = "0";
    }
}
