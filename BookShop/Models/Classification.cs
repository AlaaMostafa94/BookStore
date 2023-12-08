using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookShop.Models
{
    public class Classification
    {
        public int ID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Classification name can not be empty .")]
        [MinLength(3,ErrorMessage = "Classification name must be at least 3 characters .")]
        //[Range(3, 20, ErrorMessage = "Name must be between 3 and 20 Characters")]
        [MaxLength(20,ErrorMessage ="Classification name can not be more than 20 characters .")]

        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}