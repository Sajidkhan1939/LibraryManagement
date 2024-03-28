using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Results
    {
        public string Result { get; set; }
        public string Message { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public Results()
        {
            Result = "Fail";
            Message = "Internal Server Error";
            ID = "0";
            Name = "";
        }
    }
}
