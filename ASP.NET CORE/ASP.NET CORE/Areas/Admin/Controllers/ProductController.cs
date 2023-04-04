
using ASP.NET_CORE.DATA.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE.SERVICE.Interface;
using ASP.NET_CORE.Models;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging.Signing;
using System.IO;
using ASP.NET_CORE.DATA.EF;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.Extensions.FileProviders;

namespace Web_BAN_QUAN_AO.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        Web_Core_DbContext context;
        private readonly IProduct _product;
        private IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProduct product, IWebHostEnvironment webHostEnvironment, Web_Core_DbContext contexxt)
        {
            _product = product;
            _webHostEnvironment = webHostEnvironment;
            context = contexxt;
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
            ViewBag.list_Category = _product.List_Category();
            return View(list);
        }

        // GET: Admin/Product/Create
        public ActionResult Product_Create(string search)
        {
            ViewBag.category_Id = new SelectList(_product.List_Category(), "Id", "Name");
            ViewBag.search = search;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Product_Create(Product product, List<IFormFile> save_Upload, List<int> radio)
        {
            ViewBag.category_Id = new SelectList(_product.List_Category(), "Id", "Name", product.Category_ID);

            if (save_Upload.Count == 0)
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
                        var fileName = "";
                        var extension = "";
                        var webRootPath = _webHostEnvironment.WebRootPath;
                        var path = "";
                        product.Img = "";

                        if (save_Upload.Count != 0)
                        {
                            var choose = 0;
                            for (int i = 0; i < save_Upload.Count; i++)
                            {
                                fileName = Path.GetFileNameWithoutExtension(save_Upload[i].FileName);
                                extension = Path.GetExtension(save_Upload[i].FileName);
                                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                                path = Path.Combine(webRootPath, uploadDir, fileName);
                                await save_Upload[i].CopyToAsync(new FileStream(path, FileMode.Create));

                                choose = 0;
                                if (i + 1 == radio[0])
                                {
                                    choose = 1;
                                    product.Img = "/" + uploadDir + "/" + fileName;
                                }
                                else
                                {
                                    choose = 0;
                                }

                                product.Imgs.Add(new Img
                                {
                                    ProductId = product.Id,
                                    ImgProduct = "/" + uploadDir + "/" + fileName,
                                    SubImg = choose
                                });
                            }
                        }

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
                        return RedirectToAction("Product_List", new { page = page, search = product.Name });
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
                ViewBag.page = page;
                ViewBag.search = search;
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

        public IActionResult Edit_Img_Product(int id)
        {
            ViewBag.List_Img = _product.List_Img_Of_Product(id);
            return View();
        }

        [HttpPost]
        public IActionResult Delete_Img(int id)
        {
            _product.Delete_Img(id);
            return Json( new {success = true});
        }

        [HttpPost]
        public IActionResult Add_Img(int id, List<IFormFile> url)
        {
            var uploadDir = @"img/product";
            var webRootPath = _webHostEnvironment.WebRootPath;
            List<string> list = new List<string>();

            for (int i = 0; i < url.Count; i++)
            {
                var fileName = Path.GetFileNameWithoutExtension(url[i].FileName);
                var extension = Path.GetExtension(url[i].FileName);
                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;
                var path = Path.Combine(webRootPath, uploadDir, fileName);
                url[i].CopyToAsync(new FileStream(path, FileMode.Create));
                var s = "/" + uploadDir + "/" + fileName;
                list.Add(s);
            }
            _product.Add_Img(id, list);
            ViewBag.list = list.Count();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Delete_SubImg(int id)
        {
            //_product.Delete_All_SubImg(id);
            var list_Img_Is_Deleted = context.Imgs.Where(i => i.ProductId == id );
            context.Imgs.RemoveRange(list_Img_Is_Deleted.Where(i => i.SubImg == 0));
            context.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Change_Main_Img(int id)
        {
            _product.Change_Main_Img(id);
            return Json(new { success = true });
        }
    }
}
