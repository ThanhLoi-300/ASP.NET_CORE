using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ASP.NET_CORE.DATA;

namespace ASP.NET_CORE.DATA.Entities;

public partial class Admin
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Account { get; set; } = null!;
    [Required, MaxLength(50)]
    public string Password { get; set; } = null!;
    [Required, MaxLength(50)]
    public string Name { get; set; } = null!;
    [Required, MaxLength(90)]
    public string Email { get; set; } = null!;
    [ForeignKey("Role")]
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    //public List<Admin_In_Role>? Admin_In_Roles { get; set; }
    //public virtual Role Role { get; set; } = null!;
}
