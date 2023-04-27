using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.Models;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private IOrder _order;
        private Web_Core_DbContext _dbContext;

        public OrderController(IOrder order,Web_Core_DbContext dbContext)
        {
            _order = order;
            _dbContext = dbContext;
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
        public IActionResult Change_Status(int order_Id)
        {
            //_order.Change_Status(order_Id);
            var o = _dbContext.Orders.Where(o => o.Id == order_Id).FirstOrDefault();
            o.Status = 1;

            var list_Order = _dbContext.DetailOrders.Where(o => o.OrderId == order_Id).Include(o => o.Product).ToList();
            foreach (var item in list_Order)
            {
                if (item.Size.Equals("S"))
                    item.Product.QuantityS -= item.Quantity;
                if (item.Size.Equals("M"))
                    item.Product.QuantityM -= item.Quantity;
                if (item.Size.Equals("L"))
                    item.Product.QuantityL -= item.Quantity;
                if (item.Size.Equals("Xl"))
                    item.Product.QuantityXl -= item.Quantity;
            }

            _dbContext.SaveChanges();
            return Json(new {success = true});
        }
    }
}
