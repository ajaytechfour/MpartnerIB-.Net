//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LuminousMpartnerIB.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class DealersTran
    {
        public long ID { get; set; }
        public Nullable<long> DealerID { get; set; }
        public string ImageName { get; set; }
        public Nullable<int> status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
    }
}
