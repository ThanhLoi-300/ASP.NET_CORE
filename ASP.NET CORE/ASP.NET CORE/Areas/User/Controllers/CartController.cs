using System.Net.Sockets;
using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.Models;
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
            //var username = HttpContext.Session.GetString("Username");
            var username = Static.User;


            decimal totalPrice = 0;
            ViewBag.user = username;

            if (username != null || username != "")
            {
                var cartProduct = context.Carts.Where(i => i.ClientId == int.Parse(username)).ToList();
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
        // xoa sp khoi cart
        public IActionResult Deleted_Product(int idProduct)
        {
            var product = context.Carts.Where(i => i.Id == idProduct).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            
            context.Carts.Remove(product);
            context.SaveChanges();
            return Json(new { success = true });
        }

        // add quantity in cart
        public IActionResult Add_Quantity_Product(int idCart)
        {
            Cart cart = context.Carts.Where(i => i.Id == idCart).FirstOrDefault();
            var quantityProducSize = 0;
            decimal total = 0;
            var idProduct = cart.ProductId;
            var size = cart.Size;
            var quantityM = context.Products.Where(i => i.Id == idProduct).FirstOrDefault().QuantityM;
            var quantityL = context.Products.Where(i => i.Id == idProduct).FirstOrDefault().QuantityL;
            var quantityS = context.Products.Where(i => i.Id == idProduct).FirstOrDefault().QuantityS;
            var quantityXL = context.Products.Where(i => i.Id == idProduct).FirstOrDefault().QuantityXl;
            if (size == "M")
            {
                quantityProducSize = quantityM;
            }
            else if (size == "S")
            {
                quantityProducSize = quantityS;
            }
            else if (size == "L")
            {
                quantityProducSize = quantityL;
            }
            else if (size == "XL")
            {
                quantityProducSize = quantityXL;
            }

            if (cart.Quantity >= quantityProducSize)
            {
                total = 0;
            }
            else
            {
                decimal quantity = cart.Quantity +1;
                decimal price = cart.Product.Price;
                decimal total_Price = cart.Price;
                total = total_Price + price;

                cart.Price = total;
                cart.Quantity += 1;
                context.Carts.Update(cart);
                context.SaveChanges();
            }
            
            return Json(new { success = true, total = total });
        }
        // subtract quantity in cart
        public IActionResult Subtract_Quantity_Product(int idCart)
        {
            decimal total = 0;
            Cart cart = context.Carts.Where(i => i.Id == idCart).Include(i => i.Product).FirstOrDefault();
            if (cart.Quantity == 1)
            {
                total = 0;
            }
            else
            {
                decimal quantity = cart.Quantity + 1;
                decimal price = cart.Product.Price;
                decimal total_Price = cart.Price;
                total = total_Price - price;

                cart.Price = total;
                cart.Quantity -= 1;
                context.Carts.Update(cart);
                context.SaveChanges();
            }
            return Json(new { success = true, total = total });
        }
    }
}
