using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_CORE.DATA.Entities;

public partial class Cart
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    [ForeignKey("Client")]
    public int ClientId { get; set; }
    public virtual Client Client { get; set; } = null!;

    public string Size { get; set; } = null!;

    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}
