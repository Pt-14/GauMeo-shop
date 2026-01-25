using Microsoft.AspNetCore.Identity;
using GauMeo.Models;

namespace GauMeo.Middleware
{
    public class AdminAreaMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminAreaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            var path = context.Request.Path.Value?.ToLower();
            
            // Kiểm tra nếu đang truy cập vào Admin Area
            if (path != null && path.StartsWith("/admin"))
            {
                // Kiểm tra xem user đã đăng nhập chưa
                if (context.User.Identity?.IsAuthenticated != true)
                {
                    // Nếu chưa đăng nhập, redirect về trang login với return URL
                    var returnUrl = System.Net.WebUtility.UrlEncode(context.Request.Path + context.Request.QueryString);
                    context.Response.Redirect($"/Account/Login?returnUrl={returnUrl}");
                    return;
                }

                // Kiểm tra xem user có role Admin không
                try
                {
                    var user = await userManager.GetUserAsync(context.User);
                    if (user == null || !await userManager.IsInRoleAsync(user, "Admin"))
                    {
                        // Nếu không phải admin, redirect về trang Access Denied
                        context.Response.Redirect("/Account/AccessDenied");
                        return;
                    }
                }
                catch (Exception)
                {
                    // Nếu có lỗi, redirect về login
                    context.Response.Redirect("/Account/Login");
                    return;
                }
            }

            await _next(context);
        }
    }

    // Extension method để dễ dàng sử dụng middleware
    public static class AdminAreaMiddlewareExtensions
    {
        public static IApplicationBuilder UseAdminAreaProtection(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AdminAreaMiddleware>();
        }
    }
} 