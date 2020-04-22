using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luminous.MpartnerNewApi.Model
{
    public class WinterScheme_Distributor_Dealer_Summery
    {
       public string RedemptionDealerCode {get;set;}
        public string RedemptionDealerName {get;set;}

        public int Coupon {get;set;}
        public int Points {get;set;}
        public int BonusPoint{get;set;}

    }


    public class WinterScheme_Dealer_Distributor_List
    {
        public string ActivationDistributorCode { get; set; }
        public string ActivationDistributorName { get; set; }        

    }


    public class WinterScheme_Dealer_Dashboard
    {
        public int TotalPoints { get; set; }
        public int BonusPoint { get; set; }

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
}