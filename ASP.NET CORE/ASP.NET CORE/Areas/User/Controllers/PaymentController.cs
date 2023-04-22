using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class PaymentController : Controller
    {
        Web_Core_DbContext context;
        private readonly IProduct _product;
        private IWebHostEnvironment _webHostEnvironment;
        public PaymentController(IProduct product, IWebHostEnvironment webHostEnvironment, Web_Core_DbContext contexxt)
        {
            _product = product;
            _webHostEnvironment = webHostEnvironment;
            context = contexxt;
        }
        public IActionResult Payment_Page()
        {
            var username = HttpContext.Session.GetString("Username");
            var cartProduct = context.Carts.Where(i => i.ClientId == int.Parse(username)).ToList();
            decimal totalPrice = 0;
            ViewBag.user = username;

            if (username != null)
            {
                List<Cart> cart = context.Carts.Include(c => c.Product).Include(c => c.Client).Where(c => c.ClientId == int.Parse(username)).ToList();
                var client = context.Clients.Where(c => c.Id == int.Parse(username)).ToList();
                ViewBag.name = client[0].Name;
                ViewBag.sdt = client[0].Name;
                ViewBag.address = client[0].Address;
                ViewBag.email = client[0].Email;
                ViewBag.count = cart.Count;
                foreach (var item in cartProduct)
                {   
                    totalPrice += item.Price;
                }
                decimal vat = totalPrice * 10 / 100;
                decimal endPrice = totalPrice - vat;
                ViewBag.vat = vat;
                ViewBag.endPrice = endPrice;
                ViewBag.total = totalPrice;
                return View(cart);
            }
            else
            {
                return View();
            }
        }
    }
}
