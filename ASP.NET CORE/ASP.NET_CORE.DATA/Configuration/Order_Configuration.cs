﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_CORE.DATA.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASP.NET_CORE.DATA.Configuration
{
    public class Order_Configuration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.ToTable("Order");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Address).IsRequired();
            entity.Property(e => e.OrderTime).IsRequired();
            entity.Property(e => e.RecieveTime).IsRequired();
            entity.Property(e => e.Total).IsRequired();
            entity.Property(e => e.Total_After_Discount).IsRequired();
            entity.Property(e => e.ClientId).IsRequired();
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.Status).IsRequired();

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Client");
        }
    }
}
