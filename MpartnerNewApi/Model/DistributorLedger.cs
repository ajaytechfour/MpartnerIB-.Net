using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class DistributorLedger
    {
        public string DOC_DATE { get; set; }
        public string DOC_NO { get; set; }
        public string REF_ORG_UN { get; set; }
        public string ALLOC_NMBR { get; set; }
        public string REF_DATE { get; set; }
        public string DEBIT_AMOUNT { get; set; }
        public string CREDIT_AMOUNT { get; set; }
        public string CHANNEL_TXT { get; set; }
        public string DIVISION_TXT { get; set; }
        public string DOC_TYP_TXT { get; set; }
        public string TOTAL_CREDIT { get; set; }
        public string CDRC { get; set; }
        public string CONT { get; set; }
        public string CONTROL_AREA_DESC { get; set; }
        public string EXTENDED_CREDIT_LIMI { get; set; }
    }
}