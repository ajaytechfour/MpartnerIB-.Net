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
    
    public partial class DistributorMaster_History
    {
        public long ID { get; set; }
        public long DistributorMasterID { get; set; }
        public string DistributorName { get; set; }
        public string DistributorAddress { get; set; }
        public string MobileNo { get; set; }
        public Nullable<int> StateID { get; set; }
        public Nullable<int> CityID { get; set; }
        public string EmailID { get; set; }
        public string PinCode { get; set; }
        public Nullable<int> status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
