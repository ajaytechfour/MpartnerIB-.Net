using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Luminous.Models
{
    public class GetDataFromApi
    {
        private static string ApiName { get; set; }

        ////public async static Task<HttpResponseMessage> ApiConnectPlus2(string apiname, string token)
        ////{
        ////    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls; // | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

        ////    HttpRequestMessage request = new HttpRequestMessage();
        ////    HttpResponseMessage response = new HttpResponseMessage();
        ////    HttpClient client = new HttpClient();
        ////    dynamic responseTask = null;

        ////    try
        ////    {
        ////        ApiName = apiname;
        ////        client = new HttpClient();
        ////        var URL = ConfigurationManager.AppSettings["WebUrlConnectPlus"].ToString();

        ////        client.BaseAddress = new Uri(URL);

        ////        client.DefaultRequestHeaders.Accept.Clear();

        ////        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        ////        responseTask = ApiName + "?" + "token=" + token;

        ////        request = new HttpRequestMessage(HttpMethod.Get, responseTask);
        ////        response = await client.SendAsync(request);

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        var temp = ex.Message;

        ////    }
        ////    return response;
        ////}



        public static dynamic getConnectPlusTest(string apiname, string token)
        {
           //// ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls| SecurityProtocolType.Tls11 ;//| SecurityProtocolType.Tls12;

           // dynamic responseTask = null;
           // dynamic client;
           // UserDataParameter ud = new UserDataParameter();
           // ud.token=token;

           // try
           // {
           //     ApiName = apiname;
           //     client = new HttpClient();
           //     var URL = ConfigurationManager.AppSettings["WebUrlConnectPlus"].ToString();

           //     client.BaseAddress = new Uri(URL);

           //     //responseTask = client.GetAsync(ApiName + "?" + "token=" + token);

           //     responseTask = ItemApi(apiname, ud);

           //   //  responseTask.Wait();

           // }
           // catch (Exception ex)
           // {
           //     var temp = ex.Message;

           // }
           // return (responseTask);

            dynamic responseTask = null;
            dynamic client;

            // var HttpContext = "MS_HttpContext";
            token = "pass@1234";
            //apiname="webservice_WRS_Item_Type_Master";
            try
            {

                HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;

                //string res = "Web"+" "+"Browser:" + browser.Browser +","+ "Browser Version:" + browser.Version +","+ "Browser Platform:" + browser.Platform +","+ "IP Address:" + IPAddress.Parse(HttpContext.Current.Request.UserHostAddress);

                //string res = "Web" + " " + "Browser:" + browser.Browser + "," + "Browser Version:" + browser.Version + "," + "Browser Platform:" + browser.Platform + "," + "IP Address:" + IPAddress.Parse(HttpContext.Current.Request.UserHostAddress) + "Host Name:" + IPAddress.Parse(HttpContext.Current.Request.UserHostName);

                ApiName = apiname;
                client = new HttpClient();
                var URL = ConfigurationManager.AppSettings["WebUrlConnectPlus"].ToString();

                client.BaseAddress = new Uri(URL);

                responseTask = client.GetAsync(ApiName + "?" + "token=" + token);
                responseTask.Wait();
                // return (responseTask.Result);
            }
            catch (AggregateException ex)
            {
                var temp = ex.Message;

            }
            catch (Exception ex)
            {
                var temp = ex.Message;

            }
            return (responseTask.Result);

        }






        public static dynamic getConnectPlus_ItemList(string apiname, UserDataParameter ud)
        {
          //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;// | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            //dynamic responseTask = null;
            //dynamic client;

            //try
            //{
            //    ApiName = apiname;
            //    client = new HttpClient();
            //    var URL = ConfigurationManager.AppSettings["WebUrlConnectPlus"].ToString();

            //    client.BaseAddress = new Uri(URL);

            //    responseTask = client.GetAsync(ApiName + "?" + "token=" + ud.token + "&" +"Item_Type="+ud.Item_Type);

            //    responseTask.Wait();

            //}
            //catch (Exception ex)
            //{
            //    var temp = ex.Message;

            //}
            //return (responseTask.Result);





            dynamic responseTask = null;
            dynamic client;

            // var HttpContext = "MS_HttpContext";

            try
            {

                HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;

                //string res = "Web"+" "+"Browser:" + browser.Browser +","+ "Browser Version:" + browser.Version +","+ "Browser Platform:" + browser.Platform +","+ "IP Address:" + IPAddress.Parse(HttpContext.Current.Request.UserHostAddress);

                //string res = "Web" + " " + "Browser:" + browser.Browser + "," + "Browser Version:" + browser.Version + "," + "Browser Platform:" + browser.Platform + "," + "IP Address:" + IPAddress.Parse(HttpContext.Current.Request.UserHostAddress) + "Host Name:" + IPAddress.Parse(HttpContext.Current.Request.UserHostName);

                ApiName = apiname;
                client = new HttpClient();
                var URL = ConfigurationManager.AppSettings["WebUrlConnectPlus"].ToString();

                client.BaseAddress = new Uri(URL);

                responseTask = client.GetAsync(ApiName + "?" + "token=" + ud.token + "&" + "Item_Type=" + ud.Item_Type);
                responseTask.Wait();
                // return (responseTask.Result);
            }
            catch (Exception ex)
            {
                var temp = ex.Message;

            }
            return (responseTask.Result);

        }


        public static List<SelectListItem> ConnectPlusMultiItem()
        {
            IList<connectplusmultiResult> itemData = new List<connectplusmultiResult>();
            List<SelectListItem> itemsDist = new List<SelectListItem>();
            string token = "pass@1234";

            var response = Api("webservice_WRS_Item_Type_Master", token);

            if (response.IsSuccessStatusCode)
            {
                var responseResult = response.Content.ReadAsStringAsync().Result;
                JToken JtokenResult = JToken.Parse(responseResult);
                itemData = JsonConvert.DeserializeObject<List<connectplusmulti>>(responseResult);

                itemData = itemData.ToList();

                itemsDist.Add(new SelectListItem
                {
                    Text = "Select Items",
                    Value = "0",
                    Selected = true
                });
                foreach (var item in itemData)
                {
                    itemsDist.Add(new SelectListItem
                    {
                        Text = item.Item_Type,
                        Value = item.Item_Type,
                        //Selected = true
                    });
                }
            }
            return itemsDist;
        }




        public static dynamic Api(string apiname, string token)
        {
            dynamic responseTask = null;
            dynamic client;
          //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

            try
            {

                ApiName = apiname;
                client = new HttpClient();
                var URL = ConfigurationManager.AppSettings["UrlConnectPlus"].ToString();
                client.BaseAddress = new Uri("https://mpartnerv2.luminousindia.com/nonsapservices/api/nonsapservice/");

                //responseTask = client.GetAsync("webservice_WRS_Item_Type_Master" + "?" + "token=" + "pass@1234");

                responseTask = client.GetAsync("webservice_WRS_StateMaster" + "?" + "token=" + "pass@1234");

                responseTask.Wait();

            }
            catch (Exception ex)
            {
                var temp = ex.Message;

            }
            return (responseTask.Result);
        }








        public static dynamic ItemApi(string apiname, UserDataParameter ud)
        {
            dynamic responseTask = null;
            dynamic client;

            // var HttpContext = "MS_HttpContext";

            try
            {

                HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;

                //string res = "Web"+" "+"Browser:" + browser.Browser +","+ "Browser Version:" + browser.Version +","+ "Browser Platform:" + browser.Platform +","+ "IP Address:" + IPAddress.Parse(HttpContext.Current.Request.UserHostAddress);

                //string res = "Web" + " " + "Browser:" + browser.Browser + "," + "Browser Version:" + browser.Version + "," + "Browser Platform:" + browser.Platform + "," + "IP Address:" + IPAddress.Parse(HttpContext.Current.Request.UserHostAddress) + "Host Name:" + IPAddress.Parse(HttpContext.Current.Request.UserHostName);

                ApiName = apiname;
                client = new HttpClient();
                var URL = ConfigurationManager.AppSettings["WebUrlConnectPlus"].ToString();

                client.BaseAddress = new Uri(URL);

                responseTask = client.GetAsync(ApiName + "?" + "token=" + ud.token); 
                responseTask.Wait();
                // return (responseTask.Result);
            }
            catch (Exception ex)
            {
                var temp = ex.Message;

            }
            return (responseTask.Result);
        }




        public static dynamic ItemListApi(string apiname, UserDataParameter ud)
        {
            dynamic responseTask = null;
            dynamic client;

            // var HttpContext = "MS_HttpContext";

            try
            {

                HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;

                //string res = "Web"+" "+"Browser:" + browser.Browser +","+ "Browser Version:" + browser.Version +","+ "Browser Platform:" + browser.Platform +","+ "IP Address:" + IPAddress.Parse(HttpContext.Current.Request.UserHostAddress);

                //string res = "Web" + " " + "Browser:" + browser.Browser + "," + "Browser Version:" + browser.Version + "," + "Browser Platform:" + browser.Platform + "," + "IP Address:" + IPAddress.Parse(HttpContext.Current.Request.UserHostAddress) + "Host Name:" + IPAddress.Parse(HttpContext.Current.Request.UserHostName);

                ApiName = apiname;
                client = new HttpClient();
                var URL = ConfigurationManager.AppSettings["WebUrlConnectPlus"].ToString();

                client.BaseAddress = new Uri(URL);

                responseTask = client.GetAsync(ApiName + "?" + "Item_Type=" + ud.Item_Type + "&" + "token=" + ud.token);
                responseTask.Wait();
                // return (responseTask.Result);
            }
            catch (Exception ex)
            {
                var temp = ex.Message;

            }
            return (responseTask.Result);
        }


    }
}