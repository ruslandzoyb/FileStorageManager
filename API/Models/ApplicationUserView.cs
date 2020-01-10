using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ApplicationUserView
    {
       // public int Id { get; set; }



        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; }



        [Required]
        
        [StringLength(15,MinimumLength =8,ErrorMessage ="Password is not of corect size")]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }



        [Required]
        [DataType(DataType.Text)]
        [Display(Name ="FullName")]
        [StringLength(30,MinimumLength =2, ErrorMessage =" Not corect size of Name ")]
        public string FullName { get; set; }



       // public IList<string> Roles { get; set; }
    }
}
