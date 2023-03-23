
using ASP.NET_CORE.DATA.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE.SERVICE.Interface;
using ASP.NET_CORE.Models;

namespace Web_BAN_QUAN_AO.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProduct _product;
        private IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProduct product, IWebHostEnvironment webHostEnvironment)
        {
            _product = product;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Product
        public ActionResult Product_List(int? page, string search, int? category_Id)
        {
            var page_Size = 2;
            var page_Index = page.HasValue ? Convert.ToInt32(page) : 1;

            var cate_Id = category_Id.HasValue ? Convert.ToInt32(category_Id) : -1;
            var list = _product.data_In_Page(page_Index, page_Size, search, cate_Id);
            int total = _product.count_Product_Items(search, cate_Id);

            if (TempData["mess"] != null)
                ViewBag.message = "success";

            var pager = new Pager(total, page_Index, page_Size);
            ViewBag.total = total;
            ViewBag.pager = pager;
            ViewBag.search = search;
            ViewBag.category_Id = category_Id;
            return View(list);
        }

        // GET: Admin/Product/Create
        public ActionResult Product_Create()
        {
            ViewBag.category_Id = new SelectList(_product.List_Category(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Product_Create(Product product, IFormFile Image)
        {
            ViewBag.category_Id = new SelectList(_product.List_Category(), "Id", "Name", product.Category_ID);
            if (Image == null)
            {
                ViewBag.message = "Img";
                return View(product);
            }

            if (ModelState.IsValid)
            {
                if (product.QuantityS < 0 || product.QuantityM < 0 || product.QuantityL < 0 || product.QuantityXl < 0)
                {
                    ViewBag.message = "quantity";
                    return View(product);
                }
                else if (product.Price < 1000)
                {
                    ViewBag.message = "price";
                    return View(product);
                }
                else
                {
                    product.IsDeleted = 0;
                    if (_product.check_Name(product.Name, -1))
                    {
                        var uploadDir = @"img/product";
                        var fileName = Path.GetFileNameWithoutExtension(Image.FileName);
                        var extension = Path.GetExtension(Image.FileName);
                        var webRootPath = _webHostEnvironment.WebRootPath;
                        fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                        var path = Path.Combine(webRootPath, uploadDir, fileName);
                        await Image.CopyToAsync(new FileStream(path, FileMode.Create));
                        product.Img = "/" + uploadDir + "/" + fileName;
                        await _product.Create_Product_Async(product);

                        TempData["mess"] = "success";
                        return RedirectToAction("Product_List");
                    }
                    else
                    {
                        ViewBag.message = "name";
                        return View(product);
                    }
                }
            }
            else
            {
                ViewBag.message = "error";
                return View(product);
            }

        }

        // GET: Admin/Product/Edit/5
        public ActionResult Product_Edit(int id, int page, string search)
        {
            Product product = _product.get_Product_By_Id(id);
            ViewBag.c = product.Category_ID;
            ViewBag.cate = new SelectList(_product.List_Category(), "Id", "Name", product.Category_ID);
            ViewBag.page = page;
            ViewBag.search = search;
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Product_Edit(Product product, int page, string search)
        {
            ViewBag.cate = new SelectList(_product.List_Category(), "Id", "Name", product.Category_ID);
            if (ModelState.IsValid)
            {
                if (product.QuantityS < 0 || product.QuantityM < 0 || product.QuantityL < 0 || product.QuantityXl < 0)
                {
                    ViewBag.message = "quantity";
                    return View(product);
                }
                else if (product.Price < 1000)
                {
                    ViewBag.message = "price";
                    return View(product);
                }
                else
                {
                    if (_product.check_Name(product.Name, product.Id))
                    {
                        product.IsDeleted = 0;
                        await _product.Update_Product_Async(product);
                        TempData["mess"] = "success";
                        return RedirectToAction("Product_List", new { page = page, search = search });
                    }
                    else
                    {
                        ViewBag.message = "name";
                        ViewBag.page = page;
                        ViewBag.search = search;
                        return View(product);
                    }
                }
            }
            else
            {
                ViewBag.message = "error";
                return View(product);
            }
        }
        public async Task<IActionResult> Product_Status(int id, int page, string search)
        {
            Product p = _product.get_Product_By_Id(id);
            p.Status = (p.Status == 1) ? 0 : 1;
            await _product.Update_Product_Async(p);
            return RedirectToAction("Product_List", new { page = page, search = search });
        }

        public async Task<IActionResult> Product_Delete(int id, int page, string search)
        {
            Product p = _product.get_Product_By_Id(id);
            p.IsDeleted = 1;
            await _product.Update_Product_Async(p);
            TempData["mess"] = "delete";
            return RedirectToAction("Product_List", new { page = page, search = search });
        }
    }
}
