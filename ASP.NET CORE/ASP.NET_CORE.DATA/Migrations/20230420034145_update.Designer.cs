﻿// <auto-generated />
using System;
using ASP.NET_CORE.DATA.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASP.NET_CORE.DATA.Migrations
{
    [DbContext(typeof(Web_Core_DbContext))]
    [Migration("20230420034145_update")]
    partial class update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.1.23111.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProductId");

                    b.ToTable("Cart", (string)null);
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Img")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Sdt")
                        .HasColumnType("int");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("client", (string)null);
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.DetailDiscount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DiscountId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductId1");

                    b.ToTable("Detail_Discount", (string)null);
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.DetailOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PercentDiscount")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("Detail_Order", (string)null);
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Img", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImgProduct")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("SubImg")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Img", (string)null);
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecieveTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Category_ID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nchar(255)")
                        .IsFixedLength();

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("QuantityL")
                        .HasColumnType("int");

                    b.Property<int>("QuantityM")
                        .HasColumnType("int");

                    b.Property<int>("QuantityS")
                        .HasColumnType("int");

                    b.Property<int>("QuantityXl")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Category_ID");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Admin", b =>
                {
                    b.HasOne("ASP.NET_CORE.DATA.Entities.Role", "Role")
                        .WithMany("Admins")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Admin_Role");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Cart", b =>
                {
                    b.HasOne("ASP.NET_CORE.DATA.Entities.Client", "Client")
                        .WithMany("Carts")
                        .HasForeignKey("ClientId")
                        .IsRequired()
                        .HasConstraintName("FK_Cart_Client");

                    b.HasOne("ASP.NET_CORE.DATA.Entities.Product", "Product")
                        .WithMany("Carts")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Cart_Product");

                    b.Navigation("Client");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.DetailDiscount", b =>
                {
                    b.HasOne("ASP.NET_CORE.DATA.Entities.Discount", "Discount")
                        .WithMany("DetailDiscounts")
                        .HasForeignKey("DiscountId")
                        .IsRequired()
                        .HasConstraintName("FK_Detail_Discount_Discount");

                    b.HasOne("ASP.NET_CORE.DATA.Entities.Product", "Product")
                        .WithMany("DetailDiscounts")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Detail_Discount_Product");

                    b.HasOne("ASP.NET_CORE.DATA.Entities.Product", null)
                        .WithMany("Detail_Discount")
                        .HasForeignKey("ProductId1");

                    b.Navigation("Discount");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.DetailOrder", b =>
                {
                    b.HasOne("ASP.NET_CORE.DATA.Entities.Order", "Order")
                        .WithMany("DetailOrders")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_Detail_Order_Order");

                    b.HasOne("ASP.NET_CORE.DATA.Entities.Product", "Product")
                        .WithMany("DetailOrders")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Detail_Order_Product");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Img", b =>
                {
                    b.HasOne("ASP.NET_CORE.DATA.Entities.Product", "Product")
                        .WithMany("Imgs")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Img_Product");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Order", b =>
                {
                    b.HasOne("ASP.NET_CORE.DATA.Entities.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .IsRequired()
                        .HasConstraintName("FK_Order_Client");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Product", b =>
                {
                    b.HasOne("ASP.NET_CORE.DATA.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("Category_ID")
                        .IsRequired()
                        .HasConstraintName("FK_Product_Category");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Client", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Discount", b =>
                {
                    b.Navigation("DetailDiscounts");
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Order", b =>
                {
                    b.Navigation("DetailOrders");
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Product", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("DetailDiscounts");

                    b.Navigation("DetailOrders");

                    b.Navigation("Detail_Discount");

                    b.Navigation("Imgs");
                });

            modelBuilder.Entity("ASP.NET_CORE.DATA.Entities.Role", b =>
                {
                    b.Navigation("Admins");
                });
#pragma warning restore 612, 618
        }
    }
}