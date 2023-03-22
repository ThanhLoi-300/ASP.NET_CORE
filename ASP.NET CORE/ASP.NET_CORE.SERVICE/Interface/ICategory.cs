using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.Entities;

namespace ASP.NET_CORE.SERVICE.Interface
{
    public interface ICategory
    {
        int count_Category_Items(String search);
        List<Category> data_In_Page(int page, int page_Size, String search);
        Category get_Category_By_Id(int id);
        bool check_Name(string name, int id);
        Task Create_Category_Async(Category category);
        Task Update_Category_Async(Category category);
        string Loai_Dau(string s);
    }
}
