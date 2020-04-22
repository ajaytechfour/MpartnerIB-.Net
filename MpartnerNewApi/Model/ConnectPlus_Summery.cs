using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class ConnectPlus_Summery
    {
        public string SchemeName { get; set; }
        public string CNAmount { get; set; }
        public string SubmittedBy { get; set; }
        public string Declaration_Msg { get; set; }
    
        public List<ConnectPlus_Summery_Dealer_CN_Amount> connect_plus_cn_amount { get; set; }
    }

    public class ConnectPlus_Summery_Dealer_CN_Amount
    {
       
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string CN_Amount { get; set; }
    }

    //public class ConnectPlus_Summery_Dealer_CN_Amount_Scheme
    //{
    //    public List<ConnectPlus_Summery> connect_plus_scheme { get; set; }
    //    public List<ConnectPlus_Summery_Dealer_CN_Amount> connect_plus_cn_amount { get; set; }
        
    //}
}