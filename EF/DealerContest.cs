using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace LuminousMpartnerIB.EF
{
    public class DealerContest
    {
       public string  DealerCode {get;set;}
       public string  DealerName{get;set;}
       public string  DealerPhone{get;set;}     
       public string Dis_Code{get;set;}
       public string eventname { get; set; }
       public string Contest { get; set; }
       public string image { get; set; }
       public string Rating { get; set; }

       public int id { get; set; }
       public Nullable<DateTime> CreatedOn{get;set;}
       public string CreatedBy { get; set; }
       public string QuestionName { get; set; }
       public string CorrectAns { get; set; }
       public int eventid { get; set; }
       public List<DealerContest> Dcontest { get; set; }                        
    }
}