using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.Models
{
    public class ContactUsModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}