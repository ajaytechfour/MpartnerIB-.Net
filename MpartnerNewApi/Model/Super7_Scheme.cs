using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class Super7_Scheme
    {
        public string PrimaryEligibility { get; set; }
        public string EligibleCouponCount { get; set; }
        public string ActivatedCouponCount { get; set; }
        public string BalanceCouponCount { get; set; }
        public string RedeemedCouponCount { get; set; }
        public string AdvanceEligibility { get; set; }
        public string Info_text { get; set; }
        public string Primary_Purchase_Message { get; set; }
       


    }

    public class Super7_DealerPointData
    {
        public string Gift { get; set; }
        public string GiftDesc { get; set; }
        public string Bonus { get; set; }
        public string Bonus_Msg { get; set; }
        //public string GiftName { get; set; }
        //public string GiftDescription { get; set; }
        //public string GiftImage { get; set; }
    }

    public class Super7_Distributor_Dealer_Summery
    {
        public string RedemptionDealerCode { get; set; }
        public string RedemptionDealerName { get; set; }

        public int Coupon { get; set; }
        public string Points { get; set; }
        // public int BonusPoint { get; set; }

    }

    public class Super7_Dealer_Report
    {
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string Barcode { get; set; }
        public string SecretCode { get; set; }

        public string RedemptionDealerOn { get; set; }
      //  public string ActivationDistName { get; set; }
        public string Totalpoint { get; set; }
        public string DistCode { get; set; }
        public string DistName { get; set; }
        public string Qrcode { get; set; }
        public string ActivatedDistOn { get; set; }
        // public string Bonus { get; set; }
        // public string SchemeType { get; set; }
        // public string Multiplier { get; set; }

    }
    public class Super7_Dealer_Distributor_List
    {
        public string ActivationDistributorCode { get; set; }
        public string ActivationDistributorName { get; set; }
        public string Coupon { get; set; }
        public string Point { get; set; }

    }

   
}