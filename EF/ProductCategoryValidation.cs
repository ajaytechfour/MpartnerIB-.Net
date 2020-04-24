using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LuminousMpartnerIB.EF
{
    [MetadataType(typeof(ProductCategoryValidation))]
    public partial class ProductCatergory { }
    public class ProductCategoryValidation
    {
      //  [Required(ErrorMessage = "Product Code Has NO Value")]
        [StringLength(29, ErrorMessage="Character Should Be Less Than 30")]
        public global::System.String PCode { get; set; }
     //   [Required(ErrorMessage = "Product Category Has No Value")]
        [StringLength(99, ErrorMessage = "Character Should Be Less Than 100")]
        public global::System.String PName { get; set; }
    }
}