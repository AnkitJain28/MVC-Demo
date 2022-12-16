using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVC_Demo.Models
{
    public class LoginModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Please enter username")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "User Name length should be between 5 to 50..")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter Password")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password length should be between 8 to 20..")]
        public string Password { get; set; }
    }
}