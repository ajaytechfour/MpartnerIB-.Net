//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Luminous.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Gallery_History
    {
        public int Id { get; set; }
        public Nullable<int> Galleryid { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifyOn { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
