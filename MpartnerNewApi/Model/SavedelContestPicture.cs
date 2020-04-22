
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class SavedelContestPicture
    {
        public string user_id { get; set; }
        public string distcode { get; set; }
        public string imagename1{get;set;}
        public string imagename2 { get; set; }
        public byte[] filename1 { get; set; }
        public byte[] filename2 { get; set; }
        public int marqueeid { get; set; }
       public  string latitude{get;set;}
       public  string longitude{get;set;}
       public string location { get; set; }
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

    public class Savedelpollquestion
    {
        public string user_id { get; set; }
        public string questionid { get; set; }
        public string optionvalue { get; set; }
        public string question2 { get; set; }

        public string option2 { get; set; }
        public string question3 { get; set; }
        public string option3 { get; set; }
        public string question4 { get; set; }
        public string option4 { get; set; }
        public string marqueeid { get; set; }
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

    public class PollQuestion
    {
        public int QuestionId;
        public string Question;
        public string OptionA;
        public string OptionB;
        public string OptionC;
        public string OptionD;
        public string CorrectAns;
        public DateTime? StartDate;
        public DateTime? EndDate;
        public string CreatedBy;
        public DateTime? CreatedOn;
        
        public string Status;
      

    }

    public class PollQuestion_Date
    {


        public string Date{get;set;}


    }

    public class Getcontestinfo_data
    {
        public string Question { get; set; }
        
        public string CorrectAns { get; set; }

        public string imagename { get; set; }
    }
}