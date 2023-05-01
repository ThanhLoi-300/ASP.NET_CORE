using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.Model;

namespace ASP.NET_CORE.SERVICE.Interface
{
    public interface IDashboard
    {
        List<Product_Dashboard> get_List(DateTime start, DateTime end);
        decimal get_Total(DateTime start, DateTime end);
        int count_User();
        int count_Product();
    }
}
