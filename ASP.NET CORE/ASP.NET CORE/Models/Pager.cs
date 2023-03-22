using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_CORE.Models
{
    public class Pager
    {
        public int Total_Item { get; set; }
        public int Current_Page { get; set; }
        public int Page_Size { get; set; }
        public int Total_Page { get; set; }
        public int Start_Page { get; set; }
        public int End_Page { get; set; }

        public Pager()
        {

        }

        public Pager(int total_Item, int page, int page_Size)
        {
            int totalPage = (int)Math.Ceiling((decimal)total_Item / (decimal)page_Size);
            int currentPage = page;

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPage)
            {
                endPage = totalPage;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            Total_Item = total_Item;
            Current_Page = currentPage;
            Page_Size = page_Size;
            Total_Page = totalPage;
            Start_Page = startPage;
            End_Page = endPage;
        }
    }
}