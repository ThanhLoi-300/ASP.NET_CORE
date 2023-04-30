using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.DATA.Model;
using ASP.NET_CORE.Models;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace ASP.NET_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly ILogin _login;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(ILogin login, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _login = login;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login_Admin()
        {
            if(TempData["message"]!=null)
                ViewBag.message = TempData["message"];
            return View(new LoginVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login_Request(string username,string pass)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.FindByNameAsync(username);
                if (result != null)
                {
                    return RedirectToAction("Product_List", "Product");
                }
                //if (!_login.Login(username, pass).Equals(""))
                //{
                //    Static.Admin = _login.Login(username, pass);
                //    //HttpContext.Session.SetString("Admin", _login.Login(username, pass));
                //    return RedirectToAction("Product_List","Product");
                //}
                TempData["message"] = "success";
                return RedirectToAction("Category_List","Category");
            }
            //TempData["message"] = "login fail";
            return RedirectToAction("Login_Admin");
        }

        public async Task<IActionResult> LogOut()
        {
            //HttpContext.Session.Remove("Admin");
            //Static.Admin = "";
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login_Admin");
        }
    }
}
