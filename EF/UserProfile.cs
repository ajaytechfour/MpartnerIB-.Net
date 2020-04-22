using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuminousMpartnerIB.EF
{
    public class UserProfile
    {
        public long? id;
        public int? Dealerid;
        public string Email;
        public DateTime? createdTime;
        public DateTime? UpdatedTime;
        public string City, District, state, ContactNo, RegionCode, UserImage;
        public string Address1, Address2, CustomerType, Dis_Sap_Code, Name;
        public string CreatedBy, UpdatedBy, Message;
        public string Status;
        public string DistributorCode;
        public string FseCode;
    }
}