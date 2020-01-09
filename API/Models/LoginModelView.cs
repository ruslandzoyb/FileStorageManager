using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class LoginModelView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Password is not of corect size")]
        public string Password { get; set; }
    }
}
