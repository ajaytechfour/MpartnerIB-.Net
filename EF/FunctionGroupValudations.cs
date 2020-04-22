using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LuminousMpartnerIB.EF;
namespace LuminousMpartnerIB.EF
{
    [MetadataType(typeof(FunctionGroupValudations))]
    public partial class functionalgroup { 
    
    }
    public class FunctionGroupValudations
    {
        [Required(ErrorMessage = "Function Name Is Empty")]
        [StringLength(99, ErrorMessage = "Characters Should Be Less Than 99")]
        public global::System.String fname { get; set; }
        [Required(ErrorMessage = "Email Is Empty")]
        [RegularExpression("^((\"[\\w-\\s]+\")|([\\w-]+(?:\\.[\\w-]+)*)|(\"[\\w-\\s]+\")([\\w-]+(?:\\.[\\w-]+)*))(@((?:[\\w-]+\\.)*\\w[\\w-]{0,66})\\.([a-z]{2,6}(?:\\.[a-z]{2})?)$)|(@\\[?((25[0-5]\\.|2[0-4][0-9]\\.|1[0-9]{2}\\.|[0-9]{1,2}\\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\\]?$)", ErrorMessage = "Email Is Not Valid")]
        [StringLength(99, ErrorMessage = "Characters Should Be Less Than 99")]
        public global::System.String Femail { get; set; }
        [Required(ErrorMessage = "Department Name Is Empty")]
        [StringLength(99, ErrorMessage = "Characters Should Be Less Than 99")]
        public global::System.String FdepartmentName { get; set; }
        [Required(ErrorMessage = "Department PersonName Is Empty")]
        [StringLength(99, ErrorMessage = "Characters Should Be Less Than 99")]
        public global::System.String FDeparmentPersonName { get; set; }
    }
};