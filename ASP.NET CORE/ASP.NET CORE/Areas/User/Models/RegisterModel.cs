using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CORE.Areas.User.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username can not blank.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string Account { get; set; } = null!;
        [Required(ErrorMessage = "Password can not blank.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Fullname can not blank.")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Address can not blank.")]
        public string Address { get; set; } = null!;
        [MaxLength(50)]
        public string? Img { get; set; }
        [Required(ErrorMessage = "Email can not blank.")]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "ConfirmPassword can not blank.")]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword do not match.")]
        public String ConfirmPassword { get; set; } = null!;
        [Required(ErrorMessage = "Sex can not blank.")]
        public int Sdt { get; set; }
        [Required(ErrorMessage = "Sex can not blank.")]
        public int Sex { get; set; }
    }
}