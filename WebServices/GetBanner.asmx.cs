using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Luminous.EF;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Data.Objects;
using System.Net.Mail;
using System.Net.Configuration;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

using System.Web.Mvc;
namespace Luminous.WebServices
{
    /// <summary>
    /// Summary description for GetBanner
    /// </summary>
    /// 
    public class Products
    {
        public int Id;
        public String Name;
        public string ImageName;
        public string ImageFileName;
        public string Message;
        public string Status;
    }
    public class ProductLevelDetails
    {
        public int productCategoryId, productLevelOneId, ProductLevelTwoID, ProductLevelThreeId, ProductLevelFourId;
        public String ProductCategoryName, ProductLevelOneName, ProductLevelTWoName, PrdouctLevelThreeName, ProductLevelFourName, Message, Status;

    }
    public class ProductLevelDetailsVersion2
    {
        public int productCategoryId, productLevelOneId, ProductLevelTwoID, ProductLevelThreeId;
        public String ProductCategoryName, ProductLevelOneName, ProductLevelTWoName, PrdouctLevelThreeName, Message, Status;

    }
    public class ContactClass
    {
        public string ContactUsType;
        public string Address;
        public String email;
        public String Fax;
        public String PhoneNumber;
        public string Message;
        public string Status;
    }
    public class UsersProfile
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
    public class FullProductDetail
    {
        public int id;
        public string ProductLevelFourName;
        public string LevelTwoName;
        public string LevelOneName;
        public String LevelThreeName;
        public string ProductCode;
        public string ProductDescriptions;
        public string Status;
        public List<ProductImges> Image;

    }
    public class FullProductDetail2
    {
        public int id;
        public string ProductLevelFourName;
        public string LevelTwoName;
        public string LevelOneName;
        public String LevelThreeName;
        public string ProductCode;
        public string TechnicleSpecification;
        public string Warranty;
        public string KeyFeature;
        public string MRP;
        public string brochure;

        public string Error;
        public List<ProductImges> Image;

    }

    public class FullProductDetail2Version2
    {
        public int id;
        public string ProductLevelThreeName;
        public string LevelTwoName;
        public string LevelOneName;
        public String LevelThreeName;
        public string ProductCode;
        public string Rating;
        public string TechnicleSpecification;
        public string Warranty;
        public string KeyFeature;
        public string Status;
        public string MRP;
        public string brochure;
        public List<Product3Image> pr3image;
        public string Error;
        public List<ProductImges> Image;
        public List<TechnicalSpecification> tech;
    }

    public struct TechnicalSpecification
    {



        public string ColumnName;
        public string Value;



    }
    public struct ProductImges
    {
        public string FullFileName;
        public string Images;
    }

    public struct Product3Image
    {
        public string Pr3image;
    }
    public class PermotionList
    {
        public int id;
        public string PromotionType, ProductCategory, ProductlevelOne, Descriptons, ImagePath, startDate, enddate, PdfPath, MonthValue, Message, Status;

    }

    public class Notifications
    {
        public int id;
        public string Subject;
        public string Text;
        public bool isread;
        public string AlertFlag;
        public string Image;
        public DateTime Date;
        public string Message;
        public string Status;
    }
    public class AlertNotifications
    {
        public int id;

        public string Text;
        public bool isread;
        public string Imagename;
        public string Imagepath;
        public DateTime Date;
        public string Message;

        public string Status;
    }
    public class DataContest
    {
        public string id;
        public string ImageEncode;
        public string DealerName;
        public string DealerCity;
        public string DealerPhone;
        public string DealerEmail;
        public string DistributorCode;
        public string Empcode;
        public string DealerId;
        public int Flag;
        public string Error;
        public string DealerFirmName;
        public string DealerState;
        public string ParentCategory;

    }
    public class ProductlevelThree
    {
        public string ProductName { get; set; }
        public string Error { get; set; }
        public string Status { get; set; }
    }

    public class LuminiousUpdates
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Id { get; set; }
        public string VideoURL { get; set; }
        public string Error { get; set; }
        public string Status { get; set; }
    }
    public class UserVerifications
    {
        public string status;
        public string Token;
        public string Message;

    }

    public class CustomerPermission
    {
        public string ModuleName;
        public string CustomerType;
        public string Permission;
        public int MonthValue;
        public string ModuleImage;
        public string Message;
        public string Status;
    }

    public class LabelData
    {
        public int Id;
        public string FooterCatName;
        public string Message;
        public string Status;
    }
    public class ProductVideoList
    {
        public int Id;
        public string VideoName;
        public string Url;
        public string VideoImage;
        public string Message;
        public string Status;

    }
    public class NotificationSurvey
    {
        public int? SurveyId;
        public string Survey;
        public string QuestionType;
        public string QuestionTitle;
        public string OptionA;
        public string OptionB;
        public string OptionC;
        public string OptionD;
        public string OptionE;
        public string CorrectAns;
        public DateTime? StartDate;
        public DateTime? EndDate;
        public int? CreatedBy;
        public DateTime? CreatedOn;
        public string Message;
        public string Status;
    }

    public class PollQuestion
    {
        public int QuestionId;
        public string Question;
        public string OptionA;
        public string OptionB;
        public string OptionC;
        public string OptionD;
        public string CorrectAns;
        public DateTime? StartDate;
        public DateTime? EndDate;
        public string CreatedBy;
        public DateTime? CreatedOn;
        public string Message;
        public string Status;
        public string date;

    }
    public class RedirectPage
    {
        // public int? SurveyId;
        public string PageName;
        public string ParentCategory;
        public string ProductCategory;
        public string ProductLevelOne;
        public string ProductLevelTwo;
        public string ProductLevelThree;
        public string Media;
        public int month;
        public int year;
        // public string 
        public string Message;
        public string Status;
    }
    public class Gallery
    {
        public string id;
        public string ImageName;

        public Nullable<DateTime> date;

        public string Message;
        public string Status;

    }
    public class State
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
    public class Town
    {

        public int TownID { get; set; }
        public string TownName { get; set; }
    }

    public class ParentCategoryData
    {

        public int Parentcatid { get; set; }
        public string Parentcatname { get; set; }
    }

    public class WRS_Date
    {
        public Nullable<DateTime> Newdate { get; set; }
        public Nullable<DateTime> Olddate { get; set; }
        public Nullable<DateTime> Maxdate { get; set; }
        public Nullable<DateTime> Mindate { get; set; }
    }
    //Added new class for getting ticket data by Ravi on 26-06-2018 Taskid-4009

    public class GetTicket
    {
        public Nullable<DateTime> Date { get; set; }
        public string serialno { get; set; }
        public string attachment { get; set; }
        public string status { get; set; }
        public string createdby { get; set; }
        public string Description { get; set; }
        public string message { get; set; }
        public string distcode { get; set; }
        public string distname { get; set; }
        public string custname { get; set; }
        public string custmobile { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public Nullable<DateTime> dateofsale { get; set; }
        public int Id { get; set; }
    }
    public class contestpitcure
    {

        public string DealerID { get; set; }

        public Nullable<Boolean> Flag { get; set; }

        public Nullable<int> MarqueeFlag { get; set; }
        public string Marquee { get; set; }
        public string Message { get; set; }
        public int MarqueeId { get; set; }
    }

    public class getnamebydistcode
    {

        public string Distname { get; set; }
        public string Distcode { get; set; }
        public string imagescount { get; set; }
        public string Message { get; set; }

        public string quespollflag { get; set; }
        public string quessubmitflag { get; set; }

    }

    public class Getcontestinfo_data
    {
        public string Question { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAns { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string imagename { get; set; }
    }

    //

    //Add new feature Lucky 7 contest by Ravi//

    public class CouponData
    {
        public Nullable<int> EligibleCouponCount { get; set; }
        public Nullable<int> ActivatedCouponCount { get; set; }
        public Nullable<int> BalanceCouponCount { get; set; }
        public Nullable<int> CouponReimbursment { get; set; }
        public Nullable<int> OpenReimbursment { get; set; }
        public string Message { get; set; }
    }

    public class Distdata
    {
        public string AlphanumericCode { get; set; }
        public Nullable<DateTime> ActivatedDateTime { get; set; }
        public string Message { get; set; }
    }

    public class DealerRedeemeddata
    {
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string SecretCode { get; set; }
        public string AlphanumericCode { get; set; }
        public string GiftName { get; set; }
        public string ActivationQrCode { get; set; }
        public string DistActivatedDateTime { get; set; }
        public string DealerredemptionDateTime { get; set; }
        public string ActivationDateAndTime { get; set; }
        public string ClaimSubmissionDate { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }
    }


    public class GiftData
    {
        public int GiftId { get; set; }
        public string GiftName { get; set; }
        public string GiftDescription { get; set; }
        public string GiftImage { get; set; }

        public string Message { get; set; }
    }
    public class DealerReport
    {
        //public int GiftId { get; set; }
        public string Barcode { get; set; }
        public string Gift { get; set; }
        public string ActivatedDistName { get; set; }
        public string SecretCode { get; set; }
        public Nullable<DateTime> ReimbursmentDate_Time { get; set; }
        public string Message { get; set; }
    }
    public class TermsCondition_Scheme
    {
        //public int GiftId { get; set; }
        public string TermsCondition { get; set; }
        public string schemeinfo { get; set; }
        public string UserType { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
    }
    //End//


    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class GetBanner : System.Web.Services.WebService
    {

        //Need to change the Url before Deploying On server;
        string Url = "http://166.62.100.102/";
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<Notifications> GetNotificaions(string userid, DateTime StartDate, DateTime EndDate, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {

            LuminousEntities luminous = new LuminousEntities();

            List<Notifications> notificationList = new List<Notifications>();

            string url = HttpContext.Current.Request.Url.ToString() + "/GetNotificaions";
            //  string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            if (userid == "" || userid == null)
            {
                Notifications notification = new Notifications();
                notification.id = -1;

                notification.Text = "Userid Is Empty";
                notificationList.Add(notification);
                return notificationList;
            }
            try
            {
                var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                if (checkstatus.Count() == 0)
                {
                    string LoginToken = "";
                    string TokenString = userid + Appversion;
                    using (MD5 md5Hash = MD5.Create())
                    {

                        LoginToken = GetMd5Hash(md5Hash, TokenString);


                    }
                    if (LoginToken == token)
                    {

                        //var  _notificationlist = luminous.GetNotification(userid).Where(c => (c.createdDate.Value.Date >= StartDate.Date && c.createdDate.Value.Month >= StartDate.Month && c.createdDate.Value.Year >= StartDate.Year) &&
                        //     (c.createdDate.Value.Date <= EndDate.Date && c.createdDate.Value.Month <= EndDate.Month && c.createdDate.Value.Year <= EndDate.Year) &&
                        //     c.C_status == 1).ToList();
                        //Add code for sorting the notification in descending order by Ravi on 01-08-2018 Task Id - 4282
                        var _notificationlist = luminous.GetNotification(userid).Where(c => (c.createdDate.Value.Date >= StartDate.Date) &&
                             (c.createdDate.Value.Date <= EndDate.Date) &&
                             c.C_status == 1).OrderByDescending(c => c.createdDate).ToList();
                        foreach (var i in _notificationlist)
                        {
                            Notifications notification = new Notifications();
                            notification.id = i.id;
                            notification.Text = i.C_text;
                            notification.isread = i.isRead == 1 ? true : false;
                            notification.Subject = i.C_Subject;
                            notification.AlertFlag = i.Alertflag;

                            var imagedata = luminous.Notifications.Where(c => c.id == notification.id).Select(c => c.ImagePath).SingleOrDefault();
                            if (imagedata == null)
                            {
                                notification.Image = "0";
                            }
                            else
                            {
                                notification.Image = Url + "NotificationImage/" + imagedata;
                            }

                            notification.Date = Convert.ToDateTime(i.createdDate);
                            notification.Message = "Success";

                            notificationList.Add(notification);
                        }
                        string RequestParameter = "UserID :" + userid + ",StartDate :" + StartDate + ",EndDate :" + EndDate + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "id,Text,Isread,Subject,AlertFlag,Message:Success";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        return notificationList;
                    }
                    else
                    {
                        Notifications notification = new Notifications();
                        notification.id = -1;
                        notification.Subject = "0";

                        notification.Message = "Unauthorized Access";
                        notificationList.Add(notification);
                        string RequestParameter = "UserID :" + userid + ",StartDate :" + StartDate + ",EndDate :" + EndDate + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id :-1,Subject :0,Message :Unauthorized Access";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        return notificationList;
                    }
                }
                else
                {
                    Notifications notification = new Notifications();
                    notification.id = -1;
                    notification.Subject = "0";
                    notification.Status = "1";
                    notification.Message = "User already logged in three devices.";
                    notificationList.Add(notification);
                    return notificationList;
                }
            }
            catch (Exception ex)
            {
                Notifications notification = new Notifications();
                notification.id = -1;
                notification.Subject = "0";

                notification.Message = ex.Message.ToString();
                notificationList.Add(notification);
                string RequestParameter = "UserID :" + userid + ",StartDate :" + StartDate + ",EndDate :" + EndDate + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Id :-1,Subject :0,Message :Some exception has occurred";
                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, ex.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                //SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, ex.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                return notificationList;
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<AlertNotifications> GetAlertnotification(string userid, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {

            LuminousEntities luminous = new LuminousEntities();

            List<AlertNotifications> AlertnotificationList = new List<AlertNotifications>();
            string url = "";
            url = HttpContext.Current.Request.Url.ToString() + "/GetAlertnotification";
            // string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            if (userid == "" || userid == null)
            {
                AlertNotifications notification = new AlertNotifications();
                notification.id = -1;

                notification.Text = "Userid Is Empty";
                AlertnotificationList.Add(notification);
                return AlertnotificationList;
            }
            try
            {
                var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                if (checkstatus.Count() == 0)
                {
                    string LoginToken = "";
                    string TokenString = userid + Appversion;
                    using (MD5 md5Hash = MD5.Create())
                    {

                        LoginToken = GetMd5Hash(md5Hash, TokenString);


                    }
                    if (LoginToken == token)
                    {
                        var AlertReadStatusCount = luminous.AlertNotificationReadStatus.Where(c => c.DeviceId == deviceid && c.UserId == userid).ToList();
                        if (AlertReadStatusCount.Count == 0)
                        {

                            var getnotificationdata = luminous.GetAlertNotificationByUserId(userid).Where(c => c.C_status == 1).ToList();

                            foreach (var data in getnotificationdata)
                            {
                                var alertnotificationidexist = luminous.AlertNotificationReadStatus.Where(c => c.NotificationId == data.id && c.UserId == userid && c.DeviceId == deviceid && c.IsRead == true).ToList();


                                if (alertnotificationidexist.Count == 0)
                                {
                                    url = HttpContext.Current.Request.Url.ToString() + "/GetAlertnotification";


                                    AlertNotifications anotification = new AlertNotifications();
                                    anotification.id = data.id;
                                    anotification.Text = data.C_text;
                                    anotification.isread = false;
                                    //anotification.Status =data.C_Status.ToString();
                                    anotification.Imagename = data.Imagename;
                                    anotification.Imagepath = Url + "NotificationImage/" + data.Imagepath;
                                    AlertnotificationList.Add(anotification);
                                }
                                else if (alertnotificationidexist.Count != 0)
                                {

                                    //AlertNotifications anotification = new AlertNotifications();
                                    AlertNotifications anotification = new AlertNotifications();
                                    anotification.id = data.id;
                                    anotification.Text = data.C_text;
                                    anotification.isread = true;
                                    //anotification.Status =data.C_Status.ToString();
                                    anotification.Imagename = data.Imagename;
                                    anotification.Imagepath = Url + "NotificationImage/" + data.Imagepath;
                                    AlertnotificationList.Add(anotification);


                                }


                            }

                            // }




                            //AlertNotificationReadStatu _notifications = new AlertNotificationReadStatu();




                        }
                        else
                        {

                            var getnotificationdata = luminous.GetAlertNotificationByUserId(userid).Where(c => c.C_status == 1).ToList();

                            foreach (var data in getnotificationdata)
                            {
                                var alertnotificationidexist = luminous.AlertNotificationReadStatus.Where(c => c.NotificationId == data.id && c.UserId == userid && c.DeviceId == deviceid && c.IsRead == true).ToList();
                                if (alertnotificationidexist.Count == 0)
                                {



                                    AlertNotifications anotification = new AlertNotifications();
                                    anotification.id = data.id;
                                    anotification.Text = data.C_text;
                                    anotification.isread = false;
                                    //anotification.Status =data.C_Status.ToString();
                                    anotification.Imagename = data.Imagename;
                                    anotification.Imagepath = Url + "NotificationImage/" + data.Imagepath;
                                    AlertnotificationList.Add(anotification);
                                }
                                else if (alertnotificationidexist.Count != 0)
                                {

                                    //AlertNotifications anotification = new AlertNotifications();
                                    AlertNotifications anotification = new AlertNotifications();
                                    anotification.id = data.id;
                                    anotification.Text = data.C_text;
                                    anotification.isread = true;
                                    //anotification.Status =data.C_Status.ToString();
                                    anotification.Imagename = data.Imagename;
                                    anotification.Imagepath = Url + "NotificationImage/" + data.Imagepath;
                                    AlertnotificationList.Add(anotification);


                                }

                            }

                        }

                        string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "id,Text,Isread,Subject,AlertFlag,Message:Success";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        return AlertnotificationList;
                    }
                    else
                    {
                        AlertNotifications notification = new AlertNotifications();
                        notification.id = -1;
                        notification.Text = "0";

                        notification.Message = "Unauthorized Access";
                        AlertnotificationList.Add(notification);
                        string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id :-1,Subject :0,Message :Unauthorized Access";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        return AlertnotificationList;
                    }
                }
                else
                {
                    AlertNotifications notification = new AlertNotifications();
                    notification.id = -1;
                    notification.Text = "0";
                    notification.Status = "1";
                    notification.Message = "User already logged in three devices.";
                    AlertnotificationList.Add(notification);
                    return AlertnotificationList;
                }
            }
            catch (Exception ex)
            {
                AlertNotifications notification = new AlertNotifications();
                notification.id = -1;
                notification.Text = "0";

                notification.Message = "Some exception has occurred";
                AlertnotificationList.Add(notification);
                string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Id :-1,Subject :0,Message :Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, ex.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                return AlertnotificationList;
            }

        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]

        public string IsReadCheck(string userid, string deviceid, string notificationid, string Isread)
        {
            try
            {
                LuminousEntities luminous = new LuminousEntities();

                AlertNotificationReadStatu alreadstatus = new AlertNotificationReadStatu();
                alreadstatus.NotificationId = Convert.ToInt32(notificationid);
                alreadstatus.IsRead = true;
                alreadstatus.UserId = userid;
                alreadstatus.DeviceId = deviceid;
                alreadstatus.NotificationId = Convert.ToInt32(notificationid);
                luminous.AlertNotificationReadStatus.AddObject(alreadstatus);

                //luminous.ExecuteStoreCommand("Insert into AlertNotificationReadStatus values('" + notificationid + "','" + Isread + "','" + userid + "','" + deviceid + "'");
                //luminous.ExecuteStoreCommand("Update AlertNotificationReadStatus set IsRead=1,UserId='" + userid + "',DeviceId='" + deviceid + "' where NotificationId='" + notificationid + "'");
                luminous.SaveChanges();
                return "Successfully Inserted";
            }
            catch
            {
                return "Some exeption has Occurred";
            }

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public string Suggestion(string userId, int functionId, string suggestion, byte[] Image, string filename, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            string url = HttpContext.Current.Request.Url.ToString() + "/Suggestion";

            // LuminousEntities luminous = new LuminousEntities();
            string FileName = filename;

            if (userId == null || userId == "")
            {
                return "UserId Has NO Value";
            }
            UsersList user = luminous.UsersLists.Single(c => c.UserId == userId);
            if (luminous.UsersLists.Any(c => c.UserId == userId))
            {
                try
                {
                    string LoginToken = "";
                    string TokenString = userId + Appversion;
                    using (MD5 md5Hash = MD5.Create())
                    {

                        LoginToken = GetMd5Hash(md5Hash, TokenString);


                    }
                    string s = "";
                    if (LoginToken == token)
                    {

                        SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                        string sendTo = luminous.FUNCTIONS.Where(a => a.ID == functionId).FirstOrDefault().fEMAIL;


                        MemoryStream ms = new MemoryStream();
                        MailMessage mail = new MailMessage(smtpSection.Network.UserName, sendTo);
                        mail.Subject = "Suggestion Recieved from " + user.Dis_Name + " on " + DateTime.Now.ToShortDateString();
                        mail.Body = "Dear Sir <br/> <br/> Please find enclosed details of suggestion recieved<br/><br/> From " + user.Dis_Name + "<br/> Date: " + DateTime.Now.ToShortDateString() + "<br/> Suggestion " + suggestion;
                        if (Image != null)
                        {
                            ms = new MemoryStream(Image);
                            Attachment atch = new Attachment(ms, filename);
                            mail.Attachments.Add(atch);
                        }
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient(smtpSection.Network.Host, smtpSection.Network.Port);
                        smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                        smtp.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        smtp.EnableSsl = smtpSection.Network.EnableSsl;
                        smtp.Send(mail);
                        Suggestion _suggestion = new Suggestion();
                        _suggestion.CreateDate = DateTime.Now;
                        _suggestion.createdBy = userId;
                        _suggestion.FunctionID = functionId;
                        _suggestion.Suggestion1 = suggestion;
                        _suggestion.SentMailDate = DateTime.Now;
                        _suggestion.SentMailStatus = true;
                        if (filename != "")
                        {
                            filename = Path.GetFileNameWithoutExtension(filename) + DateTime.Now.ToString("ddMMyyyHHmmss");
                            _suggestion.ImageName = filename + Path.GetExtension(FileName);
                        }

                        luminous.Suggestions.AddObject(_suggestion);
                        if (luminous.SaveChanges() > 0)
                        {
                            if (Image != null)
                            {
                                String FileExtention = Path.GetExtension(FileName);
                                FileStream fs = new FileStream(Server.MapPath("~/SuggestionImage/") + filename + FileExtention, FileMode.Create);
                                ms.WriteTo(fs);
                                ms.Close();
                                fs.Close();
                            }
                        }
                        return "Mail Sent";
                    }
                    else
                    {
                        return "Unautorized Access";
                    }

                }
                catch (SmtpException ex)
                {
                    Suggestion _suggestion = new Suggestion();
                    _suggestion.CreateDate = DateTime.Now;
                    _suggestion.createdBy = userId;
                    _suggestion.FunctionID = functionId;
                    _suggestion.Suggestion1 = suggestion;
                    _suggestion.SentMailDate = DateTime.Now;
                    _suggestion.SentMailStatus = false;
                    if (filename != "")
                    {
                        filename = Path.GetFileNameWithoutExtension(filename) + DateTime.Now.ToString("ddMMyyyHHmmss");
                        _suggestion.ImageName = filename + Path.GetExtension(FileName);
                    }
                    luminous.Suggestions.AddObject(_suggestion);

                    if (luminous.SaveChanges() > 0)
                    {
                        if (Image != null)
                        {
                            MemoryStream ms = new MemoryStream(Image);
                            String FileExtention = Path.GetExtension(FileName);
                            FileStream fs = new FileStream(Server.MapPath("~/SuggestionImage/") + filename + FileExtention, FileMode.Create);
                            ms.WriteTo(fs);
                            ms.Close();
                            fs.Close();
                        }
                    }

                    string RequestParameter = "UserID :" + userId + ",FunctionID :" + functionId + ",Suggetion :" + suggestion + ",FileName :" + filename + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Some exception has occured";

                    //SaveServiceLog(userId, url, RequestParameter, ResponseParameter, 1, ex.InnerException.ToString(),userId, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                    return "Mail Not Sent";

                }
            }
            else
            {
                return "UserId Not Exists";
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public XmlDocument GetBannerImage(string date, string userid, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement ele = doc.CreateElement("Image");
            XmlElement banid = doc.CreateElement("ID");
            //XmlElement elestatus = doc.CreateElement("Status");
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetBannerImage";
            doc.AppendChild(ele);
            //doc.AppendChild(elestatus);
            LuminousEntities luminous = new LuminousEntities();
            string Image = "";
            string ID = "";
            string Status = "";
            try
            {
                var getappversion = luminous.AppVersions.Where(c => c.Version == Appversion).ToList();
                if (getappversion.Count > 0)
                {
                    string LoginToken = "";
                    string TokenString = userid + Appversion;
                    using (MD5 md5Hash = MD5.Create())
                    {

                        LoginToken = GetMd5Hash(md5Hash, TokenString);


                    }
                    if (LoginToken == token)
                    {


                        DateTime datea = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                        //try
                        //{
                        //    Banner ban = luminous.Banners.Single(a => a.stardate <= datea && a.ExpriyDate >= datea && a.BStatus == 1);
                        //    if (File.Exists(Server.MapPath("~/Banners/") + ban.BannerImage))
                        //    {
                        //        Image = Url + "Banners/" + ban.BannerImage;
                        //    }
                        //    else
                        //    {
                        //        Image = "No Image";
                        //    }
                        //}
                        //catch
                        //{

                        //    Image = "No Image";
                        //}
                        try
                        {
                            var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                            if (checkstatus.Count() == 0)
                            {
                                var ban = luminous.Banners.Where(a => a.stardate <= datea && a.ExpriyDate >= datea && a.BStatus == 1).OrderBy(c => c.Sequence).ToList();
                                foreach (var data in ban)
                                {
                                    ID = data.id.ToString();

                                    if (File.Exists(Server.MapPath("~/Banners/") + data.BannerImage))
                                    {
                                        Image = Url + "Banners/" + data.BannerImage;
                                    }
                                    else
                                    {
                                        Image = "No Image";
                                    }

                                    XmlText text2 = doc.CreateTextNode(Image + "&" + ID);
                                    ele.AppendChild(text2);


                                }
                                string RequestParameter = "Date :" + date + ",UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Image :" + Image + "";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                            else
                            {
                                Image = "User already logged in three devices";
                                // Status = "1";
                                XmlText text2 = doc.CreateTextNode(Image);
                                ele.AppendChild(text2);
                                string RequestParameter = "Date :" + date + ",UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Image :" + Image + "";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                                //XmlText textstatus = doc.CreateTextNode(Status);
                                //elestatus.AppendChild(textstatus);
                            }

                        }
                        catch (Exception exc)
                        {

                            Image = "No Image";
                            XmlText text2 = doc.CreateTextNode(Image);
                            ele.AppendChild(text2);
                            string RequestParameter = "Date :" + date + ",UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Image :" + Image + "";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        }
                    }
                    else
                    {
                        Image = "Unauthorized Access";
                        XmlText text2 = doc.CreateTextNode(Image);
                        ele.AppendChild(text2);
                        string RequestParameter = "Date :" + date + ",UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Image :" + Image + "";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, Image, userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }

                }
                else
                {
                    Image = "Please update your app version";
                    XmlText text2 = doc.CreateTextNode(Image);
                    ele.AppendChild(text2);
                    string RequestParameter = "Date :" + date + ",UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Image :" + Image + "";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, Image, userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }
            }
            catch (FormatException ex)
            {
                XmlText text = doc.CreateTextNode("In Valid Date");
                ele.AppendChild(text);
                string RequestParameter = "Date :" + date + ",UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Error :" + text.InnerText + "";
                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, ex.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                return doc;
            }
            return doc;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public string UpdateProfile(string userID, string mobileNo, string email, string Address1, Byte[] Image, string ImageName, string token, string Appversion, string deviceid, string Ostype, string Osversion, string distributorcode, string fsecode)
        {
            LuminousEntities luminous = new LuminousEntities();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/UpdateProfile";
            string result = String.Empty;

            int checkValidation = 0;
            string LoginToken = "";
            string TokenString = userID + Appversion;
            using (MD5 md5Hash = MD5.Create())
            {

                LoginToken = GetMd5Hash(md5Hash, TokenString);


            }
            if (LoginToken == token)
            {

                var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userID);
                if (checkstatus.Count() == 0)
                {
                    //Regex PhoneCheck = new Regex("^(?:(?:\\+?1\\s*(?:[.-]\\s*)?)?(?:\\(\\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\\s*\\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\\s*(?:[.-]\\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\\s*(?:[.-]\\s*)?([0-9]{4})(?:\\s*(?:#|x\\.?|ext\\.?|extension)\\s*(\\d+))?$");
                    Regex emailCheck = new Regex("^((\"[\\w-\\s]+\")|([\\w-]+(?:\\.[\\w-]+)*)|(\"[\\w-\\s]+\")([\\w-]+(?:\\.[\\w-]+)*))(@((?:[\\w-]+\\.)*\\w[\\w-]{0,66})\\.([a-z]{2,6}(?:\\.[a-z]{2})?)$)|(@\\[?((25[0-5]\\.|2[0-4][0-9]\\.|1[0-9]{2}\\.|[0-9]{1,2}\\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\\]?$)");
                    if (userID.Length > 99)
                    {
                        checkValidation = 1;
                        result = "Character In UserId Should Be Less Than 100";
                    }
                    if (email.Length > 99)
                    {
                        checkValidation = 1;
                        result = "Character In Email Should Be Less Than 100";
                    }
                    if (Address1.Length > 499)
                    {
                        checkValidation = 1;
                        result = "Character In Address1 Should Be Less Than 500";
                    }

                    if (!emailCheck.IsMatch(email))
                    {
                        // checkValidation = 0;
                        email = "no@mail.com";
                        // result = "Invalid Email ID";
                    }

                    if (checkValidation == 0)
                    {
                        if (luminous.UsersLists.Any(a => a.UserId == userID))
                        {
                            try
                            {
                                string filename = Path.GetFileNameWithoutExtension(ImageName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(ImageName);
                                UsersList user = luminous.UsersLists.Single(a => a.UserId == userID && a.isActive == 1);
                                user.Dis_ContactNo = mobileNo;
                                user.Dis_Address1 = Address1;
                                user.FSECode = fsecode.Substring(3, fsecode.Length - 3);
                                user.DistributorCode = distributorcode;

                                user.ProfileImage = filename;
                                user.Dis_Email = email;
                                int affectedRows = luminous.SaveChanges();
                                if (affectedRows > 0)
                                {
                                    string str = Path.Combine(Server.MapPath("~/ProfileImages/"), filename);
                                    BinaryWriter bw = new BinaryWriter(new FileStream(str, FileMode.Create, FileAccess.Write));
                                    bw.Write(Image);
                                    bw.Close();

                                    result = "Profile Updated";
                                    string RequestParameter = "UserID :" + userID + ",MobileNo :" + mobileNo + ",Email :" + email + ",Address1 :" + Address1 + ",ImageName :" + ImageName + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + ",DistributorCode:" + distributorcode + ",FseCode:" + fsecode + "";
                                    string ResponseParameter = "Result :Profile Updated";

                                    SaveServiceLog(userID, url, RequestParameter, ResponseParameter, 0, "Success", userID, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                                }
                            }
                            catch (Exception ex)
                            {
                                result = "Some exception has occurred";
                                string RequestParameter = "UserID :" + userID + ",MobileNo :" + mobileNo + ",Email :" + email + ",Address1 :" + Address1 + ",ImageName :" + ImageName + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + ",DistributorCode:" + distributorcode + ",FseCode:" + fsecode + "";
                                string ResponseParameter = "Result :Some exception has occurred";

                                SaveServiceLog(userID, url, RequestParameter, ResponseParameter, 1, ex.InnerException.ToString(), userID, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                        }
                        else
                        {
                            result = "Unautorized Access";
                            string RequestParameter = "UserID :" + userID + ",MobileNo :" + mobileNo + ",Email :" + email + ",Address1 :" + Address1 + ",ImageName :" + ImageName + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + ",DistributorCode:" + distributorcode + ",FseCode:" + fsecode + "";
                            string ResponseParameter = "Result :Unautorized Access";

                            SaveServiceLog(userID, url, RequestParameter, ResponseParameter, 1, "Unautorized Access", userID, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                    }
                    else
                    {
                        result = "UserId Not Exists";
                        string RequestParameter = "UserID :" + userID + ",MobileNo :" + mobileNo + ",Email :" + email + ",Address1 :" + Address1 + ",ImageName :" + ImageName + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + ",DistributorCode:" + distributorcode + ",FseCode:" + fsecode + "";
                        string ResponseParameter = "Result :UserId Not Exists";

                        SaveServiceLog(userID, url, RequestParameter, ResponseParameter, 1, "UserId Not Exists", userID, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {

                    result = "User already logged in three devices";
                    string RequestParameter = "UserID :" + userID + ",MobileNo :" + mobileNo + ",Email :" + email + ",Address1 :" + Address1 + ",ImageName :" + ImageName + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + ",DistributorCode:" + distributorcode + ",FseCode:" + fsecode + "";
                    string ResponseParameter = "Result :User already logged in three devices";

                    SaveServiceLog(userID, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userID, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            return result;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<Products> getProducts(string userid, string Parentcategoryname, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<Products> product = new List<Products>();
            string url = HttpContext.Current.Request.Url.ToString() + "/getProducts";
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            try
            {
                var getappversion = luminous.AppVersions.Where(c => c.Version == Appversion).ToList();
                if (getappversion.Count > 0)
                {
                    string LoginToken = "";
                    string TokenString = userid + Appversion;
                    using (MD5 md5Hash = MD5.Create())
                    {

                        LoginToken = GetMd5Hash(md5Hash, TokenString);


                    }
                    if (userid == "9900000004")
                    {
                        token = LoginToken;
                    }
                    if (LoginToken == token)
                    {

                        var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                        if (checkstatus.Count() == 0)
                        {
                            var getparentcatid = luminous.ParentCategories.Where(c => c.PCName == Parentcategoryname).Select(c => c.Pcid).SingleOrDefault();
                            var productCategoryList = (from c in luminous.ProductCatergories
                                                       where c.Pstatus == 1 && c.ParentCatid == getparentcatid
                                                       orderby c.ordersequence
                                                       select new
                                                       {
                                                           productName = c.PName,
                                                           Id = c.id
                                                       }).ToList();


                            foreach (var i in productCategoryList)
                            {
                                Products pr = new Products();
                                pr.Id = i.Id;
                                if (i.productName == "HKVA")
                                {
                                    pr.Name = i.productName;
                                }
                                else
                                {
                                    pr.Name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(i.productName.ToLower());
                                }

                                pr.Message = "Success";
                                product.Add(pr);
                                string RequestParameter = "UserID :" + userid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Id :1,Name :Product has not been added in database yet,Message :Failure";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                            }
                            if (productCategoryList.Count < 1)
                            {
                                Products pr = new Products();
                                pr.Id = 1;
                                pr.Name = "Product has not been added in database yet";
                                pr.Message = "Failure";
                                product.Add(pr);
                                string RequestParameter = "UserID :" + userid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Id :1,Name :Product has not been added in database yet,Message :Failure";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Failure", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                            }
                        }
                        else
                        {
                            Products pr = new Products();
                            pr.Id = 0;
                            pr.Name = "0";
                            pr.Status = "1";
                            pr.Message = "User already logged in three devices";
                            product.Add(pr);
                        }
                    }
                    else
                    {
                        Products pr = new Products();
                        pr.Id = -1;
                        pr.Name = "0";
                        pr.Message = "Unauthorized Access";
                        product.Add(pr);
                        string RequestParameter = "UserID :" + userid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id :-1,Name :0,Message :Unauthorized Access";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    Products pr = new Products();
                    pr.Id = -1;
                    pr.Name = "0";
                    pr.Message = "Please update your app version";
                    product.Add(pr);
                    string RequestParameter = "UserID :" + userid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Id :-1,Name :0,Message :Some exception has occurred";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Please update your app version", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }
            }
            catch (Exception exc)
            {
                Products pr = new Products();
                pr.Id = -1;
                pr.Name = "0";
                pr.Message = "Some exception has occurred";
                product.Add(pr);
                string RequestParameter = "UserID :" + userid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Id :-1,Name :0,Message :Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return product;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<Products> GetProductLevelOne(string userid, string ProductId, string Parentcategoryname, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<Products> product = new List<Products>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetProductLevelOne";
            try
            {
                int pid;
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (userid == "9900000004")
                {
                    token = LoginToken;
                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        if (!int.TryParse(ProductId, out pid))
                        {
                            Products pr = new Products();
                            pr.Id = 1;
                            pr.Name = "Ivalid Product Id";
                            product.Add(pr);
                            return product;
                        }
                        var getparentcatid = luminous.ParentCategories.Where(c => c.PCName == Parentcategoryname).Select(c => c.Pcid).SingleOrDefault();
                        var productCategoryList = (from c in luminous.ProductLevelOnes
                                                   where c.pcId == pid && c.PlStatus == 1 && c.ParentCatid == getparentcatid
                                                   select new
                                                   {
                                                       productName = c.Name,
                                                       Id = c.id,
                                                       Sequence = c.OrderSequence
                                                   }).OrderBy(c => c.Sequence).ToList();


                        foreach (var i in productCategoryList)
                        {
                            Products pr = new Products();
                            pr.Id = i.Id;
                            pr.Name = i.productName;
                            pr.Message = "Success";
                            product.Add(pr);
                            string RequestParameter = "UserID :" + userid + ",ProductID :" + ProductId + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Id :1,Message :Success";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                        if (productCategoryList.Count < 1)
                        {
                            Products pr = new Products();
                            pr.Id = 0;
                            // pr.Name = "Product has not been added in database yet";
                            pr.Message = "Failure";
                            product.Add(pr);
                            string RequestParameter = "UserID :" + userid + ",ProductID :" + ProductId + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Id :1,Name :Product has not been added in database yet,Message :Failure";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Failure", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                    }
                    else
                    {
                        Products pr = new Products();
                        pr.Id = 0;
                        pr.Name = "0";
                        pr.Status = "1";
                        pr.Message = "User already logged in three devices";
                        product.Add(pr);
                        string RequestParameter = "UserID :" + userid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id :-1,Name :0,Message :User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    Products pr = new Products();
                    pr.Id = -1;
                    pr.Name = "0";
                    pr.Message = "Unauthorized Access";
                    product.Add(pr);
                    string RequestParameter = "UserID :" + userid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Id :-1,Name :0,Message :Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }
            }
            catch (Exception exc)
            {
                Products pr = new Products();
                pr.Id = -1;
                pr.Name = "0";
                pr.Message = "Some exception has occurred.";
                product.Add(pr);
                string RequestParameter = "UserID :" + userid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Id :-1,Name :0,Message :Some exception has occurred.";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.Message.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return product;

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<Products> GetProductLevelTwo(string userid, int ProductLevelOne, int productid, string Parentcategoryname, string Appversion, string token, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<Products> product = new List<Products>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = "";

            //FOR INBT
            if (Parentcategoryname == "INBT" && productid == 9 && ProductLevelOne == 25)
            {
                url = HttpContext.Current.Request.Url.ToString() + "/GetProductLevelTwo/HomeUps_INVERLAST";
            }
            if (Parentcategoryname == "INBT" && productid == 9 && ProductLevelOne == 26)
            {
                url = HttpContext.Current.Request.Url.ToString() + "/GetProductLevelTwo/HomeUps_ELECTRA";
            }
            if (Parentcategoryname == "INBT" && productid == 8 && ProductLevelOne == 18)
            {
                url = HttpContext.Current.Request.Url.ToString() + "/GetProductLevelTwo/Battery_INVERLAST";
            }
            if (Parentcategoryname == "INBT" && productid == 8 && ProductLevelOne == 19)
            {
                url = HttpContext.Current.Request.Url.ToString() + "/GetProductLevelTwo/Battery_ELECTRA";
            }
            if (Parentcategoryname == "INBT" && productid == 15 && ProductLevelOne == 37)
            {
                url = HttpContext.Current.Request.Url.ToString() + "/GetProductLevelTwo/HKVA_HKVA";
            }
            //END

            //FOR Solar
            //if (Parentcategoryname == "Solar" && productid == 9 && ProductLevelOne == 25)
            //{
            //    url = HttpContext.Current.Request.Url.ToString() + "/GetProductLevelTwo/HomeUps_INVERLAST";
            //}
            //if (Parentcategoryname == "Solar" && productid == 9 && ProductLevelOne == 26)
            //{
            //    url = HttpContext.Current.Request.Url.ToString() + "/GetProductLevelTwo/HomeUps_ELECTRA";
            //}
            //if (Parentcategoryname == "Solar" && productid == 8 && ProductLevelOne == 18)
            //{
            //    url = HttpContext.Current.Request.Url.ToString() + "/GetProductLevelTwo/Battery_INVERLAST";
            //}
            //if (Parentcategoryname == "Solar" && productid == 8 && ProductLevelOne == 19)
            //{
            //    url = HttpContext.Current.Request.Url.ToString() + "/GetProductLevelTwo/Battery_ELECTRA";
            //}
            //if (Parentcategoryname == "Solar" && productid == 15 && ProductLevelOne == 37)
            //{
            //    url = HttpContext.Current.Request.Url.ToString() + "/GetProductLevelTwo/HKVA_HKVA";
            //}
            //END

            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (userid == "9900000004")
                {
                    token = LoginToken;
                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        var getparentcatid = luminous.ParentCategories.Where(c => c.PCName == Parentcategoryname).Select(c => c.Pcid).SingleOrDefault();
                        var productCategoryList = (from c in luminous.ProductLevelTwoes
                                                   where c.PrductID == productid && c.PlTwStatus == 1 && c.pc_Lvl_oneId == ProductLevelOne && c.ParentCatid == getparentcatid
                                                   select new
                                                   {
                                                       productName = c.Name,
                                                       Id = c.id,
                                                       ImageName = c.ImageName,
                                                       ImageFileName = c.ImageFileName,
                                                       Sequence = c.OrderSequence

                                                   }).OrderBy(c => c.Sequence).ToList();


                        foreach (var i in productCategoryList)
                        {
                            Products pr = new Products();
                            pr.Id = i.Id;
                            pr.Name = i.productName;
                            if (i.ImageFileName == null || i.ImageFileName == "")
                            {
                                pr.ImageFileName = "0";
                            }
                            else
                            {
                                pr.ImageFileName = i.ImageFileName;
                            }
                            if (i.ImageName == null || i.ImageName == "")
                            {
                                pr.ImageName = "0";
                            }
                            else
                            {
                                pr.ImageName = Url + "ProductImages/" + i.ImageName;

                            }
                            //pr.ImageFileName = i.ImageFileName;
                            //pr.ImageName = i.ImageName;
                            pr.Message = "Success";
                            product.Add(pr);


                        }
                        string RequestParameter = "UserID :" + userid + ",ProductLevelOne :" + ProductLevelOne + ",ProductiID :" + productid + ",ParnetCategoryName=" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id,Name,ImageFileName,ImageName,Message";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        if (productCategoryList.Count < 1)
                        {
                            Products pr = new Products();
                            pr.Id = -1;
                            pr.Name = "Product has not been added in database yet";
                            pr.ImageFileName = "0";
                            pr.ImageName = "0";
                            pr.Message = "Product not added";
                            product.Add(pr);

                        }
                    }
                    else
                    {
                        Products pr = new Products();
                        pr.Id = 0;
                        pr.Name = "0";
                        pr.ImageFileName = "0";
                        pr.ImageName = "0";
                        pr.Status = "1";
                        pr.Message = "User already logged in three devices";
                        product.Add(pr);
                        string RequestParameter = "UserID :" + userid + ",ProductLevelOne :" + ProductLevelOne + ",ProductiID :" + productid + ",ParentCateryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id :-1,Name :0,ImageFileName :0,ImageName :0,Message :User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    Products pr = new Products();
                    pr.Id = -1;
                    pr.Name = "0";
                    pr.ImageFileName = "0";
                    pr.ImageName = "0";
                    pr.Message = "Unauthorized Access";
                    product.Add(pr);
                    string RequestParameter = "UserID :" + userid + ",ProductLevelOne :" + ProductLevelOne + ",ProductiID :" + productid + ",ParentCateryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Id :-1,Name :0,ImageFileName :0,ImageName :0,Message :Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }
            }
            catch (Exception exc)
            {
                Products pr = new Products();
                pr.Id = -1;
                pr.Name = "0";
                pr.ImageFileName = "0";
                pr.ImageName = "0";
                pr.Message = "Some exception has occurred.";
                product.Add(pr);
                string RequestParameter = "UserID :" + userid + ",ProductLevelOne :" + ProductLevelOne + ",ProductiID :" + productid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Id :-1,Name :0,ImageFileName :0,ImageName :0,Message :Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return product;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<Products> GetProductLevelthree(string userid, int productlevelTwo, int ProductLevelOne, int productid, string Appversion, string token, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<Products> product = new List<Products>();

            string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {

                    var productCategoryList = (from c in luminous.ProductLevelThreefs
                                               where c.pc_Lvl_Id == ProductLevelOne && c.PlTwStatus == 1 && c.pc_Lv2_Id == productlevelTwo && c.PrductID == productid
                                               select new
                                               {
                                                   productName = c.Name,
                                                   Id = c.id
                                               }).ToList();


                    foreach (var i in productCategoryList)
                    {
                        Products pr = new Products();
                        pr.Id = i.Id;
                        pr.Name = i.productName;
                        product.Add(pr);
                    }
                    if (productCategoryList.Count < 1)
                    {
                        Products pr = new Products();
                        pr.Id = 1;
                        pr.Name = "Product has not been added in database yet";
                        product.Add(pr);
                    }
                }
                else
                {

                }
            }
            catch (Exception exc)
            {

            }
            return product;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<Products> GetProductLevelFour(string userid, int productlevelThree, int productlevelTwo, int ProductLevelOne, int productid)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<Products> product = new List<Products>();
            var productCategoryList = (from c in luminous.ProductLevelThrees
                                       where c.productCategoryid == productid && c.PlTwStatus == 1 && c.ProductLevelOne == ProductLevelOne
                                       && c.pc_Lv2_oneId == productlevelTwo && c.productLevelThreefId == productlevelThree
                                       select new
                                       {
                                           productName = c.Name,
                                           Id = c.id
                                       }).ToList();


            foreach (var i in productCategoryList)
            {
                Products pr = new Products();
                pr.Id = i.Id;
                pr.Name = i.productName;
                product.Add(pr);
            }
            if (productCategoryList.Count < 1)
            {
                Products pr = new Products();
                pr.Id = 1;
                pr.Name = "Product has not been added in database yet";
                product.Add(pr);
            }
            return product;

        }
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]

        //public FullProductDetail GetFullProductDetail(string userid, int ProductLevelFourId)
        //{
        //    LuminousEntities luminous = new LuminousEntities();
        //    int productTwo = ProductLevelFourId;

        //    List<GetProduct_Result> productDetails = luminous.GetProduct(userid).ToList();
        //    if (!productDetails.Any(a => a.ProductLevelFourId == ProductLevelFourId))
        //    {
        //        FullProductDetail pr = new FullProductDetail();
        //        pr.id = -1;
        //        pr.LevelTwoName = "NO Access ";
        //        return pr;
        //    }
        //    if (luminous.UsersLists.Any(a => a.UserId == userid))
        //    {

        //        var productCategoryList = (from c in luminous.ProductLevelThrees
        //                                   where c.id == productTwo && c.PlTwStatus == 1
        //                                   select new
        //                                   {
        //                                       ProductLevelTwo = c.ProductLevelTwo.Name,
        //                                       ProductLevelOne = c.ProductLevelOne1.Name,
        //                                       ProductLevelThree = c.ProductLevelThreef.Name,
        //                                       ProdcutName = c.Name,
        //                                       ProductCode = c.PrCode,
        //                                       ProductDiscriptions = c.PrDiscription,
        //                                       Id = c.id
        //                                   }).ToList();

        //        if (productCategoryList.Count < 1)
        //        {
        //            FullProductDetail pr = new FullProductDetail();
        //            pr.id = 1;
        //            pr.LevelTwoName = "Product Details has not been added in database yet";

        //            return pr;
        //        }
        //        foreach (var i in productCategoryList)
        //        {
        //            FullProductDetail pr = new FullProductDetail();
        //            pr.id = i.Id;
        //            pr.LevelOneName = i.ProductLevelOne;
        //            pr.LevelThreeName = i.ProductLevelThree;
        //            pr.LevelTwoName = i.ProductLevelTwo;
        //            pr.ProductLevelFourName = i.ProdcutName;
        //            pr.ProductCode = i.ProductCode;
        //            pr.ProductDescriptions = i.ProductDiscriptions;
        //            List<ProductImges> productImges = new List<ProductImges>();
        //            List<string> images = (from c in luminous.ProductImages
        //                                   where c.pc_Lv3_oneId == i.Id
        //                                   select c.PrImage).ToList();

        //            foreach (string image in images)
        //            {
        //                if (File.Exists(Server.MapPath("~/ProductImages/") + image))
        //                {
        //                    ProductImges pi = new ProductImges();
        //                    pi.FullFileName = image;
        //                    pi.Images = "http://50.62.56.149:5052/ProductImages/" + image;
        //                    productImges.Add(pi);
        //                }
        //            }
        //            pr.Image = productImges;
        //            return pr;

        //        }
        //    }
        //    else
        //    {
        //        FullProductDetail pr = new FullProductDetail();
        //        pr.id = 1;
        //        pr.LevelTwoName = "Invalid User Id";

        //        return pr;
        //    }

        //    return new FullProductDetail();


        //}


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<Products> GetProductLevelThreeVersion2(string userid, int productlevelTwo, int ProductLevelOne, int productid, string Parentcategoryname, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<Products> product = new List<Products>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetProductLevelThreeVersion2";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (userid == "9900000004")
                {
                    token = LoginToken;
                }
                if (LoginToken == token)
                {

                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        var getparentcatid = luminous.ParentCategories.Where(c => c.PCName == Parentcategoryname).Select(c => c.Pcid).SingleOrDefault();
                        var productCategoryList = (from c in luminous.ProductLevelThrees
                                                   join Pi in luminous.ProductImages on c.id equals Pi.pc_Lv3_oneId
                                                   where c.productCategoryid == productid && c.PlTwStatus == 1 && c.ProductLevelOne == ProductLevelOne
                                                   && c.pc_Lv2_oneId == productlevelTwo && c.ParentCatid == getparentcatid
                                                   select new
                                                   {
                                                       productName = c.Name,
                                                       Id = c.id,
                                                       ProductImage = Pi.PrImage,
                                                       Sequence = c.OrderSequence
                                                   }).OrderBy(c => c.Sequence).ToList();


                        foreach (var i in productCategoryList)
                        {
                            Products pr = new Products();
                            pr.Id = i.Id;
                            pr.Name = i.productName;
                            pr.ImageName = Url + "ProductImages/" + i.ProductImage;
                            pr.Message = "Success";
                            product.Add(pr);
                            string RequestParameter = "UserID :" + userid + ",ProductLevelOne :" + ProductLevelOne + ",ProductLevelTwo :" + productlevelTwo + ",ProductId :" + productid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Id :1,Name :Success,ImageName :0,Message :Success";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                        if (productCategoryList.Count < 1)
                        {
                            Products pr = new Products();
                            pr.Id = 1;
                            pr.Name = "Product has not been added in database yet";
                            pr.ImageName = "0";

                            pr.Message = "Product has not been added";

                            product.Add(pr);
                            string RequestParameter = "UserID :" + userid + ",ProductLevelOne :" + ProductLevelOne + ",ProductLevelTwo :" + productlevelTwo + ",ProductId :" + productid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Id :1,Name :Product has not been added in database yet,ImageName :0,Message :Product has not been added";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Product has not been added", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                    }
                    else
                    {
                        Products pr = new Products();
                        pr.Id = 0;
                        pr.Name = "0";
                        pr.ImageName = "0";
                        pr.Status = "1";
                        pr.Message = "User already logged in three devices";

                        product.Add(pr);
                        string RequestParameter = "UserID :" + userid + ",ProductLevelOne :" + ProductLevelOne + ",ProductLevelTwo :" + productlevelTwo + ",ProductId :" + productid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id :-1,Name :0,ImageName :0,Message :User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    Products pr = new Products();
                    pr.Id = -1;
                    pr.Name = "0";
                    pr.ImageName = "0";
                    pr.Message = "Unauthorized Access";
                    product.Add(pr);
                    string RequestParameter = "UserID :" + userid + ",ProductLevelOne :" + ProductLevelOne + ",ProductLevelTwo :" + productlevelTwo + ",ProductId :" + productid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Id :-1,Name :0,ImageName :0,Message :Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }
            }
            catch (Exception exc)
            {
                Products pr = new Products();
                pr.Id = -1;
                pr.Name = "0";
                pr.ImageName = "0";
                pr.Message = "Some exception has occurred";
                product.Add(pr);
                string RequestParameter = "UserID :" + userid + ",ProductLevelOne :" + ProductLevelOne + ",ProductLevelTwo :" + productlevelTwo + ",ProductId :" + productid + ",ParentCategoryName :" + Parentcategoryname + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Id :-1,Name :0,ImageName :0,Message :Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return product;
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public UsersProfile GetProfile(string Email, string userid, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetProfile";
            //string url = HttpContext.Current.Request.Url.ToString();
            UsersList user = new UsersList();
            UsersProfile userprofile = new UsersProfile();
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        user = luminous.UsersLists.Where(a => a.isActive == 1).Single(a => a.UserId == Email);

                        userprofile.id = user.id;
                        userprofile.Email = user.Dis_Email;
                        userprofile.Address1 = user.Dis_Address1;
                        userprofile.Address2 = user.Dis_Address2;
                        userprofile.createdTime = user.CreatedON;
                        userprofile.UpdatedTime = user.UpdatedOn;
                        userprofile.CreatedBy = user.CreatedBY;
                        userprofile.UpdatedBy = user.UpdatedbY;
                        userprofile.Name = user.Dis_Name;
                        userprofile.CustomerType = user.CustomerType;
                        userprofile.RegionCode = user.Region_code;
                        userprofile.District = user.Dis_District;
                        userprofile.City = user.Dis_City;
                        userprofile.state = user.Dis_State;
                        userprofile.Dis_Sap_Code = user.Dis_Sap_Code;
                        userprofile.Dealerid = user.DealerId;
                        userprofile.ContactNo = user.Dis_ContactNo;
                        if (user.DistributorCode.ToString() == null || user.DistributorCode.ToString() == "")
                        {
                            userprofile.DistributorCode = "";
                        }
                        else
                        {
                            userprofile.DistributorCode = user.DistributorCode;
                        }
                        if (user.FSECode.ToString() == null || user.FSECode.ToString() == "")
                        {
                            userprofile.FseCode = "";
                        }
                        else
                        {
                            userprofile.FseCode = user.FSECode;
                        }

                        // userprofile.di = user.Dis_ContactNo;
                        if (File.Exists(Server.MapPath("~/ProfileImages/" + user.ProfileImage)))
                        {
                            userprofile.UserImage = Url + "ProfileImages/" + user.ProfileImage;
                            userprofile.id = Convert.ToInt64(userid);
                            userprofile.Message = "Success";
                            string RequestParameter = "Email :" + Email + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Id :" + userprofile.id + ",Message :Success";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                        else
                        {
                            userprofile.UserImage = "No Image";
                        }
                    }
                    else
                    {
                        userprofile.Status = "1";
                        userprofile.Message = "User already logged in three devices";
                        string RequestParameter = "Email :" + Email + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id :" + userprofile.id + ",Message :User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    userprofile.Message = "Unauthorized Access";
                    string RequestParameter = "Email :" + Email + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Id :" + userprofile.id + ",Message :Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                }
            }
            catch (Exception exc)
            {
                userprofile.id = -1;
                userprofile.Message = "Some exception has occurred";
                string RequestParameter = "Email :" + Email + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Id :-1,Message :Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);


            }
            return userprofile;


        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<ProductLevelDetails> GetAllProducts(string Email)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<GetProduct_Result> productDetails = luminous.GetProduct(Email).ToList();
            List<ProductLevelDetails> prdList = new List<ProductLevelDetails>();

            try
            {
                if (productDetails.Count() > 0)
                {
                    foreach (var i in productDetails)
                    {
                        ProductLevelDetails pld = new ProductLevelDetails();
                        pld.productCategoryId = i.ProductCategoryId;
                        pld.ProductCategoryName = i.ProductCategoryName;
                        pld.ProductLevelFourId = i.ProductLevelFourId;
                        pld.ProductLevelFourName = i.ProductLevelFourName;
                        pld.productLevelOneId = i.ProductLevelOneId;
                        pld.ProductLevelOneName = i.prouductLevelOneName;
                        pld.ProductLevelThreeId = i.productLevelThreefId;
                        pld.PrdouctLevelThreeName = i.ProductLevelThreeName;
                        pld.ProductLevelFourId = i.ProductLevelFourId;
                        pld.ProductLevelFourName = i.ProductLevelFourName;
                        pld.ProductLevelTwoID = i.productLevelTwoId;
                        pld.ProductLevelTWoName = i.productLevelTwoName;
                        prdList.Add(pld);
                    }
                }
                else
                {
                    ProductLevelDetails pld = new ProductLevelDetails();
                    pld.productCategoryId = -1;
                    prdList.Add(pld);
                }
            }
            catch
            {
                ProductLevelDetails pld = new ProductLevelDetails();
                pld.productCategoryId = -1;
                prdList.Add(pld);
            }
            return prdList;

        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<ProductLevelDetailsVersion2> GetAllProductsVersion2(string Email, string userid, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<GetProductVersion2_Result> productDetails = luminous.GetProductVersion2(Email).ToList();
            List<ProductLevelDetailsVersion2> prdList = new List<ProductLevelDetailsVersion2>();
            string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        if (productDetails.Count() > 0)
                        {
                            foreach (var i in productDetails)
                            {
                                ProductLevelDetailsVersion2 pld = new ProductLevelDetailsVersion2();
                                pld.productCategoryId = i.ProductCategoryId;
                                pld.ProductCategoryName = i.ProductCategoryName;
                                pld.productLevelOneId = i.ProductLevelOneId;
                                pld.ProductLevelOneName = i.prouductLevelOneName;
                                pld.ProductLevelThreeId = i.ProductLevelThreeId;
                                pld.PrdouctLevelThreeName = i.ProductLevelThreeName;
                                pld.ProductLevelTwoID = i.productLevelTwoId;
                                pld.ProductLevelTWoName = i.productLevelTwoName;
                                prdList.Add(pld);
                            }
                        }
                        else
                        {
                            ProductLevelDetailsVersion2 pld = new ProductLevelDetailsVersion2();
                            pld.productCategoryId = -1;
                            prdList.Add(pld);
                        }
                    }
                    else
                    {
                        ProductLevelDetailsVersion2 pld = new ProductLevelDetailsVersion2();
                        pld.productCategoryId = 0;
                        pld.Status = "1";
                        pld.Message = "User already logged in three devices";
                        prdList.Add(pld);
                    }
                }
                else
                {
                    ProductLevelDetailsVersion2 pld = new ProductLevelDetailsVersion2();
                    pld.productCategoryId = -1;
                    pld.Message = "Unauthorized Access";
                    prdList.Add(pld);
                }
            }
            catch (Exception exc)
            {
                ProductLevelDetailsVersion2 pld = new ProductLevelDetailsVersion2();
                pld.productCategoryId = -1;
                pld.Message = "Some exception has occurred";
                prdList.Add(pld);
                string RequestParameter = "Email :" + Email + ",userid :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "productCategoryId :-1,Message :Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return prdList;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]

        public List<PermotionList> GetPromotions(string userid, int PermotionTypeID, int ProductCatergoryId, int ProductLevelOneId, string Month, string Year, string Appversion, string token, string deviceid, string Ostype, string Osversion, string status)
        {
            string mm = "", yy = "";
            LuminousEntities luminous = new LuminousEntities();
            List<PermotionList> plist = new List<PermotionList>();
            string url = "";
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            if (PermotionTypeID == 9)
            {
                url = HttpContext.Current.Request.Url.ToString() + "/GetPromotions/PriceList";
            }
            if (PermotionTypeID == 10)
            {
                url = HttpContext.Current.Request.Url.ToString() + "/GetPromotions/Scheme";
            }
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {

                        int M = 0, Y = 0;
                        if (Month != null || Month != "")
                        {
                            M = Convert.ToInt32(Month);
                            Y = Convert.ToInt32(Year);
                        }

                        if (M != 0 && Y != 0)
                        {
                            //var Glist = luminous.GetPermotionsByUserId(userid).Where(a => a.PermotionTypeId == PermotionTypeID && a.ProductCatId == ProductCatergoryId && a.ProductLvlOneId == ProductLevelOneId && a.status == 1 && a.StartDate.Value.Month <= M && a.StartDate.Value.Year == Y && a.Enddate.Value.Month >= M && a.StartDate.Value.Year == Y).ToList();

                            var data_endyear = luminous.PermotionsLists.Where(a => a.PermotionTypeId == PermotionTypeID && a.ProductCatId == ProductCatergoryId && a.ProductLvlOneId == ProductLevelOneId && a.status == 1).Max(c => c.Enddate.Value.Year);
                            var data_endmonth = luminous.PermotionsLists.Where(a => a.PermotionTypeId == PermotionTypeID && a.ProductCatId == ProductCatergoryId && a.ProductLvlOneId == ProductLevelOneId && a.status == 1).Max(c => c.Enddate.Value.Month);

                            var Glist = luminous.GetPermotionsByUserId(userid).Where(a => a.PermotionTypeId == PermotionTypeID && a.ProductCatId == ProductCatergoryId && a.ProductLvlOneId == ProductLevelOneId && a.status == 1 && a.StartDate.Value.Month == M && a.StartDate.Value.Year == Y && (a.Enddate.Value.Month <= data_endmonth) && (a.Enddate.Value.Year <= data_endyear)).ToList();

                            if (Glist.Count > 0)
                            {

                                foreach (var i in Glist)
                                {
                                    PermotionList Pl = new PermotionList();
                                    Pl.id = i.id;
                                    if (i.ImageName == null || i.ImageName == "")
                                    {
                                        Pl.ImagePath = "No Image";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~/PermotionsImage/" + i.ImageName)))
                                        {
                                            Pl.ImagePath = Url + "PermotionsImage/" + i.ImageName;
                                        }
                                        else
                                        {
                                            Pl.ImagePath = "No Image";
                                        }
                                    }
                                    if (i.PDFName == null || i.PDFName == "")
                                    {
                                        Pl.PdfPath = "No PDF Image";
                                    }
                                    else
                                    {
                                        if (File.Exists(Server.MapPath("~/PermotionsImage/" + i.PDFName)))
                                        {
                                            Pl.PdfPath = Url + "PermotionsImage/" + i.PDFName;
                                        }
                                        else
                                        {
                                            Pl.PdfPath = "No Image";

                                        }
                                    }
                                    Pl.ProductCategory = luminous.ProductCatergories.Single(a => a.id == i.ProductCatId).PName;
                                    Pl.ProductlevelOne = luminous.ProductLevelOnes.Single(a => a.id == i.ProductLvlOneId).Name;
                                    Pl.Descriptons = i.Descriptions;
                                    Pl.startDate = Convert.ToDateTime(i.StartDate).ToShortDateString();
                                    Pl.enddate = Convert.ToDateTime(i.Enddate).ToShortDateString();
                                    Pl.PromotionType = luminous.PermotionTypes.Single(a => a.id == i.PermotionTypeId).PType;
                                    Pl.MonthValue = "0";
                                    Pl.Message = "Success";

                                    plist.Add(Pl);
                                }

                                string RequestParameter = "UserID :" + userid + ",Permotion Type ID :" + PermotionTypeID + ",ProductCatergoryId :" + ProductCatergoryId + ",Product Level One Id,DeviceID :" + ProductLevelOneId + ",Month :" + Month + ",Year :" + Year + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Id,ImagePath,PdfPath,ProductCategory,ProductlevelOne,Descriptons,startDate,enddate,PromotionType,MonthValue";

                                SaveServiceLog(userid, url, RequestParameter + "%" + status, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);


                                //Session["scheme"] = null;

                            }
                            else
                            {
                                PermotionList Pl = new PermotionList();
                                Pl.id = -1;
                                Pl.Message = "No Data Exists";
                                plist.Add(Pl);
                                string RequestParameter = "UserID :" + userid + ",Permotion Type ID :" + PermotionTypeID + ",ProductCatergoryId :" + ProductCatergoryId + ",Product Level One Id,DeviceID :" + ProductLevelOneId + ",Month :" + Month + ",Year :" + Year + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Id :" + Pl.id + ",Message :" + Pl.Message + "";

                                SaveServiceLog(userid, url, RequestParameter + "%" + status, ResponseParameter, 1, Pl.Message, userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                        }
                    }
                    else
                    {
                        PermotionList Pl = new PermotionList();
                        Pl.id = 0;
                        Pl.Status = "1";
                        Pl.Message = "User already logged in three devices";
                        plist.Add(Pl);
                        string RequestParameter = "UserID :" + userid + ",Permotion Type ID :" + PermotionTypeID + ",ProductCatergoryId :" + ProductCatergoryId + ",Product Level One Id,DeviceID :" + ProductLevelOneId + ",Month :" + Month + ",Year :" + Year + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id :" + Pl.id + ",Message :" + Pl.Message + "";

                        SaveServiceLog(userid, url, RequestParameter + "%" + status, ResponseParameter, 1, Pl.Message, userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    PermotionList Pl = new PermotionList();
                    Pl.id = -1;
                    Pl.Message = "Unauthorized Access";
                    plist.Add(Pl);
                    string RequestParameter = "UserID :" + userid + ",Permotion Type ID :" + PermotionTypeID + ",ProductCatergoryId :" + ProductCatergoryId + ",Product Level One Id,DeviceID :" + ProductLevelOneId + ",Month :" + Month + ",Year :" + Year + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Id :" + Pl.id + ",Message :" + Pl.Message + "";

                    SaveServiceLog(userid, url, RequestParameter + "%" + status, ResponseParameter, 1, Pl.Message, userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }
            }
            catch (Exception exc)
            {
                PermotionList Pl = new PermotionList();
                Pl.id = -3;
                Pl.Descriptons = exc.ToString();
                Pl.Message = "Some exception has occurred";
                plist.Add(Pl);
                string RequestParameter = "UserID :" + userid + ",Permotion Type ID :" + PermotionTypeID + ",ProductCatergoryId :" + ProductCatergoryId + ",Product Level One Id,DeviceID :" + ProductLevelOneId + ",Month :" + Month + ",Year :" + Year + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Id :" + Pl.id + ",Message :" + Pl.Message + "";

                SaveServiceLog(userid, url, RequestParameter + "%" + status, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return plist;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<ContactClass> GetContact(string userid, string Appversion, string token, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<ContactClass> contact = new List<ContactClass>();
            // string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetContact";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {

                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        var cont = luminous.contactUsDetails.Where(a => a.Cstatus == 1).ToList();
                        if (cont.Count() > 0)
                        {
                            foreach (var i in cont)
                            {
                                ContactClass contcl = new ContactClass();
                                contcl.Address = i.CAddress;
                                contcl.ContactUsType = i.Contact_Us_Type;
                                contcl.email = i.Email;
                                contcl.Fax = i.Fax;
                                contcl.PhoneNumber = i.PhoneNumber;
                                contact.Add(contcl);
                                string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Address :0,ContactUsType :0,email :0,Fax :0,PhoneNumber :0,Message :Success";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                            }
                        }
                    }
                    else
                    {
                        ContactClass contcl = new ContactClass();
                        contcl.Address = "0";
                        contcl.ContactUsType = "0";
                        contcl.email = "0";
                        contcl.Fax = "0";
                        contcl.PhoneNumber = "0";
                        contcl.Status = "1";
                        contcl.Message = "User already logged in three devices";
                        contact.Add(contcl);
                        string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Address :0,ContactUsType :0,email :0,Fax :0,PhoneNumber :0,Message :User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                    }
                }
                else
                {
                    ContactClass contcl = new ContactClass();
                    contcl.Address = "0";
                    contcl.ContactUsType = "0";
                    contcl.email = "0";
                    contcl.Fax = "0";
                    contcl.PhoneNumber = "0";
                    contcl.Message = "Unauthorize Access";
                    contact.Add(contcl);
                    string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Address :0,ContactUsType :0,email :0,Fax :0,PhoneNumber :0,Message :Unauthorize Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorize Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }
            }
            catch (Exception exc)
            {
                ContactClass contcl = new ContactClass();
                contcl.Address = "0";
                contcl.ContactUsType = "0";
                contcl.email = "0";
                contcl.Fax = "0";
                contcl.PhoneNumber = "0";
                contcl.Message = "Some exception has occurred";
                contact.Add(contcl);
                string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Address :0,ContactUsType :0,email :0,Fax :0,PhoneNumber :0,Message :Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

            }

            return contact;

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<Products> GetPermotionType(string userid, string Appversion, string token, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<Products> ptypeList = new List<Products>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetPermotionType";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        List<PermotionType> promtionTypes = luminous.PermotionTypes.Where(a => a.PlTwStatus == 1).ToList();
                        if (promtionTypes.Count > 0)
                        {
                            foreach (var i in promtionTypes)
                            {
                                Products ptype = new Products();
                                ptype.Id = i.id;
                                ptype.Name = i.PType;
                                ptypeList.Add(ptype);
                                string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Userid :" + userid + ",Message :Success";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                        }
                        else
                        {

                            Products ptype = new Products();
                            ptype.Id = -1;
                            ptypeList.Add(ptype);
                            string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Userid :" + userid + ",Message :Failure";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Failure", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                    }
                    else
                    {
                        Products ptype = new Products();
                        ptype.Id = 0;
                        ptype.Status = "1";
                        ptype.Message = "User already logged in three devices";
                        ptypeList.Add(ptype);
                        string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Userid :" + userid + ",Message :User already logged in three devices";
                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    Products ptype = new Products();
                    ptype.Id = -1;
                    ptype.Message = "Unauthorized Access";
                    ptypeList.Add(ptype);
                    string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Userid :" + userid + ",Message :Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                }
            }
            catch (Exception exc)
            {
                Products ptype = new Products();
                ptype.Id = -1;
                ptype.Message = "Some exception has occurred";
                ptypeList.Add(ptype);
                string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Id :-1,Message :Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return ptypeList;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<Products> GetFunctionName(string userid, string Appversion, string token, string deviceid, string Ostype, string Osversion)
        {
            List<Products> _FunctionList = new List<Products>();

            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetFunctionName";

            using (LuminousEntities luminous = new LuminousEntities())
            {
                try
                {
                    string LoginToken = "";
                    string TokenString = userid + Appversion;
                    using (MD5 md5Hash = MD5.Create())
                    {

                        LoginToken = GetMd5Hash(md5Hash, TokenString);


                    }
                    if (LoginToken == token)
                    {
                        var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                        if (checkstatus.Count() == 0)
                        {
                            List<FUNCTION> FunctionsList = luminous.FUNCTIONS.Where(a => a.Status == 1).ToList();
                            if (FunctionsList.Count > 0)
                            {
                                foreach (var i in FunctionsList)
                                {
                                    Products function = new Products();
                                    function.Id = i.ID;
                                    function.Name = i.fNAME;
                                    _FunctionList.Add(function);
                                    string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                    string ResponseParameter = "Id :-1,Name :0,Message :Some exception has occurred.";

                                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                                }
                            }
                            else
                            {

                                Products function = new Products();
                                function.Id = -1;
                                _FunctionList.Add(function);
                            }
                        }
                        else
                        {
                            Products function = new Products();
                            function.Id = -1;
                            function.Status = "1";
                            function.Message = "User already logged in three devices";
                            _FunctionList.Add(function);
                            string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Id :-1,Name :0,Message :User already logged in three devices.";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                    }
                    else
                    {
                        Products function = new Products();
                        function.Id = -1;

                        function.Message = "Unauthorized Access";
                        _FunctionList.Add(function);
                        string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id :-1,Name :0,Message :Unauthorized Access.";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                    }
                }
                catch (Exception ex)
                {
                    Products function = new Products();
                    function.Id = -1;
                    function.Name = "0";
                    function.Message = "Some exception has occurred.";
                    _FunctionList.Add(function);
                    string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Id :-1,Name :0,Message :Some exception has occurred.";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, ex.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }
            }
            return _FunctionList;

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public FullProductDetail2 GetFullProductDetail(string userid, int ProductLevelFourId)
        {
            LuminousEntities luminous = new LuminousEntities();
            int productTwo = ProductLevelFourId;

            List<GetProduct_Result> productDetails = luminous.GetProduct(userid).ToList();
            if (!productDetails.Any(a => a.ProductLevelFourId == ProductLevelFourId))
            {
                FullProductDetail2 pr = new FullProductDetail2();
                pr.id = -1;
                pr.LevelTwoName = "NO Access ";
                return pr;
            }
            if (luminous.UsersLists.Any(a => a.UserId == userid))
            {
                try
                {
                    var productCategoryList = (from c in luminous.ProductLevelThrees
                                               where c.id == productTwo && c.PlTwStatus == 1
                                               select new
                                               {
                                                   ProductLevelTwo = c.ProductLevelTwo.Name,
                                                   ProductLevelOne = c.ProductLevelOne.Name,
                                                   ProductLevelThree = c.ProductLevelThreef.Name,
                                                   ProdcutName = c.Name,
                                                   ProductCode = c.PrCode,
                                                   ProductDiscriptions = c.PrDiscription,
                                                   Warranty = c.Warrenty,
                                                   MRP = c.MRP,
                                                   KeyFeature = c.KeyFeature,
                                                   Id = c.id,
                                                   Browcher = c.brochure,
                                                   Rating = c.Rating
                                               }).ToList();


                    if (productCategoryList.Count < 1)
                    {
                        FullProductDetail2 pr = new FullProductDetail2();
                        pr.id = 1;
                        pr.LevelTwoName = "Product Details has not been added in database yet";

                        return pr;
                    }
                    foreach (var i in productCategoryList)
                    {
                        FullProductDetail2 pr = new FullProductDetail2();
                        pr.id = i.Id;
                        pr.LevelOneName = i.ProductLevelOne;
                        pr.LevelThreeName = i.ProductLevelThree;
                        pr.LevelTwoName = i.ProductLevelTwo;
                        pr.ProductLevelFourName = i.ProdcutName;
                        pr.ProductCode = i.ProductCode;
                        pr.TechnicleSpecification = i.ProductDiscriptions;
                        pr.Warranty = i.Warranty;
                        pr.MRP = i.MRP.ToString();
                        pr.KeyFeature = i.KeyFeature;
                        if (File.Exists(Server.MapPath("~/ProductImages/" + i.Browcher)))
                        {
                            pr.brochure = Url + "ProductImages/" + i.Browcher;
                        }
                        else
                        {
                            pr.brochure = "No Browcher";
                        }
                        List<ProductImges> productImges = new List<ProductImges>();
                        List<string> images = (from c in luminous.ProductImages
                                               where c.pc_Lv3_oneId == i.Id
                                               select c.PrImage).ToList();

                        foreach (string image in images)
                        {
                            if (File.Exists(Server.MapPath("~/ProductImages/") + image))
                            {
                                ProductImges pi = new ProductImges();
                                pi.FullFileName = image;
                                pi.Images = Url + "ProductImages/" + image;
                                productImges.Add(pi);
                            }
                        }
                        pr.Image = productImges;
                        return pr;

                    }
                }
                catch (Exception ex)
                {
                    Exception ep = ex.InnerException;
                }
            }
            else
            {
                FullProductDetail2 pr = new FullProductDetail2();
                pr.id = 1;
                pr.LevelTwoName = "Invalid User Id";

                return pr;
            }

            return new FullProductDetail2();


        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public FullProductDetail2Version2 GetFullProductDetailVersion2(string userid, int ProductLevelThreeId, string Appversion, string token, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            int productTwo = ProductLevelThreeId;
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetFullProductDetailVersion2";
            List<GetProductVersion2_Result> productDetails = luminous.GetProductVersion2(userid).ToList();
            string LoginToken = "";
            string TokenString = userid + Appversion;
            using (MD5 md5Hash = MD5.Create())
            {

                LoginToken = GetMd5Hash(md5Hash, TokenString);


            }
            if (LoginToken == token)
            {
                var getProductCategory = luminous.ProductLevelThrees.Where(a => a.id == ProductLevelThreeId).Select(a => a.productCategoryid).SingleOrDefault();
                var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                if (checkstatus.Count() == 0)
                {

                    if (!productDetails.Any(a => a.ProductLevelThreeId == ProductLevelThreeId))
                    {
                        FullProductDetail2Version2 pr = new FullProductDetail2Version2();
                        pr.id = -1;
                        pr.LevelTwoName = "NO Access ";
                        return pr;
                    }
                    if (luminous.UsersLists.Any(a => a.UserId == userid && a.isActive == 1))
                    {
                        try
                        {
                            var productCategoryList = (from c in luminous.ProductLevelThrees
                                                       where c.id == productTwo && c.PlTwStatus == 1
                                                       select new
                                                       {
                                                           ProductLevelTwo = c.ProductLevelTwo.Name,
                                                           ProductLevelOne = c.ProductLevelOne.Name,
                                                           ProdcutName = c.Name,
                                                           ProductCode = c.PrCode,
                                                           ProductDiscriptions = c.PrDiscription,
                                                           Warranty = c.Warrenty,
                                                           MRP = c.MRP,
                                                           KeyFeature = c.KeyFeature,
                                                           Id = c.id,
                                                           Browcher = c.brochure,
                                                           Rating = c.Rating,
                                                           MaximumChargeCurrent = c.MaximumChargeCurrent,
                                                           NoOfBatterry = c.NoOfBattery,
                                                           SupportedBatteryType = c.SupportedBatteryType,
                                                           MaximumBulbLoad = c.Maximumbulbload,
                                                           Technology = c.Technology,
                                                           NominalVoltage = c.NominalVoltage,
                                                           DimensionMM = c.DimensionMM,
                                                           WeightFilledBattery = c.Weight_Filled_battery
                                                       }).ToList();


                            if (productCategoryList.Count < 1)
                            {
                                FullProductDetail2Version2 pr = new FullProductDetail2Version2();
                                pr.id = 1;
                                pr.LevelTwoName = "Product Details has not been added in database yet";

                                return pr;
                            }
                            foreach (var i in productCategoryList)
                            {
                                FullProductDetail2Version2 pr = new FullProductDetail2Version2();
                                pr.id = i.Id;
                                pr.LevelOneName = i.ProductLevelOne;
                                pr.LevelTwoName = i.ProductLevelTwo;
                                pr.ProductLevelThreeName = i.ProdcutName;
                                pr.ProductCode = i.ProductCode;
                                pr.TechnicleSpecification = i.ProductDiscriptions;

                                if (pr.Warranty == "")
                                {
                                    pr.Warranty = "0";
                                }
                                else
                                {
                                    pr.Warranty = i.Warranty;
                                }

                                if (pr.MRP == "")
                                {
                                    pr.MRP = "0";
                                }
                                else
                                {
                                    pr.MRP = i.MRP.ToString();
                                }



                                pr.Rating = i.Rating;

                                if (pr.KeyFeature == "")
                                {
                                    pr.KeyFeature = "0";
                                }
                                else
                                {
                                    pr.KeyFeature = i.KeyFeature;
                                }

                                if (File.Exists(Server.MapPath("~/ProductImages/" + i.Browcher)))
                                {
                                    pr.brochure = Url + "ProductImages/" + i.Browcher;
                                }
                                else
                                {
                                    pr.brochure = "No Browcher";
                                }

                                //if(getProductCategory==15 || getProductCategory==9)
                                //{
                                //   pr.Maximumchargecurrent = i.MaximumChargeCurrent;
                                //   pr.NoOfBattery = i.NoOfBatterry;
                                //   pr.SupportedBatteryType = i.SupportedBatteryType;
                                //   pr.MaximumBulbLoad = i.MaximumBulbLoad;
                                //   pr.Technology = i.Technology;


                                //}
                                //if(getProductCategory==8)
                                //{
                                //    pr.NominalVoltage = i.NominalVoltage;
                                //    pr.DimensionMM = i.DimensionMM;
                                //    pr.WeightFilledBattery = i.WeightFilledBattery;

                                //}
                                List<TechnicalSpecification> techspe = new List<TechnicalSpecification>();
                                if (getProductCategory == 15 || getProductCategory == 9)
                                {
                                    var technicalspec = luminous.ProductLevelThrees.Where(c => c.id == ProductLevelThreeId).Select(c => new { c.MaximumChargeCurrent, c.NoOfBattery, c.SupportedBatteryType, c.Maximumbulbload, c.Technology }).ToList();

                                    foreach (var techsp in technicalspec)
                                    {
                                        TechnicalSpecification ts = new TechnicalSpecification();
                                        ts.ColumnName = "Maximum Charging current";

                                        if (techsp.MaximumChargeCurrent == null)
                                        {
                                            ts.Value = "0";
                                        }
                                        else
                                        {
                                            ts.Value = techsp.MaximumChargeCurrent;
                                        }

                                        techspe.Add(ts);
                                    }
                                    foreach (var techsp in technicalspec)
                                    {
                                        TechnicalSpecification ts = new TechnicalSpecification();
                                        ts.ColumnName = "Number of battery";

                                        if (techsp.NoOfBattery == "" || techsp.NoOfBattery == null)
                                        {
                                            ts.Value = "0";
                                        }
                                        else
                                        {
                                            ts.Value = techsp.NoOfBattery;
                                        }

                                        techspe.Add(ts);
                                    }
                                    foreach (var techsp in technicalspec)
                                    {
                                        TechnicalSpecification ts = new TechnicalSpecification();
                                        ts.ColumnName = "Supported battery type";

                                        if (techsp.SupportedBatteryType == "" || techsp.SupportedBatteryType == null)
                                        {
                                            ts.Value = "0";
                                        }
                                        else
                                        {
                                            ts.Value = techsp.SupportedBatteryType;
                                        }

                                        techspe.Add(ts);
                                    }
                                    foreach (var techsp in technicalspec)
                                    {
                                        TechnicalSpecification ts = new TechnicalSpecification();
                                        ts.ColumnName = "Maximum bulb load";

                                        if (techsp.Maximumbulbload == "" || techsp.Maximumbulbload == null)
                                        {
                                            ts.Value = "0";
                                        }
                                        else
                                        {
                                            ts.Value = techsp.Maximumbulbload;
                                        }

                                        techspe.Add(ts);
                                    }
                                    foreach (var techsp in technicalspec)
                                    {
                                        TechnicalSpecification ts = new TechnicalSpecification();
                                        ts.ColumnName = "Technology";
                                        if (techsp.Technology == "" || techsp.Technology == null)
                                        {
                                            ts.Value = "0";
                                        }
                                        else
                                        {
                                            ts.Value = techsp.Technology;
                                        }

                                        techspe.Add(ts);
                                    }

                                }
                                if (getProductCategory == 8)
                                {
                                    var technicalspec = luminous.ProductLevelThrees.Where(c => c.id == ProductLevelThreeId).Select(c => new { c.NominalVoltage, c.DimensionMM, c.Weight_Filled_battery }).ToList();
                                    //List<string> technicalspec = (from c in luminous.ProductLevelThrees
                                    //                              where c.id == ProductLevelThreeId
                                    //                              select

                                    //                                  c.NominalVoltage


                                    //                            ).ToList();

                                    foreach (var techsp in technicalspec)
                                    {
                                        TechnicalSpecification ts = new TechnicalSpecification();
                                        ts.ColumnName = "Nominal Voltage";

                                        if (techsp.NominalVoltage == "" || techsp.NominalVoltage == null)
                                        {
                                            ts.Value = "0";
                                        }
                                        else
                                        {
                                            ts.Value = techsp.NominalVoltage;
                                        }

                                        techspe.Add(ts);
                                    }
                                    foreach (var techsp in technicalspec)
                                    {
                                        TechnicalSpecification ts = new TechnicalSpecification();
                                        ts.ColumnName = "Dimension(in MM)";

                                        if (techsp.DimensionMM == "" || techsp.DimensionMM == null)
                                        {
                                            ts.Value = "0";
                                        }
                                        else
                                        {
                                            ts.Value = techsp.DimensionMM;
                                        }

                                        techspe.Add(ts);
                                    }
                                    foreach (var techsp in technicalspec)
                                    {
                                        TechnicalSpecification ts = new TechnicalSpecification();
                                        ts.ColumnName = "Weight (Filled battery)";

                                        if (techsp.Weight_Filled_battery == "" || techsp.Weight_Filled_battery == null)
                                        {
                                            ts.Value = "0";
                                        }
                                        else
                                        {
                                            ts.Value = techsp.Weight_Filled_battery;
                                        }

                                        techspe.Add(ts);
                                    }
                                }

                                pr.tech = techspe;
                                List<ProductImges> productImges = new List<ProductImges>();
                                List<string> images = (from c in luminous.ProductImages
                                                       where c.pc_Lv3_oneId == i.Id
                                                       select c.PrImage).ToList();
                                string prdLevelthreeimage = "";

                                foreach (string image in images)
                                {
                                    if (File.Exists(Server.MapPath("~/ProductImages/") + image))
                                    {
                                        if (image.Contains(" "))
                                        {
                                            prdLevelthreeimage = image.Replace(" ", "%20");
                                        }
                                        ProductImges pi = new ProductImges();
                                        pi.FullFileName = image;
                                        pi.Images = Url + "ProductImages/" + prdLevelthreeimage;
                                        productImges.Add(pi);
                                    }
                                }
                                pr.Image = productImges;

                                List<Product3Image> prod3image = new List<Product3Image>();
                                List<string> pr3images = (from c in luminous.ProductthreeImageMappings
                                                          where c.ProductLevelThreeid == i.Id
                                                          select c.Primage).ToList();
                                foreach (string image3 in pr3images)
                                {
                                    if (File.Exists(Server.MapPath("~/ProductImages/") + image3))
                                    {
                                        Product3Image pi = new Product3Image();

                                        pi.Pr3image = Url + "ProductImages/" + image3;
                                        prod3image.Add(pi);
                                    }
                                }
                                pr.pr3image = prod3image;
                                string RequestParameter = "UserID :" + userid + ",ProductLevelThreeID :" + ProductLevelThreeId + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Id :" + pr.id + ",LevelOneName :" + pr.LevelOneName + ",LevelTwoName :" + pr.LevelTwoName + ",ProductLevelThreeName :" + pr.ProductLevelThreeName + ",ProductCode :" + pr.ProductCode + ",TechnicalSpecification :" + pr.TechnicleSpecification + ",Warrenty :" + pr.Warranty + ",MRP :" + pr.MRP + ",KeyFeature :" + pr.KeyFeature + ",Rating :" + pr.Rating + ",Brochure :" + pr.brochure + ",Image :" + pr.Image + "";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                                return pr;

                            }
                        }
                        catch (Exception ex)
                        {
                            FullProductDetail2Version2 pr = new FullProductDetail2Version2();

                            pr.Error = "Some exception occurred";

                            string RequestParameter = "UserID :" + userid + ",ProductLevelThreeID :" + ProductLevelThreeId + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Error :" + pr.Error + "";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, ex.Message, userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            return pr;
                        }
                    }
                    else
                    {
                        FullProductDetail2Version2 pr = new FullProductDetail2Version2();
                        pr.id = 1;
                        pr.LevelTwoName = "Invalid User Id";

                        return pr;
                    }
                }
                else
                {
                    FullProductDetail2Version2 pr = new FullProductDetail2Version2();
                    pr.id = 0;
                    pr.Status = "1";
                    pr.Error = "User already logged in three devices";
                    string RequestParameter = "UserID :" + userid + ",ProductLevelThreeID :" + ProductLevelThreeId + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Error :" + pr.Error + "";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, pr.Error, userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    return pr;

                }
            }
            else
            {
                FullProductDetail2Version2 pr = new FullProductDetail2Version2();
                pr.id = -1;

                pr.Error = "Unauthorized User";
                string RequestParameter = "UserID :" + userid + ",ProductLevelThreeID :" + ProductLevelThreeId + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Error :" + pr.Error + "";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, pr.Error, userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                return pr;
            }

            return new FullProductDetail2Version2();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public FullProductDetail2 GetProductLevelFourByName(string userId, string ProductLevelName)
        {
            LuminousEntities luminous = new LuminousEntities();
            #region Check IF User Not Exist
            if (!luminous.UsersLists.Any(a => a.UserId.Trim() == userId.Trim() && a.isActive == 1))
            {
                FullProductDetail2 FullProductDetail = new FullProductDetail2();
                FullProductDetail.id = -1;
                FullProductDetail.Error = "Incorrect Input";
                return FullProductDetail;
            }
            #endregion
            else
            {

                #region Check If ProductLevelThree not Exist
                if (!luminous.ProductLevelThrees.Any(a => a.Name.Trim() == ProductLevelName.Trim()))
                {
                    FullProductDetail2 FullProductDetail = new FullProductDetail2();
                    FullProductDetail.id = -1;
                    FullProductDetail.Error = "Product Does Not Exist";
                    return FullProductDetail;
                }
                #endregion
                else
                {
                    //Get ProductId By Name
                    int id = luminous.ProductLevelThrees.Where(a => a.Name.Trim() == ProductLevelName.Trim()).FirstOrDefault().id;
                    //Check Product Access
                    List<GetProduct_Result> productDetails = luminous.GetProduct(userId.Trim()).ToList();
                    if (!productDetails.Any(a => a.ProductLevelFourId == id))
                    {
                        FullProductDetail2 pr = new FullProductDetail2();
                        pr.id = -1;
                        pr.Error = "No Access ";
                        return pr;
                    }
                    else
                    {
                        try
                        {


                            var productCategoryList = (from c in luminous.ProductLevelThrees
                                                       where c.Name.Trim() == ProductLevelName.Trim() && c.PlTwStatus == 1
                                                       select new
                                                       {
                                                           ProductLevelTwo = c.ProductLevelTwo.Name,
                                                           ProductLevelOne = c.ProductLevelOne1.Name,
                                                           ProductLevelThree = c.ProductLevelThreef.Name,
                                                           ProdcutName = c.Name,
                                                           ProductCode = c.PrCode,
                                                           ProductDiscriptions = c.PrDiscription,
                                                           Warranty = c.Warrenty,
                                                           MRP = c.MRP,
                                                           KeyFeature = c.KeyFeature,
                                                           Id = c.id,
                                                           Browcher = c.brochure
                                                       }).ToList();


                            if (productCategoryList.Count < 1)
                            {
                                FullProductDetail2 pr = new FullProductDetail2();
                                pr.id = 1;
                                pr.Error = "Product Details has not been added in database yet";
                                return pr;
                            }
                            foreach (var i in productCategoryList)
                            {
                                FullProductDetail2 pr = new FullProductDetail2();
                                pr.id = i.Id;
                                pr.LevelOneName = i.ProductLevelOne;
                                pr.LevelThreeName = i.ProductLevelThree;
                                pr.LevelTwoName = i.ProductLevelTwo;
                                pr.ProductLevelFourName = i.ProdcutName;
                                pr.ProductCode = i.ProductCode;
                                pr.TechnicleSpecification = i.ProductDiscriptions;
                                pr.Warranty = i.Warranty;
                                pr.MRP = i.MRP.ToString();
                                pr.KeyFeature = i.KeyFeature;
                                if (File.Exists(Server.MapPath("~/ProductImages/" + i.Browcher)))
                                {
                                    pr.brochure = Url + "ProductImages/" + i.Browcher;
                                }
                                else
                                {
                                    pr.brochure = "No Browcher";
                                }
                                List<ProductImges> productImges = new List<ProductImges>();
                                List<string> images = (from c in luminous.ProductImages
                                                       where c.pc_Lv3_oneId == i.Id
                                                       select c.PrImage).ToList();

                                foreach (string image in images)
                                {
                                    if (File.Exists(Server.MapPath("~/ProductImages/") + image))
                                    {
                                        ProductImges pi = new ProductImges();
                                        pi.FullFileName = image;
                                        pi.Images = Url + "ProductImages/" + image;
                                        productImges.Add(pi);
                                    }
                                }
                                pr.Image = productImges;
                                return pr;

                            }
                        }
                        catch (Exception ex)
                        {
                            Exception ep = ex.InnerException;
                            FullProductDetail2 FullProductDetail = new FullProductDetail2();
                            FullProductDetail.id = -1;
                            FullProductDetail.Error = ex.Message;
                            return FullProductDetail;

                        }
                        return new FullProductDetail2();
                    }

                }


            }

        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public FullProductDetail2Version2 GetProductLevelFourByNameVersion2(string userId, string ProductLevelName, string Appversion, string token, string deviceid, string Ostype, string Osversion, string status)
        {
            LuminousEntities luminous = new LuminousEntities();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetProductLevelFourByNameVersion2";
            #region Check IF User Not Exist
            if (!luminous.UsersLists.Any(a => a.UserId.Trim() == userId.Trim() && a.isActive == 1))
            {
                FullProductDetail2Version2 FullProductDetail = new FullProductDetail2Version2();
                FullProductDetail.id = -1;
                FullProductDetail.Error = "Incorrect Input";
                return FullProductDetail;
            }
            #endregion
            else
            {

                #region Check If ProductLevelThree not Exist
                if (!luminous.ProductLevelThrees.Any(a => a.Name.Trim() == ProductLevelName.Trim()))
                {
                    FullProductDetail2Version2 FullProductDetail = new FullProductDetail2Version2();
                    FullProductDetail.id = -1;
                    FullProductDetail.Error = "Product Does Not Exists";
                    string RequestParameter = "UserID :" + userId + ",ProductLevelName :" + ProductLevelName + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Message :Product Does Not Exists ";

                    SaveServiceLog(userId, url, RequestParameter + "%" + status, ResponseParameter, 0, "Product Does Not Exists", userId, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    return FullProductDetail;
                }
                #endregion
                else
                {
                    //Get ProductId By Name
                    int id = luminous.ProductLevelThrees.Where(a => a.Name.Trim() == ProductLevelName.Trim()).FirstOrDefault().id;
                    //Check Product Access
                    List<GetProductVersion2_Result> productDetails = luminous.GetProductVersion2(userId.Trim()).ToList();
                    if (!productDetails.Any(a => a.ProductLevelThreeId == id))
                    {
                        FullProductDetail2Version2 pr = new FullProductDetail2Version2();
                        pr.id = -1;
                        pr.Error = "No Access ";
                        return pr;
                    }
                    else
                    {
                        string Key = "";
                        string Warrenty = "";
                        try
                        {
                            string LoginToken = "";
                            string TokenString = userId + Appversion;
                            using (MD5 md5Hash = MD5.Create())
                            {

                                LoginToken = GetMd5Hash(md5Hash, TokenString);


                            }
                            if (LoginToken == token)
                            {

                                var getProductCategory = luminous.ProductLevelThrees.Where(a => a.Name == ProductLevelName).Select(a => a.productCategoryid).SingleOrDefault();
                                var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userId);
                                if (checkstatus.Count() == 0)
                                {

                                    var checknull = luminous.ProductLevelThrees.Where(c => c.Name == ProductLevelName).Select(i => new { i.Warrenty, i.KeyFeature });
                                    foreach (var data in checknull)
                                    {

                                        if (data.KeyFeature == null || data.KeyFeature == "")
                                        {
                                            Key = "";
                                        }
                                        else
                                        {
                                            Key = data.KeyFeature;
                                        }
                                        if (data.Warrenty == null || data.Warrenty == "")
                                        {
                                            Warrenty = "";
                                        }
                                        else
                                        {
                                            Warrenty = data.Warrenty;
                                        }
                                    }
                                    var productCategoryList = (from c in luminous.ProductLevelThrees
                                                               where c.Name.Trim() == ProductLevelName.Trim() && c.PlTwStatus == 1
                                                               select new
                                                               {
                                                                   ProductLevelTwo = c.ProductLevelTwo.Name,
                                                                   ProductLevelOne = c.ProductLevelOne.Name,
                                                                   ProdcutName = c.Name,
                                                                   ProductCode = c.productCategoryid,
                                                                   ProductDiscriptions = c.PrDiscription,
                                                                   Warranty = Warrenty,
                                                                   MRP = c.MRP,
                                                                   KeyFeature = Key,
                                                                   Id = c.id,
                                                                   Browcher = c.brochure,
                                                                   Rating = c.Rating
                                                               }).ToList();


                                    if (productCategoryList.Count < 1)
                                    {
                                        FullProductDetail2Version2 pr = new FullProductDetail2Version2();
                                        pr.id = 1;
                                        pr.Error = "Product Details has not been added in database yet";
                                        string RequestParameter = "UserID :" + userId + ",ProductLevelName :" + ProductLevelName + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                        string ResponseParameter = "Message :Product Details has not been added in database yet ";

                                        SaveServiceLog(userId, url, RequestParameter + "%" + status, ResponseParameter, 1, "Product Details has not been added in database yet", userId, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                                        return pr;
                                    }
                                    foreach (var i in productCategoryList)
                                    {
                                        FullProductDetail2Version2 pr = new FullProductDetail2Version2();
                                        pr.id = i.Id;
                                        pr.LevelOneName = i.ProductLevelOne;
                                        pr.LevelTwoName = i.ProductLevelTwo;
                                        pr.ProductLevelThreeName = i.ProdcutName;
                                        pr.ProductCode = i.ProductCode.ToString();
                                        pr.TechnicleSpecification = i.ProductDiscriptions;
                                        pr.Warranty = Warrenty;
                                        pr.MRP = i.MRP.ToString();
                                        pr.KeyFeature = Key;
                                        pr.Rating = i.Rating;
                                        if (File.Exists(Server.MapPath("~/ProductImages/" + i.Browcher)))
                                        {
                                            pr.brochure = Url + "ProductImages/" + i.Browcher;
                                        }
                                        else
                                        {
                                            pr.brochure = "No Borwcher";
                                        }


                                        List<TechnicalSpecification> techspe = new List<TechnicalSpecification>();
                                        if (getProductCategory == 15 || getProductCategory == 9)
                                        {
                                            var technicalspec = luminous.ProductLevelThrees.Where(c => c.Name == ProductLevelName).Select(c => new { c.MaximumChargeCurrent, c.NoOfBattery, c.SupportedBatteryType, c.Maximumbulbload, c.Technology }).ToList();

                                            foreach (var techsp in technicalspec)
                                            {
                                                TechnicalSpecification ts = new TechnicalSpecification();
                                                ts.ColumnName = "Maximum Charging current";

                                                if (techsp.MaximumChargeCurrent == "" || techsp.MaximumChargeCurrent == null)
                                                {
                                                    ts.Value = "0";
                                                }
                                                else
                                                {
                                                    ts.Value = techsp.MaximumChargeCurrent;
                                                }

                                                techspe.Add(ts);
                                            }
                                            foreach (var techsp in technicalspec)
                                            {
                                                TechnicalSpecification ts = new TechnicalSpecification();
                                                ts.ColumnName = "Number of battery";

                                                if (techsp.NoOfBattery == "" || techsp.NoOfBattery == null)
                                                {
                                                    ts.Value = "0";
                                                }
                                                else
                                                {
                                                    ts.Value = techsp.NoOfBattery;
                                                }

                                                techspe.Add(ts);
                                            }
                                            foreach (var techsp in technicalspec)
                                            {
                                                TechnicalSpecification ts = new TechnicalSpecification();
                                                ts.ColumnName = "Supported battery type";

                                                if (techsp.SupportedBatteryType == "" || techsp.SupportedBatteryType == null)
                                                {
                                                    ts.Value = "0";
                                                }
                                                else
                                                {
                                                    ts.Value = techsp.SupportedBatteryType;
                                                }

                                                techspe.Add(ts);
                                            }
                                            foreach (var techsp in technicalspec)
                                            {
                                                TechnicalSpecification ts = new TechnicalSpecification();
                                                ts.ColumnName = "Maximum bulb load";

                                                if (techsp.Maximumbulbload == "" || techsp.Maximumbulbload == null)
                                                {
                                                    ts.Value = "0";
                                                }
                                                else
                                                {
                                                    ts.Value = techsp.Maximumbulbload;
                                                }

                                                techspe.Add(ts);
                                            }
                                            foreach (var techsp in technicalspec)
                                            {
                                                TechnicalSpecification ts = new TechnicalSpecification();
                                                ts.ColumnName = "Technology";
                                                if (techsp.Technology == "" || techsp.Technology == null)
                                                {
                                                    ts.Value = "0";
                                                }
                                                else
                                                {
                                                    ts.Value = techsp.Technology;
                                                }

                                                techspe.Add(ts);
                                            }
                                        }
                                        if (getProductCategory == 8)
                                        {
                                            var technicalspec = luminous.ProductLevelThrees.Where(c => c.Name == ProductLevelName).Select(c => new { c.NominalVoltage, c.DimensionMM, c.Weight_Filled_battery }).ToList();
                                            //List<string> technicalspec = (from c in luminous.ProductLevelThrees
                                            //                              where c.id == ProductLevelThreeId
                                            //                              select

                                            //                                  c.NominalVoltage


                                            //                            ).ToList();

                                            foreach (var techsp in technicalspec)
                                            {
                                                TechnicalSpecification ts = new TechnicalSpecification();
                                                ts.ColumnName = "Nominal Voltage";

                                                if (techsp.NominalVoltage == "" || techsp.NominalVoltage == null)
                                                {
                                                    ts.Value = "0";
                                                }
                                                else
                                                {
                                                    ts.Value = techsp.NominalVoltage;
                                                }

                                                techspe.Add(ts);
                                            }
                                            foreach (var techsp in technicalspec)
                                            {
                                                TechnicalSpecification ts = new TechnicalSpecification();
                                                ts.ColumnName = "Dimension(in MM)";

                                                if (techsp.DimensionMM == "" || techsp.DimensionMM == null)
                                                {
                                                    ts.Value = "0";
                                                }
                                                else
                                                {
                                                    ts.Value = techsp.DimensionMM;
                                                }

                                                techspe.Add(ts);
                                            }
                                            foreach (var techsp in technicalspec)
                                            {
                                                TechnicalSpecification ts = new TechnicalSpecification();
                                                ts.ColumnName = "Weight (Filled battery)";

                                                if (techsp.Weight_Filled_battery == "" || techsp.Weight_Filled_battery == null)
                                                {
                                                    ts.Value = "0";
                                                }
                                                else
                                                {
                                                    ts.Value = techsp.Weight_Filled_battery;
                                                }

                                                techspe.Add(ts);
                                            }
                                        }
                                        pr.tech = techspe;


                                        List<ProductImges> productImges = new List<ProductImges>();
                                        List<string> images = (from c in luminous.ProductImages
                                                               where c.pc_Lv3_oneId == i.Id
                                                               select c.PrImage).ToList();

                                        foreach (string image in images)
                                        {
                                            if (File.Exists(Server.MapPath("~/ProductImages/") + image))
                                            {

                                                ProductImges pi = new ProductImges();
                                                pi.FullFileName = image;
                                                pi.Images = Url + "ProductImages/" + image;
                                                productImges.Add(pi);
                                            }
                                        }
                                        pr.Image = productImges;


                                        List<Product3Image> prod3image = new List<Product3Image>();
                                        List<string> pr3images = (from c in luminous.ProductthreeImageMappings
                                                                  where c.ProductLevelThreeid == i.Id
                                                                  select c.Primage).ToList();
                                        foreach (string image3 in pr3images)
                                        {
                                            if (File.Exists(Server.MapPath("~/ProductImages/") + image3))
                                            {
                                                Product3Image pi = new Product3Image();

                                                pi.Pr3image = Url + "ProductImages/" + image3;
                                                prod3image.Add(pi);
                                            }
                                        }
                                        pr.pr3image = prod3image;

                                        string RequestParameter = "UserID :" + userId + ",ProductLevelName :" + ProductLevelName + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                        string ResponseParameter = "Message :Success";

                                        SaveServiceLog(userId, url, RequestParameter + "%" + status, ResponseParameter, 0, "Success", userId, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                                        return pr;


                                    }
                                }
                                else
                                {
                                    FullProductDetail2Version2 pr = new FullProductDetail2Version2();
                                    pr.id = -1;
                                    pr.Status = "1";
                                    pr.Error = "User already logged in three devices";
                                    string RequestParameter = "UserID :" + userId + ",ProductLevelName :" + ProductLevelName + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                    string ResponseParameter = "User already logged in three devices ";

                                    SaveServiceLog(userId, url, RequestParameter + "%" + status, ResponseParameter, 1, "User already logged in three devices", userId, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                                    return pr;
                                }
                            }
                            else
                            {
                                FullProductDetail2Version2 pr = new FullProductDetail2Version2();
                                pr.id = -1;
                                pr.Error = "Unauthorized Access";

                                string RequestParameter = "UserID :" + userId + ",ProductLevelName :" + ProductLevelName + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Id :" + pr.id + ",Error :" + pr.Error + "";

                                SaveServiceLog(userId, url, RequestParameter + "%" + status, ResponseParameter, 1, pr.Error, userId, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                                return pr;
                            }
                        }
                        catch (Exception ex)
                        {
                            Exception ep = ex.InnerException;
                            FullProductDetail2Version2 FullProductDetail = new FullProductDetail2Version2();
                            FullProductDetail.id = -1;
                            FullProductDetail.Error = ex.Message + "Key=" + Key + Warrenty;
                            string RequestParameter = "UserID :" + userId + ",ProductLevelName :" + ProductLevelName + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Id :" + FullProductDetail.id + ",Error :" + FullProductDetail.Error + "";

                            SaveServiceLog(userId, url, RequestParameter + "%" + status, ResponseParameter, 1, FullProductDetail.Error, userId, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            return FullProductDetail;

                        }
                        return new FullProductDetail2Version2();
                    }

                }


            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<ProductlevelThree> SearchProductLevelThree(string userid, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {
            List<ProductlevelThree> pr3 = new List<ProductlevelThree>();
            LuminousEntities luminous = new LuminousEntities();
            string url = HttpContext.Current.Request.Url.ToString() + "/SearchProductLevelThree";
            // string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            try
            {



                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {

                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {

                        var getProductName = luminous.ProductLevelThrees.Select(c => c.Name).ToList();
                        foreach (var data in getProductName)
                        {
                            ProductlevelThree p3 = new ProductlevelThree();
                            p3.ProductName = data;
                            pr3.Add(p3);

                        }
                        string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "ProductName";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        return pr3;
                    }
                    else
                    {
                        ProductlevelThree p3 = new ProductlevelThree();
                        p3.Error = "User already logged in three devices";
                        p3.Status = "1";
                        pr3.Add(p3);
                        string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        return pr3;
                    }
                }
                else
                {
                    ProductlevelThree p3 = new ProductlevelThree();
                    p3.Error = "No Data Found";
                    pr3.Add(p3);
                    string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Error :No Data Found";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Found", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                    return pr3;
                }
            }
            catch (Exception exc)
            {
                List<ProductlevelThree> prexception = new List<ProductlevelThree>();
                ProductlevelThree p3 = new ProductlevelThree();
                p3.Error = "Some exception has occurred";
                prexception.Add(p3);
                string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Error :Some exception has occurre";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                return prexception;
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<LuminiousUpdates> getLuminiousUpdate(string date, string userid, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {

            // string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/getLuminiousUpdate";
            try
            {
                List<LuminiousUpdates> LmupdateList = new List<LuminiousUpdates>();
                LuminousEntities luminous = new LuminousEntities();

                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        DateTime datea = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                        var getLuminiousUpdateData = luminous.luminious_Update.Where(a => a.StartDate <= datea && a.Expirydate >= datea && a.Status == 1).Select(c => new { c.Id, c.ImageName, c.Textbody, c.Title, c.VideoURL }).ToList();



                        if (getLuminiousUpdateData.Count() == 0)
                        {
                            LuminiousUpdates Lmnodata = new LuminiousUpdates();
                            Lmnodata.Error = "No Data Found";
                            LmupdateList.Add(Lmnodata);

                            string RequestParameter = "Date :" + date + ",UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Message :No Data Found";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Found", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            return LmupdateList;
                        }
                        else
                        {
                            foreach (var data in getLuminiousUpdateData)
                            {
                                LuminiousUpdates lmupdate = new LuminiousUpdates();

                                lmupdate.Id = data.Id;
                                lmupdate.Title = data.Title;
                                if (data.ImageName == "" || data.ImageName == null)
                                {
                                    lmupdate.Image = "";
                                }
                                else
                                {
                                    if (File.Exists(Server.MapPath("~/LuminiousUpdateImage/") + data.ImageName))
                                    {
                                        lmupdate.Image = Url + "LuminiousUpdateImage/" + data.ImageName;
                                    }

                                }
                                lmupdate.Description = data.Textbody;

                                if (data.VideoURL == null || data.VideoURL == "")
                                {
                                    lmupdate.VideoURL = "0";
                                }
                                else
                                {
                                    lmupdate.VideoURL = data.VideoURL;
                                }

                                LmupdateList.Add(lmupdate);

                            }
                            string RequestParameter = "Date :" + date + ",UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Message :Success";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                            return LmupdateList;
                        }
                    }
                    else
                    {
                        LuminiousUpdates Unauthorize = new LuminiousUpdates();
                        Unauthorize.Status = "1";
                        Unauthorize.Error = "User already logged in three devices";
                        LmupdateList.Add(Unauthorize);
                        string RequestParameter = "Date :" + date + ",UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Error :User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        return LmupdateList;
                    }
                }
                else
                {
                    LuminiousUpdates Unauthorize = new LuminiousUpdates();
                    Unauthorize.Error = "Unauthorized Access";
                    LmupdateList.Add(Unauthorize);

                    string RequestParameter = "Date :" + date + ",UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Error :Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    return LmupdateList;
                }
            }
            catch (Exception exc)
            {
                List<LuminiousUpdates> LmupdateList = new List<LuminiousUpdates>();
                LuminiousUpdates lmupdate = new LuminiousUpdates();
                lmupdate.Error = "Some exception has occurred";
                LmupdateList.Add(lmupdate);
                string RequestParameter = "Date :" + date + ",UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Error :Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                return LmupdateList;
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public string UpdateDeviceId(string userid, string DeviceId, string DevId)
        {
            LuminousEntities luminous = new LuminousEntities();
            if (DeviceId == "")
            {
                return "Device Id Is Empty";
            }
            else
            {
                try
                {
                    string ostype = "";
                    var dataexist = luminous.UpdateNotificationDeviceids.Where(c => c.Userid == userid && c.Devid == DevId);
                    if (dataexist.Count() == 0)
                    {
                        //UsersList userlist = luminous.UsersLists.Single(a => a.UserId.Trim() == userid.Trim());
                        if (userid != "" && DevId != "")
                        {
                            if (userid.Contains("&"))
                            {
                                string[] uid = userid.Split('&');
                                ostype = uid[1];
                                userid = uid[0];
                            }
                            UpdateNotificationDeviceid Notdeviceid = new UpdateNotificationDeviceid();
                            Notdeviceid.Userid = userid;
                            Notdeviceid.DeviceId = DeviceId;
                            Notdeviceid.Createdon = DateTime.Now;
                            Notdeviceid.Createdby = userid;
                            Notdeviceid.Devid = DevId;
                            Notdeviceid.OSType = ostype;
                            luminous.UpdateNotificationDeviceids.AddObject(Notdeviceid);
                            if (luminous.SaveChanges() > 0)
                            {
                                return "Record inserted Successfully";
                            }
                            else
                            {
                                return "Record Was Not Updated";
                            }
                        }
                        else
                        {
                            return "User Id Missing";
                        }
                    }
                    else
                    {
                        if (userid != "" && DevId != "")
                        {
                            if (userid.Contains("&"))
                            {
                                string[] uid = userid.Split('&');
                                ostype = uid[1];
                                userid = uid[0];
                            }
                            luminous.ExecuteStoreCommand("Delete from UpdateNotificationDeviceid where Userid='" + userid + "' and DevId='" + DevId + "' ");

                            UpdateNotificationDeviceid Notdeviceid = new UpdateNotificationDeviceid();
                            Notdeviceid.Userid = userid;
                            Notdeviceid.DeviceId = DeviceId;
                            Notdeviceid.Createdon = DateTime.Now;
                            Notdeviceid.Createdby = userid;
                            Notdeviceid.Devid = DevId;
                            Notdeviceid.OSType = ostype;
                            luminous.UpdateNotificationDeviceids.AddObject(Notdeviceid);
                            if (luminous.SaveChanges() > 0)
                            {
                                return "Record inserted Successfully";
                            }
                            else
                            {
                                return "Record Was Not Updated";
                            }
                        }
                        else
                        {
                            return "User Id Missing";
                        }
                    }

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "Record  Updated Successfully";
        }
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        //public string UpdateDeviceId(string userid, string DeviceId)
        //{
        //    LuminousEntities luminous = new LuminousEntities();
        //    if (DeviceId == "")
        //    {
        //        return "Device Id Is Empty";
        //    }
        //    else
        //    {
        //        try
        //        {
        //            UsersList userlist = luminous.UsersLists.Single(a => a.UserId.Trim() == userid.Trim());
        //            userlist.DeviceId = DeviceId.Trim();
        //            if (luminous.SaveChanges() > 0)
        //            {
        //                return "Record Updated Successfully";
        //            }
        //            else
        //            {
        //                return "Record Was Not Updated";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return ex.Message;
        //        }
        //    }
        //}
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public string InsertContest(string ImageName, Byte[] Image, string DealerName, string DealerCity, string DealerPhone, string DealerEmail, string DistributorCode, string EmpCode, string dealerId, string DealerState, string ParentCategory)
        {
            LuminousEntities luminous = new LuminousEntities();
            string result = String.Empty;
            //int checkValidation = 0;

            //Regex PhoneCheck = new Regex("^(?:(?:\\+?1\\s*(?:[.-]\\s*)?)?(?:\\(\\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\\s*\\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\\s*(?:[.-]\\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\\s*(?:[.-]\\s*)?([0-9]{4})(?:\\s*(?:#|x\\.?|ext\\.?|extension)\\s*(\\d+))?$");
            string data = "";
            try
            {


                var parentcatid = luminous.ParentCategories.Where(c => c.PCName == ParentCategory).Select(c => c.Pcid).SingleOrDefault();

                string Pcatid = parentcatid.ToString();
                var DealeridExist = luminous.InsertContexts.Where(c => c.DealerId == dealerId && c.ParentCategory == Pcatid).ToList();
                if (DealeridExist.Count() != 0)
                {
                    return result = "Entry Already Exist For The Same User";
                }
                else
                {
                    string filename = Path.GetFileNameWithoutExtension(ImageName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(ImageName);

                    InsertContext contest = new InsertContext();

                    contest.ImageEncode = filename;

                    if (DealerName == null || DealerName == "")
                    {
                        contest.DealerName = "";
                    }
                    else
                    {
                        contest.DealerName = DealerName;
                    }
                    if (DealerCity == null || DealerCity == "")
                    {
                        contest.DealerCity = "";
                    }
                    else
                    {
                        contest.DealerCity = DealerCity;
                    }
                    if (DealerEmail == null || DealerEmail == "")
                    {
                        contest.DealerEmail = "";
                    }
                    else
                    {
                        contest.DealerEmail = DealerEmail;
                    }


                    contest.DealerState = DealerState;

                    if (DealerPhone == null || DealerPhone == "")
                    {
                        contest.DealerPhone = "";
                    }
                    else
                    {
                        contest.DealerPhone = DealerPhone;
                    }
                    if (EmpCode == null || EmpCode == "")
                    {
                        contest.EmpCode = "";
                    }
                    else
                    {
                        contest.EmpCode = EmpCode;
                    }

                    contest.Status = 1;
                    contest.DealerId = dealerId;
                    contest.CreatedBy = dealerId;
                    contest.CreatedOn = DateTime.Now;
                    contest.ParentCategory = parentcatid.ToString();
                    // contest.DealerFirmName=DealerFirmName;
                    if (DistributorCode == null || DistributorCode == "")
                    {
                        contest.DistributorCode = "";
                    }
                    else
                    {
                        contest.DistributorCode = DistributorCode;
                    }

                    //luminous.AddToInsertContexts(new InsertContext
                    //{

                    //    ImageEncode = filename,


                    //        DealerName = DealerName.ToString(),


                    //    DealerCity = DealerCity.ToString(),
                    //    DealerPhone = DealerPhone,
                    //    DealerEmail = DealerEmail.ToString(),
                    //    DistributorCode = DistributorCode.ToString(),
                    //    EmpCode = EmpCode.ToString(),
                    //    DealerId = dealerId.ToString(),
                    //    CreatedBy = dealerId.ToString(),
                    //    CreatedOn = DateTime.Now,
                    //    DealerState=DealerState.ToString(),
                    //    ParentCategory=ParentCategory.ToString()


                    //});
                    luminous.InsertContexts.AddObject(contest);
                    data = "hello";
                    // int lastProductId = luminous.InsertContexts.Max(item => item.Id);
                    int affectedRows = luminous.SaveChanges();
                    data = "Welcome";
                    if (affectedRows > 0)
                    {
                        string str = Path.Combine(Server.MapPath("~/ProfileImages/"), filename);
                        BinaryWriter bw = new BinaryWriter(new FileStream(str, FileMode.Create, FileAccess.Write));
                        bw.Write(Image);
                        bw.Close();
                        luminous.AddToInsertContextHistories(new InsertContextHistory
                        {
                            // InserContextId=lastProductId,
                            ImageEncode = filename,
                            DealerName = DealerName,
                            DealerCity = DealerCity,
                            DealerPhone = DealerPhone,
                            DealerEmail = DealerEmail,
                            DistributorCode = DistributorCode,
                            Empcode = EmpCode,
                            DealerID = dealerId,
                            CreatedBy = dealerId,

                            createdOn = DateTime.Now


                        });
                        luminous.SaveChanges();

                        result = "Inserted Successfully";
                    }
                    else
                    {

                        result = "Data Not Inserted";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.InnerException + data;
            }
            return result;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public DataContest GetContestData(string DealerId, string Parentcategory)
        {
            LuminousEntities luminous = new LuminousEntities();
            string Img = "";
            string result = String.Empty;
            string url = HttpContext.Current.Request.Url.ToString() + "/GetContestData";
            //int checkValidation = 0;

            //Regex PhoneCheck = new Regex("^(?:(?:\\+?1\\s*(?:[.-]\\s*)?)?(?:\\(\\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\\s*\\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\\s*(?:[.-]\\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\\s*(?:[.-]\\s*)?([0-9]{4})(?:\\s*(?:#|x\\.?|ext\\.?|extension)\\s*(\\d+))?$");

            try
            {
                var parentcatid = luminous.ParentCategories.Where(c => c.PCName == Parentcategory).Select(c => c.Pcid).SingleOrDefault();
                string parentcategoryid = parentcatid.ToString();
                var gettime = luminous.InsertContexts.Where(c => c.DealerId == DealerId && c.ParentCategory == parentcategoryid).Select(c => c.CreatedOn).ToList();

                if (gettime.Count == 0)
                {
                    DataContest Dcerror = new DataContest();
                    Dcerror.id = "-1";
                    Dcerror.Error = "No Data Exist";
                    return Dcerror;
                }
                else
                {
                    DateTime dttime = gettime.First().Value.AddMinutes(15);
                    DateTime dtCurrent = DateTime.Now;
                    if (dttime >= dtCurrent)
                    {
                        var getImage = luminous.InsertContexts.Where(c => c.DealerId == DealerId && c.ParentCategory == parentcategoryid).Select(c => c.ImageEncode).SingleOrDefault();
                        if (File.Exists(Server.MapPath("~/ProfileImages/" + getImage)))
                        {
                            Img = Url + "ProfileImages/" + getImage;
                        }
                        else
                        {
                            Img = "No Image";
                        }
                        var getContext = (from c in luminous.InsertContexts
                                          where c.DealerId == DealerId && c.ParentCategory == parentcategoryid
                                          select new
                                          {

                                              ImageEncodes = Img,
                                              DealerName = c.DealerName,
                                              DealerCity = c.DealerCity,
                                              DealerPhone = c.DealerPhone,
                                              DealerEmail = c.DealerEmail,
                                              DistributerCode = c.DistributorCode,
                                              EmpCode = c.EmpCode,
                                              DealerID = c.DealerId,
                                              StateID = c.DealerState,
                                              ParentCategory = c.ParentCategory,
                                              //DealerFirmName=c.DealerFirmName,
                                              Id = c.Id

                                          }).ToList();
                        if (getContext.Count != 0)
                        {
                            foreach (var data in getContext)
                            {
                                DataContest Dc = new DataContest();
                                Dc.id = data.Id.ToString();
                                Dc.ImageEncode = data.ImageEncodes;
                                Dc.DealerName = data.DealerName.ToString();
                                Dc.DealerCity = data.DealerCity.ToString();
                                Dc.DealerPhone = data.DealerPhone.ToString();
                                Dc.DealerEmail = data.DealerEmail.ToString();
                                Dc.DistributorCode = data.DistributerCode.ToString();
                                Dc.Empcode = data.EmpCode.ToString();
                                Dc.DealerId = data.DealerID.ToString();
                                Dc.DealerState = data.StateID;
                                //  Dc.DealerFirmName = data.DealerFirmName;
                                int pcatid = Convert.ToInt32(data.ParentCategory);
                                var parentcatname = luminous.ParentCategories.Where(c => c.Pcid == pcatid).Select(c => c.PCName).SingleOrDefault();
                                Dc.ParentCategory = parentcatname.ToString();
                                Dc.Flag = 1;

                                string RequestParameter = "DealerID :" + DealerId + ",Parentcategory :" + Parentcategory + "";
                                string ResponseParameter = "Message :Success";

                                SaveServiceLog(DealerId, url, RequestParameter, ResponseParameter, 1, "Success", DealerId, DateTime.Now, "", "", "", "");


                                return Dc;
                            }

                        }
                    }
                    else
                    {
                        var getImage = luminous.InsertContexts.Where(c => c.DealerId == DealerId && c.ParentCategory == parentcategoryid).Select(c => c.ImageEncode).SingleOrDefault();
                        if (File.Exists(Server.MapPath("~/ProfileImages/" + getImage)))
                        {
                            Img = Url + "ProfileImages/" + getImage;
                        }
                        else
                        {
                            Img = "No Image";
                        }
                        var getContest = (from c in luminous.InsertContexts

                                          where c.DealerId == DealerId && c.ParentCategory == parentcategoryid
                                          select new
                                          {
                                              ImageEncodes = Img,
                                              DealerName = c.DealerName,
                                              DealerCity = c.DealerCity,
                                              DealerPhone = c.DealerPhone,
                                              DealerEmail = c.DealerEmail,
                                              DistributerCode = c.DistributorCode,
                                              EmpCode = c.EmpCode,
                                              DealerID = c.DealerId,
                                              StateID = c.DealerState,
                                              //DealerFirmName = c.DealerFirmName,
                                              Parentcategory = c.ParentCategory,
                                              Id = c.Id

                                          }).ToList();
                        if (getContest.Count != 0)
                        {
                            foreach (var data in getContest)
                            {
                                DataContest Dc = new DataContest();
                                Dc.id = data.Id.ToString();
                                Dc.ImageEncode = data.ImageEncodes;
                                Dc.DealerName = data.DealerName.ToString();
                                Dc.DealerCity = data.DealerCity.ToString();
                                Dc.DealerPhone = data.DealerPhone.ToString();
                                Dc.DealerEmail = data.DealerEmail.ToString();
                                Dc.DistributorCode = data.DistributerCode.ToString();
                                Dc.Empcode = data.EmpCode.ToString();
                                Dc.DealerId = data.DealerID.ToString();
                                Dc.DealerState = data.StateID;
                                //  Dc.DealerFirmName = data.DealerFirmName;
                                int pcatid = Convert.ToInt32(data.Parentcategory);
                                var parentcatname = luminous.ParentCategories.Where(c => c.Pcid == pcatid).Select(c => c.PCName).SingleOrDefault();
                                Dc.ParentCategory = parentcatname.ToString();
                                Dc.Flag = 0;
                                string RequestParameter = "DealerID :" + DealerId + ",Parentcategory :" + Parentcategory + "";
                                string ResponseParameter = "Message :Success";

                                SaveServiceLog(DealerId, url, RequestParameter, ResponseParameter, 1, "Success", DealerId, DateTime.Now, "", "", "", "");
                                return Dc;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Exception ep = ex.InnerException;
                DataContest Dcerror = new DataContest();
                Dcerror.id = "-1";
                Dcerror.Error = ex.Message;
                string RequestParameter = "DealerID :" + DealerId + ",Parentcategory :" + Parentcategory + "";
                string ResponseParameter = "Message :Some exception has occurred";

                SaveServiceLog(DealerId, url, RequestParameter, ResponseParameter, 1, ex.InnerException.ToString(), DealerId, DateTime.Now, "", "", "", "");
                return Dcerror;
            }

            return new DataContest();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public string UpdateContest(string ImageName, Byte[] Image, string DealerName, string DealerCity, string DealerPhone, string DealerEmail, string DistributorCode, string EmpCode, string dealerId, string DealerState, string ParentCategory)
        {
            LuminousEntities luminous = new LuminousEntities();
            string result = String.Empty;
            //int checkValidation = 0;

            //Regex PhoneCheck = new Regex("^(?:(?:\\+?1\\s*(?:[.-]\\s*)?)?(?:\\(\\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\\s*\\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\\s*(?:[.-]\\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\\s*(?:[.-]\\s*)?([0-9]{4})(?:\\s*(?:#|x\\.?|ext\\.?|extension)\\s*(\\d+))?$");

            try
            {
                string filename = Path.GetFileNameWithoutExtension(ImageName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(ImageName);
                var parentcatid = luminous.ParentCategories.Where(c => c.PCName == ParentCategory).Select(c => c.Pcid).SingleOrDefault();
                string parentcategoryid = parentcatid.ToString();
                var gettime = luminous.InsertContexts.Where(c => c.DealerId == dealerId && c.ParentCategory == parentcategoryid).Select(c => c.CreatedOn).ToList();
                DateTime dttime = gettime.First().Value.AddMinutes(15);
                DateTime dtCurrent = DateTime.Now;
                if (dttime >= dtCurrent)
                {
                    InsertContext user = luminous.InsertContexts.Single(a => a.DealerId == dealerId && a.ParentCategory == parentcategoryid);
                    user.ImageEncode = filename;
                    user.DealerName = DealerName;
                    user.DealerCity = DealerCity;
                    user.DealerState = DealerState;
                    user.ParentCategory = parentcatid.ToString();
                    user.DealerPhone = DealerPhone;
                    user.DealerEmail = DealerEmail;
                    user.DistributorCode = DistributorCode;
                    user.EmpCode = EmpCode;
                    //user.DealerFirmName = DealerFirmName;
                    int affectedRows = luminous.SaveChanges();

                    if (affectedRows > 0)
                    {
                        string str = Path.Combine(Server.MapPath("~/ProfileImages/"), filename);
                        BinaryWriter bw = new BinaryWriter(new FileStream(str, FileMode.Create, FileAccess.Write));
                        bw.Write(Image);
                        bw.Close();

                        luminous.AddToInsertContextHistories(new InsertContextHistory
                        {

                            ImageEncode = filename,
                            DealerName = DealerName,
                            DealerCity = DealerCity,
                            DealerPhone = DealerPhone,
                            DealerEmail = DealerEmail,
                            DistributorCode = DistributorCode,
                            Empcode = EmpCode,
                            DealerID = dealerId,
                            CreatedBy = dealerId,
                            createdOn = user.CreatedOn,
                            ModifyBy = dealerId,
                            ModifyOn = DateTime.Now

                        });

                        luminous.SaveChanges();
                        result = "Updated Successfully";
                    }


                    else
                    {
                        result = "Date Not Updated";
                    }
                }
                else
                {
                    result = "Cannot Update Data beacuse 15 Minute Limit Exceeded0";
                }

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public UserVerifications Userverification(string userid, string deviceid, string Otp, string Appversion, string Ostype, string Osversion)
        {
            string url = HttpContext.Current.Request.Url.ToString() + "/Userverification";
            // string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            LuminousEntities luminous = new LuminousEntities();
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;

                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                var Usercount = luminous.UserVerifications.Where(c => c.UserId == userid && c.DeviceId == deviceid && c.Status != 0 && c.Status != 1).Count();
                var Usercountexist = luminous.UserVerifications.Where(c => c.UserId == userid && c.Status == 2).Count();
                if (Usercount == 0 && Usercountexist <= 2)
                {
                    //var Usercountexist = luminous.UserVerifications.Where(c => c.UserId == userid && c.DeviceId == deviceid && c.Status==2).Count();

                    luminous.AddToUserVerifications(new UserVerification
                    {

                        UserId = userid,
                        DeviceId = deviceid,
                        Otp = Otp,
                        AppVersion = Appversion,
                        OSVersion = Osversion,
                        OSType = Ostype,
                        Status = 2,
                        CreatedOn = DateTime.Now,
                        CreatedBy = userid.ToString()
                    });
                    luminous.SaveChanges();




                    UserVerifications Uv = new UserVerifications();
                    Uv.status = "2";
                    Uv.Token = LoginToken;
                    Uv.Message = "Success";

                    string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :" + Otp + ",AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Status :" + Uv.status + ",Token :" + Uv.Token + ",Mesaage :" + Uv.Message + "";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, Uv.Message, userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    return Uv;
                }
                else
                {

                    if (Usercount == 1)
                    {

                        UserVerifications Uv = new UserVerifications();
                        Uv.status = "2";
                        Uv.Token = LoginToken;
                        Uv.Message = "Success";
                        return Uv;

                    }
                    else
                    {
                        luminous.AddToUserVerifications(new UserVerification
                        {

                            UserId = userid,
                            DeviceId = deviceid,
                            Otp = Otp,
                            OSVersion = Osversion,
                            OSType = Ostype,
                            UnauthorizedUser = "logged in three devices",
                            Status = 0,
                            CreatedOn = DateTime.Now,
                            CreatedBy = userid.ToString()
                        });
                        luminous.SaveChanges();

                        UserVerifications Uv = new UserVerifications();
                        Uv.status = "1";
                        Uv.Token = "0";
                        Uv.Message = "User already logged in three devices";

                        //Used for maintain the Service Log"//

                        string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :" + Otp + ",AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Status :" + Uv.status + ",Token :" + Uv.Token + ",Mesaage :" + Uv.Message + "";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized User", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        return Uv;
                    }
                }
            }
            catch (Exception exc)
            {
                UserVerifications Uv = new UserVerifications();
                Uv.status = "-1";
                Uv.Token = "0";
                Uv.Message = "Some exception has occurred";

                //Used for maintain the Service Log"//

                string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :" + Otp + ",AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Status :" + Uv.status + ",Token :" + Uv.Token + ",Mesaage :" + Uv.Message + "";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString().ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                return Uv;
            }

            return new UserVerifications();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<CustomerPermission> getCustomerPermission(string userid, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<CustomerPermission> getPermission = new List<CustomerPermission>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/getCustomerPermission";
            try
            {
                var getappversion = luminous.AppVersions.Where(c => c.Version == Appversion).ToList();
                if (getappversion.Count > 0)
                {
                    string LoginToken = "";
                    string TokenString = userid + Appversion;
                    using (MD5 md5Hash = MD5.Create())
                    {

                        LoginToken = GetMd5Hash(md5Hash, TokenString);


                    }
                    if (LoginToken == token)
                    {
                        var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                        if (checkstatus.Count() == 0)
                        {
                            List<getCustomerPermission_Result> getPerm = luminous.getCustomerPermission(userid).ToList();
                            foreach (getCustomerPermission_Result gp in getPerm)
                            {
                                CustomerPermission cpermission = new CustomerPermission();
                                cpermission.CustomerType = gp.Usertype.ToString();
                                cpermission.ModuleName = gp.ModuleName.ToString();
                                cpermission.Permission = gp.Permission.ToString();
                                if (gp.ModuleImage != "0")
                                {
                                    cpermission.ModuleImage = Url + "AppMenuIcon/" + gp.ModuleImage.ToString();
                                }
                                else
                                {
                                    cpermission.ModuleImage = "0";
                                }
                                cpermission.MonthValue = 5;
                                cpermission.Message = "Success";
                                getPermission.Add(cpermission);
                            }
                            string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "CustomerType,ModuleName,Permission,Message";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                        else
                        {
                            CustomerPermission cpermission = new CustomerPermission();
                            cpermission.CustomerType = "0";
                            cpermission.ModuleName = "0";
                            cpermission.Permission = "0";
                            cpermission.MonthValue = 0;
                            cpermission.Status = "1";
                            cpermission.Message = "User already logged in three devices";
                            getPermission.Add(cpermission);
                            string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "CustomerType,ModuleName,Permission,Message:User already logged in three devices";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                    }
                    else
                    {
                        CustomerPermission cpermission = new CustomerPermission();
                        cpermission.CustomerType = "0";
                        cpermission.ModuleName = "0";
                        cpermission.Permission = "0";
                        cpermission.MonthValue = 0;
                        cpermission.Message = "Unauthorized Access";
                        getPermission.Add(cpermission);
                        string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "CustomerType:0,ModuleName:0,Permission:0,Message:Unauthorized Access";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    CustomerPermission cpermission = new CustomerPermission();
                    cpermission.CustomerType = "0";
                    cpermission.ModuleName = "0";
                    cpermission.Permission = "0";
                    cpermission.MonthValue = 0;
                    cpermission.Message = "Please update your app version";
                    getPermission.Add(cpermission);
                    string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "CustomerType:0,ModuleName:0,Permission:0,Message:Some exception has occurred";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Please update your app version", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                CustomerPermission cpermission = new CustomerPermission();
                cpermission.CustomerType = "0";
                cpermission.ModuleName = "0";
                cpermission.Permission = "0";
                cpermission.MonthValue = 0;
                cpermission.Message = "Some exception has occurred";
                getPermission.Add(cpermission);
                string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "CustomerType:0,ModuleName:0,Permission:0,Message:Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getPermission;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<LabelData> GetWRSLabels(string userid, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<LabelData> getWRSLabel = new List<LabelData>();
            // string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetWRSLabels";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        var _WRS = luminous.FooterCategories.Where(c => c.CatType == "WRS" && c.FCategoryName != "Scheme Status").Select(c => new { c.Id, c.FCategoryName }).ToList();
                        foreach (var data in _WRS)
                        {
                            LabelData wd = new LabelData();
                            wd.Id = data.Id;
                            wd.FooterCatName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(data.FCategoryName.ToLower());
                            wd.Message = "Success";
                            getWRSLabel.Add(wd);
                        }
                        string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id,FooterCatName,Message";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                    else
                    {
                        LabelData wd = new LabelData();
                        wd.Id = 0;
                        wd.FooterCatName = "0";
                        wd.Status = "1";
                        wd.Message = "User already logged in three devices";
                        getWRSLabel.Add(wd);
                        string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id,FooterCatName,Message";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                    }

                }
                else
                {
                    LabelData wd = new LabelData();
                    wd.Id = 0;
                    wd.FooterCatName = "0";
                    wd.Message = "Unauthorized Access";
                    getWRSLabel.Add(wd);
                    string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Id :0,FooterCatName :0,Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                LabelData wd = new LabelData();
                wd.Id = -1;
                wd.FooterCatName = "0";
                wd.Message = "Some exception has occurred";
                getWRSLabel.Add(wd);
                string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Id :-1,FooterCatName :0,Message:Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getWRSLabel;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<LabelData> GetMediaLabels(string userid, string token, string Appversion, string deviceid, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<LabelData> getMediaLabel = new List<LabelData>();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetMediaLabels";
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        var _WRS = luminous.FooterCategories.Where(c => c.CatType == "Media").Select(c => new { c.Id, c.FCategoryName }).ToList();
                        foreach (var data in _WRS)
                        {
                            LabelData wd = new LabelData();
                            wd.Id = data.Id;
                            wd.FooterCatName = data.FCategoryName;
                            wd.Message = "Success";
                            getMediaLabel.Add(wd);
                        }
                        string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id,FooterCatName,Message";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                    else
                    {
                        LabelData wd = new LabelData();
                        wd.Id = 0;
                        wd.FooterCatName = "0";
                        wd.Status = "1";
                        wd.Message = "User already logged in three devices";
                        getMediaLabel.Add(wd);
                    }

                }
                else
                {
                    LabelData wd = new LabelData();
                    wd.Id = 0;
                    wd.FooterCatName = "0";
                    wd.Message = "Unauthorized Access";
                    getMediaLabel.Add(wd);
                    string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Id :0,FooterCatName :0,Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                LabelData wd = new LabelData();
                wd.Id = -1;
                wd.FooterCatName = "0";
                wd.Message = "Some exception has occurred";
                getMediaLabel.Add(wd);
                string RequestParameter = "UserID :" + userid + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Id :-1,FooterCatName :0,Message:Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getMediaLabel;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<ProductVideoList> GetProductVideoList(string userid, int Labelid, string token, string Appversion, string deviceid, string Ostype, string Osversion, string status)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<ProductVideoList> getPVL = new List<ProductVideoList>();
            string url = "";
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            if (Labelid == 4)
            {
                url = HttpContext.Current.Request.Url.ToString() + "/GetProductVideoList/TVC";
            }
            else if (Labelid == 5)
            {
                url = HttpContext.Current.Request.Url.ToString() + "/GetProductVideoList/ProductVideos";
            }
            else if (Labelid == 6)
            {
                url = HttpContext.Current.Request.Url.ToString() + "/GetProductVideoList/Testimonials";
            }
            else
            {
                url = HttpContext.Current.Request.Url.ToString() + "/GetProductVideoList";
            }

            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        var _ProductVideoList = luminous.MediaDatas.Where(c => c.LabelId == Labelid && c.Status != 0).ToList();
                        // var _ProductVideoList = luminous..Where(c => c.LabelId == Labelid && c.Status!=0).ToList();
                        foreach (var data in _ProductVideoList)
                        {
                            ProductVideoList pvl = new ProductVideoList();
                            pvl.Id = data.Id;
                            pvl.VideoName = data.VideoName;
                            pvl.VideoImage = Url + "MediaImages/" + data.VideoImage;
                            pvl.Url = data.Url;
                            pvl.Message = "Success";
                            getPVL.Add(pvl);
                        }
                        string RequestParameter = "UserID :" + userid + ",LabelId :" + Labelid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id,VideoName,VideoImage,Url,Message";

                        SaveServiceLog(userid, url, RequestParameter + "%" + status, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                    else
                    {
                        ProductVideoList pvl = new ProductVideoList();
                        pvl.Id = -1;
                        pvl.VideoName = "0";
                        pvl.VideoName = "0";
                        pvl.Url = "0";
                        pvl.Status = "1";
                        pvl.Message = "User already logged in three devices";
                        getPVL.Add(pvl);
                        string RequestParameter = "UserID :" + userid + ",LabelId :" + Labelid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Id :-1,VideoName :0,VideoName:0,Url:0,Message :User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter + "%" + status, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }

                }
                else
                {
                    ProductVideoList pvl = new ProductVideoList();
                    pvl.Id = -1;
                    pvl.VideoName = "0";
                    pvl.VideoName = "0";
                    pvl.Url = "0";
                    pvl.Message = "Unauthorized Access";
                    getPVL.Add(pvl);
                    string RequestParameter = "UserID :" + userid + ",LabelId :" + Labelid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Id :-1,VideoName :0,VideoName:0,Url:0,Message :Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter + "%" + status, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                ProductVideoList pvl = new ProductVideoList();
                pvl.Id = -1;
                pvl.VideoName = "0";
                pvl.VideoName = "0";
                pvl.Url = "0";
                pvl.Message = "Some exception has occurred";
                getPVL.Add(pvl);
                string RequestParameter = "UserID :" + userid + ",LabelId :" + Labelid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Id :-1,VideoName :0,VideoName:0,Url:0,Message:Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter + "%" + status, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getPVL;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<NotificationSurvey> GetSurveyNotificationList(string date, string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<NotificationSurvey> getSurveyList = new List<NotificationSurvey>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetSurveyNotificationList";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                        //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();

                        var data = from cl in luminous.NotificationSurveys
                                   where (
                                       from i in luminous.SaveNotificationSurveys
                                       where i.SurveyID == cl.SurveyID && i.UserId == userid
                                       select i).Count() == 0
                                   select new { SurveyID = cl.SurveyID, Survey = cl.Survey, StartDate = cl.StartDate, Enddate = cl.Enddate };

                        var Surveylist = from cdata in data
                                         where cdata.StartDate <= dateExist && cdata.Enddate >= dateExist
                                         select new
                                         {
                                             SurveyID = cdata.SurveyID,
                                             Survey = cdata.Survey
                                         };
                        if (Surveylist.Count() > 0)
                        {
                            foreach (var list in Surveylist)
                            {
                                NotificationSurvey Nsurvey = new NotificationSurvey();
                                Nsurvey.SurveyId = list.SurveyID;
                                Nsurvey.Survey = list.Survey;
                                Nsurvey.Message = "Success";
                                getSurveyList.Add(Nsurvey);
                            }
                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "SurveyId,Survey,Message";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                        else
                        {
                            NotificationSurvey Nsurvey = new NotificationSurvey();
                            Nsurvey.SurveyId = -1;
                            Nsurvey.Survey = "0";
                            Nsurvey.Message = "No Data Exist";
                            getSurveyList.Add(Nsurvey);
                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "SurveyId,Survey,Message";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                    }
                    else
                    {
                        NotificationSurvey Nsurvey = new NotificationSurvey();
                        Nsurvey.SurveyId = 0;
                        Nsurvey.Survey = "0";
                        Nsurvey.Status = "1";
                        Nsurvey.Message = "User already logged in three devices";
                        getSurveyList.Add(Nsurvey);
                        string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "SurveyId,Survey,Message";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    NotificationSurvey Nsurvey = new NotificationSurvey();
                    Nsurvey.SurveyId = -1;
                    Nsurvey.Survey = "0";
                    Nsurvey.Message = "Unauthorized Access";
                    getSurveyList.Add(Nsurvey);
                    string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "SurveyId,Survey,Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                NotificationSurvey Nsurvey = new NotificationSurvey();
                Nsurvey.SurveyId = -1;
                Nsurvey.Survey = "0";
                Nsurvey.Message = "Some exception has occurred";
                getSurveyList.Add(Nsurvey);
                string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "SurveyId,Survey,Message";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getSurveyList;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<NotificationSurvey> GetSurveyQuestion(int Surveyid, string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<NotificationSurvey> getSurveyListQues = new List<NotificationSurvey>();
            // string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetSurveyQuestion";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        var Surveylist = luminous.NotificationSurveys.Where(c => c.SurveyID == Surveyid).Select(c => new { c.SurveyID, c.Survey, c.QuestionType, c.QuestionTitle, c.OptionA, c.OptionB, c.OptionC, c.OptionD, c.OptionE, c.CorrectAns }).ToList();

                        //  var Surveylist = from cl in luminous.NotificationSurveys
                        //where ( 
                        //    from i in luminous.SaveNotificationSurveys
                        //    where i.SurveyID != cl.SurveyID && 
                        //    select i ).Count( ) == 0 
                        //select cl;

                        // var matchsurvey=Surveylist.Where(!luminous.SaveNotificationSurveys.Where(c=>c.SurveyID.Value==Surveyid).ToList();

                        foreach (var list in Surveylist)
                        {
                            NotificationSurvey Nsurvey = new NotificationSurvey();
                            Nsurvey.SurveyId = list.SurveyID;
                            Nsurvey.Survey = list.Survey;
                            Nsurvey.QuestionTitle = list.QuestionTitle;
                            Nsurvey.QuestionType = list.QuestionType;

                            if (list.OptionA == null || list.OptionA == "")
                            {
                                Nsurvey.OptionA = "0";
                            }
                            else
                            {
                                Nsurvey.OptionA = list.OptionA;
                            }
                            if (list.OptionB == null || list.OptionB == "")
                            {
                                Nsurvey.OptionB = "0";
                            }
                            else
                            {
                                Nsurvey.OptionB = list.OptionB;
                            }
                            if (list.OptionC == null || list.OptionC == "")
                            {
                                Nsurvey.OptionC = "0";
                            }
                            else
                            {
                                Nsurvey.OptionC = list.OptionC;
                            }
                            if (list.OptionD == null || list.OptionD == "")
                            {
                                Nsurvey.OptionD = "0";
                            }
                            else
                            {
                                Nsurvey.OptionD = list.OptionD;
                            }
                            if (list.OptionE == null || list.OptionE == "")
                            {
                                Nsurvey.OptionE = "0";
                            }
                            else
                            {
                                Nsurvey.OptionE = list.OptionE;
                            }
                            if (list.CorrectAns == null || list.CorrectAns == "")
                            {
                                Nsurvey.CorrectAns = "0";
                            }
                            else
                            {
                                Nsurvey.CorrectAns = list.CorrectAns;
                            }

                            Nsurvey.Message = "Success";
                            getSurveyListQues.Add(Nsurvey);
                        }
                        string RequestParameter = "SurveyId :" + Surveyid + ",UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "SurveyId,Survey,QuestionTitle,QuestionType,OptionA,OptionB,OptionC,OptionD,OptionE,CorrectAns,Message";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                    else
                    {
                        NotificationSurvey Nsurvey = new NotificationSurvey();
                        Nsurvey.SurveyId = 0;
                        Nsurvey.Survey = "0";
                        Nsurvey.QuestionType = "0";
                        Nsurvey.QuestionTitle = "0";
                        Nsurvey.OptionA = "0";
                        Nsurvey.OptionB = "0";
                        Nsurvey.OptionC = "0";
                        Nsurvey.OptionD = "0";
                        Nsurvey.OptionE = "0";
                        Nsurvey.CorrectAns = "0";
                        Nsurvey.Status = "1";
                        Nsurvey.Message = "User already logged in three devices";
                        getSurveyListQues.Add(Nsurvey);
                    }

                }
                else
                {
                    NotificationSurvey Nsurvey = new NotificationSurvey();
                    Nsurvey.SurveyId = -1;
                    Nsurvey.Survey = "0";
                    Nsurvey.QuestionType = "0";
                    Nsurvey.QuestionTitle = "0";
                    Nsurvey.OptionA = "0";
                    Nsurvey.OptionB = "0";
                    Nsurvey.OptionC = "0";
                    Nsurvey.OptionD = "0";
                    Nsurvey.OptionE = "0";
                    Nsurvey.CorrectAns = "0";
                    Nsurvey.Message = "Unauthorized Access";
                    getSurveyListQues.Add(Nsurvey);
                    string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "SurveyId :-1,Survey :0,QuestionType :0,OptionA :0,OptionB :0,OptionC :0,OptionD :0,OptionE :0,CorrectAns :0,Message :Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                NotificationSurvey Nsurvey = new NotificationSurvey();
                Nsurvey.SurveyId = -1;
                Nsurvey.Survey = "0";
                Nsurvey.QuestionTitle = "0";
                Nsurvey.QuestionType = "0";
                Nsurvey.OptionA = "0";
                Nsurvey.OptionB = "0";
                Nsurvey.OptionC = "0";
                Nsurvey.OptionD = "0";
                Nsurvey.OptionE = "0";
                Nsurvey.CorrectAns = "0";
                Nsurvey.Message = "Some exception has occurred";
                getSurveyListQues.Add(Nsurvey);
                string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "SurveyId :-1,Survey :0,QuestionType :0,OptionA :0,OptionB :0,OptionC :0,OptionD :0,OptionE :0,CorrectAns :0,Message :Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getSurveyListQues;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public string SaveSurveyResult(string userid, int SurveyId, string option, string Optionvalue, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();

            string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {

                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {

                        luminous.AddToSaveNotificationSurveys(new SaveNotificationSurvey
                        {

                            UserId = userid,
                            DeviceId = deviceid,
                            SurveyID = SurveyId,
                            Options = option,
                            OptionValue = Optionvalue,
                            CreatedOn = DateTime.Now,
                            CreatedBy = userid.ToString()
                        });
                        luminous.SaveChanges();

                        string RequestParameter = "SurveyId :" + SurveyId + ",Option :" + option + ",UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Inserted Successfully";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Inserted Successfully", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                    else
                    {
                        return "User already logged in three devices";
                    }
                }
                else
                {
                    string RequestParameter = "SurveyId :" + SurveyId + ",Option :" + option + ",UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    return "Unauthorized Access";
                }
            }
            catch (Exception exc)
            {
                string RequestParameter = "SurveyId :" + SurveyId + ",Option :" + option + ",UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                return "Some exception has occurred";
            }
            return "Inserted Successfully";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<RedirectPage> getRedirectPageData(int Bannerid, string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<RedirectPage> getpagedata = new List<RedirectPage>();
            string url = "";
            string specificpage = "";
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            url = HttpContext.Current.Request.Url.ToString() + "/getRedirectPageData";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        //  DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                        //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();
                        var pagename = from cl in luminous.Banners
                                       join redirectpage in luminous.Redirectdatas on cl.RedirectPage equals redirectpage.Id
                                       where cl.id == Bannerid
                                       select new
                                       {
                                           PageName = redirectpage.PageName
                                       };

                        var page = pagename.SingleOrDefault();
                        if (page.PageName == "Product Details")
                        {

                            url = HttpContext.Current.Request.Url.ToString() + "/BannerRedirect/GetProductLevelFourByNameVersion2";
                            var Productdetails = from cl in luminous.Banners
                                                 join redirectpage in luminous.Redirectdatas on cl.RedirectPage equals redirectpage.Id
                                                 join prdlevelthree in luminous.ProductLevelThrees on cl.ProductLevelThreeId equals prdlevelthree.id
                                                 where cl.id == Bannerid
                                                 select new
                                                 {
                                                     PageName = redirectpage.PageName,

                                                     Productlevelthree = prdlevelthree.Name,
                                                 };
                            if (Productdetails.Count() > 0)
                            {
                                foreach (var prddetails in Productdetails)
                                {
                                    RedirectPage rp = new RedirectPage();
                                    rp.PageName = prddetails.PageName;
                                    rp.ProductLevelThree = prddetails.Productlevelthree;
                                    rp.ParentCategory = "0";
                                    rp.ProductCategory = "0";
                                    rp.ProductLevelOne = "0";
                                    rp.ProductLevelTwo = "0";

                                    rp.Media = "0";
                                    rp.month = 0;
                                    rp.year = 0;
                                    rp.Message = "Success";
                                    getpagedata.Add(rp);

                                    specificpage = prddetails.Productlevelthree;

                                    // url = HttpContext.Current.Request.Url.ToString() + "/BannerRedirect/GetProductLevelFourByNameVersion2/";

                                }
                                string RequestParameter = "UserID :" + userid + ",BannerID :" + Bannerid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid + ",Redirect from Banner to " + page.PageName + "_" + specificpage + "", url, RequestParameter, ResponseParameter, 0, "Success Product Details ", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                            else
                            {
                                RedirectPage rp = new RedirectPage();
                                rp.PageName = "0";
                                rp.ProductLevelThree = "0";
                                rp.ParentCategory = "0";
                                rp.ProductCategory = "0";
                                rp.ProductLevelOne = "0";
                                rp.ProductLevelTwo = "0";
                                rp.ProductLevelThree = "0";
                                rp.Media = "0";
                                rp.month = 0;
                                rp.year = 0;
                                rp.Message = "Failure";
                                getpagedata.Add(rp);
                                string RequestParameter = "UserID :" + userid + ",BannerID :" + Bannerid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid + ",Redirect from Banner to " + page.PageName, url, RequestParameter, ResponseParameter, 1, "Failure Product Details", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }

                        }

                        if (page.PageName == "Scheme" || page.PageName == "Price List")
                        {

                            if (page.PageName == "Scheme")
                            {
                                url = HttpContext.Current.Request.Url.ToString() + "/BannerRedirect/Scheme";
                            }
                            if (page.PageName == "Price List")
                            {
                                url = HttpContext.Current.Request.Url.ToString() + "/BannerRedirect/PriceList";
                            }
                            var Productdetails = from cl in luminous.Banners
                                                 join redirectpage in luminous.Redirectdatas on cl.RedirectPage equals redirectpage.Id

                                                 where cl.id == Bannerid
                                                 select new
                                                 {
                                                     PageName = redirectpage.PageName,

                                                     ParentCategory = cl.ParentCatid,
                                                     ProductCategory = cl.CategoryId,
                                                     ProductLevelOne = cl.ProductLevelOne
                                                 };
                            if (Productdetails.Count() > 0)
                            {
                                foreach (var prddetails in Productdetails)
                                {
                                    RedirectPage rp = new RedirectPage();
                                    rp.PageName = prddetails.PageName;
                                    rp.ProductLevelThree = "0";
                                    rp.ParentCategory = prddetails.ParentCategory.ToString();
                                    rp.ProductCategory = prddetails.ProductCategory.ToString();
                                    rp.ProductLevelOne = prddetails.ProductLevelOne.ToString();
                                    rp.month = DateTime.Now.Month;
                                    rp.year = DateTime.Now.Year;
                                    rp.ProductLevelTwo = "0";
                                    rp.ProductLevelThree = "0";
                                    rp.Media = "0";
                                    rp.Message = "Success";
                                    getpagedata.Add(rp);

                                    if (rp.ParentCategory == "1" && rp.ProductCategory == "8" && rp.ProductLevelOne == "18")
                                    {
                                        specificpage = "INBT_Battery_Inverlast";
                                    }
                                    if (rp.ParentCategory == "1" && rp.ProductCategory == "8" && rp.ProductLevelOne == "19")
                                    {
                                        specificpage = "INBT_Battery_Electra";
                                    }
                                    if (rp.ParentCategory == "1" && rp.ProductCategory == "9" && rp.ProductLevelOne == "25")
                                    {
                                        specificpage = "INBT_HomeUPS_Inverlast";
                                    }
                                    if (rp.ParentCategory == "1" && rp.ProductCategory == "9" && rp.ProductLevelOne == "26")
                                    {
                                        specificpage = "INBT_HomeUPS_Electra";
                                    }
                                    if (rp.ParentCategory == "1" && rp.ProductCategory == "15" && rp.ProductLevelOne == "37")
                                    {
                                        specificpage = "INBT_HKVA_HKVA";
                                    }
                                }

                                string RequestParameter = "UserID :" + userid + ",BannerID :" + Bannerid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid + ",Redirect from Banner to " + page.PageName + "_" + specificpage + "", url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                            }
                            else
                            {
                                RedirectPage rp = new RedirectPage();
                                rp.PageName = "0";
                                rp.ProductLevelThree = "0";
                                rp.ParentCategory = "0";
                                rp.ProductCategory = "0";
                                rp.ProductLevelOne = "0";
                                rp.month = 0;
                                rp.year = 0;
                                rp.ProductLevelTwo = "0";
                                rp.ProductLevelThree = "0";
                                rp.Media = "0";
                                rp.Message = "Failure";
                                getpagedata.Add(rp);
                                string RequestParameter = "UserID :" + userid + ",BannerID :" + Bannerid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid + ",Redirect from Banner to " + page.PageName, url, RequestParameter, ResponseParameter, 1, "Failure Scheme/PriceList", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                        }

                        if (page.PageName == "Media")
                        {
                            url = HttpContext.Current.Request.Url.ToString() + "/BannerRedirect/GetProductVideoList";

                            var Productdetails = from cl in luminous.Banners
                                                 join redirectpage in luminous.Redirectdatas on cl.RedirectPage equals redirectpage.Id

                                                 where cl.id == Bannerid
                                                 select new
                                                 {
                                                     PageName = redirectpage.PageName,

                                                     ParentCategory = cl.ParentCatid,
                                                     Media = cl.Media
                                                 };
                            if (Productdetails.Count() > 0)
                            {
                                foreach (var prddetails in Productdetails)
                                {
                                    RedirectPage rp = new RedirectPage();
                                    rp.PageName = prddetails.PageName;
                                    rp.ProductLevelThree = "0";
                                    rp.ParentCategory = prddetails.ParentCategory.ToString();
                                    rp.ProductCategory = "0";
                                    rp.ProductLevelOne = "0";
                                    rp.month = 0;
                                    rp.year = 0;
                                    rp.ProductLevelTwo = "0";
                                    rp.ProductLevelThree = "0";
                                    rp.Media = prddetails.Media.ToString();
                                    rp.Message = "Success";
                                    getpagedata.Add(rp);

                                    if (rp.ParentCategory == "1" && rp.Media == "4")
                                    {
                                        specificpage = "INBT_TVC";
                                    }
                                    if (rp.ParentCategory == "1" && rp.Media == "5")
                                    {
                                        specificpage = "INBT_ProductVideos";
                                    }
                                    if (rp.ParentCategory == "1" && rp.Media == "6")
                                    {
                                        specificpage = "INBT_Testimonials";
                                    }

                                }
                                string RequestParameter = "UserID :" + userid + ",BannerID :" + Bannerid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid + ",Redirect from Banner to " + page.PageName + "_" + specificpage + "", url, RequestParameter, ResponseParameter, 0, "Success Media", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                            else
                            {
                                RedirectPage rp = new RedirectPage();
                                rp.PageName = "0";
                                rp.ProductLevelThree = "0";
                                rp.ParentCategory = "0";
                                rp.ProductCategory = "0";
                                rp.ProductLevelOne = "0";
                                rp.month = 0;
                                rp.year = 0;
                                rp.ProductLevelTwo = "0";
                                rp.ProductLevelThree = "0";
                                rp.Media = "0";
                                rp.Message = "Failure";
                                getpagedata.Add(rp);
                                string RequestParameter = "UserID :" + userid + ",BannerID :" + Bannerid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid + ",Redirect from Banner to " + page.PageName, url, RequestParameter, ResponseParameter, 1, "Failure Media", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                        }
                        if (page.PageName == "No Redirection")
                        {

                            var NoRedirect = from cl in luminous.Banners
                                             join redirectpage in luminous.Redirectdatas on cl.RedirectPage equals redirectpage.Id

                                             where cl.id == Bannerid
                                             select new
                                             {
                                                 PageName = redirectpage.PageName,

                                                 ParentCategory = cl.ParentCatid,
                                                 Media = cl.Media
                                             };
                            if (NoRedirect.Count() > 0)
                            {
                                foreach (var prddetails in NoRedirect)
                                {
                                    RedirectPage rp = new RedirectPage();
                                    rp.PageName = prddetails.PageName;
                                    rp.ProductLevelThree = "0";
                                    rp.ParentCategory = prddetails.ParentCategory.ToString();
                                    rp.ProductCategory = "0";
                                    rp.ProductLevelOne = "0";
                                    rp.month = 0;
                                    rp.year = 0;
                                    rp.ProductLevelTwo = "0";
                                    rp.ProductLevelThree = "0";
                                    rp.Media = prddetails.Media.ToString();
                                    rp.Message = "Success";
                                    getpagedata.Add(rp);

                                }
                                string RequestParameter = "UserID :" + userid + ",BannerID :" + Bannerid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success No redirection", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                            else
                            {
                                RedirectPage rp = new RedirectPage();
                                rp.PageName = "0";
                                rp.ProductLevelThree = "0";
                                rp.ParentCategory = "0";
                                rp.ProductCategory = "0";
                                rp.ProductLevelOne = "0";
                                rp.month = 0;
                                rp.year = 0;
                                rp.ProductLevelTwo = "0";
                                rp.ProductLevelThree = "0";
                                rp.Media = "0";
                                rp.Message = "Failure";
                                getpagedata.Add(rp);
                                string RequestParameter = "UserID :" + userid + ",BannerID :" + Bannerid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Failure No Redirection", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                        }


                    }
                    else
                    {
                        RedirectPage rp = new RedirectPage();

                        rp.Status = "1";
                        rp.Message = "User already logged in three devices";
                        getpagedata.Add(rp);
                        string RequestParameter = "UserID :" + userid + ",BannerID :" + Bannerid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    RedirectPage rp = new RedirectPage();
                    rp.PageName = "0";
                    rp.ProductLevelThree = "0";
                    rp.ParentCategory = "0";
                    rp.ProductCategory = "0";
                    rp.ProductLevelOne = "0";
                    rp.month = 0;
                    rp.year = 0;
                    rp.ProductLevelTwo = "0";
                    rp.ProductLevelThree = "0";
                    rp.Media = "0";
                    rp.Message = "Unauthorized Access";
                    getpagedata.Add(rp);
                    string RequestParameter = "UserID :" + userid + ",BannerID :" + Bannerid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                RedirectPage rp = new RedirectPage();
                rp.PageName = "0";
                rp.ProductLevelThree = "0";
                rp.ParentCategory = "0";
                rp.ProductCategory = "0";
                rp.ProductLevelOne = "0";
                rp.month = 0;
                rp.year = 0;
                rp.ProductLevelTwo = "0";
                rp.ProductLevelThree = "0";
                rp.Media = "0";
                rp.Message = "Some exception has occurred";
                getpagedata.Add(rp);

                string RequestParameter = "UserID :" + userid + ",BannerId :" + Bannerid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getpagedata;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<RedirectPage> LuminousUpdatesRedirectPageData(int lumupdateid, string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<RedirectPage> getpagedata = new List<RedirectPage>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = "";
            string specificpage = "";
            url = HttpContext.Current.Request.Url.ToString() + "/LuminousUpdatesRedirectPageData";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        //  DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                        //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();
                        var pagename = from cl in luminous.luminious_Update
                                       join redirectpage in luminous.Redirectdatas on cl.RedirectPage equals redirectpage.Id
                                       where cl.Id == lumupdateid
                                       select new
                                       {
                                           PageName = redirectpage.PageName
                                       };

                        var page = pagename.SingleOrDefault();
                        if (page.PageName == "Product Details")
                        {
                            url = HttpContext.Current.Request.Url.ToString() + "/Newsfeeds/GetProductLevelFourByNameVersion2";
                            var Productdetails = from cl in luminous.luminious_Update
                                                 join redirectpage in luminous.Redirectdatas on cl.RedirectPage equals redirectpage.Id
                                                 join prdlevelthree in luminous.ProductLevelThrees on cl.ProductLevelThreeId equals prdlevelthree.id
                                                 where cl.Id == lumupdateid
                                                 select new
                                                 {
                                                     PageName = redirectpage.PageName,

                                                     Productlevelthree = prdlevelthree.Name,
                                                 };
                            if (Productdetails.Count() > 0)
                            {
                                foreach (var prddetails in Productdetails)
                                {
                                    RedirectPage rp = new RedirectPage();
                                    rp.PageName = prddetails.PageName;
                                    rp.ProductLevelThree = prddetails.Productlevelthree;
                                    rp.ParentCategory = "0";
                                    rp.ProductCategory = "0";
                                    rp.ProductLevelOne = "0";
                                    rp.ProductLevelTwo = "0";

                                    rp.Media = "0";
                                    rp.month = 0;
                                    rp.year = 0;
                                    rp.Message = "Success";

                                    getpagedata.Add(rp);
                                    specificpage = prddetails.Productlevelthree;

                                }
                                string RequestParameter = "UserID :" + userid + ",LuminousUpdateID :" + lumupdateid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid + ",Redirect from News Feeds to " + page.PageName + "_" + specificpage + "", url, RequestParameter, ResponseParameter, 0, "Success Product Details", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                            else
                            {
                                RedirectPage rp = new RedirectPage();
                                rp.PageName = "0";
                                rp.ProductLevelThree = "0";
                                rp.ParentCategory = "0";
                                rp.ProductCategory = "0";
                                rp.ProductLevelOne = "0";
                                rp.ProductLevelTwo = "0";
                                rp.ProductLevelThree = "0";
                                rp.Media = "0";
                                rp.month = 0;
                                rp.year = 0;
                                rp.Message = "Failure";
                                getpagedata.Add(rp);
                                string RequestParameter = "UserID :" + userid + ",LuminousUpdateID :" + lumupdateid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid + ",Redirect from News Feeds to " + page.PageName, url, RequestParameter, ResponseParameter, 1, "Failure Product Details ", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }

                        }

                        if (page.PageName == "Scheme" || page.PageName == "Price List")
                        {
                            if (page.PageName == "Scheme")
                            {
                                url = HttpContext.Current.Request.Url.ToString() + "/Newsfeeds/Scheme";
                            }
                            if (page.PageName == "Price List")
                            {
                                url = HttpContext.Current.Request.Url.ToString() + "/Newsfeeds/PriceList";
                            }


                            var Productdetails = from cl in luminous.luminious_Update
                                                 join redirectpage in luminous.Redirectdatas on cl.RedirectPage equals redirectpage.Id

                                                 where cl.Id == lumupdateid
                                                 select new
                                                 {
                                                     PageName = redirectpage.PageName,

                                                     ParentCategory = cl.ParentCatid,
                                                     ProductCategory = cl.CategoryId,
                                                     ProductLevelOne = cl.ProductLevelOne
                                                 };
                            if (Productdetails.Count() > 0)
                            {
                                foreach (var prddetails in Productdetails)
                                {
                                    RedirectPage rp = new RedirectPage();
                                    rp.PageName = prddetails.PageName;
                                    rp.ProductLevelThree = "0";
                                    rp.ParentCategory = prddetails.ParentCategory.ToString();
                                    rp.ProductCategory = prddetails.ProductCategory.ToString();
                                    rp.ProductLevelOne = prddetails.ProductLevelOne.ToString();
                                    rp.month = DateTime.Now.Month;
                                    rp.year = DateTime.Now.Year;
                                    rp.ProductLevelTwo = "0";
                                    rp.ProductLevelThree = "0";
                                    rp.Media = "0";
                                    rp.Message = "Success";
                                    getpagedata.Add(rp);

                                    if (rp.ParentCategory == "1" && rp.ProductCategory == "8" && rp.ProductLevelOne == "18")
                                    {
                                        specificpage = "INBT_Battery_Inverlast";
                                    }
                                    if (rp.ParentCategory == "1" && rp.ProductCategory == "8" && rp.ProductLevelOne == "19")
                                    {
                                        specificpage = "INBT_Battery_Electra";
                                    }
                                    if (rp.ParentCategory == "1" && rp.ProductCategory == "9" && rp.ProductLevelOne == "25")
                                    {
                                        specificpage = "INBT_HomeUPS_Inverlast";
                                    }
                                    if (rp.ParentCategory == "1" && rp.ProductCategory == "9" && rp.ProductLevelOne == "26")
                                    {
                                        specificpage = "INBT_HomeUPS_Electra";
                                    }
                                    if (rp.ParentCategory == "1" && rp.ProductCategory == "15" && rp.ProductLevelOne == "37")
                                    {
                                        specificpage = "INBT_HKVA_HKVA";
                                    }
                                }
                                string RequestParameter = "UserID :" + userid + ",LuminousUpdateID :" + lumupdateid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid + ",Redirect from News Feeds to " + page.PageName + "_" + specificpage + "", url, RequestParameter, ResponseParameter, 0, "Success Scheme/Price List", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                            else
                            {
                                RedirectPage rp = new RedirectPage();
                                rp.PageName = "0";
                                rp.ProductLevelThree = "0";
                                rp.ParentCategory = "0";
                                rp.ProductCategory = "0";
                                rp.ProductLevelOne = "0";
                                rp.month = 0;
                                rp.year = 0;
                                rp.ProductLevelTwo = "0";
                                rp.ProductLevelThree = "0";
                                rp.Media = "0";
                                rp.Message = "Failure";
                                getpagedata.Add(rp);
                                string RequestParameter = "UserID :" + userid + ",LuminousUpdateID :" + lumupdateid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid + ",Redirect from News Feeds to " + page.PageName, url, RequestParameter, ResponseParameter, 1, "Failure Scheme/Price List", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                        }

                        if (page.PageName == "Media")
                        {
                            url = HttpContext.Current.Request.Url.ToString() + "/Newsfeeds/GetProductVideoList";
                            var Productdetails = from cl in luminous.luminious_Update
                                                 join redirectpage in luminous.Redirectdatas on cl.RedirectPage equals redirectpage.Id

                                                 where cl.Id == lumupdateid
                                                 select new
                                                 {
                                                     PageName = redirectpage.PageName,

                                                     ParentCategory = cl.ParentCatid,
                                                     Media = cl.Media
                                                 };
                            if (Productdetails.Count() > 0)
                            {
                                foreach (var prddetails in Productdetails)
                                {
                                    RedirectPage rp = new RedirectPage();
                                    rp.PageName = prddetails.PageName;
                                    rp.ProductLevelThree = "0";
                                    rp.ParentCategory = prddetails.ParentCategory.ToString();
                                    rp.ProductCategory = "0";
                                    rp.ProductLevelOne = "0";
                                    rp.month = 0;
                                    rp.year = 0;
                                    rp.ProductLevelTwo = "0";
                                    rp.ProductLevelThree = "0";
                                    rp.Media = prddetails.Media.ToString();
                                    rp.Message = "Success";
                                    getpagedata.Add(rp);

                                    if (rp.ParentCategory == "1" && rp.Media == "4")
                                    {
                                        specificpage = "INBT_TVC";
                                    }
                                    if (rp.ParentCategory == "1" && rp.Media == "5")
                                    {
                                        specificpage = "INBT_ProductVideos";
                                    }
                                    if (rp.ParentCategory == "1" && rp.Media == "6")
                                    {
                                        specificpage = "INBT_Testimonials";
                                    }

                                }
                                string RequestParameter = "UserID :" + userid + ",LuminousUpdateID :" + lumupdateid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid + ",Redirect from News Feeds to " + page.PageName + "_" + specificpage + "", url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                            else
                            {
                                RedirectPage rp = new RedirectPage();
                                rp.PageName = "0";
                                rp.ProductLevelThree = "0";
                                rp.ParentCategory = "0";
                                rp.ProductCategory = "0";
                                rp.ProductLevelOne = "0";
                                rp.month = 0;
                                rp.year = 0;
                                rp.ProductLevelTwo = "0";
                                rp.ProductLevelThree = "0";
                                rp.Media = "0";
                                rp.Message = "Failure";
                                getpagedata.Add(rp);
                                string RequestParameter = "UserID :" + userid + ",LuminousUpdateID :" + lumupdateid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid + ",Redirect from News Feeds to " + page.PageName, url, RequestParameter, ResponseParameter, 1, "Failure Media", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                        }

                        if (page.PageName == "No Redirection")
                        {
                            var Productdetails = from cl in luminous.luminious_Update
                                                 join redirectpage in luminous.Redirectdatas on cl.RedirectPage equals redirectpage.Id

                                                 where cl.Id == lumupdateid
                                                 select new
                                                 {
                                                     PageName = redirectpage.PageName,

                                                     ParentCategory = cl.ParentCatid,
                                                     Media = cl.Media
                                                 };
                            if (Productdetails.Count() > 0)
                            {
                                foreach (var prddetails in Productdetails)
                                {
                                    RedirectPage rp = new RedirectPage();
                                    rp.PageName = prddetails.PageName;
                                    rp.ProductLevelThree = "0";
                                    rp.ParentCategory = "0";
                                    rp.ProductCategory = "0";
                                    rp.ProductLevelOne = "0";
                                    rp.month = 0;
                                    rp.year = 0;
                                    rp.ProductLevelTwo = "0";
                                    rp.ProductLevelThree = "0";
                                    rp.Media = prddetails.Media.ToString();
                                    rp.Message = "Success";
                                    getpagedata.Add(rp);

                                }
                                string RequestParameter = "UserID :" + userid + ",LuminousUpdateID :" + lumupdateid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success No Redirection", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                            else
                            {
                                RedirectPage rp = new RedirectPage();
                                rp.PageName = "0";
                                rp.ProductLevelThree = "0";
                                rp.ParentCategory = "0";
                                rp.ProductCategory = "0";
                                rp.ProductLevelOne = "0";
                                rp.month = 0;
                                rp.year = 0;
                                rp.ProductLevelTwo = "0";
                                rp.ProductLevelThree = "0";
                                rp.Media = "0";
                                rp.Message = "Failure";
                                getpagedata.Add(rp);
                                string RequestParameter = "UserID :" + userid + ",LuminousUpdateID :" + lumupdateid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Failure No Redirection", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                        }

                    }
                    else
                    {
                        RedirectPage rp = new RedirectPage();

                        rp.Status = "1";
                        rp.Message = "User already logged in three devices";
                        getpagedata.Add(rp);
                        string RequestParameter = "UserID :" + userid + ",LuminousUpdateID :" + lumupdateid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    RedirectPage rp = new RedirectPage();
                    rp.PageName = "0";
                    rp.ProductLevelThree = "0";
                    rp.ParentCategory = "0";
                    rp.ProductCategory = "0";
                    rp.ProductLevelOne = "0";
                    rp.month = 0;
                    rp.year = 0;
                    rp.ProductLevelTwo = "0";
                    rp.ProductLevelThree = "0";
                    rp.Media = "0";
                    rp.Message = "Unauthorized Access";
                    getpagedata.Add(rp);
                    string RequestParameter = "UserID :" + userid + ",LuminousUpdateID :" + lumupdateid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                RedirectPage rp = new RedirectPage();
                rp.PageName = "0";
                rp.ProductLevelThree = "0";
                rp.ParentCategory = "0";
                rp.ProductCategory = "0";
                rp.ProductLevelOne = "0";
                rp.month = 0;
                rp.year = 0;
                rp.ProductLevelTwo = "0";
                rp.ProductLevelThree = "0";
                rp.Media = "0";
                rp.Message = "Some exception has occurred";
                getpagedata.Add(rp);

                string RequestParameter = "UserID :" + userid + ",LuminousUpdateID :" + lumupdateid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "PageName,ProductLevelThree,ParentCategory,ProductCategory,ProductLevelOne,month,year,ProductLevelTwo,ProductLevelThree,Media,Message";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getpagedata;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<Gallery> GetGalleryData(string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<Gallery> getgallerydata = new List<Gallery>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetGalleryData";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        // DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                        //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();

                        var lastthreemonth = DateTime.Now.AddMonths(-3);
                        //var data = db.Galleries.Where(c => c.CreatedOn.Value.Month >= lastthreemonth && c.CreatedOn.Value.Month <= currectmonth);
                        var getdata = from x in luminous.Galleries
                                      where x.CreatedOn > lastthreemonth && x.Status != 0 && x.Status != 2
                                      orderby x.Id ascending
                                      select x;


                        if (getdata.Count() > 0)
                        {
                            foreach (var list in getdata)
                            {
                                Gallery glry = new Gallery();
                                glry.ImageName = Url + "Gallery/" + list.ImageName.ToString();
                                glry.date = list.CreatedOn.Value.Date;
                                glry.Message = "Success";
                                getgallerydata.Add(glry);
                            }
                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "ImageName,date,Message";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                        else
                        {
                            Gallery glry = new Gallery();
                            glry.ImageName = "0";
                            glry.date = null;
                            glry.Message = "No Data Exist";
                            getgallerydata.Add(glry);
                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "ImageName,date,Message";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                    }
                    else
                    {
                        Gallery glry = new Gallery();
                        glry.ImageName = "0";
                        glry.date = null;
                        glry.Status = "1";
                        glry.Message = "User already logged in three devices";
                        getgallerydata.Add(glry);
                    }
                }
                else
                {
                    Gallery glry = new Gallery();
                    glry.ImageName = "0";
                    glry.date = null;
                    glry.Message = "Unauthorized Access";
                    getgallerydata.Add(glry);
                    string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "ImageName,date,Message";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                Gallery glry = new Gallery();
                glry.ImageName = "0";
                glry.date = null;
                glry.Message = "Some exception has occurred";
                getgallerydata.Add(glry);
                string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "ImageName,date,Message";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getgallerydata;
        }

        [WebMethod]
        //  [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public void CountSalesConnectVisit(string userid, string pagename, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            // List<Gallery> getgallerydata = new List<Gallery>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = "";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        // DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                        //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();
                        if (pagename == "Connect Entry")
                        {
                            url = HttpContext.Current.Request.Url.ToString() + "/CountSalesConnectVisit/ConnectEntry";
                        }
                        if (pagename == "Connect Report")
                        {
                            url = HttpContext.Current.Request.Url.ToString() + "/CountSalesConnectVisit/ConnectReport";
                        }
                        if (pagename == "Sales Entry")
                        {
                            url = HttpContext.Current.Request.Url.ToString() + "/CountSalesConnectVisit/SalesEntry";
                        }
                        if (pagename == "Sales Report")
                        {
                            url = HttpContext.Current.Request.Url.ToString() + "/CountSalesConnectVisit/SalesReport";
                        }
                        if (pagename == "Mailbox")
                        {
                            url = HttpContext.Current.Request.Url.ToString() + "/TapMailbox/mailbox";
                        }




                        string RequestParameter = "UserID :" + userid + ",PageName:" + pagename + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Success";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);


                    }

                }
                else
                {

                    string RequestParameter = "UserID :" + userid + ",PageName:" + pagename + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {

                string RequestParameter = "UserID :" + userid + ",PageName:" + pagename + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "ImageName,date,Message";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }

        }


        [WebMethod]
        public List<State> GetState()
        {
            List<State> ObjGetState = new List<State>();
            try
            {
                LuminousEntities luminous = new LuminousEntities();

                //using (con = new SqlConnection(Connstring))
                //{

                //    con.Open();
                //cmd = new SqlCommand("select st.StateID,st.StateName  from state st join users urs on urs.State= st.StateID where urs.EmployeeCode='" + employeecode + "'", con);

                var statedata = luminous.allstates.Select(c => new { c.id, c.statename }).ToList();

                // SqlCommand cmd = new SqlCommand("select StateID,StateName  from state", con);
                /// DataTable dt = new DataTable();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                // da.Fill(dt);

                foreach (var data in statedata)
                {
                    State st = new State();
                    st.StateID = data.id;
                    st.StateName = data.statename;
                    ObjGetState.Add(st);
                }

                return ObjGetState;
                // }
            }
            catch (Exception exc)
            {
                State st = new State();
                st.StateID = 0;
                st.StateName = "0";
                ObjGetState.Add(st);
                return ObjGetState;
            }
            /*Add New Code*/


        }
        [WebMethod]
        public List<Town> GetTown(string StateId)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<Town> ObjGetTown = new List<Town>();
            try
            {

                int stateid = Convert.ToInt32(StateId);
                var citydata = luminous.cities.Where(c => c.stateid == stateid).Select(c => new { c.id, c.cityname }).ToList();

                foreach (var data in citydata)
                {
                    Town town = new Town();
                    town.TownID = data.id;
                    town.TownName = data.cityname;
                    ObjGetTown.Add(town);

                }

                return ObjGetTown;
            }
            catch (Exception exc)
            {
                Town town = new Town();
                town.TownID = 0;
                town.TownName = "0";
                ObjGetTown.Add(town);
                return ObjGetTown;
                //List<Town> resmes = new List<Town>();
                //ResultMessage resmessage = new ResultMessage { Message = "Some Exception has occured. Please try again later" };

                //resmes.Add(resmessage);
                // return null;
            }


        }
        [WebMethod]
        public List<ParentCategoryData> GetParentCategoryData()
        {
            LuminousEntities luminous = new LuminousEntities();
            List<ParentCategoryData> ObjGetParentCatData = new List<ParentCategoryData>();
            try
            {


                var Parentcategorydata = luminous.ParentCategories.Where(c => c.PCStatus == 1).Select(c => new { c.Pcid, c.PCName }).ToList();

                foreach (var data in Parentcategorydata)
                {
                    ParentCategoryData pcat = new ParentCategoryData();
                    pcat.Parentcatid = data.Pcid;
                    pcat.Parentcatname = data.PCName;
                    ObjGetParentCatData.Add(pcat);

                }

                return ObjGetParentCatData;
            }
            catch (Exception exc)
            {
                ParentCategoryData pcat = new ParentCategoryData();
                pcat.Parentcatid = 0;
                pcat.Parentcatname = "0";
                ObjGetParentCatData.Add(pcat);
                return ObjGetParentCatData;
                //List<Town> resmes = new List<Town>();
                //ResultMessage resmessage = new ResultMessage { Message = "Some Exception has occured. Please try again later" };

                //resmes.Add(resmessage);
                // return null;
            }


        }


        [WebMethod]

        public void App_Exception(string ID, string exception)
        {
            SaveServiceLog(ID, "APPException", ID, "", 1, exception.ToString(), ID, DateTime.Now, "", "", "", "");
        }




        [WebMethod]
        public List<WRS_Date> WRS_Date()
        {
            List<WRS_Date> wrsdt = new List<WRS_Date>();

            try
            {
                LuminousEntities luminous = new LuminousEntities();

                var getwrsdate = luminous.WRS_Datecheck.SingleOrDefault();
                WRS_Date dt = new WRS_Date();

                dt.Newdate = Convert.ToDateTime(getwrsdate.NewDate);
                dt.Olddate = Convert.ToDateTime(getwrsdate.OldDate);
                dt.Maxdate = Convert.ToDateTime(getwrsdate.Maxdate);
                dt.Mindate = Convert.ToDateTime(getwrsdate.Mindate);
                wrsdt.Add(dt);


            }
            catch (Exception exc)
            {
                WRS_Date dt = new WRS_Date();

                dt.Newdate = null;
                dt.Olddate = null;
                dt.Maxdate = null;
                dt.Olddate = null;
                wrsdt.Add(dt);
            }
            return wrsdt;
        }

        // Add Connect Assist for logging ticket by Ravi on 26-06-2018

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        //public string SaveTicket(string userid, string attachmentname, Byte[] attachment, string serialno, string description, string status, string distcode, string distname, string custname, string custmobile, string address, string state, string city, string dateofsale, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        //{
        //    LuminousEntities luminous = new LuminousEntities();

        //    string url = HttpContext.Current.Request.Url.ToString() + "/SaveTicket";
        //    string Filename = "";
        //    try
        //    {
        //        string LoginToken = "";
        //        string TokenString = userid + Appversion;
        //        using (MD5 md5Hash = MD5.Create())
        //        {

        //            LoginToken = GetMd5Hash(md5Hash, TokenString);


        //        }
        //        if (LoginToken == token)
        //        {

        //            var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
        //            if (checkstatus.Count() == 0)
        //            {
        //                if (attachmentname.ToString() != null && attachmentname.ToString() != "")
        //                {
        //                    Filename = Path.GetFileNameWithoutExtension(attachmentname) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(attachmentname);
        //                    string str = Path.Combine(Server.MapPath("~/ConnectAssist/"), Filename);
        //                    BinaryWriter bw = new BinaryWriter(new FileStream(str, FileMode.Create, FileAccess.Write));
        //                    bw.Write(attachment);
        //                    bw.Close();
        //                }
        //                else
        //                {
        //                    attachmentname = "";
        //                    Filename = "";
        //                }
        //                if (status != "")
        //                {
        //                    if (status == "O")
        //                    {
        //                        status = "Open";
        //                    }

        //                }
        //                luminous.AddToConnectAssists(new ConnectAssist
        //                {

        //                    Userid = userid,
        //                    Srno = serialno,
        //                    Flag = status,
        //                    DistCode = distcode,
        //                    DistName = distname,
        //                    CustName = custname,
        //                    CustMobile = custmobile,
        //                    Adderss = address,
        //                    State = state,
        //                    City = city,
        //                    DateOfSale = Convert.ToDateTime(dateofsale),
        //                    CreatedOn = DateTime.Now,
        //                    CreatedBy = userid.ToString()
        //                });
        //                luminous.SaveChanges();
        //                int connectassist_id = luminous.ConnectAssists.Select(c => c.Id).Max();
        //                // int id = (from record in luminous.ConnectAssists orderby record.Id select record.Id).Last();
        //                luminous.AddToMapConnectAssist_Comments(new MapConnectAssist_Comments
        //                {
        //                    ConnectAssistId = connectassist_id,
        //                    Description = description,
        //                    CreatedOn = DateTime.Now,
        //                    Attachment = Filename,
        //                    filename = attachmentname,
        //                    CreatedBy = userid.ToString()


        //                }


        //                    );

        //                luminous.SaveChanges();

        //                string RequestParameter = "UserId :" + userid + ",AttachmentName :" + attachmentname + ",SerialNo :" + serialno + ",Description :" + description + ",Status :" + status + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
        //                string ResponseParameter = "Inserted Successfully";

        //                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Inserted Successfully", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
        //            }
        //            else
        //            {
        //                return "User already logged in three devices";
        //            }
        //        }
        //        else
        //        {
        //            string RequestParameter = "UserId :" + userid + ",AttachmentName :" + attachmentname + ",SerialNo :" + serialno + ",Description :" + description + ",Status :" + status + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
        //            string ResponseParameter = "Unauthorized Access";

        //            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
        //            return "Unauthorized Access";
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        string RequestParameter = "UserId :" + userid + ",AttachmentName :" + attachmentname + ",SerialNo :" + serialno + ",Description :" + description + ",Status :" + status + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
        //        string ResponseParameter = "Some exception has occurred";

        //        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
        //        return "Some exception has occurred";
        //    }
        //    return "Inserted Successfully";
        //}


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<GetTicket> GetTicketList(string userid, string month, string year, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {

            int M = 0, Y = 0;
            if (month != null || year != "")
            {
                M = Convert.ToInt32(month);
                Y = Convert.ToInt32(year);
            }
            LuminousEntities luminous = new LuminousEntities();
            List<GetTicket> getticketdata = new List<GetTicket>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetTicketList";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        if (M != 0 && Y != 0)
                        {
                            var getdata = from x in luminous.ConnectAssists
                                          where x.CreatedOn.Value.Month == M && x.CreatedOn.Value.Year == Y && x.Userid == userid
                                          orderby x.Id ascending
                                          select x;


                            if (getdata.Count() > 0)
                            {
                                foreach (var list in getdata)
                                {
                                    GetTicket gt = new GetTicket();
                                    gt.Date = list.CreatedOn;
                                    gt.serialno = list.Srno;

                                    if (list.Flag == "Resolved")
                                    {
                                        var resolveddate = luminous.ConnectAssists.Where(c => c.Id == list.Id).Select(c => c.ResolvedDate).SingleOrDefault();
                                        var date = resolveddate.Value.AddDays(7).Date;
                                        if (date == DateTime.Today)
                                        {
                                            luminous.ExecuteStoreCommand("Update ConnectAssist set Flag='Closed' where Id='" + list.Id + "' ");
                                        }
                                        gt.status = list.Flag;

                                    }
                                    else
                                    {
                                        gt.status = list.Flag;
                                    }

                                    gt.Id = list.Id;
                                    // Gallery glry = new Gallery();
                                    // glry.ImageName = Url + "Gallery/" + list.ImageName.ToString();
                                    // glry.date = list.CreatedOn.Value.Date;
                                    gt.message = "Success";

                                    getticketdata.Add(gt);
                                }
                                getticketdata.Sort();
                                string RequestParameter = "UserID :" + userid + ",Month :" + month + ",Year=" + year + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Message:Success";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }

                            else
                            {

                                GetTicket gt = new GetTicket();
                                gt.Date = null;
                                gt.serialno = "0";
                                gt.status = "0";
                                gt.Id = 0;
                                gt.message = "No Data Exist";
                                getticketdata.Add(gt);

                                string RequestParameter = "UserID :" + userid + ",Month :" + month + ",Year=" + year + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Message:No Data Exist";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                        }
                    }
                    else
                    {
                        GetTicket gt = new GetTicket();
                        gt.Date = null;
                        gt.serialno = "0";
                        gt.status = "0";
                        gt.Id = 0;
                        gt.message = "User already logged in three devices";
                        getticketdata.Add(gt);
                        string RequestParameter = "UserID :" + userid + ",Month :" + month + ",Year=" + year + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Message:No Data Exist";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                    }
                }
                else
                {
                    GetTicket gt = new GetTicket();
                    gt.Date = null;
                    gt.serialno = "0";
                    gt.status = "0";
                    gt.Id = 0;
                    gt.message = "Unauthorized Access";
                    getticketdata.Add(gt);


                    string RequestParameter = "UserID :" + userid + ",Month :" + month + ",Year=" + year + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {

                GetTicket gt = new GetTicket();
                gt.Date = null;
                gt.serialno = "0";
                gt.status = "0";
                gt.Id = 0;
                gt.message = "Some exception has occurred";
                getticketdata.Add(gt);

                string RequestParameter = "UserID :" + userid + ",Month :" + month + ",Year=" + year + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Message:Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getticketdata;
        }


        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public List<GetTicket> ViewTicket(string userid, int ticketid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        //{


        //    LuminousEntities luminous = new LuminousEntities();
        //    List<GetTicket> getticketdata = new List<GetTicket>();
        //    //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
        //    string url = HttpContext.Current.Request.Url.ToString() + "/ViewTicket";
        //    try
        //    {
        //        string LoginToken = "";
        //        string TokenString = userid + Appversion;
        //        using (MD5 md5Hash = MD5.Create())
        //        {

        //            LoginToken = GetMd5Hash(md5Hash, TokenString);


        //        }
        //        if (LoginToken == token)
        //        {
        //            var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
        //            if (checkstatus.Count() == 0)
        //            {



        //                var getdata = (from x in luminous.ConnectAssists
        //                               join m in luminous.MapConnectAssist_Comments on x.Id equals m.ConnectAssistId
        //                               join u in luminous.UsersLists on x.Userid equals u.UserId
        //                               where x.Id == ticketid
        //                               select new
        //                               {
        //                                   id = x.Id,
        //                                   date = m.CreatedOn,
        //                                   text = m.Description,
        //                                   createdby = u.Dis_Name,
        //                                   attachment = m.Attachment,
        //                                   serialno = x.Srno,
        //                                   Status = x.Flag,
        //                                   DistCode = x.DistCode,
        //                                   DistName = x.DistName,
        //                                   CustName = x.CustName,
        //                                   CustMobile = x.CustMobile,
        //                                   Address = x.Adderss,
        //                                   City = x.City,
        //                                   State = x.State,
        //                                   DateofSale = x.DateOfSale
        //                               }).OrderBy(c => c.id).ToList();


        //                if (getdata.Count() > 0)
        //                {
        //                    foreach (var list in getdata)
        //                    {
        //                        GetTicket gt = new GetTicket();
        //                        gt.Date = list.date;
        //                        gt.serialno = list.serialno;
        //                        gt.distcode = list.DistCode;
        //                        gt.distname = list.DistName;
        //                        gt.custname = list.CustName;
        //                        gt.custmobile = list.CustMobile;
        //                        gt.state = list.State;
        //                        gt.city = list.City;
        //                        gt.dateofsale = list.DateofSale;
        //                        gt.attachment = Url + "ConnectAssist/" + list.attachment;
        //                        gt.createdby = list.createdby;
        //                        gt.status = list.Status;


        //                        // Gallery glry = new Gallery();
        //                        // glry.ImageName = Url + "Gallery/" + list.ImageName.ToString();
        //                        // glry.date = list.CreatedOn.Value.Date;
        //                        gt.Description = list.text;
        //                        gt.message = "Success";
        //                        getticketdata.Add(gt);
        //                    }
        //                    string RequestParameter = "UserID :" + userid + ",Ticketid :" + ticketid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
        //                    string ResponseParameter = "Message:Success";

        //                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
        //                }

        //                else
        //                {

        //                    GetTicket gt = new GetTicket();
        //                    gt.Date = null;
        //                    gt.serialno = "0";
        //                    gt.attachment = "0";
        //                    gt.Description = "0";
        //                    gt.status = "0";
        //                    gt.message = "No Data Exist";
        //                    getticketdata.Add(gt);

        //                    string RequestParameter = "UserID :" + userid + ",Ticketid :" + ticketid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
        //                    string ResponseParameter = "Message:No Data Exist";

        //                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
        //                }
        //            }
        //            else
        //            {
        //                GetTicket gt = new GetTicket();
        //                gt.Date = null;
        //                gt.serialno = "0";
        //                gt.attachment = "0";
        //                gt.Description = "0";
        //                gt.status = "0";
        //                gt.message = "User already logged in three devices";
        //                getticketdata.Add(gt);
        //                string RequestParameter = "UserID :" + userid + ",Ticketid :" + ticketid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
        //                string ResponseParameter = "Message:No Data Exist";

        //                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

        //            }
        //        }
        //        else
        //        {
        //            GetTicket gt = new GetTicket();
        //            gt.Date = null;
        //            gt.serialno = "0";
        //            gt.attachment = "0";
        //            gt.Description = "0";
        //            gt.status = "0";
        //            gt.message = "Unauthorized Access";
        //            getticketdata.Add(gt);


        //            string RequestParameter = "UserID :" + userid + ",Ticketid :" + ticketid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
        //            string ResponseParameter = "Message:Unauthorized Access";

        //            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
        //        }

        //    }
        //    catch (Exception exc)
        //    {

        //        GetTicket gt = new GetTicket();
        //        gt.Date = null;
        //        gt.serialno = "0";
        //        gt.attachment = "0";
        //        gt.Description = "0";
        //        gt.status = "0";
        //        gt.message = "Some exception has occurred";
        //        getticketdata.Add(gt);

        //        string RequestParameter = "UserID :" + userid + ",Ticketid :" + ticketid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
        //        string ResponseParameter = "Message:Some exception has occurred";

        //        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
        //    }
        //    return getticketdata;
        //}

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public string UpdateTicket(string userid, int ticketid, string attachmentname, Byte[] attachment, string description, string status, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();

            string url = HttpContext.Current.Request.Url.ToString() + "/UpdateTicket";
            string Filename = "";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {

                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        if (attachmentname.ToString() != null && attachmentname.ToString() != "")
                        {
                            Filename = Path.GetFileNameWithoutExtension(attachmentname) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(attachmentname);
                            string str = Path.Combine(Server.MapPath("~/ConnectAssist/"), Filename);
                            BinaryWriter bw = new BinaryWriter(new FileStream(str, FileMode.Create, FileAccess.Write));
                            bw.Write(attachment);
                            bw.Close();
                        }
                        else
                        {
                            attachmentname = "";
                            Filename = "";
                        }
                        if (status != "")
                        {
                            if (status == "O")
                            {
                                status = "Open";
                            }
                            if (status == "P")
                            {
                                status = "InProcess";
                            }
                            if (status == "R")
                            {
                                status = "Resolved";
                            }
                            if (status == "C")
                            {
                                status = "Closed";
                            }

                        }
                        var ticketdata = luminous.ConnectAssists.SingleOrDefault(c => c.Id == ticketid);

                        ticketdata.Flag = status;



                        //luminous.AddToConnectAssists(new ConnectAssist
                        //{

                        //    Userid = userid,
                        //   // Srno = serialno,

                        //    Attachment = Filename,
                        //    Flag = status,
                        //    Filename = attachmentname,
                        //    CreatedOn = DateTime.Now,
                        //    CreatedBy = userid.ToString()
                        //});

                        luminous.SaveChanges();
                        //  int connectassist_id = luminous.ConnectAssists.Select(c => c.Id).Max();

                        luminous.AddToMapConnectAssist_Comments(new MapConnectAssist_Comments
                        {
                            ConnectAssistId = ticketid,
                            Description = description,
                            CreatedOn = DateTime.Now,
                            Attachment = Filename,
                            filename = attachmentname,
                            CreatedBy = userid.ToString()


                        }


                            );

                        luminous.SaveChanges();



                        string RequestParameter = "UserId :" + userid + ",AttachmentName :" + attachmentname + ",TicketId :" + ticketid + ",Description :" + description + ",Status :" + status + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Updated Successfully";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Updated Successfully", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                    else
                    {
                        return "User already logged in three devices";
                    }
                }
                else
                {
                    string RequestParameter = "UserId :" + userid + ",AttachmentName :" + attachmentname + ",TicketId :" + ticketid + ",Description :" + description + ",Status :" + status + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    return "Unauthorized Access";
                }
            }
            catch (Exception exc)
            {
                string RequestParameter = "UserId :" + userid + ",AttachmentName :" + attachmentname + ",TicketId :" + ticketid + ",Description :" + description + ",Status :" + status + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                return "Some exception has occurred";
            }
            return "Updated Successfully";
        }
        //End Connect Assist //

        //New Change for contest pitcure//

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<contestpitcure> GetContestPictureFlag(string date, string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<contestpitcure> bImage = new List<contestpitcure>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/getContestPitcureFlag";
            try
            {
                var getappversion = luminous.AppVersions.Where(c => c.Version == Appversion).ToList();
                if (getappversion.Count > 0)
                {
                    string LoginToken = "";
                    string TokenString = userid + Appversion;
                    DateTime curr_date = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                    using (MD5 md5Hash = MD5.Create())
                    {
                        LoginToken = GetMd5Hash(md5Hash, TokenString);
                    }

                    if (LoginToken == token)
                    {
                        var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                        if (checkstatus.Count() == 0)
                        {
                            var contesttab = luminous.ContestPicture_Tab.Where(c => c.Flag == true && c.DealerId == userid).Select(c => new { c.DealerId, c.Flag }).ToList();
                            if (contesttab.Count() != 0)
                            {

                                var Marqueetext = luminous.ShowMarquee_Text.Where(a => a.Statrtdate <= curr_date && a.Enddate >= curr_date && a.Status == 1).ToList();
                                if (Marqueetext.Count() > 0)
                                {


                                    foreach (var tab in contesttab)
                                    {

                                        if (tab.DealerId == userid.ToString())
                                        {
                                            foreach (var list in Marqueetext)
                                            {
                                                contestpitcure bi = new contestpitcure();
                                                bi.DealerID = tab.DealerId;
                                                bi.Flag = tab.Flag;
                                                bi.Marquee = list.Marqueetext;
                                                bi.MarqueeFlag = list.MarqueeFlag;
                                                bi.MarqueeId = list.Id;
                                                bi.Message = "Success";
                                                bImage.Add(bi);
                                            }
                                        }
                                    }

                                    string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                    string ResponseParameter = "Message:Success";
                                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                                }
                                else
                                {
                                    contestpitcure bi = new contestpitcure();
                                    bi.Flag = false;
                                    bi.Marquee = "0";
                                    bi.DealerID = "0";
                                    bi.Message = "No Event Exist";
                                    bImage.Add(bi);
                                    string RequestParameter = "UserID :" + userid + ",Date=" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                    string ResponseParameter = "Message:No Event Exist";
                                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Event Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                                }

                            }
                            else
                            {

                                contestpitcure bi = new contestpitcure();
                                bi.Flag = false;
                                bi.Marquee = "0";
                                bi.DealerID = "0";
                                bi.Message = "No Data Exist";
                                bImage.Add(bi);
                                string RequestParameter = "UserID :" + userid + ",Date=" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Message:No Data Exist";
                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                        }
                        else
                        {
                            contestpitcure bi = new contestpitcure();
                            bi.Flag = false;
                            bi.Marquee = "0";
                            bi.DealerID = "0";
                            bi.Message = "User already logged in three devices";
                            bImage.Add(bi);
                            string RequestParameter = "UserID :" + userid + ",Date=" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "User already logged in three devices";
                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        }
                    }
                    else
                    {
                        contestpitcure bi = new contestpitcure();
                        bi.Flag = false;
                        bi.Marquee = "0";
                        bi.DealerID = "0";
                        bi.Message = "Unauthorized Access";
                        bImage.Add(bi);
                        string RequestParameter = "UserID :" + userid + ",Date=" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Message:Unauthorized Access";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    contestpitcure bi = new contestpitcure();
                    bi.Flag = false;
                    bi.Marquee = "0";
                    bi.DealerID = "0";
                    bi.Message = "Please update your app version";
                    bImage.Add(bi);

                    string RequestParameter = "UserID :" + userid + ",Date=" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Message:Some exception has occurred";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Please update your app version", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {

                contestpitcure bi = new contestpitcure();
                bi.Flag = false;
                bi.Marquee = "0";
                bi.DealerID = "0";
                bi.Message = "Some exception has occurred";
                bImage.Add(bi);

                string RequestParameter = "UserID :" + userid + ",Date=" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Message:Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return bImage;
        }


        //End contest pitcure//


        //get distributor name  by distributor//

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<getnamebydistcode> GetNameBySapcode(string distcode, string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<getnamebydistcode> getdistname = new List<getnamebydistcode>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetNameBySapcode";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {
                    LoginToken = GetMd5Hash(md5Hash, TokenString);
                }

                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        var getDistcode = luminous.UsersLists.Where(c => c.UserId == distcode).Select(c => new { c.Dis_Name, c.CustomerType }).ToList();

                        if (getDistcode.Count() != 0)
                        {
                            foreach (var data in getDistcode)
                            {
                                if (data.CustomerType == "DISTY")
                                {
                                    getnamebydistcode getname = new getnamebydistcode();
                                    getname.Distname = data.Dis_Name;
                                    getname.Message = "Success";
                                    getdistname.Add(getname);
                                }
                                else
                                {
                                    getnamebydistcode getname = new getnamebydistcode();
                                    getname.Distname = "0";
                                    getname.Message = "Please enter correct distributor code";
                                    getdistname.Add(getname);
                                }
                            }

                            string RequestParameter = "UserID :" + userid + ",distcode :" + distcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Message:Success";
                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);


                        }
                        else
                        {

                            getnamebydistcode getname = new getnamebydistcode();
                            getname.Distname = " ";
                            getname.Message = "No data exist";
                            getdistname.Add(getname);
                            string RequestParameter = "UserID :" + userid + ",distcode :" + distcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Message:No Data Exist";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        }
                    }
                    else
                    {
                        getnamebydistcode getname = new getnamebydistcode();
                        getname.Distname = "0";
                        getname.Message = "User already logged in three devices";
                        getdistname.Add(getname);
                        string RequestParameter = "UserID :" + userid + ",distcode :" + distcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                    }
                }
                else
                {
                    getnamebydistcode getname = new getnamebydistcode();
                    getname.Distname = "0";
                    getname.Message = "Unauthorized Access";
                    getdistname.Add(getname);
                    string RequestParameter = "UserID :" + userid + ",distcode=" + distcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                getnamebydistcode getname = new getnamebydistcode();
                getname.Distname = "0";
                getname.Message = "Some exception has occurred";
                getdistname.Add(getname);

                string RequestParameter = "UserID :" + userid + ",distcode=" + distcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Message:Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getdistname;
        }


        //End distributor name  by distributor//

        //Save dealer contest image//

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public string SaveDelContestPicture(string userid, string distcode, string imagename1, string imagename2, Byte[] filename1, Byte[] filename2, int marqueeid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();

            string url = HttpContext.Current.Request.Url.ToString() + "/SaveDelContestPicture";
            string Filename1 = "";
            string Filename2 = "";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {

                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        if ((imagename1.ToString() != null && imagename1.ToString() != "") || (imagename2.ToString() != null && imagename2.ToString() != ""))
                        {

                            if (filename1.Count() > 0)
                            {

                                string dealerimage1 = imagename1 + "_" + userid + "_" + marqueeid + "_";


                                Filename1 = Path.GetFileNameWithoutExtension(dealerimage1) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(imagename1);
                                string str = Path.Combine(Server.MapPath("~/ContestImage/"), Filename1);
                                BinaryWriter bw = new BinaryWriter(new FileStream(str, FileMode.Create, FileAccess.Write));
                                bw.Write(filename1);
                                bw.Close();


                                luminous.AddToSaveDelContestImages(new SaveDelContestImage
                                {
                                    DealerCode = userid,
                                    DistributorCode = distcode,
                                    CreatedOn = DateTime.Now,
                                    ImageName = Filename1,
                                    Status = 1,
                                    CreatedBy = userid.ToString(),
                                    Contestid = 2,
                                    Marqueeid = marqueeid,
                                    PollFlag = "No",
                                    PollSubmitFlag = "No"

                                }


                              );

                                luminous.SaveChanges();
                            }

                            if (filename2.Count() > 0)
                            {
                                string dealerimage2 = imagename2 + "_" + userid + "_" + marqueeid + "_";

                                Filename2 = Path.GetFileNameWithoutExtension(dealerimage2) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(imagename2);
                                string str = Path.Combine(Server.MapPath("~/ContestImage/"), Filename2);
                                BinaryWriter bw = new BinaryWriter(new FileStream(str, FileMode.Create, FileAccess.Write));
                                bw.Write(filename2);
                                bw.Close();


                                luminous.AddToSaveDelContestImages(new SaveDelContestImage
                                {
                                    DealerCode = userid,
                                    DistributorCode = distcode,
                                    CreatedOn = DateTime.Now,
                                    ImageName = Filename2,
                                    Status = 1,
                                    CreatedBy = userid.ToString(),
                                    Contestid = 2,
                                    Marqueeid = marqueeid,
                                    PollFlag = "No",
                                    PollSubmitFlag = "No"

                                }


                              );

                                luminous.SaveChanges();
                            }

                        }
                        else
                        {
                            imagename1 = "";
                            Filename2 = "";
                        }




                        string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",DistCode :" + distcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Inserted Successfully";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Inserted Successfully", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                    else
                    {
                        return "User already logged in three devices";
                    }
                }
                else
                {
                    string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",DistCode :" + distcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    return "Unauthorized Access";
                }
            }
            catch (Exception exc)
            {
                string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",DistCode :" + distcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                return "Some exception has occurred";
            }
            return "Inserted Successfully";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public string SaveDelContestPictureGeoTag(string userid, string distcode, string imagename1, string imagename2, Byte[] filename1, Byte[] filename2, int marqueeid, string latitude, string longitude, string location, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();

            string url = HttpContext.Current.Request.Url.ToString() + "/SaveDelContestPicture_test";
            string Filename1 = "";
            string Filename2 = "";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {

                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        if ((imagename1.ToString() != null && imagename1.ToString() != "") || (imagename2.ToString() != null && imagename2.ToString() != ""))
                        {

                            if (filename1.Count() > 0)
                            {




                                string dealerimage1 = imagename1 + "_" + userid + "_" + marqueeid + "_";


                                Filename1 = Path.GetFileNameWithoutExtension(dealerimage1) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(imagename1);
                                string str = Path.Combine(Server.MapPath("~/ContestImage/"), Filename1);
                                BinaryWriter bw = new BinaryWriter(new FileStream(str, FileMode.Create, FileAccess.Write));
                                bw.Write(filename1);
                                bw.Close();


                                luminous.AddToSaveDelContestImages(new SaveDelContestImage
                                {
                                    DealerCode = userid,
                                    DistributorCode = distcode,
                                    CreatedOn = DateTime.Now,
                                    ImageName = Filename1,
                                    Status = 1,
                                    CreatedBy = userid.ToString(),
                                    Contestid = 2,
                                    Marqueeid = marqueeid,
                                    PollFlag = "No",
                                    PollSubmitFlag = "No",
                                    Latitude = latitude,
                                    Longitude = longitude,
                                    Location = location

                                }


                              );

                                luminous.SaveChanges();
                            }

                            if (filename2.Count() > 0)
                            {




                                string dealerimage2 = imagename2 + "_" + userid + "_" + marqueeid + "_";

                                Filename2 = Path.GetFileNameWithoutExtension(dealerimage2) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(imagename2);
                                string str = Path.Combine(Server.MapPath("~/ContestImage/"), Filename2);
                                BinaryWriter bw = new BinaryWriter(new FileStream(str, FileMode.Create, FileAccess.Write));
                                bw.Write(filename2);
                                bw.Close();


                                luminous.AddToSaveDelContestImages(new SaveDelContestImage
                                {
                                    DealerCode = userid,
                                    DistributorCode = distcode,
                                    CreatedOn = DateTime.Now,
                                    ImageName = Filename2,
                                    Status = 1,
                                    CreatedBy = userid.ToString(),
                                    Contestid = 2,
                                    Marqueeid = marqueeid,
                                    PollFlag = "No",
                                    PollSubmitFlag = "No",
                                    Latitude = latitude,
                                    Longitude = longitude,
                                    Location = location

                                }


                              );

                                luminous.SaveChanges();
                            }

                        }
                        else
                        {
                            imagename1 = "";
                            Filename2 = "";
                        }




                        string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",DistCode :" + distcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Inserted Successfully";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Inserted Successfully", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                    else
                    {
                        return "User already logged in three devices";
                    }
                }
                else
                {
                    string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",DistCode :" + distcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    return "Unauthorized Access";
                }
            }
            catch (Exception exc)
            {
                string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",DistCode :" + distcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                return "Some exception has occurred";
            }
            return "Inserted Successfully";
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<getnamebydistcode> GetDealerImageCount(string userid, int marqueeid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            List<getnamebydistcode> imagcountdata = new List<getnamebydistcode>();
            LuminousEntities luminous = new LuminousEntities();

            string url = HttpContext.Current.Request.Url.ToString() + "/GetDealerImageCount";
            string Filename = "";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {

                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {

                        //var imgcount = luminous.SaveDelContestImages.Where(c => c.DealerCode == userid && c.Marqueeid == marqueeid).Select(c => new { c.PollFlag, c.PollSubmitFlag, c.DistributorCode }).ToList();

                        var imgcount = luminous.SaveDelContestImages.Where(c => c.DealerCode == userid && c.Marqueeid <= marqueeid).Select(c => new { c.PollFlag, c.PollSubmitFlag, c.DistributorCode }).ToList();
                        var imgcountprevious = luminous.ExecuteStoreQuery<imagecountdealer>("select max(marqueeid) as maxdata,count(*) as countdata from savedelcontestimages where dealercode='" + userid + "'");

                        int maxmrqid = 0;
                        int imgcountdata = 0;
                        foreach (var d in imgcountprevious)
                        {
                            maxmrqid = Convert.ToInt32(d.maxdata);
                            imgcountdata = d.countdata;
                        }
                        //var data_Count_Max = luminous.ExecuteStoreQuer>("select max(marqueeid) as maxdata,count(*) as countdata from savedelcontestimages where dealercode='990000000'");
                        // var imgcountprevious = luminous.SaveDelContestImages.Where(c => c.DealerCode == userid && c.Marqueeid< marqueeid).Count();

                        if (imgcountdata >= 2)
                        {
                            // imgcountdata = 3;

                            foreach (var datacount in imgcount)
                            {
                                var getDistname = luminous.UsersLists.Where(c => c.UserId == datacount.DistributorCode).Select(c => new { c.Dis_Name, c.CustomerType }).SingleOrDefault();
                                if (datacount.PollFlag == "Yes" && datacount.PollSubmitFlag == "Yes" && marqueeid == maxmrqid)
                                {
                                    getnamebydistcode dc = new getnamebydistcode();
                                    dc.imagescount = "2";
                                    dc.Distcode = datacount.DistributorCode;
                                    dc.Distname = getDistname.Dis_Name;
                                    dc.quespollflag = "Yes";
                                    dc.quessubmitflag = "Yes";
                                    imagcountdata.Add(dc);
                                }
                                else if (datacount.PollFlag == "Yes" && datacount.PollSubmitFlag == "Yes" && marqueeid > maxmrqid)
                                {
                                    getnamebydistcode dc = new getnamebydistcode();
                                    dc.imagescount = "3";
                                    dc.Distcode = datacount.DistributorCode;
                                    dc.Distname = getDistname.Dis_Name;
                                    dc.quespollflag = "Yes";
                                    dc.quessubmitflag = "Yes";
                                    imagcountdata.Add(dc);

                                }
                                else if (datacount.PollFlag == "Yes" && datacount.PollSubmitFlag == "No" && marqueeid > maxmrqid)
                                {
                                    getnamebydistcode dc = new getnamebydistcode();
                                    dc.imagescount = "3";
                                    dc.Distcode = datacount.DistributorCode;
                                    dc.Distname = getDistname.Dis_Name;
                                    dc.quespollflag = "Yes";
                                    dc.quessubmitflag = "Yes";
                                    imagcountdata.Add(dc);
                                    luminous.ExecuteStoreCommand("Delete SaveDelContestImages where DealerCode='" + userid + "' and Marqueeid='" + maxmrqid + "'");

                                }
                                else if (datacount.PollFlag == "Yes" && datacount.PollSubmitFlag == "No" && marqueeid == maxmrqid)
                                {
                                    getnamebydistcode dc = new getnamebydistcode();
                                    dc.imagescount = "0";
                                    dc.Distcode = datacount.DistributorCode;
                                    dc.Distname = getDistname.Dis_Name;
                                    dc.quespollflag = "Yes";
                                    dc.quessubmitflag = "No";
                                    imagcountdata.Add(dc);
                                    luminous.ExecuteStoreCommand("Delete SaveDelContestImages where DealerCode='" + userid + "' and Marqueeid='" + maxmrqid + "'");
                                }
                                else if (datacount.PollFlag == "No" && datacount.PollSubmitFlag == "No" && marqueeid == maxmrqid)
                                {
                                    getnamebydistcode dc = new getnamebydistcode();
                                    dc.imagescount = "2";
                                    dc.Distcode = datacount.DistributorCode;
                                    dc.Distname = getDistname.Dis_Name;
                                    dc.quespollflag = "No";
                                    dc.quessubmitflag = "No";
                                    imagcountdata.Add(dc);
                                }
                                else if (datacount.PollFlag == "No" && datacount.PollSubmitFlag == "No" && marqueeid > maxmrqid)
                                {
                                    getnamebydistcode dc = new getnamebydistcode();
                                    dc.imagescount = "3";
                                    dc.Distcode = datacount.DistributorCode;
                                    dc.Distname = getDistname.Dis_Name;
                                    dc.quespollflag = "Yes";
                                    dc.quessubmitflag = "Yes";
                                    imagcountdata.Add(dc);

                                }
                                break;
                            }

                        }


                        if (imgcount.Count == 0)
                        {
                            getnamebydistcode dc = new getnamebydistcode();
                            dc.imagescount = "0";
                            dc.Distcode = "0";
                            dc.Distname = "0";
                            dc.quespollflag = "0";
                            dc.quessubmitflag = "0";
                            imagcountdata.Add(dc);

                        }






                        string RequestParameter = "UserId :" + userid + ",Marqueeid :" + marqueeid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Success";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                    else
                    {
                        getnamebydistcode dc = new getnamebydistcode();
                        dc.imagescount = "5";
                        dc.Distcode = "0";
                        dc.Distname = "0";
                        dc.Message = "User already logged in three devices";
                        imagcountdata.Add(dc);


                    }
                }
                else
                {
                    getnamebydistcode dc = new getnamebydistcode();
                    dc.imagescount = "5";
                    dc.Distcode = "0";
                    dc.Distname = "0";
                    dc.Message = "Unauthorized Access";
                    imagcountdata.Add(dc);
                    string RequestParameter = "UserId :" + userid + ",Marqueeid :" + marqueeid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Unauthorized Access";



                }
            }
            catch (Exception exc)
            {
                getnamebydistcode dc = new getnamebydistcode();
                dc.imagescount = "5";
                dc.Distcode = "0";
                dc.Distname = "0";
                dc.Message = "Some exception has occurred";
                imagcountdata.Add(dc);
                string RequestParameter = "UserId :" + userid + ",Marqueeid :" + marqueeid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);


            }
            return imagcountdata;
        }


        //Save Poll Question//
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public string SavePollQuestion(string userid, string questionid, string optionvalue, string question2, string option2, string question3, string option3, string question4, string option4, string marqueeid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            string abc = "";
            string url = HttpContext.Current.Request.Url.ToString() + "/SavePollQuestion";
            string Filename = "";
            try
            {
                string LoginToken = "";


                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {

                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        if (questionid != "" && optionvalue != "")
                        {

                            luminous.AddToSaveNotificationSurveys(new SaveNotificationSurvey
                            {

                                SurveyID = Convert.ToInt32(questionid),
                                UserId = userid.ToString(),

                                OptionValue = optionvalue,
                                CreatedOn = DateTime.Now,
                                // stat = 1,
                                CreatedBy = userid.ToString(),
                                ContestId = 2,
                                Marqueeid = Convert.ToInt32(marqueeid)

                            });
                            try
                            {
                                luminous.SaveChanges();
                                //  abc = "inserted successfully123";
                                // return abc;
                            }
                            catch (Exception exc)
                            {
                                abc = exc.InnerException.ToString();
                                // return abc;
                            }

                        }
                        if (question2 != "" && option2 != "")
                        {

                            luminous.AddToSaveNotificationSurveys(new SaveNotificationSurvey
                            {

                                SurveyID = Convert.ToInt32(question2),
                                UserId = userid.ToString(),

                                OptionValue = option2,
                                CreatedOn = DateTime.Now,
                                // stat = 1,
                                CreatedBy = userid.ToString(),
                                ContestId = 2,
                                Marqueeid = Convert.ToInt32(marqueeid)

                            });
                            try
                            {
                                luminous.SaveChanges();
                                //  abc = "inserted successfully123";
                                // return abc;
                            }
                            catch (Exception exc)
                            {
                                abc = exc.InnerException.ToString();
                                // return abc;
                            }

                        }
                        if (question3 != "" && option3 != "")
                        {

                            luminous.AddToSaveNotificationSurveys(new SaveNotificationSurvey
                            {

                                SurveyID = Convert.ToInt32(question3),
                                UserId = userid.ToString(),

                                OptionValue = option3,
                                CreatedOn = DateTime.Now,
                                // stat = 1,
                                CreatedBy = userid.ToString(),
                                ContestId = 2,
                                Marqueeid = Convert.ToInt32(marqueeid)

                            });
                            try
                            {
                                luminous.SaveChanges();
                                //  abc = "inserted successfully123";
                                // return abc;
                            }
                            catch (Exception exc)
                            {
                                abc = exc.InnerException.ToString();
                                // return abc;
                            }


                        }
                        if (question4 != "" && option4 != "")
                        {

                            luminous.AddToSaveNotificationSurveys(new SaveNotificationSurvey
                            {

                                SurveyID = Convert.ToInt32(question4),
                                UserId = userid.ToString(),

                                OptionValue = option4,
                                CreatedOn = DateTime.Now,
                                // stat = 1,
                                CreatedBy = userid.ToString(),
                                ContestId = 2,
                                Marqueeid = Convert.ToInt32(marqueeid)

                            });
                            try
                            {
                                luminous.SaveChanges();
                                //  abc = "inserted successfully123";
                                // return abc;
                            }
                            catch (Exception exc)
                            {
                                abc = exc.InnerException.ToString();
                                // return abc;
                            }


                        }

                        abc = "inserted successfully";

                        if (marqueeid.ToString() != "")
                        {
                            int marqid = Convert.ToInt32(marqueeid);
                            var imgdata = luminous.SaveDelContestImages.Where(c => c.DealerCode == userid && c.Marqueeid == marqid).ToList();
                            if (imgdata.Count > 0)
                            {
                                luminous.ExecuteStoreCommand("update SaveDelContestImages set PollSubmitFlag='Yes' where DealerCode='" + userid + "'and Marqueeid='" + marqueeid + "'");
                            }
                        }




                        string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Inserted Successfully";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Inserted Successfully", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        // abc = "Inserted Succesfully123";
                    }
                    else
                    {

                        return "User already logged in three devices";
                    }
                }
                else
                {
                    abc = "Unauthorized Access";
                    string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    // return "Unauthorized Access";
                }
            }
            catch (Exception exc)
            {
                abc = "Some exception has occurred";
                string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                // return "Some exception has occurred";
            }
            return abc;
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<PollQuestion> GetContestPollQuestion(string date, string userid, string marqueeid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<PollQuestion> getQueslist = new List<PollQuestion>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetContestPollQuestion";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                        //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();

                        //var data = from cl in luminous.NotificationSurveys
                        //           where (
                        //               from i in luminous.SaveNotificationSurveys
                        //               where i.SurveyID == cl.SurveyID && i.UserId == userid
                        //               select i).Count() == 0
                        //           select new { SurveyID = cl.SurveyID, Survey = cl.Survey, StartDate = cl.StartDate, Enddate = cl.Enddate };

                        var Polllist = from cdata in luminous.NotificationSurveys
                                       where cdata.StartDate <= dateExist && cdata.Enddate >= dateExist && cdata.ContestId == 2
                                       select new
                                       {
                                           QuestionId = cdata.SurveyID,
                                           Question = cdata.QuestionTitle,
                                           OptionA = cdata.OptionA,
                                           OptionB = cdata.OptionB,
                                           OptionC = cdata.OptionC,
                                           OptionD = cdata.OptionD,

                                       };
                        if (Polllist.Count() > 0)
                        {
                            foreach (var list in Polllist)
                            {
                                PollQuestion pq = new PollQuestion();

                                pq.QuestionId = list.QuestionId;
                                pq.Question = list.Question;
                                pq.OptionA = list.OptionA;
                                pq.OptionB = list.OptionB;
                                pq.OptionC = list.OptionC;
                                pq.OptionD = list.OptionD;
                                pq.CreatedBy = userid.ToString();
                                pq.CreatedOn = DateTime.Now;
                                pq.Message = "Success";
                                getQueslist.Add(pq);

                            }
                            if (marqueeid.ToString() != "")
                            {
                                int marqid = Convert.ToInt32(marqueeid);
                                var imgdata = luminous.SaveDelContestImages.Where(c => c.DealerCode == userid && c.Marqueeid == marqid).ToList();
                                if (imgdata.Count > 0)
                                {
                                    luminous.ExecuteStoreCommand("update SaveDelContestImages set PollFlag='Yes',PollSubmitFlag='No' where DealerCode='" + userid + "'and Marqueeid='" + marqueeid + "'");
                                }
                            }


                            string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "SurveyId,Survey,Message";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                        else
                        {
                            PollQuestion pq = new PollQuestion();
                            pq.Question = "0";
                            pq.OptionA = "0";
                            pq.OptionB = "0";
                            pq.OptionC = "0";
                            pq.OptionD = "0";
                            pq.CreatedBy = userid.ToString();
                            pq.CreatedOn = DateTime.Now;
                            pq.Message = "No Data Exist";
                            getQueslist.Add(pq);

                            if (marqueeid != "")
                            {
                                int marqid = Convert.ToInt32(marqueeid);
                                var imgdata = luminous.SaveDelContestImages.Where(c => c.DealerCode == userid && c.Marqueeid == marqid).ToList();
                                if (imgdata.Count > 0)
                                {
                                    luminous.ExecuteStoreCommand("update SaveDelContestImages set PollFlag='No',PollSubmitFlag='No' where DealerCode='" + userid + "'and Marqueeid='" + marqueeid + "'");
                                }
                            }
                            string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "SurveyId,Survey,Message";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                    }
                    else
                    {
                        PollQuestion pq = new PollQuestion();
                        pq.Question = "0";
                        pq.OptionA = "0";
                        pq.OptionB = "0";
                        pq.OptionC = "0";
                        pq.OptionD = "0";
                        pq.CreatedBy = userid.ToString();
                        pq.CreatedOn = DateTime.Now;
                        pq.Message = "User already logged in three devices";
                        getQueslist.Add(pq);
                        string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "SurveyId,Survey,Message";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    PollQuestion pq = new PollQuestion();
                    pq.Question = "0";
                    pq.OptionA = "0";
                    pq.OptionB = "0";
                    pq.OptionC = "0";
                    pq.OptionD = "0";
                    pq.CreatedBy = userid.ToString();
                    pq.CreatedOn = DateTime.Now;
                    pq.Message = "Unauthorized Access";
                    getQueslist.Add(pq);
                    string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "SurveyId,Survey,Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                PollQuestion pq = new PollQuestion();
                pq.Question = "0";
                pq.OptionA = "0";
                pq.OptionB = "0";
                pq.OptionC = "0";
                pq.OptionD = "0";
                pq.CreatedBy = userid.ToString();
                pq.CreatedOn = DateTime.Now;
                pq.Message = "Some exception has occurred";
                getQueslist.Add(pq);
                string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "SurveyId,Survey,Message";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getQueslist;
        }
        //End Poll Question//

        //Get Info date//
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<PollQuestion> GetInfo_Date(string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<PollQuestion> getdate = new List<PollQuestion>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetInfo_Date";
            try
            {
                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        //  DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                        //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();

                        //var data = from cl in luminous.NotificationSurveys
                        //           where (
                        //               from i in luminous.SaveNotificationSurveys
                        //               where i.SurveyID == cl.SurveyID && i.UserId == userid
                        //               select i).Count() == 0
                        //           select new { SurveyID = cl.SurveyID, Survey = cl.Survey, StartDate = cl.StartDate, Enddate = cl.Enddate };
                        var getDate = luminous.SaveDelContestImages.Where(c => c.DealerCode == userid).ToList().Select(r => r.CreatedOn.Value.Date).Distinct();
                        //var getDate = luminous.SaveDelContestImages.Where(c => c.CreatedBy == userid).Select(c => c.CreatedOn).ToList();

                        //var getDate = luminous.SaveDelContestImages.Where(c => c.CreatedBy == userid).Select(c => new { c.CreatedOn.Value.Date }).Distinct().ToList();
                        if (getDate.Count() > 0)
                        {
                            foreach (var list in getDate)
                            {
                                PollQuestion pq = new PollQuestion();

                                pq.date = list.Date.ToString("dd-MM-yyyy");
                                pq.Message = "Success";
                                getdate.Add(pq);
                            }
                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "SurveyId,Survey,Message";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                        else
                        {
                            PollQuestion pq = new PollQuestion();
                            pq.date = "0";
                            pq.Message = "No Data Exist";
                            getdate.Add(pq);
                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "SurveyId,Survey,Message";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                    }
                    else
                    {
                        PollQuestion pq = new PollQuestion();
                        pq.date = "0";
                        pq.Message = "User already logged in three devices";
                        getdate.Add(pq);
                        string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "SurveyId,Survey,Message";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    PollQuestion pq = new PollQuestion();
                    pq.date = "0";
                    pq.Message = "Unauthorized Access";
                    getdate.Add(pq);
                    string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";

                    string ResponseParameter = "SurveyId,Survey,Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                PollQuestion pq = new PollQuestion();
                pq.date = "0";
                pq.Message = "Some exception has occurred";
                getdate.Add(pq);
                string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "SurveyId,Survey,Message";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getdate;
        }


        //End info date//

        //Get GetContestInfoData//
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<Getcontestinfo_data> GetContestInfodata(string userid, string date, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            string dd = "";
            int count = 0;
            LuminousEntities luminous = new LuminousEntities();
            List<Getcontestinfo_data> getinfodata = new List<Getcontestinfo_data>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetContestInfodata";
            try
            {

                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        if (date.ToString() != "0")
                        {
                            DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                            //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();
                            dd = dateExist.ToString();
                            var infodata = (from ci in luminous.SaveDelContestImages
                                            join pq in luminous.SaveNotificationSurveys on ci.Marqueeid equals pq.Marqueeid
                                            into ps
                                            from pq in ps.DefaultIfEmpty()
                                            join cpq in luminous.NotificationSurveys on pq.SurveyID equals cpq.SurveyID

    into psq
                                            from cpq in psq.DefaultIfEmpty()
                                            where ci.CreatedOn.Value.Year == dateExist.Year && ci.CreatedOn.Value.Month == dateExist.Month && ci.CreatedOn.Value.Day == dateExist.Day && ci.Contestid == 2 && ci.DealerCode == userid
                                            select new { Id = ci.Id, Imagename = ci.ImageName, Question = cpq.QuestionTitle, OptionA = cpq.OptionA, OptionB = cpq.OptionB, OptionC = cpq.OptionC, OptionD = cpq.OptionD, Optiontext = pq.Options, Optionvalue = pq.OptionValue }).GroupBy(elem => elem.Id).Select(group => group.FirstOrDefault()).ToList();



                            if (infodata.Count() > 0)
                            {

                                foreach (var list in infodata)
                                {
                                    Getcontestinfo_data info = new Getcontestinfo_data();
                                    info.imagename = Url + "ContestImage/" + list.Imagename;

                                    if (list.Question == "" || list.Question == null)
                                    {
                                        info.Question = "0";
                                    }
                                    else
                                    {
                                        info.Question = list.Question;

                                    }
                                    if (list.Optionvalue == "" || list.Optionvalue == null)
                                    {
                                        info.CorrectAns = "0";
                                    }
                                    else
                                    {
                                        info.CorrectAns = list.Optionvalue;

                                    }

                                    //info.OptionA = list.OptionA;
                                    //info.OptionB = list.OptionB;
                                    //info.OptionC = list.OptionC;
                                    //info.OptionD = list.OptionD;

                                    info.Message = "Success";
                                    getinfodata.Add(info);
                                }
                                string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "SurveyId,Survey,Message";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                            }
                            else
                            {
                                Getcontestinfo_data info = new Getcontestinfo_data();
                                info.imagename = "0";
                                info.Question = "0";
                                //info.OptionA = "0";
                                //info.OptionB = "0";
                                //info.OptionC = "0";
                                //info.OptionD = "0";
                                info.CorrectAns = "0";
                                info.Message = "No Data Exist";
                                getinfodata.Add(info);
                                string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "SurveyId,Survey,Message";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                        }
                        else
                        {
                            Getcontestinfo_data info = new Getcontestinfo_data();
                            info.imagename = "0";
                            info.Question = "0";
                            //info.OptionA = "0";
                            //info.OptionB = "0";
                            //info.OptionC = "0";
                            //info.OptionD = "0";
                            info.CorrectAns = "0";
                            info.Message = "Please enroll the contest first";
                            getinfodata.Add(info);

                        }
                    }
                    else
                    {
                        Getcontestinfo_data info = new Getcontestinfo_data();
                        info.imagename = "0";
                        info.Question = "0";
                        //info.OptionA = "0";
                        //info.OptionB = "0";
                        //info.OptionC = "0";
                        //info.OptionD = "0";
                        info.CorrectAns = "0";
                        info.Message = "User already logged in three devices";
                        getinfodata.Add(info);
                        string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "SurveyId,Survey,Message";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    Getcontestinfo_data info = new Getcontestinfo_data();
                    info.imagename = "0";
                    info.Question = "0";
                    //info.OptionA = "0";
                    //info.OptionB = "0";
                    //info.OptionC = "0";
                    //info.OptionD = "0";
                    info.CorrectAns = "0";
                    info.Message = "Unauthorized Access";
                    getinfodata.Add(info);
                    string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";

                    string ResponseParameter = "SurveyId,Survey,Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                Getcontestinfo_data info = new Getcontestinfo_data();
                info.imagename = dd;
                info.Question = exc.ToString();
                //info.OptionA = "0";
                //info.OptionB = "0";
                //info.OptionC = "0";
                //info.OptionD = "0";
                info.CorrectAns = "0";
                info.Message = "Some exception has occurred";
                getinfodata.Add(info);
                string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "SurveyId,Survey,Message";

                //SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getinfodata;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<Getcontestinfo_data> GetContestInfodata_New(string userid, string date, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            string dd = "";
            int count = 0;
            LuminousEntities luminous = new LuminousEntities();
            List<Getcontestinfo_data> getinfodata = new List<Getcontestinfo_data>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetContestInfodata_New";
            try
            {

                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        if (date.ToString() != "0")
                        {
                            DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
                            //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();
                            dd = dateExist.ToString();
                            var infodata = (from ci in luminous.SaveDelContestImages

                                            where ci.CreatedOn.Value.Year == dateExist.Year && ci.CreatedOn.Value.Month == dateExist.Month && ci.CreatedOn.Value.Day == dateExist.Day && ci.Contestid == 2 && ci.DealerCode == userid
                                            select new { Id = ci.Id, Imagename = ci.ImageName }).GroupBy(elem => elem.Id).Select(group => group.FirstOrDefault()).ToList();


                            var quesdata = (from pq in luminous.SaveNotificationSurveys


                                            join cpq in luminous.NotificationSurveys on pq.SurveyID equals cpq.SurveyID



                                            where pq.CreatedOn.Value.Year == dateExist.Year && pq.CreatedOn.Value.Month == dateExist.Month && pq.CreatedOn.Value.Day == dateExist.Day && pq.ContestId == 2 && pq.CreatedBy == userid
                                            select new { Question = cpq.QuestionTitle, OptionA = cpq.OptionA, OptionB = cpq.OptionB, OptionC = cpq.OptionC, OptionD = cpq.OptionD, Optiontext = pq.Options, Optionvalue = pq.OptionValue }).ToList();




                            if (infodata.Count() > 0)
                            {

                                foreach (var list in infodata)
                                {
                                    Getcontestinfo_data infoimage = new Getcontestinfo_data();
                                    //Getcontestinfo_data info = new Getcontestinfo_data();
                                    infoimage.imagename = Url + "ContestImage/" + list.Imagename;
                                    infoimage.Message = "Success";
                                    getinfodata.Add(infoimage);
                                }
                                foreach (var list in quesdata)
                                {
                                    Getcontestinfo_data info = new Getcontestinfo_data();
                                    if (list.Question == "" || list.Question == null)
                                    {
                                        info.Question = "0";
                                    }
                                    else
                                    {
                                        info.Question = list.Question;

                                    }
                                    if (list.Optionvalue == "" || list.Optionvalue == null)
                                    {
                                        info.CorrectAns = "0";
                                    }
                                    else
                                    {
                                        info.CorrectAns = list.Optionvalue;

                                    }
                                    info.Message = "Success";
                                    getinfodata.Add(info);
                                }
                                string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "SurveyId,Survey,Message";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                            }
                            else
                            {
                                Getcontestinfo_data info = new Getcontestinfo_data();
                                info.imagename = "0";
                                info.Question = "0";
                                //info.OptionA = "0";
                                //info.OptionB = "0";
                                //info.OptionC = "0";
                                //info.OptionD = "0";
                                info.CorrectAns = "0";
                                info.Message = "No Data Exist";
                                getinfodata.Add(info);
                                string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "SurveyId,Survey,Message";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }
                        }
                        else
                        {
                            Getcontestinfo_data info = new Getcontestinfo_data();
                            info.imagename = "0";
                            info.Question = "0";
                            //info.OptionA = "0";
                            //info.OptionB = "0";
                            //info.OptionC = "0";
                            //info.OptionD = "0";
                            info.CorrectAns = "0";
                            info.Message = "Please enroll the contest first";
                            getinfodata.Add(info);

                        }
                    }
                    else
                    {
                        Getcontestinfo_data info = new Getcontestinfo_data();
                        info.imagename = "0";
                        info.Question = "0";
                        //info.OptionA = "0";
                        //info.OptionB = "0";
                        //info.OptionC = "0";
                        //info.OptionD = "0";
                        info.CorrectAns = "0";
                        info.Message = "User already logged in three devices";
                        getinfodata.Add(info);
                        string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "SurveyId,Survey,Message";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    Getcontestinfo_data info = new Getcontestinfo_data();
                    info.imagename = "0";
                    info.Question = "0";
                    //info.OptionA = "0";
                    //info.OptionB = "0";
                    //info.OptionC = "0";
                    //info.OptionD = "0";
                    info.CorrectAns = "0";
                    info.Message = "Unauthorized Access";
                    getinfodata.Add(info);
                    string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";

                    string ResponseParameter = "SurveyId,Survey,Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                Getcontestinfo_data info = new Getcontestinfo_data();
                info.imagename = dd;
                info.Question = exc.ToString();
                //info.OptionA = "0";
                //info.OptionB = "0";
                //info.OptionC = "0";
                //info.OptionD = "0";
                info.CorrectAns = "0";
                info.Message = "Some exception has occurred";
                getinfodata.Add(info);
                string RequestParameter = "UserID :" + userid + ",Date :" + date + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "SurveyId,Survey,Message";

                //SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getinfodata;
        }


        //End GetContestInfoData//


        // Add new feature Lucky 7 contest by Ravi on 15-12-2018//


        //Add new api GetDistributorCount for distcount by Ravi on 15-12-2018

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<CouponData> LSD_GetDistributorCount(string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {

            LuminousEntities luminous = new LuminousEntities();
            List<CouponData> getdisteligiblecount = new List<CouponData>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/GetDistributorCount";
            try
            {
                var getappversion = luminous.AppVersions.Where(c => c.Version == Appversion).ToList();
                if (getappversion.Count > 0)
                {
                    string LoginToken = "";
                    string TokenString = userid + Appversion;
                    using (MD5 md5Hash = MD5.Create())
                    {

                        LoginToken = GetMd5Hash(md5Hash, TokenString);


                    }
                    if (LoginToken == token)
                    {
                        var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                        if (checkstatus.Count() == 0)
                        {

                            var geteligiblecount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).ToList();



                            if (geteligiblecount.Count > 0)
                            {
                                foreach (var list in geteligiblecount)
                                {

                                    CouponData coupondata = new CouponData();
                                    coupondata.EligibleCouponCount = list.EligibleCoupon;
                                    if (list.DistActivatedCount == 0 || list.DistActivatedCount == null)
                                    {
                                        coupondata.ActivatedCouponCount = 0;
                                    }
                                    else
                                    {
                                        coupondata.ActivatedCouponCount = list.DistActivatedCount;
                                    }
                                    if (list.DistBalanceCount == 0 || list.DistBalanceCount == null)
                                    {
                                        coupondata.BalanceCouponCount = 0;
                                    }
                                    else
                                    {
                                        coupondata.BalanceCouponCount = list.DistBalanceCount;
                                    }
                                    if (list.DealerRedeemedCount == 0 || list.DealerRedeemedCount == null)
                                    {
                                        coupondata.OpenReimbursment = 0;
                                    }
                                    else
                                    {
                                        // var dealercouponsubmitted = luminous.LSD_Master.Where(c => c.RedemptionDealerCode != null && c.RedemptionDealerSecretCode != null && c.ClaimDistCode == null && c.ActivationDistCode==userid).Count();

                                        // coupondata.CouponReimbursment = list.DealerRedeemedCount;
                                        coupondata.OpenReimbursment = list.DealerRedeemedCount;
                                        // coupondata.CouponReimbursment = dealercouponsubmitted;
                                    }
                                    if (list.DistClaimedCount == 0 || list.DistClaimedCount == null)
                                    {
                                        coupondata.CouponReimbursment = 0;

                                    }
                                    else
                                    {
                                        //var DistClimsubmitted = luminous.LSD_Master.Where(c => c.RedemptionDealerCode != null && c.RedemptionDealerSecretCode != null && c.ClaimDistCode != null && c.ActivationDistCode == userid).Count();
                                        coupondata.CouponReimbursment = list.DistClaimedCount;

                                    }
                                    coupondata.Message = "Success";
                                    getdisteligiblecount.Add(coupondata);
                                }

                                string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "EligibleCount :" + getdisteligiblecount.ToString() + ",Message:Success";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                            }
                            else
                            {
                                CouponData coupondata = new CouponData();
                                coupondata.EligibleCouponCount = 0;

                                coupondata.Message = "No Data Exist";
                                getdisteligiblecount.Add(coupondata);
                                string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "EligibleCount :0,Message:No Data Exist";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            }


                        }
                        else
                        {
                            CouponData coupondata = new CouponData();
                            coupondata.EligibleCouponCount = 0;

                            coupondata.Message = "User already logged in three devices";
                            getdisteligiblecount.Add(coupondata);
                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "EligibleCount :0,Message:User already logged in three devices";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }
                    }
                    else
                    {

                        CouponData coupondata = new CouponData();
                        coupondata.EligibleCouponCount = 0;

                        coupondata.Message = "Unauthorized Access";
                        getdisteligiblecount.Add(coupondata);
                        string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";

                        string ResponseParameter = "EligibleCount :0,Message:Unauthorized Access";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {
                    CouponData coupondata = new CouponData();
                    coupondata.EligibleCouponCount = 0;

                    coupondata.Message = "Please update your app version";
                    getdisteligiblecount.Add(coupondata);
                    string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "EligibleCount :0,Message:Some exception has occurred";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Please update your app version", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }
            }
            catch (Exception exc)
            {
                CouponData coupondata = new CouponData();
                coupondata.EligibleCouponCount = 0;

                coupondata.Message = "Some exception has occurred";
                getdisteligiblecount.Add(coupondata);
                string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "EligibleCount :0,Message:Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getdisteligiblecount;
        }

        //End GetDistributorCount api//

        //Add new api LSD_SaveQrCode by ravi on 15-12-2018//

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public string LSD_SaveQrCode(string userid, string qrcode, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            string abc = "";
            int activatecount = 0;
            int balancecount = 0;
            string url = HttpContext.Current.Request.Url.ToString() + "/LSD_SaveQrCode";

            try
            {
                string LoginToken = "";


                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {

                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        var getGiftid = luminous.LSD_Master.Where(c => c.QrCode == qrcode).Select(c => c.GiftId).SingleOrDefault();
                        if (getGiftid != 0)
                        {
                            var checkcouponcount_exist = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).ToList();
                            if (checkcouponcount_exist.Count > 0)
                            {
                                var distactivatedcount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => new { c.DistActivatedCount, c.EligibleCoupon }).SingleOrDefault();

                                activatecount = Convert.ToInt32(distactivatedcount.DistActivatedCount) + 1;

                                if (activatecount <= distactivatedcount.EligibleCoupon)
                                {
                                    var matchQrcode = luminous.LSD_Master.Where(c => c.QrCode == qrcode).Count();
                                    if (matchQrcode > 0)
                                    {
                                        var checkqrcodeExist = luminous.LSD_Master.Where(c => c.ActivationDistCode == userid && c.ActivatedQrCode == qrcode).Count();

                                        if (checkqrcodeExist > 0)
                                        {

                                            string RequestParameter = "UserId :" + userid + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                            string ResponseParameter = "QR code already Activated";

                                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "QR code already Activated", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                                            return "QR code already Activated";
                                        }
                                        else
                                        {

                                            var getdistName = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.Dis_Name).SingleOrDefault();
                                            //luminous.ExecuteStoreCommand("Update LSD_Master set ActivationDistCode='" + userid + "',ActivationDistName='" + getdistName + "',ActivatedQrCode='" + qrcode + "',ActivationDistOn='" + DateTime.Now + "' where QrCode='" + qrcode + "'");
                                            luminous.UpdateActivationDistCode(userid, getdistName, qrcode, qrcode);
                                            balancecount = Convert.ToInt32(distactivatedcount.EligibleCoupon) - Convert.ToInt32(activatecount);

                                            luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set DistActivatedCount='" + activatecount + "',DistBalanceCount='" + balancecount + "' where DistCode='" + userid + "'");


                                            string RequestParameter = "UserId :" + userid + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                            string ResponseParameter = "Coupon Activated Successfully";

                                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Coupon Activated Successfully", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                                            return "Coupon Activated Successfully";


                                        }
                                    }
                                    else
                                    {
                                        string RequestParameter = "UserId :" + userid + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                        string ResponseParameter = "QR Code not valid";

                                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "QR Code not valid", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                                        return "QR Code not valid";
                                    }

                                }
                                else
                                {

                                    string RequestParameter = "UserId :" + userid + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                    string ResponseParameter = "Coupon Activation limit has been exceeded.";

                                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Coupon Activation limit has been exceeded.", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                                    return "Coupon Activation limit has been exceeded.";

                                }
                            }
                            else
                            {
                                string RequestParameter = "UserId :" + userid + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Issue with coupon.Kindly coordinate with Luminous team.";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Issue with coupon.Kindly coordinate with Luminous team.", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                                return "Issue with coupon.Kindly coordinate with Luminous team.";
                            }

                        }
                        else
                        {
                            string RequestParameter = "UserId :" + userid + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Issue with coupon.Kindly coordinate with Luminous team.";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Issue with coupon.Kindly coordinate with Luminous team.", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            return "Issue with coupon.Kindly coordinate with Luminous team.";
                        }

                        // abc = "Inserted Succesfully123";
                    }
                    else
                    {
                        string RequestParameter = "UserId :" + userid + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        return "User already logged in three devices";
                    }
                }
                else
                {
                    //abc = "Unauthorized Access";
                    string RequestParameter = "UserId :" + userid + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    return "Unauthorized Access";
                }
            }
            catch (Exception exc)
            {
                abc = "Some exception has occurred";
                string RequestParameter = "UserId :" + userid + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                // return "Some exception has occurred";
            }
            return abc;
        }


        //End LSD_SaveQrCode api//


        //Add new api LSD_GetActivatedCoupon by ravi on 15-12-2018//
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<Distdata> LSD_GetActivatedCouponReport(string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {

            LuminousEntities luminous = new LuminousEntities();
            List<Distdata> getdistdata = new List<Distdata>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/LSD_GetActivatedCoupon";
            try
            {

                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {

                        var getDistQrCode_Time = luminous.LSD_Master.Where(c => c.ActivationDistCode == userid).ToList();



                        if (getDistQrCode_Time.Count > 0)
                        {
                            foreach (var list in getDistQrCode_Time)
                            {

                                Distdata distdata = new Distdata();

                                if (list.SecretCode == "0" || list.SecretCode == null)
                                {
                                    distdata.AlphanumericCode = "0";
                                }
                                else
                                {
                                    distdata.AlphanumericCode = list.SecretCode;
                                }
                                if (list.ActivationDistOn == null)
                                {
                                    distdata.ActivatedDateTime = null;
                                }
                                else
                                {
                                    distdata.ActivatedDateTime = list.ActivationDistOn;
                                }
                                distdata.Message = "Success";

                                getdistdata.Add(distdata);
                            }

                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Message:Success";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        }
                        else
                        {
                            Distdata distdata = new Distdata();
                            distdata.AlphanumericCode = "0";
                            distdata.ActivatedDateTime = null;
                            distdata.Message = "No Data Exist";
                            getdistdata.Add(distdata);
                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "ActivatedQrCode :0,Message:No Data Exist";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }


                    }
                    else
                    {
                        Distdata distdata = new Distdata();
                        distdata.AlphanumericCode = "0";
                        distdata.ActivatedDateTime = null;
                        distdata.Message = "User already logged in three devices";
                        getdistdata.Add(distdata);
                        string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "ActivatedQrCode :0,Message:User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {

                    Distdata distdata = new Distdata();
                    distdata.AlphanumericCode = "0";
                    distdata.ActivatedDateTime = null;
                    distdata.Message = "Unauthorized Access";
                    getdistdata.Add(distdata);
                    string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";

                    string ResponseParameter = "ActivatedQrCode :0,Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                Distdata distdata = new Distdata();
                distdata.AlphanumericCode = "0";
                distdata.ActivatedDateTime = null;
                distdata.Message = "Some exception has occurred";
                getdistdata.Add(distdata);
                string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "ActivatedQrCode :0,Message:Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getdistdata;
        }

        //End  LSD_GetActivatedCoupon by ravi on 15-12-2018//


        //Add new api LSD_GetRedeemedData by ravi on 16-12-2018//
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<DealerRedeemeddata> LSD_GetRedeemedReport(string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {

            LuminousEntities luminous = new LuminousEntities();
            List<DealerRedeemeddata> getdelaerdata = new List<DealerRedeemeddata>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/LSD_GetRedeemedReport";
            try
            {

                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {

                        var getDealerReedemedData = (from c in luminous.LSD_Master
                                                     join Pi in luminous.Lsd_GiftMaster on c.GiftId equals Pi.GiftId

                                                     where c.ActivationDistCode == userid && c.RedemptionDealerCode != null && c.ClaimDistCode!=null
                                                     select new
                                                     {
                                                         DealerCode = c.RedemptionDealerCode,
                                                         DealerName = c.RedemptionDealerName,
                                                         SecretCode = c.SecretCode,
                                                         GiftName = Pi.GiftName,
                                                         DistributorActivatedDate = c.ActivationDistOn,
                                                         DealerredeptionDatetime = c.RedemptionDealerOn,
                                                         ClaimSubmission=c.ClaimDistOn
                                                     }).ToList();



                        if (getDealerReedemedData.Count > 0)
                        {
                            foreach (var list in getDealerReedemedData)
                            {
                                //DealerCode = c.RedemptionDealerCode,
                                //                      DealerName = c.RedemptionDealerName,
                                //                      SerialNo = c.RedemtionDealerSerNo,
                                //                      GiftName = Pi.GiftName,
                                //                      DistributorActivatedDate=c.ActivationDistOn,
                                //                      DealerredeptionDatetime=c.RedemptionDealerOn

                                DealerRedeemeddata delreddata = new DealerRedeemeddata();

                                if (list.DealerCode == "" || list.DealerCode == null)
                                {
                                    delreddata.DealerCode = "0";
                                }
                                else
                                {
                                    delreddata.DealerCode = list.DealerCode;
                                }
                                if (list.DealerName == "" || list.DealerName == null)
                                {
                                    delreddata.DealerName = "0";
                                }
                                else
                                {
                                    delreddata.DealerName = list.DealerName;
                                }
                            
                                if (list.SecretCode == "" || list.SecretCode == null)
                                {
                                    delreddata.AlphanumericCode = "0";
                                }
                                else
                                {
                                    delreddata.AlphanumericCode = list.SecretCode;
                                }
                                if (list.GiftName == "" || list.GiftName == null)
                                {
                                    delreddata.GiftName = "0";
                                }
                                else
                                {
                                    delreddata.GiftName = list.GiftName;
                                }
                                if (list.DistributorActivatedDate == null)
                                {
                                    delreddata.DistActivatedDateTime = "0";
                                }
                                else
                                {
                                    delreddata.DistActivatedDateTime = list.DistributorActivatedDate.ToString();
                                }
                                if (list.DealerredeptionDatetime == null)
                                {
                                    delreddata.DealerredemptionDateTime = "0";
                                }
                                else
                                {
                                    delreddata.DealerredemptionDateTime = list.DealerredeptionDatetime.ToString();
                                }
                                if (list.ClaimSubmission == null)
                                {
                                    delreddata.ClaimSubmissionDate = "0";
                                }
                                else
                                {
                                    delreddata.ClaimSubmissionDate = list.ClaimSubmission.ToString();
                                }
                                delreddata.Message = "Success";

                                getdelaerdata.Add(delreddata);
                            }

                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Message:Success";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        }
                        else
                        {
                            DealerRedeemeddata delreddata = new DealerRedeemeddata();

                            delreddata.DealerCode = "0";
                            delreddata.DealerName = "0";
                            delreddata.SecretCode = "0";
                            delreddata.AlphanumericCode = "0";
                            delreddata.GiftName = "0";
                            delreddata.DistActivatedDateTime = "0";
                            delreddata.DealerredemptionDateTime = "0";
                            delreddata.ClaimSubmissionDate = "0";
                            delreddata.Message = "No Data Exist";
                            getdelaerdata.Add(delreddata);
                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Message:No Data Exist";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }


                    }
                    else
                    {
                        DealerRedeemeddata delreddata = new DealerRedeemeddata();

                        delreddata.DealerCode = "0";
                        delreddata.DealerName = "0";
                      
                        delreddata.AlphanumericCode = "0";
                        delreddata.GiftName = "0";
                        delreddata.DistActivatedDateTime = "0";
                        delreddata.DealerredemptionDateTime = "0";
                        delreddata.ClaimSubmissionDate = "0";
                        delreddata.Message = "User already logged in three devices";
                        getdelaerdata.Add(delreddata);
                        string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Message:User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {

                    DealerRedeemeddata delreddata = new DealerRedeemeddata();

                    delreddata.DealerCode = "0";
                    delreddata.DealerName = "0";
                    delreddata.AlphanumericCode = "0";
                   
                    delreddata.GiftName = "0";
                    delreddata.DistActivatedDateTime = "0";
                    delreddata.DealerredemptionDateTime = "0";
                    delreddata.ClaimSubmissionDate = "0";
                    delreddata.Message = "Unauthorized Access";
                    getdelaerdata.Add(delreddata);
                    string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";

                    string ResponseParameter = "Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                DealerRedeemeddata delreddata = new DealerRedeemeddata();

                delreddata.DealerCode = "0";
                delreddata.DealerName = "0";
                delreddata.AlphanumericCode = "0";
               
                delreddata.GiftName = "0";
                delreddata.DistActivatedDateTime = "0";
                delreddata.DealerredemptionDateTime = "0";
                delreddata.ClaimSubmissionDate = "0";
                delreddata.Message = "Some exception has occurred";
                getdelaerdata.Add(delreddata);
                string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Message:Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getdelaerdata;
        }
        //End LSD_GetRedeemedData by ravi on 16-12-2018//

        //Add new api LSD_GetClaimReport by ravi on 16-12-2018//
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<DealerRedeemeddata> LSD_GetClaimReport(string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {

            LuminousEntities luminous = new LuminousEntities();
            List<DealerRedeemeddata> getdelaerdata = new List<DealerRedeemeddata>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/LSD_GetClaimReport";
            try
            {

                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {

                        var getDealerReedemedData = (from c in luminous.LSD_Master
                                                     join Pi in luminous.Lsd_GiftMaster on c.GiftId equals Pi.GiftId

                                                     where c.ActivationDistCode == userid && c.RedemptionDealerCode != null && c.ClaimDistCode != null && c.ClaimDistOn != null
                                                     select new
                                                     {
                                                         DealerCode = c.RedemptionDealerCode,
                                                         DealerName = c.RedemptionDealerName,
                                                         SercretCode = c.SecretCode,
                                                         GiftName = Pi.GiftName,
                                                         DistributorActivatedDate = c.ActivationDistOn,
                                                         DealerredeptionDatetime = c.RedemptionDealerOn,
                                                         ClaimSubmissionDate = c.ClaimDistOn
                                                     }).ToList();



                        if (getDealerReedemedData.Count > 0)
                        {
                            foreach (var list in getDealerReedemedData)
                            {
                                //DealerCode = c.RedemptionDealerCode,
                                //                      DealerName = c.RedemptionDealerName,
                                //                      SerialNo = c.RedemtionDealerSerNo,
                                //                      GiftName = Pi.GiftName,
                                //                      DistributorActivatedDate=c.ActivationDistOn,
                                //                      DealerredeptionDatetime=c.RedemptionDealerOn

                                DealerRedeemeddata delreddata = new DealerRedeemeddata();

                                if (list.DealerCode == "" || list.DealerCode == null)
                                {
                                    delreddata.DealerCode = "0";
                                }
                                else
                                {
                                    delreddata.DealerCode = list.DealerCode;
                                }
                                if (list.DealerName == "" || list.DealerName == null)
                                {
                                    delreddata.DealerName = "0";
                                }
                                else
                                {
                                    delreddata.DealerName = list.DealerName;
                                }
                                if (list.SercretCode == "" || list.SercretCode == null)
                                {
                                    delreddata.SecretCode = "0";
                                }
                                else
                                {
                                    delreddata.SecretCode = list.SercretCode;
                                }


                                if (list.GiftName == "" || list.GiftName == null)
                                {
                                    delreddata.GiftName = "0";
                                }
                                else
                                {
                                    delreddata.GiftName = list.GiftName;
                                }
                                if (list.DistributorActivatedDate == null)
                                {
                                    delreddata.DistActivatedDateTime = "0";
                                }
                                else
                                {
                                    delreddata.DistActivatedDateTime = list.DistributorActivatedDate.ToString();
                                }
                                if (list.DealerredeptionDatetime == null)
                                {
                                    delreddata.DealerredemptionDateTime = "0";
                                }
                                else
                                {
                                    delreddata.DealerredemptionDateTime = list.DealerredeptionDatetime.ToString();
                                }
                                if (list.ClaimSubmissionDate == null)
                                {
                                    delreddata.ClaimSubmissionDate = "0";
                                }
                                else
                                {
                                    delreddata.ClaimSubmissionDate = list.ClaimSubmissionDate.ToString();
                                }
                                delreddata.Message = "Success";

                                getdelaerdata.Add(delreddata);
                            }

                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Message:Success";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        }
                        else
                        {
                            DealerRedeemeddata delreddata = new DealerRedeemeddata();

                            delreddata.DealerCode = "0";
                            delreddata.DealerName = "0";
                            delreddata.SecretCode = "0";
                            delreddata.SecretCode = "0";
                            delreddata.GiftName = "0";
                            delreddata.DistActivatedDateTime = "0";
                            delreddata.DealerredemptionDateTime = "0";
                            delreddata.ClaimSubmissionDate = "0";
                            delreddata.Message = "No Data Exist";
                            getdelaerdata.Add(delreddata);
                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Message:No Data Exist";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }


                    }
                    else
                    {
                        DealerRedeemeddata delreddata = new DealerRedeemeddata();

                        delreddata.DealerCode = "0";
                        delreddata.DealerName = "0";
                        delreddata.SecretCode = "0";
                        delreddata.SecretCode = "0";
                        delreddata.GiftName = "0";
                        delreddata.DistActivatedDateTime = "0";
                        delreddata.DealerredemptionDateTime = "0";
                        delreddata.ClaimSubmissionDate = "0";
                        delreddata.Message = "User already logged in three devices";
                        getdelaerdata.Add(delreddata);
                        string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "Message:User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {

                    DealerRedeemeddata delreddata = new DealerRedeemeddata();

                    delreddata.DealerCode = "0";
                    delreddata.DealerName = "0";
                    delreddata.SecretCode = "0";
                    delreddata.SecretCode = "0";
                    delreddata.GiftName = "0";
                    delreddata.DistActivatedDateTime = "0";
                    delreddata.DealerredemptionDateTime = "0";
                    delreddata.ClaimSubmissionDate = "0";
                    delreddata.Message = "Unauthorized Access";
                    getdelaerdata.Add(delreddata);
                    string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";

                    string ResponseParameter = "Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                DealerRedeemeddata delreddata = new DealerRedeemeddata();

                delreddata.DealerCode = "0";
                delreddata.DealerName = "0";
                delreddata.SecretCode = "0";
                delreddata.SecretCode = "0";
                delreddata.GiftName = "0";
                delreddata.DistActivatedDateTime = "0";
                delreddata.DealerredemptionDateTime = "0";
                delreddata.ClaimSubmissionDate = "0";
                delreddata.Message = "Some exception has occurred";
                getdelaerdata.Add(delreddata);
                string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Message:Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getdelaerdata;
        }
        //End LSD_GetClaimReport by ravi on 16-12-2018//


        //Add new api LSD_GetActivatedCoupon by ravi on 15-12-2018//
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<DealerRedeemeddata> LSD_GetDistOpenReimbursmentReport(string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {

            LuminousEntities luminous = new LuminousEntities();
            List<DealerRedeemeddata> getdealerredemdata = new List<DealerRedeemeddata>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/LSD_GetDistOpenReimbursmentReport";
            try
            {

                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {

                        //var getDealerQrCode_Time = luminous.LSD_Master.Where(c => c.ActivationDistCode == userid && c.RedemptionDealerCode != null && c.RedemptionDealerSecretCode != null).ToList();
                        var getDealerQrCode_Time = (from c in luminous.LSD_Master
                                                     join Pi in luminous.Lsd_GiftMaster on c.GiftId equals Pi.GiftId

                                                     where c.ActivationDistCode == userid && c.RedemptionDealerCode != null && c.RedemptionDealerSecretCode != null && c.ClaimDistCode==null
                                                     select new
                                                     {
                                                         RedemptionDealerCode = c.RedemptionDealerCode,
                                                         RedemptionDealerName = c.RedemptionDealerName,
                                                         SecretCode = c.SecretCode,
                                                         GiftName = Pi.GiftName,
                                                         DistributorActivatedDate = c.ActivationDistOn,
                                                         RedemptionDealerOn = c.RedemptionDealerOn,
                                                         ActivatedQrCode=c.ActivatedQrCode,
                                                         Id=c.Id
                                                     }).ToList();


                        if (getDealerQrCode_Time.Count > 0)
                        {
                            foreach (var list in getDealerQrCode_Time)
                            {

                                DealerRedeemeddata dealerdata = new DealerRedeemeddata();

                              
                                if (list.RedemptionDealerOn == null)
                                {
                                    dealerdata.DealerredemptionDateTime = "0";
                                }
                                else
                                {
                                    dealerdata.DealerredemptionDateTime = list.RedemptionDealerOn.ToString();
                                }


                                if (list.RedemptionDealerName == "" || list.RedemptionDealerName == null)
                                {
                                    dealerdata.DealerName = "0";
                                }
                                else
                                {
                                    dealerdata.DealerName = list.RedemptionDealerName;
                                }


                                if (list.RedemptionDealerCode == "" || list.RedemptionDealerCode == null)
                                {
                                    dealerdata.DealerCode = "0";
                                }
                                else
                                {
                                    dealerdata.DealerCode = list.RedemptionDealerCode;
                                }
                                
                                
                                if (list.SecretCode == "" || list.SecretCode == null)
                                {
                                    dealerdata.AlphanumericCode = "0";
                                }
                                else
                                {
                                    dealerdata.AlphanumericCode = list.SecretCode;
                                }
                                
                                
                                
                                if (list.GiftName == "" || list.GiftName == null)
                                {
                                    dealerdata.GiftName = "0";
                                }
                                else
                                {
                                    dealerdata.GiftName = list.GiftName;
                                }
                                
                                
                           
                                
                                
                                dealerdata.ActivationDateAndTime = list.DistributorActivatedDate.ToString();

                                dealerdata.Message = "Success";

                                getdealerredemdata.Add(dealerdata);
                            }

                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Message:Success";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        }
                        else
                        {
                            DealerRedeemeddata dealerdata = new DealerRedeemeddata();
                           // dealerdata.SecretCode = "0";
                            dealerdata.DealerredemptionDateTime = "0";
                            dealerdata.Message = "No Data Exist";
                            dealerdata.ActivationDateAndTime = "0";
                            dealerdata.GiftName = "0";
                            dealerdata.AlphanumericCode = "0";
                           // dealerdata.Id = 0;
                            dealerdata.DealerName = "0";
                            dealerdata.DealerCode = "0";
                            getdealerredemdata.Add(dealerdata);
                            string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Message:No Data Exist";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }


                    }
                    else
                    {
                        DealerRedeemeddata dealerdata = new DealerRedeemeddata();
                       // dealerdata.SecretCode = "0";
                        dealerdata.DealerredemptionDateTime = "0";
                        dealerdata.Message = "User already logged in three devices";
                        dealerdata.ActivationDateAndTime = "0";
                        dealerdata.GiftName = "0";
                        dealerdata.AlphanumericCode = "0";
                        //dealerdata.Id = 0;
                        dealerdata.DealerName = "0";
                        dealerdata.DealerCode = "0";
                        getdealerredemdata.Add(dealerdata);
                        string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "ActivatedQrCode :0,Message:User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {

                    DealerRedeemeddata dealerdata = new DealerRedeemeddata();
                   // dealerdata.SecretCode = "0";
                    dealerdata.DealerredemptionDateTime = "0";
                    dealerdata.Message = "Unauthorized Access";
                    dealerdata.ActivationDateAndTime = "0";
                    dealerdata.GiftName = "0";
                    dealerdata.AlphanumericCode = "0";
                   // dealerdata.Id = 0;
                    dealerdata.DealerName = "0";
                    dealerdata.DealerCode = "0";
                    getdealerredemdata.Add(dealerdata);
                    string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";

                    string ResponseParameter = "ActivatedQrCode :0,Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                DealerRedeemeddata dealerdata = new DealerRedeemeddata();
              //  dealerdata.SecretCode = "0";
                dealerdata.DealerredemptionDateTime = "0";
                dealerdata.Message = "Some exception has occurred";
                dealerdata.ActivationDateAndTime = "0";
                dealerdata.GiftName = "0";
                dealerdata.AlphanumericCode = "0";
                //dealerdata.Id = 0;
                dealerdata.DealerName = "0";
                dealerdata.DealerCode = "0";
                getdealerredemdata.Add(dealerdata);
                string RequestParameter = "UserID :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "ActivatedQrCode :0,Message:Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return getdealerredemdata;
        }

        //End  LSD_GetActivatedCoupon by ravi on 15-12-2018//


        //Add new api LSD_SaveClaimData by ravi on 16-12-2018//

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public string LSD_SaveClaimData(string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();

            string url = HttpContext.Current.Request.Url.ToString() + "/LSD_SaveClaimData";

            try
            {
                string LoginToken = "";


                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {

                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        var checkredeemedata = luminous.LSD_Master.Where(c => c.RedemptionDealerCode != null && c.RedemptionDealerOn != null && c.ActivationDistOn != null && c.ActivationDistCode == userid).Count();

                        if (checkredeemedata > 0)
                        {

                          //  luminous.ExecuteStoreCommand("Update LSD_Master set ClaimDistCode='" + userid + "',ClaimDistOn='" + DateTime.Now + "' where ActivationDistOn='" + userid + "' and RedemptionDealerCode is not null ");
                            luminous.LSD_UpdateClaimDistCode(userid);


                           var DealerRedeemedCount=  luminous.Lsd_DistCouponCount.Where(c=>c.DistCode==userid).Select(c=>c.DealerRedeemedCount).SingleOrDefault();
                            var DistClaimCount=  luminous.Lsd_DistCouponCount.Where(c=>c.DistCode==userid).Select(c=>c.DistClaimedCount).SingleOrDefault();

                            var  distclaimcount=DealerRedeemedCount + DistClaimCount;

                            luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set DealerRedeemedCount=0,DistClaimedCount='" + distclaimcount + "' where DistCode='" + userid + "'");
                            

                            string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Claim Updated Successfully";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Inserted Successfully", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                            return "Claim Updated Successfully";

                        }
                        else
                        {
                            string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "No Data Exist";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                            return "No Data Exist";
                        }
                    }
                    else
                    {
                        string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        return "User already logged in three devices";
                    }
                }
                else
                {

                    string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    return "Unauthorized Access";
                }
            }
            catch (Exception exc)
            {

                string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                return "Some exception has occurred";
            }

        }


        //End LSD_SaveClaimData api//


        //Add new api LSD_SaveQrCode by ravi on 15-12-2018//

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<GiftData> LSD_SaveDealerScanCode(string userid, string barcode, string secretcode, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<GiftData> giftdata = new List<GiftData>();
            int dealersubmitcount = 0;
           
            string url = HttpContext.Current.Request.Url.ToString() + "/LSD_SaveDelaerScanCode";

            try
            {
                string LoginToken = "";


                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {

                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                        // var distactivatedcount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => new { c.DistActivatedCount, c.EligibleCoupon }).SingleOrDefault();

                        //  activatecount = Convert.ToInt32(distactivatedcount.DistActivatedCount) + 1;

                        // if (activatecount < distactivatedcount.EligibleCoupon)
                        // {
                        var barcodenotactive = luminous.LSD_Master.Where(c => c.Barcode == barcode && c.ActivationDistCode == null).Count();
                        if (barcodenotactive == 0)
                        {
                            var matchbarcode_secretcode = luminous.LSD_Master.Where(c => c.Barcode == barcode && c.SecretCode == secretcode).Count();
                            if (matchbarcode_secretcode > 0)
                            {

                                var checkexist = luminous.LSD_Master.Where(c => c.ActivatedQrCode != null && c.ActivationDistCode != null && c.RedemtionDealerBarCode == barcode && c.RedemptionDealerSecretCode == secretcode).ToList();

                                if (checkexist.Count() > 0)
                                {
                                    GiftData gdata = new GiftData();
                                    gdata.GiftId = 0;
                                    gdata.GiftName = "0";
                                    gdata.GiftDescription = "0";
                                    gdata.GiftImage = "0";
                                    gdata.Message = "BarCode already submitted";
                                    giftdata.Add(gdata);
                                }
                                else
                                {

                                    var getdealerName = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.Dis_Name).SingleOrDefault();
                                    var getdistcode = luminous.LSD_Master.Where(c => c.Barcode == barcode && c.SecretCode == secretcode).Select(c => c.ActivationDistCode).SingleOrDefault();
                                    string distributorcode = getdistcode;
                                    //  luminous.ExecuteStoreCommand("Update LSD_Master set RedemptionDealerCode='" + userid + "',RedemptionDealerName='" + getdealerName + "',RedemtionDealerBarCode='" + barcode + "',RedemptionDealerSecretCode='" + secretcode + "',RedemptionDealerOn='" + DateTime.Now + "' where ActivationDistCode='" + getdistcode + "'");

                                    luminous.UpdateRedemptionDealerCode(userid, getdealerName, barcode, secretcode, getdistcode);
                                    var dealerreedemedcount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == distributorcode).Select(c => c.DealerRedeemedCount).SingleOrDefault();

                                    dealersubmitcount = dealerreedemedcount.Value + 1;

                                    luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set DealerRedeemedCount='" + dealersubmitcount + "' where DistCode ='" + getdistcode + "'");

                                    var giftList = (from c in luminous.LSD_Master
                                                    join Pi in luminous.Lsd_GiftMaster on c.GiftId equals Pi.GiftId
                                                    where c.RedemptionDealerCode == userid && c.ActivationDistCode == distributorcode && c.RedemtionDealerBarCode == barcode && c.RedemptionDealerSecretCode == secretcode
                                                    select new
                                                    {
                                                        GiftId = Pi.GiftId,
                                                        GiftName = Pi.GiftName,
                                                        Giftdescription = Pi.GiftDesc,
                                                        GiftImage = Pi.GiftImage

                                                    }).Distinct().ToList();
                                    foreach (var gift in giftList)
                                    {
                                        GiftData gdata = new GiftData();
                                        gdata.GiftId = gift.GiftId;
                                        gdata.GiftName = gift.GiftName;
                                        gdata.GiftDescription = gift.Giftdescription;
                                        gdata.GiftImage = Url + "LSDImage/" + gift.GiftImage;
                                        gdata.Message = "Success";
                                        giftdata.Add(gdata);
                                    }

                                    string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",BarCode :"+barcode+",AlphanumericCode :"+secretcode+",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                    string ResponseParameter = "Success";

                                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);




                                }
                            }
                            else
                            {
                                GiftData gdata = new GiftData();
                                gdata.GiftId = 0;
                                gdata.GiftName = "0";
                                gdata.GiftDescription = "0";
                                gdata.GiftImage = "0";
                                gdata.Message = "Invalid Barcod or Serial no";
                                giftdata.Add(gdata);
                                string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",BarCode :" + barcode + ",AlphanumericCode :" + secretcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                                string ResponseParameter = "Invalid Barcod or Serial no";

                                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Invalid Barcod or Serial no", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                            }
                        }
                        else
                        {
                            GiftData gdata = new GiftData();
                            gdata.GiftId = 0;
                            gdata.GiftName = "0";
                            gdata.GiftDescription = "0";
                            gdata.GiftImage = "0";
                            gdata.Message = "Coupon Not Active";
                            giftdata.Add(gdata);
                            string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",BarCode :" + barcode + ",AlphanumericCode :" + secretcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Coupon Not Active";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Coupon Not Active", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }




                        // abc = "Inserted Succesfully123";
                    }
                    else
                    {
                        GiftData gdata = new GiftData();
                        gdata.GiftId = 0;
                        gdata.GiftName = "0";
                        gdata.GiftDescription = "0";
                        gdata.GiftImage = "0";
                        gdata.Message = "User already logged in three devices";
                        giftdata.Add(gdata);
                        string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",BarCode :" + barcode + ",AlphanumericCode :" + secretcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                       
                    }
                }
                else
                {
                    GiftData gdata = new GiftData();
                    gdata.GiftId = 0;
                    gdata.GiftName = "0";
                    gdata.GiftDescription = "0";
                    gdata.GiftImage = "0";
                    gdata.Message = "Unauthorized Access";
                    giftdata.Add(gdata);
                    string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",BarCode :" + barcode + ",AlphanumericCode :" + secretcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                  
                }
            }
            catch (Exception exc)
            {
                GiftData gdata = new GiftData();
                gdata.GiftId = 0;
                gdata.GiftName = "0";
                gdata.GiftDescription = "0";
                gdata.GiftImage = "0";
                gdata.Message = "Some exception has occurred";
                giftdata.Add(gdata);
                string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",BarCode :" + barcode + ",AlphanumericCode :" + secretcode + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                
            }
            return giftdata;
        }


        //End LSD_SaveQrCode api//


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<DealerReport> LSD_DealerReport(string userid, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();
            List<DealerReport> dealerreportdata = new List<DealerReport>();


            string url = HttpContext.Current.Request.Url.ToString() + "/LSD_DealerReport";

            try
            {
                string LoginToken = "";


                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {

                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {

                        var getDealerReport = (from c in luminous.LSD_Master
                                               join Pi in luminous.Lsd_GiftMaster on c.GiftId equals Pi.GiftId
                                               where c.RedemptionDealerCode == userid
                                               select new
                                               {
                                                   barcode = c.RedemtionDealerBarCode,
                                                   secretcode=c.RedemptionDealerSecretCode,
                                                   ReimbursmentDate_Time=c.RedemptionDealerOn,
                                                   GiftName = Pi.GiftName,
                                                   DistName = c.ActivationDistName
                                               }).ToList();
                        if (getDealerReport.Count > 0)
                        {




                            foreach (var Dreport in getDealerReport)
                            {
                                DealerReport report = new DealerReport();
                                report.Barcode = Dreport.barcode;
                                report.Gift = Dreport.GiftName;
                                report.ActivatedDistName = Dreport.DistName;
                                report.SecretCode = Dreport.secretcode;
                                report.ReimbursmentDate_Time = Dreport.ReimbursmentDate_Time;
                                report.Message = "Success";
                                dealerreportdata.Add(report);
                            }

                            string RequestParameter = "UserId :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Success";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);




                        }

                        else
                        {
                            DealerReport report = new DealerReport();
                            report.Barcode = "0";
                            report.Gift = "0";
                            report.ActivatedDistName = "0";
                            report.SecretCode = "0";
                            report.ReimbursmentDate_Time =null;
                            report.Message = "No data Exist";
                            dealerreportdata.Add(report);

                            string RequestParameter = "UserId :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "No data Exist";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        }




                        // abc = "Inserted Succesfully123";
                    }
                    else
                    {
                        DealerReport report = new DealerReport();
                        report.Barcode = "0";
                        report.Gift = "0";
                        report.ActivatedDistName = "0";
                        report.SecretCode = "0";
                        report.ReimbursmentDate_Time = null;
                        report.Message = "User already logged in three devices";
                        dealerreportdata.Add(report);
                        string RequestParameter = "UserId :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                    }
                }
                else
                {
                    DealerReport report = new DealerReport();
                    report.Barcode = "0";
                    report.Gift = "0";
                    report.ActivatedDistName = "0";
                    report.SecretCode = "0";
                    report.ReimbursmentDate_Time = null;
                    report.Message = "Unauthorized Access";
                    dealerreportdata.Add(report);
                    string RequestParameter = "UserId :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                    string ResponseParameter = "Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                }
            }
            catch (Exception exc)
            {
                DealerReport report = new DealerReport();
                report.Barcode = "0";
                report.Gift = "0";
                report.ActivatedDistName = "0";
                report.SecretCode = "0";
                report.ReimbursmentDate_Time = null;
                report.Message = "Some exception has occurred";
                dealerreportdata.Add(report);
                string RequestParameter = "UserId :" + userid + ",Dealercode :" + userid + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

            }
            return dealerreportdata;
        }


        //End//
        //LSD_TermsCondtionInfo for showing terms and condition info by Ravi on 25-12-2018//
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<TermsCondition_Scheme> LSD_TermsConditionInfo(string userid,string Usertype, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {

            LuminousEntities luminous = new LuminousEntities();
            List<TermsCondition_Scheme> gettermcondition = new List<TermsCondition_Scheme>();
            //string url = System.Reflection.MethodBase.GetCurrentMethod().ToString();
            string url = HttpContext.Current.Request.Url.ToString() + "/LSD_TermsCondtionInfo";
            try
            {

                string LoginToken = "";
                string TokenString = userid + Appversion;
                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                if (LoginToken == token)
                {
                    var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == deviceid && c.UserId == userid);
                    if (checkstatus.Count() == 0)
                    {
                       
                        var getTermsCondition = luminous.LSD_TNCMaster.Where(c => c.UserType == Usertype.ToLower() && c.Status==1).ToList();



                        if (getTermsCondition.Count > 0)
                        {
                            foreach (var list in getTermsCondition)
                            {

                                TermsCondition_Scheme termcondition_scheme = new TermsCondition_Scheme();

                                if (list.Tnc == "0" || list.Tnc == null)
                                {
                                    termcondition_scheme.TermsCondition = "0";
                                }
                                else
                                {
                                    termcondition_scheme.TermsCondition = list.Tnc;
                                }
                                if (list.Schemeinfo == "0" || list.Schemeinfo == null)
                                {
                                    termcondition_scheme.schemeinfo = "0";
                                }
                                else
                                {
                                    termcondition_scheme.schemeinfo =list.Schemeinfo;
                                }
                                if (list.Img == "0" || list.Img == null)
                                {
                                    termcondition_scheme.Image = "0";
                                }
                                else
                                {
                                    termcondition_scheme.Image = Url + "TCImage/" + list.Img;
                                }
                                termcondition_scheme.Message = "Success";

                                gettermcondition.Add(termcondition_scheme);
                            }

                            string RequestParameter = "UserID :" + userid + ",Usertype :"+Usertype+", Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "Message:Success";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Success", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                        }
                        else
                        {
                            TermsCondition_Scheme termcondition_scheme = new TermsCondition_Scheme();

                            
                                termcondition_scheme.TermsCondition = "0";
                                termcondition_scheme.Image = "0";
                                termcondition_scheme.schemeinfo = "0";
                                termcondition_scheme.Message = "No Data Exist";

                            gettermcondition.Add(termcondition_scheme);
                            string RequestParameter = "UserID :" + userid + ",Usertype :" + Usertype + ", Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                            string ResponseParameter = "ActivatedQrCode :0,Message:No Data Exist";

                            SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "No Data Exist", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                        }


                    }
                    else
                    {
                        TermsCondition_Scheme termcondition_scheme = new TermsCondition_Scheme();


                        termcondition_scheme.TermsCondition = "0";
                        termcondition_scheme.Image = "0";
                        termcondition_scheme.schemeinfo = "0";
                        termcondition_scheme.Message = "User already logged in three devices";

                        gettermcondition.Add(termcondition_scheme);
                        string RequestParameter = "UserID :" + userid + ",Usertype :" + Usertype + ", Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                        string ResponseParameter = "ActivatedQrCode :0,Message:User already logged in three devices";

                        SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "User already logged in three devices", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                    }
                }
                else
                {

                    TermsCondition_Scheme termcondition_scheme = new TermsCondition_Scheme();


                    termcondition_scheme.TermsCondition = "0";
                    termcondition_scheme.Image = "0";
                    termcondition_scheme.schemeinfo = "0";
                    termcondition_scheme.Message = "Unauthorized Access";

                    gettermcondition.Add(termcondition_scheme);
                    string RequestParameter = "UserID :" + userid + ",Usertype :" + Usertype + ", Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";

                    string ResponseParameter = "ActivatedQrCode :0,Message:Unauthorized Access";

                    SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, "Unauthorized Access", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                }

            }
            catch (Exception exc)
            {
                TermsCondition_Scheme termcondition_scheme = new TermsCondition_Scheme();


                termcondition_scheme.TermsCondition = "0";
                termcondition_scheme.Image = "0";
                termcondition_scheme.schemeinfo = "0";
                termcondition_scheme.Message = "Some exception has occurred";

                gettermcondition.Add(termcondition_scheme);
                string RequestParameter = "UserID :" + userid + ",Usertype :" + Usertype + ", Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "ActivatedQrCode :0,Message:Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
            }
            return gettermcondition;
        }


        //Added registration api for IOS by Ravi

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public string UserRegistration(string userid, string name, string email, string token, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            LuminousEntities luminous = new LuminousEntities();

            string url = HttpContext.Current.Request.Url.ToString() + "/UserRegistration";

            try
            {


                luminous.AddToUserRegistrations(new UserRegistration
                {
                    Name = name,
                    Email = email,

                    CreatedOn = DateTime.Now


                }


                                );

                luminous.SaveChanges();


                string RequestParameter = "UserId :" + userid + ",Name :" + name + ",Email :" + email + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Regitered Successfully";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 0, "Regitered Successfully", userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);

                return "Regitered Successfully";






            }
            catch (Exception exc)
            {

                string RequestParameter = "UserId :" + userid + ",Name :" + name + ",Email :" + email + ",Token :" + token + ",DeviceID :" + deviceid + ",Otp :,AppVersion :" + Appversion + ",OsType :" + Ostype + ",OsVersion :" + Osversion + "";
                string ResponseParameter = "Some exception has occurred";

                SaveServiceLog(userid, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), userid, DateTime.Now, deviceid, Appversion, Ostype, Osversion);
                return "Some exception has occurred";
            }

        }

        public void SaveServiceLog(string Userid, string url, string Req_Parameter, string Res_Parameter, int Error, string ErrorDes, string createdby, DateTime Createdon, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            try
            {
                LuminousEntities luminous = new LuminousEntities();

                string[] redirect;
                string comments = "";
                string[] status;
                string flag = "";
                if (Userid.Contains(","))
                {
                    redirect = Userid.Split(',');
                    comments = redirect[1];
                    Userid = redirect[0];
                }
                else
                    if (Req_Parameter.Contains("%"))
                    {
                        status = Req_Parameter.Split('%');
                        flag = status[1];
                        Req_Parameter = status[0];
                    }

                luminous.AddToMPartnerServiceLogs(new MPartnerServiceLog
                  {

                      UserId = Userid,
                      Url = url,
                      Req_Parameter = Req_Parameter,
                      Res_parameter = Res_Parameter,
                      Error = Error,
                      ErrorDescription = ErrorDes,
                      Flag = flag,

                      CreatedBy = Userid,
                      CreatedOn = Createdon,
                      DeviceId = deviceid,
                      AppVersion = Appversion,
                      OSType = Ostype,
                      OSVersion = Osversion,
                      NewURL = url,
                      Comments = comments

                  });

                luminous.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //Encrypt token code///
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

    }

    //public List<Products> GetProductlevelFullNames(string ProductName)
    //{
    //    Luminous db = new Luminous();

    //}


}


