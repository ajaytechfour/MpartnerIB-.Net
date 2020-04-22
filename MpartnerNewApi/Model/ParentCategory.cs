using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class ParentCategory
    {
        public int Id { get; set; }
        public string Parentcategoryname { get; set; }
        public string PageName { get; set; }
       // public string PdfURL { get; set; }

    }




    public class CatalogMainPdf
    {
        public int Id { get; set; }
        public string Categoryname { get; set; }
        public string PdfName { get; set; }
        public string PdfURL { get; set; }

    }
}