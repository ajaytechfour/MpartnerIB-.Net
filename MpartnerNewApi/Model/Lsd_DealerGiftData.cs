using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class Lsd_DealerGiftData
    {
        public int GiftId { get; set; }
        public string GiftName { get; set; }
        public string GiftDescription { get; set; }
        public string GiftImage { get; set; }
    }

    public class Winter_DealerPointData
    {
        public int Pointvalue { get; set; }
        public int bonuspoint { get; set; }
        //public string GiftName { get; set; }
        //public string GiftDescription { get; set; }
        //public string GiftImage { get; set; }
    }

    public class WinterDhamaka_DealerPointData
    {
        public string Pointvalue { get; set; }
        public string bonuspoint { get; set; }
        //public string GiftName { get; set; }
        //public string GiftDescription { get; set; }
        //public string GiftImage { get; set; }
    }
}