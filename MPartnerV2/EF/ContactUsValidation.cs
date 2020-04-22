using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Luminous.EF
{
    [MetadataType(typeof(ContactUsValidation))]
    public partial class contactUsDetail
    {

    }
    public class ContactUsValidation
    {
       
        [Required(ErrorMessage = "Address Is Empty")]
        
        [StringLength(499, ErrorMessage = "Character Should Be Less Than 500")]
        public global::System.String CAddress { get; set; }

        [Required(ErrorMessage = "Phone No Is Empty")]                
        public global::System.String PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email Is Empty")]
        [RegularExpression("^((\"[\\w-\\s]+\")|([\\w-]+(?:\\.[\\w-]+)*)|(\"[\\w-\\s]+\")([\\w-]+(?:\\.[\\w-]+)*))(@((?:[\\w-]+\\.)*\\w[\\w-]{0,66})\\.([a-z]{2,6}(?:\\.[a-z]{2})?)$)|(@\\[?((25[0-5]\\.|2[0-4][0-9]\\.|1[0-9]{2}\\.|[0-9]{1,2}\\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\\]?$)", ErrorMessage = "Email Is Not Valid")]       
        [StringLength(99, ErrorMessage = "Character Should Be Less Than 100")]
        public global::System.String Email { get; set; }

        [Required(ErrorMessage = "Fax Is Empty")]
        [StringLength(99, ErrorMessage = "Character Should Be Less Than 100")]
        public global::System.String Fax { get; set; }


    }
}