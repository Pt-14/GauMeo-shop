using Microsoft.AspNetCore.Mvc;

namespace GauMeo.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            _logger.LogWarning("Error page accessed with status code: {StatusCode}", statusCode);

            // Sử dụng Error404.cshtml cho tất cả lỗi với message tùy chỉnh
            switch (statusCode)
            {
                case 404:
                    ViewData["Title"] = "404 - Không tìm thấy trang";
                    ViewData["ErrorTitle"] = "Ối zời ơi, lạc đường zồi! Zời đất quỷ thần!";
                    ViewData["ErrorMessage"] = "Tui thề tui thấy trang này hồi sáng giờ nó trốn đâu mất tiêu!";
                    ViewData["StatusCode"] = "404";
                    break;
                case 403:
                    ViewData["Title"] = "403 - Truy cập bị từ chối";
                    ViewData["ErrorTitle"] = "Ối! Bạn không được phép vào đây!";
                    ViewData["ErrorMessage"] = "Khu vực này chỉ dành cho những người có quyền đặc biệt thôi!";
                    ViewData["StatusCode"] = "403";
                    break;
                case 500:
                    ViewData["Title"] = "500 - Lỗi máy chủ";
                    ViewData["ErrorTitle"] = "Ối! Server bị ngáo rồi!";
                    ViewData["ErrorMessage"] = "Hệ thống đang có chút vấn đề, vui lòng thử lại sau nhé!";
                    ViewData["StatusCode"] = "500";
                    break;
                default:
                    ViewData["Title"] = $"Lỗi {statusCode}";
                    ViewData["ErrorTitle"] = "Ối! Có gì đó không ổn!";
                    ViewData["ErrorMessage"] = "Đã xảy ra lỗi không mong muốn, vui lòng thử lại sau!";
                    ViewData["StatusCode"] = statusCode.ToString();
                    break;
            }

            return View("Error404");
        }

        [Route("Error")]
        public IActionResult Error()
        {
            ViewData["Title"] = "Đã xảy ra lỗi";
            ViewData["ErrorTitle"] = "Ối! Có gì đó không ổn!";
            ViewData["ErrorMessage"] = "Đã xảy ra lỗi không mong muốn, vui lòng thử lại sau!";
            ViewData["StatusCode"] = "Lỗi";
            return View("Error404");
        }
    }
} 