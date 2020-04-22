using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class ConnectPlusData
    {

        public string user_id { get; set; }
        public string serialno { get; set; }
        public string discode{get;set;}
        
        public string dlrCode{get;set;}
        public string saledate { get; set; }
        public string customername{get;set;}
        
        public string customerphone{get;set;}
        
        public string customerlandLinenumber{get;set;}
        
        public string customerstate{get;set;}
        
        public string customercity{get;set;}
        
        public string customeraddress{get;set;}
        
        public string ismtype{get;set;}

        public string connectplusimage_name { get; set; }

        public byte[] connectplusimage { get; set; }
    
        public string token { get; set; }
        public string app_version { get; set; }
        public string device_id { get; set; }
        public string device_name { get; set; }
        public string os_type { get; set; }
        public string os_version_name { get; set; }
        public string os_version_code { get; set; }
        public string ip_address { get; set; }
        public string language { get; set; }
        public string screen_name { get; set; }
        public string network_type { get; set; }
        public string network_operator { get; set; }
        public string time_captured { get; set; }
        public string channel { get; set; }

        public string pagename { get; set; }
    }
}