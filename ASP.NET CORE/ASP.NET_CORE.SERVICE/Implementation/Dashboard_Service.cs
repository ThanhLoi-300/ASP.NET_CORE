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

        public List<Product_Dashboard> Best_Seller()
        {
            List<Product_Dashboard> product_Dashboards = (from o in _context.Orders.Where(x => x.Status == 2)
                                                          join od in _context.DetailOrders on o.Id equals od.OrderId
                                                          join p in _context.Products on od.ProductId equals p.Id
                                                          group new { od.Price, od.Quantity } by new { p.Id, p.Img, p.Name, p.Price } into g
                                                          orderby g.Sum(x => x.Quantity) descending, g.Key.Id
                                                          select new Product_Dashboard
                                                          {
                                                              Id = g.Key.Id,
                                                              Total_Price = g.Sum(x => x.Price),
                                                              Quantity = g.Sum(x => x.Quantity),
                                                              Img = g.Key.Img,
                                                              Name = g.Key.Name,
                                                              Price = g.Key.Price,
                                                          }).Take(7).ToList();
            return product_Dashboards;
        }

        public async Task Change(Discount d, List<int> choose)
        {
            Discount dc = Edit_Discount(d.Id);
            dc.Value = d.Value;
            dc.StartTime = d.StartTime;
            dc.EndTime = d.EndTime;

            var list_detail_Discount = _context.DetailDiscounts.Where(i => i.DiscountId == d.Id).ToList();
            if (list_detail_Discount.Count > 0) _context.DetailDiscounts.RemoveRange(list_detail_Discount);

            foreach(var i in choose)
            {
                _context.DetailDiscounts.Add(new DetailDiscount
                {
                    DiscountId = d.Id,
                    ProductId = i
                });
            }
            await _context.SaveChangesAsync();
        }

        public bool Check_Value(int value, int? id)
        {
            List<Discount> list = null;
            if (id == null)
                list = _context.Discounts.Where(i => i.Value == value).ToList();
            else list = _context.Discounts.Where(i => i.Value == value && i.Id != id).ToList();

            if (list.Count > 0) return true;
            return false;

        }

        public int count_Product()
        {
            return _context.Products.ToList().Count;
        }

        public int count_User()
        {
            return _context.Clients.ToList().Count;
        }

        public Discount Edit_Discount(int id)
        {
            return _context.Discounts.Where(i => i.Id == id).FirstOrDefault();
        }

        public async Task<List<Discount>> Get_All_DiscountAsync()
        {
            return await _context.Discounts.ToListAsync();
        }

        public List<Product_Dashboard> get_List(DateTime start, DateTime end)
        {
            List<Product_Dashboard> product_Dashboards = (from o in _context.Orders.Where(x => x.Status == 2)
                          join od in _context.DetailOrders on o.Id equals od.OrderId
                          join p in _context.Products on od.ProductId equals p.Id
                          where o.OrderTime >= start && o.OrderTime <= end.AddDays(1)
                          group new { od.Price, od.Quantity } by new { p.Id, p.Img, p.Name, p.Price } into g
                          orderby g.Key.Id
                          select new Product_Dashboard
                          {
                              Total_Price = g.Sum(x => x.Price),
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

        public Task<List<Discount>> Get_Detail_Of_DiscountAsync(int id)
        {
            List<Discount> list = _context.Discounts.Include(i => i.DetailDiscounts).ThenInclude(a => a.Product).Where(i => i.Id == id && i.IsDeleted == 0).ToList();
            return Task.FromResult(list);
        }

        public async Task Insert_Discount(Discount d)
        {
            _context.Discounts.Add(d);
            await _context.SaveChangesAsync();
        }

        public async Task Update_Discount()
        {
            var d = _context.Discounts.ToList();
            DateTime now = DateTime.Now;

            foreach(var i in d)
            {
                int start = DateTime.Compare(now.Date, i.StartTime.Date);
                int end = DateTime.Compare(now.Date, i.EndTime.Date);
                if (start >= 0 && end <= 0)
                {
                    i.Status = "Đang kích hoạt";
                }
                else i.Status = "Ngừng kích hoạt";
            }

            await _context.SaveChangesAsync();
        }

        public List<Product> Get_Product_Remaining()
        {
            var list_Id_Product_Discounting = _context.DetailDiscounts.Include(i => i.Product).Select(i => i.ProductId);

            return _context.Products.Where(i => !list_Id_Product_Discounting.Contains(i.Id)).ToList();
        }
    }
}
