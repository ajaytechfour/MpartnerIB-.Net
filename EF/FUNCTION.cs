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
    
    public partial class FUNCTION
    {
        public FUNCTION()
        {
            this.FUNCTIONSHistories = new HashSet<FUNCTIONSHistory>();
            this.Suggestions = new HashSet<Suggestion>();
        }
    
        public int ID { get; set; }
        public string fNAME { get; set; }
        public string fEMAIL { get; set; }
        public string fDepartmentName { get; set; }
        public string DepartmentPersoneName { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CareatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual ICollection<FUNCTIONSHistory> FUNCTIONSHistories { get; set; }
        public virtual ICollection<Suggestion> Suggestions { get; set; }
    }
}
