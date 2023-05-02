using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.Models;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class DetailProductController : Controller
    {
        Web_Core_DbContext _context;
        private readonly IProduct _product;
        private readonly IDashboard _dashboard;

        public DetailProductController(IProduct product, Web_Core_DbContext context, IDashboard dashboard)
        {
            _product = product;
            _context = context;
            _dashboard = dashboard;
        }

        public async Task<IActionResult> Detail_Product_Page(int id)
        {
            await _dashboard.Update_Discount();
            //var d = _context.Discounts.ToList();
            //DateTime now = DateTime.Now;

            //foreach (var i in d)
            //{
            //    int start = DateTime.Compare(now.Date, i.StartTime.Date);
            //    int end = DateTime.Compare(now.Date, i.EndTime.Date);
            //    if (start >= 0 && end <= 0)
            //    {
            //        i.Status = "Đang kích hoạt";
            //    }
            //    else i.Status = "Ngừng kích hoạt";
            //}

            //await _context.SaveChangesAsync();
            Product product = _product.get_Product_By_Id(id);
            ViewBag.product_Relationship = _product.get_Product_Relationship(id);
            ViewBag.user = Static.User;
            return View(product);
        }
    }
}
