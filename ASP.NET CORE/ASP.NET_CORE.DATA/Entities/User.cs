using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CORE.DATA.Entities;

public partial class User
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Account { get; set; } = null!;
    [Required, MaxLength(50)]
    public string Password { get; set; } = null!;
    [Required, MaxLength(90)]
    public string Name { get; set; } = null!;
    [Required, MaxLength(500)]
    public string Address { get; set; } = null!;
    [MaxLength(50)]
    public string? Img { get; set; }
    [Required, MaxLength(90)]
    public string Email { get; set; } = null!;

    public int Sdt { get; set; }

    public int Sex { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
