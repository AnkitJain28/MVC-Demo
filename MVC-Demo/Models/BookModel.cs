using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVC_Demo.Models
{
    public class BookModel
    {
        public int BookId { get; set; }

        [Display(Name = "Book Title")]
        [Required(ErrorMessage = "Please enter user name")]
        [StringLength(50, ErrorMessage = "Book title length should not be more than 50..")]
        public string BookName { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please select a category")]
        public string Category { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Please select status of book")]
        public string Status { get; set; }
    }
}