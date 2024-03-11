using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class BooksViewModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Book_ID { get; set; }
        public int issue_id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string genre { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string publisher { get; set; }
        public int publication_year { get; set; }
        public string ISBN { get; set; }
        public int total_copies { get; set; }
        public string issue_date { get; set; }
        public string return_date { get; set; }
        public string Status { get; set; }
        public long Fine { get; set; }
        public List<Images> bookimages { get; set; }
    }
}