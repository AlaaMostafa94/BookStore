using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookShop.DAL
{
    public class Author
    {
        public int ID { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Author name can not be empty .")]
        [MaxLength(30,ErrorMessage ="Author name can not be more than 30 characters .")]
        [MinLength(3,ErrorMessage ="Author name must be at least 3 characters .")]
        [Index(IsUnique =true)]
        //[Range(3, 30, ErrorMessage = "Name must be between 3 and 20 Characters")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="The field 'About' can not be empty .")]
        [MaxLength(500, ErrorMessage = "Text can not be more than 500 characters")]
        [MinLength(10,ErrorMessage ="Text must be at least 20 characters .")]
        public string About { get; set; }
        //[RegularExpression()]
        
        public string Image { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}