//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Luminous.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lsd_DistCouponCount
    {
        public long Id { get; set; }
        public string DistCode { get; set; }
        public Nullable<int> EligibleCoupon { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> DistActivatedCount { get; set; }
        public Nullable<int> DistBalanceCount { get; set; }
        public Nullable<int> DealerRedeemedCount { get; set; }
        public Nullable<int> DistClaimedCount { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> Gold_EligibleCouponCount { get; set; }
        public Nullable<int> Gold_ActivatedCouponCount { get; set; }
        public Nullable<int> Gold_BalanceCouponCount { get; set; }
    }
}
