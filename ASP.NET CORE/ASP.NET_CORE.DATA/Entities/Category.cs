using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ASP.NET_CORE.DATA.Entities;

namespace ASP.NET_CORE.DATA.Entities;

public partial class Category
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(500)]
    public string Name { get; set; } = null!;

    public int Status { get; set; }

    public int IsDeleted { get; set; }
    public ICollection<Product> Products { get; } = new List<Product>();
}
