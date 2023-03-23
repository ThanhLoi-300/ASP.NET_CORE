using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_CORE.DATA.Entities;

public partial class Product
{
    public Product()
    {
        Carts = new HashSet<Cart>();
        Detail_Discount = new HashSet<DetailDiscount>();
        DetailOrders = new HashSet<DetailOrder>();
        Imgs = new HashSet<Img>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Required, Column(TypeName ="decimal(18,2)")]
    public decimal Price { get; set; }

    public int QuantityS { get; set; }

    public int QuantityM { get; set; }

    public int QuantityL { get; set; }

    public int QuantityXl { get; set; }

    [Required]
    [StringLength(255)]
    public string Img { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    [Required]
    [StringLength(255)]
    public string Type { get; set; } = null!;

    public int Status { get; set; }

    public int IsDeleted { get; set; }

    [ForeignKey("Category")]
    public int Category_ID { get; set; }
    public Category Category { get; set; } = null!;

    public ICollection<Cart> Carts { get; set; }

    public ICollection<DetailDiscount> Detail_Discount { get; set; }

    public ICollection<DetailOrder> DetailOrders { get; set; }

    public ICollection<Img> Imgs { get; set; }
    public ICollection<DetailDiscount> DetailDiscounts { get; set; } = new List<DetailDiscount>();
}
