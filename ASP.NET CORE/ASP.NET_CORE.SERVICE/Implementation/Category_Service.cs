using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.SERVICE.Interface;
using NHibernate.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ASP.NET_CORE.SERVICE.Implementation
{
    public class Category_Service : ICategory
    {
        private Web_Core_DbContext _context;
        private List<Category> list;


        public Category_Service(Web_Core_DbContext context)
        {
            _context = context;
            list = new List<Category>();
        }

        public bool check_Name(string name, int id)
        {
            if (id == -1)
                list = _context.Categories.Where(cate => cate.Name.Equals(name)).ToList();
            else
                list = _context.Categories.Where(cate => cate.Name.Equals(name) && cate.Id != id).ToList();
            if (list.Count == 0) return true;
            return false;
        }

        public int count_Category_Items( String search)
        {
            IEnumerable<Category> list = _context.Categories.Where(cate => cate.IsDeleted == 0);

            if (!String.IsNullOrEmpty(search))
            {
                list = list.Where(cate => Loai_Dau(cate.Name.ToLower()).Contains(Loai_Dau(search.ToLower())));
            }

            return list.OrderByDescending(cate => cate.Id).ToList().Count();
        }

        public async Task Create_Category_Async(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public List<Category> data_In_Page(int page, int page_Size, string search)
        {
            if (page < 1)
                page = 1;

            int skip = (page - 1) * page_Size;
            IEnumerable<Category> list = _context.Categories.Where(cate => cate.IsDeleted == 0);

            if (!String.IsNullOrEmpty(search))
            {
                list = list.Where(cate => Loai_Dau(cate.Name.ToLower()).Contains(Loai_Dau(search.ToLower())));
            }

            return list.OrderByDescending(cate => cate.Id).Skip(skip).Take(page_Size).ToList();
        }

        public Category get_Category_By_Id(int id)
        {
            return _context.Categories.Where(cate => cate.Id == id).FirstOrDefault();
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

        public async Task Update_Category_Async(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
