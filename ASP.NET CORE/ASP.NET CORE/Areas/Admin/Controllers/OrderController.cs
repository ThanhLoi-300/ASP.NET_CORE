using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.Models;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private IOrder _order;

        public OrderController(IOrder order)
        {
            _order = order;
        }

        public IActionResult Order_Manage()
        {
            if (Static.Admin == "")
                return RedirectToAction("Login_Admin", "Login");

            var list = _order.get_Accounts_Have_Order();
            ViewBag.Admin = Static.Admin;
            return View(list);
        }

        public IActionResult Order_Detail(int clientId, int status, string account, string name)
        {
            if (Static.Admin == "")
                return RedirectToAction("Login_Admin", "Login");

            var list = _order.get_Order_Of_Username(clientId, status);
            ViewBag.Admin = Static.Admin;
            ViewBag.Status = status;
            ViewBag.Account = account;
            ViewBag.Name = name;
            ViewBag.ClientId = clientId;
            return View(list);
        }

        [HttpPost]
        public IActionResult Change_Status(int order_Id, string action)
        {
            _order.Change_Status(order_Id, action);
            return Json(new {success = true});
        }
    }
}
