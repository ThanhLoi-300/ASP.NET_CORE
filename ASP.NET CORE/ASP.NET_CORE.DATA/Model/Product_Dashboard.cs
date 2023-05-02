using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_CORE.DATA.Model
{
    public class Product_Dashboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        //public int M { get; set; }
        //public int L { get; set; }
        //public int Xl { get; set; }
        public decimal Total_Price { get; set; }
        public decimal Price { get; set; }
        //public int Total_Sale { get; set; }
        public int Quantity { get; set; }
    }
}
