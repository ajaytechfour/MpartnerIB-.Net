using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.MpartnerNewApi.Model
{
    public class Report
    {
        public IList<PrimaryBilling> PrimaryBillingReport { get; set; }
    }

    public class PrimaryBilling
    {
        public string INV_NO { get; set; }
        public string INVOICE_DA { get; set; }
        public string ITEM_CODE { get; set; }
        public string MATERIAL_DESC { get; set; }
        public string INV_QTY { get; set; }
        public string AMOUNT { get; set; }
        public string DIVISION { get; set; }
    }
}