using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookShop.DAL
{
    public class Book
    {
        public int ID { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Book title can not be empty .")]
        [MaxLength(40,ErrorMessage ="Book title can not be more than 40 characters .")]
        [MinLength(3,ErrorMessage ="Book title must be at least 3 characters .")]
        
        //[Range(3, 40, ErrorMessage = "Name must be between 3 and 40 characters")]
        public string Title { get; set; }
        [Required]
        [ForeignKey("Author")]
        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }
        //[Required]
        public string CoverPicture { get; set; }
        //[Required]
        public string Content { get; set; }
        [Required]
        public int DownloadNumber { get; set; }
        [Required]
        //[Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; }
        [ForeignKey("Classification")]
        public int ClassificationID { get; set; }
        public virtual Classification Classification { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Description can not be empty .")]
        [MinLength(3,ErrorMessage ="Description must be at least 3 characters .")]
        [MaxLength(300, ErrorMessage = "Description can not be more than 300 characters .")]
        public string Description { get; set; }
        //[Required]
        //public ApplicationUser User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}