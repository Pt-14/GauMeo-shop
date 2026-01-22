using Microsoft.AspNetCore.Mvc;
using GauMeo.Services;

namespace GauMeo.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly INewsletterService _newsletterService;
        private readonly ILogger<NewsletterController> _logger;

        public NewsletterController(
            INewsletterService newsletterService,
            ILogger<NewsletterController> logger)
        {
            _newsletterService = newsletterService;
            _logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe([FromForm] string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return Json(new { success = false, message = "Vui lòng nhập email" });
                }

                if (!new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email))
                {
                    return Json(new { success = false, message = "Email không hợp lệ" });
                }

                var isSubscribed = await _newsletterService.IsSubscribedAsync(email);
                if (isSubscribed)
                {
                    return Json(new { success = false, message = "Email này đã đăng ký nhận tin" });
                }

                var result = await _newsletterService.SubscribeAsync(email);
                if (result)
                {
                    return Json(new { success = true, message = "Đăng ký nhận tin thành công! Vui lòng kiểm tra email của bạn." });
                }

                return Json(new { success = false, message = "Không thể đăng ký. Vui lòng thử lại sau" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Subscribe: {ex.Message}");
                return Json(new { success = false, message = "Đã có lỗi xảy ra. Vui lòng thử lại sau" });
            }
        }
    }
} 