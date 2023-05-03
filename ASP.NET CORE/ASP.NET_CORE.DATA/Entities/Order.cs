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

    [Column(TypeName = "decimal(18,2)")]
    public decimal Total_After_Discount { get; set; }

    public DateTime OrderTime { get; set; }

    public DateTime RecieveTime { get; set; }

    public int Status { get; set; }
    [ForeignKey("Users")]
    public string UserId { get; set; }

    public ApplicationUser Users { get; set; } = null!;

    [ForeignKey("Client")]
    public int ClientId { get; set; }
    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<DetailOrder> DetailOrders { get; set; }
}
