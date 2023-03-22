using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_CORE.DATA.Entities;

public partial class DetailDiscount
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Discount")]
    public int DiscountId { get; set; }
    public Discount Discount { get; set; } = null!;

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
