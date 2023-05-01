using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.DATA.Model;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CORE.SERVICE.Implementation
{
    public class Dashboard_Service : IDashboard
    {
        private readonly Web_Core_DbContext _context;

        public Dashboard_Service(Web_Core_DbContext context)
        {
            _context = context;
        }

        public int count_Product()
        {
            return _context.Products.ToList().Count;
        }

        public int count_User()
        {
            return _context.Clients.ToList().Count;
        }

        public List<Product_Dashboard> get_List(DateTime start, DateTime end)
        {
            List<Product_Dashboard> product_Dashboards = (from o in _context.Orders
                          join od in _context.DetailOrders on o.Id equals od.OrderId
                          join p in _context.Products on od.ProductId equals p.Id
                          where o.OrderTime >= start && o.OrderTime <= end.AddDays(1)
                          group new { od.Price, od.Quantity } by new { p.Id, p.Img, p.Name, p.Price } into g
                          orderby g.Key.Id
                          select new Product_Dashboard
                          {
                              Total_Price = g.Sum(x => x.Price * x.Quantity),
                              Quantity = g.Sum(x => x.Quantity),
                              Img = g.Key.Img,
                              Name = g.Key.Name,
                              Price = g.Key.Price,
                          }).ToList();


            return product_Dashboards;
        }

        public decimal get_Total(DateTime start, DateTime end)
        {
            List<Product_Dashboard> product_Dashboards = (from o in _context.Orders
                                                          join od in _context.DetailOrders on o.Id equals od.OrderId
                                                          join p in _context.Products on od.ProductId equals p.Id
                                                          where o.OrderTime >= start && o.OrderTime <= end.AddDays(1)
                                                          group new { od.Price, od.Quantity } by new { p.Id, p.Img, p.Name, p.Price } into g
                                                          orderby g.Key.Id
                                                          select new Product_Dashboard
                                                          {
                                                              Total_Price = g.Sum(x => x.Price * x.Quantity),
                                                              Quantity = g.Sum(x => x.Quantity),
                                                              Img = g.Key.Img,
                                                              Name = g.Key.Name,
                                                              Price = g.Key.Price,
                                                          }).ToList();
            Decimal total = 0;
            foreach(var i in product_Dashboards)
            {
                total += i.Total_Price;
            }
            return total;
        }
    }
}
