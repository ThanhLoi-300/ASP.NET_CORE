using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class CartController : Controller
    {
        public IActionResult Cart_Page()
        {
            ViewBag.user = HttpContext.Session.GetString("Username");
            return View();
        }
    }
}
