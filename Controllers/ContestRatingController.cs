using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.IO;
using PagedList;
using PagedList.Mvc;
using System.Data;
using System.Web.Routing;
using System.Web.UI;
namespace Luminous.Controllers
{
    public class ContestRatingController : Controller
    {
        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();
        public ActionResult Index(string Error)
        {
           

           

            if (Error != null)
            {

                ViewBag.ERR = "No Data Available For Rating";
            }

            RouteCollection rc = RouteTable.Routes;
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/ContestRating/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                   // var dd = db.SaveDelContestImages.OrderByDescending(a => a.Id).ToList();
                    DealerContest dc = new DealerContest();

    

                    dc.Dcontest = (from ic in db.SaveDelContestImages
                                   join urs in db.UsersLists
                                   on ic.DealerCode equals urs.UserId
                                   join marquee in db.ShowMarquee_Text
                                   on ic.Marqueeid equals marquee.Id
                                   join cm in db.ContestMasters
                                   on ic.Contestid equals cm.Id
                                   select new DealerContest
                                   {
                                       id = ic.Id,
                                       DealerCode = ic.DealerCode,
                                       DealerName = urs.Dis_Name,
                                       DealerPhone = urs.Dis_ContactNo,
                                       Dis_Code = ic.DistributorCode,
                                       Rating = ic.Rating,
                                       eventid = marquee.Id,
                                       eventname = marquee.Marqueetext,
                                       Contest = cm.ContestName,
                                       CreatedOn = ic.CreatedOn,
                                       CreatedBy = urs.Dis_Name
                                   }).GroupBy(elem => new { elem.DealerCode, elem.eventid }).Select(group => group.FirstOrDefault()).ToList();

                    return View(dc.Dcontest.OrderByDescending(a => a.id).ToList());
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        //public List<SelectListItem> Loadstate()
        //{
        //    var myResults = (from data in db.allstates
        //                     orderby data.statename
        //                     select new
        //                     {
        //                         Text = data.statename,
        //                         Value = data.id

        //                     }
        //  ).ToList();


        //    List<SelectListItem> li = new List<SelectListItem>();
        //    li.Add(new SelectListItem { Text = "Select", Value = "0" });
        //    foreach (var dd in myResults)
        //    {
        //        li.Add(new SelectListItem { Text = dd.Text, Value = Convert.ToString(dd.Value) });

        //    }


        //    ViewData["country"] = li;

        //    return li;
        //}
        //public ActionResult Save(string ParentCatid, string DealerName, string DealerCity, string stateName, string DealerPhone, string DealerEmail, string DistributorCode, string Status, string EmpCode, string FseName, string DealerFirmName, HttpPostedFileBase postedFile)
        //{
        //    if (Session["userid"] == null)
        //    {
        //        return RedirectToAction("login", "login");
        //    }
        //    else
        //    {
        //        //dt = Session["permission"] as DataTable;
        //        //string pageUrl2 = "/Banner/Index";
        //        //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
        //        // if (result[0]["createrole"].ToString() == "1")
        //        // {

        //        Status = Status ?? "off";
        //        #region Check Validation

        //        #region Dealer Details Validations

        //        //if (DealerCity == null)
        //        //{
        //        //    ModelState.AddModelError("DealerCity", "Dealer City Is Required");
        //        //}
        //        //else
        //        //{
        //        //    if (DealerCity == "")
        //        //    {
        //        //        ModelState.AddModelError("DealerCity", "Dealer City Is Required");
        //        //    }


        //        //}
        //        if (Session["userid"].ToString() != null && Session["userid"].ToString() != "")
        //        {
        //            string SAPuserid = Session["userid"].ToString();
        //            var parentcatexist = db.InsertContexts.Where(c => c.ParentCategory == ParentCatid && c.DealerId == SAPuserid).ToList();
        //            if (parentcatexist.Count > 0)
        //            {
        //                if (Session["Dis_DelName"] != null && Session["SapCode"] != null)
        //                {
        //                    ViewBag.Username = Session["Dis_DelName"];
        //                    ViewBag.Userid = Session["SapCode"];
        //                }

        //                ModelState.AddModelError("ParentCategory", "Contest Already Created Under This Parent Category.");
        //            }
        //        }
        //        else
        //        {
        //            if (Session["ASMEmpCode"].ToString() != null && Session["mobileno"].ToString() != "")
        //            {
        //                string Asmempcode = Session["mobileno"].ToString();
        //                var parentcatexist = db.InsertContexts.Where(c => c.ParentCategory == ParentCatid && c.FSE_DealerMobileNo == Asmempcode).ToList();
        //                if (parentcatexist.Count > 0)
        //                {
        //                    if (Session["Dis_DelName"] != null && Session["SapCode"] != null)
        //                    {
        //                        ViewBag.Username = Session["Dis_DelName"];
        //                        ViewBag.Userid = Session["SapCode"];
        //                    }

        //                    ModelState.AddModelError("ParentCategory", "Cannot Create Contest With Same Parent Category");
        //                }
        //            }
        //        }
        //        if (DealerPhone == null)
        //        {

        //            ModelState.AddModelError("DealerPhone", "Dealer Phone Is Required");
        //        }
        //        else
        //        {
        //            if (DealerPhone == "")
        //            {
        //                ModelState.AddModelError("DealerPhone", "Dealer Phone Is Required");
        //            }


        //        }

        //        if (Session["ASMEmpCode"] != null)
        //        {
        //            if (DealerFirmName == null)
        //            {
        //                ModelState.AddModelError("DealerFirmName", "Dealer Firm Name Is Required");
        //            }
        //            else
        //            {
        //                if (DealerFirmName == "")
        //                {
        //                    ModelState.AddModelError("DealerFirmName", "Dealer Firm Name Is Required");
        //                }


        //            }
        //        }

        //        if (ParentCatid == null || ParentCatid == "0")
        //        {
        //            ModelState.AddModelError("ParentCategory", "Parent Category Is Required");
        //        }
        //        else
        //        {
        //            if (DealerPhone == "")
        //            {
        //                ModelState.AddModelError("ParentCategory", "Parent Category Is Required");
        //            }


        //        }
        //        if (stateName == "0")
        //        {
        //            ModelState.AddModelError("DealerState", "Please select State");
        //        }

        //        if (FseName == null)
        //        {
        //            ModelState.AddModelError("FseName", "Emp/FSE Name Is Required");
        //        }
        //        else
        //        {
        //            if (FseName == "")
        //            {
        //                ModelState.AddModelError("FseName", "Emp/FSE Name Email Is Required");
        //            }


        //        }
        //        //if (DistributorCode == null)
        //        //{
        //        //    ModelState.AddModelError("DistributorCode", "Distributor Code Is Required");
        //        //}
        //        //else
        //        //{
        //        //    if (DistributorCode == "")
        //        //    {
        //        //        ModelState.AddModelError("DistributorCode", "Distributor Code Is Required");
        //        //    }


        //        //}
        //        if (EmpCode == null)
        //        {
        //            ModelState.AddModelError("Emp/FSE Code", "Emp Code Is Required");
        //        }
        //        else
        //        {
        //            if (EmpCode == "")
        //            {
        //                ModelState.AddModelError("Emp/FSE Code", "Emp Code Is Required");
        //            }


        //        }
        //        if (FseName == null)
        //        {
        //            ModelState.AddModelError("FseName", "EMP/FSE Name Is Required");
        //        }
        //        else
        //        {
        //            if (EmpCode == "")
        //            {
        //                ModelState.AddModelError("EmpCode", "Emp Code Is Required");
        //            }


        //        }
        //        #endregion

        //        if (postedFile == null)
        //        {
        //            ModelState.AddModelError("File", "*");
        //            ViewBag.File = "File Is Not Uploaded";
        //        }
        //        else
        //        {
        //            string FileExtension = Path.GetExtension(postedFile.FileName);
        //            if (FileExtension.ToLower() == ".jpeg" || FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".tiff" || FileExtension.ToLower() == ".gif" ||
        //               FileExtension.ToLower() == ".png")
        //            {

        //            }
        //            else
        //            {
        //                ModelState.AddModelError("File", "*");
        //                ViewBag.File = "File Extention Should Be In .Jpeg, .Tiff, .Gif, .Png, .jpg";
        //            }



        //        }
        //        #endregion


        //        if (ModelState.IsValid)
        //        {
        //            string filename = Path.GetFileNameWithoutExtension(postedFile.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(postedFile.FileName);
        //            string Imagename = filename.Replace(" ", string.Empty);
        //            InsertContext contest = new InsertContext();
        //            contest.DealerName = DealerName;
        //            contest.DealerCity = DealerCity;
        //            contest.DealerPhone = DealerPhone;
        //            contest.DealerEmail = DealerEmail;
        //            contest.DistributorCode = DistributorCode;
        //            contest.EmpCode = EmpCode;

        //            contest.ImageEncode = Imagename;
        //            contest.FseName = FseName;
        //            contest.DealerState = stateName;
        //            contest.ParentCategory = ParentCatid;
        //            contest.CreatedOn = DateTime.Now;


        //            if (Session["ASMEmpCode"] != null)
        //            {
        //                contest.DealerFirmName = DealerFirmName;
        //                contest.DealerId = "";
        //                contest.ASMEmpCode = Session["ASMEmpCode"].ToString();
        //                contest.CreatedBy = Session["ASMEmpCode"].ToString();
        //                contest.FSE_DealerMobileNo = Session["mobileno"].ToString();
        //            }
        //            else
        //            {
        //                contest.ASMEmpCode = "";
        //                contest.FSE_DealerMobileNo = "";
        //                contest.DealerId = Session["userid"].ToString();
        //                contest.CreatedBy = Session["userid"].ToString();
        //            }
        //            //contest.CreatedBy = Session["SapCode"].ToString();

        //            if (Status.ToLower() == "on")
        //            {
        //                contest.Status = 1;

        //            }
        //            else
        //            {
        //                contest.Status = 0;
        //            }
        //            db.InsertContexts.AddObject(contest);
        //            int a = db.SaveChanges();
        //            if (a > 0)
        //            {
        //                string str = Path.Combine(Server.MapPath("~/ProfileImages/"), Imagename);
        //                postedFile.SaveAs(str);
        //                ViewBag.SaveStatus = "Record Saved Successfully";
        //            }

        //        }

        //        return View("Index", db.InsertContexts.ToList().ToPagedList(1, 5));
        //        // }
        //        //else
        //        //{
        //        //    return RedirectToAction("snotallowed", "snotallowed");

        //        //}
        //    }
        //}


        public JsonResult GetContactDetail(int? page)
        {
            if (Session["userid"] == null)
            {
                return Json("login");
            }
            else
            {

                string createdby = Session["userid"].ToString();
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = "/Createcontest/Index";
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                //if (result[0]["uview"].ToString() == "1")
                //{

                int totalrecord;
                if (page != null)
                {
                    page = (page - 1) * 15;
                }



                var contestdetails = (from c in db.SaveDelContestImages
                                      where c.Status != 2 

                                      select c).ToList();
                var Contestdetails2 = (from c in contestdetails
                                       join urs in db.UsersLists on c.DealerCode equals urs.UserId.ToString()
                                       
                                      
                                       select new
                                       {
                                           DealerCode=c.DealerCode,
                                           DealerName = urs.Dis_Name,
                                           DistributorCode = c.DistributorCode,
                                           status = c.Status == 1 ? "Enable" : "Disable",
                                           DealerPhone = urs.Dis_ContactNo,
                                           Id = c.Id,
                                           CreatedOn = Convert.ToDateTime(c.CreatedOn).ToString("yyyy-MM-ddTHH:mm:ss"),
                                           createddate = Convert.ToDateTime(c.CreatedOn).ToShortDateString(),
                                       

                                       }).OrderByDescending(c => c.Id).Skip(page ?? 0).Take(15).ToList();
                   
                    if (contestdetails.Count() % 15 == 0)
                    {
                        totalrecord = contestdetails.Count() / 15;
                    }
                    else
                    {
                        totalrecord = (contestdetails.Count() / 15) + 1;
                    }
                    var data = new { result = Contestdetails2, TotalRecord = totalrecord };

                    return Json(data, JsonRequestBehavior.AllowGet);
             


                // }
                //else
                //{
                //    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                //}
            }
        }

        public ActionResult Edit(string Type, string NOdata, int? id = 0, int? Marquid = 0)
        {
            int imgcount = 0;
            //if (Contestid != 0)
            //{
            //    //var nextImage = db.InsertContexts.Where(i => i.Id > Contestid).OrderBy(i => i.Id).First();
            //    var nextImage = db.InsertContexts.Where(i => i.Id > Contestid).OrderBy(i => i.Id).First();
            //    id = nextImage.Id;
            //}
            if (NOdata != null)
            {
                //ModelState.AddModelError(string.Empty, "Access for this program already exists.");

                return RedirectToAction("Index", new { Error = "Error" });
            }
          
          //  SaveDelContestImage insertcontext = db.SaveDelContestImages.Single(i => i.Id == id);
            if (Session["marqueeid"] != null)
            {
                Marquid = Convert.ToInt32(Session["marqueeid"]);
            }
            DealerContest dc = new DealerContest();
            var getdealercode = db.SaveDelContestImages.Where(c => c.Id == id).Select(c => c.DealerCode).SingleOrDefault();
            var countdata = db.SaveDelContestImages.Where(c => c.DealerCode == getdealercode && c.Marqueeid == Marquid).ToList() ;
            if (countdata.Count == 2)
            {
                Session["imgcount"] = "2";
               
              
                var imagedata = db.SaveDelContestImages.Where(c => c.DealerCode == getdealercode && c.Marqueeid == Marquid).ToList();
                List<DealerContest> dcontest = new List<DealerContest>();
                foreach (var img in imagedata)
                {
                    DealerContest dc1 = new DealerContest();
                    if (imgcount == 1)
                    {
                        TempData["image2"] = img.ImageName;
                    }
                  
                 
                 
                    imgcount++;
                }
            }
            else
            {
                Session["imgcount"] = "1";
            }
            dc.Dcontest = (from ic in db.SaveDelContestImages
                           join urs in db.UsersLists
                           on ic.DealerCode equals urs.UserId
                           join marquee in db.ShowMarquee_Text
                           on ic.Marqueeid equals marquee.Id
                           join savenotsurvey in db.SaveNotificationSurveys on   ic.DealerCode equals savenotsurvey.CreatedBy
                            into ps
                           from savenotsurvey in ps.DefaultIfEmpty()
                           join notsurvey in db.NotificationSurveys on savenotsurvey.SurveyID equals notsurvey.SurveyID
                            into psq
                           from notsurvey in psq.DefaultIfEmpty()
                           where ic.Id==id && ic.Contestid==2
                           select new DealerContest
                           {
                               id = ic.Id,
                               DealerCode = ic.DealerCode,
                               image=ic.ImageName,
                               QuestionName=notsurvey.QuestionTitle,
                               CorrectAns=savenotsurvey.OptionValue,
                               DealerName = urs.Dis_Name,
                               DealerPhone = urs.Dis_ContactNo,
                               Dis_Code = ic.DistributorCode,
                               eventname = marquee.Marqueetext,
                               eventid=marquee.Id,
                               CreatedOn = ic.CreatedOn,
                               CreatedBy = urs.Dis_Name,
                               Rating=ic.Rating
                           }).ToList();

            if(dc.Dcontest.FirstOrDefault().QuestionName==null)
            {
                ViewBag.questionname = "Questionhide";
            }

            if (dc.Dcontest == null)
            {
                return HttpNotFound();
            }
            return View(dc.Dcontest);
        }
        public string GetNextImage(int Id, string Rating, string DealerName, string DealerCode, string DistributorCode, string CreatedBy, string CreatedOn, int Marqueeid)
        {
            try
            {
               
                String RatingBy = Session["Id"].ToString();
                if (Session["imgcount"].ToString() == "2")
                {
                    db.UpdateInsertContextRating_Contest(Id, Rating, DealerCode, DealerName, DistributorCode, CreatedBy, RatingBy,Marqueeid);
                  //  db.ExecuteStoreCommand("Update SaveDelContestImages set Rating='" + Rating + "',RatingBy='" + RatingBy + "',RatingOn=" + DateTime.Now.ToShortDateString() + " where Dealercode='"+DealerCode+"' and Marqueeid='"+Marqueeid+"'");
                }
                else
                {
                    db.UpdateInsertContextRating_Contest(Id, Rating, DealerCode, DealerName, DistributorCode, CreatedBy, RatingBy,Marqueeid);
                }
                
                var nextImage = db.SaveDelContestImages.Where(i => i.Id > Id && i.Rating == null || i.Rating == "").OrderBy(i => i.Id).First();
                var imgnext = nextImage.Id.ToString();
                var marqiddata = nextImage.Marqueeid;
                Session["marqueeid"] = marqiddata;
                return imgnext;
            }
            catch (Exception exc)
            {
                Session["marqueeid"] = null;
                return "No Data";
            }
        }
        public string Update(int Id, string Rating, string DealerName, string DealerCode, string DistributorCode, string CreatedBy, string CreatedOn, int Marqueeid)
        {
            try
            {
                if (Id != 0 && Rating != null)
                {
                    //db.ExecuteStoreCommand("Update InsertContext set Rating='" + Rating + "' where Id='" + Id + "'");
                    //db.ExecuteStoreCommand("Insert into Insertcontexthistory(ImageEncode,Rating,DealerName,DealerCity,DealerPhone,DealerEmail,DistributorCode,EmpCode,DealerId,CreatedBy,CreatedOn) values('" + ImageEncode + "','" + Rating + "','" + DealerName + "','" + DealerCity + "','" + DealerPhone + "','" + DealerEmail + "','" + DistributorCode + "','" + EmpCode + "','" + DealerId + "','" + CreatedBy + "','" + CreatedOn + "')");
                    //db.SaveChanges();
                    String RatingBy = Session["Id"].ToString();
                    if (Session["imgcount"].ToString() == "2")
                    {
                        db.ExecuteStoreCommand("Update SaveDelContestImages set Rating='" + Rating + "',RatingBy='" + RatingBy + "',RatingOn='" + DateTime.Now + "' where Dealercode='" + DealerCode + "' and Marqueeid='" + Marqueeid + "'");
                    }
                    else
                    {
                        db.UpdateInsertContextRating_Contest(Id, Rating, DealerCode, DealerName, DistributorCode, CreatedBy, RatingBy,Marqueeid);
                    }
                    return "Point Allocation Update Successfully";
                }
                else
                {
                    return "Point Allocation Not Updated";
                }
            }
            catch (Exception exc)
            {
                return "Some exception has occurred";
            }


        }
        public ActionResult GetDealerData()
        {
            //var DealerId = db.InsertContexts.Where(r=>r.Rating==null).Min(r =>r.Id);


            //return RedirectToAction("Edit", new {id=DealerId});
            var DelID = db.SaveDelContestImages.Where(r => r.Rating == null || r.Rating == "").ToList();
            if (DelID.Count == 0)
            {
                return RedirectToAction("Edit", new { NOdata = "Nodata" });
            }
            else
            {
                var Dealcode = DelID.Min(r => r.DealerCode);
                var DealerId = DelID.Min(r => r.Id);
                var marq = DelID.Min(r => r.Marqueeid);
                return RedirectToAction("Edit", new { id = DealerId,Marquid=marq });
            }
        }

        public ActionResult ExportToExcel()
        {
            var datacontest = db.ExecuteStoreQuery<Contestexceldata>("select sd.DealerCode,urs.Dis_Name as DealerName,sd.DistributorCode,sd.CreatedOn,sd.CreatedBy,sd.Marqueeid as EventNumber,sd.Rating from SaveDelContestImages sd join UsersList urs on urs.UserId=sd.DealerCode");

            var DealerData = datacontest.ToList();
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = DealerData;
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachement; filename=dealerContest" + DateTime.Now + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }


    }
}
