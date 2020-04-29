using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace LuminousMpartnerIB.EF
{
    [MetadataType(typeof(ProfileValidations))]
    public partial class AllProfile { 
    
    }

    public class ProfileValidations
    {
        [Required(ErrorMessage="*")]
        [StringLength(49,ErrorMessage="Profile Name Should Be Less Than 50 Character")]
        [Remote("CheckDuplicate", "Profile", ErrorMessage = "Already Exists")]
        public global::System.String profileName { get; set; }
    }
}