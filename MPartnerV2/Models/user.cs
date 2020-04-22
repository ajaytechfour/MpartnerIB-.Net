using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TVS.Models
{
    public partial class User
    {

        public int Userid { get; set; }
        [Required(ErrorMessage = "Please provide username", AllowEmptyStrings = false)]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Can not be greater than 10 words")]
        public string username { get; set; }
        [Required(ErrorMessage = "Please provide email address", AllowEmptyStrings = false)]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
          ErrorMessage = "Please provide valid email id")]
        public string email { get; set; }
        [Required(ErrorMessage = "Please provide Password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be 8 char long.")]
        public string password { get; set; }
        
        [Required(ErrorMessage = "Please provide full name", AllowEmptyStrings = false)]
        public string fullname { get; set; }

      
    }



   
  
}