using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class getnamebydistcode
    {

        public string Distname { get; set; }

        public string Message { get; set; }
    }

    public class dealerimagecount
    {
        public string imagescount { get; set; }
        public string Message { get; set; }
        public string Distname { get; set; }
        public string Distcode { get; set; }
        public string quespollflag { get; set; }
        public string quessubmitflag { get; set; }
    }
}