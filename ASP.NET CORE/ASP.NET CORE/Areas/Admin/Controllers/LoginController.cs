using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.DATA.Model;
using ASP.NET_CORE.Models;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace ASP.NET_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly ILogin _login;

        public LoginController(ILogin login)
        {
            _login = login;
        }

        public IActionResult Login_Admin()
        {
            if(TempData["message"]!=null)
                ViewBag.message = "login fail";
            return View(new LoginVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login_Request(string username,string pass)
        {
            if (ModelState.IsValid)
            {
                if (!_login.Login(username, pass).Equals(""))
                {
                    Static.Admin = _login.Login(username, pass);
                    //HttpContext.Session.SetString("Admin", _login.Login(username, pass));
                    return RedirectToAction("Product_List","Product");
                }
                TempData["message"] = "login fail";
                return RedirectToAction("Login_Admin");
            }
            TempData["message"] = "login fail";
            return RedirectToAction("Login_Admin");
        }

        public IActionResult LogOut()
        {
            //HttpContext.Session.Remove("Admin");
            Static.Admin = "";
            return RedirectToAction("Login_Admin");
        }
    }
}
