using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        public IActionResult Order_Manage()
        {
            return View();
        }

        public IActionResult Order_Detail()
        {
            return View();
        }
    }
}
