using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.EntityFrameworkCore;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Mapping;
using Order = ASP.NET_CORE.DATA.Entities.Order;

namespace ASP.NET_CORE.SERVICE.Implementation
{
    public class Order_Service : IOrder
    {
        private Web_Core_DbContext _context;

        public Order_Service(Web_Core_DbContext context)
        {
            _context = context;
        }

        public async Task Change_Status(int order_Id)
        {
            var o = _context.Orders.Where(o => o.Id == order_Id).FirstOrDefault();
            o.Status = 1;
            var list_Order = _context.DetailOrders.Where(o => o.OrderId == order_Id).Include(o => o.Product).ToList();
            foreach (var item in list_Order)
            {
                if (item.Size.Equals("S"))
                    item.Product.QuantityS -= item.Quantity;
                if (item.Size.Equals("M"))
                    item.Product.QuantityM -= item.Quantity;
                if (item.Size.Equals("L"))
                    item.Product.QuantityL -= item.Quantity;
                if (item.Size.Equals("Xl"))
                    item.Product.QuantityXl -= item.Quantity;
            }

            //_context.Orders.Update(o);
            await _context.SaveChangesAsync();     
        }

        public List<Order> get_Accounts_Have_Order()
        {
            var list =  _context.Orders.Include(a => a.Client).ToList();
            return list;
        }

        public List<Order> get_Order_Of_Username(int clientId, int status)
        {
            return _context.Orders.Include(a => a.DetailOrders).ThenInclude(o => o.Product).ThenInclude(o => o.DetailDiscounts).ThenInclude(o => o.Discount).Where(a => a.ClientId == clientId && a.Status == status).ToList();
        }
    }
}
