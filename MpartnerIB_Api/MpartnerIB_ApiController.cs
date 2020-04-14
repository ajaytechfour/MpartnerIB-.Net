using System;
using System.Collections.Generic;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.IO;
//using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Configuration;
//using System.Data;
using System.Net.Mail;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LuminousMpartnerIB.EF;
using LuminousMpartnerIB.MpartnerIB_Api.Model;
namespace LuminousMpartnerIB.MpartnerIB_Api
{
    public class MpartnerIB_ApiController : ApiController
    {
        LuminousMpartnerIBEntities luminous = new LuminousMpartnerIBEntities();
        #region Api
        [System.Web.Http.HttpGet]
        [ActionName("LogInCreateOtp")]
        public object LogInCreateOtp(string user_id, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {

                var getAppMessage = getAppversion(app_version, os_type, channel);




                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "'";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_LoginAuthentication(getAppMessage.Message, getAppMessage.Status);

                }
                else
                {
                    var logincreate_otp = func_LoginCreateOtp(user_id, device_id, os_version_code, device_name, app_version);
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + logincreate_otp.Message + ",Status : " + logincreate_otp.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, logincreate_otp.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_LoginAuthentication(logincreate_otp.Message, logincreate_otp.Status);
                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "'";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_LoginAuthentication(exception.Message, exception.Status);
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("OTPAuthentication")]
        public object OTPAuthentication(string user_id, string otp, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {

            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            List<Userprofile> u_profile = new List<Userprofile>();

            try
            {



                var getAppMessage = getAppversion(app_version, os_type, channel);



                if (getAppMessage.Status != "")
                {

                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",OTP :" + otp + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_OTPAuthentication(getAppMessage.Message, getAppMessage.Status, "", null);
                    }




                }
                else
                {

                    var otp_authentication = func_OTPAuthentication(user_id, device_id, os_version_code, device_name, otp, os_version_name, os_type, app_version);
                    if (otp_authentication.Status == "200")
                    {
                       
                        var userprofile = luminous.get_UserProfile(user_id).SingleOrDefault();
                        Userprofile usr_profile = new Userprofile();
                        usr_profile.Employeeid = userprofile.Userid;
                        usr_profile.Username = userprofile.Username.ToString().TrimEnd();
                        usr_profile.PhoneNumber = userprofile.PhoneNumber;
                        usr_profile.Address = userprofile.Address.TrimEnd();
                        usr_profile.State = userprofile.State;
                        usr_profile.City = userprofile.City;
                        //usr_profile.Pincode = userprofile.Pincode;
                        u_profile.Add(usr_profile);
                    }
                    //Save Api log data/v
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",OTP :" + otp + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + otp_authentication.Message + ",Status : " + otp_authentication.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, otp_authentication.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_OTPAuthentication(otp_authentication.Message, otp_authentication.Status, otp_authentication.Token, u_profile);


                }


            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",OTP :" + otp + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_OTPAuthentication(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("Customer_permission_data")]
        public object Customer_permission_data(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel, string browser = null, string Browser_version = null)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getAppMessage = getAppversion(app_version, os_type, channel);
                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                if (getTokenMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_customer_permission(getTokenMessage.Message, getTokenMessage.Status, "", null);
                }
                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion

                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_customer_permission(getAppMessage.Message, getAppMessage.Status, getTokenMessage.Token, null);
                }
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                if (checkedThreeUserLoggedIn.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_customer_permission(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                }
                var dataexistornot = Checked_data_existornot_customerpermission_data(user_id, language);
                if (dataexistornot.Status == "0")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_customer_permission(dataexistornot.Message, dataexistornot.Status, "", null);
                }
                if (dataexistornot.Status == "200")
                {
                    var permissiondata = getcustomer_permission(user_id, language);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_customer_permission(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, permissiondata);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_customer_permission(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("home_page_cards")]
        public object home_page_cards(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {

                var getAppMessage = getAppversion(app_version, os_type, channel);
                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                if (getTokenMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_CardProvider(getTokenMessage.Message, getTokenMessage.Status, "", null, "HomePage");
                }
                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion

                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_CardProvider(getAppMessage.Message, getAppMessage.Status, getTokenMessage.Token, null, "HomePage");
                }

                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                if (checkedThreeUserLoggedIn.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_CardProvider(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null, "HomePage");
                }
                var dataexistornot = Checked_data_existornot(user_id,"HomePage", "");

                if (dataexistornot.Status == "0")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_CardProvider(dataexistornot.Message, dataexistornot.Status, "", null, "HomePage");
                }
                if (dataexistornot.Status == "200")
                {
                    var cardProvider = getCardprovider(user_id, "HomePage", "", channel);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_CardProvider(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, cardProvider, "HomePage");

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_CardProvider(exception.Message, exception.Status, "", null, "HomePage");
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("scheme_page_cards")]
        public object scheme_page_cards(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel, string browser = null, string Browser_version = null)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getAppMessage = getAppversion(app_version, os_type, channel);
                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion

                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_CardProvider(getAppMessage.Message, getAppMessage.Status, getTokenMessage.Token, null, "Scheme");
                }
                if (getTokenMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_CardProvider(getTokenMessage.Message, getTokenMessage.Status, "", null, "Scheme");
                }
               
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                if (checkedThreeUserLoggedIn.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_CardProvider(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null, "Scheme");
                }
                var dataexistornot = Checked_data_existornot(user_id,"Scheme", "");
                if (dataexistornot.Status == "0")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_CardProvider(dataexistornot.Message, dataexistornot.Status, "", null, "Scheme");
                }


                if (dataexistornot.Status == "200")
                {
                    var cardProvider = getCardprovider(user_id, "Scheme", "", channel);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_CardProvider(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, cardProvider, "Scheme");

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_CardProvider(exception.Message, exception.Status, "", null, "Scheme");
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("pricelist_page_cards")]
        public object pricelist_page_cards(string user_id, string parentid, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel, string browser = null, string Browser_version = null)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getAppMessage = getAppversion(app_version, os_type, channel);
                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",ParentId :" + parentid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion

                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_CardProvider(getAppMessage.Message, getAppMessage.Status, getTokenMessage.Token, null, "Price");
                }
                if (getTokenMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",ParentId :" + parentid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_CardProvider(getTokenMessage.Message, getTokenMessage.Status, "", null, "Price");
                }
              
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                if (checkedThreeUserLoggedIn.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",ParentId :" + parentid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_CardProvider(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null, "Price");
                }
                var dataexistornot = Checked_data_existornot(user_id,"Price", parentid);

                if (dataexistornot.Status == "0")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",ParentId :" + parentid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_CardProvider(dataexistornot.Message, dataexistornot.Status, "", null, "Price");
                }
              
                if (dataexistornot.Status == "200")
                {
                    var cardProvider = getCardprovider(user_id, "Price", parentid, channel);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",ParentId :" + parentid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_CardProvider(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, cardProvider, "Price");

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",ParentId :" + parentid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_CardProvider(exception.Message, exception.Status, "", null, "Price");
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("ParentCategory")]
        public object ParentCategory(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel, string browser = null, string Browser_version = null)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getAppMessage = getAppversion(app_version, os_type, channel);
                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion

                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_ParentCategory(getAppMessage.Message, getAppMessage.Status, getTokenMessage.Token, null);
                }
                if (getTokenMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_ParentCategory(getTokenMessage.Message, getTokenMessage.Status, "", null);
                }
               
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                if (checkedThreeUserLoggedIn.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_ParentCategory(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                }
                var dataexistornot = Checked_data_existornot(user_id,"ParentCategory", "");

                if (dataexistornot.Status == "0")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_ParentCategory(dataexistornot.Message, dataexistornot.Status, "", null);
                }

               
                if (dataexistornot.Status == "200")
                {
                    var cardProvider = func_GetParentCategory();
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_ParentCategory(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, cardProvider);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_ParentCategory(exception.Message, exception.Status, "", null);
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("Catalog_menu_footer")]
        public object Catalog_menu_footer(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel, string browser = null, string Browser_version = null)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getAppMessage = getAppversion(app_version, os_type, channel);
                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion

                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_ProductCategory(getAppMessage.Message, getAppMessage.Status, getTokenMessage.Token, null, "ProductCategory");
                }
                if (getTokenMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_ProductCategory(getTokenMessage.Message, getTokenMessage.Status, "", null, "ProductCategory");
                }
               
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                if (checkedThreeUserLoggedIn.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_ProductCategory(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null, "ProductCategory");
                }
                var dataexistornot = Checked_data_existornot(user_id,"ProductCategory", "");

                if (dataexistornot.Status == "0")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_ProductCategory(dataexistornot.Message, dataexistornot.Status, "", null, "ProductCategory");
                }
                if (dataexistornot.Status == "200")
                {
                    var product_category = getProductCategory(user_id,"ProductCategory");
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_ProductCategory(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, product_category, "ProductCategory");

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_CardProvider(exception.Message, exception.Status, "", null, "ProductCategory");
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("Catalog_products")]
        public object Catalog_products(string user_id, int productcategoryid, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel, string browser = null, string Browser_version = null)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getAppMessage = getAppversion(app_version, os_type, channel);
                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion

                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_catalog(getAppMessage.Message, getAppMessage.Status, getTokenMessage.Token, null, "ProductCategory");
                }
                if (getTokenMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_catalog(getTokenMessage.Message, getTokenMessage.Status, "", null, "ProductCategory");
                }
             
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                if (checkedThreeUserLoggedIn.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_catalog(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null, "ProductCategory");
                }
                var dataexistornot = Checked_data_existornot_catalog_products(user_id,productcategoryid);
                if (dataexistornot.Status == "0")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_catalog(dataexistornot.Message, dataexistornot.Status, "", null, "ProductCategory");
                }


                if (dataexistornot.Status == "200")
                {
                    var product_catalog = getProduct_Catalog(user_id,productcategoryid);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_Product_catalog(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, product_catalog, "ProductCategory");

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_Product_catalog(exception.Message, exception.Status, "", null, "Price");
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("Search_products")]
        public object Search_products(string search_key, string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel, string browser = null, string Browser_version = null)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getAppMessage = getAppversion(app_version, os_type, channel);
                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "SearchKey : " + search_key + ",UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion

                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_search(getAppMessage.Message, getAppMessage.Status, getTokenMessage.Token, null);
                }

                if (getTokenMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "SearchKey : " + search_key + ",UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_search(getTokenMessage.Message, getTokenMessage.Status, "", null);
                }
               


                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                if (checkedThreeUserLoggedIn.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "SearchKey : " + search_key + ",UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_search(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                }
                var dataexistornot = Checked_data_existornot_search_products(user_id,search_key);

                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "SearchKey : " + search_key + ",UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_search(dataexistornot.Message, dataexistornot.Status, "", null);
                    }


                if (dataexistornot.Status == "200")
                {
                    var product_catalog = getProduct_Search(user_id,search_key);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "SearchKey : " + search_key + ",UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_Product_search(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, product_catalog);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "SearchKey : " + search_key + ",UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_Product_search(exception.Message, exception.Status, "", null);
            }

            return "";

        }

        [ActionName("GetData_searchproducts")]
        public object GetData_searchproducts(string user_id, string productname, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel, string plus_flag, string browser = null, string Browser_version = null)
        {

            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getAppMessage = getAppversion(app_version, os_type, channel);
                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",ProductName :" + productname + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion

                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_search_data(getAppMessage.Message, getAppMessage.Status, getTokenMessage.Token, null);
                }

                if (getTokenMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",ProductName :" + productname + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_search_data(getTokenMessage.Message, getTokenMessage.Status, "", null);
                }
               

                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                if (checkedThreeUserLoggedIn.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",ProductName :" + productname + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_search_data(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                }
                var dataexistornot = Checked_data_existornot_getdata_searchproduct(user_id,productname, plus_flag);
                if (dataexistornot.Status == "0")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",ProductName :" + productname + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_search_data(dataexistornot.Message, dataexistornot.Status, "", null);
                }
                if (dataexistornot.Status == "200")
                {
                    var product_catalog = getProduct_SearchData(productname, plus_flag, user_id);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",ProductName :" + productname + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_Product_search_data(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, product_catalog);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",ProductName :" + productname + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_Product_search(exception.Message, exception.Status, "", null);
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("Catalog_menu_upper")]
        public object Catalog_menu_upper(string user_id, int productcategoryid, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel, string browser = null, string Browser_version = null)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getAppMessage = getAppversion(app_version, os_type, channel);
                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion

                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_catalog_menu_upper(getAppMessage.Message, getAppMessage.Status, getTokenMessage.Token, null);
                }
                if (getTokenMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_catalog_menu_upper(getTokenMessage.Message, getTokenMessage.Status, "", null);
                }
               
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                if (checkedThreeUserLoggedIn.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_catalog_menu_upper(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                }
                var dataexistornot = Checked_data_existornot_catalog_upper(user_id,productcategoryid);
                if (dataexistornot.Status == "0")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_Product_catalog_menu_upper(dataexistornot.Message, dataexistornot.Status, "", null);
                }


                
                if (dataexistornot.Status == "200")
                {
                    var product_catalog_upper = getProduct_Catalog_Upper(user_id,productcategoryid);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_Product_catalog_menu_upper(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, product_catalog_upper);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_Product_catalog_menu_upper(exception.Message, exception.Status, "", null);
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("Contactus_details")]
        public object Contactus_details(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel, string browser = null, string Browser_version = null)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getAppMessage = getAppversion(app_version, os_type, channel);
                var getTokenMessage = getToken(user_id, app_version, device_id, token);

                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion

                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_contactus_details(getAppMessage.Message, getAppMessage.Status, getTokenMessage.Token, null);
                }


                if (getTokenMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_contactus_details(getTokenMessage.Message, getTokenMessage.Status, "", null);
                }
                
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                if (checkedThreeUserLoggedIn.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_contactus_details(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                }
                var dataexistornot = Checked_data_existornot_contactus_details(user_id);
                if (dataexistornot.Status == "0")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_contactus_details(dataexistornot.Message, dataexistornot.Status, "", null);
                }


               
                if (dataexistornot.Status == "200")
                {
                    var contactus = getContactUs(user_id);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_contactus_details(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, contactus);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_contactus_details(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpPost]
        [ActionName("Save_Contactus_suggestion")]
        public object Save_Contactus_suggestion([FromBody]ContactusImage value)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getAppMessage = getAppversion(value.app_version, value.os_type, value.channel);
                var getTokenMessage = getToken(value.user_id, value.app_version, value.device_id, value.token);

                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + value.user_id + ",Name :" + value.name + ",Email :" + value.email + ",Message :" + value.message + ",Contactusimage :" + value.contactusimage + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion

                    SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                    return getJson_contactus_suggestion(getAppMessage.Message, getAppMessage.Status, getTokenMessage.Token);
                }

                if (getTokenMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + value.user_id + ",Name :" + value.name + ",Email :" + value.email + ",Message :" + value.message + ",Contactusimage :" + value.contactusimage + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                    string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                    #endregion
                    SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                    return getJson_contactus_suggestion(getTokenMessage.Message, getTokenMessage.Status, "");
                }
               

                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(value.user_id, value.device_id);
                if (checkedThreeUserLoggedIn.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + value.user_id + ",Name :" + value.name + ",Email :" + value.email + ",Message :" + value.message + ",Contactusimage :" + value.contactusimage + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                    string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                    #endregion
                    SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                    return getJson_contactus_suggestion(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                }

                var contactus_suggestion = save_contactus_suggetion(value.user_id, value.name, value.email, value.message, value.contactusimage, value.filename);

                
                if (contactus_suggestion.Status == "200" || contactus_suggestion.Status == "0")
                {
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + value.user_id + ",Name :" + value.name + ",Email :" + value.email + ",Message :" + value.message + ",Contactusimage :" + value.contactusimage + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                    string ResponseParameter = "Message : " + contactus_suggestion.Message + ",Status : " + contactus_suggestion.Status + "";
                    #endregion
                    SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 0, contactus_suggestion.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                    //End Save Api log data//

                    return getJson_contactus_suggestion(contactus_suggestion.Message, contactus_suggestion.Status, getTokenMessage.Token);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + value.user_id + ",Name :" + value.name + ",Email :" + value.email + ",Message :" + value.message + ",Contactusimage :" + value.contactusimage + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                return getJson_contactus_suggestion(exception.Message, exception.Status, "");
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("Gallery_menu_upper")]
        public object Gallery_menu_upper(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel, string browser = null, string Browser_version = null)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getAppMessage = getAppversion(app_version, os_type, channel);
                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                if (getAppMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                    #endregion

                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_gallery_menu_upper(getAppMessage.Message, getAppMessage.Status, getTokenMessage.Token, null);
                }
                if (getTokenMessage.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_gallery_menu_upper(getTokenMessage.Message, getTokenMessage.Status, "", null);
                }
                
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                if (checkedThreeUserLoggedIn.Status != "")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_gallery_menu_upper(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                }
                var dataexistornot = Checked_data_existornot(user_id,"Gallery_Menu_Upper", "");
                if (dataexistornot.Status == "0")
                {
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    return getJson_gallery_menu_upper(dataexistornot.Message, dataexistornot.Status, "", null);
                }



               
                if (dataexistornot.Status == "200")
                {
                    var gallery_menu_upper = getGalleryUpperData(user_id);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_gallery_menu_upper(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, gallery_menu_upper);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Browser :" + browser + ",Browser_version :" + Browser_version + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_gallery_menu_upper(exception.Message, exception.Status, "", null);
            }

            return "";

        }
        #endregion


        #region check token valid or not
        public MessageData getToken(string user_id, string app_version, string deviceid, string token)
        {
            string LoginToken = "";


            // string TokenString = user_id + app_version + deviceid;
            MessageData msgdata = new MessageData();
            

            var gettoken = luminous.UserVerifications.Where(c => c.UserId == user_id && c.DeviceId == deviceid).Select(c => new { c.Token, c.TokenFlag }).Count();
            if (user_id == "9900000000")
            {
                msgdata.Message = "";
                msgdata.Status = "";
                msgdata.Token = token;
            }
            else
            {


                if (gettoken > 0)
                {

                    var gettokendata = luminous.UserVerifications.Where(c => c.UserId == user_id && c.DeviceId == deviceid).Select(c => new { c.Token, c.TokenFlag, c.AppVersion }).SingleOrDefault();
                    if (gettokendata.TokenFlag == 1)
                    {

                        if (gettokendata.Token != token)
                        {
                            msgdata.Message = "Unauthorized Access";
                            msgdata.Status = "403";
                            msgdata.Token = "";
                        }
                        else
                        {
                            msgdata.Message = "";
                            msgdata.Status = "";
                            msgdata.Token = gettokendata.Token;
                        }
                    }
                    else
                    {
                        using (MD5 md5Hash = MD5.Create())
                        {
                            string New_Token = (from c in luminous.AppVersions
                                                join d in luminous.UserVerifications
                                                on c.AppOs equals d.OSType
                                                where d.UserId == user_id && d.DeviceId == deviceid
                                                select c.Version).First();


                            string TokenString = user_id + New_Token + deviceid;

                            LoginToken = GetMd5Hash(md5Hash, TokenString);


                        }
                        if (app_version == gettokendata.AppVersion && gettokendata.TokenFlag != 1)
                        {
                            //luminous.ExecuteStoreCommand("Update Userverification set TokenFlag=0 where userid='" + user_id + "' and deviceid='" + deviceid + "'");
                            msgdata.Message = "";
                            msgdata.Status = "";
                            msgdata.Token = LoginToken;
                        }
                        else
                        {
                            var uvdata = luminous.UserVerifications.Single(c => c.UserId == user_id && c.DeviceId == deviceid);

                            Auditlog_Userverificationstatus audit_user = new Auditlog_Userverificationstatus();
                            audit_user.Userverification_Id = uvdata.Id;
                            audit_user.AppVersion = uvdata.AppVersion;
                            audit_user.UserId = uvdata.UserId;
                            audit_user.OSVersion = uvdata.OSVersion;
                            audit_user.Fcm_token = uvdata.Fcm_token;
                            audit_user.OSType = uvdata.OSType;
                            audit_user.DeviceId = uvdata.DeviceId;
                            audit_user.CreatedOn = DateTime.Now;
                            audit_user.CreatedBy = user_id;
                            audit_user.UnBolckedBy = uvdata.UnBolckedBy;
                            audit_user.Status = uvdata.Status;
                            luminous.Auditlog_Userverificationstatus.Add(audit_user);
                            luminous.SaveChanges();


                            luminous.Database.ExecuteSqlCommand("Update Userverification set Token='" + LoginToken + "',AppVersion='" + app_version + "',TokenFlag=1 where userid='" + user_id + "' and deviceid='" + deviceid + "'");

                            msgdata.Message = "";
                            msgdata.Status = "";
                            msgdata.Token = LoginToken;
                        }

                    }
                }
                else
                {
                    msgdata.Message = "Unauthorized Access";
                    msgdata.Status = "403";
                }
            }


            return msgdata;
        }
        //End Token//
        #endregion check token valid or not

        #region appversion valid or not
        public  MessageData getAppversion(string app_version, string ostype, string channel)
        {
            
            int? appVersionCount;
            //if (channel.Contains("Web"))
            //{
            //    appVersionCount = 1;

            //}

            //else
            //{
            appVersionCount = luminous.AppVersions.Where(c => c.Version == app_version && c.AppOs == ostype).Count();

            // }
            MessageData msgdata = new MessageData();
            if (appVersionCount == 0)
            {
                msgdata.Message = "App version is old. Please update your App";
                msgdata.Status = "402";
            }
            else
            {
                msgdata.Message = "";
                msgdata.Status = "";
            }
            return msgdata;
        }
        #endregion


        #region Checked data exist or not customer permission
        public MessageData Checked_data_existornot_customerpermission_data(string userid, string language)
        {
            
            MessageData msgdata = new MessageData();

            List<getCustomerPermission_New_Result> getPerm = luminous.getCustomerPermission_New(userid, language).ToList();

            if (getPerm.Count == 0)
            {
                msgdata.Message = "No Data Found";
                msgdata.Status = "0";
            }
            else
            {
                msgdata.Status = "200";
                msgdata.Message = "Data  fetched successfully.";
            }



            return msgdata;
        }
        #endregion

        #region Checked data exist or not catalog_products
        public MessageData Checked_data_existornot_catalog_products(string userid,int productcategoryid)
        {
            var usertype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => new { c.CustomerType, c.CreatedBY }).SingleOrDefault();
            if (usertype.CustomerType == "Dealer" || usertype.CustomerType == "DEALER")
            {
                userid = usertype.CreatedBY;
            }
            MessageData msgdata = new MessageData();

            var getproduct_catalog = (from prdcat in luminous.ProductLevelThrees
                                      join prdimages in luminous.ProductthreeImageMappings
                                      on prdcat.id equals prdimages.ProductLevelThreeid
                                      where prdcat.productCategoryid == productcategoryid && prdcat.CreatedBy==userid && prdcat.PlTwStatus == 1
                                      select new
                                      {
                                          Id = prdcat.id,
                                          productimages = prdimages.Primage


                                      }
                ).ToList();

            if (getproduct_catalog.Count == 0)
            {
                msgdata.Message = "No Data Found";
                msgdata.Status = "0";
            }
            else
            {
                msgdata.Status = "200";
                msgdata.Message = "Data  fetched successfully.";
            }



            return msgdata;
        }
        #endregion

        #region Checked data exist or not search_products
        public MessageData Checked_data_existornot_search_products(string userid,string search_key)
        {
           
            MessageData msgdata = new MessageData();
            var usertype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => new { c.CustomerType, c.CreatedBY }).SingleOrDefault();
            if (usertype.CustomerType == "Dealer" || usertype.CustomerType == "DEALER")
            {
                userid = usertype.CreatedBY;
            }

            var getproduct_catalog = (from prdcat in luminous.ProductLevelThrees
                                      where prdcat.PlTwStatus == 1 && prdcat.CreatedBy==userid && prdcat.Name.Contains(search_key)
                                      select new
                                      {
                                          Id = prdcat.id,
                                          productname = prdcat.Name


                                      }
                ).ToList();

            if (getproduct_catalog.Count == 0)
            {
                msgdata.Message = "No Data Found";
                msgdata.Status = "0";
            }
            else
            {
                msgdata.Status = "200";
                msgdata.Message = "Data  fetched successfully.";
            }



            return msgdata;
        }
        #endregion

        #region Checked data exist or not search_products_data
        public MessageData Checked_data_existornot_getdata_searchproduct(string userid,string productname, string plus_flag)
        {
            string replacePlus = "";
            if (plus_flag == "1")
            {
                replacePlus = productname.Replace("plus", "+");
            }
            else
            {
                replacePlus = productname;
            }
            
            MessageData msgdata = new MessageData();
            var usertype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => new { c.CustomerType, c.CreatedBY }).SingleOrDefault();
            if (usertype.CustomerType == "Dealer" || usertype.CustomerType == "DEALER")
            {
                userid = usertype.CreatedBY;
            }

            var getproduct_catalog = (from prdcat in luminous.ProductLevelThrees
                                      where prdcat.Name == replacePlus && prdcat.CreatedBy==userid && prdcat.PlTwStatus == 1
                                      select new
                                      {
                                          Id = prdcat.id,
                                          productname = prdcat.Name


                                      }
                ).ToList();

            if (getproduct_catalog.Count == 0)
            {
                msgdata.Message = "No Data Found";
                msgdata.Status = "0";
            }
            else
            {
                msgdata.Status = "200";
                msgdata.Message = "Data  fetched successfully.";
            }



            return msgdata;
        }
        #endregion

        #region Checked data exist or not catalog_uppper
        public MessageData Checked_data_existornot_catalog_upper(string userid,int productcategoryid)
        {
           
            MessageData msgdata = new MessageData();

            var usertype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => new { c.CustomerType, c.CreatedBY }).SingleOrDefault();
            if (usertype.CustomerType == "Dealer" || usertype.CustomerType == "DEALER")
            {
                userid = usertype.CreatedBY;
            }


            var getproduct_catalog = luminous.getCatalog_Upper(userid,productcategoryid).ToList();

            if (getproduct_catalog.Count == 0)
            {
                msgdata.Message = "No Data Found";
                msgdata.Status = "0";
            }
            else
            {
                msgdata.Status = "200";
                msgdata.Message = "Data  fetched successfully.";
            }



            return msgdata;
        }
        #endregion

        #region Checked data exist or not contact_us_details
        public MessageData Checked_data_existornot_contactus_details(string userid)
        {
            
            MessageData msgdata = new MessageData();

            var contactusdata = luminous.contactUsDetails.Where(c => c.Cstatus == 1 && c.CreatedBy==userid).Count();

            if (contactusdata == 0)
            {
                msgdata.Message = "No Data Found";
                msgdata.Status = "0";
            }
            else
            {
                msgdata.Status = "200";
                msgdata.Message = "Data  fetched successfully.";
            }



            return msgdata;
        }
        #endregion

        #region Checked data exist or not by page name
        public MessageData Checked_data_existornot(string userid,string pagename, string parentid)
        {
            LuminousMpartnerIBEntities luminous = new LuminousMpartnerIBEntities();
            MessageData msgdata = new MessageData();
            var usertype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => new { c.CustomerType, c.CreatedBY }).SingleOrDefault();
            if (usertype.CustomerType == "Dealer" || usertype.CustomerType == "DEALER")
            {
                userid = usertype.CreatedBY;
            }
            if (pagename == "ProductCategory")
            {
                var getProductcategory = luminous.ProductCatergories.Where(c => c.Pstatus == 1 && c.CreatedBy==userid).Select(c => new { c.id, c.PName, c.PdfSystemName }).ToList();

                if (getProductcategory.Count == 0)
                {
                    msgdata.Message = "No Data Found";
                    msgdata.Status = "0";
                }
                else
                {
                    msgdata.Status = "200";
                    msgdata.Message = "Data  fetched successfully.";
                }
                return msgdata;

            }
            if (pagename == "Catalog_MainPdf")
            {
                var getProductcategory = luminous.catalog_MainPdf.Where(c => c.Status == 1).Select(c => new { c.Id, c.CategoryName, c.PdfName, c.PdfUrl }).ToList();

                if (getProductcategory.Count == 0)
                {
                    msgdata.Message = "No Data Found";
                    msgdata.Status = "0";
                }
                else
                {
                    msgdata.Status = "200";
                    msgdata.Message = "Data  fetched successfully.";
                }
                return msgdata;

            }


            if (pagename == "ParentCategory")
            {
                var getparentcategory = luminous.ParentCategories.Where(c => c.PCStatus == 1).Select(c => new { c.Pcid, c.PCName }).ToList();

                if (getparentcategory.Count == 0)
                {
                    msgdata.Message = "No Data Found";
                    msgdata.Status = "0";
                }
                else
                {
                    msgdata.Status = "200";
                    msgdata.Message = "Data  fetched successfully.";
                }
                return msgdata;

            }
            if (pagename == "Gallery_Menu_Upper")
            {



                var getGallery_Menu_Footer = luminous.FooterCategories.Where(c => c.Status == true && c.CatType == "Media").Count();

                if (getGallery_Menu_Footer == 0)
                {
                    msgdata.Message = "No Data Found";
                    msgdata.Status = "0";
                }
                else
                {
                    msgdata.Status = "200";
                    msgdata.Message = "Data  fetched successfully.";
                }



                return msgdata;
            }
            if (pagename == "Faq")
            {



                var currentdate = DateTime.Now.Date;
                var getFaq = (from c in luminous.FAQs
                              where c.StartDate <= currentdate && c.EndDate >= currentdate && c.Status == 1
                              select new
                              {
                                  id = c.Id,
                                  question = c.QuestionName,
                                  answer = c.Answer

                              }).Count();


                if (getFaq == 0)
                {
                    msgdata.Message = "No Data Found";
                    msgdata.Status = "0";
                }
                else
                {
                    msgdata.Status = "200";
                    msgdata.Message = "Data  fetched successfully.";
                }



                return msgdata;
            }
            if (pagename == "Media")
            {




                var getMedia = luminous.MediaDatas.Where(c => c.Status == 1 && c.PageFlag == "Media").Count();


                if (getMedia == 0)
                {
                    msgdata.Message = "No Data Found";
                    msgdata.Status = "0";
                }
                else
                {
                    msgdata.Status = "200";
                    msgdata.Message = "Data  fetched successfully.";
                }
                return msgdata;
            }

            else
            {
                var currentdate = DateTime.Now.Date;

                var getHomePage = (from c in luminous.Card_dynamicPage
                                   where c.Startdate <= currentdate && c.Enddate >= currentdate && c.Pagename == pagename && c.Subcatid == parentid && c.Status == 1
                                   select new
                                   {
                                       id = c.Id,
                                       name = c.CardProviderName,
                                       class_name = c.ClassName,
                                       card_action = c.CardAction_Deeplink,
                                       background_image = c.ImageSystemName,
                                       image_height = c.Height,
                                       image_width = c.Width,
                                       title = c.Title,
                                       title_color = c.TitleColour,
                                       sub_title = c.Sub_Title,
                                       subtitle_color = c.Sub_TitleColour,
                                       action1_color = c.Action1_Colour,
                                       action1_text = c.Action1_Text,
                                       carddataflag = c.CardDataFlag
                                   }).ToList();
                //var getHomePage = luminous.GetHomePage_Parent_ByUserId(userid, "Homepage").Where(c => c.Startdate <= currentdate && c.Enddate >= currentdate && c.Subcatid == parentid && c.status == 1).ToList();

                if (getHomePage.Count == 0)
                {
                    msgdata.Message = "No Data Found";
                    msgdata.Status = "0";
                }
                else
                {
                    msgdata.Status = "200";
                    msgdata.Message = "Data  fetched successfully.";
                }
                return msgdata;
            }

        }
        #endregion



        #region check three user logged in
        public MessageData checked_ThreeUserLoggedIn(string user_id, string device_id)
        {
           
            var checkstatus = luminous.UserVerifications.Where(c => c.Status == 1 && c.DeviceId == device_id && c.UserId == user_id).Count();
            MessageData msgdata = new MessageData();
            if (checkstatus != 0)
            {
                msgdata.Message = "Your device for this particular user id has been deactivated";
                msgdata.Status = "406";
            }
            else
            {
                msgdata.Message = "";
                msgdata.Status = "";
            }
            return msgdata;
        }
        #endregion


        #region JSON Login Authentication

        public object getJson_LoginAuthentication(string message, string status)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        public object getJson_OTPAuthentication(string message, string status, string token, List<Userprofile> get_userprofile)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                get_user_profile = get_userprofile
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        public object getJson_customer_permission(string message, string status, string token, List<UserPermission> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                permission_data = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        public object getJson_CardProvider(string message, string status, string token, List<HomePage> carddata, string pagename)
        {
            if (pagename == "HomePage")
            {
                var json = JsonConvert.SerializeObject(new
                {
                    Message = message,
                    Status = status,
                    Token = token,
                    dynamic_home_page = carddata
                });

                //Create a HTTP response - Set to OK
                var res = Request.CreateResponse(HttpStatusCode.OK);
                res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                //Return the Response
                return res;
            }
            if (pagename == "Scheme")
            {
                var json = JsonConvert.SerializeObject(new
                {
                    Message = message,
                    Status = status,
                    Token = token,
                    scheme_data = carddata
                });

                //Create a HTTP response - Set to OK
                var res = Request.CreateResponse(HttpStatusCode.OK);
                res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                //Return the Response
                return res;
            }
            if (pagename == "Price")
            {
                var json = JsonConvert.SerializeObject(new
                {
                    Message = message,
                    Status = status,
                    Token = token,
                    Price_data = carddata
                });

                //Create a HTTP response - Set to OK
                var res = Request.CreateResponse(HttpStatusCode.OK);
                res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                //Return the Response
                return res;
            }
            if (pagename == "ProductCategory")
            {
                var json = JsonConvert.SerializeObject(new
                {
                    Message = message,
                    Status = status,
                    Token = token,
                    productcategory_data = carddata
                });

                //Create a HTTP response - Set to OK
                var res = Request.CreateResponse(HttpStatusCode.OK);
                res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                //Return the Response
                return res;
            }
            if (pagename == "Connect+")
            {
                var json = JsonConvert.SerializeObject(new
                {
                    Message = message,
                    Status = status,
                    Token = token,
                    connectplus_data = carddata
                });

                //Create a HTTP response - Set to OK
                var res = Request.CreateResponse(HttpStatusCode.OK);
                res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                //Return the Response
                return res;
            }
            if (pagename == "ewarranty")
            {
                var json = JsonConvert.SerializeObject(new
                {
                    Message = message,
                    Status = status,
                    Token = token,
                    ewarranty_data = carddata
                });

                //Create a HTTP response - Set to OK
                var res = Request.CreateResponse(HttpStatusCode.OK);
                res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                //Return the Response
                return res;
            }

            return "";
        }

        public object getJson_ParentCategory(string message, string status, string token, List<LuminousMpartnerIB.MpartnerIB_Api.Model.ParentCategory> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                parent_category = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }
        public object getJson_ProductCategory(string message, string status, string token, List<ProductsCategory> carddata, string pagename)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                product_category = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        public object getJson_Product_catalog(string message, string status, string token, List<ProductCatalog> carddata, string pagename)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                product_catalog = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        public object getJson_Product_search(string message, string status, string token, List<Product_Search> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                product_search = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        public object getJson_Product_search_data(string message, string status, string token, List<Product_Searchdata> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                product_search_data = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        public object getJson_Product_catalog_menu_upper(string message, string status, string token, List<Catalog_Menu_Upper> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                product_catalog_upper = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        public object getJson_contactus_details(string message, string status, string token, List<ContactUs> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                contactus = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        public object getJson_contactus_suggestion(string message, string status, string token)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token
                //contactus_suggestion = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        public object getJson_gallery_menu_upper(string message, string status, string token, List<Gallery_Menu_Upper> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                gallery_menu_upper = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }
        #endregion

        #region check try catch exception
        public MessageData getTryCatchExc()
        {
            MessageData msgdata = new MessageData();
            msgdata.Message = "Some Exception has occurred";

            msgdata.Status = "201";

            return msgdata;
        }
        #endregion

        #region MainFunction

        public MessageData func_LoginCreateOtp(string user_id, string device_id, string os_version_code, string device_name, string app_version)
        {

            MessageData msg = new MessageData();
            try
            {
               
                var check_electexistornot = luminous.UsersLists.Where(c => c.UserId == user_id && c.isActive == 1).Count();


                var user_exist = check_userexist_or_not(user_id, device_id, os_version_code, device_name, app_version);
                msg.Status = user_exist.Status;
                msg.Message = user_exist.Message;

                // System.Data.Linq<Sp_MHrCreateOtp_Result> mhrCreateOtpResult = db.Sp_MHrCreateOtp(empid, imeinumber, osversion, devicename, appversion);

                // System.Data.Linq.ISingleResult<Sp_MHrCreateOtpResult> mhrCreateOtpResult = lead.Sp_MHrCreateOtp(empid, imeinumber, osversion, devicename, appversion);

            }
            catch (Exception ex)
            {
                MessageData ip = new MessageData();
                msg.Status = ex.ToString();
                msg.Message = ex.ToString();
                //LogException(ex, "LuminiousLogInCreateOtp, empid | " + empid + ", imeinumber | " + imeinumber + ", osversion | " + osversion + ", devicename | " + devicename + ", appversion | " + appversion);


            }
            return msg;
        }

        public MessageData check_userexist_or_not(string user_id, string device_id, string os_version_code, string device_name, string app_version)
        {
            
            MessageData msg = new MessageData();
            var userexist = luminous.UsersLists.Where(c => c.UserId == user_id && c.isActive == 1).Count();
            if (userexist > 0)
            {
                var mhrCreateOtpResult = luminous.Sp_MHrCreateOtp(user_id, device_id, os_version_code, device_name, app_version);
                if (mhrCreateOtpResult != null)
                {
                    SMSbyWebservice sms = SMSbyWebservice.Instance;
                    var smsTemp = mhrCreateOtpResult.Where(m => m != null && !String.IsNullOrEmpty(m.Employee_Mob))
                    .Select(m => new
                    {
                        EmployeeId = m.Emp_Code,
                        MobileNumber = m.Employee_Mob,
                        OTP = m.otp,
                        Biker_Name = m.Employee_Name,
                        Status = m.Status,
                        Message = m.Message
                    }).FirstOrDefault();

                    if (smsTemp != null)
                    {
                        sms.EmployeeId = smsTemp.EmployeeId;
                        sms.MobileNumber = smsTemp.MobileNumber;
                        sms.OTP = smsTemp.OTP;
                        string returnValue = sms.SendSMS();
                        //string returnValue = otpDb.sendsms(sms.MobileNumber, BodyMessage(sms.Biker_Name, sms.OTP), sms.Biker_Name, "pass@1234");// 

                        msg.Status = smsTemp.Status;
                        msg.Message = smsTemp.Message;
                        //string url = "http://50.62.56.149:5286/service1.asmx?op=LuminousLogInCreateOtp";
                        // string url = "http://mapps.luminousindia.com/LeadWebService/service1.asmx?op=LuminousLogInCreateOtp";
                        //string reqparameter = "empid :" + user + ",imeinumber :" + imeinumber + ",devicename :" + devicename + ",appversion :" + appversion + " ";
                        //string resparameter = "Message :" + msg.Code + ",Description :" + msg.des + "";
                        // Luminious_ReqResp(empid, url, reqparameter, resparameter, 0, System.DateTime.Now, empid);
                        return msg;
                    }

                    //if (!String.IsNullOrEmpty(dt.Rows[0][1].ToString()))
                    //{
                    //    string body = BodyMessage(dt.Rows[0][1].ToString(), dt.Rows[0][4].ToString());
                    //    mailsql m2 = new mailsql(dt.Rows[0][2].ToString(), "not", "not", "Luminous_mobileApp One Time Password", body, "");
                    //    m2.sendMaildb();
                    //}
                }
                else
                {
                    msg.Status = "0";
                    msg.Message = "Invalid Employee";
                    return msg;
                }
            }
            else
            {
                msg.Status = "0";
                msg.Message = "Invalid Employee";
                return msg;
            }
            return msg;
        }



        public MessageData func_OTPAuthentication(string user_id, string device_id, string os_version_code, string device_name, string otp, string os_version_name, string os_type, string app_version)
        {
        
            MessageData msg = new MessageData();
            try
            {
                string LoginToken = "";
                string TokenString = user_id + app_version + device_id;

                using (MD5 md5Hash = MD5.Create())
                {

                    LoginToken = GetMd5Hash(md5Hash, TokenString);


                }
                var mhrVarifyOtpNotificationResult =
                        luminous.MHrVarifyOtpNotification(user_id, device_id, os_version_code, device_name, otp, os_version_name, device_id, os_type);
                if (mhrVarifyOtpNotificationResult != null)
                {
                    //userdatacode//



                    MessageData inputMessage = mhrVarifyOtpNotificationResult.Where(m => m != null)
                        .Select(m => new MessageData { Status = m.Code, Message = m.Message }).FirstOrDefault();
                    if (inputMessage != null)
                    {
                        //msg.ModuleName = inputMessage.ModuleName;
                        //msg.Permission = inputMessage.Permission;
                        //msg.StateId = inputMessage.StateId;
                        //msg.Statename = inputMessage.Statename;
                        //msg.UserName = inputMessage.UserName;
                        //msg.UserType = inputMessage.UserType;
                        msg.Status = inputMessage.Status;
                        msg.Message = inputMessage.Message;
                        msg.Token = LoginToken;
                        //luminous.Database.ExecuteSqlCommand("Update EmployeeMaster set Token='" + LoginToken + "',TokenFlag=1,DeviceId='" + device_id + "',Appversion='" + app_version + "' where EmployeeId='" + user_id + "'");

                    }
                    else
                    {
                        //msg.ModuleName ="0";
                        //msg.Permission = "0";
                        //msg.StateId = "0";
                        //msg.Statename = "0";
                        //msg.UserName = "0";
                        //msg.UserType = "0";
                        msg.Status = inputMessage.Status;
                        msg.Message = inputMessage.Message;
                        msg.Token = "";


                    }
                }
                else
                {
                    //msg.ModuleName = "0";
                    //msg.Permission = "0";
                    //msg.StateId = "0";
                    //msg.Statename = "0";
                    //msg.UserName = "0";
                    //msg.UserType = "0";
                    msg.Status = "ERROR";
                    msg.Message = "Please try again";
                    msg.Token = "";
                }
            }
            catch (Exception ex)
            {

            }
            return msg;
        }

        public List<UserPermission> getcustomer_permission(string userid, string language)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
           
            //Get Home Page List
            List<UserPermission> permissionlist = new List<UserPermission>();
            MessageData msgdata = new MessageData();
            //Get Card Data List

            try
            {
                List<getCustomerPermission_New_Result> getPerm = luminous.getCustomerPermission_New(userid, language).ToList();





                foreach (var data in getPerm)
                {

                    UserPermission obj_permission = new UserPermission();

                    obj_permission.CustomerType = data.Usertype;

                    if (data.ModuleName == "")
                    {
                        obj_permission.ModuleName = "";
                    }
                    else
                    {
                        obj_permission.ModuleName = data.ModuleName;
                    }
                    if (data.Language == "" || data.Language == null)
                    {
                        obj_permission.ModuleText = "";
                    }
                    else
                    {
                        obj_permission.ModuleText = data.Language;
                    }
                    obj_permission.Permission = data.Permission.ToString();
                    if (data.ModuleImage == "0")
                    {
                        obj_permission.ModuleImage = "0";
                    }
                    else
                    {
                        obj_permission.ModuleImage = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "AppMenuIcon/" + data.ModuleImage;


                    }

                    permissionlist.Add(obj_permission);


                }

                // }




            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return permissionlist;
        }

        public List<HomePage> getCardprovider(string userid, string pagename, string parentid, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousMpartnerIBEntities luminous = new LuminousMpartnerIBEntities();
            //Get Home Page List
            List<HomePage> HomepageData = new List<HomePage>();
            MessageData msgdata = new MessageData();

            

            try
            {
                
                var usertype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => new { c.CustomerType, c.CreatedBY }).SingleOrDefault();
                if(usertype.CustomerType=="Dealer" || usertype.CustomerType=="DEALER" )
                {
                    userid = usertype.CreatedBY;
                }
                var currentdate = DateTime.Now.Date;
                if (pagename != "Scheme" && pagename != "Price")
                {

                    var getHomePage = (from c in luminous.Card_dynamicPage
                                       where c.Startdate <= currentdate && c.Enddate >= currentdate && c.Pagename == pagename && c.Subcatid == parentid && c.Status == 1
                                       && c.CreatedBy==userid
                                       
                                       select new
                                       {
                                           id = c.Id,
                                           name = c.CardProviderName,
                                           class_name = c.ClassName,
                                           card_action = c.CardAction_Deeplink,
                                           background_image = c.ImageSystemName,
                                           image_height = c.Height,
                                           image_width = c.Width,
                                           title = c.Title,
                                           title_color = c.TitleColour,
                                           sub_title = c.Sub_Title,
                                           subtitle_color = c.Sub_TitleColour,
                                           action1_color = c.Action1_Colour,
                                           action1_text = c.Action1_Text,
                                           carddataflag = c.CardDataFlag,
                                           pagename = c.Pagename,
                                           pdfurl = c.PdfOriginalName,
                                           subcategory = c.Subcatname,
                                           mainimage = c.SystemMainImage,
                                           status = c.Status,
                                           sequence = c.Sequence,
                                           createdby=c.CreatedBy
                                       }).OrderBy(c => c.sequence).ToList();

                    //var getHomePage = luminous.GetHomePage_Parent_ByUserId(userid, pagename).Where(c => c.Startdate <= currentdate && c.Enddate >= currentdate && c.Subcatid == parentid && c.status == 1).ToList();

                    if (getHomePage.Count > 0)
                    {
                        //Checked_data_existornot(pagename);
                        foreach (var data in getHomePage)
                        {

                            HomePage pr = new HomePage();
                            pr.name = data.name;

                            if (data.class_name == "")
                            {
                                pr.class_name = "";
                            }
                            else
                            {
                                pr.class_name = data.class_name;
                            }

                            if (data.pagename == "")
                            {
                                pr.current_page = "";
                            }
                            else
                            {
                                pr.current_page = data.pagename;
                            }

                            if (data.card_action == "")
                            {
                                pr.card_action = "";
                            }
                            else
                            {
                                //if (data.card_action == "Lucky7" || data.card_action == "Lucky7Dealer")
                                //{
                                //    var custtype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.CustomerType).SingleOrDefault();
                                //    if (data.card_action == "Lucky7")
                                //    {
                                //        if (custtype == "DISTY")
                                //        {
                                //            pr.card_action = data.card_action;
                                //        }
                                //        else
                                //        {
                                //            pr.card_action = "Lucky7Dealer";
                                //        }
                                //    }
                                //    if (data.card_action == "Lucky7Dealer")
                                //    {
                                //        if (custtype == "DISTY")
                                //        {
                                //            pr.card_action = "Lucky7";
                                //        }
                                //        else
                                //        {
                                //            pr.card_action = data.card_action;
                                //        }
                                //    }

                                //}

                                if (data.card_action == "ws_dist" || data.card_action == "ws_dealer")
                                {
                                    var custtype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.CustomerType).SingleOrDefault();
                                    if (data.card_action == "ws_dist")
                                    {
                                        if (custtype == "DISTY")
                                        {
                                            pr.card_action = data.card_action;
                                        }
                                        else
                                        {
                                            pr.card_action = "ws_dealer";
                                        }
                                    }
                                    if (data.card_action == "ws_dealer")
                                    {
                                        if (custtype == "DISTY")
                                        {
                                            pr.card_action = "ws_dist";
                                        }
                                        else
                                        {
                                            pr.card_action = data.card_action;
                                        }
                                    }

                                }


                                else
                                {
                                    pr.card_action = data.card_action;
                                }



                            }

                            if (data.pagename == "Price")
                            {
                                pr.subcategory = data.subcategory;
                            }
                            else
                            {
                                pr.subcategory = "";
                            }
                            if (data.background_image == "")
                            {
                                pr.background_image = "";
                            }
                            else
                            {
                                if (channel.Contains("Web"))
                                {
                                    pr.background_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerIB_Api/WebImages/" + data.background_image;

                                }
                                else
                                {
                                    pr.background_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerIB_Api/CardImage/" + data.background_image;

                                }



                            }
                            if (data.image_height == "")
                            {
                                pr.image_height = "";
                            }
                            else
                            {
                                pr.image_height = data.image_height;
                            }
                            if (data.image_width == "")
                            {
                                pr.image_width = "";
                            }
                            else
                            {
                                pr.image_width = data.image_width;
                            }
                            if (data.mainimage == "")
                            {
                                pr.main_image = "";
                            }
                            else
                            {

                                pr.main_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerIB_Api/CardImage/" + data.mainimage;



                            }
                            if (data.title == "")
                            {
                                pr.title = "";
                            }
                            else
                            {
                                pr.title = data.title;
                            }
                            if (data.title_color == "")
                            {
                                pr.title_color = "";
                            }
                            else
                            {
                                pr.title_color = data.title_color;
                            }
                            if (data.sub_title == "")
                            {
                                pr.sub_title = "";
                            }
                            else
                            {
                                pr.sub_title = data.sub_title;
                            }
                            if (data.subtitle_color == "")
                            {
                                pr.subtitle_color = "";
                            }
                            else
                            {
                                pr.subtitle_color = data.subtitle_color;
                            }

                            if (data.action1_color == "")
                            {
                                pr.action1_color = "";
                            }
                            else
                            {
                                pr.action1_color = data.action1_color;
                            }

                            if (data.carddataflag == "1" || data.carddataflag == "2")
                            {
                                var carddata = (from c in luminous.Card_dynamicPage
                                                join ccd in luminous.Card_CardData
                                                    on c.Id equals ccd.DynamicHomePageId
                                                where ccd.DynamicHomePageId == data.id && c.Startdate <= currentdate && c.Enddate >= currentdate && ccd.Status == 1

                                                select new
                                                {

                                                    title = ccd.Title,
                                                    card_action = ccd.DeepLink,
                                                    background_image = ccd.Background_image,
                                                    main_image = ccd.Main_image,
                                                    image_height = ccd.Image_height,
                                                    image_width = ccd.Image_width,
                                                }).ToList();

                                //var carddata = luminous.GetHomePage_Child_ByUserId(userid, pagename).Where(c => c.DynamicHomePageId == data.id && c.Startdate <= currentdate && c.Enddate >= currentdate && c.Status == 1).ToList();

                                List<Bannerdata> bannerdata = new List<Bannerdata>();
                                //Get Card Data List
                                List<Carddata> crddata = new List<Carddata>();
                                foreach (var cdata in carddata)
                                {


                                    if (data.carddataflag == "1")
                                    {
                                        Carddata Cdata = new Carddata();
                                        Cdata.title = cdata.title;


                                        Cdata.background_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerIB_Api/CardImage/" + cdata.background_image;

                                        Cdata.main_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerIB_Api/CardImage/" + cdata.main_image;

                                       




                                        //if (cdata.card_action == "Lucky7" || cdata.card_action == "Lucky7Dealer")
                                        //{
                                        //    var custtype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.CustomerType).SingleOrDefault();
                                        //    if (cdata.card_action == "Lucky7")
                                        //    {
                                        //        if (custtype == "DISTY")
                                        //        {
                                        //            Cdata.card_action = cdata.card_action;
                                        //        }
                                        //        else
                                        //        {
                                        //            Cdata.card_action = "Lucky7Dealer";
                                        //        }
                                        //    }
                                        //    if (cdata.card_action == "Lucky7Dealer")
                                        //    {
                                        //        if (custtype == "DISTY")
                                        //        {
                                        //            Cdata.card_action = "Lucky7";
                                        //        }
                                        //        else
                                        //        {
                                        //            Cdata.card_action = cdata.card_action;
                                        //        }
                                        //    }

                                        //}
                                        //else
                                        //{
                                            Cdata.card_action = cdata.card_action;
                                      //  }



                                        if (cdata.image_height == "")
                                        {
                                            Cdata.image_height = "";
                                        }
                                        else
                                        {
                                            Cdata.image_height = cdata.image_height;
                                        }
                                        if (cdata.image_width == "")
                                        {
                                            Cdata.image_width = "";
                                        }
                                        else
                                        {
                                            Cdata.image_width = cdata.image_width;
                                        }
                                        crddata.Add(Cdata);

                                        pr.card_data = crddata;
                                    }
                                    if (data.carddataflag == "2")
                                    {
                                        // crddata.Add(Cdata);
                                        //Get Banner Data List

                                        Bannerdata Cdata = new Bannerdata();
                                        Cdata.title = cdata.title;


                                        Cdata.background_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerIB_Api/CardImage/" + cdata.background_image;

                                      


                                        if (cdata.main_image == "")
                                        {
                                            Cdata.main_image = "";
                                        }
                                        else
                                        {
                                            Cdata.main_image = cdata.main_image;
                                        }


                                        //if (cdata.card_action == "Lucky7" || cdata.card_action == "Lucky7Dealer")
                                        //{
                                        //    var custtype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.CustomerType).SingleOrDefault();
                                        //    if (cdata.card_action == "Lucky7")
                                        //    {
                                        //        if (custtype == "DISTY")
                                        //        {
                                        //            Cdata.card_action = cdata.card_action;
                                        //        }
                                        //        else
                                        //        {
                                        //            Cdata.card_action = "Lucky7Dealer";
                                        //        }
                                        //    }
                                        //    if (cdata.card_action == "Lucky7Dealer")
                                        //    {
                                        //        if (custtype == "DISTY")
                                        //        {
                                        //            Cdata.card_action = "Lucky7";
                                        //        }
                                        //        else
                                        //        {
                                        //            Cdata.card_action = cdata.card_action;
                                        //        }
                                        //    }

                                        //}


                                        //else
                                        //{
                                            Cdata.card_action = cdata.card_action;
                                       // }




                                        if (cdata.image_height == "")
                                        {
                                            Cdata.image_height = "";
                                        }
                                        else
                                        {
                                            Cdata.image_height = cdata.image_height;
                                        }
                                        if (cdata.image_width == "")
                                        {
                                            Cdata.image_width = "";
                                        }
                                        else
                                        {
                                            Cdata.image_width = cdata.image_width;
                                        }
                                        bannerdata.Add(Cdata);

                                        pr.Bannercard_data = bannerdata;
                                    }

                                }


                            }
                            if (data.action1_text == "")
                            {
                                pr.action1_text = "";
                            }
                            else
                            {
                                pr.action1_text = data.action1_text;
                            }


                            HomepageData.Add(pr);


                        }

                        // }

                    }
                    else
                    {
                        Checked_data_existornot(userid,pagename, "");
                    }

                }
                else
                    if (pagename == "Scheme" || pagename == "Price")
                    {

                        var getprice_scheme_data = luminous.GetPrice_SchemeByUserId(userid, pagename).Where(c => c.Startdate <= currentdate && c.Enddate >= currentdate && c.Pagename == pagename && c.status == 1 && c.Subcatid == parentid && c.CreatedBy==userid).ToList();

                        var getHomePage = (from c in getprice_scheme_data

                                           select new
                                           {
                                               id = c.Id,
                                               name = c.CardProviderName,
                                               class_name = c.ClassName,
                                               card_action = c.CardAction_Deeplink,
                                               background_image = c.ImageSystemName,
                                               image_height = c.Height,
                                               image_width = c.Width,
                                               title = c.Title,
                                               title_color = c.TitleColour,
                                               sub_title = c.Sub_Title,
                                               subtitle_color = c.Sub_TitleColour,
                                               action1_color = c.Action1_Colour,
                                               action1_text = c.Action1_Text,
                                               carddataflag = c.CardDataFlag,
                                               pagename = c.Pagename,
                                               pdfurl = c.PdfOriginalName,
                                               subcategory = c.Subcatname,
                                               mainimage = c.SystemMainImage,
                                               status = c.status,
                                               sequence = c.Sequence
                                           }).OrderBy(c => c.sequence).ToList();

                        if (getHomePage.Count > 0)
                        {
                            //Checked_data_existornot(pagename);
                            foreach (var data in getHomePage)
                            {

                                HomePage pr = new HomePage();
                                pr.name = data.name;

                                if (data.class_name == "")
                                {
                                    pr.class_name = "";
                                }
                                else
                                {
                                    pr.class_name = data.class_name;
                                }

                                if (data.card_action == "")
                                {
                                    pr.card_action = "";
                                }
                                else
                                {
                                    pr.card_action = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerIB_Api/Pdf/" + data.pdfurl;
                                }



                                if (data.pagename == "Price")
                                {
                                    pr.subcategory = data.subcategory;
                                }
                                else
                                {
                                    pr.subcategory = "";
                                }
                                if (data.background_image == "")
                                {
                                    pr.background_image = "";
                                }
                                else
                                {
                                   
                                        pr.background_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerIB_Api/CardImage/" + data.background_image;


                                }
                                if (data.image_height == "")
                                {
                                    pr.image_height = "";
                                }
                                else
                                {
                                    pr.image_height = data.image_height;
                                }
                                if (data.image_width == "")
                                {
                                    pr.image_width = "";
                                }
                                else
                                {
                                    pr.image_width = data.image_width;
                                }
                                if (data.mainimage == "")
                                {
                                    pr.main_image = "";
                                }
                                else
                                {

                                    pr.main_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerIB_Api/CardImage/" + data.mainimage;

                                 


                                }
                                if (data.title == "")
                                {
                                    pr.title = "";
                                }
                                else
                                {
                                    pr.title = data.title;
                                }
                                if (data.title_color == "")
                                {
                                    pr.title_color = "";
                                }
                                else
                                {
                                    pr.title_color = data.title_color;
                                }
                                if (data.sub_title == "")
                                {
                                    pr.sub_title = "";
                                }
                                else
                                {
                                    pr.sub_title = data.sub_title;
                                }
                                if (data.subtitle_color == "")
                                {
                                    pr.subtitle_color = "";
                                }
                                else
                                {
                                    pr.subtitle_color = data.subtitle_color;
                                }

                                if (data.action1_color == "")
                                {
                                    pr.action1_color = "";
                                }
                                else
                                {
                                    pr.action1_color = data.action1_color;
                                }

                                if (data.action1_text == "")
                                {
                                    pr.action1_text = "";
                                }
                                else
                                {
                                    pr.action1_text = data.action1_text;
                                }


                                HomepageData.Add(pr);


                            }

                            // }

                        }
                        else
                        {
                            Checked_data_existornot(userid,pagename, "");
                        }


                    }



            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return HomepageData;
        }

        public List<LuminousMpartnerIB.MpartnerIB_Api.Model.ParentCategory> func_GetParentCategory()
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);


            List<LuminousMpartnerIB.MpartnerIB_Api.Model.ParentCategory> pcategory = new List<LuminousMpartnerIB.MpartnerIB_Api.Model.ParentCategory>();



            try
            {
                var getparencategory = luminous.ParentCategories.Where(c => c.PCStatus == 1).Select(c => new { c.Pcid, c.PCName, c.PageName, c.PdfUrl }).ToList();



                foreach (var data in getparencategory)
                {

                    LuminousMpartnerIB.MpartnerIB_Api.Model.ParentCategory obj_Pcategory = new LuminousMpartnerIB.MpartnerIB_Api.Model.ParentCategory();



                    obj_Pcategory.Id = data.Pcid;
                    obj_Pcategory.Parentcategoryname = data.PCName;
                    obj_Pcategory.PageName = data.PageName;
                    //   obj_Pcategory.PdfURL = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/Pdf/" + data.PdfUrl;

                    pcategory.Add(obj_Pcategory);



                }
            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return pcategory;
        }

        public List<ProductsCategory> getProductCategory(string userid,string pagename)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            
            //Get Home Page List
            List<ProductsCategory> Pcategorylist = new List<ProductsCategory>();
            MessageData msgdata = new MessageData();
            //Get Card Data List

            try
            {
                var usertype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => new { c.CustomerType, c.CreatedBY }).SingleOrDefault();
                if (usertype.CustomerType == "Dealer" || usertype.CustomerType == "DEALER")
                {
                    userid = usertype.CreatedBY;
                }

                var getProductcategory = luminous.ProductCatergories.Where(c => c.Pstatus == 1 && c.CreatedBy==userid).Select(c => new { c.id, c.PName, c.PdfSystemName, c.ordersequence }).OrderBy(c => c.ordersequence).ToList();

                if (getProductcategory.Count > 0)
                {

                    foreach (var data in getProductcategory)
                    {

                        ProductsCategory pcategory = new ProductsCategory();
                        pcategory.Id = data.id;

                        if (data.PName == "")
                        {
                            pcategory.product_category_name = "";
                        }
                        else
                        {
                            pcategory.product_category_name = data.PName;
                        }

                        if (data.PdfSystemName == "")
                        {
                            pcategory.url_product_category_pdf = "";
                        }
                        else
                        {
                            pcategory.url_product_category_pdf = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerIB_Api/Pdf/" + data.PdfSystemName;
                        }


                        Pcategorylist.Add(pcategory);


                    }

                    // }

                }
                else
                {

                }


            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return Pcategorylist;
        }

        public List<ProductCatalog> getProduct_Catalog(string userid,int productcategoryid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            
            //Get Home Page List
            List<ProductCatalog> Pcataloglist = new List<ProductCatalog>();
            MessageData msgdata = new MessageData();


            try
            {
                var usertype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => new { c.CustomerType, c.CreatedBY }).SingleOrDefault();
                if (usertype.CustomerType == "Dealer" || usertype.CustomerType == "DEALER")
                {
                    userid = usertype.CreatedBY;
                }


                var getproduct_catalog = (from prdcat in luminous.ProductLevelThrees
                                          join prdimages in luminous.ProductthreeImageMappings
                                          on prdcat.id equals prdimages.ProductLevelThreeid
                                          join prd2 in luminous.ProductLevelTwoes
                                       on prdcat.pc_Lv2_oneId equals prd2.id
                                          join attribute in luminous.ProductLevelOnes
                                          on prdcat.ProductLevelOne equals attribute.id into ps
                                          from p in ps.DefaultIfEmpty()
                                          where prdcat.productCategoryid == productcategoryid && prdcat.CreatedBy==userid && prdcat.PlTwStatus == 1
                                          select new
                                          {
                                              Id = prdcat.id,
                                              productimages = prdimages.Primage,
                                              rating = prdcat.Rating,
                                              name = prdcat.Name,
                                              keyfeature = prdcat.KeyFeature,
                                              warrenty = prdcat.Warrenty,
                                              maxcurrentcharge = prdcat.MaximumChargeCurrent,
                                              noofbattery = prdcat.NoOfBattery,
                                              supportedbatterytype = prdcat.SupportedBatteryType,
                                              maxbulbload = prdcat.Maximumbulbload,
                                              technology = prdcat.Technology,
                                              nominalvoltage = prdcat.NominalVoltage,
                                              dimensionmm = prdcat.DimensionMM,
                                              weightfilledbattery = prdcat.Weight_Filled_battery,
                                              prodcatid = prdcat.productCategoryid,
                                              prd2name = prd2.Name,
                                              attribute_name = p.Name,

                                              RatedCapacity = prdcat.RatedCapacity,
                                              FilledWeight = prdcat.FilledWeight,
                                              DCOutputVoltage = prdcat.DCOutputVoltage,
                                              DCOutputCurrent = prdcat.DCOutputCurrent,
                                              MaxSupportedPanelpower = prdcat.MaxSupportedPanelpower,
                                              MaxSolarPanelVoltage = prdcat.MaxSolarPanelVoltage,
                                              VA = prdcat.VA,
                                              NoofCells = prdcat.NoofCells,
                                              PeakPowerPMax = prdcat.PeakPowerPMax,
                                              RatedModuleVoltage = prdcat.RatedModuleVoltage,
                                              MaximumPowerVoltage = prdcat.MaximumPowerVoltage,
                                              MaximumPowerCurrent = prdcat.MaximumPowerCurrent,
                                              NominalDCOutputVoltage = prdcat.NominalDCOutputVoltage,
                                              MaxDCOutputCurrent = prdcat.MaxDCOutputCurrent,
                                              Noof12VBatteriesinSeries = prdcat.Noof12VBatteriesinSeries,
                                              SolarLength = prdcat.SolarLength,
                                              SolarWidth = prdcat.SolarWidth,
                                              Heightuptofloattop = prdcat.Heightuptofloattop,
                                              DryWeight = prdcat.DryWeight,
                                              RatedACpower = prdcat.RatedACpower,
                                              OperatingVoltage = prdcat.OperatingVoltage,
                                              ChargeControllerRating = prdcat.ChargeControllerRating,
                                              NominalBatterybankvoltage = prdcat.NominalBatterybankvoltage,
                                              InputVoltageWorkingRange = prdcat.InputVoltageWorkingRange,
                                              OutputVoltageWorkingRange = prdcat.OutputVoltageWorkingRange,
                                              MainsACLowCut = prdcat.MainsACLowCut,
                                              MainACLowCutRecovery = prdcat.MainACLowCutRecovery,


                                          }
                    ).ToList();

               
                    // Checked_data_existornot(pagename);
                    foreach (var data in getproduct_catalog)
                    {
                        List<Technicalspecification> techspecification = new List<Technicalspecification>();
                       ProductCatalog pcatalog = new ProductCatalog();
                        pcatalog.id = data.Id;
                        var pchildcatalog = luminous.ProductthreeImageMappings.Where(c => c.ProductLevelThreeid == pcatalog.id).Select(c => c.Primage).Take(1).Count();
                        if (pchildcatalog == 0)
                        {
                            if (data.productimages == "")
                            {
                                pcatalog.productcatalog_image_url = "";
                            }
                        }
                        else
                        {
                            var pchildcatalogimg = luminous.ProductthreeImageMappings.Where(c => c.ProductLevelThreeid == pcatalog.id).Select(c => c.Primage).Take(1).SingleOrDefault();
                            pcatalog.productcatalog_image_url = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "ProductImages/" + pchildcatalogimg;
                        }
                        if (data.name == "")
                        {
                            pcatalog.productioncatalog_name = "";
                        }
                        else
                        {
                            pcatalog.productioncatalog_name = data.name;
                        }
                        if (data.rating == "")
                        {
                            pcatalog.productioncatalog_rating = "";
                        }
                        else
                        {
                            pcatalog.productioncatalog_rating = data.rating;
                        }
                        if (data.keyfeature == "")
                        {
                            pcatalog.keyfeature = "";
                        }
                        else
                        {
                            pcatalog.keyfeature = data.keyfeature;
                        }
                        if (data.warrenty == "")
                        {
                            pcatalog.warrenty = "";
                        }
                        else
                        {
                            pcatalog.warrenty = data.warrenty;
                        }
                        if (data.prd2name == "")
                        {
                            pcatalog.productleveltwo = "";
                        }
                        else
                        {
                            pcatalog.productleveltwo = data.prd2name;
                        }
                        if (data.attribute_name == "" || data.attribute_name == null)
                        {
                            pcatalog.attribute_name = "";
                        }
                        else
                        {
                            pcatalog.attribute_name = data.attribute_name;
                        }
                        if (productcategoryid == 30) //HUPS
                        {
                            if (data.maxcurrentcharge == "" || data.maxcurrentcharge == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {

                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Maximum Charging current";
                                techdata.Value = data.maxcurrentcharge;


                                //List<Luminous.MpartnerNewApi.Model.Technicalspecification> techspecification = new List<Luminous.MpartnerNewApi.Model.Technicalspecification>();
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;

                            }

                            if (data.noofbattery == "" || data.noofbattery == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Number of battery";
                                techdata.Value = data.noofbattery;
                                //List<Luminous.MpartnerNewApi.Model.Technicalspecification> techspecification = new List<Luminous.MpartnerNewApi.Model.Technicalspecification>();
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;

                            }

                            if (data.supportedbatterytype == "" || data.supportedbatterytype == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Supported battery type";
                                techdata.Value = data.supportedbatterytype;
                                //List<Luminous.MpartnerNewApi.Model.Technicalspecification> techspecification = new List<Luminous.MpartnerNewApi.Model.Technicalspecification>();
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;

                            }

                            if (data.maxbulbload == "" || data.maxbulbload == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Maximum bulb load";
                                techdata.Value = data.maxbulbload;
                                //List<Luminous.MpartnerNewApi.Model.Technicalspecification> techspecification = new List<Luminous.MpartnerNewApi.Model.Technicalspecification>();
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;

                            }

                            if (data.technology == "" || data.technology == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Technology";
                                techdata.Value = data.technology;
                                //List<Luminous.MpartnerNewApi.Model.Technicalspecification> techspecification = new List<Luminous.MpartnerNewApi.Model.Technicalspecification>();
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;

                            }
                        }
                        if (productcategoryid == 31)
                        {
                            if (data.nominalvoltage == "" || data.nominalvoltage == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Nominal Voltage";
                                techdata.Value = data.nominalvoltage;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.dimensionmm == "" || data.dimensionmm == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Dimension(in MM)";
                                techdata.Value = data.dimensionmm;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.weightfilledbattery == "" || data.weightfilledbattery == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Weight (Filled battery)";
                                techdata.Value = data.weightfilledbattery;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }


                        }
                        //New CR
                        if (productcategoryid == 32) //Charge Controller
                        {
                            if (data.DCOutputVoltage == "" || data.DCOutputVoltage == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "DC Output Voltage (V)";
                                techdata.Value = data.DCOutputVoltage;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.DCOutputCurrent == "" || data.DCOutputCurrent == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "DC Output Current (Amp.)";
                                techdata.Value = data.DCOutputCurrent;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.MaxSupportedPanelpower == "" || data.MaxSupportedPanelpower == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Max Supported Panel power (Wp)";
                                techdata.Value = data.MaxSupportedPanelpower;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.MaxSolarPanelVoltage == "" || data.MaxSolarPanelVoltage == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Max Solar Panel Voltage (V)";
                                techdata.Value = data.MaxSolarPanelVoltage;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }
                        }

                        else if (productcategoryid == 33) //HKVA
                        {
                            if (data.VA == "" || data.VA == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "VA";
                                techdata.Value = data.VA;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.maxcurrentcharge == "" || data.maxcurrentcharge == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Maximum Charging current";
                                techdata.Value = data.maxcurrentcharge;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.noofbattery == "" || data.noofbattery == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Number of battery";
                                techdata.Value = data.noofbattery;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.supportedbatterytype == "" || data.supportedbatterytype == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Supported battery type";
                                techdata.Value = data.supportedbatterytype;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                        }

                        else if (productcategoryid == 34) //Panel
                        {
                            if (data.NoofCells == "" || data.NoofCells == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "No. of Cells";
                                techdata.Value = data.NoofCells;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.PeakPowerPMax == "" || data.PeakPowerPMax == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Peak Power PMax (Wp)";
                                techdata.Value = data.PeakPowerPMax;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.RatedModuleVoltage == "" || data.RatedModuleVoltage == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Rated Module Voltage (V)";
                                techdata.Value = data.RatedModuleVoltage;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.MaximumPowerVoltage == "" || data.MaximumPowerVoltage == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Maximum Power Voltage Vmp (V)";
                                techdata.Value = data.MaximumPowerVoltage;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.MaximumPowerCurrent == "" || data.MaximumPowerCurrent == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Maximum Power Current Imp (A)";
                                techdata.Value = data.MaximumPowerCurrent;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }
                        }

                        else if (productcategoryid == 35) //Retrofit
                        {
                            if (data.NominalDCOutputVoltage == "" || data.NominalDCOutputVoltage == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Nominal DC Output Voltage (V)";
                                techdata.Value = data.NominalDCOutputVoltage;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.MaxDCOutputCurrent == "" || data.MaxDCOutputCurrent == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Max DC Output Current (Amp.)";
                                techdata.Value = data.MaxDCOutputCurrent;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.MaxSupportedPanelpower == "" || data.MaxSupportedPanelpower == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Max Supported Panel power (Wp)";
                                techdata.Value = data.MaxSupportedPanelpower;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.MaxSolarPanelVoltage == "" || data.MaxSolarPanelVoltage == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Max Solar Panel Voltage (V)";
                                techdata.Value = data.MaxSolarPanelVoltage;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.Noof12VBatteriesinSeries == "" || data.Noof12VBatteriesinSeries == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "No. of 12V Batteries in Series";
                                techdata.Value = data.Noof12VBatteriesinSeries;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }
                        }

                        else if (productcategoryid == 36) //Solar Battery
                        {
                            if (data.RatedCapacity == "" || data.RatedCapacity == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Rated Capacity (Ah) - C10";
                                techdata.Value = data.RatedCapacity;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.SolarLength == "" || data.SolarLength == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Length +- 3 mm";
                                techdata.Value = data.SolarLength;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.SolarWidth == "" || data.SolarWidth == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Width +- 3 mm";
                                techdata.Value = data.SolarWidth;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.Heightuptofloattop == "" || data.Heightuptofloattop == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Height upto float top +- 3 mm";
                                techdata.Value = data.Heightuptofloattop;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.DryWeight == "" || data.DryWeight == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Dry Weight +- 5% kg";
                                techdata.Value = data.DryWeight;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                        }

                        else if (productcategoryid == 37) //Solar HUPS
                        {
                            if (data.RatedACpower == "" || data.RatedACpower == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Rated AC power (VA)";
                                techdata.Value = data.RatedACpower;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.MaxSupportedPanelpower == "" || data.MaxSupportedPanelpower == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Max Supported Panel power (Wp)";
                                techdata.Value = data.MaxSupportedPanelpower;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.OperatingVoltage == "" || data.OperatingVoltage == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Operating Voltage";
                                techdata.Value = data.OperatingVoltage;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.ChargeControllerRating == "" || data.ChargeControllerRating == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Charge Controller Rating";
                                techdata.Value = data.ChargeControllerRating;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.NominalBatterybankvoltage == "" || data.NominalBatterybankvoltage == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Nominal Battery bank voltage";
                                techdata.Value = data.NominalBatterybankvoltage;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                        }

                        else if (productcategoryid == 38) //Stabilizers
                        {
                            if (data.InputVoltageWorkingRange == "" || data.InputVoltageWorkingRange == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Input Voltage Working Range";
                                techdata.Value = data.InputVoltageWorkingRange;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.OutputVoltageWorkingRange == "" || data.OutputVoltageWorkingRange == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Output Voltage Working Range";
                                techdata.Value = data.OutputVoltageWorkingRange;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.MainsACLowCut == "" || data.MainsACLowCut == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Mains AC Low Cut";
                                techdata.Value = data.MainsACLowCut;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                            if (data.MainACLowCutRecovery == "" || data.MainACLowCutRecovery == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Technicalspecification techdata = new Technicalspecification();
                                techdata.ColumnName = "Mains AC Low Cut Recovery";
                                techdata.Value = data.MainACLowCutRecovery;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }

                        }
                        //End New CR

                        // pcatalog.tech_specification = techspecification;

                        Pcataloglist.Add(pcatalog);

                    }

                    // }

                


            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return Pcataloglist;
        }

        public List<Product_Search> getProduct_Search(string userid,string search_key)
        {
            
            //Get Home Page List
            List<Product_Search> Pcataloglist = new List<Product_Search>();
            MessageData msgdata = new MessageData();


            try
            {

                var usertype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => new { c.CustomerType, c.CreatedBY }).SingleOrDefault();
                if (usertype.CustomerType == "Dealer" || usertype.CustomerType == "DEALER")
                {
                    userid = usertype.CreatedBY;
                }

                var getproduct_search = (from prdcat in luminous.ProductLevelThrees
                                         where prdcat.PlTwStatus == 1 && prdcat.CreatedBy==userid && prdcat.Name.Contains(search_key)
                                         select new
                                         {
                                             Id = prdcat.id,
                                             name = prdcat.Name

                                         }
                    ).ToList();


                // Checked_data_existornot(pagename);
                foreach (var data in getproduct_search)
                {

                    Product_Search pcatalog = new Product_Search();
                    pcatalog.id = data.Id;


                    if (data.name == "")
                    {
                        pcatalog.productioncatalog_name = "";
                    }
                    else
                    {
                        pcatalog.productioncatalog_name = data.name;
                    }


                    Pcataloglist.Add(pcatalog);

                }

                // }





            }
            catch (Exception exc)
            {


                
            }
            return Pcataloglist;
        }

        public List<Product_Searchdata> getProduct_SearchData(string productname, string plus_flag, string userid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            
            //Get Home Page List
            List<Product_Searchdata> Pcataloglist = new List<Product_Searchdata>();
            MessageData msgdata = new MessageData();


            try
            {
                var usertype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => new { c.CustomerType, c.CreatedBY }).SingleOrDefault();
                if (usertype.CustomerType == "Dealer" || usertype.CustomerType == "DEALER")
                {
                    userid = usertype.CreatedBY;
                }
                string replacePlus = "";
                if (plus_flag == "1")
                {
                    replacePlus = productname.Replace("plus", "+");
                }
                else
                {
                    replacePlus = productname;
                }
                var save_mostsearchitem = new MostSearchItem();
                save_mostsearchitem.SearchItem = replacePlus;
                save_mostsearchitem.UserId = userid;
                save_mostsearchitem.CreatedBy = userid;
                save_mostsearchitem.Status = 1;
                save_mostsearchitem.CreatedOn = DateTime.Now;
                luminous.MostSearchItems.Add(save_mostsearchitem);
                luminous.SaveChanges();
                var getproduct_search_data = (from prdcat in luminous.ProductLevelThrees

                                              join prd2 in luminous.ProductLevelTwoes
                                              on prdcat.pc_Lv2_oneId equals prd2.id
                                              join prodcategory in luminous.ProductCatergories
                                              on prdcat.productCategoryid equals prodcategory.id


                                              where prdcat.Name == replacePlus && prdcat.PlTwStatus == 1 && prdcat.CreatedBy==userid && prd2.PlTwStatus == 1
                                              select new
                                              {
                                                  productcategoryid = prodcategory.id,
                                                  productcategoryname = prodcategory.PName,
                                                  product_upper_id = prd2.id,
                                                  product_upper_name = prd2.Name,
                                                  product_catalog_id = prdcat.id,
                                                  product_catalog_name = prdcat.Name

                                              }
                    ).ToList();


                // Checked_data_existornot(pagename);
                foreach (var data in getproduct_search_data)
                {

                    Product_Searchdata pcatalog = new Product_Searchdata();



                    if (data.productcategoryname == "")
                    {
                        pcatalog.product_category_id = 0;
                        pcatalog.product_category_name = "";
                    }
                    else
                    {
                        pcatalog.product_category_id = data.productcategoryid;
                        pcatalog.product_category_name = data.productcategoryname;
                    }

                    if (data.product_upper_name == "")
                    {
                        pcatalog.catalog_menu_upper_id = 0;
                        pcatalog.catalog_menu_upper_name = "";
                    }
                    else
                    {
                        pcatalog.catalog_menu_upper_id = data.product_upper_id;
                        pcatalog.catalog_menu_upper_name = data.product_upper_name;
                    }

                    if (data.product_catalog_name == "")
                    {
                        pcatalog.product_catalog_id = 0;
                        pcatalog.product_catalog_name = "";
                    }
                    else
                    {
                        pcatalog.product_catalog_id = data.product_catalog_id;
                        pcatalog.product_catalog_name = data.product_catalog_name;
                    }


                    Pcataloglist.Add(pcatalog);

                }

                // }





            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return Pcataloglist;
        }


        public List<Catalog_Menu_Upper> getProduct_Catalog_Upper(string userid,int productcategoryid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            var usertype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => new { c.CustomerType, c.CreatedBY }).SingleOrDefault();
            if (usertype.CustomerType == "Dealer" || usertype.CustomerType == "DEALER")
            {
                userid = usertype.CreatedBY;
            }
           
            //Get Home Page List
            List<Catalog_Menu_Upper> cat_menu_upper = new List<Catalog_Menu_Upper>();


            MessageData msgdata = new MessageData();
            //Get Card Data List

            try
            {


                var getproduct_catalog = luminous.getCatalog_Upper(userid,productcategoryid).ToList();

              
                    // Checked_data_existornot(pagename);
                    foreach (var data in getproduct_catalog)
                    {

                        Catalog_Menu_Upper cat_upper = new Catalog_Menu_Upper();
                        cat_upper.catalog_menu_upper_id = data.id;


                        if (data.Name == "")
                        {
                            cat_upper.catalog_menu_upper_name = "";
                            cat_upper.filter_key = "";
                        }
                        else
                        {
                            if (data.CreatedBy == "prodlevelone")
                            {
                                cat_upper.filter_key = "attribute_name";
                                cat_upper.catalog_menu_upper_name = data.Name;
                            }
                            else
                            {
                                cat_upper.filter_key = "productleveltwo";
                                cat_upper.catalog_menu_upper_name = data.Name;
                            }

                        }



                        cat_menu_upper.Add(cat_upper);


                    }

                    // }

                
                


            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return cat_menu_upper;
        }

        public List<ContactUs> getContactUs(string userid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            
            //Get Home Page List
            List<ContactUs> contactus_data = new List<ContactUs>();
            MessageData msgdata = new MessageData();

           
            //Get Card Data List

            try
            {
                var usertype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => new { c.CustomerType, c.CreatedBY }).SingleOrDefault();
                if (usertype.CustomerType == "Dealer" || usertype.CustomerType == "DEALER")
                {
                    userid = usertype.CreatedBY;
                }
                var getContact = luminous.contactUsDetails.Where(c => c.Cstatus == 1 && c.CreatedBy==userid).Select(c => new { c.id, c.Contact_Us_Type, c.CAddress, c.PhoneNumber, c.Email, c.Fax }).ToList();



                foreach (var data in getContact)
                {

                    ContactUs contact_us = new ContactUs();


                    if (data.Contact_Us_Type == "")
                    {
                        contact_us.contactus_title = "";
                    }
                    else
                    {
                        contact_us.contactus_title = data.Contact_Us_Type;
                    }

                    if (data.CAddress == "")
                    {
                        contact_us.address = "";
                    }
                    else
                    {
                        contact_us.address = data.CAddress;
                    }
                    if (data.PhoneNumber == "")
                    {
                        contact_us.phoneno = "";
                    }
                    else
                    {
                        contact_us.phoneno = data.PhoneNumber;
                    }
                    if (data.Fax == "")
                    {
                        contact_us.sales_support_phoneno = "";
                    }
                    else
                    {
                        contact_us.sales_support_phoneno = data.Fax;
                    }
                    if (data.Email == "")
                    {
                        contact_us.email = "";
                    }
                    else
                    {
                        contact_us.email = data.Email;
                    }

                    contactus_data.Add(contact_us);


                }

                // }




            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return contactus_data;
        }

        #region save contact us suggestion
        public MessageData save_contactus_suggetion(string userid, string custname, string email, string message, byte[] image, string filename)
        {
            string Filename = "";
          
            MessageData msgdata = new MessageData();
            if (image != null)
            {
                Filename = Path.GetFileNameWithoutExtension(filename) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(filename);
                string str = Path.Combine(HttpContext.Current.Server.MapPath("~/SuggestionImage/"), Filename);
                BinaryWriter bw = new BinaryWriter(new FileStream(str, FileMode.Create, FileAccess.Write));
                bw.Write(image);
                bw.Close();
            }
             var contectus_suggetion=new Suggestion();
            
                contectus_suggetion.Suggestion1 = message;
                contectus_suggetion.Email = email;
                contectus_suggetion.createdBy = userid;
                contectus_suggetion.CustName = custname;
                contectus_suggetion.CreateDate = DateTime.Now;
                contectus_suggetion.ImageName = Filename;


                luminous.Suggestions.Add(contectus_suggetion);
                         

            int savestatus = luminous.SaveChanges();

            if (savestatus > 0)
            {
                msgdata.Status = "200";
                msgdata.Message = "Thank you for your feedback.";

            }
            else
            {
                msgdata.Message = "Data not inserted";
                msgdata.Status = "0";
            }



            return msgdata;
        }
        #endregion

        public List<Gallery_Menu_Upper> getGalleryUpperData(string userid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
           
            //Get Home Page List
            List<Gallery_Menu_Upper> gallery_data = new List<Gallery_Menu_Upper>();
            MessageData msgdata = new MessageData();
            //Get Card Data List

            try
            {
                var getGallery_Menu_Footer = luminous.FooterCategories.Where(c => c.Status == true && c.CatType == "Media").ToList();





                foreach (var data in getGallery_Menu_Footer)
                {

                    Gallery_Menu_Upper gallerymenu_upper = new Gallery_Menu_Upper();

                    gallerymenu_upper.gallery_menu_upper_id = data.Id;

                    if (data.FCategoryName == "")
                    {
                        gallerymenu_upper.gallery_menu_upper_name = "";
                    }
                    else
                    {
                        gallerymenu_upper.gallery_menu_upper_name = data.FCategoryName;
                    }


                    gallery_data.Add(gallerymenu_upper);


                }

                // }




            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return gallery_data;
        }

        #endregion



        public void SaveServiceLog(string Userid, string url, string Req_Parameter, string Res_Parameter, int Error, string ErrorDes, string createdby, DateTime Createdon, string deviceid, string Appversion, string Ostype, string Osversion)
        {
            try
            {
               
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

                   MPartnerServiceLog mpl =   new MPartnerServiceLog();
               

                    mpl.UserId = Userid;
                    mpl.Url = url;
                    mpl.Req_Parameter = Req_Parameter;
                    mpl.Res_parameter = Res_Parameter;
                    mpl.Error = Error;
                    mpl.ErrorDescription = ErrorDes;
                    mpl.Flag = flag;

                    mpl.CreatedBy = Userid;
                    mpl.CreatedOn = Createdon;
                    mpl.DeviceId = deviceid;
                    mpl.AppVersion = Appversion;
                    mpl.OSType = Ostype;
                    mpl.OSVersion = Osversion;
                    mpl.NewURL = url;
                    mpl.Comments = comments;

                    luminous.MPartnerServiceLogs.Add(mpl);

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
}