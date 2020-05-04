using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Luminous;

namespace LuminousMpartnerIB.Models
{
    public class RegistrationModel
    {
        
        [Display(Name = "Catalog",ResourceType =typeof(Resource))]
        public string Catalog { get; set; }

        [Display(Name = "Contact Us", ResourceType = typeof(Resource))]
        public string ContactUs { get; set; }

        [Display(Name = "Dealer Management", ResourceType = typeof(Resource))]
        public string DealerManagement { get; set; }

        [Display(Name = "FAQs", ResourceType = typeof(Resource))]
        public string FAQs { get; set; }

        [Display(Name = "Gallery", ResourceType = typeof(Resource))]
        public string Gallery { get; set; }

        [Display(Name = "Price List", ResourceType = typeof(Resource))]
        public string PriceList { get; set; }

        [Display(Name = "Schemes", ResourceType = typeof(Resource))]
        public string Schemes { get; set; }

        [Display(Name = "Welcome", ResourceType = typeof(Resource))]
        public string Welcome { get; set; }

        [Display(Name = "Dashbaord", ResourceType = typeof(Resource))]
        public string Dashbaord { get; set; }

        [Display(Name = "Logout", ResourceType = typeof(Resource))]
        public string Logout { get; set; }

        [Display(Name = "More Details", ResourceType = typeof(Resource))]
        public string MoreDetails { get; set; }
    }
}