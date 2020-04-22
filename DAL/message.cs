using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TVS
{
    public static class message
    {
       
        public static string save()
        {
            return "Record Has Been Saved Successfully";
        
        }

        public static string notsave()
        {

            return "Record Has Not Been Saved Successfully";
        }
        public static string update()
        {

            return "Record Has Been Updated Successfully";
        }
        public static string delete()
        {

            return "Record Has Been Deleted Successfully";
        }
        public static string exsits()
        {

            return "A Record With The Same Name Already Exists";
        }
    }

  
}