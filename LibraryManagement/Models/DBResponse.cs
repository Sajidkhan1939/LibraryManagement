using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class DBResponse
    {
        public bool Result { get; set; }
        public string ExceptionMessage { get; set; }
        public DataSet DataResult { get; set; }      
    }
}
