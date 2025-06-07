using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [Route("Error/404")]
    public IActionResult Error404()
    {
        return View();
    }

    [Route("Error/{code}")]
    public IActionResult Error(int code)
    {
        if (code == 404)
            return View("Error404");
        return View("Error");
    }
}
