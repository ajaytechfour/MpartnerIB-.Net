using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class WinterScheme_Distributor_Dealer_Summery
    {
       public string RedemptionDealerCode {get;set;}
        public string RedemptionDealerName {get;set;}

        public int Coupon {get;set;}
        public int Points {get;set;}
        public int BonusPoint{get;set;}

    }

    public class WinterDhamaka_Distributor_Dealer_Summery
    {
        public string RedemptionDealerCode { get; set; }
        public string RedemptionDealerName { get; set; }

        public int Coupon { get; set; }
        public string Points { get; set; }
        public int BonusPoint { get; set; }

    }


    public class WinterScheme_Dealer_Distributor_List
    {
        public string ActivationDistributorCode { get; set; }
        public string ActivationDistributorName { get; set; }        

    }

    public class WinterDhamaka_Dealer_Distributor_List
    {
        public string ActivationDistributorCode { get; set; }
        public string ActivationDistributorName { get; set; }

    }


    public class WinterScheme_Dealer_Dashboard
    {
        public string TotalPoints { get; set; }
        public int BonusPoint { get; set; }

    }

    public class WinterDhamaka_Dealer_Dashboard
    {
        public string TotalCouponCount { get; set; }
        public string TotalPoints { get; set; }
        public string BonusPoint { get; set; }
        public string DistyMultiplier { get; set; }
        public string WMF_Multiplier { get; set; }

    }



    public class WinterScheme_Dealer_Report
    {
        public string Barcode { get; set; }
        public string SecretCode { get; set; }

        public string RedemptionDealerOn { get; set; }
        public string ActivationDistName { get; set; }
        public int Totalpoint { get; set; }
        public int Bonus { get; set; }

    }
    public class WinterDhamaka_Dealer_Report
    {
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string Barcode { get; set; }
        public string SecretCode { get; set; }

        public string RedemptionDealerOn { get; set; }
        public string ActivationDistName { get; set; }
        public string Totalpoint { get; set; }
        public string Bonus { get; set; }
        public string SchemeType { get; set; }
        public string Multiplier { get; set; }

    }

    public class Wfs_Summery_Dealer_Report
    {
        public string Wfs_Coupon { get; set; }
        public string Wfs_NormalPoint { get; set; }

        public string Wfs_Bonus { get; set; }
        public string Wfs_Total { get; set; }
        public string Wfs_Scheme_ext_bonus { get; set; }
        public string Wfs_Multiplier { get; set; }  

    }
}