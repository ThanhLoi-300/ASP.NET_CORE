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
            ViewBag.user = username;

            if (username != null)
            {
              List<Cart> cart = context.Carts.Include(c => c.Product).Where(c => c.ClientId == int.Parse(username)).ToList();
                ViewBag.count = cart.Count;
                return View(cart);
            }
            else
            {
                return View();
            }
           
        }
        [HttpPost]
        public IActionResult AddCart(string id, string size, int quantity, int id_Client)
        {
            var price = context.Products.FirstOrDefault(p => p.Id == int.Parse(id)).Price;
            var totalPrice = price * quantity;
            Cart cart = new Cart();
            cart.ProductId = int.Parse(id);
            cart.Size = size;
            cart.Quantity = quantity;
            cart.ClientId = id_Client;
            cart.Price = totalPrice;

            context.Carts.Add(cart);
            context.SaveChanges();
            ViewBag.user = id_Client;
            return Json(new { success = true});
        }

    }
}
