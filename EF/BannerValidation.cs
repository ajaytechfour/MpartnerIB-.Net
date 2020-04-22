using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LuminousMpartnerIB.EF
{
    [MetadataType(typeof(BannerValidation))]
    public partial class Banner {
    
    }
    public class BannerValidation
    {
        //[Required(ErrorMessage="Banner Header Is Required")]
        //[StringLength(49,ErrorMessage="Characters In Banner Header Should Be Less Than 50")]
        //public global::System.String Header_Details{get;set;}
        //[Required(ErrorMessage = "Sub Header Is Required")]
        //[StringLength(99, ErrorMessage = "Characters In Sub Header Should Be Less Than 100")]
        //public global::System.String Sub_Header_Details { get; set; }
        //[Required(ErrorMessage = "Descriptions Is Required")]
        //[StringLength(499, ErrorMessage = "Characters In Discriptions Should Be Less Than 500")]
        //public global::System.String Banner_Details { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<global::System.DateTime> ExpriyDate
        {
            get;

            set;
        }
         [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<global::System.DateTime> stardate { get; set; }
        
        
    }
}