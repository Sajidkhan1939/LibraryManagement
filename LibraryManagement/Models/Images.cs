using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Images
    {
        public int imageId { get; set; }
        public string imageUrl { get; set; }
        public int BookId { get; set; }
        public int IsActive { get; set; }  
    }
}