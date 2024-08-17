using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookShop.DAL
{
    public class Comment
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(200)]
        public string Text { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [ForeignKey("Book")]
        public int BookID { get; set; }
        public Book Book { get; set; }



        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}