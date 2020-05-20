using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuminousMpartnerIB.Models
{
    public class DistributorModel
    {
        public List<SelectListItem> ddlSelectDitributer { get; set; }

        //[Required(ErrorMessage = "Please select atleast one Distributor")]
        public int[] ddlSelectDitributerIDs { get; set; }
    }
}