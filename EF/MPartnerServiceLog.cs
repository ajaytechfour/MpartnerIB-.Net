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
    
    public partial class MPartnerServiceLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        public string Req_Parameter { get; set; }
        public string Res_parameter { get; set; }
        public Nullable<int> Error { get; set; }
        public string ErrorDescription { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string DeviceId { get; set; }
        public string AppVersion { get; set; }
        public string OSType { get; set; }
        public string OSVersion { get; set; }
        public string NewURL { get; set; }
        public string Comments { get; set; }
        public string Flag { get; set; }
    }
}
