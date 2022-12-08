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
        public string Name { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
    }
}