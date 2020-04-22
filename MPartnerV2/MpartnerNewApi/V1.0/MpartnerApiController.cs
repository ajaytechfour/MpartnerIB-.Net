using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Text;
using Luminous.EF;
using System.Data;
using System.IO;
//using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using Luminous.MpartnerNewApi.Model;
namespace Luminous.MpartnerNewApi.MpartnerVer1._1
{
    public class MpartnerApiController : ApiController
    {



        // GET api/<controller>
        [ActionName("GetState")]
        public object GetState()
        {
            LuminousEntities luminous = new LuminousEntities();
            var statedata = luminous.allstates.Select(c => new { c.id, c.statename }).ToList();


            //Convert List Into JSON
            var jsonmsgs = JsonConvert.SerializeObject(statedata);

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);

            //Set the content of the response to be JSON Format
            res.Content = new StringContent(jsonmsgs, System.Text.Encoding.UTF8, "application/json");

            //Return the Response
            return res;
        }

        // POST api/values





        [System.Web.Http.HttpGet]
        [ActionName("home_page_cards")]
        public object home_page_cards(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot("HomePage", "");



                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
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
                        return getJson_CardProvider(getAppMessage.Message, getAppMessage.Status, "", null, "HomePage");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null, "HomePage");
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(dataexistornot.Message, dataexistornot.Status, "", null, "HomePage");
                    }


                }
                if (dataexistornot.Status == "200")
                {
                    var cardProvider = getCardprovider(user_id,"HomePage", "");
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
        [ActionName("connectplus_page_cards")]
        public object connectplus_page_cards(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot("Connect+", "");



                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(getTokenMessage.Message, getTokenMessage.Status, "", null, "Connect+");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(getAppMessage.Message, getAppMessage.Status, "", null, "Connect+");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null, "Connect+");
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(dataexistornot.Message, dataexistornot.Status, "", null, "Connect+");
                    }


                }
                if (dataexistornot.Status == "200")
                {
                    var cardProvider = getConnectPlusCardProvider("Connect+");
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_CardProvider(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, cardProvider, "Connect+");

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
        public object scheme_page_cards(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot("Scheme", "");




                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(getTokenMessage.Message, getTokenMessage.Status, "", null, "Scheme");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(getAppMessage.Message, getAppMessage.Status, "", null, "Scheme");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null, "Scheme");
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(dataexistornot.Message, dataexistornot.Status, "", null, "Scheme");
                    }


                }
                if (dataexistornot.Status == "200")
                {
                    var cardProvider = getCardprovider(user_id,"Scheme", "");
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
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
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_CardProvider(exception.Message, exception.Status, "", null, "Scheme");
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("pricelist_page_cards")]
        public object pricelist_page_cards(string user_id, string parentid, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot("Price",parentid);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",ParentId :"+parentid+",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(getTokenMessage.Message, getTokenMessage.Status, "", null, "Price");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",ParentId :" + parentid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(getAppMessage.Message, getAppMessage.Status, "", null, "Price");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",ParentId :" + parentid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null, "Price");
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",ParentId :" + parentid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_CardProvider(dataexistornot.Message, dataexistornot.Status, "", null, "Price");
                    }


                }
                if (dataexistornot.Status == "200")
                {
                    var cardProvider = getCardprovider(user_id,"Price", parentid);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",ParentId :" + parentid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
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
                string RequestParameter = "UserID :" + user_id + ",ParentId :" + parentid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_CardProvider(exception.Message, exception.Status, "", null, "Price");
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("ParentCategory")]
        public object ParentCategory(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot("ParentCategory", "");


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_ParentCategory(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_ParentCategory(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_ParentCategory(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_ParentCategory(dataexistornot.Message, dataexistornot.Status, "", null);
                    }


                }
                if (dataexistornot.Status == "200")
                {
                    var cardProvider = func_GetParentCategory();
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
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
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_ParentCategory(exception.Message, exception.Status, "", null);
            }

            return "";

        }
       


        [System.Web.Http.HttpGet]
        [ActionName("Catalog_menu_footer")]
        public object Catalog_menu_footer(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot("ProductCategory", "");


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_ProductCategory(getTokenMessage.Message, getTokenMessage.Status, "", null, "ProductCategory");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_ProductCategory(getAppMessage.Message, getAppMessage.Status, "", null, "ProductCategory");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_ProductCategory(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null, "ProductCategory");
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_ProductCategory(dataexistornot.Message, dataexistornot.Status, "", null, "ProductCategory");
                    }


                }
                if (dataexistornot.Status == "200")
                {
                    var product_category = getProductCategory("ProductCategory");
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
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
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_CardProvider(exception.Message, exception.Status, "", null, "ProductCategory");
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("Catalog_products")]
        public object Catalog_products(string user_id, int productcategoryid, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_catalog_products(productcategoryid);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_catalog(getTokenMessage.Message, getTokenMessage.Status, "", null, "ProductCategory");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_catalog(getAppMessage.Message, getAppMessage.Status, "", null, "ProductCategory");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_catalog(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null, "ProductCategory");
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_catalog(dataexistornot.Message, dataexistornot.Status, "", null, "ProductCategory");
                    }


                }
                if (dataexistornot.Status == "200")
                {
                    var product_catalog = getProduct_Catalog(productcategoryid);
                    //Save Api log data//
                    #region save request and response data in api log
                     string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
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
                string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_Product_catalog(exception.Message, exception.Status, "", null, "Price");
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("Search_products")]
        public object Search_products(string search_key,string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_search_products(search_key);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "SearchKey : "+search_key+",UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_search(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "SearchKey : " + search_key + ",UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_search(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "SearchKey : " + search_key + ",UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_search(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "SearchKey : " + search_key + ",UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_search(dataexistornot.Message, dataexistornot.Status, "", null);
                    }


                }
                if (dataexistornot.Status == "200")
                {
                    var product_catalog = getProduct_Search(search_key);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "SearchKey : " + search_key + ",UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
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
                string RequestParameter = "SearchKey : " + search_key + ",UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_Product_search(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("GetData_searchproducts")]
        public object GetData_searchproducts(string user_id, string productname, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_getdata_searchproduct(productname);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",ProductName :" + productname + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_search_data(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",ProductName :" + productname + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_search_data(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",ProductName :" + productname + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_search_data(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",ProductName :" + productname + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_search_data(dataexistornot.Message, dataexistornot.Status, "", null);
                    }


                }
                if (dataexistornot.Status == "200")
                {
                    var product_catalog = getProduct_SearchData(productname);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",ProductName :" + productname + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
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
                string RequestParameter = "UserID :" + user_id + ",ProductName :" + productname + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_Product_search(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        //[System.Web.Http.HttpGet]
        //[ActionName("Catalog_products_details")]
        //public object Catalog_products_details(string user_id, int catalogid, int productcategoryid, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        //{
        //    string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

        //    try
        //    {


        //        var getTokenMessage = getToken(user_id, app_version,device_id, token);
        //        var getAppMessage = getAppversion(app_version, os_type);
        //        var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
        //        var dataexistornot = Checked_data_existornot_catalog_products_details(catalogid, productcategoryid);
        //        var product_catalog = getProduct_Catalog_details(catalogid, productcategoryid);

        //        if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
        //        {
        //            if (getTokenMessage.Status != "")
        //            {
        //                #region save request and response data in api log
        //                string RequestParameter = "UserID :" + user_id + ",CatalogId :" + catalogid + ",ProductCategoryId :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
        //                string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
        //                #endregion
        //                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
        //                return getJson_Product_catalog_details(getTokenMessage.Message, getTokenMessage.Status, null);
        //            }
        //            if (getAppMessage.Status != "")
        //            {
        //                #region save request and response data in api log
        //                string RequestParameter = "UserID :" + user_id + ",CatalogId :" + catalogid + ",ProductCategoryId :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
        //                string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
        //                #endregion

        //                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
        //                return getJson_Product_catalog_details(getAppMessage.Message, getAppMessage.Status, null);
        //            }

        //            if (checkedThreeUserLoggedIn.Status != "")
        //            {
        //                #region save request and response data in api log
        //                string RequestParameter = "UserID :" + user_id + ",CatalogId :" + catalogid + ",ProductCategoryId :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
        //                string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
        //                #endregion
        //                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
        //                return getJson_Product_catalog_details(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, null);
        //            }
        //            if (dataexistornot.Status == "0")
        //            {
        //                #region save request and response data in api log
        //                string RequestParameter = "UserID :" + user_id + ",CatalogId :" + catalogid + ",ProductCategoryId :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
        //                string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
        //                #endregion
        //                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
        //                return getJson_Product_catalog_details(dataexistornot.Message, dataexistornot.Status, null);
        //            }


        //        }
        //        if (dataexistornot.Status == "200")
        //        {
        //            //Save Api log data//
        //            #region save request and response data in api log
        //            string RequestParameter = "UserID :" + user_id + ",CatalogId :" + catalogid + ",ProductCategoryId :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
        //            string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
        //            #endregion
        //            SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
        //            //End Save Api log data//

        //            return getJson_Product_catalog_details(dataexistornot.Message, dataexistornot.Status, product_catalog);

        //        }

        //    }
        //    catch (Exception exc)
        //    {
        //        var exception = getTryCatchExc();

        //        #region save request and response data in api log
        //        string RequestParameter = "UserID :" + user_id + ",CatalogId :" + catalogid + ",ProductCategoryId :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
        //        string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
        //        #endregion
        //        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
        //        return getJson_Product_catalog_details(exception.Message, exception.Status, null);
        //    }

        //    return "";

        //}

        [System.Web.Http.HttpGet]
        [ActionName("Catalog_menu_upper")]
        public object Catalog_menu_upper(string user_id, int productcategoryid, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_catalog_upper(productcategoryid);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_catalog_menu_upper(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_catalog_menu_upper(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_catalog_menu_upper(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Product_catalog_menu_upper(dataexistornot.Message, dataexistornot.Status, "", null);
                    }


                }
                if (dataexistornot.Status == "200")
                {
                    var product_catalog_upper = getProduct_Catalog_Upper(productcategoryid);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
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
                string RequestParameter = "UserID :" + user_id + ",productcategoryid :" + productcategoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_Product_catalog_menu_upper(exception.Message, exception.Status, "", null);
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("Contactus_details")]
        public object Contactus_details(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_contactus_details();


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_contactus_details(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_contactus_details(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_contactus_details(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_contactus_details(dataexistornot.Message, dataexistornot.Status, "", null);
                    }


                }
                if (dataexistornot.Status == "200")
                {
                    var contactus = getContactUs();
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
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
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
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


                var getTokenMessage = getToken(value.user_id, value.app_version, value.device_id, value.token);
                var getAppMessage = getAppversion(value.app_version, value.os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(value.user_id, value.device_id);

                var contactus_suggestion = save_contactus_suggetion(value.user_id, value.name, value.email, value.message, value.contactusimage, value.filename);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + value.user_id + ",Name :" + value.name + ",Email :" + value.email + ",Message :" + value.message + ",Contactusimage :" + value.contactusimage + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                        return getJson_contactus_suggestion(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + value.user_id + ",Name :" + value.name + ",Email :" + value.email + ",Message :" + value.message + ",Contactusimage :" + value.contactusimage + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                        return getJson_contactus_suggestion(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + value.user_id + ",Name :" + value.name + ",Email :" + value.email + ",Message :" + value.message + ",Contactusimage :" + value.contactusimage + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                        return getJson_contactus_suggestion(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }



                }
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
        public object Gallery_menu_upper(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot("Gallery_Menu_Upper", "");


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_gallery_menu_upper(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_gallery_menu_upper(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_gallery_menu_upper(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_gallery_menu_upper(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var gallery_menu_upper = getGalleryUpperData();
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
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
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_gallery_menu_upper(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("Gallery_maindata")]
        public object Gallery_maindata(string user_id, int gallery_categoryid, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_gallery_data(gallery_categoryid);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Gallery_CategoryId :" + gallery_categoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_gallery_maindata(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Gallery_CategoryId :" + gallery_categoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_gallery_maindata(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Gallery_CategoryId :" + gallery_categoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_gallery_maindata(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Gallery_CategoryId :" + gallery_categoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_gallery_maindata(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var gallery_maindata = getGalleryMainData(gallery_categoryid);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Gallery_CategoryId :" + gallery_categoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_gallery_maindata(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, gallery_maindata);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Gallery_CategoryId :" + gallery_categoryid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_gallery_maindata(exception.Message, exception.Status, "", null);
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("Media_maindata")]
        public object Media_maindata(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot("Media", "");


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_media_maindata(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_media_maindata(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_media_maindata(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_media_maindata(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var gallery_maindata = getMediaMainData();
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_media_maindata(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, gallery_maindata);

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
                return getJson_media_maindata(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("Faq_data")]
        public object Faq_data(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot("Faq", "");


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_faq(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_faq(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_faq(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_faq(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var faqdata = getFaqData();
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_faq(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, faqdata);

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
                return getJson_faq(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("Customer_permission_data")]
        public object Customer_permission_data(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_customerpermission_data(user_id, language);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_customer_permission(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_customer_permission(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_customer_permission(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_customer_permission(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var permissiondata = getcustomer_permission(user_id, language);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
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
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_customer_permission(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("LSD_GetDistributorCount")]
        public object LSD_GetDistributorCount(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_LSD_GetDistributorCount(user_id);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetDistributorCount(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetDistributorCount(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetDistributorCount(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetDistributorCount(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var lsd_getdistcount = func_LSD_GetDistributorCount(user_id);

                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_LSD_GetDistributorCount(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, lsd_getdistcount);

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
                return getJson_LSD_GetDistributorCount(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("LSD_SaveQrCode")]
        public object LSD_SaveQrCode(string user_id, string qrcode, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                // var dataexistornot = Checked_data_existornot_LSD_GetDistributorCount(user_id);
                var lsd_saveQrCode = func_LSD_SaveQrCode(user_id, qrcode);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_lsd_saveqrcode(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_lsd_saveqrcode(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_lsd_saveqrcode(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }




                }
                if (lsd_saveQrCode.Status == "200" || lsd_saveQrCode.Status == "0")
                {
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + lsd_saveQrCode.Message + ",Status : " + lsd_saveQrCode.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, lsd_saveQrCode.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_lsd_saveqrcode(lsd_saveQrCode.Message, lsd_saveQrCode.Status, getTokenMessage.Token);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",QrCode :" + qrcode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_lsd_saveqrcode(exception.Message, exception.Status, "");
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("LSD_GetDistOpenReimbursmentReport")]
        public object LSD_GetDistOpenReimbursmentReport(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_LSD_GetDistOpenReimbursmentReport(user_id);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id +",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetDistOpenReimbursmentReport(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetDistOpenReimbursmentReport(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetDistOpenReimbursmentReport(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetDistOpenReimbursmentReport(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var lsd_getdistopenreimbursment = func_LSD_GetDistOpenReimbursmentReport(user_id);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_LSD_GetDistOpenReimbursmentReport(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, lsd_getdistopenreimbursment);

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
                return getJson_LSD_GetDistributorCount(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("LSD_SaveClaimData")]
        public object LSD_SaveClaimData(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                // var dataexistornot = Checked_data_existornot_LSD_GetDistributorCount(user_id);
                var lsd_saveclaimdata = func_LSD_SaveClaimData(user_id);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_lsd_save_claimdata(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_lsd_save_claimdata(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_lsd_save_claimdata(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }




                }
                if (lsd_saveclaimdata.Status == "200" || lsd_saveclaimdata.Status == "0")
                {
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + lsd_saveclaimdata.Message + ",Status : " + lsd_saveclaimdata.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, lsd_saveclaimdata.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_lsd_save_claimdata(lsd_saveclaimdata.Message, lsd_saveclaimdata.Status, getTokenMessage.Token);

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
                return getJson_lsd_save_claimdata(exception.Message, exception.Status, "");
            }

            return "";

        }



        [System.Web.Http.HttpGet]
        [ActionName("LSD_GetRedeemedReport")]
        public object LSD_GetRedeemedReport(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_LSD_GetRedeemedReport(user_id);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetRedeemedReport(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetRedeemedReport(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetRedeemedReport(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetRedeemedReport(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var lsd_getredeemedreport = func_LSD_GetRedeemedReport(user_id);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_LSD_GetRedeemedReport(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, lsd_getredeemedreport);

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
                return getJson_LSD_GetRedeemedReport(exception.Message, exception.Status, "", null);
            }

            return "";

        }



        [System.Web.Http.HttpGet]
        [ActionName("LSD_GetActivatedCouponReport")]
        public object LSD_GetActivatedCouponReport(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_LSD_GetActivatedCouponReport(user_id);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetActivatedCouponReport(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetActivatedCouponReport(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetActivatedCouponReport(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetActivatedCouponReport(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var lsd_getactivated_coupon = func_LSD_GetActivatedCouponReport(user_id,"B");
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_LSD_GetActivatedCouponReport(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, lsd_getactivated_coupon);

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
                return getJson_LSD_GetActivatedCouponReport(exception.Message, exception.Status, "", null);
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("LSD_Gold_GetActivatedCouponReport")]
        public object LSD_Gold_GetActivatedCouponReport(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_LSD_GetActivatedCouponReport(user_id);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetActivatedCouponReport(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetActivatedCouponReport(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetActivatedCouponReport(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_GetActivatedCouponReport(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var lsd_getactivated_coupon = func_LSD_GetActivatedCouponReport(user_id, "G");
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_LSD_GetActivatedCouponReport(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, lsd_getactivated_coupon);

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
                return getJson_LSD_GetActivatedCouponReport(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("LSD_DealerReport")]
        public object LSD_DealerReport(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_LSD_DealerReport(user_id);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_DealerReport(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_DealerReport(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_DealerReport(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_DealerReport(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var lsd_dealerreport = func_LSD_DealerReport(user_id);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_LSD_DealerReport(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, lsd_dealerreport);

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
                return getJson_LSD_DealerReport(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("LSD_SaveDealerScanCode")]
        public object LSD_SaveDealerScanCode(string user_id, string barcode, string secretcode, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_LSD_SaveDealerScanCode(user_id, barcode, secretcode);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",BarCode :" + barcode + ",AlphanumericCode  :" + secretcode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_SaveDealerScanCode(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",BarCode :" + barcode + ",AlphanumericCode  :" + secretcode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_SaveDealerScanCode(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",BarCode :" + barcode + ",AlphanumericCode  :" + secretcode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_SaveDealerScanCode(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",BarCode :" + barcode + ",AlphanumericCode  :" + secretcode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LSD_SaveDealerScanCode(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var lsd_savedealerscancode = func_LSD_SaveDealerScanCode(user_id, barcode, secretcode);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",BarCode :" + barcode + ",AlphanumericCode  :" + secretcode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_LSD_SaveDealerScanCode(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, lsd_savedealerscancode);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",BarCode :" + barcode + ",AlphanumericCode  :" + secretcode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_LSD_SaveDealerScanCode(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("GetSurveyNotificationList")]
        public object GetSurveyNotificationList(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_SurveyNotificationList(user_id);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_SurveyNotificationList(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_SurveyNotificationList(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_SurveyNotificationList(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_SurveyNotificationList(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var survey_not_list = func_SurveyNotificationList(user_id);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_SurveyNotificationList(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, survey_not_list);

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
                return getJson_SurveyNotificationList(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("GetSurveyQuestion")]
        public object GetSurveyQuestion(string user_id, int surveyid, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_GetSurveyQuestion(surveyid);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",SurveyId :" + surveyid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_GetSurveyQuestion(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",SurveyId :" + surveyid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_GetSurveyQuestion(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",SurveyId :" + surveyid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_GetSurveyQuestion(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status == "0")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",SurveyId :" + surveyid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_GetSurveyQuestion(dataexistornot.Message, dataexistornot.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var survey_not_list = func_GetSurveyQuestion(surveyid);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",SurveyId :" + surveyid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_GetSurveyQuestion(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, survey_not_list);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",SurveyId :" + surveyid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_GetSurveyQuestion(exception.Message, exception.Status, "", null);
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("SaveSurveyResult")]
        public object SaveSurveyResult(string user_id, int surveyid, string option, string optionvalue, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);

                var savesurveyres = func_SaveSurveyResult(user_id, surveyid, option, optionvalue);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",SurveyId :" + surveyid + ",Option :" + option + ",OptionValue :" + optionvalue + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_SaveSurveyResult(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",SurveyId :" + surveyid + ",Option :" + option + ",OptionValue :" + optionvalue + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_SaveSurveyResult(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",SurveyId :" + surveyid + ",Option :" + option + ",OptionValue :" + optionvalue + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_SaveSurveyResult(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }



                }
                if (savesurveyres.Status == "200" || savesurveyres.Status == "0")
                {
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",SurveyId :" + surveyid + ",Option :" + option + ",OptionValue :" + optionvalue + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + savesurveyres.Message + ",Status : " + savesurveyres.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, savesurveyres.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_SaveSurveyResult(savesurveyres.Message, savesurveyres.Status, getTokenMessage.Token);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",SurveyId :" + surveyid + ",Option :" + option + ",OptionValue :" + optionvalue + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_SaveSurveyResult(exception.Message, exception.Status, "");
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("GetAlertnotification")]
        public object GetAlertnotification(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_GetAlertnotification(user_id);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_GetAlertnotification(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_GetAlertnotification(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_GetAlertnotification(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }



                }
                if (dataexistornot.Status == "200")
                {
                    var getAlertNot = func_GetAlertnotification(user_id, device_id);
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_GetAlertnotification(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, getAlertNot);

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
                return getJson_GetSurveyQuestion(exception.Message, exception.Status, "", null);
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("IsReadCheck_AlertNotification")]
        public object IsReadCheck_AlertNotification(string user_id, string notificationid, string isread, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);

                var isreadcheck = func_IsReadCheck_AlertNotification(user_id, device_id, notificationid, isread);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",NotificationId :" + notificationid + ",Isread :" + isread + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_IsReadCheck_AlertNotification(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",NotificationId :" + notificationid + ",Isread :" + isread + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_IsReadCheck_AlertNotification(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",NotificationId :" + notificationid + ",Isread :" + isread + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_IsReadCheck_AlertNotification(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }



                }
                if (isreadcheck.Status == "200" || isreadcheck.Status == "0")
                {
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",NotificationId :" + notificationid + ",Isread :" + isread + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + isreadcheck.Message + ",Status : " + isreadcheck.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, isreadcheck.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_IsReadCheck_AlertNotification(isreadcheck.Message, isreadcheck.Status, getTokenMessage.Token);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",NotificationId :" + notificationid + ",Isread :" + isread + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_IsReadCheck_AlertNotification(exception.Message, exception.Status, "");
            }

            return "";

        }




        #region Report Section

        [System.Web.Http.HttpGet]
        [ActionName("Distributor_Secondary_sales_report")]
        public object Distributor_Secondary_sales_report(string user_id, string dealerid, int itemid, string fromdate, string todate, string token, string way, string token_m, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {
                var getTokenMessage = getToken(user_id, app_version, device_id, token_m);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",DealerId :" + dealerid + ",ItemId :" + itemid + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LumReports(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",DealerId :" + dealerid + ",ItemId :" + itemid + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LumReports(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",DealerId :" + dealerid + ",ItemId :" + itemid + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LumReports(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }



                }

                else
                {
                    LuminousEntities luminous = new LuminousEntities();
                    MessageData msgdata = new MessageData();
                    string Path = HttpContext.Current.Server.MapPath("~/MpartnerNewApi/Attachment/");
                    // Create a request for the URL.   
                    WebRequest request = WebRequest.Create(
                    "https://mpartnerv2.luminousindia.com/nonsapservices/api/nonsapservice/sscDistachedReportDataPopUp?SessionLoginDisID=" + user_id + "&DlrID=" + dealerid + "&ItemID=" + itemid + "&DispatchedDateFrom=" + fromdate + "&DispatchedDateTo=" + todate + "&token=" + token + "&way=" + way + "");


                    // If required by the server, set the credentials.  
                    request.Credentials = CredentialCache.DefaultCredentials;
                    // Get the response.  
                    WebResponse response = request.GetResponse();
                    // Display the status.  
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    // Get the stream containing content returned by the server.  
                    Stream dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.  
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.  
                    string responseFromServer = reader.ReadToEnd();
                    //// Display the content.  
                    //Console.WriteLine(responseFromServer);

                    // Clean up the streams and the response.  
                    reader.Close();
                    response.Close();



                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(responseFromServer, (typeof(DataTable)));
                    var getemail = luminous.UsersLists.Where(c => c.UserId == user_id).Select(c => c.Dis_Email).SingleOrDefault();

                    if (dt.Columns.Count > 0)
                    {
                        string date_month_year = DateTime.Now.ToString("_yyyy_MM_dd_HH_MM_ss");

                        string FileName01 = "Distributor_Secondary_sales_Report_" + date_month_year + "_.xls";
                        GenerateExcel_report(Path + FileName01, dt);
                        string attachment = Path + FileName01;

                        sendMail("Primary Billing Report", attachment, getemail, senddataformat(attachment));
                        msgdata.Message = "Your request has been sent and you will receive the report in your registered Email id.";
                        msgdata.Status = "200";
                        string RequestParameter = "UserID :" + user_id + ",DealerId :" + dealerid + ",ItemId :" + itemid + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + msgdata.Message + ",Status : " + msgdata.Status + "";

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, msgdata.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);

                        return getJson_LumReports(msgdata.Message, msgdata.Status, getTokenMessage.Token);
                    }
                }
            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",DealerId :" + dealerid + ",ItemId :" + itemid + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_LumReports(exception.Message, exception.Status, "");
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("Distributor_Ledger_Report")]
        public object Distributor_Ledger_Report(string user_id, string fromdate, string todate, string token, string way, string token_m, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {
                var getTokenMessage = getToken(user_id, app_version, device_id, token_m);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LumReports(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LumReports(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LumReports(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }



                }
                else
                {
                    var getreports = func_GetReportsByDate("DISTRIBUTOR_LEDGER", user_id, fromdate, todate, token, way);
                    return getJson_LumReports(getreports.Message, getreports.Status, getTokenMessage.Token);
                }
            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_LumReports(exception.Message, exception.Status, "");
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("Distributor_Credit_Debit_Report")]
        public object Distributor_Credit_Debit_Report(string user_id, string fromdate, string todate, string token, string way, string token_m, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {
                var getTokenMessage = getToken(user_id, app_version, device_id, token_m);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LumReports(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LumReports(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LumReports(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }



                }
                else
                {
                    var getreports = func_GetReportsByDate("DISTRIBUTOR_CRDR", user_id, fromdate, todate, token, way);
                    return getJson_LumReports(getreports.Message, getreports.Status, getTokenMessage.Token);
                }
            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_LumReports(exception.Message, exception.Status, "");
            }

            return "";
        }

        [System.Web.Http.HttpGet]
        [ActionName("Distributor_Primary_Billing_Report")]
        public object Distributor_Primary_Billing_Report(string user_id, string fromdate, string todate, string token, string way, string token_m, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {
                var getTokenMessage = getToken(user_id, app_version, device_id, token_m);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LumReports(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LumReports(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_LumReports(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }



                }
                else
                {
                    var getreports = func_GetReportsByDate("PRIMARY_RECV_REPORT", user_id, fromdate, todate, token, way);
                    return getJson_LumReports(getreports.Message, getreports.Status, getTokenMessage.Token);
                }
            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Fromdate :" + fromdate + ",Todate :" + todate + ",Token :" + token + ",Way :" + way + ",Token_M :" + token_m + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                if (exc.InnerException == null)
                {
                    // exc="Object reference not set to an instance of an object."
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, "Object reference not set to an instance of an object.", user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                }
                else
                {
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                }
                return getJson_LumReports(exception.Message, exception.Status, "");
            }

            return "";

        }

        public void GenerateExcel_report(string File_Path_Name, System.Data.DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                System.Web.UI.WebControls.DataGrid dgGrid = new System.Web.UI.WebControls.DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                StreamWriter writer = File.AppendText(File_Path_Name);
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                writer.WriteLine(tw.ToString());
                writer.Close();

            }
        }

        public string senddataformat(string path)
        {

            string str = "";
            str = str + "<body style=";
            str = str + @"""font-family: Cambria";
            str = str + @""">";
            str = str + @"<b>Dear Sir/Mam,<br /> Please find the attached file.</b><br /><br />";
            //str = str + @"<b>Lead Dump : </b> <a href='" + path + "'>Download</a>";
            str = str + "<br /><br />This is a system generated mail for your information only. Please do not respond.<br /> ";
            str = str + "<br />";
            str = str + "<b><br><br> Regards <br> Luminous IT Team  <br></b><br />";
            str = str + "</body>";
            return str;
        }
        public void sendMail(string subject, string attachment, string tomailid, string mailbody)
        {
            string chk;
            try
            {

                string msgBody = string.Empty;
                MailMessage mMailMessage = new MailMessage();
                mMailMessage.From = new MailAddress(ConfigurationSettings.AppSettings["mailAdd"].ToString(), "NO REPLY");
                mMailMessage.To.Add(tomailid);




                try
                {
                    //mMailMessage.Subject = subject;
                    //mMailMessage.Attachments.Add(new Attachment(attachment));
                    //mMailMessage.Body = mailbody;
                    //mMailMessage.IsBodyHtml = true;
                    //mMailMessage.Priority = MailPriority.High;
                    //SmtpClient smtp = new SmtpClient();
                    ////smtp.Host = "smtp.office365.com";
                    //smtp.Host = "smtp.gmail.com";

                    //smtp.Port = 587;
                    //smtp.EnableSsl = true;
                    //smtp.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["mailAdd"].ToString(), ConfigurationSettings.AppSettings["mailPass"].ToString());
                    //smtp.Send(mMailMessage);
                    //chk = "send";

                    mMailMessage.Subject = subject;
                    mMailMessage.Body = mailbody;
                    mMailMessage.IsBodyHtml = true;
                    mMailMessage.Priority = MailPriority.High;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.office365.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["mailAdd"].ToString(), ConfigurationSettings.AppSettings["mailPass"].ToString());
                    smtp.Send(mMailMessage);
                    chk = "send";
                }
                catch (Exception ex)
                {

                }
            }
            catch
            {
                chk = "failed";
            }


        }

        public MessageData func_GetReportsByDate(string apiname, string user_id, string fromdate, string todate, string token, string way)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            string Path = HttpContext.Current.Server.MapPath("~/MpartnerNewApi/Attachment/");
            // Create a request for the URL.   
            WebRequest request = WebRequest.Create(
            "https://mpartnerv2.luminousindia.com/nonsapservices/api/sapservice/" + apiname + "?Discode=" + user_id + "&FROMDATE=" + fromdate + "&TODATE=" + todate + "&token=" + token + "&way=" + way + "");
            // If required by the server, set the credentials.  
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            //// Display the content.  
            //Console.WriteLine(responseFromServer);

            // Clean up the streams and the response.  
            reader.Close();
            response.Close();



            DataTable dt = (DataTable)JsonConvert.DeserializeObject(responseFromServer, (typeof(DataTable)));
            var getemail = luminous.UsersLists.Where(c => c.UserId == user_id).Select(c => c.Dis_Email).SingleOrDefault();

            if (dt.Columns.Count > 0)
            {
                string date_month_year = DateTime.Now.ToString("_yyyy_MM_dd_HH_MM_ss");

                if (apiname == "DISTRIBUTOR_LEDGER")
                {
                    string FileName01 = "Distributor_Ledger_Report_" + date_month_year + ".xls";
                    GenerateExcel_report(Path + FileName01, dt);
                    string attachment = Path + FileName01;

                    sendMail("Distributor Ledger Report", attachment, getemail, senddataformat(attachment));


                    msgdata.Message = "Your request has been sent and you will receive the report in your registered Email id.";
                    msgdata.Status = "200";

                }
                if (apiname == "DISTRIBUTOR_CRDR")
                {
                    string FileName01 = "Distributor_Credit_Debit_Report_" + date_month_year + ".xls";
                    GenerateExcel_report(Path + FileName01, dt);
                    string attachment = Path + FileName01;

                    sendMail("Distributor Credit Debit Report", attachment, getemail, senddataformat(attachment));
                    msgdata.Message = "Your request has been sent and you will receive the report in your registered Email id.";
                    msgdata.Status = "200";

                }
                if (apiname == "PRIMARY_RECV_REPORT")
                {
                    string FileName01 = "Distributor_Primary_Recieve_Report_" + date_month_year + ".xls";
                    GenerateExcel_report(Path + FileName01, dt);
                    string attachment = Path + FileName01;

                    sendMail("Distributor Primary Receive Report", attachment, getemail, senddataformat(attachment));
                    msgdata.Message = "Your request has been sent and you will receive the report in your registered Email id.";
                    msgdata.Status = "200";

                }
            }
            else
            {
                msgdata.Message = "No Data Found";
                msgdata.Status = "0";
            }
            return msgdata;
        }


        #endregion


        [System.Web.Http.HttpGet]
        [ActionName("EscalationMatrix")]
        public object EscalationMatrix(string user_id, int stateid, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);

                var dataexistornot = Checked_data_existornot_EscalationMatrix(stateid);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" ||dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Stateid :" + stateid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_EscalationMatrix(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Stateid :" + stateid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_EscalationMatrix(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Stateid :" + stateid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_EscalationMatrix(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }
                    if (dataexistornot.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Stateid :" + stateid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_EscalationMatrix(dataexistornot.Message, dataexistornot.Status, "", null);
                    }


                }
                if (dataexistornot.Status == "200")
                {
                    //Save Api log data//
                    #region save request and response data in api log

                    var escalationmatrix = func_EscalationMatrix(stateid);
                    string RequestParameter = "UserID :" + user_id + ",Stateid :" + stateid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_EscalationMatrix(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, escalationmatrix);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Stateid :" + stateid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_EscalationMatrix(exception.Message, exception.Status, "", null);
            }

            return "";

        }



        [System.Web.Http.HttpGet]
        [ActionName("Userverification")]
        public object Userverification(string user_id, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel,string fcm_token)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {



                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);


                if (getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Userverification(getAppMessage.Message, getAppMessage.Status, "0");
                    }
                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Userverification(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "0");
                    }
                }
                else
                {
                    var userverification = func_Userverification(user_id, device_id, app_version, os_version_code, os_type, fcm_token);
                    if (userverification.Status == "200" || userverification.Status == "0")
                    {
                        //Save Api log data//
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + userverification.Message + ",Status : " + userverification.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, userverification.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        //End Save Api log data//

                        return getJson_Userverification(userverification.Message, userverification.Status, userverification.Token);

                    }
                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_Userverification(exception.Message, exception.Status, "0");
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("Get_UserLanguage")]
        public object User_Language(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_Language();


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Language(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Language(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Language(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }

                    if (dataexistornot.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_Language(dataexistornot.Message, dataexistornot.Status, "", null);
                    }

                }
                if (dataexistornot.Status == "200")
                {
                    //Save Api log data//
                    #region save request and response data in api log

                    var languagdata = func_user_Language();
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_Language(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, languagdata);

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
                return getJson_Language(exception.Message, exception.Status, "", null);
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("Save_UserLanguage")]
        public object Save_UserLanguage(string user_id,string languagecode, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);

                var savelanguage = func_Savelanguage(user_id, device_id, token, languagecode);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",LanguageCode :" + languagecode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :"+device_name+",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_SaveUserLanguage(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",LanguageCode :" + languagecode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_SaveUserLanguage(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",LanguageCode :" + languagecode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_SaveUserLanguage(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }



                }
                if (savelanguage.Status == "200" || savelanguage.Status == "0")
                {
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",LanguageCode :" + languagecode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + savelanguage.Message + ",Status : " + savelanguage.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, savelanguage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_SaveUserLanguage(savelanguage.Message, savelanguage.Status, getTokenMessage.Token);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",LanguageCode :" + languagecode + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_SaveUserLanguage(exception.Message, exception.Status, "");
            }

            return "";

        }


        [System.Web.Http.HttpGet]
        [ActionName("Save_luminous_log")]
        public object Save_luminous_log(string user_id, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel, string pagename)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);



                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Pagename:" + pagename + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_saveservice_log(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Pagename:" + pagename + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_saveservice_log(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Pagename:" + pagename + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_saveservice_log(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }


                }
                else
                {
                    MessageData msg = new MessageData();


                    msg.Message = "Data  inserted successfully.";
                    msg.Status = "200";
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Pagename:" + pagename + "";
                    string ResponseParameter = "Message :Data fetched successfully ,Status : 200";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, "", user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_saveservice_log(msg.Message, msg.Status, getTokenMessage.Token);
                }





            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + ",Pagename:" + pagename + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_saveservice_log(exception.Message, exception.Status, "");

            }

            return "";

        }


        //Implement Connect Assist//

        [System.Web.Http.HttpPost]
        [ActionName("SaveTicket")]
        public object SaveTicket([FromBody]SaveTicket value)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(value.user_id, value.app_version, value.device_id, value.token);
                var getAppMessage = getAppversion(value.app_version, value.os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(value.user_id, value.device_id);

                var savelanguage = func_SaveTicket(value.user_id, value.attachmentname, value.attachment, value.serialno, value.description, value.status,value.connectplus_message);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + value.user_id + ",Attachmentname :" + value.attachmentname + ",Serialno :" + value.serialno + ",Description :" + value.description + ",Status :" + value.status + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                        return getJson_SaveTicket(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + value.user_id + ",Attachmentname :" + value.attachmentname + ",Serialno :" + value.serialno + ",Description :" + value.description + ",Status :" + value.status + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                        return getJson_SaveTicket(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + value.user_id + ",Attachmentname :" + value.attachmentname + ",Serialno :" + value.serialno + ",Description :" + value.description + ",Status :" + value.status + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                        return getJson_SaveTicket(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }



                }
                if (savelanguage.Status == "200" || savelanguage.Status == "0")
                {
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + value.user_id + ",Attachmentname :" + value.attachmentname + ",Serialno :" + value.serialno + ",Description :" + value.description + ",Status :" + value.status + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                    string ResponseParameter = "Message : " + savelanguage.Message + ",Status : " + savelanguage.Status + "";
                    #endregion
                    SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 0, savelanguage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                    //End Save Api log data//

                    return getJson_SaveTicket(savelanguage.Message, savelanguage.Status, getTokenMessage.Token);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + value.user_id + ",Attachmentname :" + value.attachmentname + ",Serialno :" + value.serialno + ",Description :" + value.description + ",Status :" + value.status + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                return getJson_SaveTicket(exception.Message, exception.Status, "");
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("ViewTicket")]
        public object ViewTicket(string user_id, int ticketid, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_ViewTicket(ticketid);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",TicketId :" + ticketid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_ViewTicket(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",TicketId :" + ticketid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_ViewTicket(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",TicketId :" + ticketid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_ViewTicket(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }

                    if (dataexistornot.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",TicketId :" + ticketid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_ViewTicket(dataexistornot.Message, dataexistornot.Status, "", null);
                    }

                }
                if (dataexistornot.Status == "200")
                {
                    //Save Api log data//
                    #region save request and response data in api log

                    var Viewticket_data = func_viewticket(user_id, ticketid);
                    string RequestParameter = "UserID :" + user_id + ",TicketId :" + ticketid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_ViewTicket(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, Viewticket_data);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",TicketId :" + ticketid + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_Language(exception.Message, exception.Status, "", null);
            }

            return "";

        }

        [System.Web.Http.HttpGet]
        [ActionName("GetTicketList")]
        public object GetTicketList(string user_id, int month, int year, string token, string app_version, string device_id, string device_name, string os_type, string os_version_name, string os_version_code, string ip_address, string language, string screen_name, string network_type, string network_operator, string time_captured, string channel)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(user_id, app_version, device_id, token);
                var getAppMessage = getAppversion(app_version, os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(user_id, device_id);
                var dataexistornot = Checked_data_existornot_getTicket(user_id, month, year);


                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "" || dataexistornot.Status == "0")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Month :" + month + ",Year :" + year + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_GetTicket(getTokenMessage.Message, getTokenMessage.Status, "", null);
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Month :" + month + ",Year :" + year + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_GetTicket(getAppMessage.Message, getAppMessage.Status, "", null);
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Month :" + month + ",Year :" + year + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_GetTicket(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "", null);
                    }

                    if (dataexistornot.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + user_id + ",Month :" + month + ",Year :" + year + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                        string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                        #endregion
                        SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                        return getJson_GetTicket(dataexistornot.Message, dataexistornot.Status, "", null);
                    }

                }
                if (dataexistornot.Status == "200")
                {
                    //Save Api log data//
                    #region save request and response data in api log

                    var Getticket_data = func_getticket(user_id, month, year);
                    string RequestParameter = "UserID :" + user_id + ",Month :" + month + ",Year :" + year + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                    string ResponseParameter = "Message : " + dataexistornot.Message + ",Status : " + dataexistornot.Status + "";
                    #endregion
                    SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 0, dataexistornot.Message, user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                    //End Save Api log data//

                    return getJson_GetTicket(dataexistornot.Message, dataexistornot.Status, getTokenMessage.Token, Getticket_data);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + user_id + ",Month :" + month + ",Year :" + year + ",Token :" + token + ",DeviceID :" + device_id + ",DeviceName :" + device_name + ",AppVersion :" + app_version + ",OsType :" + os_type + ",OsVersion :" + os_version_code + ",OSVersionName :" + os_version_name + ",IPAddress :" + ip_address + ",Language :" + language + ",ScreenName :" + screen_name + ",NetworkType :" + network_type + ",NetworkOperator :" + network_operator + ",TimeCaptured :" + time_captured + ",Channel :" + channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), user_id, DateTime.Now, device_id, app_version, os_type, os_version_code);
                return getJson_Language(exception.Message, exception.Status, "", null);
            }

            return "";

        }

        [System.Web.Http.HttpPost]
        [ActionName("UpdateTicket")]
        public object UpdateTicket([FromBody]SaveTicket value)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(value.user_id, value.app_version, value.device_id, value.token);
                var getAppMessage = getAppversion(value.app_version, value.os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(value.user_id, value.device_id);

                var savelanguage = func_UpdateTicket(value.user_id, value.ticketid, value.attachmentname, value.attachment, value.serialno, value.description, value.status);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + value.user_id + ",Attachmentname :" + value.attachmentname + ",Serialno :" + value.serialno + ",Description :" + value.description + ",Status :" + value.status + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                        return getJson_SaveTicket(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + value.user_id + ",Attachmentname :" + value.attachmentname + ",Serialno :" + value.serialno + ",Description :" + value.description + ",Status :" + value.status + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, getAppMessage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                        return getJson_SaveTicket(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + value.user_id + ",Attachmentname :" + value.attachmentname + ",Serialno :" + value.serialno + ",Description :" + value.description + ",Status :" + value.status + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, checkedThreeUserLoggedIn.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                        return getJson_SaveTicket(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }



                }
                if (savelanguage.Status == "200" || savelanguage.Status == "0")
                {
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + value.user_id + ",Attachmentname :" + value.attachmentname + ",Serialno :" + value.serialno + ",Description :" + value.description + ",Status :" + value.status + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                    string ResponseParameter = "Message : " + savelanguage.Message + ",Status : " + savelanguage.Status + "";
                    #endregion
                    SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 0, savelanguage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                    //End Save Api log data//

                    return getJson_SaveTicket(savelanguage.Message, savelanguage.Status, getTokenMessage.Token);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + value.user_id + ",Attachmentname :" + value.attachmentname + ",Serialno :" + value.serialno + ",Description :" + value.description + ",Status :" + value.status + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                return getJson_SaveTicket(exception.Message, exception.Status, "");
            }

            return "";

        }


        //End//

        //Conct Plus save data//


        [System.Web.Http.HttpPost]
        [ActionName("Save_ConnectPlusDataEntry")]
        public object Save_ConnectPlusDataEntry([FromBody]ConnectPlusData value)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);

            try
            {


                var getTokenMessage = getToken(value.user_id, value.app_version, value.device_id, value.token);
                var getAppMessage = getAppversion(value.app_version, value.os_type);
                var checkedThreeUserLoggedIn = checked_ThreeUserLoggedIn(value.user_id, value.device_id);

                var contactus_suggestion = func_SaveConnectPlus(value.user_id, value.serialno, value.dlrCode, value.discode, value.saledate, value.customername, value.customerphone, value.customerlandLinenumber, value.customerstate, value.customercity, value.customeraddress, value.ismtype, value.connectplusimage_name, value.connectplusimage);

                if (getTokenMessage.Status != "" || getAppMessage.Status != "" || checkedThreeUserLoggedIn.Status != "")
                {
                    if (getTokenMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + value.user_id + ",SerialNo :" + value.serialno + ",DealerCode :" + value.dlrCode + ",DistCode :" + value.discode + ",Saledate :" + value.saledate + ",CustomerName :" + value.customername + ",CustomerPhone :" + value.customerphone + ",CustomerLandlineNo :" + value.customerlandLinenumber + ",CustomerState :" + value.customerstate + ",CustomerCity :" + value.customercity + ",CustomerAddress :" + value.customeraddress + ",IsMtype :" + value.ismtype + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                        string ResponseParameter = "Message : " + getTokenMessage.Message + ",Status : " + getTokenMessage.Status + "";
                        #endregion
                        SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                        return getJson_SaveConnectPlus(getTokenMessage.Message, getTokenMessage.Status, "");
                    }
                    if (getAppMessage.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + value.user_id + ",SerialNo :" + value.serialno + ",DealerCode :" + value.dlrCode + ",DistCode :" + value.discode + ",Saledate :" + value.saledate + ",CustomerName :" + value.customername + ",CustomerPhone :" + value.customerphone + ",CustomerLandlineNo :" + value.customerlandLinenumber + ",CustomerState :" + value.customerstate + ",CustomerCity :" + value.customercity + ",CustomerAddress :" + value.customeraddress + ",IsMtype :" + value.ismtype + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                        string ResponseParameter = "Message : " + getAppMessage.Message + ",Status : " + getAppMessage.Status + "";
                        #endregion

                        SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                        return getJson_SaveConnectPlus(getAppMessage.Message, getAppMessage.Status, "");
                    }

                    if (checkedThreeUserLoggedIn.Status != "")
                    {
                        #region save request and response data in api log
                        string RequestParameter = "UserID :" + value.user_id + ",SerialNo :" + value.serialno + ",DealerCode :" + value.dlrCode + ",DistCode :" + value.discode + ",Saledate :" + value.saledate + ",CustomerName :" + value.customername + ",CustomerPhone :" + value.customerphone + ",CustomerLandlineNo :" + value.customerlandLinenumber + ",CustomerState :" + value.customerstate + ",CustomerCity :" + value.customercity + ",CustomerAddress :" + value.customeraddress + ",IsMtype :" + value.ismtype + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                        string ResponseParameter = "Message : " + checkedThreeUserLoggedIn.Message + ",Status : " + checkedThreeUserLoggedIn.Status + "";
                        #endregion
                        SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, getTokenMessage.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                        return getJson_SaveConnectPlus(checkedThreeUserLoggedIn.Message, checkedThreeUserLoggedIn.Status, "");
                    }



                }
                if (contactus_suggestion.Status == "200" || contactus_suggestion.Status == "0")
                {
                    //Save Api log data//
                    #region save request and response data in api log
                    string RequestParameter = "UserID :" + value.user_id + ",SerialNo :" + value.serialno + ",DealerCode :" + value.dlrCode + ",DistCode :" + value.discode + ",Saledate :" + value.saledate + ",CustomerName :" + value.customername + ",CustomerPhone :" + value.customerphone + ",CustomerLandlineNo :" + value.customerlandLinenumber + ",CustomerState :" + value.customerstate + ",CustomerCity :" + value.customercity + ",CustomerAddress :" + value.customeraddress + ",IsMtype :" + value.ismtype + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                    string ResponseParameter = "Message : " + contactus_suggestion.Message + ",Status : " + contactus_suggestion.Status + "";
                    #endregion
                    SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 0, contactus_suggestion.Message, value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                    //End Save Api log data//

                    return getJson_SaveConnectPlus(contactus_suggestion.Message, contactus_suggestion.Status, getTokenMessage.Token);

                }

            }
            catch (Exception exc)
            {
                var exception = getTryCatchExc();

                #region save request and response data in api log
                string RequestParameter = "UserID :" + value.user_id + ",SerialNo :" + value.serialno + ",DealerCode :" + value.dlrCode + ",DistCode :" + value.discode + ",Saledate :" + value.saledate + ",CustomerName :" + value.customername + ",CustomerPhone :" + value.customerphone + ",CustomerLandlineNo :" + value.customerlandLinenumber + ",CustomerState :" + value.customerstate + ",CustomerCity :" + value.customercity + ",CustomerAddress :" + value.customeraddress + ",IsMtype :" + value.ismtype + ",Token :" + value.token + ",DeviceID :" + value.device_id + ",DeviceName :" + value.device_name + ",AppVersion :" + value.app_version + ",OsType :" + value.os_type + ",OsVersion :" + value.os_version_code + ",OSVersionName :" + value.os_version_name + ",IPAddress :" + value.ip_address + ",Language :" + value.language + ",ScreenName :" + value.screen_name + ",NetworkType :" + value.network_type + ",NetworkOperator :" + value.network_operator + ",TimeCaptured :" + value.time_captured + ",Channel :" + value.channel + "";
                string ResponseParameter = "Message : " + exception.Message + ",Status : " + exception.Status + "";
                #endregion
                SaveServiceLog(value.user_id, url, RequestParameter, ResponseParameter, 1, exc.InnerException.ToString(), value.user_id, DateTime.Now, value.device_id, value.app_version, value.os_type, value.os_version_code);
                return getJson_SaveConnectPlus(exception.Message, exception.Status, "");
            }

            return "";

        }


        //End//


        #region check token valid or not
        public MessageData getToken(string user_id, string app_version, string deviceid, string token)
        {
            string LoginToken = "";


            string TokenString = user_id + app_version + deviceid;
            MessageData msgdata = new MessageData();
            LuminousEntities luminous = new LuminousEntities();

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

                    var gettokendata = luminous.UserVerifications.Where(c => c.UserId == user_id && c.DeviceId == deviceid).Select(c => new { c.Token, c.TokenFlag }).SingleOrDefault();
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

                            LoginToken = GetMd5Hash(md5Hash, TokenString);


                        }
                        //luminous.ExecuteStoreCommand("Update Userverification set Token='" + LoginToken + "' where userid='" + user_id + "' and deviceid='" + deviceid + "'");
                        luminous.ExecuteStoreCommand("Update Userverification set Token='" + LoginToken + "',AppVersion='" + app_version + "',TokenFlag=1 where userid='" + user_id + "' and deviceid='" + deviceid + "'");
                        msgdata.Message = "";
                        msgdata.Status = "";
                        msgdata.Token = LoginToken;

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
        public MessageData getAppversion(string app_version, string ostype)
        {
            LuminousEntities luminous = new LuminousEntities();
            var appVersionCount = luminous.AppVersions.Where(c => c.Version == app_version && c.AppOs == ostype).Count();
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


        #region check three user logged in
        public MessageData checked_ThreeUserLoggedIn(string user_id, string device_id)
        {
            LuminousEntities luminous = new LuminousEntities();
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


        #region Checked data exist or not by page name
        public MessageData Checked_data_existornot(string pagename, string parentid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            if (pagename == "ProductCategory")
            {
                var getProductcategory = luminous.ProductCatergories.Where(c => c.Pstatus == 1).Select(c => new { c.id, c.PName, c.PdfSystemName }).ToList();

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
                                   where c.Startdate <= currentdate && c.Enddate >= currentdate && c.Pagename == pagename 
                                   && c.Subcatid == parentid && c.Status==1
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

        #region Checked data exist or not catalog_products
        public MessageData Checked_data_existornot_catalog_products(int productcategoryid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();

            var getproduct_catalog = (from prdcat in luminous.ProductLevelThrees
                                      join prdimages in luminous.ProductthreeImageMappings
                                      on prdcat.id equals prdimages.ProductLevelThreeid
                                      where prdcat.productCategoryid == productcategoryid && prdcat.PlTwStatus == 1
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
        public MessageData Checked_data_existornot_search_products(string search_key)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();

            var getproduct_catalog = (from prdcat in luminous.ProductLevelThrees
                                      where prdcat.PlTwStatus == 1 && prdcat.Name.Contains(search_key)
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



        #region Checked data exist or not search_products
        public MessageData Checked_data_existornot_getdata_searchproduct(string productname)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();

            var getproduct_catalog = (from prdcat in luminous.ProductLevelThrees
                                      where prdcat.Name == productname && prdcat.PlTwStatus == 1
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


        #region Checked data exist or not catalog_products
        public MessageData Checked_data_existornot_catalog_upper(int productcategoryid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();


            var getproduct_catalog = luminous.getCatalog_Upper(productcategoryid).ToList();

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
        public MessageData Checked_data_existornot_contactus_details()
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();

            var contactusdata = luminous.contactUsDetails.Where(c => c.Cstatus == 1).Count();

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

        #region Checked data exist or not gallery main data
        public MessageData Checked_data_existornot_gallery_data(int categoryid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();

            var getGallery_data = luminous.MediaDatas.Where(c => c.Status == 1 && c.LabelId == categoryid).Count();

            if (getGallery_data == 0)
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

        #region Checked data exist or not customer permission
        public MessageData Checked_data_existornot_customerpermission_data(string userid, string language)
        {
            LuminousEntities luminous = new LuminousEntities();
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


        #region Checked data exist or not LSD_GetDistributorCount
        public MessageData Checked_data_existornot_LSD_GetDistributorCount(string userid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();

            var geteligiblecount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Count();

            if (geteligiblecount == 0)
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

        #region Checked data exist or not LSD_GetDistOpenReimbursmentReport
        public MessageData Checked_data_existornot_LSD_GetDistOpenReimbursmentReport(string userid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();

            var getDealerQrCode_Time = (from c in luminous.LSD_Master
                                        join Pi in luminous.Lsd_GiftMaster on c.GiftId equals Pi.GiftId

                                        where c.ActivationDistCode == userid && c.RedemptionDealerCode != null && c.RedemptionDealerSecretCode != null && c.ClaimDistCode == null
                                        select new
                                        {
                                            RedemptionDealerCode = c.RedemptionDealerCode,
                                            RedemptionDealerName = c.RedemptionDealerName,
                                            SecretCode = c.SecretCode,
                                            GiftName = Pi.GiftName,
                                            DistributorActivatedDate = c.ActivationDistOn,
                                            RedemptionDealerOn = c.RedemptionDealerOn,
                                            ActivatedQrCode = c.ActivatedQrCode,
                                            Id = c.Id
                                        }).Count();

            if (getDealerQrCode_Time == 0)
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

        #region Checked data exist or not LSD_GetRedeemedReport
        public MessageData Checked_data_existornot_LSD_GetRedeemedReport(string userid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();

            var getDealerReedemedData = (from c in luminous.LSD_Master
                                         join Pi in luminous.Lsd_GiftMaster on c.GiftId equals Pi.GiftId

                                         where c.ActivationDistCode == userid && c.RedemptionDealerCode != null && c.ClaimDistCode != null
                                         select new
                                         {
                                             DealerCode = c.RedemptionDealerCode,
                                             DealerName = c.RedemptionDealerName,
                                             SecretCode = c.SecretCode,
                                             GiftName = Pi.GiftName,
                                             DistributorActivatedDate = c.ActivationDistOn,
                                             DealerredeptionDatetime = c.RedemptionDealerOn,
                                             ClaimSubmission = c.ClaimDistOn
                                         }).Count();

            if (getDealerReedemedData == 0)
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

        #region Checked data exist or not LSD_GetActivatedCouponReport
        public MessageData Checked_data_existornot_LSD_GetActivatedCouponReport(string userid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();

            var getDistQrCode_Time = luminous.LSD_Master.Where(c => c.ActivationDistCode == userid).Count();

            if (getDistQrCode_Time == 0)
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



        #region Checked data exist or not LSD_DealerReport
        public MessageData Checked_data_existornot_LSD_DealerReport(string userid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();

            var getDealerReport = (from c in luminous.LSD_Master
                                   join Pi in luminous.Lsd_GiftMaster on c.GiftId equals Pi.GiftId
                                   where c.RedemptionDealerCode == userid
                                   select new
                                   {
                                       barcode = c.RedemtionDealerBarCode,
                                       secretcode = c.RedemptionDealerSecretCode,
                                       ReimbursmentDate_Time = c.RedemptionDealerOn,
                                       GiftName = Pi.GiftName,
                                       DistName = c.ActivationDistName
                                   }).Count();


            if (getDealerReport == 0)
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


        #region Checked data exist or not survey_notification
        public MessageData Checked_data_existornot_SurveyNotificationList(string userid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            //  DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
            var currentdate = DateTime.Now.Date;
            var data = from cl in luminous.NotificationSurveys
                       where (
                           from i in luminous.SaveNotificationSurveys
                           where i.SurveyID == cl.SurveyID && i.UserId == userid
                           select i).Count() == 0
                       select new { SurveyID = cl.SurveyID, Survey = cl.Survey, StartDate = cl.StartDate, Enddate = cl.Enddate };

            var Surveylist = (from cdata in data
                              where cdata.StartDate <= currentdate && cdata.Enddate >= currentdate
                              select new
                              {
                                  SurveyID = cdata.SurveyID,
                                  Survey = cdata.Survey
                              }).Count();


            if (Surveylist == 0)
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

        #region Checked data exist or not survey_notification_Question
        public MessageData Checked_data_existornot_GetSurveyQuestion(int Surveyid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            //  DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
            var Surveylist = luminous.NotificationSurveys.Where(c => c.SurveyID == Surveyid).Count();

            if (Surveylist == 0)
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


        #region Checked data exist or not GetAlertnotification
        public MessageData Checked_data_existornot_GetAlertnotification(string userid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            //  DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));



            if (userid != "" || userid != null)
            {
                msgdata.Status = "200";
                msgdata.Message = "Data  fetched successfully.";
            }



            return msgdata;
        }
        #endregion


        #region Checked data exist or not LSD_SaveDealerScanCode
        public MessageData Checked_data_existornot_LSD_SaveDealerScanCode(string userid, string barcode, string secretcode)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();

            var barcodenotactive = luminous.LSD_Master.Where(c => c.Barcode == barcode && c.ActivationDistCode == null).Count();
            if (barcodenotactive == 0)
            {
                msgdata.Status = "200";
                msgdata.Message = "Data  fetched successfully.";
                var matchbarcode_secretcode = luminous.LSD_Master.Where(c => c.Barcode == barcode && c.SecretCode == secretcode).Count();
                if (matchbarcode_secretcode > 0)
                {

                    var coupontype = luminous.LSD_Master.Where(c => c.Barcode == barcode && c.SecretCode == secretcode).Select(c => c.CouponType).SingleOrDefault();

                    if (coupontype == "G")
                    {

                        var checkcouponcount_exist = luminous.LSD_DealerCouponCount.Where(c => c.CreatedBy == userid).Count();
                        if (checkcouponcount_exist > 0)
                        {
                            var coupnbalance = luminous.LSD_DealerCouponCount.Where(c => c.CreatedBy == userid).Select(c => c.Gold_DealerBalanceCount).SingleOrDefault();
                            if (coupnbalance <= 0)
                            {
                                msgdata.Message = "You have not scanned 7 blue coupons therefore not eligible for activating gold coupon.";
                                msgdata.Status = "0";
                            }
                            else
                            {

                                var checkexist = luminous.LSD_Master.Where(c => c.ActivatedQrCode != null && c.ActivationDistCode != null && c.RedemtionDealerBarCode == barcode && c.RedemptionDealerSecretCode == secretcode).ToList();

                                if (checkexist.Count() > 0)
                                {
                                    msgdata.Message = "BarCode already submitted.";
                                    msgdata.Status = "0";

                                }
                                else
                                {
                                    msgdata.Status = "200";
                                    msgdata.Message = "Data  fetched successfully.";

                                }

                            }
                        }
                        else
                        {

                            msgdata.Message = "You have not scanned 7 blue coupons therefore not eligible for activating gold coupon.";
                            msgdata.Status = "0";
                        }

                    }
                    else
                    {

                        msgdata.Status = "200";
                        msgdata.Message = "Data  fetched successfully.";
                        var checkexist = luminous.LSD_Master.Where(c => c.ActivatedQrCode != null && c.ActivationDistCode != null && c.RedemtionDealerBarCode == barcode && c.RedemptionDealerSecretCode == secretcode).ToList();

                        if (checkexist.Count() > 0)
                        {
                            msgdata.Message = "BarCode already submitted.";
                            msgdata.Status = "0";

                        }
                        else
                        {
                            msgdata.Status = "200";
                            msgdata.Message = "Data  fetched successfully.";

                        }
                    }

                }
                else
                {

                    msgdata.Message = "Invalid Barcode or Serial no.";
                    msgdata.Status = "0";
                }
            }
            else
            {

                msgdata.Message = "Coupon Not Active.";
                msgdata.Status = "0";
            }

            return msgdata;
        }
        #endregion


        #region Checked data exist or not EscalationMatrix
        public MessageData Checked_data_existornot_EscalationMatrix(int stateid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            //  DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
            var dataexist = luminous.EscalationMatrices.Where(c => c.StateId == stateid).Count();


            if (dataexist == 0)
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

        #region Checked data exist or not language
        public MessageData Checked_data_existornot_Language()
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            //  DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
            var dataexist = luminous.LanguageMasters.Count();


            if (dataexist == 0)
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



        #region Checked data exist or not view ticket
        public MessageData Checked_data_existornot_ViewTicket(int ticketid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            //  DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
            var dataexist = luminous.ConnectAssists.Where(c => c.Id == ticketid).Count();


            if (dataexist == 0)
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

        #region Checked data exist or not view ticket
        public MessageData Checked_data_existornot_getTicket(string user_id, int month, int year)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            //  DateTime dateExist = Convert.ToDateTime(Convert.ToDateTime(date).ToString("yyyy-MM-dd"));


            var dataexist = (from x in luminous.ConnectAssists
                             where x.CreatedOn.Value.Month == month && x.CreatedOn.Value.Year == year && x.Userid == user_id
                             orderby x.Id ascending
                             select x).Count();


            if (dataexist == 0)
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


        #region check try catch exception
        public MessageData getTryCatchExc()
        {
            MessageData msgdata = new MessageData();
            msgdata.Message = "Some Exception has occurred";

            msgdata.Status = "201";

            return msgdata;
        }
        #endregion




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

            return "";
        }
        //End JSON card provider data

        // JSON product category data//

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

        //End JSON product cateegory data//

        // JSON product category data//

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

        //End JSON product cateegory data//


        // JSON product search data//

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

        //End JSON product search data//


        // JSON product search product data//

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

        //End JSON product search product data//

        // JSON product getJson_Product_catalog_menu_upper//

        public object getJson_Product_catalog_menu_upper(string message, string status, string token, List<Luminous.MpartnerNewApi.Model.Catalog_Menu_Upper> carddata)
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

        //End JSON product getJson_Product_catalog_menu_upper//

        // JSON product getJson_contactusdetails//

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

        //End JSON product getJson_contactusdetails//

        // JSON product getJson_SaveserviceLog//

        public object getJson_saveservice_log(string message, string status, string token)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        //End JSON product getJson_SaveserviceLog//



        // JSON product getJson_contactus_suggestion//

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

        //End JSON product getJson_contactus_suggestion//

       

        // JSON getJson_gallery_menu_upper//

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

        //End JSON getJson_gallery_menu_upper//


        // JSON getJson_gallery_maindata//

        public object getJson_gallery_maindata(string message, string status, string token, List<Gallery_Details> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                gallery_data = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        //End JSON getJson_gallery_maindata//

        // JSON getJson_media_maindata//

        public object getJson_media_maindata(string message, string status, string token, List<Gallery_Details> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                media_data = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        //End JSON getJson_media_maindata//

        // JSON getJson_faq//

        public object getJson_faq(string message, string status, string token, List<Faq> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                faq_data = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        //End JSON getJson_faq//

        // JSON getJson_userpermission//

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

        //End JSON getJson_faq//


        // JSON getJson_LSD_GetDistributorCount//

        public object getJson_LSD_GetDistributorCount(string message, string status, string token, List<Lsd_CouponCountData> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                lsd_coupon_count_data = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        //End JSON getJson_LSD_GetDistributorCount//


        // JSON product getJson_contactus_suggestion//

        public object getJson_lsd_saveqrcode(string message, string status, string token)
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

        //End JSON product getJson_contactus_suggestion//


        // JSON LSD_GetDistOpenReimbursmentReport//

        public object getJson_LSD_GetDistOpenReimbursmentReport(string message, string status, string token, List<Lsd_OpenReimbursmentData> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                lsd_open_reimbursment_data = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        //End JSON LSD_GetDistOpenReimbursmentReport//

        // JSON product getJson_contactus_suggestion//

        public object getJson_lsd_save_claimdata(string message, string status, string token)
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

        //End JSON product getJson_contactus_suggestion//

        // JSON LSD_GetRedeemedReport//

        public object getJson_LSD_GetRedeemedReport(string message, string status, string token, List<Lsd_DealerRedeemedData> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                lsd_dealer_redeemed_data = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        //End JSON LSD_GetDistOpenReimbursmentReport//


        // JSON LSD_GetActivatedCouponReport//

        public object getJson_LSD_GetActivatedCouponReport(string message, string status, string token, List<Lsd_DistActivatedCoupon> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                lsd_dist_Activated_data = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        //End JSON LSD_GetDistOpenReimbursmentReport//

        // JSON LSD_DealerReport//

        public object getJson_LSD_DealerReport(string message, string status, string token, List<Lsd_DealerReport> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                lsd_dealer_report = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        //End JSON LSD_DealerReport//



        // JSON LSD_SaveDealerScanCode//

        public object getJson_LSD_SaveDealerScanCode(string message, string status, string token, List<Lsd_DealerGiftData> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                lsd_dealer_gift = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        //End JSON LSD_SaveDealerScanCode//


        // JSON Survey_Notification//

        public object getJson_SurveyNotificationList(string message, string status, string token, List<SurveyNotification> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                survey_notification = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        //End JSON Survey_Notification//



        // JSON Survey_Notification_Question//

        public object getJson_GetSurveyQuestion(string message, string status, string token, List<SurveyNotification_Question> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                survey_notification_ques = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        //End JSON Survey_Notification_Question//


        // JSON product getJson_SaveSurveyResult//

        public object getJson_SaveSurveyResult(string message, string status, string token)
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

        //End JSON product getJson_SaveSurveyResult//


        // JSON GetAlertnotification//

        public object getJson_GetAlertnotification(string message, string status, string token, List<Luminous.MpartnerNewApi.Model.AlertNotifications> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                survey_notification_alert = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }

        //End JSON GetAlertnotification//

        // JSON product getJson_IsReadCheck_AlertNotification//

        public object getJson_IsReadCheck_AlertNotification(string message, string status, string token)
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

        //End JSON product getJson_SaveSurveyResult//

        //JSON getJson_Reports

        public object getJson_LumReports(string message, string status, string token)
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


        //End JSON getJson_Reports


        //JSON getJson_EscalationMatrix

        public object getJson_EscalationMatrix(string message, string status, string token, List<EsacalationMatrix> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                escalation_matrix = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }


        //End JSON getJson_Reports

        //JSON getJson_Userverification

        public object getJson_Userverification(string message, string status, string token)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }


        //End JSON getJson_Userverification

        //JSON getJson_Language

        public object getJson_Language(string message, string status, string token, List<Language> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                language = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }


        //End JSON getJson_Language

        // JSON product getJson_SaveUserLanguage//

        public object getJson_SaveUserLanguage(string message, string status, string token)
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

        //End JSON product getJson_SaveUserLanguage//

        // JSON product getJson_SaveTicket//

        public object getJson_SaveTicket(string message, string status, string token)
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

        //End JSON product getJson_SaveTicket//




        //JSON getJson_ViewTicket

        public object getJson_ViewTicket(string message, string status, string token, List<ViewTicket> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                viewticket_data = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }


        //End JSON getJson_ViewTicket

        //JSON getJson_GetTicket

        public object getJson_GetTicket(string message, string status, string token, List<ViewTicket> carddata)
        {

            var json = JsonConvert.SerializeObject(new
            {
                Message = message,
                Status = status,
                Token = token,
                getticket_data = carddata
            });

            //Create a HTTP response - Set to OK
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Return the Response
            return res;
        }


        //End JSON getJson_ViewTicket

        // JSON product getJson_SaveConnectPlus//

        public object getJson_SaveConnectPlus(string message, string status, string token)
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

        //End JSON product getJson_SaveTicket//

        //JSON getJson_ParentCategory

        public object getJson_ParentCategory(string message, string status, string token, List<MpartnerNewApi.Model.ParentCategory> carddata)
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


        //End JSON getJson_ParentCategory

        //Check card provider data//


        public List<HomePage> getCardprovider(string userid, string pagename, string parentid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<HomePage> HomepageData = new List<HomePage>();
            MessageData msgdata = new MessageData();
            //Get Card Data List
            List<Carddata> crddata = new List<Carddata>();


            try
            {
                var currentdate = DateTime.Now.Date;
                if (pagename != "Scheme" && pagename != "Price")
                {
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
                                           carddataflag = c.CardDataFlag,
                                           pagename = c.Pagename,
                                           pdfurl = c.PdfOriginalName,
                                           subcategory = c.Subcatname,
                                           mainimage = c.SystemMainImage,
                                           status = c.Status,
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
                                if (data.card_action == "Lucky7" || data.card_action == "Lucky7Dealer")
                                {
                                    var custtype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.CustomerType).SingleOrDefault();
                                    if (data.card_action == "Lucky7")
                                    {
                                        if (custtype == "DISTY")
                                        {
                                            pr.card_action = data.card_action;
                                        }
                                        else
                                        {
                                            pr.card_action = "Lucky7Dealer";
                                        }
                                    }
                                    if (data.card_action == "Lucky7Dealer")
                                    {
                                        if (custtype == "DISTY")
                                        {
                                            pr.card_action = "Lucky7";
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


                                pr.background_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/CardImage/" + data.background_image;
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
                                pr.main_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/CardImage/" + data.mainimage;
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
                                                where ccd.DynamicHomePageId == data.id && c.Startdate <= currentdate && c.Enddate >= currentdate
                                                //where c.Pstatus == 1 && c.ParentCatid == getparentcatid
                                                //orderby c.ordersequence
                                                select new
                                                {

                                                    title = ccd.Title,
                                                    card_action = ccd.DeepLink,
                                                    background_image = ccd.Background_image,
                                                    main_image = ccd.Main_image,
                                                    image_height = ccd.Image_height,
                                                    image_width = ccd.Image_width,
                                                }).ToList();

                                List<Bannerdata> bannerdata = new List<Bannerdata>();
                                foreach (var cdata in carddata)
                                {


                                    if (data.carddataflag == "1")
                                    {
                                        Carddata Cdata = new Carddata();
                                        Cdata.title = cdata.title;

                                        Cdata.background_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/CardImage/" + cdata.background_image;

                                        Cdata.main_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/CardImage/" + cdata.main_image;

                                        if (cdata.card_action == "Lucky7" || cdata.card_action == "Lucky7Dealer")
                                        {
                                            var custtype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.CustomerType).SingleOrDefault();
                                            if (cdata.card_action == "Lucky7")
                                            {
                                                if (custtype == "DISTY")
                                                {
                                                    Cdata.card_action = cdata.card_action;
                                                }
                                                else
                                                {
                                                    Cdata.card_action = "Lucky7Dealer";
                                                }
                                            }
                                            if (cdata.card_action == "Lucky7Dealer")
                                            {
                                                if (custtype == "DISTY")
                                                {
                                                    Cdata.card_action = "Lucky7";
                                                }
                                                else
                                                {
                                                    Cdata.card_action = cdata.card_action;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            Cdata.card_action = cdata.card_action;
                                        }



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
                                        Cdata.background_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/CardImage/" + cdata.background_image;
                                        if (cdata.main_image == "")
                                        {
                                            Cdata.main_image = "";
                                        }
                                        else
                                        {
                                            Cdata.main_image = cdata.main_image;
                                        }


                                        if (cdata.card_action == "Lucky7" || cdata.card_action == "Lucky7Dealer")
                                        {
                                            var custtype = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.CustomerType).SingleOrDefault();
                                            if (cdata.card_action == "Lucky7")
                                            {
                                                if (custtype == "DISTY")
                                                {
                                                    Cdata.card_action = cdata.card_action;
                                                }
                                                else
                                                {
                                                    Cdata.card_action = "Lucky7Dealer";
                                                }
                                            }
                                            if (cdata.card_action == "Lucky7Dealer")
                                            {
                                                if (custtype == "DISTY")
                                                {
                                                    Cdata.card_action = "Lucky7";
                                                }
                                                else
                                                {
                                                    Cdata.card_action = cdata.card_action;
                                                }
                                            }

                                        }


                                        else
                                        {
                                            Cdata.card_action = cdata.card_action;
                                        }




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
                        Checked_data_existornot(pagename, "");
                    }

                }
                else
                    if (pagename == "Scheme" || pagename == "Price")
                    {

                        var getprice_scheme_data = luminous.GetPrice_SchemeByUserId(userid, pagename).Where(c => c.Startdate <= currentdate && c.Enddate >= currentdate && c.Pagename == pagename && c.status == 1 && c.Subcatid == parentid).ToList();

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
                                    pr.card_action = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/Pdf/" + data.pdfurl;
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


                                    pr.background_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/CardImage/" + data.background_image;
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
                                    pr.main_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/CardImage/" + data.mainimage;
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
                            Checked_data_existornot(pagename, "");
                        }


                    }



            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return HomepageData;
        }



        //End card provider data

        //Check connect+ card provider data


        public List<HomePage> getConnectPlusCardProvider(string pagename)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<HomePage> HomepageData = new List<HomePage>();
            MessageData msgdata = new MessageData();
            //Get Card Data List
            List<Carddata> crddata = new List<Carddata>();

            //Get Banner Data List
            List<Bannerdata> bannerdata = new List<Bannerdata>();
            try
            {
                var currentdate = DateTime.Now.Date;
                var getHomePage = (from c in luminous.Card_dynamicPage
                                   where c.Startdate <= currentdate && c.Enddate >= currentdate && c.Pagename == pagename
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
                        //if (data.pagename == "Scheme" || data.pagename == "Price")
                        //{
                        //    if (data.card_action == "")
                        //    {
                        //        pr.card_action = "";
                        //    }
                        //    else
                        //    {
                        //        pr.card_action = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/Pdf/" + data.pdfurl;
                        //    }

                        //}
                        //else
                        //{
                        if (data.card_action == "")
                        {
                            pr.card_action = "";
                        }
                        else
                        {
                            pr.card_action = data.card_action;
                        }
                        //}

                        if (data.background_image == "")
                        {
                            pr.background_image = "";
                        }
                        else
                        {


                            pr.background_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/CardImage/" + data.background_image;
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
                            pr.main_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/CardImage/" + data.mainimage;
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

                        if (data.carddataflag == "3" || data.carddataflag == "4")
                        {
                            var carddata = (from c in luminous.Card_dynamicPage
                                            join ccd in luminous.Card_CardData
                                                on c.Id equals ccd.DynamicHomePageId
                                            where c.Id == data.id && c.Startdate <= currentdate && c.Enddate >= currentdate
                                            //where c.Pstatus == 1 && c.ParentCatid == getparentcatid
                                            //orderby c.ordersequence
                                            select new
                                            {

                                                title = ccd.Title,
                                                card_action = ccd.DeepLink,
                                                background_image = ccd.Background_image,
                                                main_image = ccd.Main_image,
                                                image_height = ccd.Image_height,
                                                image_width = ccd.Image_width,
                                            }).ToList();

                            foreach (var cdata in carddata)
                            {



                                if (data.carddataflag == "3")
                                {
                                    // crddata.Add(Cdata);
                                    Bannerdata Cdata = new Bannerdata();
                                    Cdata.title = cdata.title;
                                    Cdata.background_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/CardImage/" + cdata.background_image;
                                    if (cdata.main_image == "")
                                    {
                                        Cdata.main_image = "";
                                    }
                                    else
                                    {
                                        Cdata.main_image = cdata.main_image;
                                    }

                                    Cdata.card_action = cdata.card_action;
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
                                if (data.carddataflag == "4")
                                {
                                    Carddata Cdata = new Carddata();
                                    Cdata.title = cdata.title;

                                    Cdata.background_image = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/CardImage/" + cdata.background_image;

                                    Cdata.main_image = "";
                                    Cdata.card_action = cdata.card_action;
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
                    Checked_data_existornot(pagename, "");
                }


            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return HomepageData;
        }

        //End connect+  card provider data

        //Get Product category//

        public List<Luminous.MpartnerNewApi.Model.ProductsCategory> getProductCategory(string pagename)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<Luminous.MpartnerNewApi.Model.ProductsCategory> Pcategorylist = new List<Luminous.MpartnerNewApi.Model.ProductsCategory>();
            MessageData msgdata = new MessageData();
            //Get Card Data List

            try
            {

                var getProductcategory = luminous.ProductCatergories.Where(c => c.Pstatus == 1).Select(c => new { c.id, c.PName, c.PdfSystemName, c.ordersequence }).OrderBy(c => c.ordersequence).ToList();

                if (getProductcategory.Count > 0)
                {

                    foreach (var data in getProductcategory)
                    {

                        Luminous.MpartnerNewApi.Model.ProductsCategory pcategory = new ProductsCategory();
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
                            pcategory.url_product_category_pdf = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "MpartnerNewApi/Pdf/" + data.PdfSystemName;
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

        //End Product Category//

        //Get Product Catalog//

        public List<Luminous.MpartnerNewApi.Model.ProductCatalog> getProduct_Catalog(int productcategoryid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<Luminous.MpartnerNewApi.Model.ProductCatalog> Pcataloglist = new List<Luminous.MpartnerNewApi.Model.ProductCatalog>();
            MessageData msgdata = new MessageData();


            try
            {


                var getproduct_catalog = (from prdcat in luminous.ProductLevelThrees
                                          join prdimages in luminous.ProductthreeImageMappings
                                          on prdcat.id equals prdimages.ProductLevelThreeid
                                          join prd2 in luminous.ProductLevelTwoes
                                       on prdcat.pc_Lv2_oneId equals prd2.id
                                          join attribute in luminous.ProductLevelOnes
                                          on prdcat.ProductLevelOne equals attribute.id into ps
                                          from p in ps.DefaultIfEmpty()
                                          where prdcat.productCategoryid == productcategoryid && prdcat.PlTwStatus == 1
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

                if (getproduct_catalog.Count > 0)
                {
                    // Checked_data_existornot(pagename);
                    foreach (var data in getproduct_catalog)
                    {
                        List<Luminous.MpartnerNewApi.Model.Technicalspecification> techspecification = new List<Luminous.MpartnerNewApi.Model.Technicalspecification>();
                        Luminous.MpartnerNewApi.Model.ProductCatalog pcatalog = new ProductCatalog();
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
                        if (productcategoryid == 30)
                        {
                            if (data.maxcurrentcharge == "" || data.maxcurrentcharge == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                else
                {
                    Checked_data_existornot_catalog_products(productcategoryid);
                }


            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return Pcataloglist;
        }

        //End Product Catalog Category//

             
        
        //Get Product Catalog Old//

        public List<Luminous.MpartnerNewApi.Model.ProductCatalog> getProduct_CatalogOld(int productcategoryid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<Luminous.MpartnerNewApi.Model.ProductCatalog> Pcataloglist = new List<Luminous.MpartnerNewApi.Model.ProductCatalog>();
            MessageData msgdata = new MessageData();


            try
            {


                var getproduct_catalog = (from prdcat in luminous.ProductLevelThrees
                                          join prdimages in luminous.ProductthreeImageMappings
                                          on prdcat.id equals prdimages.ProductLevelThreeid
                                          join prd2 in luminous.ProductLevelTwoes
                                       on prdcat.pc_Lv2_oneId equals prd2.id
                                          join attribute in luminous.ProductLevelOnes
                                          on prdcat.ProductLevelOne equals attribute.id into ps
                                          from p in ps.DefaultIfEmpty()
                                          where prdcat.productCategoryid == productcategoryid && prdcat.PlTwStatus == 1 
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
                                               prd2name=prd2.Name,
                                          
                                              attribute_name=p.Name
                                          }
                    ).ToList();

                if (getproduct_catalog.Count > 0)
                {
                    // Checked_data_existornot(pagename);
                    foreach (var data in getproduct_catalog)
                    {
                        List<Luminous.MpartnerNewApi.Model.Technicalspecification> techspecification = new List<Luminous.MpartnerNewApi.Model.Technicalspecification>();
                        Luminous.MpartnerNewApi.Model.ProductCatalog pcatalog = new ProductCatalog();
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
                        if (productcategoryid == 30)
                        {
                            if (data.maxcurrentcharge == "" || data.maxcurrentcharge == null)
                            {
                                pcatalog.tech_specification = null;
                            }
                            else
                            {
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
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
                                Luminous.MpartnerNewApi.Model.Technicalspecification techdata = new Luminous.MpartnerNewApi.Model.Technicalspecification();
                                techdata.ColumnName = "Weight (Filled battery)";
                                techdata.Value = data.weightfilledbattery;
                                techspecification.Add(techdata);
                                pcatalog.tech_specification = techspecification;
                            }


                        }


                        // pcatalog.tech_specification = techspecification;

                        Pcataloglist.Add(pcatalog);

                    }

                    // }

                }
                else
                {
                    Checked_data_existornot_catalog_products(productcategoryid);
                }


            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return Pcataloglist;
        }

        //End Product Catalog Category Old//






        //Get Product Search//

        public List<Luminous.MpartnerNewApi.Model.Product_Search> getProduct_Search(string search_key)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<Luminous.MpartnerNewApi.Model.Product_Search> Pcataloglist = new List<Luminous.MpartnerNewApi.Model.Product_Search>();
            MessageData msgdata = new MessageData();


            try
            {


                var getproduct_search = (from prdcat in luminous.ProductLevelThrees
                                         where prdcat.PlTwStatus == 1 && prdcat.Name.Contains(search_key)
                                         select new
                                         {
                                             Id = prdcat.id,
                                             name = prdcat.Name

                                         }
                    ).ToList();


                // Checked_data_existornot(pagename);
                foreach (var data in getproduct_search)
                {

                    Luminous.MpartnerNewApi.Model.Product_Search pcatalog = new Product_Search();
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


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return Pcataloglist;
        }

        //End Product Search//

        //Get ProductData by search//

        public List<Luminous.MpartnerNewApi.Model.Product_Searchdata> getProduct_SearchData(string productname)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<Luminous.MpartnerNewApi.Model.Product_Searchdata> Pcataloglist = new List<Luminous.MpartnerNewApi.Model.Product_Searchdata>();
            MessageData msgdata = new MessageData();


            try
            {


                var getproduct_search_data = (from prdcat in luminous.ProductLevelThrees

                                              join prd2 in luminous.ProductLevelTwoes
                                              on prdcat.pc_Lv2_oneId equals prd2.id
                                              join prodcategory in luminous.ProductCatergories
                                              on prdcat.productCategoryid equals prodcategory.id


                                              where prdcat.Name == productname && prdcat.PlTwStatus == 1 && prd2.PlTwStatus==1
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

                    Luminous.MpartnerNewApi.Model.Product_Searchdata pcatalog = new Product_Searchdata();



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

        //End Product Search//

        //Get Product Catalog Upper Menu//

        public List<Luminous.MpartnerNewApi.Model.Catalog_Menu_Upper> getProduct_Catalog_Upper(int productcategoryid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<Luminous.MpartnerNewApi.Model.Catalog_Menu_Upper> cat_menu_upper = new List<Luminous.MpartnerNewApi.Model.Catalog_Menu_Upper>();


            MessageData msgdata = new MessageData();
            //Get Card Data List

            try
            {


                var getproduct_catalog = luminous.getCatalog_Upper(productcategoryid).ToList();

                if (getproduct_catalog.Count > 0)
                {
                    // Checked_data_existornot(pagename);
                    foreach (var data in getproduct_catalog)
                    {

                        Luminous.MpartnerNewApi.Model.Catalog_Menu_Upper cat_upper = new Catalog_Menu_Upper();
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
                else
                {
                    Checked_data_existornot_catalog_products(productcategoryid);
                }


            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return cat_menu_upper;
        }

        //End Catalog Upper Menu//


        //Get contact us details//

        public List<Luminous.MpartnerNewApi.Model.ContactUs> getContactUs()
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<Luminous.MpartnerNewApi.Model.ContactUs> contactus_data = new List<Luminous.MpartnerNewApi.Model.ContactUs>();
            MessageData msgdata = new MessageData();
            //Get Card Data List

            try
            {

                var getContact = luminous.contactUsDetails.Where(c => c.Cstatus == 1).Select(c => new { c.id, c.Contact_Us_Type, c.CAddress, c.PhoneNumber, c.Email, c.Fax }).ToList();



                foreach (var data in getContact)
                {

                    Luminous.MpartnerNewApi.Model.ContactUs contact_us = new ContactUs();


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

        //End Product Category//

        #region save contact us suggestion
        public MessageData save_contactus_suggetion(string userid, string custname, string email, string message, byte[] image, string filename)
        {
            string Filename = "";
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            if (image != null)
            {
                Filename = Path.GetFileNameWithoutExtension(filename) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(filename);
                string str = Path.Combine(HttpContext.Current.Server.MapPath("~/SuggestionImage/"), Filename);
                BinaryWriter bw = new BinaryWriter(new FileStream(str, FileMode.Create, FileAccess.Write));
                bw.Write(image);
                bw.Close();
            }
            luminous.AddToSuggestions(new Suggestion
            {
                Suggestion1 = message,
                Email = email,
                createdBy = userid,
                CustName = custname,
                CreateDate = DateTime.Now,
                ImageName = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "SuggestionImage/" + Filename
            }


                            );

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



        //Get contact us details//

        public List<Luminous.MpartnerNewApi.Model.Gallery_Menu_Upper> getGalleryUpperData()
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<Luminous.MpartnerNewApi.Model.Gallery_Menu_Upper> gallery_data = new List<Luminous.MpartnerNewApi.Model.Gallery_Menu_Upper>();
            MessageData msgdata = new MessageData();
            //Get Card Data List

            try
            {
                var getGallery_Menu_Footer = luminous.FooterCategories.Where(c => c.Status == true && c.CatType == "Media").ToList();





                foreach (var data in getGallery_Menu_Footer)
                {

                    Luminous.MpartnerNewApi.Model.Gallery_Menu_Upper gallerymenu_upper = new Gallery_Menu_Upper();

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

        //End Product Category//

        //Get getGalleryMainData//

        public List<Luminous.MpartnerNewApi.Model.Gallery_Details> getGalleryMainData(int categoryid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<Luminous.MpartnerNewApi.Model.Gallery_Details> gallery_data = new List<Luminous.MpartnerNewApi.Model.Gallery_Details>();
            MessageData msgdata = new MessageData();
            //Get Card Data List

            try
            {
                var getGallery_data = luminous.MediaDatas.Where(c => c.Status == 1 && c.LabelId == categoryid).OrderByDescending(c=>c.CreatedOn).ToList();





                foreach (var data in getGallery_data)
                {

                    Luminous.MpartnerNewApi.Model.Gallery_Details obj_gallerydata = new Gallery_Details();

                    obj_gallerydata.id = data.Id;

                    if (data.VideoName == "")
                    {
                        obj_gallerydata.gallery_video_name = "";
                    }
                    else
                    {
                        obj_gallerydata.gallery_video_name = data.VideoName;
                    }
                    if (data.Url == "")
                    {
                        obj_gallerydata.gallery_video_url = "";
                    }
                    else
                    {
                        obj_gallerydata.gallery_video_url = data.Url;
                    }


                    gallery_data.Add(obj_gallerydata);


                }

                // }




            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return gallery_data;
        }

        //End getGalleryMainData//

        // Get Media Main Data//



        public List<Luminous.MpartnerNewApi.Model.Gallery_Details> getMediaMainData()
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<Luminous.MpartnerNewApi.Model.Gallery_Details> gallery_data = new List<Luminous.MpartnerNewApi.Model.Gallery_Details>();
            MessageData msgdata = new MessageData();
            //Get Card Data List

            try
            {
                var getGallery_data = luminous.MediaDatas.Where(c => c.Status == 1 && c.PageFlag == "Media").ToList();





                foreach (var data in getGallery_data)
                {

                    Luminous.MpartnerNewApi.Model.Gallery_Details obj_gallerydata = new Gallery_Details();

                    obj_gallerydata.id = data.Id;

                    if (data.VideoName == "")
                    {
                        obj_gallerydata.gallery_video_name = "";
                    }
                    else
                    {
                        obj_gallerydata.gallery_video_name = data.VideoName;
                    }
                    if (data.Url == "")
                    {
                        obj_gallerydata.gallery_video_url = "";
                    }
                    else
                    {
                        obj_gallerydata.gallery_video_url = data.Url;
                    }


                    gallery_data.Add(obj_gallerydata);


                }

                // }




            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return gallery_data;
        }



        //End Media Main Data//

        //Get get faq data//

        public List<Luminous.MpartnerNewApi.Model.Faq> getFaqData()
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<Luminous.MpartnerNewApi.Model.Faq> faqlist = new List<Luminous.MpartnerNewApi.Model.Faq>();
            MessageData msgdata = new MessageData();
            //Get Card Data List

            try
            {
                var currentdate = DateTime.Now.Date;
                var getFaq = (from c in luminous.FAQs
                              where c.StartDate <= currentdate && c.EndDate >= currentdate && c.Status == 1
                              select new
                              {
                                  id = c.Id,
                                  question = c.QuestionName,
                                  answer = c.Answer

                              }).ToList();





                foreach (var data in getFaq)
                {

                    Luminous.MpartnerNewApi.Model.Faq obj_faq = new Faq();

                    obj_faq.id = data.id;

                    if (data.question == "")
                    {
                        obj_faq.question = "";
                    }
                    else
                    {
                        obj_faq.question = data.question;
                    }
                    if (data.answer == "")
                    {
                        obj_faq.answer = "";
                    }
                    else
                    {
                        obj_faq.answer = data.answer;
                    }


                    faqlist.Add(obj_faq);


                }

                // }




            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return faqlist;
        }

        //End getGalleryMainData//

        //Get get customer permission//

        public List<Luminous.MpartnerNewApi.Model.UserPermission> getcustomer_permission(string userid, string language)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<Luminous.MpartnerNewApi.Model.UserPermission> permissionlist = new List<Luminous.MpartnerNewApi.Model.UserPermission>();
            MessageData msgdata = new MessageData();
            //Get Card Data List

            try
            {
                List<getCustomerPermission_New_Result> getPerm = luminous.getCustomerPermission_New(userid, language).ToList();





                foreach (var data in getPerm)
                {

                    Luminous.MpartnerNewApi.Model.UserPermission obj_permission = new UserPermission();

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

        //End getGalleryMainData//

        //Get  LSD_GetDistributorCount//

        public List<Luminous.MpartnerNewApi.Model.Lsd_CouponCountData> func_LSD_GetDistributorCount(string userid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            //Get Home Page List
            List<Luminous.MpartnerNewApi.Model.Lsd_CouponCountData> Lsd_CouponCountData_list = new List<Luminous.MpartnerNewApi.Model.Lsd_CouponCountData>();
            MessageData msgdata = new MessageData();
            //Get Card Data List

            try
            {
                var geteligiblecount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).ToList();





                foreach (var data in geteligiblecount)
                {

                    Luminous.MpartnerNewApi.Model.Lsd_CouponCountData obj_Lsd_CouponCountData = new Lsd_CouponCountData();



                    obj_Lsd_CouponCountData.EligibleCouponCount = data.EligibleCoupon;
                    if (data.DistActivatedCount == 0 || data.DistActivatedCount == null)
                    {
                        obj_Lsd_CouponCountData.ActivatedCouponCount = 0;
                    }
                    else
                    {
                        obj_Lsd_CouponCountData.ActivatedCouponCount = data.DistActivatedCount;
                    }
                    if (data.DistBalanceCount == 0 || data.DistBalanceCount == null)
                    {
                        obj_Lsd_CouponCountData.BalanceCouponCount = 0;
                    }
                    else
                    {
                        obj_Lsd_CouponCountData.BalanceCouponCount = data.DistBalanceCount;
                    }
                    if (data.DealerRedeemedCount == 0 || data.DealerRedeemedCount == null)
                    {
                        obj_Lsd_CouponCountData.OpenReimbursment = 0;
                    }
                    else
                    {
                        // var dealercouponsubmitted = luminous.LSD_Master.Where(c => c.RedemptionDealerCode != null && c.RedemptionDealerSecretCode != null && c.ClaimDistCode == null && c.ActivationDistCode==userid).Count();

                        // coupondata.CouponReimbursment = list.DealerRedeemedCount;
                        obj_Lsd_CouponCountData.OpenReimbursment = data.DealerRedeemedCount;
                        // coupondata.CouponReimbursment = dealercouponsubmitted;
                    }

                    obj_Lsd_CouponCountData.Gold_ActivatedCouponCount = data.Gold_ActivatedCouponCount;
                    obj_Lsd_CouponCountData.Gold_BalanceCouponCount = data.Gold_BalanceCouponCount;
                    obj_Lsd_CouponCountData.Gold_EligibleCouponCount = data.Gold_EligibleCouponCount;
                    if (data.DistClaimedCount == 0 || data.DistClaimedCount == null)
                    {
                        obj_Lsd_CouponCountData.CouponReimbursment = 0;

                    }
                    else
                    {
                        //var DistClimsubmitted = luminous.LSD_Master.Where(c => c.RedemptionDealerCode != null && c.RedemptionDealerSecretCode != null && c.ClaimDistCode != null && c.ActivationDistCode == userid).Count();
                        obj_Lsd_CouponCountData.CouponReimbursment = data.DistClaimedCount;

                    }

                    Lsd_CouponCountData_list.Add(obj_Lsd_CouponCountData);


                }

                // }




            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return Lsd_CouponCountData_list;
        }

        //End LSD_GetDistributorCount//

        #region Save_LSD_SaveQrCode
        public MessageData func_LSD_SaveQrCode(string userid, string qrcode)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            int activatecount = 0;
            int balancecount = 0;
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            try
            {
                var matchQrcode_exist = luminous.LSD_Master.Where(c => c.QrCode == qrcode).Count();
                if (matchQrcode_exist > 0)
                {
                    var coupontype = luminous.LSD_Master.Where(c => c.QrCode == qrcode).Select(c => c.CouponType).SingleOrDefault();
                    if (coupontype == "B")
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


                                            msgdata.Message = "QR code already Activated";
                                            msgdata.Status = "0";




                                        }
                                        else
                                        {

                                            var getdistName = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.Dis_Name).SingleOrDefault();

                                            luminous.UpdateActivationDistCode(userid, getdistName, qrcode, qrcode);
                                            balancecount = Convert.ToInt32(distactivatedcount.EligibleCoupon) - Convert.ToInt32(activatecount);

                                            luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set DistActivatedCount='" + activatecount + "',DistBalanceCount='" + balancecount + "' where DistCode='" + userid + "'");

                                            //Get Gold dist activated coupon count//
                                            var getactivated_couponcount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => c.DistActivatedCount).SingleOrDefault();


                                            var gold_eligible = Math.Floor(Convert.ToDecimal(getactivated_couponcount.Value) / 7);

                                            var gold_activatdcount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => c.Gold_ActivatedCouponCount).SingleOrDefault();

                                            var gold_balance = Convert.ToInt32(gold_eligible) - gold_activatdcount.Value;

                                            luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set Gold_EligibleCouponCount='" + gold_eligible + "',Gold_BalanceCouponCount='" + gold_balance + "' where DistCode='" + userid + "'");


                                            msgdata.Status = "200";
                                            msgdata.Message = "Coupon Activated Successfully.";

                                        }
                                    }
                                    else
                                    {

                                        msgdata.Status = "0";
                                        msgdata.Message = "QR Code not valid.";

                                    }

                                }
                                else
                                {

                                    msgdata.Status = "0";
                                    msgdata.Message = "Coupon Activation limit has been exceeded.";


                                }
                            }
                            else
                            {

                                msgdata.Status = "0";
                                msgdata.Message = "Issue with coupon.Kindly coordinate with Luminous team.";

                            }

                        }
                        else
                        {
                            msgdata.Status = "0";
                            msgdata.Message = "Issue with coupon.Kindly coordinate with Luminous team.";

                        }
                    }
                    else
                    {

                        //Gold Check //

                        var getGiftid = luminous.LSD_Master.Where(c => c.QrCode == qrcode).Select(c => c.GiftId).SingleOrDefault();
                        if (getGiftid != 0)
                        {
                            var checkcouponcount_exist = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).ToList();
                            if (checkcouponcount_exist.Count > 0)
                            {

                                var check_dealer_eligible_coupon_gold = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => c.Gold_BalanceCouponCount).SingleOrDefault();
                                if (check_dealer_eligible_coupon_gold.Value > 0)
                                {

                                    //var distactivatedcount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => new { c.DistActivatedCount, c.EligibleCoupon }).SingleOrDefault();

                                    //activatecount = Convert.ToInt32(distactivatedcount.DistActivatedCount.Value) + 1;

                                    //if (activatecount <= distactivatedcount.EligibleCoupon.Value)
                                    //{
                                    var matchQrcode = luminous.LSD_Master.Where(c => c.QrCode == qrcode).Count();
                                    if (matchQrcode > 0)
                                    {
                                        var checkqrcodeExist = luminous.LSD_Master.Where(c => c.ActivationDistCode == userid && c.ActivatedQrCode == qrcode).Count();

                                        if (checkqrcodeExist > 0)
                                        {


                                            msgdata.Message = "QR code already Activated";
                                            msgdata.Status = "0";




                                        }
                                        else
                                        {
                                            //                    var getGiftid = luminous.LSD_Master.Where(c => c.QrCode == qrcode).Select(c => c.GiftId).SingleOrDefault();
                                            //if (getGiftid != 0)
                                            //{
                                            //    var checkcouponcount_exist = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).ToList();
                                            //    if (checkcouponcount_exist.Count > 0)
                                            //    {
                                            var getdistName = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.Dis_Name).SingleOrDefault();
                                            //luminous.ExecuteStoreCommand("Update LSD_Master set ActivationDistCode='" + userid + "',ActivationDistName='" + getdistName + "',ActivatedQrCode='" + qrcode + "',ActivationDistOn='" + DateTime.Now + "' where QrCode='" + qrcode + "'");
                                            luminous.UpdateActivationDistCode(userid, getdistName, qrcode, qrcode);
                                            //balancecount = Convert.ToInt32(distactivatedcount.EligibleCoupon) - Convert.ToInt32(activatecount);

                                            //luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set DistActivatedCount='" + activatecount + "',DistBalanceCount='" + balancecount + "' where DistCode='" + userid + "'");

                                            //Get Gold dist activated coupon count//
                                            var getactivated_couponcount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => new { c.Gold_EligibleCouponCount, c.Gold_ActivatedCouponCount, c.Gold_BalanceCouponCount }).SingleOrDefault();



                                            int gold_activatecount = Convert.ToInt32(getactivated_couponcount.Gold_ActivatedCouponCount.Value) + 1;

                                            if (gold_activatecount <= getactivated_couponcount.Gold_EligibleCouponCount.Value)
                                            {
                                                var gold_balancecount = Convert.ToInt32(getactivated_couponcount.Gold_EligibleCouponCount.Value) -
                                                    gold_activatecount;
                                                luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set Gold_ActivatedCouponCount='" + gold_activatecount + "',Gold_BalanceCouponCount='" + gold_balancecount + "' where DistCode='" + userid + "'");


                                                msgdata.Status = "200";
                                                msgdata.Message = "Coupon Activated Successfully.";

                                            }
                                            else
                                            {
                                                msgdata.Status = "0";
                                                msgdata.Message = "Gold Coupon Activation limit has been exceeded.";

                                            }
                                        }

                                    }
                                    else
                                    {

                                        msgdata.Status = "0";
                                        msgdata.Message = "QR Code not valid.";

                                    }



                                }
                                else
                                {
                                    var matchQrcode = luminous.LSD_Master.Where(c => c.QrCode == qrcode).Count();
                                    if (matchQrcode > 0)
                                    {
                                        var checkqrcodeExist = luminous.LSD_Master.Where(c => c.ActivationDistCode == userid && c.ActivatedQrCode == qrcode).Count();

                                        if (checkqrcodeExist > 0)
                                        {


                                            msgdata.Message = "QR code already Activated";
                                            msgdata.Status = "0";




                                        }
                                        else
                                        {
                                            msgdata.Status = "0";
                                            msgdata.Message = "You have not scanned 7 blue coupons therefore not eligible for activating gold coupon.";
                                        }
                                    }


                                }

                            }
                            else
                            {
                                msgdata.Status = "0";
                                msgdata.Message = "Issue with coupon.Kindly coordinate with Luminous team.";

                            }
                        }
                        else
                        {
                            msgdata.Status = "0";
                            msgdata.Message = "You have not scanned 7 blue coupons therefore not eligible for activating gold coupon";
                        }

                    }
                }
                else
                {
                    msgdata.Status = "0";
                    msgdata.Message = "QR Code not valid.";
                }
            }
            catch (Exception exc)
            {
                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }


            return msgdata;
        }

        public MessageData func_LSD_SaveQrCodeOld(string userid, string qrcode)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            int activatecount = 0;
            int balancecount = 0;
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            try
            {
                var matchQrcode_exist = luminous.LSD_Master.Where(c => c.QrCode == qrcode).Count();
                if (matchQrcode_exist > 0)
                {
                    var coupontype = luminous.LSD_Master.Where(c => c.QrCode == qrcode).Select(c => c.CouponType).SingleOrDefault();
                    if (coupontype == "B")
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


                                            msgdata.Message = "QR code already Activated";
                                            msgdata.Status = "0";




                                        }
                                        else
                                        {

                                            var getdistName = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.Dis_Name).SingleOrDefault();

                                            luminous.UpdateActivationDistCode(userid, getdistName, qrcode, qrcode);
                                            balancecount = Convert.ToInt32(distactivatedcount.EligibleCoupon) - Convert.ToInt32(activatecount);

                                            luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set DistActivatedCount='" + activatecount + "',DistBalanceCount='" + balancecount + "' where DistCode='" + userid + "'");

                                            //Get Gold dist activated coupon count//
                                            var getactivated_couponcount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => c.DistActivatedCount).SingleOrDefault();


                                            var gold_eligible = Math.Floor(Convert.ToDecimal(getactivated_couponcount.Value) / 7);

                                            var gold_activatdcount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => c.Gold_ActivatedCouponCount).SingleOrDefault();

                                            var gold_balance = Convert.ToInt32(gold_eligible) - gold_activatdcount.Value;

                                            luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set Gold_EligibleCouponCount='" + gold_eligible + "',Gold_BalanceCouponCount='" + gold_balance + "' where DistCode='" + userid + "'");


                                            msgdata.Status = "200";
                                            msgdata.Message = "Coupon Activated Successfully.";

                                        }
                                    }
                                    else
                                    {

                                        msgdata.Status = "0";
                                        msgdata.Message = "QR Code not valid.";

                                    }

                                }
                                else
                                {

                                    msgdata.Status = "0";
                                    msgdata.Message = "Coupon Activation limit has been exceeded.";


                                }
                            }
                            else
                            {
                                msgdata.Status = "0";
                                msgdata.Message = "Issue with coupon.Kindly coordinate with Luminous team.";

                            }

                        }
                        else
                        {
                            msgdata.Status = "0";
                            msgdata.Message = "Issue with coupon.Kindly coordinate with Luminous team.";

                        }
                    }
                    else
                    {

                        //Gold Check //

                        var check_dealer_eligible_coupon_gold = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => c.Gold_BalanceCouponCount).SingleOrDefault();
                        if (check_dealer_eligible_coupon_gold.Value != 0)
                        {
                            var getGiftid = luminous.LSD_Master.Where(c => c.QrCode == qrcode).Select(c => c.GiftId).SingleOrDefault();
                            if (getGiftid != 0)
                            {
                                var checkcouponcount_exist = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).ToList();
                                if (checkcouponcount_exist.Count > 0)
                                {
                                    var distactivatedcount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => new { c.DistActivatedCount, c.EligibleCoupon }).SingleOrDefault();

                                    activatecount = Convert.ToInt32(distactivatedcount.DistActivatedCount.Value) + 1;

                                    if (activatecount <= distactivatedcount.EligibleCoupon.Value)
                                    {
                                        var matchQrcode = luminous.LSD_Master.Where(c => c.QrCode == qrcode).Count();
                                        if (matchQrcode > 0)
                                        {
                                            var checkqrcodeExist = luminous.LSD_Master.Where(c => c.ActivationDistCode == userid && c.ActivatedQrCode == qrcode).Count();

                                            if (checkqrcodeExist > 0)
                                            {


                                                msgdata.Message = "QR code already Activated";
                                                msgdata.Status = "0";




                                            }
                                            else
                                            {

                                                var getdistName = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.Dis_Name).SingleOrDefault();
                                                //luminous.ExecuteStoreCommand("Update LSD_Master set ActivationDistCode='" + userid + "',ActivationDistName='" + getdistName + "',ActivatedQrCode='" + qrcode + "',ActivationDistOn='" + DateTime.Now + "' where QrCode='" + qrcode + "'");
                                                luminous.UpdateActivationDistCode(userid, getdistName, qrcode, qrcode);
                                                balancecount = Convert.ToInt32(distactivatedcount.EligibleCoupon) - Convert.ToInt32(activatecount);

                                                luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set DistActivatedCount='" + activatecount + "',DistBalanceCount='" + balancecount + "' where DistCode='" + userid + "'");

                                                //Get Gold dist activated coupon count//
                                                var getactivated_couponcount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => new { c.Gold_EligibleCouponCount, c.Gold_ActivatedCouponCount, c.Gold_BalanceCouponCount }).SingleOrDefault();



                                                int gold_activatecount = Convert.ToInt32(getactivated_couponcount.Gold_ActivatedCouponCount.Value) + 1;
                                                var gold_balancecount = Convert.ToInt32(getactivated_couponcount.Gold_EligibleCouponCount.Value) -
                                                    gold_activatecount;
                                                luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set Gold_ActivatedCouponCount='" + gold_activatecount + "',Gold_BalanceCouponCount='" + gold_balancecount + "' where DistCode='" + userid + "'");


                                                msgdata.Status = "200";
                                                msgdata.Message = "Coupon Activated Successfully.";

                                            }
                                        }
                                        else
                                        {

                                            msgdata.Status = "0";
                                            msgdata.Message = "QR Code not valid.";

                                        }

                                    }
                                    else
                                    {

                                        msgdata.Status = "0";
                                        msgdata.Message = "Coupon Activation limit has been exceeded.";


                                    }

                                }
                                else
                                {
                                    msgdata.Status = "0";
                                    msgdata.Message = "Issue with coupon.Kindly coordinate with Luminous team.";

                                }

                            }
                            else
                            {
                                msgdata.Status = "0";
                                msgdata.Message = "Issue with coupon.Kindly coordinate with Luminous team.";

                            }
                        }
                        else
                        {
                            msgdata.Status = "0";
                            msgdata.Message = "You have not scanned 7 blue coupons therefore not eligible for activating gold coupon";
                        }

                    }
                }
                else
                {
                    msgdata.Status = "0";
                    msgdata.Message = "QR Code not valid.";
                }
            }
            catch (Exception exc)
            {
                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }


            return msgdata;
        }
        #endregion

        //Get  LSD_GetDistOpenReimbursmentReport//

        public List<Luminous.MpartnerNewApi.Model.Lsd_OpenReimbursmentData> func_LSD_GetDistOpenReimbursmentReport(string userid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();

            List<Luminous.MpartnerNewApi.Model.Lsd_OpenReimbursmentData> Lsd_OpenReimbursmentData_list = new List<Luminous.MpartnerNewApi.Model.Lsd_OpenReimbursmentData>();



            try
            {
                var getDealerQrCode_Time = (from c in luminous.LSD_Master
                                            join Pi in luminous.Lsd_GiftMaster on c.GiftId equals Pi.GiftId

                                            where c.ActivationDistCode == userid && c.RedemptionDealerCode != null && c.RedemptionDealerSecretCode != null && c.ClaimDistCode == null
                                            select new
                                            {
                                                RedemptionDealerCode = c.RedemptionDealerCode,
                                                RedemptionDealerName = c.RedemptionDealerName,
                                                SecretCode = c.SecretCode,
                                                GiftName = Pi.GiftName,
                                                DistributorActivatedDate = c.ActivationDistOn,
                                                RedemptionDealerOn = c.RedemptionDealerOn,
                                                ActivatedQrCode = c.ActivatedQrCode,
                                                Id = c.Id
                                            }).ToList();



                foreach (var data in getDealerQrCode_Time)
                {

                    Lsd_OpenReimbursmentData obj_Lsd_OpenReimbursmentData = new Lsd_OpenReimbursmentData();


                    if (data.RedemptionDealerOn == null)
                    {
                        obj_Lsd_OpenReimbursmentData.DealerredemptionDateTime = "0";
                    }
                    else
                    {
                        obj_Lsd_OpenReimbursmentData.DealerredemptionDateTime = data.RedemptionDealerOn.ToString();
                    }


                    if (data.RedemptionDealerName == "" || data.RedemptionDealerName == null)
                    {
                        obj_Lsd_OpenReimbursmentData.DealerName = "0";
                    }
                    else
                    {
                        obj_Lsd_OpenReimbursmentData.DealerName = data.RedemptionDealerName;
                    }


                    if (data.RedemptionDealerCode == "" || data.RedemptionDealerCode == null)
                    {
                        obj_Lsd_OpenReimbursmentData.DealerCode = "0";
                    }
                    else
                    {
                        obj_Lsd_OpenReimbursmentData.DealerCode = data.RedemptionDealerCode;
                    }


                    if (data.SecretCode == "" || data.SecretCode == null)
                    {
                        obj_Lsd_OpenReimbursmentData.AlphanumericCode = "0";
                    }
                    else
                    {
                        obj_Lsd_OpenReimbursmentData.AlphanumericCode = data.SecretCode;
                    }



                    if (data.GiftName == "" || data.GiftName == null)
                    {
                        obj_Lsd_OpenReimbursmentData.GiftName = "0";
                    }
                    else
                    {
                        obj_Lsd_OpenReimbursmentData.GiftName = data.GiftName;
                    }





                    obj_Lsd_OpenReimbursmentData.ActivationDateAndTime = data.DistributorActivatedDate.ToString();

                    Lsd_OpenReimbursmentData_list.Add(obj_Lsd_OpenReimbursmentData);

                }
            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return Lsd_OpenReimbursmentData_list;
        }

        //End LSD_GetDistributorCount//

        #region LSD_SaveClaimData
        public MessageData func_LSD_SaveClaimData(string userid)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();

            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            try
            {

                var checkredeemedata = luminous.LSD_Master.Where(c => c.RedemptionDealerCode != null && c.RedemptionDealerOn != null && c.ActivationDistOn != null && c.ActivationDistCode == userid).Count();

                if (checkredeemedata > 0)
                {


                    //  luminous.ExecuteStoreCommand("Update LSD_Master set ClaimDistCode='" + userid + "',ClaimDistOn='" + DateTime.Now + "' where ActivationDistOn='" + userid + "' and RedemptionDealerCode is not null ");
                    luminous.LSD_UpdateClaimDistCode(userid);


                    var DealerRedeemedCount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => c.DealerRedeemedCount).SingleOrDefault();
                    var DistClaimCount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == userid).Select(c => c.DistClaimedCount).SingleOrDefault();

                    var distclaimcount = DealerRedeemedCount + DistClaimCount;

                    luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set DealerRedeemedCount=0,DistClaimedCount='" + distclaimcount + "' where DistCode='" + userid + "'");


                    msgdata.Status = "200";
                    msgdata.Message = "Claim Updated Successfully.";
                }
                else
                {
                    msgdata.Status = "0";
                    msgdata.Message = "No data found.";

                }





            }
            catch (Exception exc)
            {
                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }


            return msgdata;
        }
        #endregion


        //Get  LSD_GetRedeemedReport//

        public List<Luminous.MpartnerNewApi.Model.Lsd_DealerRedeemedData> func_LSD_GetRedeemedReport(string userid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();

            List<Luminous.MpartnerNewApi.Model.Lsd_DealerRedeemedData> Lsd_redeemedData_list = new List<Luminous.MpartnerNewApi.Model.Lsd_DealerRedeemedData>();



            try
            {
                var getDealerReedemedData = (from c in luminous.LSD_Master
                                             join Pi in luminous.Lsd_GiftMaster on c.GiftId equals Pi.GiftId

                                             where c.ActivationDistCode == userid && c.RedemptionDealerCode != null && c.ClaimDistCode != null
                                             select new
                                             {
                                                 DealerCode = c.RedemptionDealerCode,
                                                 DealerName = c.RedemptionDealerName,
                                                 SecretCode = c.SecretCode,
                                                 GiftName = Pi.GiftName,
                                                 DistributorActivatedDate = c.ActivationDistOn,
                                                 DealerredeptionDatetime = c.RedemptionDealerOn,
                                                 ClaimSubmission = c.ClaimDistOn
                                             }).ToList();



                foreach (var data in getDealerReedemedData)
                {

                    Lsd_DealerRedeemedData obj_LSD_GetRedeemedReport = new Lsd_DealerRedeemedData();


                    if (data.DealerCode == "" || data.DealerCode == null)
                    {
                        obj_LSD_GetRedeemedReport.DealerCode = "0";
                    }
                    else
                    {
                        obj_LSD_GetRedeemedReport.DealerCode = data.DealerCode;
                    }
                    if (data.DealerName == "" || data.DealerName == null)
                    {
                        obj_LSD_GetRedeemedReport.DealerName = "0";
                    }
                    else
                    {
                        obj_LSD_GetRedeemedReport.DealerName = data.DealerName;
                    }

                    if (data.SecretCode == "" || data.SecretCode == null)
                    {
                        obj_LSD_GetRedeemedReport.AlphanumericCode = "0";
                    }
                    else
                    {
                        obj_LSD_GetRedeemedReport.AlphanumericCode = data.SecretCode;
                    }
                    if (data.GiftName == "" || data.GiftName == null)
                    {
                        obj_LSD_GetRedeemedReport.GiftName = "0";
                    }
                    else
                    {
                        obj_LSD_GetRedeemedReport.GiftName = data.GiftName;
                    }
                    if (data.DistributorActivatedDate == null)
                    {
                        obj_LSD_GetRedeemedReport.DistActivatedDateTime = "0";
                    }
                    else
                    {
                        obj_LSD_GetRedeemedReport.DistActivatedDateTime = data.DistributorActivatedDate.ToString();
                    }
                    if (data.DealerredeptionDatetime == null)
                    {
                        obj_LSD_GetRedeemedReport.DealerredemptionDateTime = "0";
                    }
                    else
                    {
                        obj_LSD_GetRedeemedReport.DealerredemptionDateTime = data.DealerredeptionDatetime.ToString();
                    }
                    if (data.ClaimSubmission == null)
                    {
                        obj_LSD_GetRedeemedReport.ClaimSubmissionDate = "0";
                    }
                    else
                    {
                        obj_LSD_GetRedeemedReport.ClaimSubmissionDate = data.ClaimSubmission.ToString();
                    }
                    Lsd_redeemedData_list.Add(obj_LSD_GetRedeemedReport);

                }
            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return Lsd_redeemedData_list;
        }

        //End LSD_GetDistributorCount//


        //Get  LSD_GetActivatedCouponReport//

        public List<Luminous.MpartnerNewApi.Model.Lsd_DistActivatedCoupon> func_LSD_GetActivatedCouponReport(string userid,string coupontype)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();

            List<Luminous.MpartnerNewApi.Model.Lsd_DistActivatedCoupon> Lsd_distactivated_coupon_list = new List<Luminous.MpartnerNewApi.Model.Lsd_DistActivatedCoupon>();



            try
            {
                var getDistQrCode_Time = luminous.LSD_Master.Where(c => c.ActivationDistCode == userid && c.CouponType==coupontype).ToList();



                foreach (var data in getDistQrCode_Time)
                {

                    Lsd_DistActivatedCoupon obj_Lsd_DistActivatedCoupon = new Lsd_DistActivatedCoupon();


                    if (data.SecretCode == "0" || data.SecretCode == null)
                    {
                        obj_Lsd_DistActivatedCoupon.AlphanumericCode = "0";
                    }
                    else
                    {
                        obj_Lsd_DistActivatedCoupon.AlphanumericCode = data.SecretCode;
                    }
                    if (data.ActivationDistOn == null)
                    {
                        obj_Lsd_DistActivatedCoupon.ActivatedDateTime = null;
                    }
                    else
                    {
                        obj_Lsd_DistActivatedCoupon.ActivatedDateTime = data.ActivationDistOn.ToString();
                    }
                    Lsd_distactivated_coupon_list.Add(obj_Lsd_DistActivatedCoupon);

                }
            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return Lsd_distactivated_coupon_list;
        }

        //End LSD_GetDistributorCount//

        //Get  LSD_SaveDealerScanCode//
        public List<Luminous.MpartnerNewApi.Model.Lsd_DealerGiftData> func_LSD_SaveDealerScanCode(string userid, string barcode, string secretcode)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            int dealersubmitcount = 0;
            List<Luminous.MpartnerNewApi.Model.Lsd_DealerGiftData> LSD_SaveDealerScanCode_list = new List<Luminous.MpartnerNewApi.Model.Lsd_DealerGiftData>();



            try
            {

                var coupontype = luminous.LSD_Master.Where(c => c.Barcode == barcode && c.SecretCode == secretcode).Select(c => c.CouponType).SingleOrDefault();
                if (coupontype == "B")
                {
                    var getdealerName = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.Dis_Name).SingleOrDefault();
                    var getdistcode = luminous.LSD_Master.Where(c => c.Barcode == barcode && c.SecretCode == secretcode).Select(c => c.ActivationDistCode).SingleOrDefault();
                    string distributorcode = getdistcode;

                    luminous.UpdateRedemptionDealerCode(userid, getdealerName, barcode, secretcode, getdistcode);
                    var dealerreedemedcount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == distributorcode).Select(c => c.DealerRedeemedCount).SingleOrDefault();

                    dealersubmitcount = dealerreedemedcount.Value + 1;

                    luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set DealerRedeemedCount='" + dealersubmitcount + "' where DistCode ='" + getdistcode + "'");



                    //Dealer Activate Gold//
                    var dealerexist = luminous.LSD_DealerCouponCount.Where(c => c.CreatedBy == userid).Count();
                    if (dealerexist > 0)
                    {
                        var dealerredeemed_count = luminous.LSD_DealerCouponCount.Where(c => c.CreatedBy == userid).Select(c => c.Dealer_ReedemedCount).SingleOrDefault();
                        int dealer_redeemed = dealerredeemed_count.Value + 1;

                        //Gold_RedeemedCoupon
                        var gold_dealerredeemed_coupon = luminous.LSD_DealerCouponCount.Where(c => c.CreatedBy == userid).Select(c => c.Gold_DealerRedeemedCoupon).SingleOrDefault();

                        luminous.ExecuteStoreCommand("Update Lsd_DealerCouponCount set Dealer_ReedemedCount='" + dealer_redeemed + "' where CreatedBy='" + userid + "'");

                        var gold_eligible = Math.Floor(Convert.ToDecimal(dealer_redeemed - gold_dealerredeemed_coupon) / 7);

                        var gold_activatdcount = luminous.LSD_DealerCouponCount.Where(c => c.CreatedBy == userid).Select(c => c.Gold_DealerRedeemedCoupon).SingleOrDefault();

                        var gold_balance = Convert.ToInt32(gold_eligible) - gold_activatdcount.Value;

                        luminous.ExecuteStoreCommand("Update Lsd_DealerCouponCount set Glod_DealerEligibleCoupon='" + gold_eligible + "',Gold_DealerBalanceCount='" + gold_balance + "' where CreatedBy='" + userid + "'");
                    }
                    else
                    {
                        luminous.AddToLSD_DealerCouponCount(new LSD_DealerCouponCount
                        {
                            CreatedBy = userid,
                            CreatedOn = DateTime.Now,
                            Glod_DealerEligibleCoupon = 0,
                            Dealer_ReedemedCount = 1,
                            Gold_DealerRedeemedCoupon = 0,
                            Gold_DealerBalanceCount = 0,
                            Gold_DealerRedeemedCount = 0,
                            Status = 1,

                        });
                        luminous.SaveChanges();
                    }
                    //End//



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
                        Lsd_DealerGiftData gdata = new Lsd_DealerGiftData();
                        gdata.GiftId = gift.GiftId;
                        gdata.GiftName = gift.GiftName;
                        gdata.GiftDescription = gift.Giftdescription;
                        gdata.GiftImage = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "LSDImage/" + gift.GiftImage;


                        LSD_SaveDealerScanCode_list.Add(gdata);
                    }



                }
                else
                {

                    var getdealerName = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.Dis_Name).SingleOrDefault();
                    var getdistcode = luminous.LSD_Master.Where(c => c.Barcode == barcode && c.SecretCode == secretcode).Select(c => c.ActivationDistCode).SingleOrDefault();
                    string distributorcode = getdistcode;
                    ////  luminous.ExecuteStoreCommand("Update LSD_Master set RedemptionDealerCode='" + userid + "',RedemptionDealerName='" + getdealerName + "',RedemtionDealerBarCode='" + barcode + "',RedemptionDealerSecretCode='" + secretcode + "',RedemptionDealerOn='" + DateTime.Now + "' where ActivationDistCode='" + getdistcode + "'");

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
                        Lsd_DealerGiftData gdata = new Lsd_DealerGiftData();
                        gdata.GiftId = gift.GiftId;
                        gdata.GiftName = gift.GiftName;
                        gdata.GiftDescription = gift.Giftdescription;
                        gdata.GiftImage = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "LSDImage/" + gift.GiftImage;


                        LSD_SaveDealerScanCode_list.Add(gdata);
                    }

                    //Get Gold dist activated coupon count//
                    var getactivated_couponcount = luminous.LSD_DealerCouponCount.Where(c => c.CreatedBy == userid).Select(c => new { c.Glod_DealerEligibleCoupon, c.Gold_DealerRedeemedCoupon, c.Gold_DealerBalanceCount, c.Dealer_ReedemedCount }).SingleOrDefault();

                    int dealer_redeemedcount = getactivated_couponcount.Dealer_ReedemedCount.Value + 1;
                    int eligiblecoupon = getactivated_couponcount.Glod_DealerEligibleCoupon.Value;
                    int gold_activatecount = Convert.ToInt32(getactivated_couponcount.Gold_DealerRedeemedCoupon.Value) + 1;
                    var gold_balancecount = Convert.ToInt32(getactivated_couponcount.Glod_DealerEligibleCoupon.Value) -
                        gold_activatecount;
                    //luminous.ExecuteStoreCommand("Update LSD_DealerCouponCount set Glod_DealerEligibleCoupon='" + gold_activatecount + "',Gold_DealerBalanceCount='" + gold_balancecount + "',Gold_DealerRedeemedCoupon='" + gold_activatecount + "' where CreatedBy='" + userid + "'");

                    luminous.ExecuteStoreCommand("Update LSD_DealerCouponCount set Glod_DealerEligibleCoupon='" + eligiblecoupon + "',Gold_DealerBalanceCount='" + gold_balancecount + "',Gold_DealerRedeemedCoupon='" + gold_activatecount + "',Dealer_ReedemedCount='" + dealer_redeemedcount + "' where CreatedBy='" + userid + "'");
                }
            }

            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return LSD_SaveDealerScanCode_list;
        }



        public List<Luminous.MpartnerNewApi.Model.Lsd_DealerGiftData> func_LSD_SaveDealerScanCodeOld(string userid, string barcode, string secretcode)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();
            int dealersubmitcount = 0;
            List<Luminous.MpartnerNewApi.Model.Lsd_DealerGiftData> LSD_SaveDealerScanCode_list = new List<Luminous.MpartnerNewApi.Model.Lsd_DealerGiftData>();



            try
            {

                var coupontype = luminous.LSD_Master.Where(c => c.Barcode == barcode && c.SecretCode == secretcode).Select(c => c.CouponType).SingleOrDefault();
                if (coupontype == "B")
                {
                    var getdealerName = luminous.UsersLists.Where(c => c.UserId == userid).Select(c => c.Dis_Name).SingleOrDefault();
                    var getdistcode = luminous.LSD_Master.Where(c => c.Barcode == barcode && c.SecretCode == secretcode).Select(c => c.ActivationDistCode).SingleOrDefault();
                    string distributorcode = getdistcode;

                    luminous.UpdateRedemptionDealerCode(userid, getdealerName, barcode, secretcode, getdistcode);
                    var dealerreedemedcount = luminous.Lsd_DistCouponCount.Where(c => c.DistCode == distributorcode).Select(c => c.DealerRedeemedCount).SingleOrDefault();

                    dealersubmitcount = dealerreedemedcount.Value + 1;

                    luminous.ExecuteStoreCommand("Update Lsd_DistCouponCount set DealerRedeemedCount='" + dealersubmitcount + "' where DistCode ='" + getdistcode + "'");

                    //Dealer Activate Gold//
                    var dealerexist = luminous.LSD_DealerCouponCount.Where(c => c.CreatedBy == userid).Count();
                    if (dealerexist > 0)
                    {
                        var dealerredeemed_count = luminous.LSD_DealerCouponCount.Where(c => c.CreatedBy == userid).Select(c => c.Dealer_ReedemedCount).SingleOrDefault();
                        int dealer_redeemed = dealerredeemed_count.Value + 1;

                        var gold_eligible = Math.Floor(Convert.ToDecimal(dealer_redeemed) / 7);

                        var gold_activatdcount = luminous.LSD_DealerCouponCount.Where(c => c.CreatedBy == userid).Select(c => c.Gold_DealerRedeemedCoupon).SingleOrDefault();

                        var gold_balance = Convert.ToInt32(gold_eligible) - gold_activatdcount.Value;

                        luminous.ExecuteStoreCommand("Update Lsd_DealerCouponCount set Glod_DealerEligibleCoupon='" + gold_eligible + "',Gold_DealerBalanceCount='" + gold_balance + "' where CreatedBy='" + userid + "'");
                    }
                    else
                    {
                        luminous.AddToLSD_DealerCouponCount(new LSD_DealerCouponCount
                        {
                            CreatedBy = userid,
                            CreatedOn = DateTime.Now,
                            Glod_DealerEligibleCoupon = 0,
                            Dealer_ReedemedCount = 1,
                            Gold_DealerRedeemedCoupon = 0,
                            Gold_DealerBalanceCount = 0,
                            Gold_DealerRedeemedCount = 0,
                            Status = 1,

                        });
                        luminous.SaveChanges();
                    }
                    //End//



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
                        Lsd_DealerGiftData gdata = new Lsd_DealerGiftData();
                        gdata.GiftId = gift.GiftId;
                        gdata.GiftName = gift.GiftName;
                        gdata.GiftDescription = gift.Giftdescription;
                        gdata.GiftImage = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "LSDImage/" + gift.GiftImage;


                        LSD_SaveDealerScanCode_list.Add(gdata);
                    }



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
                        Lsd_DealerGiftData gdata = new Lsd_DealerGiftData();
                        gdata.GiftId = gift.GiftId;
                        gdata.GiftName = gift.GiftName;
                        gdata.GiftDescription = gift.Giftdescription;
                        gdata.GiftImage = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "LSDImage/" + gift.GiftImage;


                        LSD_SaveDealerScanCode_list.Add(gdata);
                    }

                    //Get Gold dist activated coupon count//
                    var getactivated_couponcount = luminous.LSD_DealerCouponCount.Where(c => c.CreatedBy == userid).Select(c => new { c.Glod_DealerEligibleCoupon, c.Gold_DealerRedeemedCoupon, c.Gold_DealerBalanceCount }).SingleOrDefault();



                    int gold_activatecount = Convert.ToInt32(getactivated_couponcount.Gold_DealerRedeemedCoupon.Value) + 1;
                    var gold_balancecount = Convert.ToInt32(getactivated_couponcount.Glod_DealerEligibleCoupon.Value) -
                        gold_activatecount;
                    luminous.ExecuteStoreCommand("Update LSD_DealerCouponCount set Glod_DealerEligibleCoupon='" + gold_activatecount + "',Gold_DealerBalanceCount='" + gold_balancecount + "' where CreatedBy='" + userid + "'");
                }
            }

            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return LSD_SaveDealerScanCode_list;
        }

        //End LSD_GetDistributorCount//



        // LSD_DealerReport//

        public List<Luminous.MpartnerNewApi.Model.Lsd_DealerReport> func_LSD_DealerReport(string userid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();

            List<Luminous.MpartnerNewApi.Model.Lsd_DealerReport> Lsd_dealer_report_list = new List<Luminous.MpartnerNewApi.Model.Lsd_DealerReport>();



            try
            {
                var getDealerReport = (from c in luminous.LSD_Master
                                       join Pi in luminous.Lsd_GiftMaster on c.GiftId equals Pi.GiftId
                                       where c.RedemptionDealerCode == userid
                                       select new
                                       {
                                           barcode = c.RedemtionDealerBarCode,
                                           secretcode = c.RedemptionDealerSecretCode,
                                           ReimbursmentDate_Time = c.RedemptionDealerOn,
                                           GiftName = Pi.GiftName,
                                           DistName = c.ActivationDistName
                                       }).ToList();



                foreach (var data in getDealerReport)
                {

                    Lsd_DealerReport obj_Lsd_DealerReport = new Lsd_DealerReport();



                    obj_Lsd_DealerReport.Barcode = data.barcode;
                    obj_Lsd_DealerReport.Gift = data.GiftName;
                    obj_Lsd_DealerReport.ActivatedDistName = data.DistName;
                    obj_Lsd_DealerReport.SecretCode = data.secretcode;
                    obj_Lsd_DealerReport.ReimbursmentDate_Time = data.ReimbursmentDate_Time.ToString();

                    Lsd_dealer_report_list.Add(obj_Lsd_DealerReport);



                }
            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return Lsd_dealer_report_list;
        }

        //End LSD_DealerReport//



        // func_SurveyNotificationList//

        public List<Luminous.MpartnerNewApi.Model.SurveyNotification> func_SurveyNotificationList(string userid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();

            List<Luminous.MpartnerNewApi.Model.SurveyNotification> survey_notification_list = new List<Luminous.MpartnerNewApi.Model.SurveyNotification>();



            try
            {
                var currentdate = DateTime.Now.Date;
                //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();

                var survey_data = from cl in luminous.NotificationSurveys
                                  where (
                                      from i in luminous.SaveNotificationSurveys
                                      where i.SurveyID == cl.SurveyID && i.UserId == userid
                                      select i).Count() == 0
                                  select new { SurveyID = cl.SurveyID, Survey = cl.Survey, StartDate = cl.StartDate, Enddate = cl.Enddate, cl.ContestId };

                var Surveylist = from cdata in survey_data
                                 where cdata.StartDate <= currentdate && cdata.Enddate >= currentdate && cdata.ContestId == 1
                                 select new
                                 {
                                     SurveyID = cdata.SurveyID,
                                     Survey = cdata.Survey
                                 };



                foreach (var data in Surveylist)
                {

                    SurveyNotification obj_survey_not = new SurveyNotification();



                    obj_survey_not.SurveyId = data.SurveyID;
                    obj_survey_not.Survey = data.Survey;


                    survey_notification_list.Add(obj_survey_not);



                }
            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return survey_notification_list;
        }

        //End func_SurveyNotificationList//

        // func_SurveyNotificationList//

        public List<Luminous.MpartnerNewApi.Model.SurveyNotification_Question> func_GetSurveyQuestion(int Surveyid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();

            List<Luminous.MpartnerNewApi.Model.SurveyNotification_Question> getSurveyListQues = new List<Luminous.MpartnerNewApi.Model.SurveyNotification_Question>();



            try
            {
                var currentdate = DateTime.Now.Date;
                //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();

                var Surveylist = luminous.NotificationSurveys.Where(c => c.SurveyID == Surveyid).Select(c => new { c.SurveyID, c.Survey, c.QuestionType, c.QuestionTitle, c.OptionA, c.OptionB, c.OptionC, c.OptionD, c.OptionE, c.CorrectAns }).ToList();



                foreach (var data in Surveylist)
                {

                    SurveyNotification_Question Nsurvey = new SurveyNotification_Question();
                    Nsurvey.SurveyId = data.SurveyID;
                    Nsurvey.Survey = data.Survey;
                    Nsurvey.QuestionTitle = data.QuestionTitle;
                    Nsurvey.QuestionType = data.QuestionType;

                    if (data.OptionA == null || data.OptionA == "")
                    {
                        Nsurvey.OptionA = "0";
                    }
                    else
                    {
                        Nsurvey.OptionA = data.OptionA;
                    }
                    if (data.OptionB == null || data.OptionB == "")
                    {
                        Nsurvey.OptionB = "0";
                    }
                    else
                    {
                        Nsurvey.OptionB = data.OptionB;
                    }
                    if (data.OptionC == null || data.OptionC == "")
                    {
                        Nsurvey.OptionC = "0";
                    }
                    else
                    {
                        Nsurvey.OptionC = data.OptionC;
                    }
                    if (data.OptionD == null || data.OptionD == "")
                    {
                        Nsurvey.OptionD = "0";
                    }
                    else
                    {
                        Nsurvey.OptionD = data.OptionD;
                    }
                    if (data.OptionE == null || data.OptionE == "")
                    {
                        Nsurvey.OptionE = "0";
                    }
                    else
                    {
                        Nsurvey.OptionE = data.OptionE;
                    }
                    if (data.CorrectAns == null || data.CorrectAns == "")
                    {
                        Nsurvey.CorrectAns = "0";
                    }
                    else
                    {
                        Nsurvey.CorrectAns = data.CorrectAns;
                    }


                    getSurveyListQues.Add(Nsurvey);



                }
            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return getSurveyListQues;
        }

        //End func_SurveyNotificationList//


        // func_GetParentCategory//

        public List<Luminous.MpartnerNewApi.Model.ParentCategory> func_GetParentCategory()
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();

            List<Luminous.MpartnerNewApi.Model.ParentCategory> pcategory = new List<Luminous.MpartnerNewApi.Model.ParentCategory>();



            try
            {
                var getparencategory = luminous.ParentCategories.Where(c => c.PCStatus == 1).Select(c => new { c.Pcid, c.PCName }).ToList();



                foreach (var data in getparencategory)
                {

                    MpartnerNewApi.Model.ParentCategory obj_Pcategory = new MpartnerNewApi.Model.ParentCategory();



                    obj_Pcategory.Id = data.Pcid;
                    obj_Pcategory.Parentcategoryname = data.PCName;


                    pcategory.Add(obj_Pcategory);



                }
            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return pcategory;
        }

        //End func_GetParentCategory//

        #region save notification survey
        public MessageData func_SaveSurveyResult(string userid, int SurveyId, string option, string Optionvalue)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();


            luminous.AddToSaveNotificationSurveys(new SaveNotificationSurvey
            {

                UserId = userid,
                DeviceId = "",
                SurveyID = SurveyId,
                Options = option,
                OptionValue = Optionvalue,
                ContestId = 1,
                CreatedOn = DateTime.Now,
                CreatedBy = userid.ToString()
            });


            int savestatus = luminous.SaveChanges();

            if (savestatus > 0)
            {
                msgdata.Status = "200";
                msgdata.Message = "Data  inserted successfully.";

            }
            else
            {
                msgdata.Message = "Data not inserted";
                msgdata.Status = "0";
            }



            return msgdata;
        }
        #endregion



        // func_GetAlertnotification//

        public List<Luminous.MpartnerNewApi.Model.AlertNotifications> func_GetAlertnotification(string userid, string deviceid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();

            List<Luminous.MpartnerNewApi.Model.AlertNotifications> getAlertNotification = new List<Luminous.MpartnerNewApi.Model.AlertNotifications>();



            try
            {

                //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();

                var AlertReadStatusCount = luminous.AlertNotificationReadStatus.Where(c => c.DeviceId == deviceid && c.UserId == userid).ToList();
                if (AlertReadStatusCount.Count == 0)
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
                            anotification.Imagepath = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "NotificationImage/" + data.Imagepath;
                            getAlertNotification.Add(anotification);
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
                            anotification.Imagepath = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "NotificationImage/" + data.Imagepath;
                            getAlertNotification.Add(anotification);


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
                            anotification.Imagepath = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "NotificationImage/" + data.Imagepath;
                            getAlertNotification.Add(anotification);
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
                            anotification.Imagepath = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "NotificationImage/" + data.Imagepath;
                            getAlertNotification.Add(anotification);


                        }

                    }

                }



            }

            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return getAlertNotification;
        }

        //End func_SurveyNotificationList//


        #region IsReadCheck_AlertNotification
        public MessageData func_IsReadCheck_AlertNotification(string userid, string deviceid, string notificationid, string Isread)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();


            AlertNotificationReadStatu alreadstatus = new AlertNotificationReadStatu();
            alreadstatus.NotificationId = Convert.ToInt32(notificationid);
            alreadstatus.IsRead = true;
            alreadstatus.UserId = userid;
            alreadstatus.DeviceId = deviceid;
            alreadstatus.NotificationId = Convert.ToInt32(notificationid);
            luminous.AlertNotificationReadStatus.AddObject(alreadstatus);



            int savestatus = luminous.SaveChanges();

            if (savestatus > 0)
            {
                msgdata.Status = "200";
                msgdata.Message = "Data  inserted successfully.";

            }
            else
            {
                msgdata.Message = "Data not inserted";
                msgdata.Status = "0";
            }



            return msgdata;
        }
        #endregion




        // func_EscalationMatrix//

        public List<Luminous.MpartnerNewApi.Model.EsacalationMatrix> func_EscalationMatrix(int stateid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();

            List<Luminous.MpartnerNewApi.Model.EsacalationMatrix> escmatrix = new List<Luminous.MpartnerNewApi.Model.EsacalationMatrix>();



            try
            {

                //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();

                var escalationmatrixlist = luminous.EscalationMatrices.Where(c => c.StateId == stateid && c.status == 1).Select(c => new { c.ServiceCenterName, c.Address, c.Phoneno }).ToList();



                foreach (var data in escalationmatrixlist)
                {

                    EsacalationMatrix ematrix = new EsacalationMatrix();


                    ematrix.Phoneno = data.Phoneno;


                    if (data.ServiceCenterName == null || data.ServiceCenterName == "")
                    {
                        ematrix.ServiceCenterName = "0";
                    }
                    else
                    {
                        ematrix.ServiceCenterName = data.ServiceCenterName;
                    }
                    if (data.Address == null || data.Address == "")
                    {
                        ematrix.Address = "0";
                    }
                    else
                    {
                        ematrix.Address = data.Address;
                    }
                    if (data.Phoneno == null || data.Phoneno == "")
                    {
                        ematrix.Phoneno = "0";
                    }
                    else
                    {
                        ematrix.Phoneno = data.Phoneno;
                    }



                    escmatrix.Add(ematrix);



                }
            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return escmatrix;
        }

        //End func_EscalationMatrix//

        // func_Userverification//

        public MessageData func_Userverification(string userid, string deviceid, string appversion, string osversion, string ostype,string fcm_token)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            int savestatus = 0;
            string LoginToken = "";
            string TokenString = userid + appversion + deviceid;

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
                    Otp = "",
                    AppVersion = appversion,
                    OSVersion = osversion,
                    OSType = ostype,
                    Status = 2,
                    Token = LoginToken,
                    Fcm_token = fcm_token,
                    TokenFlag = 1,
                    CreatedOn = DateTime.Now,
                    CreatedBy = userid.ToString()
                });
                savestatus = luminous.SaveChanges();
                if (savestatus > 0)
                {
                    msgdata.Status = "200";
                    msgdata.Message = "Data fetched successfully";
                    msgdata.Token = LoginToken;
                }
                else
                {
                    msgdata.Message = "Data not inserted";
                    msgdata.Status = "0";
                    msgdata.Token = "0";
                }






            }
            else
            {

                if (Usercount == 1)
                {
                    var uvdata = luminous.UserVerifications.Single(c => c.UserId == userid && c.DeviceId == deviceid);

                    Auditlog_Userverificationstatus audit_user = new Auditlog_Userverificationstatus();
                    audit_user.Userverification_Id = uvdata.Id;
                    audit_user.AppVersion = uvdata.AppVersion;
                    audit_user.UserId = uvdata.UserId;
                    audit_user.OSVersion = uvdata.OSVersion;
                    audit_user.Fcm_token = fcm_token;
                    audit_user.OSType = uvdata.OSType;
                    audit_user.DeviceId = uvdata.DeviceId;
                    audit_user.CreatedOn = DateTime.Now;
                    audit_user.CreatedBy = userid;
                    audit_user.UnBolckedBy = uvdata.UnBolckedBy;
                    audit_user.Status = uvdata.Status;
                    luminous.Auditlog_Userverificationstatus.AddObject(audit_user);
                    luminous.SaveChanges();

                    var data = luminous.update_Userverification(userid, osversion, ostype, appversion, deviceid,fcm_token);



                    if (data > 0)
                    {


                        msgdata.Status = "200";
                        msgdata.Message = "Data fetched successfully";
                        msgdata.Token = LoginToken;
                    }
                    else
                    {
                        msgdata.Message = "Data not inserted";
                        msgdata.Status = "0";
                        msgdata.Token = "0";
                    }

                }
                else
                {
                    //luminous.AddToUserVerifications(new UserVerification
                    //{

                    //    UserId = userid,
                    //    DeviceId = deviceid,
                    //    Otp = "",
                    //    OSVersion = osversion,
                    //    OSType = ostype,
                    //    UnauthorizedUser = "logged in three devices",
                    //    Status = 0,
                    //    CreatedOn = DateTime.Now,
                    //    CreatedBy = userid.ToString()
                    //});
                    //luminous.SaveChanges();


                    msgdata.Status = "0";
                    msgdata.Message = "User already logged in three devices";
                    msgdata.Token = "0";
                }
            }



            return msgdata;
        }

        //End func_Userverification//

        // func_user_Language//

        public List<Luminous.MpartnerNewApi.Model.Language> func_user_Language()
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();

            List<Luminous.MpartnerNewApi.Model.Language> language = new List<Luminous.MpartnerNewApi.Model.Language>();



            try
            {

                //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();

                var langlist = luminous.LanguageMasters.Where(c => c.Status == 1).Select(c => new { c.Id, c.Lang, c.LangCode }).ToList();



                foreach (var data in langlist)
                {

                    Language lng = new Language();


                    lng.id = data.Id;


                    if (data.Lang == null || data.Lang == "")
                    {
                        lng.language = "0";
                    }
                    else
                    {
                        lng.language = data.Lang;
                    }
                    if (data.LangCode == null || data.LangCode == "")
                    {
                        lng.languagecode = "0";
                    }
                    else
                    {
                        lng.languagecode = data.LangCode;
                    }




                    language.Add(lng);



                }
            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return language;
        }

        //End func_user_Language//

        // func_Savelanguage//

        public MessageData func_Savelanguage(string userid, string deviceid, string token,string languagecode)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();


            int savestatus = luminous.ExecuteStoreCommand("Update userverification set LanguageCode='" + languagecode + "' where userid='" + userid + "' and Deviceid='" + deviceid + "' and token='" + token + "'");




            if (savestatus > 0)
            {
                msgdata.Status = "200";
                msgdata.Message = "Data  inserted successfully.";

            }
            else
            {
                msgdata.Message = "Data not inserted";
                msgdata.Status = "0";
            }



            return msgdata;
        }


        //End func_Savelanguage//


        // func_SaveTicket//

        public MessageData func_SaveTicket(string user_id, string attachmentname, byte[] attachment, string serialno, string description, string status, string connectplus_message)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            string Filename = "";

            if (attachmentname.ToString() != null && attachmentname.ToString() != "")
            {
                Filename = Path.GetFileNameWithoutExtension(attachmentname) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(attachmentname);
                string str = Path.Combine(HttpContext.Current.Server.MapPath("~/ConnectAssist/"), Filename);
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

            }
            var getdata_serialno = luminous.ConnectAssists.SingleOrDefault(c => c.Srno == serialno);


            getdata_serialno.Userid = user_id;
            getdata_serialno.Srno = serialno;
            getdata_serialno.Flag = status;
            getdata_serialno.Message = connectplus_message;
            getdata_serialno.CreatedOn = DateTime.Now;
            getdata_serialno.CreatedBy = user_id.ToString();

            luminous.SaveChanges();
            int connectassist_id = luminous.ConnectAssists.Select(c => c.Id).Max();
            // int id = (from record in luminous.ConnectAssists orderby record.Id select record.Id).Last();
            luminous.AddToMapConnectAssist_Comments(new MapConnectAssist_Comments
            {
                ConnectAssistId = connectassist_id,
                Description = description,
                CreatedOn = DateTime.Now,
                Attachment = Filename,
                filename = attachmentname,
                CreatedBy = user_id.ToString()


            }


                );

            int savestatus = luminous.SaveChanges();





            if (savestatus > 0)
            {
                msgdata.Status = "200";
                msgdata.Message = "Data  inserted successfully.";

            }
            else
            {
                msgdata.Message = "Data not inserted";
                msgdata.Status = "0";
            }



            return msgdata;
        }


        //End func_SaveTicket//


        // func_UpdateTicket//

        public MessageData func_UpdateTicket(string user_id, int ticketid, string attachmentname, byte[] attachment, string serialno, string description, string status)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            string Filename = "";

            if (attachmentname.ToString() != null && attachmentname.ToString() != "")
            {
                Filename = Path.GetFileNameWithoutExtension(attachmentname) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(attachmentname);
                string str = Path.Combine(HttpContext.Current.Server.MapPath("~/ConnectAssist/"), Filename);
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
            luminous.SaveChanges();
            //  int connectassist_id = luminous.ConnectAssists.Select(c => c.Id).Max();

            luminous.AddToMapConnectAssist_Comments(new MapConnectAssist_Comments
            {
                ConnectAssistId = ticketid,
                Description = description,
                CreatedOn = DateTime.Now,
                Attachment = Filename,
                filename = attachmentname,
                CreatedBy = user_id.ToString()


            }


                );

            int savestatus = luminous.SaveChanges();




            if (savestatus > 0)
            {
                msgdata.Status = "200";
                msgdata.Message = "Data  updated successfully.";

            }
            else
            {
                msgdata.Message = "Data not updated";
                msgdata.Status = "0";
            }



            return msgdata;
        }


        //End func_UpdateTicket//

        // func_Saveconnectplus//

        public MessageData func_SaveConnectPlus(string user_id, string serialno, string dlrCode, string distcode, string saledate, string customername, string customerphone, string customerlandLinenumber, string customerstate, string customercity, string customeraddress, string ismtype, string imagename, byte[] connectplusimage)
        {
            LuminousEntities luminous = new LuminousEntities();
            MessageData msgdata = new MessageData();
            string Filename = "";

            if (ismtype == "yes")
            {
                Filename = Path.GetFileNameWithoutExtension(imagename) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(imagename);
                string str = Path.Combine(HttpContext.Current.Server.MapPath("~/MpartnerNewApi/connectplusimage/"), Filename);
                BinaryWriter bw = new BinaryWriter(new FileStream(str, FileMode.Create, FileAccess.Write));
                bw.Write(connectplusimage);
                bw.Close();
            }
            else
            {
                imagename = "";
                Filename = "";
            }

            var check_serno_exist = luminous.ConnectAssists.Where(c => c.Srno == serialno).Count();
            if (check_serno_exist == 0)
            {
                luminous.AddToConnectAssists(new ConnectAssist
                {

                    Userid = user_id,
                    DlrCode = dlrCode,
                    Srno = serialno,
                    DistCode = distcode,

                    CustName = customername,
                    CustMobile = customerphone,
                    CustLandLine = customerlandLinenumber,
                    Adderss = customeraddress,
                    State = customerstate,
                    City = customercity,
                    DateOfSale = Convert.ToDateTime(saledate),
                    CreatedOn = DateTime.Now,
                    CreatedBy = user_id.ToString(),
                    ConnectplusImage = Filename
                });

                luminous.SaveChanges();
            }
            else
            {



                var getassistdata_by_id = luminous.ConnectAssists.SingleOrDefault(c => c.Srno == serialno);


                ConnectAssist_log obj_connectassistlog = new ConnectAssist_log();
                obj_connectassistlog.ParentId = getassistdata_by_id.Id;
                obj_connectassistlog.Userid = getassistdata_by_id.Userid;
                obj_connectassistlog.DlrCode = getassistdata_by_id.DlrCode;
                obj_connectassistlog.Srno = getassistdata_by_id.Srno;
                obj_connectassistlog.DistCode = getassistdata_by_id.DistCode;

                obj_connectassistlog.CustName = getassistdata_by_id.CustName;
                obj_connectassistlog.CustMobile = getassistdata_by_id.CustMobile;
                obj_connectassistlog.CustLandLine = getassistdata_by_id.CustLandLine;
                obj_connectassistlog.Adderss = getassistdata_by_id.Adderss;
                obj_connectassistlog.State = getassistdata_by_id.State;
                obj_connectassistlog.City = getassistdata_by_id.City;
                obj_connectassistlog.DateOfSale = getassistdata_by_id.DateOfSale;
                obj_connectassistlog.CreatedOn = System.DateTime.Now;
                obj_connectassistlog.CreatedBy = user_id.ToString();
                obj_connectassistlog.ConnectplusImage = getassistdata_by_id.ConnectplusImage;

                luminous.ConnectAssist_log.AddObject(obj_connectassistlog);


                getassistdata_by_id.Userid = user_id;
                getassistdata_by_id.DlrCode = dlrCode;
                getassistdata_by_id.Srno = serialno;
                getassistdata_by_id.DistCode = distcode;

                getassistdata_by_id.CustName = customername;
                getassistdata_by_id.CustMobile = customerphone;
                getassistdata_by_id.CustLandLine = customerlandLinenumber;
                getassistdata_by_id.Adderss = customeraddress;
                getassistdata_by_id.State = customerstate;
                getassistdata_by_id.City = customercity;
                getassistdata_by_id.DateOfSale = Convert.ToDateTime(saledate);
                getassistdata_by_id.Modifiedon = DateTime.Now;
                getassistdata_by_id.CreatedOn = DateTime.Now;
                getassistdata_by_id.CreatedBy = user_id;

                getassistdata_by_id.ConnectplusImage = Filename;


            }

            int savestatus = luminous.SaveChanges();










            //if (savestatus > 0)
           // {
                msgdata.Status = "200";
                msgdata.Message = "Data  inserted successfully.";

           // }
            //else
            //{
            //    msgdata.Message = "Data not inserted";
            //    msgdata.Status = "0";
            //}



            return msgdata;
        }


        //End func_SaveTicket//


        // func_viewticket//

        public List<Luminous.MpartnerNewApi.Model.ViewTicket> func_viewticket(string user_id, int ticketid)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();

            List<Luminous.MpartnerNewApi.Model.ViewTicket> viewticket = new List<Luminous.MpartnerNewApi.Model.ViewTicket>();



            try
            {

                //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();

                var getdata = (from x in luminous.ConnectAssists
                               join m in luminous.MapConnectAssist_Comments on x.Id equals m.ConnectAssistId
                               join u in luminous.UsersLists on x.Userid equals u.UserId
                               where x.Id == ticketid
                               select new
                               {
                                   id = x.Id,
                                   date = m.CreatedOn,
                                   text = m.Description,
                                   createdby = u.Dis_Name,
                                   attachment = m.Attachment,
                                   connectplus_msg = x.Message,
                                   serialno = x.Srno,
                                   Status = x.Flag
                               }).OrderBy(c => c.id).ToList();



                foreach (var data in getdata)
                {
                    //             public string serialno { get; set; }
                    //public string attachment { get; set; }
                    //public string Date { get; set; }
                    //public string createdby { get; set; }
                    //public string description { get; set; }

                    ViewTicket v_ticket = new ViewTicket();
                    v_ticket.serialno = data.serialno;
                    v_ticket.Date = data.date.ToString();
                    v_ticket.connectplus_msg = data.connectplus_msg;
                    v_ticket.attachment = ConfigurationSettings.AppSettings["UatUrl"].ToString() + "ConnectAssist/" + data.attachment;
                    v_ticket.createdby = data.createdby;
                    v_ticket.status = data.Status;
                    // Gallery glry = new Gallery();
                    // glry.ImageName = Url + "Gallery/" + list.ImageName.ToString();
                    // glry.date = list.CreatedOn.Value.Date;
                    v_ticket.description = data.text;

                    v_ticket.Id = data.id;

                    viewticket.Add(v_ticket);



                }
            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return viewticket;
        }

        //End func_viewticket//


        // func_getticket//

        public List<Luminous.MpartnerNewApi.Model.ViewTicket> func_getticket(string user_id, int month, int year)
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            LuminousEntities luminous = new LuminousEntities();

            List<Luminous.MpartnerNewApi.Model.ViewTicket> viewticket = new List<Luminous.MpartnerNewApi.Model.ViewTicket>();



            try
            {

                //var Surveylist = luminous.NotificationSurveys.Where(c => c.StartDate <= dateExist && c.Enddate >= dateExist).Select(c => new { c.SurveyID, c.Survey }).ToList();

                var getdata = from x in luminous.ConnectAssists
                              where x.CreatedOn.Value.Month == month && x.CreatedOn.Value.Year == year && x.Userid == user_id
                              orderby x.Id ascending
                              select x;



                foreach (var data in getdata)
                {
                    //             public string serialno { get; set; }
                    //public string attachment { get; set; }
                    //public string Date { get; set; }
                    //public string createdby { get; set; }
                    //public string description { get; set; }

                    ViewTicket v_ticket = new ViewTicket();
                    v_ticket.Id = data.Id;
                    v_ticket.serialno = data.Srno;

                    v_ticket.Date = data.CreatedOn.ToString();

                    if (data.Flag == "Resolved")
                    {
                        var resolveddate = luminous.ConnectAssists.Where(c => c.Id == data.Id).Select(c => c.ResolvedDate).SingleOrDefault();
                        var date = resolveddate.Value.AddDays(7).Date;
                        if (date == DateTime.Today)
                        {
                            luminous.ExecuteStoreCommand("Update ConnectAssist set Flag='Closed' where Id='" + data.Id + "' ");
                        }
                        v_ticket.status = data.Flag;

                    }
                    else
                    {
                        v_ticket.status = data.Flag;
                    }
                    v_ticket.attachment = "";
                    v_ticket.createdby = "";
                    v_ticket.description = "";



                    viewticket.Add(v_ticket);



                }
            }
            catch (Exception exc)
            {


                SaveServiceLog("", url, "", "", 1, exc.InnerException.ToString(), "", DateTime.Now, "", "", "", "");
            }
            return viewticket;
        }

        //End func_viewticket//

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
}