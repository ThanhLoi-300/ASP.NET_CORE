using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_CORE.Areas.Users.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username can not blank.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password can not blank.")]
        public string Password { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}