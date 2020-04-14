using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerIB_Api.Model
{
    public class Carddata
    {
        public string title { get; set; }
        public string background_image { get; set; }
        public string main_image { get; set; }
        public string card_action { get; set; }
        public string image_height { get; set; }
        public string image_width { get; set; }


        public int product_category_id { get; set; }
        public string product_category_name { get; set; }
        public int catalog_menu_upper_id { get; set; }
        public string catalog_menu_upper_name { get; set; }
        public int product_catalog_id { get; set; }
        public string product_catalog_name { get; set; }
        
        
    }

    public class Bannerdata
    {
        public string title { get; set; }
        public string background_image { get; set; }
        public string main_image { get; set; }
        public string card_action { get; set; }
        public string image_height { get; set; }
        public string image_width { get; set; }
        public string product_id { get; set; }
        public string product_name { get; set; }

    }
   
}