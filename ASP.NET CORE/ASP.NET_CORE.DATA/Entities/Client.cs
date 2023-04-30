using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_CORE.DATA.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Account { get; set; } = null!;
        [Required, MaxLength(250)]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Name can not blank."), MaxLength(90)]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Address can not blank."), MaxLength(500)]
        public string Address { get; set; } = null!;
        [MaxLength(50)]
        public string? Img { get; set; }
        [Required(ErrorMessage = "Email can not blank."), MaxLength(90)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "SĐT can not blank.")]
        public int Sdt { get; set; }

        public int Sex { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
