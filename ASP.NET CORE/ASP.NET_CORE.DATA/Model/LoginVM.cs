using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_CORE.DATA.Model
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username cannot be blank")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password can not blank.")]
        public string Password { get; set; }
    }
}
