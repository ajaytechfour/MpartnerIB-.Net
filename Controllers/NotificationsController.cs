using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.Text.RegularExpressions;
using System.Data;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using System.Configuration;
//using Luminous.EF;

namespace LuminousMpartnerIB.Controllers
{
    public class NotificationsController : Controller
    {
        //
        // GET: /Notifications/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private string PageUrl = "/Notifications/Notification";
        string android_serverkey = ConfigurationSettings.AppSettings["android_ServerKey"].ToString();
        string ios_serverkey = ConfigurationSettings.AppSettings["ios_ServerKey"].ToString();

        string url = ConfigurationSettings.AppSettings["UatUrl"].ToString();
        private DataTable dt = new DataTable();
        [ActionName("Notification")]
        public ActionResult Notification1()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        public ActionResult Save(Notification noti, string statusC, string Alls, string rglist, string DistriCheck, string DealCheck, string statusD, HttpPostedFileBase userupload_file, HttpPostedFileBase postedFile)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    #region Check Validation for permission
                    string Image = "";
                    Alls = Alls ?? "off";
                    DealCheck = DealCheck ?? "off";
                    DistriCheck = DistriCheck ?? "off";
                    rglist = rglist ?? "";
                    //if ((Alls.ToLower() == "off" || Alls == "") && (rglist == "" || rglist == "0"))
                    //{
                    //    ModelState.AddModelError("createdBy", "Send To Has No Value");

                    //}
                    //else if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    //{
                    //    ModelState.AddModelError("createdBy", "Check Eiter Distributor OR Dealer");
                    //}

                    if (userupload_file == null)
                    {
                        //if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (rglist == "" || rglist == null || rglist == "0") && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                        //{

                        //    ModelState.AddModelError("createdBy", "Permission For Has No Value");

                        //}
                        //else if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                        //{
                        //    ModelState.AddModelError("createdBy", "Check Either Distributor OR Dealer");
                        //}
                    }
                    #endregion

                    #region Check Validation for Image

                    //if (postedFile == null)
                    //{
                    //    ModelState.AddModelError("File", "*");
                    //    ViewBag.File = "File Is Not Uploaded";
                    //}
                    //else
                    //{
                    //    string FileExtension = Path.GetExtension(postedFile.FileName);
                    //    if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".tiff" || FileExtension.ToLower() == ".gif" ||
                    //       FileExtension.ToLower() == ".png")
                    //    {

                    //    }
                    //    else
                    //    {
                    //        ModelState.AddModelError("File", "*");
                    //        ViewBag.File = "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg";
                    //    }



                    //}

                    #endregion

                    //#region Subject
                    //if (noti.C_Subject == null)
                    //{
                    //    ModelState.AddModelError("C_Subject", "Subject Has No Value");
                    //}
                    //else
                    //{
                    //    if (noti.C_Subject == "")
                    //    {
                    //        ModelState.AddModelError("C_Subject", "Subject Has No Value");
                    //    }
                    //    if (noti.C_Subject.Length > 200)
                    //    {
                    //        ModelState.AddModelError("C_Subject", "Character Should Be Less Than 200");
                    //    }
                    //}
                    //#endregion

                    //#region Text
                    //if (noti.C_text == null)
                    //{
                    //    ModelState.AddModelError("C_text", "Subject Has No Value");
                    //}
                    //else
                    //{
                    //    if (noti.C_text == "")
                    //    {
                    //        ModelState.AddModelError("C_text", "Subject Has No Value");
                    //    }
                    //    if (noti.C_text.Length > 249)
                    //    {
                    //        ModelState.AddModelError("C_text", "Character Should Be Less Than 100");
                    //    }
                    //}
                    //#endregion
                    if (ModelState.IsValid)
                    {
                        #region Date Saving
                        string Imagename = "";

                        if (postedFile == null)
                        {
                            Imagename = "";
                        }
                        else
                        {
                            string filename = Path.GetFileNameWithoutExtension(postedFile.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(postedFile.FileName);
                            Imagename = filename.Replace(" ", string.Empty);
                        }

                        Notification _notifications = new Notification();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            _notifications.C_status = 1;
                        }
                        else
                        {
                            _notifications.C_status = 0;
                        }
                        string statusd = statusD ?? "off";
                        if (statusd == "on")
                        {
                            _notifications.Alertflag = "1";
                        }
                        else
                        {
                            _notifications.Alertflag = "0";
                        }
                        _notifications.C_Subject = noti.C_Subject;
                        _notifications.C_text = noti.C_text;
                        _notifications.createdDate = DateTime.Now;
                        _notifications.ImagePath = Imagename;
                        _notifications.createdBy = Session["userid"].ToString();
                        db.Notifications.Add(_notifications);
                        int affectedValue = db.SaveChanges();
                        if (affectedValue > 0)
                        {
                            try
                            {
                                if (Imagename == "")
                                {
                                    Image = "";
                                }
                                else
                                {
                                    string str = Path.Combine(Server.MapPath("~/NotificationImage/"), Imagename);
                                    postedFile.SaveAs(str);


                                    if (System.IO.File.Exists(Server.MapPath("~/NotificationImage/") + _notifications.ImagePath))
                                    {
                                        Image = url + "NotificationImage/" + _notifications.ImagePath;
                                    }
                                    else
                                    {
                                        Image = "No Image";
                                    }
                                }
                                #region Dealer Destributor Region
                                if (userupload_file == null)
                                {
                                    Alls = Alls ?? "off";
                                    if (Alls.ToLower() != "on")
                                    {
                                        if (rglist != "" && rglist != null)
                                        {
                                            Regex rg = new Regex(",");
                                            string[] reglist = rg.Split(rglist);
                                            foreach (string s in reglist)
                                            {
                                                NotificationAccessTable pat = new NotificationAccessTable();
                                                pat.notificationid = _notifications.id;
                                                pat.RegId = int.Parse(s);
                                                pat.createdate = DateTime.Now;
                                                pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                                pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                                pat.createby = Session["userid"].ToString();
                                                db.NotificationAccessTables.Add(pat);
                                                db.SaveChanges();

                                            }
                                        }
                                    }
                                    else
                                    {
                                        NotificationAccessTable pat = new NotificationAccessTable();
                                        pat.notificationid = _notifications.id;
                                        pat.AllAcess = true;
                                        db.NotificationAccessTables.Add(pat);
                                        db.SaveChanges();
                                    }
                                    if ((Alls.ToLower() != "on") && (rglist == "" || rglist == null) && (DistriCheck != "on" || DealCheck != "on"))
                                    {
                                        NotificationAccessTable pat = new NotificationAccessTable();
                                        pat.notificationid = _notifications.id;
                                        pat.RegId = null;
                                        pat.createdate = DateTime.Now;
                                        pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                        pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                        pat.createby = Session["userid"].ToString();
                                        db.NotificationAccessTables.Add(pat);
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {

                                    DataTable dtupload = new DataTable();

                                    System.Data.DataTable DTimportexcel = new System.Data.DataTable();
                                    StringBuilder strValidations = new StringBuilder(string.Empty);
                                    List<UserSapcode> sapcode = new List<UserSapcode>();

                                    if (userupload_file.ContentLength > 0)
                                    {
                                        string ext = System.IO.Path.GetExtension(Path.GetFileName(userupload_file.FileName));
                                        var originalfilename = Path.GetFileName(userupload_file.FileName);
                                        if (ext == ".CSV" || ext == ".csv")
                                        {
                                            //string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads"),
                                            //  string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads"),
                                            string[] changeextension = originalfilename.Split('.');
                                            string xlsextension = changeextension[0] + ".csv";
                                            string path =
                                Path.Combine(Path.GetDirectoryName(xlsextension)
                                , string.Concat(Path.GetFileNameWithoutExtension(xlsextension)
                                , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                                , Path.GetExtension(xlsextension)
                                )
                                );
                                            //Path.GetFileName(FileUpload.FileName));
                                            var pathfolder = Path.Combine(Server.MapPath("~/UploadSapcode/"), path);
                                            userupload_file.SaveAs(pathfolder);
                                            dtupload = ProcessCSV(pathfolder);
                                            DataSet ds = new DataSet();

                                            ds.Tables.Add(dtupload);

                                            //  int dsRowcount = 0;

                                            if (ds.Tables.Count > 0)
                                            {
                                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                                {
                                                    UserSapcode scode = new UserSapcode();
                                                    scode.SAPCode = ds.Tables[0].Rows[i]["Column1"].ToString();
                                                    sapcode.Add(scode);
                                                }
                                            }
                                            foreach (var data in sapcode)
                                            {
                                                var usertype = db.UsersLists.Where(c => c.UserId == data.SAPCode).Select(c => new { c.CustomerType, c.UserId }).SingleOrDefault();
                                                NotificationAccessTable pat = new NotificationAccessTable();
                                                pat.notificationid = _notifications.id;
                                                pat.RegId = null;
                                                pat.createdate = DateTime.Now;
                                                pat.AllDealerAccess = false;
                                                pat.AllDestriAccess = false;
                                                if (usertype.CustomerType == "DISTY")
                                                {
                                                    pat.SpecificDealerAccess = "0";
                                                    pat.SpecificDestriAccess = usertype.UserId;
                                                }
                                                if (usertype.CustomerType == "Dealer" || usertype.CustomerType == "DEALER")
                                                {
                                                    pat.SpecificDealerAccess = usertype.UserId;
                                                    pat.SpecificDestriAccess = "0";
                                                }
                                                pat.createby = Session["userid"].ToString();
                                                db.NotificationAccessTables.Add(pat);
                                                db.SaveChanges();



                                            }
                                        }
                                    }

                                }
                                #region Commented Code
                                //    if (disList != "0" && disList != "" && disList != null)
                                //    {
                                //        Regex rg = new Regex(",");
                                //        string[] reglist = rg.Split(disList);
                                //        foreach (string s in reglist)
                                //        {
                                //            NotificationAccessTable pat2 = new NotificationAccessTable();
                                //            pat2.notificationid = _notifications.id;
                                //            pat2.DestributorID = int.Parse(s);
                                //            pat2.createdate = DateTime.Now;
                                //            pat2.createby = Session["userid"].ToString();
                                //            db.NotificationAccessTables.AddObject(pat2);
                                //            db.SaveChanges();

                                //        }
                                //    }
                                //    else
                                //    {
                                //        NotificationAccessTable pat2 = new NotificationAccessTable();
                                //        pat2.notificationid = _notifications.id;
                                //        pat2.AllDestriAccess = true;
                                //        pat2.createdate = DateTime.Now;
                                //        pat2.createby = Session["userid"].ToString();
                                //        db.NotificationAccessTables.AddObject(pat2);
                                //        db.SaveChanges();

                                //    }



                                //    if (Dealist != "0" && Dealist != "" && Dealist != null)
                                //    {
                                //        Regex rg = new Regex(",");
                                //        string[] reglist = rg.Split(Dealist);
                                //        foreach (string s in reglist)
                                //        {
                                //            NotificationAccessTable pat3 = new NotificationAccessTable();
                                //            pat3.notificationid = _notifications.id;
                                //            pat3.DealerId = int.Parse(s);
                                //            pat3.createdate = DateTime.Now;
                                //            pat3.createby = Session["userid"].ToString();
                                //            db.NotificationAccessTables.AddObject(pat3);
                                //            db.SaveChanges();

                                //        }
                                //    }
                                //    else
                                //    {
                                //        NotificationAccessTable pat2 = new NotificationAccessTable();
                                //        pat2.notificationid = _notifications.id;
                                //        pat2.AllDealerAccess = true;
                                //        pat2.createdate = DateTime.Now;
                                //        pat2.createby = Session["userid"].ToString();
                                //        db.NotificationAccessTables.AddObject(pat2);
                                //        db.SaveChanges();

                                //    }
                                //}
                                //else
                                //{
                                //    NotificationAccessTable pat3 = new NotificationAccessTable();
                                //    pat3.notificationid = _notifications.id;
                                //    pat3.AllAcess = true;
                                //    pat3.createdate = DateTime.Now;
                                //    pat3.createby = Session["userid"].ToString();
                                //    db.NotificationAccessTables.AddObject(pat3);
                                //    db.SaveChanges();
                                //}
                                #endregion
                                #endregion
                                string pushNotificationResponse = PushNotifioncationToAndroid(_notifications.id, _notifications.C_text, _notifications.C_Subject, Image);
                                return Content("<script>alert('Notification Saved Successfully " + pushNotificationResponse + "');location.href='../Notifications/Notification';</script>");


                            }
                            catch (Exception ex)
                            {
                                Exception exs = ex.InnerException;
                                return Content("<script>alert('" + ex.Message.ToString() + "');location.href='../Notifications/Notification';</script>");
                            }


                        }
                        #endregion

                    }
                    return View("Notification");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        [NonAction]
        public string PushNotifioncationToAndroid(int NotificationID, string NotificationDescription, string NotificationSubject, string Imagepath)
        {
            List<userIdForPushNotification_Result> useridList = db.userIdForPushNotification(NotificationID).ToList();
            string FailedToSendTo = "";
            if (useridList.Count > 0)
            {
                foreach (var i in useridList)
                {
                    try
                    {
                        if (i != null)
                        {
                            Session["UserNotId"] = i.userid.ToString();
                        }

                        if (i.Fcm_token == null || i.Fcm_token == "")
                        {
                            FailedToSendTo += i.userid + ", ";
                        }
                        else
                        {
                            string fcmtoken = i.Fcm_token;
                            string message = NotificationSubject;
                            string image = Imagepath;
                            string title = NotificationSubject;
                            string description = NotificationDescription;
                            
                            if (i.OSType == "iOS")
                            {
                                if (image == "")
                                {
                                    var payload = new
              {
                  to = fcmtoken,

                  priority = "high",
                  content_available = true,
                  notification = new
                  {
                      body = description,
                      title = title,
                    
                      badge = 1
                  },
              };
                                    var serializer = new JavaScriptSerializer();
                                    var json = serializer.Serialize(payload);
                                    SendGCMNotification(ios_serverkey, json);
                                }
                                else
                                {
                                    var payload = new
                                    {
                                        to = fcmtoken,

                                        priority = "high",
                                        content_available = true,
                                        notification = new
                                        {
                                            body = description,
                                            title = title,
                                            img_url = Imagepath,
                                            badge = 1
                                        },
                                    };
                                    var serializer = new JavaScriptSerializer();
                                    var json = serializer.Serialize(payload);
                                    SendGCMNotification(ios_serverkey, json);
                                }



                            }

                            else
                            {
                                if (image == "")
                                {
                                    var payload1 = new
                                    {
                                        to = fcmtoken,
                                        data = new
                                        {
                                            body = description,
                                            title = title,

                                        },
                                    };
                                    var serializer = new JavaScriptSerializer();
                                    var json = serializer.Serialize(payload1);
                                    SendGCMNotification(android_serverkey, json);
                                }
                                else
                                {
                                    var payload1 = new
                                    {
                                        to = fcmtoken,
                                        data = new
                                        {
                                            body = description,
                                            title = title,
                                            img_url = Imagepath
                                        },
                                    };
                                    var serializer = new JavaScriptSerializer();
                                    var json = serializer.Serialize(payload1);
                                    SendGCMNotification(android_serverkey, json);
                                }

                                //SendGCMNotification("AIzaSyARvUZVlbzRkB_vfD2YtvBbhsTtlCdflg4", postData);
                            }

                        }
                    }
                    catch (Exception exc)
                    {
                        FailedToSendTo += i.userid + exc + ", ";
                    }

                }
                if (FailedToSendTo == "")
                {
                    return "";
                }
                else
                {
                    FailedToSendTo = FailedToSendTo.Remove(FailedToSendTo.Length - 1, 1);
                    return "and Unable To Send Notifications To " + FailedToSendTo;
                }

            }
            else
            {
                return "There Is No User In List To Send The Notifications";
            }

        }
        [NonAction]
        private string SendGCMNotification(string apiKey, string postData, string postDataContentType = "application/json")
        {


            //  MESSAGE CONTENT

            if (apiKey == ios_serverkey || apiKey == android_serverkey)
            {
                //var serializer = new JavaScriptSerializer();
                // var json = serializer.Serialize(postData);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                //
                //  CREATE REQUEST
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                Request.Method = "POST";
                Request.KeepAlive = false;
                Request.ContentType = postDataContentType;
                Request.Headers.Add(string.Format("Authorization: key={0}", apiKey));
                Request.ContentLength = byteArray.Length;
                //Request.GetRequestStream();
                Stream dataStream = Request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                //
                //  SEND MESSAGE
                try
                {
                    WebResponse Response = Request.GetResponse();
                    HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
                    if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
                    {
                        var text = "Unauthorized - need new token";
                    }
                    else if (!ResponseCode.Equals(HttpStatusCode.OK))
                    {
                        var text = "Response from web service isn't OK";
                    }

                    StreamReader Reader = new StreamReader(Response.GetResponseStream());
                    string responseLine = Reader.ReadToEnd();
                    SaveGCMLog(Session["UserNotId"].ToString(), apiKey, postData, postDataContentType, responseLine);
                    Reader.Close();

                    return responseLine;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }


            return "error";
        }
        public JsonResult GetNotificationsDetail(int? page)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    var Notificationdetails = (from c in db.Notifications
                                               where c.C_status != 2
                                               select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }

                    var Notificationdetails2 = (from c in Notificationdetails

                                                select new
                                                {
                                                    id = c.id,
                                                    Subject = c.C_Subject,
                                                    Imagepath = c.ImagePath,
                                                    Text = c.C_text,
                                                    status = c.C_status == 1 ? "Active" : "Deactive",
                                                }).OrderByDescending(a => a.id).Skip(page ?? 0).Take(15).ToList();
                    if (Notificationdetails.Count() % 15 == 0)
                    {
                        totalrecord = Notificationdetails.Count() / 15;
                    }
                    else
                    {
                        totalrecord = (Notificationdetails.Count() / 15) + 1;
                    }
                    var data = new { result = Notificationdetails2, TotalRecord = totalrecord };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }
        public ActionResult Edit(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    Notification cud = db.Notifications.Single(a => a.id == id);
                    ViewBag.status = cud.C_status;
                    ViewBag.Alertstatus = cud.Alertflag;
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        public ActionResult Update(Notification _notifications, string statusC, string statusD, HttpPostedFileBase postedFile)
        {

            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    #region Subject
                    if (_notifications.C_Subject == null)
                    {
                        ModelState.AddModelError("C_Subject", "Subject Has No Value");
                    }
                    else
                    {
                        if (_notifications.C_Subject == "")
                        {
                            ModelState.AddModelError("C_Subject", "Subject Has No Value");
                        }
                        if (_notifications.C_Subject.Length > 99)
                        {
                            ModelState.AddModelError("C_Subject", "Character Should Be Less Than 100");
                        }
                    }
                    #endregion

                    #region Text
                    if (_notifications.C_text == null)
                    {
                        ModelState.AddModelError("C_text", "Subject Has No Value");
                    }
                    else
                    {
                        if (_notifications.C_text == "")
                        {
                            ModelState.AddModelError("C_text", "Subject Has No Value");
                        }
                        if (_notifications.C_text.Length > 249)
                        {
                            ModelState.AddModelError("C_text", "Character Should Be Less Than 100");
                        }
                    }
                    #endregion
                    if (ModelState.IsValid)
                    {
                        if (postedFile == null)
                        {

                            Notification notification = db.Notifications.Single(a => a.id == _notifications.id);
                            NotificationsHistory notiHistory = new NotificationsHistory();
                            notiHistory.C_status = notification.C_status;
                            notiHistory.C_Subject = notification.C_Subject;
                            notiHistory.C_text = notification.C_text;
                            notiHistory.ImagePath = notification.ImagePath;
                            notiHistory.NotificationId = notification.id;
                            notiHistory.ModifiedBy = Session["userid"].ToString();
                            notiHistory.ModifiedDate = DateTime.Now;
                            db.NotificationsHistories.Add(notiHistory);

                            string status = statusC ?? "off";
                            if (status == "on")
                            {
                                notification.C_status = 1;
                            }
                            else
                            {
                                notification.C_status = 0;
                            }
                            string statusd = statusD ?? "off";
                            if (statusd == "on")
                            {
                                notification.Alertflag = "1";
                            }
                            else
                            {
                                notification.Alertflag = "0";
                            }
                            notification.C_Subject = _notifications.C_Subject;
                            notification.C_text = _notifications.C_text;

                            notification.ModifiedBy = Session["userid"].ToString();
                            notification.ModifiedDate = DateTime.Now;
                            db.SaveChanges();
                            return Content("<script>alert('Notification Updated Successfully');location.href='../Notifications/Notification';</script>");
                        }
                        else
                        {

                            string FileExtension = Path.GetExtension(postedFile.FileName);
                            if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".tiff" || FileExtension.ToLower() == ".gif" ||
                               FileExtension.ToLower() == ".png")
                            {

                            }
                            else
                            {
                                ModelState.AddModelError("File", "*");
                                ViewBag.File = "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg";
                            }

                            if (ModelState.IsValid)
                            {
                                try
                                {
                                    string filename = Path.GetFileNameWithoutExtension(postedFile.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(postedFile.FileName);
                                    string Imagename = filename.Replace(" ", string.Empty);
                                    Notification notification = db.Notifications.Single(a => a.id == _notifications.id);

                                    NotificationsHistory notiHistory = new NotificationsHistory();
                                    notiHistory.C_status = notification.C_status;
                                    notiHistory.C_Subject = notification.C_Subject;
                                    notiHistory.C_text = notification.C_text;
                                    notiHistory.ImagePath = notification.ImagePath;
                                    notiHistory.NotificationId = notification.id;
                                    notiHistory.ModifiedBy = Session["userid"].ToString();
                                    notiHistory.ModifiedDate = DateTime.Now;
                                    db.NotificationsHistories.Add(notiHistory);


                                    //Save New Record

                                    string status = statusC ?? "off";
                                    if (status == "on")
                                    {
                                        notification.C_status = 1;
                                    }
                                    else
                                    {
                                        notification.C_status = 0;
                                    }
                                    string statusd = statusD ?? "off";
                                    if (statusd == "on")
                                    {
                                        notification.Alertflag = "1";
                                    }
                                    else
                                    {
                                        notification.Alertflag = "0";
                                    }
                                    notification.C_Subject = _notifications.C_Subject;
                                    notification.C_text = _notifications.C_text;
                                    notification.ImagePath = Imagename;
                                    notification.ModifiedBy = Session["userid"].ToString();
                                    notification.ModifiedDate = DateTime.Now;

                                    if (db.SaveChanges() > 0)
                                    {
                                        ViewBag.Update = "Done";
                                        string str = Path.Combine(Server.MapPath("~/NotificationImage/"), Imagename);
                                        postedFile.SaveAs(str);
                                        return Content("<script>alert('Notification Updated Successfully');location.href='../Notifications/Notification';</script>");

                                    }
                                    else
                                    {
                                        return Content("<script>alert('Record Has Not updated');</script>");
                                    }


                                }
                                catch
                                {
                                    return View("Index");
                                }
                            }

                        }

                    }
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
            return View("Edit");
        }
        public ActionResult EditReciverList(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    // List<NotificationAccessTable> Pat = db.NotificationAccessTables.Where(a => a.notificationid == id && (a.deleted != true || a.deleted == null || a.deleted == false)).ToList();
                    List<GetNotificationAccessTable_Result> Pat = (from c in db.GetNotificationAccessTable(id)
                                                                   select c
                                                           ).ToList();

                    Session["id"] = id;
                    return View(Pat);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        public ActionResult SavePermission(string Alls, string rglist, string DistriCheck, string DealCheck)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    Alls = Alls ?? "off";
                    int id = Convert.ToInt32(Session["id"].ToString());
                    #region Check Validation for permission

                    Alls = Alls ?? "off";
                    rglist = rglist ?? "";
                    DealCheck = DealCheck ?? "off";
                    DistriCheck = DistriCheck ?? "off";
                    //if ((Alls.ToLower() == "off" || Alls == "") && (rglist == "" || rglist == "0"))
                    //{
                    //    ModelState.AddModelError("AllAcess", "Permission For Has No Value");

                    //}
                    //else if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    //{
                    //    ModelState.AddModelError("AllAcess", "Check Eiter Distributor OR Dealer");
                    //}
                    if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (rglist == "" || rglist == null || rglist == "0") && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    {

                        ModelState.AddModelError("AllAcess", "Permission For Has No Value");

                    }
                    else if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    {
                        ModelState.AddModelError("AllAcess", "Check Either Distributor OR Dealer");
                    }
                    #endregion
                    if (ModelState.IsValid)
                    {
                        List<NotificationAccessTable> patDel = db.NotificationAccessTables.Where(a => a.notificationid == id).ToList();
                        //Rajesh
                        //foreach (var i in patDel)
                        //{
                        //    db.DeleteObject(i);
                        //}

                        if (Alls.ToLower() != "on")
                        {
                            if (rglist != "" && rglist != null)
                            {
                                Regex rg = new Regex(",");
                                string[] reglist = rg.Split(rglist);
                                foreach (string s in reglist)
                                {
                                    NotificationAccessTable pat = new NotificationAccessTable();
                                    pat.notificationid = id;
                                    pat.RegId = int.Parse(s);
                                    pat.createdate = DateTime.Now;
                                    pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                    pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                    pat.createby = Session["userid"].ToString();
                                    db.NotificationAccessTables.Add(pat);
                                    db.SaveChanges();

                                }
                            }
                        }
                        else
                        {
                            NotificationAccessTable pat = new NotificationAccessTable();
                            pat.notificationid = id;
                            pat.AllAcess = true;
                            db.NotificationAccessTables.Add(pat);
                            db.SaveChanges();
                        }
                        if ((Alls.ToLower() != "on") && (rglist == "" || rglist == null) && (DistriCheck != "on" || DealCheck != "on"))
                        {
                            NotificationAccessTable pat = new NotificationAccessTable();
                            pat.notificationid = id;
                            pat.RegId = null;
                            pat.createdate = DateTime.Now;
                            pat.AllDealerAccess = DealCheck == "off" ? false : true;
                            pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                            pat.createby = Session["userid"].ToString();
                            db.NotificationAccessTables.Add(pat);
                            db.SaveChanges();
                        }
                        #region Commented Code
                        //    if (disList != "0" && disList != "" && disList != null)
                        //    {
                        //        Regex rg = new Regex(",");
                        //        string[] reglist = rg.Split(disList);
                        //        foreach (string s in reglist)
                        //        {
                        //            NotificationAccessTable pat2 = new NotificationAccessTable();
                        //            pat2.notificationid = id;
                        //            pat2.DestributorID = int.Parse(s);
                        //            pat2.createdate = DateTime.Now;
                        //            pat2.createby = Session["userid"].ToString();
                        //            db.NotificationAccessTables.AddObject(pat2);
                        //            db.SaveChanges();

                        //        }
                        //    }
                        //    else
                        //    {
                        //        NotificationAccessTable pat2 = new NotificationAccessTable();
                        //        pat2.notificationid = id;
                        //        pat2.AllDestriAccess = true;
                        //        pat2.createdate = DateTime.Now;
                        //        pat2.createby = Session["userid"].ToString();
                        //        db.NotificationAccessTables.AddObject(pat2);
                        //        db.SaveChanges();

                        //    }



                        //    if (Dealist != "0" && Dealist != "" && Dealist != null)
                        //    {
                        //        Regex rg = new Regex(",");
                        //        string[] reglist = rg.Split(Dealist);
                        //        foreach (string s in reglist)
                        //        {
                        //            NotificationAccessTable pat3 = new NotificationAccessTable();
                        //            pat3.notificationid = id;
                        //            pat3.DealerId = int.Parse(s);
                        //            pat3.createdate = DateTime.Now;
                        //            pat3.createby = Session["userid"].ToString();
                        //            db.NotificationAccessTables.AddObject(pat3);
                        //            db.SaveChanges();

                        //        }
                        //    }
                        //    else
                        //    {
                        //        NotificationAccessTable pat2 = new NotificationAccessTable();
                        //        pat2.notificationid = id;
                        //        pat2.AllDealerAccess = true;
                        //        pat2.createdate = DateTime.Now;
                        //        pat2.createby = Session["userid"].ToString();
                        //        db.NotificationAccessTables.AddObject(pat2);
                        //        db.SaveChanges();

                        //    }
                        //}
                        //else
                        //{
                        //    NotificationAccessTable pat3 = new NotificationAccessTable();
                        //    pat3.notificationid = id;
                        //    pat3.AllAcess = true;
                        //    pat3.createdate = DateTime.Now;
                        //    pat3.createby = Session["userid"].ToString();
                        //    db.NotificationAccessTables.AddObject(pat3);
                        //    db.SaveChanges();
                        //}
                        #endregion

                        return RedirectToAction("EditReciverList", new { id = id });
                    }
                    return View("EditReciverList", db.GetNotificationAccessTable(id).ToList());
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }

            //return Content("<script>alert('Permisson For Has No Value');location.href='../Notifications/EditReciverList/" + id + "';</script>");
        }
        [HttpPost]
        public JsonResult DeleteNotification(int id)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = PageUrl;
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (true)
                {
                    Notification notification = db.Notifications.Single(a => a.id == id);
                    NotificationsHistory notiHistory = new NotificationsHistory();
                    notiHistory.C_status = notification.C_status;
                    notiHistory.C_Subject = notification.C_Subject;
                    notiHistory.C_text = notification.C_text;
                    notiHistory.NotificationId = notification.id;
                    db.NotificationsHistories.Add(notiHistory);


                    notification.C_status = 2;
                    notification.ModifiedBy = Session["userid"].ToString();
                    notification.ModifiedDate = DateTime.Now;

                    db.SaveChanges();
                    return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }

        }
        private static DataTable ProcessCSV(string fileName)
        {
            //Set up our variables
            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray;
            DataTable dt = new DataTable();
            DataRow row;
            // work out where we should split on comma, but not in a sentence
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            //Set the filename in to our stream
            StreamReader sr = new StreamReader(fileName);

            //Read the first line and split the string at , with our regular expression in to an array
            line = sr.ReadLine();
            strArray = r.Split(line);

            //For each item in the new split array, dynamically builds our Data columns. Save us having to worry about it.
            Array.ForEach(strArray, s => dt.Columns.Add(new DataColumn()));

            //Read each line in the CVS file until it’s empty
            while ((line = sr.ReadLine()) != null)
            {
                row = dt.NewRow();

                //add our current value to our data row
                row.ItemArray = r.Split(line);
                dt.Rows.Add(row);
            }

            //Tidy Streameader up
            sr.Dispose();

            //return a the new DataTable
            return dt;

        }

        public void SaveGCMLog(string sapcode, string Apikey, string PostData, string PostContent_Type, string ResponseStatus)
        {
            var gcmlog = new GCM_NotificationLog();
            gcmlog.SapCode = sapcode;
            gcmlog.Regid = "";
            gcmlog.ApiKey = Apikey;
            gcmlog.PostData = PostData;
            gcmlog.PostContent_Type = PostContent_Type;
            gcmlog.ResponseStatus = ResponseStatus;
            gcmlog.CreatedOn = DateTime.Now;
            gcmlog.CreatedBy = Session["userid"].ToString();

            db.GCM_NotificationLog.Add(gcmlog);
            db.SaveChanges();
        }

    }
}






