using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Images
    {
        [Key]
        public int imageId { get; set; }
        public string imageUrl { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public int IsActive { get; set; }
    }
}