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
    
    public partial class NotificationsHistory
    {
        public int id { get; set; }
        public Nullable<int> NotificationId { get; set; }
        public string C_Subject { get; set; }
        public string C_text { get; set; }
        public Nullable<int> C_status { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Alertflag { get; set; }
        public string ImagePath { get; set; }
    }
}
