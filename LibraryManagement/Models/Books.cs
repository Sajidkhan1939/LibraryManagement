using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Books
    {
        [Key]
        public int Book_ID { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string genre { get; set; }
        public string publisher { get; set; }
        public int publication_year { get; set; }
        public string ISBN { get; set; }
        public int total_copies { get; set; }
        public List<Images> bookimages { get; set; }
    }
}