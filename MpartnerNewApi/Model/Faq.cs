using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class Faq
    {
        public long id { get; set; }
        public string  question { get; set; }
        public string answer { get; set; }
    }
}