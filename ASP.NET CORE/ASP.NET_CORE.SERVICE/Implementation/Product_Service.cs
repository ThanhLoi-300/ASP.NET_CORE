using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using NHibernate.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ASP.NET_CORE.SERVICE.Implementation
{
    public class Product_Service : IProduct
    {
        private readonly Web_Core_DbContext _context;
        private List<Product> list;

        public Product_Service(Web_Core_DbContext context)
        {
            _context = context;
            list = new List<Product>();
        }

        public async Task Add_Img(int id, List<string> url)
        {
            foreach(var i in url)
            {
                _context.Imgs.Add(new Img
                {
                    ImgProduct = i,
                    ProductId = id,
                    SubImg = 0
                });
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task Change_Main_Img(int id, int id_Product)
        {
            var list_Img = _context.Imgs.ToList();
            var sub_Img = list_Img.Find(i => i.Id == id && i.ProductId == id_Product);
            sub_Img.SubImg = 1;

            var main_Img = _context.Imgs.FirstOrDefault(i => i.ProductId == sub_Img.ProductId && i.SubImg == 1);
            main_Img.SubImg = 0;

            var product = _context.Products.FirstOrDefault(i => i.Id == sub_Img.ProductId);
            product.Img = sub_Img.ImgProduct;

            await _context.SaveChangesAsync();
        }

        public bool check_Name(string name, int id)
        {
            if (id == -1)
                list = _context.Products.Where(product => product.Name.Equals(name)).ToList();
            else
                list = _context.Products.Where(product => product.Name.Equals(name) && product.Id != id).ToList();
            if (list.Count == 0) return true;
            return false;
        }

        public int count_Product_Items(string search, int category_Id)
        {
            IEnumerable<Product> list = _context.Products.Where(product => product.IsDeleted == 0);

            if (!String.IsNullOrEmpty(search))
            {
                list = list.Where(product => Loai_Dau(product.Name.ToLower()).Contains(Loai_Dau(search.ToLower())));
            }

            if(category_Id != -1)
            {
                list = list.Where(product => product.Category_ID == category_Id);
            }

            return list.OrderByDescending(product => product.Id).ToList().Count();
        }

        public int count_Product_Items(string search, List<string>? category_Id, List<String> type, double price, string sort)
        {
            IEnumerable<Product> list = _context.Products.Where(product => product.IsDeleted == 0 && product.Status == 1);

            if (category_Id != null && category_Id.Count > 0)
                list = list.Where(p => category_Id.Contains(p.Category_ID.ToString())).ToList();

            if (type != null && type.Count > 0)
                list = list.Where(p => type.Contains(p.Type)).ToList();

            if (!String.IsNullOrEmpty(search))
                list = list.Where(product => Loai_Dau(product.Name.ToLower()).Contains(Loai_Dau(search.ToLower())));

            if (price > 0)
                list = list.Where(p => p.Price <= Decimal.Parse(price.ToString())).ToList();

            if (sort != null)
            {
                if (sort.Equals("up"))
                    list = list.OrderBy(p => p.Price).ToList();
                else if (sort.Equals("down"))
                    list = list.OrderByDescending(p => p.Price).ToList();
            }

            return list.ToList().Count();
        }

        public async Task Create_Product_Async(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public List<Product> data_In_Page(int page, int page_Size, string search, int category_Id)
        {
            if (page < 1)
                page = 1;

            int skip = (page - 1) * page_Size;
            IEnumerable<Product> list = _context.Products.Where(product => product.IsDeleted == 0);

            if (!String.IsNullOrEmpty(search))
            {
                list = list.Where(product => Loai_Dau(product.Name.ToLower()).Contains(Loai_Dau(search.ToLower())));
            }

            if (category_Id != -1)
            {
                list = list.Where(product => product.Category_ID == category_Id);
            }

            return list.OrderByDescending(product => product.Id).Skip(skip).Take(page_Size).ToList();
        }

        public List<Product> data_In_Page(int page, int page_Size, string search, List<string>? category_Id, List<string> type, double price, string sort)
        {
            if (page < 1)
                page = 1;

            int skip = (page - 1) * page_Size;
            IEnumerable<Product> list = _context.Products.Where(product => product.IsDeleted == 0 && product.Status == 1);

            if (!String.IsNullOrEmpty(search))
                list = list.Where(product => Loai_Dau(product.Name.ToLower()).Contains(Loai_Dau(search.ToLower())));

            if (category_Id != null && category_Id.Count > 0)
                list = list.Where(p => category_Id.Contains(p.Category_ID.ToString()));

            if (type != null && type.Count > 0)
                list = list.Where(p => type.Contains(p.Type));

            if (price > 0)
                list = list.Where(p => p.Price <= Decimal.Parse(price.ToString()));

            if(sort != null)
            {
                if (sort.Equals("up"))
                    list = list.OrderBy(p => p.Price);
                else if (sort.Equals("down"))
                    list = list.OrderByDescending(p => p.Price);
            } 

            return list.Skip(skip).Take(page_Size).ToList();
        }

        public void Delete_All_SubImg(int id)
        {
            var list_Img_Is_Deleted = _context.Imgs.Where(i => i.ProductId == id && i.SubImg == 0).ToList();
            _context.Imgs.RemoveRange(list_Img_Is_Deleted);
            _context.SaveChangesAsync();
        }

        public async Task Delete_Img(int id)
        {
            var item = _context.Imgs.Find(id);
            _context.Imgs.Remove(item);
            await _context.SaveChangesAsync();
        }

        public Product get_Product_By_Id(int id)
        {
            return _context.Products.Where(product => product.Id == id).FirstOrDefault();
        }

        public List<Category> List_Category()
        {
            return _context.Categories.Where(c => c.IsDeleted == 0 && c.Status == 1).OrderBy(c => c.Name).ToList();
        }

        public List<Img> List_Img_Of_Product(int id)
        {
            return _context.Imgs.Where(p => p.ProductId  == id).OrderByDescending(p => p.SubImg).ToList();
        }

        public string Loai_Dau(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                                            "đ",
                                            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                                            "í","ì","ỉ","ĩ","ị",
                                            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                                            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                                            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                                            "d",
                                            "e","e","e","e","e","e","e","e","e","e","e",
                                            "i","i","i","i","i",
                                            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                                            "u","u","u","u","u","u","u","u","u","u","u",
                                            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        public async Task Update_Product_Async(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
