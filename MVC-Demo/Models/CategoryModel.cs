using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVC_Demo.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        [Display(Name = "Categogy Name")]
        [Required(ErrorMessage = "Please enter category name")]
        [StringLength(50, ErrorMessage = "Category name length should not be more than 50..")]
        public string CategoryName { get; set; }
    }
}