using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class OrderController : Controller
    {
        public IActionResult Order_Manage()
        {
            return View();
        }
    }
}
