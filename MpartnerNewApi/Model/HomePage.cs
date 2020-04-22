using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class HomePage
    {
        public string name { get; set; }
        public string class_name { get; set; }
        public string card_action { get; set; }
        public string background_image { get; set; }
        public string image_height { get; set; }
        public string image_width { get; set; }
        // public string tag { get; set; }
        public string main_image { get; set; }
        public string title { get; set; }
        public string title_color { get; set; }
        public string sub_title { get; set; }
        public string subtitle_color { get; set; }
        public List<Carddata> card_data { get; set; }
        public List<Bannerdata> Bannercard_data { get; set; }
        public string action1_color { get; set; }
        public string action1_text { get; set; }
        public string subcategory { get; set; }
        public string current_page { get; set; }
        public string ProductFooter { get; set; }
        public string ProductUpper { get; set; }
        public string ProductMain { get; set; }

        //public string BMHR_Pdf_Name { get; set; }
        //public string BMHR_Flag { get; set; }
        //public string BMHR_URL { get; set; }

        public List<BMHR_Data> BMHR_Data { get; set; }



    }


    public class BMHR_Data
    {
        public string BMHR_Pdf_Name { get; set; }
        public string BMHR_Flag { get; set; }
        public string BMHR_URL { get; set; }

    }


    public class HomePage_BMHR
    {
        public string sub_title { get; set; }

    }


    public class HomePageViewModel
    {
        List<HomePage_BMHR> HomePage { get; set; }
        List<HomePage> HomePage_BMHR { get; set; }

    }
    
}