using System.Net.Sockets;
using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
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
            var cartProduct = context.Carts.Where(i => i.ClientId == int.Parse(username)).ToList();
            decimal totalPrice = 0;
            ViewBag.user = username;

            if (username != null)
            {
              List<Cart> cart = context.Carts.Include(c => c.Product).Where(c => c.ClientId == int.Parse(username)).ToList();
                ViewBag.count = cart.Count;
                foreach (var item in cartProduct)
                {
                    totalPrice += item.Price;
                }
                decimal vat = totalPrice * 10/100;
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
        [HttpPost]
        public IActionResult AddCart(string id, string size, int quantity, int id_Client)
        {
            var price = context.Products.FirstOrDefault(p => p.Id == int.Parse(id)).Price;
            var check_Product_Exist = context.Carts.Where(c => c.ClientId == id_Client && c.ProductId == int.Parse(id) && c.Size == size).ToList();
           
            var totalPriceProduct = price * quantity;
            
            Cart cart = new Cart();
            if(check_Product_Exist.Count > 0)
            {
                check_Product_Exist[0].Quantity += quantity;
                check_Product_Exist[0].Price = price * check_Product_Exist[0].Quantity;
                context.Carts.Update(check_Product_Exist[0]);
            }
            else
            {
                cart.ProductId = int.Parse(id);
                cart.Size = size;
                cart.Quantity = quantity;
                cart.ClientId = id_Client;
                cart.Price = totalPriceProduct;
                context.Carts.Add(cart);
            } 
            context.SaveChanges();
            ViewBag.user = id_Client;
            return Json(new { success = true});
        }

    }
}
