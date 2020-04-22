using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Luminous.EF
{
    [MetadataType(typeof(ProductlevelTwoValidations))]
    public partial class ProductLevelTwo { }
    public class ProductlevelTwoValidations
    {
       [Required(ErrorMessage = "Please Enter Product lvel Two")]
       [StringLength(99, ErrorMessage = "Character Should Be Less Than 100")]
       public global::System.String Name { get; set; }
    }
}