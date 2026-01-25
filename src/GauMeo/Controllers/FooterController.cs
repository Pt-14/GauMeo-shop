using Microsoft.AspNetCore.Mvc;

namespace GauMeo.Controllers
{
    public class FooterController : Controller
    {
        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        public IActionResult ReturnPolicy()
        {
            return View();
        }

        public IActionResult PaymentShipping()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult MembershipBenefits()
        {
            return View();
        }
    }
} 