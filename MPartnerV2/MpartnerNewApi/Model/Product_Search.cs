using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luminous.MpartnerNewApi.Model
{
    public class Product_Search
    {
        public int id { get; set; }
        public string productioncatalog_name { get; set; }
    }

    public class Product_Searchdata
    {
        public int product_category_id { get; set; }
        public string product_category_name { get; set; }
        public int catalog_menu_upper_id { get; set; }
        public string catalog_menu_upper_name { get; set; }
        public int product_catalog_id { get; set; }
        public string product_catalog_name { get; set; }
    }
}