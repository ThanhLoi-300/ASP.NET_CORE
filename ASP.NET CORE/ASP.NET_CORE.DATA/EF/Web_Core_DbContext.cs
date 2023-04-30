using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.Configuration;
using ASP.NET_CORE.DATA.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_CORE.DATA.EF
{
    public class Web_Core_DbContext : IdentityDbContext<IdentityUser,IdentityRole,string>
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

            //modelBuilder.ApplyConfiguration(new Role_Configuration());

            modelBuilder.ApplyConfiguration(new Client_Configuration());

            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserClaim<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();

            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "2ca8fa08-4a80-4714-a5fb-17b7316fddc4",
                Name = "Admin",
                NormalizedName = "ADMIN".ToUpper()
            },
            new IdentityRole
            {
                Id = "88ac3925-8432-4f60-89e2-96433d08bbcf",
                Name = "User",
                NormalizedName = "USER".ToUpper()
            }
            );
            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityUser>().HasData(
               new IdentityUser
               {
                   Id = "ff045d07-be86-4a4e-bfa4-0264ec832c12",
                   UserName = "admin",
                   NormalizedUserName = "SUPER ADMIN".ToUpper(),
                   Email = "admin@gmail.com",
                   NormalizedEmail = "ADMIN@GMAIL.COM".ToUpper(),
                   PasswordHash = hasher.HashPassword(null, "123")
               }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(

                new IdentityUserRole<string>
                {
                    UserId = "ff045d07-be86-4a4e-bfa4-0264ec832c12",
                    RoleId = "2ca8fa08-4a80-4714-a5fb-17b7316fddc4"
                }
            );

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

        //public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
    }
}
