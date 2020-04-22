using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.Models
{
    public class SuccessModel
    {
        public string SuccessStatus { get; set; }
        public int SuccessCount { get; set; }
        public string SuccessFile { get; set; }
        public string Success_UploadedBy { get; set; }
        public string Success_UploadedDate { get; set; }
    }


    public class ErrorModel
    {
        public string ErrorStatus { get; set; }
        public int ErrorCount { get; set; }
        public string ErrorFile { get; set; }
        public string Error_UploadedBy { get; set; }
        public string Error_UploadedDate { get; set; }
    }



}