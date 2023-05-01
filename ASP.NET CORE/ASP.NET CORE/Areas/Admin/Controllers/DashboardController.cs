using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Model;
using ASP.NET_CORE.Models;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IDashboard _dashborad;
        public DashboardController(IDashboard dashborad)
        {
            _dashborad = dashborad;
        }

        public IActionResult Dashboard_Page()
        {
            if (Static.Admin == "")
                return RedirectToAction("Login_Admin", "Login");

            ViewBag.Count_Product = _dashborad.count_Product();
            ViewBag.Count_User = _dashborad.count_User();
            ViewBag.Start = DateTime.Today.ToString("yyyy-MM-dd");
            ViewBag.End = DateTime.Today.ToString("yyyy-MM-dd");
            List<Product_Dashboard> list = _dashborad.get_List(DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd")), DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd")));
            ViewBag.Total = _dashborad.get_Total(DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd")), DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd")));
            return View(list);
        }

        [HttpPost]
        public IActionResult Dashboard_Page(DateTime start, DateTime end)
        {
            ViewBag.Count_Product = _dashborad.count_Product();
            ViewBag.Count_User = _dashborad.count_User();
            ViewBag.Start = start.ToString("yyyy-MM-dd");
            ViewBag.End = end.ToString("yyyy-MM-dd");
            ViewBag.Total = _dashborad.get_Total(start, end);
            List<Product_Dashboard> list = _dashborad.get_List(start, end);
            return View(list);
        }
    }
}
