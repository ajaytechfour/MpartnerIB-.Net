//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LuminousMpartnerIB.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class NotificationSurvey
    {
        public int SurveyID { get; set; }
        public string Survey { get; set; }
        public string QuestionType { get; set; }
        public string QuestionTitle { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string OptionE { get; set; }
        public string CorrectAns { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> Enddate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyOn { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> ContestId { get; set; }
    }
}
