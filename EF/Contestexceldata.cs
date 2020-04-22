using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.EF
{
    public class Contestexceldata
    {
//          select sd.DealerCode,urs.Dis_Name as DealerName,sd.DistributorCode,sd.CreatedOn,sd.CreatedBy,sd.Marqueeid as EventNumber,sd.Rating 
//from SaveDelContestImages sd
//join UsersList urs on urs.UserId=sd.DealerCode

    public string DealerCode {get;set;}
    public string DealerName { get; set; }
    public string DistributorCode { get; set; }
    public Nullable<DateTime> CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public int EventNumber { get; set; }
    public string Rating { get; set; }

    }
}