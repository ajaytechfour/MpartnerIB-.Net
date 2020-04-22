using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerIB_Api.Model
{
    public class DealerData
    {
        public string DealerName { get; set; }
        public string DealerAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public List<DealerImage> ImageList { get; set; }


    }

    public class DealerImage
    {
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
        public byte[] Image4 { get; set; }
        public byte[] Image5 { get; set; }

    }

}