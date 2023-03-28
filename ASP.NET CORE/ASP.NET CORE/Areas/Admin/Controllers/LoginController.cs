using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        public IActionResult Login_Admin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login_Request()
        {
            return RedirectToAction("Category_List","Category");
        }
    }
}
