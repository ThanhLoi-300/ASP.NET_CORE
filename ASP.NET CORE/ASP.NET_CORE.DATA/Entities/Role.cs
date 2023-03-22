using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ASP.NET_CORE.DATA.Entities;

namespace ASP.NET_CORE.DATA.Entities;

public partial class Role
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Name { get; set; } = null!;
    [Required, MaxLength(1000)]
    public string Description { get; set; } = null!;
    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

}
