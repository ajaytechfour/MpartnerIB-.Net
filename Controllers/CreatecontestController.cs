using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.IO;
using PagedList;
using PagedList.Mvc;
using System.Data;
using System.Web.Routing;
namespace LuminousMpartnerIB.Controllers
{
    public class CreatecontestController : Controller
    {
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        public ActionResult Index()
        {

            RouteCollection rc = RouteTable.Routes;
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/CreateContest/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    if (Session["Dis_DelName"] != null && Session["SapCode"] != null && Session["mobileno"] == null)
                    {
                        ViewBag.Username = Session["Dis_DelName"];
                        ViewBag.Userid = Session["SapCode"];
                    }
                    else if (Session["mobileno"] != null)
                    {

                        ViewBag.Userid = "";
                        ViewBag.Mobileno = Session["mobileno"];
                    }


                    return View(db.InsertContexts.Where(a => a.Id == null).OrderByDescending(a => a.Id).ToList().ToPagedList(1, 1));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        public List<SelectListItem> Loadstate()
        {
            var myResults = (from data in db.allstates
                             orderby data.statename
                             select new
                             {
                                 Text = data.statename,
                                 Value = data.id

                             }
          ).ToList();


            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var dd in myResults)
            {
                li.Add(new SelectListItem { Text = dd.Text, Value = Convert.ToString(dd.Value) });

            }


            ViewData["country"] = li;

            return li;
        }
        public ActionResult Save(string ParentCatid, string DealerName, string DealerCity, string stateName, string DealerPhone, string DealerEmail, string DistributorCode, string Status, string EmpCode, string FseName, string DealerFirmName, HttpPostedFileBase postedFile)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = "/Banner/Index";
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                // if (result[0]["createrole"].ToString() == "1")
                // {

                Status = Status ?? "off";
                #region Check Validation

                #region Dealer Details Validations

                //if (DealerCity == null)
                //{
                //    ModelState.AddModelError("DealerCity", "Dealer City Is Required");
                //}
                //else
                //{
                //    if (DealerCity == "")
                //    {
                //        ModelState.AddModelError("DealerCity", "Dealer City Is Required");
                //    }


                //}
                if (Session["userid"].ToString() != null && Session["userid"].ToString() != "")
                {
                    string SAPuserid = Session["userid"].ToString();
                    var parentcatexist = db.InsertContexts.Where(c => c.ParentCategory == ParentCatid && c.DealerId == SAPuserid).ToList();
                    if (parentcatexist.Count > 0)
                    {
                        if (Session["Dis_DelName"] != null && Session["SapCode"] != null)
                        {
                            ViewBag.Username = Session["Dis_DelName"];
                            ViewBag.Userid = Session["SapCode"];
                        }

                        ModelState.AddModelError("ParentCategory", "Contest Already Created Under This Parent Category.");
                    }
                }
                else
                {
                    if (Session["ASMEmpCode"].ToString() != null && Session["mobileno"].ToString() != "")
                    {
                        string Asmempcode = Session["mobileno"].ToString();
                        var parentcatexist = db.InsertContexts.Where(c => c.ParentCategory == ParentCatid && c.FSE_DealerMobileNo == Asmempcode).ToList();
                        if (parentcatexist.Count > 0)
                        {
                            if (Session["Dis_DelName"] != null && Session["SapCode"] != null)
                            {
                                ViewBag.Username = Session["Dis_DelName"];
                                ViewBag.Userid = Session["SapCode"];
                            }

                            ModelState.AddModelError("ParentCategory", "Cannot Create Contest With Same Parent Category");
                        }
                    }
                }
                if (DealerPhone == null)
                {

                    ModelState.AddModelError("DealerPhone", "Dealer Phone Is Required");
                }
                else
                {
                    if (DealerPhone == "")
                    {
                        ModelState.AddModelError("DealerPhone", "Dealer Phone Is Required");
                    }


                }

                if (Session["ASMEmpCode"] != null)
                {
                    if (DealerFirmName == null)
                    {
                        ModelState.AddModelError("DealerFirmName", "Dealer Firm Name Is Required");
                    }
                    else
                    {
                        if (DealerFirmName == "")
                        {
                            ModelState.AddModelError("DealerFirmName", "Dealer Firm Name Is Required");
                        }


                    }
                }

                if (ParentCatid == null || ParentCatid=="0")
                {
                    ModelState.AddModelError("ParentCategory", "Parent Category Is Required");
                }
                else
                {
                    if (DealerPhone == "")
                    {
                        ModelState.AddModelError("ParentCategory", "Parent Category Is Required");
                    }


                }
                if (stateName == "0")
                {
                    ModelState.AddModelError("DealerState", "Please select State");
                }

                if (FseName == null)
                {
                    ModelState.AddModelError("FseName", "Emp/FSE Name Is Required");
                }
                else
                {
                    if (FseName == "")
                    {
                        ModelState.AddModelError("FseName", "Emp/FSE Name Email Is Required");
                    }


                }
                //if (DistributorCode == null)
                //{
                //    ModelState.AddModelError("DistributorCode", "Distributor Code Is Required");
                //}
                //else
                //{
                //    if (DistributorCode == "")
                //    {
                //        ModelState.AddModelError("DistributorCode", "Distributor Code Is Required");
                //    }


                //}
                if (EmpCode == null)
                {
                    ModelState.AddModelError("Emp/FSE Code", "Emp Code Is Required");
                }
                else
                {
                    if (EmpCode == "")
                    {
                        ModelState.AddModelError("Emp/FSE Code", "Emp Code Is Required");
                    }


                }
                if (FseName == null)
                {
                    ModelState.AddModelError("FseName", "EMP/FSE Name Is Required");
                }
                else
                {
                    if (EmpCode == "")
                    {
                        ModelState.AddModelError("EmpCode", "Emp Code Is Required");
                    }


                }
                #endregion

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
                    string filename = Path.GetFileNameWithoutExtension(postedFile.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(postedFile.FileName);
                    string Imagename = filename.Replace(" ", string.Empty);
                    InsertContext contest = new InsertContext();
                    contest.DealerName = DealerName;
                    contest.DealerCity = DealerCity;
                    contest.DealerPhone = DealerPhone;
                    contest.DealerEmail = DealerEmail;
                    contest.DistributorCode = DistributorCode;
                    contest.EmpCode = EmpCode;
                
                    contest.ImageEncode = Imagename;
                    contest.FseName = FseName;
                    contest.DealerState = stateName;
                    contest.ParentCategory = ParentCatid;
                    contest.CreatedOn = DateTime.Now;


                    if (Session["ASMEmpCode"] != null)
                    {
                        contest.DealerFirmName = DealerFirmName;
                        contest.DealerId = "";
                        contest.ASMEmpCode = Session["ASMEmpCode"].ToString();
                        contest.CreatedBy = Session["ASMEmpCode"].ToString();
                        contest.FSE_DealerMobileNo = Session["mobileno"].ToString();
                    }
                    else
                    {
                        contest.ASMEmpCode = "";
                        contest.FSE_DealerMobileNo = "";
                        contest.DealerId = Session["userid"].ToString();
                        contest.CreatedBy = Session["userid"].ToString();
                    }
                    //contest.CreatedBy = Session["SapCode"].ToString();

                    if (Status.ToLower() == "on")
                    {
                        contest.Status = 1;

                    }
                    else
                    {
                        contest.Status = 0;
                    }
                    db.InsertContexts.AddObject(contest);
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        string str = Path.Combine(Server.MapPath("~/ProfileImages/"), Imagename);
                        postedFile.SaveAs(str);
                        ViewBag.SaveStatus = "Record Saved Successfully";
                    }

                }

                return View("Index", db.InsertContexts.ToList().ToPagedList(1, 5));
                // }
                //else
                //{
                //    return RedirectToAction("snotallowed", "snotallowed");

                //}
            }
        }


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
                if (Session["ASMEmpCode"] != null)
                {
                    string Asmempcode = Session["ASMEmpCode"].ToString();
                    string Mobileno = Session["mobileno"].ToString();
                    var contestdetails = (from c in db.InsertContexts
                                          where c.Status != 2 && c.CreatedBy == Asmempcode && c.FSE_DealerMobileNo == Mobileno

                                          select c).ToList();
                    var Contestdetails2 = (from c in contestdetails
                                           join cty in db.cities on c.DealerCity equals cty.id.ToString()
                                           join st in db.allstates on c.DealerState equals st.id.ToString()
                                           join pcategory in db.ParentCategories on c.ParentCategory equals pcategory.Pcid.ToString()
                                           select new
                                           {
                                               DealerName = c.DealerName,
                                               DealerCity = cty.cityname,
                                               Dealestate = st.statename,
                                               status = c.Status == 1 ? "Enable" : "Disable",
                                               DealerPhone = c.DealerPhone,
                                               DelaerEmail = c.DealerEmail,
                                               DistributorCode = c.DistributorCode,
                                               Empcode = c.EmpCode,
                                               ImageEncode = c.ImageEncode,
                                               Id = c.Id,
                                               Parentcategory = pcategory.PCName,

                                               //   CreatedOn = Convert.ToDateTime(c.CreatedOn),
                                               CreatedOn = Convert.ToDateTime(c.CreatedOn).ToString("yyyy-MM-ddTHH:mm:ss"),
                                              
                                               // createdtime = Convert.ToDateTime(c.CreatedOn).ToShortTimeString(),
                                               createddate = Convert.ToDateTime(c.CreatedOn).ToShortDateString(),

                                           }).OrderByDescending(c => c.Id).Skip(page ?? 0).Take(15).ToList();
                    if (Contestdetails2.Count == 2)
                    {
                        Session["Count2"] = "Count2";
                    }
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
                }
                else
                {
                    string Sapcode = Session["SapCode"].ToString();
                    var contestdetails = (from c in db.InsertContexts
                                          where c.Status != 2 && c.CreatedBy == createdby && c.DealerId == Sapcode

                                          select c).ToList();
                    var Contestdetails2 = (from c in contestdetails
                                           join cty in db.cities on c.DealerCity equals cty.id.ToString()
                                           join st in db.allstates on c.DealerState equals st.id.ToString()
                                           join pcategory in db.ParentCategories on c.ParentCategory equals pcategory.Pcid.ToString()
                                           select new
                                           {
                                               DealerName = c.DealerName,
                                               DealerCity = cty.cityname,
                                               Dealestate = st.statename,
                                               status = c.Status == 1 ? "Enable" : "Disable",
                                               DealerPhone = c.DealerPhone,
                                               DelaerEmail = c.DealerEmail,
                                               DistributorCode = c.DistributorCode,
                                               Empcode = c.EmpCode,
                                               ImageEncode = c.ImageEncode,
                                               Id = c.Id,
                                               Parentcategory = pcategory.PCName,

                                               //   CreatedOn = Convert.ToDateTime(c.CreatedOn),
                                               CreatedOn = Convert.ToDateTime(c.CreatedOn).ToString("yyyy-MM-ddTHH:mm:ss"),
                                               createddate = Convert.ToDateTime(c.CreatedOn).ToShortDateString(),
                                               // createdtime = Convert.ToDateTime(c.CreatedOn).ToShortTimeString(),
                                               // startdate = Convert.ToDateTime(c.stardate).ToShortDateString(),

                                           }).OrderByDescending(c => c.Id).Skip(page ?? 0).Take(15).ToList();
                    if (Contestdetails2.Count == 2)
                    {
                        Session["Count2"] = "Count2";
                    }
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
                }


                // }
                //else
                //{
                //    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                //}
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
                //string pageUrl2 = "/Banner/Index";
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                //if (result[0]["editrole"].ToString() == "1")
                //{
                try
                {
                    InsertContext contest = db.InsertContexts.Single(a => a.Id == id && a.Status != 2);

                    DateTime addminute = contest.CreatedOn.Value.AddMinutes(15);

                    ViewBag.stateid = contest.DealerState;
                    ViewBag.cityid = contest.DealerCity;
                    ViewBag.parentcatid = contest.ParentCategory;
                    ViewBag.preStatus = contest.Status;
                    ViewBag.Update = "";


                    return View(db.InsertContexts.Single(a => a.Id == id));
                }
                catch
                {
                    return View("Error");
                }
                //}
                //else
                //{
                //    return RedirectToAction("snotallowed", "snotallowed");
                //}
            }
        }
        [HttpPost]
        public ActionResult Update(string Id, string ParentCatid, string DealerName, string stateName, string DealerCity, string DealerPhone, string DealerEmail, string DistributorCode, string Status, string EmpCode, string FseName, string DealerFirmName, HttpPostedFileBase postedFile)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = "/Banner/Index";
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                //if (result[0]["editrole"].ToString() == "1")
                //{
                int intid = int.Parse(Id);
                Status = Status ?? "off";
                #region Check Validation

                #region Dealer Details Validations
                if (Session["ASMEmpCode"] != null)
                {
                    if (DealerFirmName == null)
                    {
                        ModelState.AddModelError("DealerFirmName", "Dealer Firm Name Is Required");
                    }
                    else
                    {
                        if (DealerFirmName == "")
                        {
                            ModelState.AddModelError("DealerFirmName", "Dealer Firm Name Is Required");
                        }


                    }
                }
                //if (DealerCity == null)
                //{
                //    ModelState.AddModelError("DealerCity", "Dealer City Is Required");
                //}
                //else
                //{
                //    if (DealerCity == "")
                //    {
                //        ModelState.AddModelError("DealerCity", "Dealer City Is Required");
                //    }


                //}
                if (DealerPhone == null)
                {
                    ModelState.AddModelError("DealerPhone", "Dealer Phone Is Required");
                }
                else
                {
                    if (DealerPhone == "")
                    {
                        ModelState.AddModelError("DealerPhone", "Dealer Phone Is Required");
                    }


                }
                if (stateName == "0")
                {
                    ModelState.AddModelError("DealerState", "Please select State");
                }

                if (FseName == null)
                {
                    ModelState.AddModelError("FseName", "Emp/FSE Name Is Required");
                }
                else
                {
                    if (FseName == "")
                    {
                        ModelState.AddModelError("FseName", "Emp/FSE Name Email Is Required");
                    }


                }
                //if (DealerEmail == null)
                //{
                //    ModelState.AddModelError("DealerEmail", "Dealer Email Is Required");
                //}
                //else
                //{
                //    if (DealerEmail == "")
                //    {
                //        ModelState.AddModelError("DealerEmail", "Dealer Email Is Required");
                //    }


                //}
                //if (DistributorCode == null)
                //{
                //    ModelState.AddModelError("DistributorCode", "Distributor Code Is Required");
                //}
                //else
                //{
                //    if (DistributorCode == "")
                //    {
                //        ModelState.AddModelError("DistributorCode", "Distributor Code Is Required");
                //    }


                //}
                if (EmpCode == null)
                {
                    ModelState.AddModelError("EmpCode", "Emp Code Is Required");
                }
                else
                {
                    if (EmpCode == "")
                    {
                        ModelState.AddModelError("EmpCode", "Emp Code Is Required");
                    }


                }
                if (FseName == null)
                {
                    ModelState.AddModelError("FseName", "FSE Name Is Required");
                }
                else
                {
                    if (FseName == "")
                    {
                        ModelState.AddModelError("FseName", "FSE Name Is Required");
                    }


                }
                #endregion

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


                if (ModelState.IsValid)
                {
                    if (postedFile == null)
                    {
                        try
                        {
                            InsertContext contest = db.InsertContexts.Single(a => a.Id == intid && a.Status != 2);
                            InsertContextHistory contestHisotry = new InsertContextHistory();
                            contestHisotry.InserContextId = contest.Id;
                            contestHisotry.DealerName = contest.DealerName;
                            contestHisotry.DealerCity = contest.DealerCity;
                            contestHisotry.DealerEmail = contest.DealerEmail;
                            contestHisotry.DealerPhone = contest.DealerPhone;
                            //  contestHisotry. = contest.BStatus;
                            contestHisotry.DealerID = Session["userid"].ToString();
                            contestHisotry.DistributorCode = contest.DistributorCode;
                            contestHisotry.Empcode = contest.EmpCode;
                            contestHisotry.ModifyBy = Session["userid"].ToString();
                            contestHisotry.ModifyOn = DateTime.Now;

                            db.InsertContextHistories.AddObject(contestHisotry);

                            contest.DealerName = DealerName;
                            contest.DealerCity = DealerCity;
                            contest.DealerPhone = DealerPhone;
                            contest.DealerEmail = DealerEmail;
                            contest.DealerState = stateName;
                            contest.FseName = FseName;
                            contest.DistributorCode = DistributorCode;
                            contest.EmpCode = EmpCode;
                            if (Session["ASMEmpCode"] != null)
                            {
                                contest.FSE_DealerMobileNo = Session["mobileno"].ToString();
                                contest.DealerFirmName = DealerFirmName;
                                contest.ModifyBy = Session["ASMEmpCode"].ToString();
                            }
                            else
                            {
                                contest.FSE_DealerMobileNo = "";
                                contest.DealerFirmName = "";
                                contest.DealerId = Session["userid"].ToString();
                                contest.ModifyBy = Session["userid"].ToString();

                            }

                            contest.ModifyOn = DateTime.Now;

                            if (Status.ToLower() == "on")
                            {
                                contest.Status = 1;

                            }
                            else
                            {
                                contest.Status = 0;
                            }
                            int affectedRows = db.SaveChanges();
                            if (affectedRows > 0)
                            {

                                ViewBag.Update = "Done";

                            }
                        }
                        catch
                        {
                            return View("Index");
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
                                InsertContext contest = db.InsertContexts.Single(a => a.Id == intid && a.Status != 2);

                                //Save Previous Record In History
                                InsertContextHistory contesthistory = new InsertContextHistory();
                                contesthistory.InserContextId = contest.Id;
                                contesthistory.DealerName = contest.DealerName;
                                contesthistory.DealerCity = contest.DealerCity;
                                contesthistory.DealerPhone = contest.DealerPhone;
                                contesthistory.DealerEmail = contest.DealerEmail;
                                contesthistory.DealerID = contest.DealerId;
                                contesthistory.DistributorCode = contest.DistributorCode;
                                contesthistory.Empcode = contest.EmpCode;
                                contesthistory.ModifyBy = Session["userid"].ToString();
                                contesthistory.ModifyOn = DateTime.Now;

                                db.InsertContextHistories.AddObject(contesthistory);


                                //Save New Record
                                contest.DealerName = DealerName;
                                contest.DealerCity = DealerCity;
                                contest.DealerPhone = DealerPhone;
                                contest.DealerEmail = DealerEmail;
                               // contest.DealerId = Session["userid"].ToString();
                                contest.DealerFirmName = DealerFirmName;
                                contest.ImageEncode = Imagename;
                                contest.DistributorCode = DistributorCode;
                                contest.EmpCode = EmpCode;
                                if (Session["ASMEmpCode"] != null)
                                {
                                    contest.DealerId = "";
                                    contest.FSE_DealerMobileNo = Session["mobileno"].ToString();
                                    contest.DealerFirmName = DealerFirmName;
                                    contest.ModifyBy = Session["ASMEmpCode"].ToString();
                                }
                                else
                                {
                                    contest.FSE_DealerMobileNo = "";
                                    contest.DealerFirmName = "";
                                    contest.DealerId = Session["userid"].ToString();
                                    contest.ModifyBy = Session["userid"].ToString();

                                }


                               // contest.ModifyBy = Session["userid"].ToString();
                                contest.ModifyOn = DateTime.Now;
                                if (Status.ToLower() == "on")
                                {
                                    contest.Status = 1;

                                }
                                else
                                {
                                    contest.Status = 0;
                                }

                                if (db.SaveChanges() > 0)
                                {
                                    ViewBag.Update = "Done";
                                    string str = Path.Combine(Server.MapPath("~/ProfileImages/"), Imagename);
                                    postedFile.SaveAs(str);
                                }
                                else
                                {
                                    return Content("<script>alert('Record Has Not Been Saved');</script>");
                                }


                            }
                            catch
                            {
                                return View("Index");
                            }
                        }

                    }


                }
                InsertContext contestid = db.InsertContexts.Single(a => a.Id == intid);
                //ViewBag.preStartDate = Convert.ToDateTime(banner2.stardate).ToString("dd-MM-yyyy");
                //ViewBag.PreEndDate = Convert.ToDateTime(banner2.ExpriyDate).ToString("dd-MM-yyyy");
                ViewBag.preStatus = contestid.Status;

                return View("Edit", contestid);
                //}
                //else
                //{
                //    return RedirectToAction("snotallowed", "snotallowed");
                //}
            }
            //return RedirectToAction("Edit", new {id=id});

        }

        //public ActionResult Update()
        //{
        //    return RedirectToAction("BannerList");
        //}
        public JsonResult Delete(int id)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                //dt = Session["permission"] as DataTable;
                //string pageUrl2 = "/Banner/Index";
                //DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                //if (result[0]["deleterole"].ToString() == "1")
                //{
                try
                {
                    InsertContext contest = db.InsertContexts.Single(a => a.Id == id);
                    //Save Previous Record In History
                    InsertContextHistory contestHisotry = new InsertContextHistory();
                    contestHisotry.InserContextId = contest.Id;
                    contestHisotry.DealerName = contest.DealerName;
                    contestHisotry.DealerCity = contest.DealerCity;
                    contestHisotry.DealerEmail = contest.DealerEmail;
                    contestHisotry.DealerPhone = contest.DealerPhone;

                    contestHisotry.DistributorCode = contest.DistributorCode;
                    contestHisotry.Empcode = contest.EmpCode;

                    contestHisotry.ModifyBy = Session["userid"].ToString();
                    contestHisotry.ModifyOn = DateTime.Now;

                    db.InsertContextHistories.AddObject(contestHisotry);

                    //Delete Record From Table
                    contest.Status = 2;
                    contest.ModifyOn = DateTime.Now;
                    contest.ModifyBy = Session["userid"].ToString();
                    if (db.SaveChanges() > 0)
                    {
                        return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                    }

                    else
                    {
                        return Json("Record Not Deleted", JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    return Json("Invalid Operation", JsonRequestBehavior.AllowGet);
                }

                //}
                //else
                //{
                //    return Json("You Have No Delete Permission", JsonRequestBehavior.AllowGet);

                //}
            }
        }

        [HttpPost]
        public ActionResult UpdateSequence(string ids)
        {
            var bannersplit = ids.Split(',');
            int bannerid = Convert.ToInt32(bannersplit[0]);
            Banner bn = db.Banners.Single(c => c.id == bannerid && c.BStatus != 2);
            bn.Sequence = Convert.ToInt32(bannersplit[1]);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
