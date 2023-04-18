using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CORE.Areas.User.Models
{
	public class EditProfileModel
	{
        [Required, MaxLength(50)]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Name can not blank."), MaxLength(90)]
        public string FullName { get; set; } = null!;
        [Required(ErrorMessage = "Address can not blank."), MaxLength(500)]
        public string Address { get; set; } = null!;
        [MaxLength(50)]
        public string? Image { get; set; }
        [Required(ErrorMessage = "Email can not blank."), MaxLength(90)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "SĐT can not blank.")]
        public int Phone { get; set; }

        public int Sex { get; set; }

    }
}

