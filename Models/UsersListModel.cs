using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuminousMpartnerIB.Models
{
    public class UsersListModel
    {
        public int? id { get; set; }
        public string UserId { get; set; }
        public string UPassword { get; set; }
        public Nullable<int> RegId { get; set; }
        public Nullable<int> DestriId { get; set; }
        public Nullable<int> DealerId { get; set; }
        public string CustomerType { get; set; }
        public string Dis_Sap_Code { get; set; }
        public string Dis_Name { get; set; }
        public string Dis_Address1 { get; set; }
        public string Dis_Address2 { get; set; }
        public string Dis_City { get; set; }
        public string Dis_District { get; set; }
        public string Dis_State { get; set; }
        public string Dis_ContactNo { get; set; }
        public string Dis_Email { get; set; }
        public string Region_code { get; set; }
        public Nullable<int> isActive { get; set; }
        public string ActivatedBY { get; set; }
        public Nullable<System.DateTime> ActivatedDate { get; set; }
        public Nullable<System.DateTime> CreatedON { get; set; }
        public string CreatedBY { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedbY { get; set; }
        public string BlockedBy { get; set; }
        public Nullable<System.DateTime> BlockedDATE { get; set; }
        public string ProfileImage { get; set; }
        public string DeviceId { get; set; }
        public string DistributorCode { get; set; }
        public string FSECode { get; set; }
        public string MagicalFlag { get; set; }
        public string PermissionFlag { get; set; }
        public string HostName { get; set; }
        public string LastUpdated_web { get; set; }
        public string IPAddress { get; set; }
        public string session_cookies { get; set; }
        public Nullable<System.DateTime> ChangePasswordDate { get; set; }
        public Nullable<System.DateTime> ExpiryMaxDate { get; set; }
        public Nullable<int> ExpiryFlag { get; set; }
        public string OldPassword { get; set; }
        public Nullable<int> Locked { get; set; }
        public string WEB_PASSWORD { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string GoogleAddress { get; set; }

        public string Name { get; set; }     
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string SapCode { get; set; }
        public string Country { get; set; }

        //public List<SelectListItem> ddlSelectDitributer { get; set; }

        //public string[] ddlSelectDitributerIDs { get; set; }



        public List<SelectListItem> ddlSelectLanguage { get; set; }

        public string[] ddlSelectLanguageIDs { get; set; }

    }
}