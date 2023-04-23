using ASP.NET_CORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
          
            if (Static.User == null || Static.User == "")
            {
                return RedirectToAction("Login_Page", "Account");
            }
            else
            {
                if (TempData["mess"] != null)
                    ViewBag.message = "success";
                else if (TempData["messss"] != null)
                    ViewBag.message = "successss";
                ViewBag.user = Static.User; ;
                return View();
            }
        }

        public IActionResult About_Us()
        {
            if (Static.User != null || Static.User != "")
            {
                ViewBag.user = Static.User;
                
            }
            return View();


        }
    }
}
