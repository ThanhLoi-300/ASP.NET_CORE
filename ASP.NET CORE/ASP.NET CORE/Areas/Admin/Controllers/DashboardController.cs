using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Dashboard_Page()
        {
            return View();
        }
    }
}
