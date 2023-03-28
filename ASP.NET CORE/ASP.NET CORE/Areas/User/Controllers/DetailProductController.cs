using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class DetailProductController : Controller
    {
        public IActionResult Detail_Product_Page()
        {
            return View();
        }
    }
}
