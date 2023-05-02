using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiscountController : Controller
    {
        private readonly IDashboard _dashborad;
        public DiscountController(IDashboard dashborad)
        {
            _dashborad = dashborad;
        }

        public async Task<IActionResult> Discount_Page()
        {
            await _dashborad.Update_Discount();
            List<Discount> list = await _dashborad.Get_All_DiscountAsync();

            if (TempData["message"] != null)
                ViewBag.message = TempData["message"];

            return  View(list);
        }

        public IActionResult Create_Discount()
        {
            ViewBag.Start = DateTime.Today.ToString("yyyy-MM-dd");
            ViewBag.End = DateTime.Today.ToString("yyyy-MM-dd");
            return View();
        }

        [HttpPost]
        public IActionResult Create_Discount(int value, string start,string end)
        {
            ViewBag.Start = DateTime.Parse(start).ToString("yyyy-MM-dd");
            ViewBag.End = DateTime.Parse(end).ToString("yyyy-MM-dd");
            if (value <= 0 || value > 90) ViewBag.message = "value invalid";
            else if (_dashborad.Check_Value(value, null))
            {
                ViewBag.message = "value existed";
            }
            else
            {
                Discount d = new Discount();
                d.Value = value;
                d.StartTime = DateTime.Parse(start);
                d.EndTime = DateTime.Parse(end);
                d.Status = "Ngừng kích hoạt";
                d.IsDeleted = 0;
                _dashborad.Insert_Discount(d);
                TempData["message"] = "success";
                return RedirectToAction("Discount_Page");
            }
            return View();
        }

        public async Task<IActionResult> Discount_EditAsync(int id)
        {
            var item = _dashborad.Edit_Discount(id);
            ViewBag.Start = item.StartTime.ToString("yyyy-MM-dd");
            ViewBag.End = item.EndTime.ToString("yyyy-MM-dd");
            var list_item_of_Discount = await _dashborad.Get_Detail_Of_DiscountAsync(id);
            ViewBag.list_item_of_Discount = list_item_of_Discount;
            var List_Product_Remaining = _dashborad.Get_Product_Remaining();
            ViewBag.list_product_Remaining = List_Product_Remaining;
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Discount_Edit(Discount d, List<int> choose)
        {
            await _dashborad.Change(d, choose);
            ViewBag.message = "success";
            ViewBag.Start = d.StartTime.ToString("yyyy-MM-dd");
            ViewBag.End = d.EndTime.ToString("yyyy-MM-dd");
            var list_item_of_Discount = await _dashborad.Get_Detail_Of_DiscountAsync(d.Id);
            ViewBag.list_item_of_Discount = list_item_of_Discount;
            var List_Product_Remaining = _dashborad.Get_Product_Remaining();
            ViewBag.list_product_Remaining = List_Product_Remaining;
            return View(d);
        }

        public IActionResult List_Product_Discount(int id)
        {
            return View();
        }
    }
}
