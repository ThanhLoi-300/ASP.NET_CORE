﻿
using ASP.NET_CORE.DATA.Entities;
using Microsoft.AspNetCore.Http;
using NHibernate.Type;

namespace ASP.NET_CORE.SERVICE.Interface
{
    public interface IProduct
    {
        int count_Product_Items(String search, int category_Id);
        int count_Product_Items(String search, List<string>? category_Id, List<String> type, double price, string sort);
        List<Product> data_In_Page(int page, int page_Size, String search, int category_Id);
        List<Product> data_In_Page(int page, int page_Size, string search, List<string>? category_Id, List<String> Type, double price, string sort);
        Product get_Product_By_Id(int id);
        bool check_Name(string name, int id);
        Task Create_Product_Async(Product product);
        Task Update_Product_Async(Product product);
        string Loai_Dau(string s);
        List<Category> List_Category();
        List<Img> List_Img_Of_Product(int id);
        Task Delete_Img(int id);
        Task Add_Img(int id, List<string> url);
        void Delete_All_SubImg(int id);
        Task Change_Main_Img(int id, int id_Product);
        List<Product> get_Product_Relationship(int id);
    }
}
