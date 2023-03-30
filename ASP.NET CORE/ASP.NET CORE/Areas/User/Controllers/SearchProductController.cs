using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class SearchProductController : Controller
    {
        public IActionResult Search_Page()
        {
            return View();
        }

        public IActionResult Product_Category()
        {
            return View();
        }
    }
}
