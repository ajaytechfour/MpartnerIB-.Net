using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.Models
{
    public class AllPageModel
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public string ModuleName { get; set; }
        public string RequestType { get; set; }
        public int RootNode { get; set; }
        public Nullable<bool> IsTopMenu { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string ModuleImage { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyOn { get; set; }
    }
}