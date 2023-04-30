using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.SERVICE.Interface;
using ASP.NET_CORE.Models;

namespace ASP.NET_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category;
        }

        // GET: Admin/Category
        public IActionResult Category_List(string search, int? page)
        {
            var page_Size = 2;
            var page_Index = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = _category.data_In_Page(page_Index, page_Size, search);

            if (TempData["mess"] != null)
                ViewBag.message = "success";

            int total = _category.count_Category_Items(search);
            var pager = new Pager(total, page_Index, page_Size);
            ViewBag.total = total;
            ViewBag.pager = pager;
            ViewBag.search = search;

            //if (Static.Admin == "")
            //    return RedirectToAction("Login_Admin", "Login");

            //ViewBag.Admin = Static.Admin/*HttpContext.Session.GetString("Admin")*/;
            return View(list);
        }

        // GET: Admin/Category/Create
        public IActionResult Category_Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Category_Create(Category cate)
        {
            if (String.IsNullOrEmpty(cate.Name) || cate.Status == -1)
            {
                ViewBag.message = "error";
                return View(cate);
            }
            else
            {
                if (_category.check_Name(cate.Name, -1))
                {
                    cate.IsDeleted = 0;
                    await _category.Create_Category_Async(cate);
                    TempData["mess"] = "success";
                    return RedirectToAction("Category_List");
                }
                else
                {
                    ViewBag.message = "name";
                    return View(cate);
                }
            }
        }

        // GET: Admin/Category/Edit/5
        public IActionResult Category_Edit(int? id, int page, string search)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _category.get_Category_By_Id((int)id);
            if (category == null)
            {
                return NotFound();
            }
            ViewBag.category = category;
            ViewBag.page = page;
            ViewBag.search = search;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Category_Edit(int id,Category cate, int page, string search)
        {
            if (String.IsNullOrEmpty(cate.Name))
            {
                ViewBag.message = "error";
                ViewBag.category = cate;
                return View();
            }
            else
            {
                if (_category.check_Name(cate.Name, id))
                {
                    Category category = _category.get_Category_By_Id(id);
                    category.Name = cate.Name;
                    await _category.Update_Category_Async(category);
                    TempData["mess"] = "success";
                    return RedirectToAction("Category_List", new { page = page, search = search });
                }
                else
                {
                    ViewBag.message = "name";
                    ViewBag.category = cate;
                    ViewBag.page = page;
                    ViewBag.search = search;
                    return View();
                }
            }
        }
        public async Task<IActionResult> Category_Status(int id, int page, string search)
        {
            Category cate = _category.get_Category_By_Id(id);
            cate.Status = (cate.Status == 1) ? 0 : 1;
            await _category.Update_Category_Async(cate);
            return RedirectToAction("Category_List", new { page = page, search = search });
        }

        public async Task<IActionResult> Category_Delete(int id, int page,string search)
        {
            Category cate = _category.get_Category_By_Id(id);
            cate.IsDeleted = 1;
            await _category.Update_Category_Async(cate);
            TempData["mess"] = "delete";
            return RedirectToAction("Category_List", new { page = page ,search = search});
        }
    }
}
