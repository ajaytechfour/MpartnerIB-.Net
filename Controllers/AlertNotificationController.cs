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
using Luminous.EF;

namespace LuminousMpartnerIB.Controllers
{
    public class AlertNotificationController : Controller
    {
        //
        // GET: /Notifications/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private string PageUrl = "/AlertNotification/AlertNotification";
        private DataTable dt = new DataTable();
        [ActionName("AlertNotification")]
        public ActionResult Notification1()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        public ActionResult Save(AlertNotification noti, string statusC, string Alls, string rglist, string disList, string Dealist, string DistriCheck, string DealCheck, HttpPostedFileBase postedFile, HttpPostedFileBase userupload_file)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["createrole"].ToString() == "1")
                {
                    #region Check Validation for permission

                    Alls = Alls ?? "off";
                    rglist = rglist ?? "";
                    //if ((Alls.ToLower() == "off" || Alls == "") && (rglist == "" || rglist == "0"))
                    //{
                    //    ModelState.AddModelError("CreatedBy", "Permission For Has No Value");

                    //}
                    //else if ((Alls.ToLower() == "off" || Alls == "") && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    //{
                    //    ModelState.AddModelError("CreatedBy", "Check Eiter Distributor OR Dealer");
                    //}
                    if (userupload_file == null)
                    {
                        if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (rglist == "" || rglist == null || rglist == "0") && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                        {

                            ModelState.AddModelError("createdBy", "Permission For Has No Value");

                        }
                        else if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                        {
                            ModelState.AddModelError("createdBy", "Check Either Distributor OR Dealer");
                        }
                    }
                    #endregion

                    #region Text validation
                    if (noti.C_text == "" || noti.C_text == null)
                    {
                        ModelState.AddModelError("C_text", "Text Cannot be empty.");
                        //  ViewBag.File = "File Is Not Uploaded";
                    }
                    #endregion

                    #region Image validation
                    if (postedFile == null)
                    {
                        ModelState.AddModelError("File", "*");
                        ViewBag.File = "File Is Not Uploaded";
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



                    }
                    #endregion
                    if (ModelState.IsValid)
                    {


                        string filename = Path.GetFileNameWithoutExtension(postedFile.FileName.Trim()) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(postedFile.FileName);
                        string Imagename = filename.Replace(" ", string.Empty);
                        AlertNotification _notifications = new AlertNotification();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            _notifications.C_Status = 1;
                        }
                        else
                        {
                            _notifications.C_Status = 0;
                        }



                        _notifications.Imagename = postedFile.FileName.ToString();
                        _notifications.Imagepath = Imagename;
                        _notifications.C_text = noti.C_text;
                        _notifications.CreatedOn = DateTime.Now;
                        _notifications.CreatedBy = Session["userid"].ToString();
                        db.AlertNotifications.AddObject(_notifications);
                        int affectedValue = db.SaveChanges();
                        if (affectedValue > 0)
                        {
                            try
                            {
                                var maxnotificationid = db.AlertNotifications.Max(c => c.id);
                                AlertNotificationReadStatu Alertread = new AlertNotificationReadStatu();
                                Alertread.NotificationId = maxnotificationid;
                                Alertread.IsRead = false;
                                Alertread.UserId = null;
                                Alertread.DeviceId = null;
                                db.AlertNotificationReadStatus.AddObject(Alertread);
                                db.SaveChanges();

                                string str = Path.Combine(Server.MapPath("~/NotificationImage/"), Imagename);
                                postedFile.SaveAs(str);

                                if (userupload_file == null)
                                {
                                    Alls = Alls ?? "off";
                                    DealCheck = DealCheck ?? "off";
                                    DistriCheck = DistriCheck ?? "off";
                                    if (Alls.ToLower() != "on")
                                    {
                                        if (rglist != "" && rglist != null)
                                        {
                                            Regex rg = new Regex(",");
                                            string[] reglist = rg.Split(rglist);
                                            foreach (string s in reglist)
                                            {
                                                AlertNotificationAccessTable pat = new AlertNotificationAccessTable();
                                                pat.notificationid = _notifications.id;
                                                pat.RegId = int.Parse(s);
                                                pat.createdate = DateTime.Now;
                                                pat.createby = Session["userid"].ToString();
                                                pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                                pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                                db.AlertNotificationAccessTables.AddObject(pat);
                                                db.SaveChanges();

                                            }
                                        }
                                    }
                                    else
                                    {
                                        AlertNotificationAccessTable pat = new AlertNotificationAccessTable();
                                        pat.notificationid = _notifications.id;
                                        pat.AllAcess = true;
                                        db.AlertNotificationAccessTables.AddObject(pat);
                                        db.SaveChanges();
                                    }

                                    if ((Alls.ToLower() != "on") && (rglist == "" || rglist == null) && (DistriCheck != "on" || DealCheck != "on"))
                                    {
                                        AlertNotificationAccessTable pat = new AlertNotificationAccessTable();
                                        pat.notificationid = _notifications.id;
                                        pat.RegId = null;
                                        pat.createdate = DateTime.Now;
                                        pat.createby = Session["userid"].ToString();
                                        pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                        pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                        db.AlertNotificationAccessTables.AddObject(pat);
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

                                                AlertNotificationAccessTable pat = new AlertNotificationAccessTable();
                                                pat.notificationid = _notifications.id;
                                                pat.RegId = null;
                                                pat.createdate = DateTime.Now;
                                                pat.createby = Session["userid"].ToString();
                                                pat.AllDealerAccess =false;
                                                pat.AllDestriAccess =false;
                                                if (usertype.CustomerType == "DISTY")
                                                {
                                                    pat.SpecificDealerAccess = "0";
                                                    pat.SpecificDestriAccess = usertype.UserId;
                                                }
                                                if (usertype.CustomerType == "Dealer")
                                                {
                                                    pat.SpecificDealerAccess = usertype.UserId;
                                                    pat.SpecificDestriAccess = "0";
                                                }
                                                db.AlertNotificationAccessTables.AddObject(pat);
                                                db.SaveChanges();

                                            }
                                        }
                                    }

                                }

                                return Content("<script>alert('Notification Saved Successfully');location.href='../AlertNotification/AlertNotification';</script>");
                            }
                            catch (Exception ex)
                            {
                                Exception exs = ex.InnerException;
                                return Content("<script>alert('" + ex.Message.ToString() + "');location.href='../AlertNotification/AlertNotification';</script>");
                            }


                        }


                    }
                    return View("AlertNotification");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        //[NonAction]
        //public string PushNotifioncationToAndroid(int NotificationID, string NotificationSubject)
        //{
        //    List<userIdForPushNotification_Result> useridList = db.userIdForPushNotification(NotificationID).ToList();
        //    string FailedToSendTo = "";
        //    if (useridList.Count > 0)
        //    {
        //        foreach (var i in useridList)
        //        {
        //            try
        //            {
        //                if (i.DeviceId == null || i.DeviceId == "")
        //                {
        //                    FailedToSendTo += i.userid + ", ";
        //                }
        //                else
        //                {
        //                    string deviceId = i.DeviceId;
        //                    string message = NotificationSubject;
        //                    string tickerText = "example test2 GCM";
        //                    string contentTitle = "content title GCM";
        //                    string postData =
        //                    "{ \"registration_ids\": [ \"" + deviceId + "\" ], " +
        //                      "\"data\": {\"tickerText\":\"" + tickerText + "\", " +
        //                                 "\"contentTitle\":\"" + contentTitle + "\", " +
        //                                 "\"message\": \"" + message + "\"}}";

        //                    SendGCMNotification("AIzaSyAvSfXr_9BA3meEWxJFpNR-kR_U9KRMzQo", postData);
        //                }
        //            }
        //            catch
        //            {
        //                FailedToSendTo += i.userid + ", ";
        //            }

        //        }
        //        if (FailedToSendTo == "")
        //        {
        //            return "";
        //        }
        //        else
        //        {
        //            FailedToSendTo = FailedToSendTo.Remove(FailedToSendTo.Length - 1, 1);
        //            return "and Unable To Send Notifications To " + FailedToSendTo;
        //        }

        //    }
        //    else
        //    {
        //        return "There Is No User In List To Send The Notifications";
        //    }

        //}
        //[NonAction]
        //private string SendGCMNotification(string apiKey, string postData, string postDataContentType = "application/json")
        //{

        //    //  MESSAGE CONTENT
        //    byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        //    //
        //    //  CREATE REQUEST
        //    HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
        //    Request.Method = "POST";
        //    Request.KeepAlive = false;
        //    Request.ContentType = postDataContentType;
        //    Request.Headers.Add(string.Format("Authorization: key={0}", apiKey));
        //    Request.ContentLength = byteArray.Length;
        //    //Request.GetRequestStream();
        //    Stream dataStream = Request.GetRequestStream();
        //    dataStream.Write(byteArray, 0, byteArray.Length);
        //    dataStream.Close();

        //    //
        //    //  SEND MESSAGE
        //    try
        //    {
        //        WebResponse Response = Request.GetResponse();
        //        HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
        //        if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
        //        {
        //            var text = "Unauthorized - need new token";
        //        }
        //        else if (!ResponseCode.Equals(HttpStatusCode.OK))
        //        {
        //            var text = "Response from web service isn't OK";
        //        }

        //        StreamReader Reader = new StreamReader(Response.GetResponseStream());
        //        string responseLine = Reader.ReadToEnd();
        //        Reader.Close();

        //        return responseLine;
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //    return "error";
        //}
        public JsonResult GetNotificationsDetail(int? page)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    var Notificationdetails = (from c in db.AlertNotifications
                                               where c.C_Status != 2
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
                                                    Imagename = c.Imagename,
                                                    ImagePath = c.Imagepath,
                                                    Text = c.C_text,
                                                    status = c.C_Status == 1 ? "Active" : "Deactive",
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
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    AlertNotification cud = db.AlertNotifications.Single(a => a.id == id);
                    ViewBag.status = cud.C_Status;

                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        public ActionResult Update(AlertNotification _notifications, string statusC, HttpPostedFileBase postedFile)
        {

            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {


                    //#region Text
                    //if (_notifications.C_text == null)
                    //{
                    //    ModelState.AddModelError("C_text", "Subject Has No Value");
                    //}
                    //else
                    //{
                    //    if (_notifications.C_text == "")
                    //    {
                    //        ModelState.AddModelError("C_text", "Subject Has No Value");
                    //    }
                    //    if (_notifications.C_text.Length > 249)
                    //    {
                    //        ModelState.AddModelError("C_text", "Character Should Be Less Than 100");
                    //    }
                    //}
                    //#endregion
                    if (ModelState.IsValid)
                    {
                        // int intid = int.Parse(id);
                        if (postedFile == null)
                        {
                            try
                            {
                                AlertNotification not = db.AlertNotifications.Single(a => a.id == _notifications.id && a.C_Status != 2);
                                AlertNotifications_History notHisotry = new AlertNotifications_History();
                                notHisotry.NotificationId = not.id;
                                notHisotry.C_text = not.C_text;
                                notHisotry.C_Status = not.C_Status;
                                notHisotry.Imagename = not.Imagename;
                                notHisotry.Imagepath = not.Imagepath;
                                notHisotry.ModifyBy = Session["userid"].ToString();
                                notHisotry.ModifyOn = DateTime.Now;

                                db.AlertNotifications_History.AddObject(notHisotry);
                                not.C_text = _notifications.C_text;
                                not.C_Status = _notifications.C_Status;


                                not.ModifyBy = Session["userid"].ToString();
                                not.ModifyOn = DateTime.Now;
                                if (statusC.ToLower() == "on")
                                {
                                    not.C_Status = 1;

                                }
                                else
                                {
                                    not.C_Status = 1;
                                }
                                int affectedRows = db.SaveChanges();
                                if (affectedRows > 0)
                                {

                                    ViewBag.Update = "Done";

                                }
                            }
                            catch
                            {
                                return View("Notification");
                            }
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
                                    AlertNotification Notfica = db.AlertNotifications.Single(a => a.id == _notifications.id && a.C_Status != 2);

                                    //Save Previous Record In History
                                    AlertNotifications_History NotHisotry = new AlertNotifications_History();
                                    NotHisotry.NotificationId = Notfica.id;
                                    NotHisotry.C_text = Notfica.C_text;
                                    NotHisotry.C_Status = Notfica.C_Status;
                                    NotHisotry.Imagename = Notfica.Imagename;
                                    NotHisotry.Imagepath = Notfica.Imagepath;

                                    NotHisotry.ModifyBy = Session["userid"].ToString();
                                    NotHisotry.ModifyOn = DateTime.Now;

                                    db.AlertNotifications_History.AddObject(NotHisotry);


                                    //Save New Record
                                    // Notfica.C_Status = Notfica.C_Status;
                                    Notfica.C_text = _notifications.C_text;
                                    Notfica.Imagename = postedFile.FileName;
                                    Notfica.Imagepath = Imagename;

                                    Notfica.ModifyBy = Session["userid"].ToString();
                                    Notfica.ModifyOn = DateTime.Now;
                                    if (statusC.ToLower() == "on")
                                    {
                                        Notfica.C_Status = 1;

                                    }
                                    else
                                    {
                                        Notfica.C_Status = 0;
                                    }

                                    if (db.SaveChanges() > 0)
                                    {

                                        ViewBag.Update = "Done";
                                        string str = Path.Combine(Server.MapPath("~/NotificationImage/"), Imagename);
                                        postedFile.SaveAs(str);
                                    }
                                    else
                                    {
                                        return Content("<script>alert('Record Has Not Been Saved');</script>");
                                    }


                                }
                                catch
                                {
                                    return View("Notification");
                                }
                            }
                        }
                        Notification notification = db.Notifications.Single(a => a.id == _notifications.id);
                        //NotificationsHistory notiHistory = new NotificationsHistory();
                        //notiHistory.C_status = notification.C_status;
                        //notiHistory.C_Subject = notification.C_Subject;
                        //notiHistory.C_text = notification.C_text;
                        //notiHistory.NotificationId = notification.id;
                        //db.NotificationsHistories.AddObject(notiHistory);

                        //string status = statusC ?? "off";
                        //if (status == "on")
                        //{
                        //    notification.C_status = 1;
                        //}
                        //else
                        //{
                        //    notification.C_status = 0;
                        //}

                        //notification.AlertFlag = 0;

                        //notification.C_Subject = _notifications.C_Subject;
                        //notification.C_text = _notifications.C_text;
                        //notification.ModifiedBy = Session["userid"].ToString();
                        //notification.ModifiedDate = DateTime.Now;
                        //db.SaveChanges();
                        return Content("<script>alert('Notification Updated Successfully');location.href='../AlertNotification/AlertNotification';</script>");
                    }
                    return View("Edit");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        public ActionResult EditReciverList(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    // List<NotificationAccessTable> Pat = db.NotificationAccessTables.Where(a => a.notificationid == id && (a.deleted != true || a.deleted == null || a.deleted == false)).ToList();
                    List<GetAlertNotificationAccessTable_Result> Pat = (from c in db.GetAlertNotificationAccessTable(id)
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
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
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
                        List<AlertNotificationAccessTable> patDel = db.AlertNotificationAccessTables.Where(a => a.notificationid == id).ToList();
                        foreach (var i in patDel)
                        {
                            db.DeleteObject(i);
                        }

                        if (Alls.ToLower() != "on")
                        {
                            if (rglist != "" && rglist != null)
                            {
                                Regex rg = new Regex(",");
                                string[] reglist = rg.Split(rglist);
                                foreach (string s in reglist)
                                {
                                    AlertNotificationAccessTable pat = new AlertNotificationAccessTable();
                                    pat.notificationid = id;
                                    pat.RegId = int.Parse(s);
                                    pat.createdate = DateTime.Now;
                                    pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                    pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                    pat.createby = Session["userid"].ToString();
                                    db.AlertNotificationAccessTables.AddObject(pat);
                                    db.SaveChanges();

                                }
                            }
                        }
                        else
                        {
                            AlertNotificationAccessTable pat = new AlertNotificationAccessTable();
                            pat.notificationid = id;
                            pat.AllAcess = true;
                            db.AlertNotificationAccessTables.AddObject(pat);
                            db.SaveChanges();
                        }

                        if ((Alls.ToLower() != "on") && (rglist == "" || rglist == null) && (DistriCheck != "on" || DealCheck != "on"))
                        {
                            AlertNotificationAccessTable pat = new AlertNotificationAccessTable();
                            pat.notificationid = id;
                            pat.RegId = null;
                            pat.createdate = DateTime.Now;
                            pat.createby = Session["userid"].ToString();
                            pat.AllDealerAccess = DealCheck == "off" ? false : true;
                            pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                            db.AlertNotificationAccessTables.AddObject(pat);
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
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["deleterole"].ToString() == "1")
                {
                    AlertNotification notification = db.AlertNotifications.Single(a => a.id == id);
                    AlertNotifications_History notiHistory = new AlertNotifications_History();
                    notiHistory.C_Status = notification.C_Status;

                    notiHistory.C_text = notification.C_text;
                    notiHistory.NotificationId = notification.id;
                    db.AlertNotifications_History.AddObject(notiHistory);


                    notification.C_Status = 2;
                    notification.ModifyBy = Session["userid"].ToString();
                    notification.ModifyOn = DateTime.Now;

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

    }
}