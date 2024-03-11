using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class BooksIssued
    {
        public int issue_id { get; set; }
        public int book_id { get; set; }
        public DateTime issue_date { get; set; }
        public DateTime return_date { get; set; }
        public string UserId { get; set; }
        public ApplicationUser Aspnetuser { get; set; }
        public Books book { get; set; }
        public string Status { get; set; }
        public long  Fine { get; set; }
    }
}