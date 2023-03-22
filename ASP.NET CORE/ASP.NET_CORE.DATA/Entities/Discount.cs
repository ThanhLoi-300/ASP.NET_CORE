using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CORE.DATA.Entities;

public partial class Discount
{
    [Key]
    public int Id { get; set; }

    public int Value { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
    [Required, MaxLength(50)]
    public string Status { get; set; } = null!;

    public int IsDeleted { get; set; }
    public ICollection<DetailDiscount> DetailDiscounts { get; set; } = new List<DetailDiscount>();
}
