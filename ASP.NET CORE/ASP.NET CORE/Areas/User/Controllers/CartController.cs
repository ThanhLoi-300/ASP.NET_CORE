using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class CartController : Controller
    {
        Web_Core_DbContext context;
        private readonly IProduct _product;
        private IWebHostEnvironment _webHostEnvironment;
        public CartController(IProduct product, IWebHostEnvironment webHostEnvironment, Web_Core_DbContext contexxt)
        {
            _product = product;
            _webHostEnvironment = webHostEnvironment;
            context = contexxt;
        }
        public IActionResult Cart_Page()
        {
            var username = HttpContext.Session.GetString("Username");
            if(username != null)
            {
              var cart = context.Carts.Include(c => c.Product).Where(c => c.Id == int.Parse(username)).ToList();

                return View(cart);
            }
            else
            {
                return View();
            }
           
        }
        [HttpPost]
        public IActionResult AddCart(string id, string size, string quantity)
        {
            var username = HttpContext.Session.GetString("Username");
            var price = context.Products.FirstOrDefault(p => p.Id == int.Parse(id)).Price;
            var totalPrice = price * int.Parse(quantity);
            Cart cart = new Cart();
            cart.ProductId = int.Parse(id);
            cart.Size = size;
            cart.Quantity = int.Parse(quantity);
            cart.ClientId = int.Parse(username);
            cart.Price = totalPrice;
            context.Carts.Add(cart);
            context.SaveChanges();
            ViewBag.user = username;
            TempData["Cart success"] = "Cart success";
            return RedirectToAction("Cart_Page");
        }

    }
}
