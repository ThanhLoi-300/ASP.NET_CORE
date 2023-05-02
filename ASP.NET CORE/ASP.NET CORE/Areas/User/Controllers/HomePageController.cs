using ASP.NET_CORE.DATA.Model;
using ASP.NET_CORE.Models;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class HomePageController : Controller
    {
        private readonly IDashboard _dashborad;
        public HomePageController(IDashboard dashborad)
        {
            _dashborad = dashborad;
        }

        public IActionResult Index()
        {
            if (TempData["mess"] != null)
                ViewBag.message = TempData["mess"];
            List<Product_Dashboard> list = _dashborad.Best_Seller();
            ViewBag.user = Static.User;
            return View(list);
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
