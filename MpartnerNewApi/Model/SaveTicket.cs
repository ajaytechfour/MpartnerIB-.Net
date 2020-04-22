using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class SaveTicket
    {
        public string user_id { get; set; }
     
        public int ticketid { get; set; }
        public string attachmentname { get; set; }
        public byte[] attachment { get; set; }
        public string serialno { get; set; }
        public string description { get; set; }
        public string connectplus_message { get; set; }
        public string status { get; set; }
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

    public class ViewTicket
    {
       
        public string serialno { get; set; }
        public string attachment { get; set; }
        public string Date { get; set; }
        public string createdby { get; set; }
        public string connectplus_msg { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public int Id { get; set; }
    }
}