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
using Microsoft.AspNetCore.Http;
using ASP.NET_CORE.Models;
using System.Web;
using System.Security.Cryptography;
using System.Text;

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
        // update profile
        public IActionResult Account_Index()
        {
            // Lấy thông tin người dùng từ session
            //var userName = HttpContext.Session.GetString("Username");
            var userName = Static.User;

            if (string.IsNullOrEmpty(userName))
            {
                // Nếu session không có giá trị, chuyển hướng về trang đăng nhập
                return RedirectToAction("Login_Page", "Account");
            }

            // Lấy thông tin người dùng từ database theo username
            var user = context.Clients.FirstOrDefault(u => u.Id == int.Parse(userName));
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.user = Static.User;
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Account_Index(Client model)
        {
            // Kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                // Lưu thông tin người dùng chỉnh sửa vào database
                var user = context.Clients.FirstOrDefault(u => u.Account == model.Account);
                if (user == null)
                {
                    return NotFound(user);
                }
                // check maill
                var checkMail = context.Clients.FirstOrDefault(u => u.Email == model.Email && u.Account != model.Account);

                if (checkMail != null)
                {
                    ModelState.AddModelError("Email", "This Email is already in use.");
                    return View(model);
                }
                user.Account = user.Account;
                user.Password = user.Password;
                user.Name = model.Name;
                user.Address = model.Address;
                user.Email = model.Email;
                user.Sdt = model.Sdt;
                user.Sex = model.Sex;

                context.SaveChanges();
                TempData["messss"] = "successss";
                return RedirectToAction("Index", "HomePage");
            }

        }


        public IActionResult Login_Page()
        {
            ViewBag.user = Static.User;
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

                List<Client> client = context.Clients.Where(c => c.Account == user.Username /*&& c.Password == user.Password*/).ToList();
                if (client.Count == 1)
                {
                    // verify the password hash
                    if (client[0].Password == GetSHA256Hash(user.Password))
                    {
                        // password is valid
                        TempData["mess"] = "success";
                        //HttpContext.Session.SetString("Username", client[0].Id+"");
                        //ViewBag.user = HttpContext.Session.GetString("Username");
                        ViewBag.user = client[0].Id;
                        Static.User = client[0].Id + "";
                        return RedirectToAction("Index", "HomePage");
                    }
                    else
                    {
                        // password is invalid
                        ViewBag.message = "login fail";
                        return View();
                    } 
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
        // logout
        public IActionResult Logout()
        {
            // Xoá session
            //HttpContext.Session.Clear();
            Static.User = "";

            // Điều hướng đến trang đăng nhập
            return RedirectToAction("Login_Page", "Account");
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
                user.Password = GetSHA256Hash(user.Password);
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

        public static string GetSHA256Hash(string input)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }

}
