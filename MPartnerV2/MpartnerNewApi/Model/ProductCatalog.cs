using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luminous.MpartnerNewApi.Model
{
    public class ProductCatalog
    {
        public int id { get; set; }
        public string productcatalog_image_url { get; set; }
        public string productioncatalog_rating { get; set; }
        public string productioncatalog_name { get; set; }
        public string keyfeature { get; set; }
        public string warrenty { get; set; }
        public string productleveltwo { get; set; }
        
        public string attribute_name { get; set; }
        public List<Technicalspecification> tech_specification { get; set; }
    }
    public class Technicalspecification
    {
        public string ColumnName;
        public string Value;
    }
}