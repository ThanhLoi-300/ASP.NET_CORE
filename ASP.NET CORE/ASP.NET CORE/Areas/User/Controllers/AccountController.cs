using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        public IActionResult Account_Index()
        {
            return View();
        }

        public IActionResult Login_Page()
        {
            return View();
        }

        public IActionResult Sign_Up_Page()
        {
            return View();
        }
    }
}
