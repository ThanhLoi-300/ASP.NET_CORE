using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.DATA.Model;

namespace ASP.NET_CORE.SERVICE.Interface
{
    public interface ILogin
    {
        string Login(string? username, string? pass);
    }
}
