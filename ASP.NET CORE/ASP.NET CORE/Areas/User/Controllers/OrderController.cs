using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class OrderController : Controller
    {
        public IActionResult Order_Manage()
        {
            ViewBag.user = HttpContext.Session.GetString("Username");
            return View();
        }
    }
}
