using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luminous.MpartnerNewApi.Model
{
    public class AlertNotifications
    {
        public int id{get;set;}

        public string Text { get; set; }
        public bool isread { get; set; }
        public string Imagename { get; set; }
        public string Imagepath { get; set; }
        public DateTime Date { get; set; }
      

       
    }
}