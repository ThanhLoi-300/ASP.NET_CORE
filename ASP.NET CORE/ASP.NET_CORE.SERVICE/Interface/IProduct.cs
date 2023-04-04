
using ASP.NET_CORE.DATA.Entities;
using Microsoft.AspNetCore.Http;

namespace ASP.NET_CORE.SERVICE.Interface
{
    public interface IProduct
    {
        int count_Product_Items(String search, int category_Id);
        List<Product> data_In_Page(int page, int page_Size, String search, int category_Id);
        Product get_Product_By_Id(int id);
        bool check_Name(string name, int id);
        Task Create_Product_Async(Product product);
        Task Update_Product_Async(Product product);
        string Loai_Dau(string s);
        List<Category> List_Category();
        List<Img> List_Img_Of_Product(int id);
        Task Delete_Img(int id);
        Task Add_Img(int id, List<string> url);
        Task Delete_All_SubImg(int id);
        string find_Img_By_Id(int id);
        Task Change_Main_Img(int id);
    }
}
