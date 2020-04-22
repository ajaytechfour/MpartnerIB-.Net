using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luminous.MpartnerNewApi.Model
{
    public class contestpitcure
    {
        public string DealerID { get; set; }

        public Nullable<Boolean> Flag { get; set; }

        public Nullable<int> MarqueeFlag { get; set; }
        public string Marquee { get; set; }
        public string Message { get; set; }
        public int MarqueeId { get; set; }
        public string MarqueeImage { get; set; } 

    }
}