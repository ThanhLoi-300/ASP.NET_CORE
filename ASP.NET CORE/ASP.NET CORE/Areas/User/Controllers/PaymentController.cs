using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class PaymentController : Controller
    {
        public IActionResult Payment_Page()
        {
            return View();
        }
    }
}
