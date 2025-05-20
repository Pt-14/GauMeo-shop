using Microsoft.AspNetCore.Mvc;

namespace GauMeo.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add(int id)
        {
            // Trong thực tế sẽ thêm sản phẩm vào giỏ hàng
            // Hiện tại chỉ chuyển hướng về trang giỏ hàng
            return RedirectToAction("Index");
        }
    }
} 