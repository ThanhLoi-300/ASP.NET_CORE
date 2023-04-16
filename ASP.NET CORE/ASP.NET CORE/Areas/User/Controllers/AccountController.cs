using ASP.NET_CORE.Areas.Users.Models;
using System.Security.Claims;
using ASP.NET_CORE.DATA.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Scripting;
using ASP.NET_CORE.Areas.User.Models;
namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        Web_Core_DbContext context;
        private readonly IProduct _product;
        private IWebHostEnvironment _webHostEnvironment;
        public AccountController(IProduct product, IWebHostEnvironment webHostEnvironment, Web_Core_DbContext contexxt)
        {
            _product = product;
            _webHostEnvironment = webHostEnvironment;
            context = contexxt;
        }
        public IActionResult Account_Index()
        {
            return View();
        }

        public IActionResult Login_Page()
        {
            if (TempData["messs"] != null)
                ViewBag.message = "successs";
            return View();
        }
        // login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login_Page(LoginModel user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (ModelState.IsValid)
            {

                List<Client> client = context.Clients.Where(c => c.Account == user.Username && c.Password == user.Password).ToList();
                if (client.Count == 1)
                {
                    //List<Claim> claims = new List<Claim>()
                    //{
                    //    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    //    new Claim("OtherProperties", "Example Role")
                    //};
                    //ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    //    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //AuthenticationProperties properties = new AuthenticationProperties()
                    //{
                    //    AllowRefresh = true,
                    //    IsPersistent = user.KeepLoggedIn
                    //};
                    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    //    new ClaimsPrincipal(claimsIdentity), properties);
                    TempData["mess"] = "success";
                    HttpContext.Session.SetString("Username", user.Username);
                    return RedirectToAction("Index", "HomePage");
                }
                else
                {
                    ViewBag.message = "login fail";
                    return View();
                }

            }
            else
            {
                return View();

            }
        }

        // register
        public IActionResult Sign_Up_Page()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sign_Up_Page(RegisterModel user)
        {
            // validate input
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                // check if username is already taken
                if (context.Clients.Any(u => u.Account == user.Account))
                {
                    ModelState.AddModelError("Account", "Username is already taken");
                    //ViewBag.message = "register fail";
                    return View(user);
                }

                // check if email is already registered
                if (context.Clients.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email is already registered");
                    return View(user);
                }
                // Hash the password
                //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                //user.Password = hashedPassword;
                // create a new Client entity and map the properties
                var client = new Client
                {
                    Account = user.Account,
                    Email = user.Email,
                    Password = user.Password,
                    Name = user.Name,
                    Sdt = user.Sdt,
                    Sex = user.Sex,
                    Address = user.Address,
                    Img = user.Img
                };
                // save user to database
                context.Clients.Add(client);
                context.SaveChanges();

                // redirect to login page
                TempData["messs"] = "successs";
                return RedirectToAction("Login_Page", "Account");
            }
        }
    }

}
