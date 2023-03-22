using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ASP.NET_CORE.DATA.Entities;

namespace ASP.NET_CORE.DATA.Entities;

public partial class Img
{
    [Key]
    public int Id { get; set; }
    [Required,MaxLength(50)]
    public string? ImgProduct { get; set; }
    public int SubImg { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
