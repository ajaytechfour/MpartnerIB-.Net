using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class Lsd_CouponCountData
    {
        public Nullable<int> EligibleCouponCount { get; set; }
        public Nullable<int> ActivatedCouponCount { get; set; }
        public Nullable<int> BalanceCouponCount { get; set; }
        public Nullable<int> CouponReimbursment { get; set; }
        public Nullable<int> OpenReimbursment { get; set; }

        //Gold Coupon //
        public Nullable<int> Gold_EligibleCouponCount { get; set; }
        public Nullable<int> Gold_ActivatedCouponCount { get; set; }
        public Nullable<int> Gold_BalanceCouponCount { get; set; }
    }

    public class WinterScheme_CouponCountData
    {
        public Nullable<int> EligibleCouponCount { get; set; }
        public Nullable<int> ActivatedCouponCount { get; set; }
        public Nullable<int> BalanceCouponCount { get; set; }
        
    }

    public class WinterDhamaka_CouponCountData
    {
        public int? PrimaryEligibility { get; set; }
        public Nullable<int> EligibleCouponCount { get; set; }
        public Nullable<int> ActivatedCouponCount { get; set; }
        public Nullable<int> BalanceCouponCount { get; set; }
        public string Multiplier { get; set; }
        public string Primary_Purchase_Message { get; set; }

    }

}