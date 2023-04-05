using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elasticsearch.Net;

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
    [Range(0, 999999999, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0.")]
    public int QuantityS { get; set; }
    [Range(0, 999999999, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0.")]
    public int QuantityM { get; set; }
    [Range(0, 999999999, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0.")]
    public int QuantityL { get; set; }
    [Range(0, 999999999, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0.")]
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
    public Category? Category { get; set; }

    public ICollection<Cart> Carts { get; set; }

    public ICollection<DetailDiscount> Detail_Discount { get; set; }

    public ICollection<DetailOrder> DetailOrders { get; set; }

    public ICollection<Img> Imgs { get; set; }
    public ICollection<DetailDiscount> DetailDiscounts { get; set; } = new List<DetailDiscount>();
}
