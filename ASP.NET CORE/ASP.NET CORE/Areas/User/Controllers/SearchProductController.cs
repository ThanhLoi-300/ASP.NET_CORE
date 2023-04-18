using System.Drawing.Printing;
using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.Models;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class SearchProductController : Controller
    {
        Web_Core_DbContext _context;
        private readonly IProduct _product;

        public SearchProductController(IProduct product, Web_Core_DbContext context)
        {
            _product = product;
            _context = context;
        }

        public IActionResult Search_Page(string search,List<string> type, int? page, List<string> category_Id, double price, string sort)
        {
            int page_Size = 6;
            var page_Index = page.HasValue ? Convert.ToInt32(page) : 1;
            List<Product> product = _product.data_In_Page(page_Index, page_Size, search, category_Id, type, price,sort);
            int total = _product.count_Product_Items(search, category_Id, type, price,sort);

            var pager = new Pager(total, page_Index, page_Size);
            ViewBag.total = total;
            ViewBag.pager = pager;
            ViewBag.search = search;
            ViewBag.list_Category = _product.List_Category();
            ViewBag.category_Id = category_Id;
            ViewBag.type = type;
            ViewBag.price = price;
            ViewBag.sort = sort;
            ViewBag.user = HttpContext.Session.GetString("Username");
            return View(product);
        }

        [HttpPost]
        public IActionResult Get_Img(int id)
        {
            string s = _context.Imgs.OrderBy(x => Guid.NewGuid()).Where(i => i.ProductId == id && i.SubImg == 0).First().ImgProduct.ToString();
            return Json(new { url = s, success = true });
        }

        [HttpPost]
        public IActionResult Get_Main_Img(int id)
        {
            string s = _context.Imgs.OrderBy(x => Guid.NewGuid()).Where(i => i.ProductId == id && i.SubImg == 1).First().ImgProduct.ToString();
            return Json(new { url = s, success = true });
        }

        public IActionResult Product_Category()
        {
            ViewBag.user = HttpContext.Session.GetString("Username");
            return View();
        }
    }
}
