using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_CORE.DATA.Entities;

public partial class DetailOrder
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public string Size { get; set; } = null!;

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public int PercentDiscount { get; set; }
}
