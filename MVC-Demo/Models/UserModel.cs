using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace MVC_Demo.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage ="Please enter username")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "UserName length must between 5 to 20..")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter Password")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password length must between 8 to 20..")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please enter Password")]
        [Compare("Password",ErrorMessage ="Password do not match")]
        public string Confirm { get; set; }



        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email..")]
        public string Email { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Please enter Mobile Number")]
        public string Mobile { get; set; }
        public string Gender { get; set; }
    }
}