using ASP.NET_CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Dashboard_Page()
        {
            if (Static.Admin == "")
                return RedirectToAction("Login_Admin", "Login");

            ViewBag.Admin = Static.Admin/*HttpContext.Session.GetString("Admin")*/;
            return View();
        }
    }
}
