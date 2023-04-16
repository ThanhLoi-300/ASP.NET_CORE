using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
          
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login_Page", "Account");
            }
            else
            {
                if (TempData["mess"] != null)
                    ViewBag.message = "success";
                return View();
            }
        }

        public IActionResult About_Us()
        {
            return View();
        }
    }
}
