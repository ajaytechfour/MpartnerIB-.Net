using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luminous.Models
{
    public class MuliplierModel
    {
        public string ErrorStatus { get; set; }
        public int ErrorCount { get; set; }
        public string ErrorFile { get; set; }
        public string Error_UploadedBy { get; set; }
        public string Error_UploadedDate { get; set; }

        public string SuccessStatus { get; set; }
        public int SuccessCount { get; set; }
        public string SuccessFile { get; set; }
        public string Success_UploadedBy { get; set; }
        public string Success_UploadedDate { get; set; }

    }

    public class ItemName
    {
        public string Item_Type { get; set; }
    }




    public class ItemMaster
    {
        public string Item_Type { get; set; }
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }        
    }


}