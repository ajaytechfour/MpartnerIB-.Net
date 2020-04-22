using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Luminous.EF
{
    [MetadataType(typeof(ProductleveOneValidations))]
    public partial class ProductLevelOne { 
    
    
    }
    public class ProductleveOneValidations
    {
        [Required(ErrorMessage = "Select Product Category")]
       
        public Nullable<global::System.Int32> pcId { get; set; }

        //[Required(ErrorMessage = "Product Level One Has No Value")]
        //[StringLength(99,ErrorMessage="Character Should Be Less Than 100 ")]
        //public global::System.String Name { get; set; }
    }


}