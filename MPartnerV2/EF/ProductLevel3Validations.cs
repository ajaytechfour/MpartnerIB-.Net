using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Luminous.EF
{
    [MetadataType(typeof(ProductLevel3Validations))]
    public partial class ProductLevelThree { }
    public class ProductLevel3Validations
    {
        //[Required(ErrorMessage = "Product Code Is Empty")]
        [StringLength(99, ErrorMessage = "Characters Should Be Less Than 100")]
       
        public global::System.String PrCode { get; set; }

        //[Required(ErrorMessage = "Technical Specification Is Empty")]
        public global::System.String PrDiscription { get; set; }

        [Required(ErrorMessage = "Name Is Empty")]
        [StringLength(99, ErrorMessage = "Characters Should Be Less Than 100")]
        public global::System.String Name { get; set; }

        [Required(ErrorMessage = "Start Date Is Empty")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<global::System.DateTime> startDate { get; set; }

        [Required(ErrorMessage = "End Date Is Empty")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<global::System.DateTime> enddate { get; set; }

        [Required(ErrorMessage = "Warranty Is Required ")]
        [StringLength(249, ErrorMessage = "Characters Should Be Less Than 250")]
        public string Warrenty { get; set; }

        [Required(ErrorMessage = "Key Feature Is Required ")]
        //[StringLength(249, ErrorMessage = "Characters Should Be Less Than 250")]
        public string KeyFeature { get; set; }

        //[Required(ErrorMessage = "MRP Is Required")]
        [Range(0,9999999999,ErrorMessage="Digits Should Be Less Than 11 Character")]
        [DataType(DataType.Currency)]
        public decimal? MRP { get; set; }
    }
}