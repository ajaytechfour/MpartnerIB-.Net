using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerIB_Api.Model
{
    public class SurveyNotification
    {
         public int? SurveyId{get;set;}
        public string Survey{get;set;}
       
    }
    public class SurveyNotification_Question
    {
        public int? SurveyId { get; set; }
        //public string Survey { get; set; }
        //public string QuestionType { get; set; }
        public string Survey { get; set; }
        public string QuestionType { get; set; }
        public string QuestionTitle { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string OptionE { get; set; }
        public string CorrectAns { get; set; }
        public string Mandatory_flag { get; set; }
       
    }
}