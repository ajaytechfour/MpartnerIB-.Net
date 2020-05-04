using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Luminous.Models
{

    public class UserDataParameter
    {
        public string token { get; set; }
        public string Item_Type { get; set; }
        public string State_Name { get; set; }
    }
    public class WRSMultiplierMasterError
    {
        public string Material_Code { get; set; }
        public string itemdesc { get; set; }
        public string Item_Type { get; set; }
        public string State_Name { get; set; }
        public string Valid_Start_Date { get; set; }
        public string Valid_End_Date { get; set; }
        public string Sale_Start_Date { get; set; }
        public string Sale_End_Date { get; set; }
        public string SerialNo_Entry_Start_Date { get; set; }
        public string SerialNo_Entry_End_Date { get; set; }
        public string Entries_Count { get; set; }
        public string Multiplier_Count { get; set; }
        public string Multiplier_Type { get; set; }
        public string ErrorDescription { get; set; }
    }

   
}