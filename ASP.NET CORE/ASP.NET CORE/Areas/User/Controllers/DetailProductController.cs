using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class DetailProductController : Controller
    {
        Web_Core_DbContext _context;
        private readonly IProduct _product;

        public DetailProductController(IProduct product, Web_Core_DbContext context)
        {
            _product = product;
            _context = context;
        }

        public IActionResult Detail_Product_Page(int id)
        {
            Product product = _product.get_Product_By_Id(id);
            ViewBag.product_Relationship = _product.get_Product_Relationship(id);
            return View(product);
        }
    }
}
