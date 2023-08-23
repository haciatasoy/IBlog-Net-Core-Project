using Microsoft.AspNetCore.Mvc;

namespace IBlog.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error(int code)
        {
            return View();
        }
    }
}
