using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.Configuration;
using ASP.NET_CORE.DATA.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CORE.DATA.EF
{
    public class Web_Core_DbContext : DbContext
    {
        public Web_Core_DbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Admin_Configuration());

            modelBuilder.ApplyConfiguration(new Cart_Configuration());

            modelBuilder.ApplyConfiguration(new Category_Configuration());

            modelBuilder.ApplyConfiguration(new Detail_Discount_Configuration());

            modelBuilder.ApplyConfiguration(new Detail_Order_Configuration());

            modelBuilder.ApplyConfiguration(new Img_Configuration());

            modelBuilder.ApplyConfiguration(new Order_Configuration());

            modelBuilder.ApplyConfiguration(new Product_Configuration());

            modelBuilder.ApplyConfiguration(new Role_Configuration());

            modelBuilder.ApplyConfiguration(new User_Configuration());
        }

        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<DetailDiscount> DetailDiscounts { get; set; }

        public virtual DbSet<DetailOrder> DetailOrders { get; set; }

        public virtual DbSet<Discount> Discounts { get; set; }

        public virtual DbSet<Img> Imgs { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<User> Users { get; set; }
    }
}
