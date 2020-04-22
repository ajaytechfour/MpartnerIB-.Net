using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luminous.EF
{
    //Added new class by Ravi on 16-07-2018 Task id - 4009
    public class ConAssist
    {

        public string Serialno { get; set; }
        public Nullable<DateTime> Createdon { get; set; }
        public string CreatedBy { get; set; }

        public string Text { get; set; }

        public string Username { get; set; }

        public string Attachment { get; set; }

        public string Flag { get; set; }

    }
}