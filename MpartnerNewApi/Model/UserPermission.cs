using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class UserPermission
    {
        public string ModuleName{get;set;}
        public string ModuleText { get; set; }
        public string CustomerType{get;set;}
        public string Permission { get; set; }
       // public int MonthValue { get; set; }
        public string ModuleImage { get; set; }
    
    }
}