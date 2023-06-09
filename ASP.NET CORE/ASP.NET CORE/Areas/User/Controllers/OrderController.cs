﻿using System.Collections.Generic;
using ASP.NET_CORE.DATA.EF;
using ASP.NET_CORE.DATA.Entities;
using ASP.NET_CORE.Models;
using ASP.NET_CORE.SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NHibernate.Mapping;
using NHibernate.Proxy;

namespace ASP.NET_CORE.Areas.User.Controllers
{
    [Area("User")]
    public class OrderController : Controller
    {
        Web_Core_DbContext context;
        private readonly IProduct _product;
        private IWebHostEnvironment _webHostEnvironment;
        public OrderController(IProduct product, IWebHostEnvironment webHostEnvironment, Web_Core_DbContext contexxt)
        {
            _product = product;
            _webHostEnvironment = webHostEnvironment;
            context = contexxt;
        }
        public IActionResult Order_Manage()
        {
            var username = Static.User;
           // ViewBag.user = username;
            if (username != null)
            {
                List<Order> order = context.Orders.Include(i => i.DetailOrders).ThenInclude(i => i.Product).ThenInclude(i => i.DetailDiscounts).ThenInclude(i => i.Discount).Where(i => i.ClientId == int.Parse(username)).ToList();
                return View(order);
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public IActionResult Add_Order(string address, decimal total, int idClient, decimal sum_Price)
        {
            var username = Static.User;
            DateTime now = DateTime.Now;
            DateTime recieveTime = now.AddDays(3);

            //string formattedDateTime = now.ToString("dd/MM/yyyy HH:mm:ss");
    

            List<Cart> cart = context.Carts.Include(c => c.Product).ThenInclude(c => c.DetailDiscounts).ThenInclude(c => c.Discount).Where(c => c.ClientId == int.Parse(username)).ToList();

            Order order = new Order();
            order.ClientId = idClient;
            order.Address = address;
            order.Total = total;
            order.Total_After_Discount = sum_Price;
            order.OrderTime = now;
            order.RecieveTime = recieveTime;
            order.Status = 0;
            order.UserId = "ff045d07-be86-4a4e-bfa4-0264ec832c12";
            context.Orders.Add(order);
            context.SaveChanges();
            var idOrder = context.Orders.Where(c => c.OrderTime == now).FirstOrDefault().Id;
            foreach (var item in cart)
            {
                List<DetailDiscount> percent = null;
                var rs = 0;
                if (item.Product.DetailDiscounts.Count > 0)
                {
                    percent = item.Product.DetailDiscounts.Where(d => d.Discount.Status == "Đang kích hoạt").ToList();
                    if (percent != null && percent.Count > 0) rs = percent[0].Discount.Value;
                }

                DetailOrder detailOrder = new DetailOrder();
                detailOrder.ProductId = item.ProductId;
                detailOrder.Quantity = item.Quantity;
                detailOrder.Size = item.Size;
                detailOrder.OrderId = idOrder;
                detailOrder.Price = item.Product.Price * item.Quantity;
                detailOrder.PercentDiscount = rs;
                context.DetailOrders.Add(detailOrder);
              
            }
            context.SaveChanges();
            return Json(new { success = true });
        }
        // huy don
        [HttpPost]
        public IActionResult Cancel_Oder(int idOder)
        {
            Order statusNew = context.Orders.Where(i => i.Id == idOder).FirstOrDefault();
            statusNew.Status = -1;
            context.Orders.Update(statusNew);
            context.SaveChanges();
            return Json(new { success = true });
        }
        // nhan hang thanh cong
        [HttpPost]
        public IActionResult Confirm_Oder(int idOder)
        {
            Order statusNew = context.Orders.Where(i => i.Id == idOder).FirstOrDefault();
            statusNew.Status = 2;
            context.Orders.Update(statusNew);
            context.SaveChanges();
            return Json(new { success = true });
        }
    }
}
