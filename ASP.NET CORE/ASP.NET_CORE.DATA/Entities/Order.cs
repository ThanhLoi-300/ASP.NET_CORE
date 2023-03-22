using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_CORE.DATA.Entities;

public partial class Order
{
    public Order()
    {
        DetailOrders = new HashSet<DetailOrder>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Address { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    public DateTime OrderTime { get; set; }

    public DateTime RecieveTime { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public virtual ICollection<DetailOrder> DetailOrders { get; set; }
}
