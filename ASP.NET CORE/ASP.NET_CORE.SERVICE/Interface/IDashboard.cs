using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.DATA.Model;

namespace ASP.NET_CORE.SERVICE.Interface
{
    public interface IDashboard
    {
        List<Product_Dashboard> get_List(DateTime start, DateTime end);
        decimal get_Total(DateTime start, DateTime end);
        int count_User();
        int count_Product();
        Task Insert_Discount(Discount d);
        Task Update_Discount();
        Task<List<Discount>> Get_All_DiscountAsync();
        bool Check_Value(int value,int? id);
        List<Product_Dashboard> Best_Seller();
        Discount Edit_Discount(int id);
        Task Change(Discount d, List<int> choose);
        Task<List<Discount>> Get_Detail_Of_DiscountAsync(int id);
        List<Product> Get_Product_Remaining();
    }
}
