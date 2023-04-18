using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiscountController : Controller
    {
        public IActionResult Discount_Page()
        {
            return View();
        }

        public IActionResult Create_Discount()
        {
            return View();
        }
    }
}
