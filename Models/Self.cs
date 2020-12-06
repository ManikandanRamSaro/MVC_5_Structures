using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StructuredMVC.Models
{
    public class Self
    {
        public string id { get;set;}
        public string name { get; set; }
        public string locat { get; set; }

        public string dateat { get; set; }
    }
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
    }
}