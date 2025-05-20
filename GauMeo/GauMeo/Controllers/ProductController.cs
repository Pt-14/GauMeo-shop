using Microsoft.AspNetCore.Mvc;

namespace GauMeo.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Detail()
        {
            return View();
        }
    }
} 