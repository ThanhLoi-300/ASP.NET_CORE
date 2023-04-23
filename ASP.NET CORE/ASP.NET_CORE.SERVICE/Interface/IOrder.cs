using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.Entities;
using NHibernate.Engine;

namespace ASP.NET_CORE.SERVICE.Interface
{
    public interface IOrder
    {
        List<Order> get_Accounts_Have_Order();
        List<Order> get_Order_Of_Username(int clientId, int status);
        Task Change_Status(int order_Id, string action);
    }
}
