using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.DATA.Model;
using ASP.NET_CORE.SERVICE.Interface;

namespace ASP.NET_CORE.SERVICE.Implementation
{
    public class Login_Service : ILogin
    {
        private readonly Web_Core_DbContext _context;

        public Login_Service(Web_Core_DbContext context)
        {
            _context = context;
        }
        public string Login(string? username, string? pass)
        {
            Admin admin = _context.Admins.FirstOrDefault(a => a.Account == username && a.Password == pass);
            if (admin != null)
            {
                return admin.Name;
            }
            else return "";
        }
    }
}
