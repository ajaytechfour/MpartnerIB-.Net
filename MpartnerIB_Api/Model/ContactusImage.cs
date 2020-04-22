using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerIB_Api.Model
{
    public class ContactusImage
    {
        public string user_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string message { get; set; }
        public byte[] contactusimage { get; set; }
        public string filename { get; set; }
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
       
     
    }
}