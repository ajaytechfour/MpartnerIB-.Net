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
    
    public partial class ProductAccessTableForProduct
    {
        public int id { get; set; }
        public Nullable<int> RegId { get; set; }
        public Nullable<int> DestributorID { get; set; }
        public Nullable<int> DealerId { get; set; }
        public Nullable<bool> AllAcess { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string createby { get; set; }
        public Nullable<System.DateTime> modifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<bool> deleted { get; set; }
        public Nullable<bool> AllDestriAccess { get; set; }
        public Nullable<bool> AllDealerAccess { get; set; }
        public string SpecificDealerAccess { get; set; }
        public string SpecificDestriAccess { get; set; }
    }
}
