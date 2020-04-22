using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.Text.RegularExpressions;
using System.Data.Entity;
using System.Transactions;
using System.Data;
namespace Luminous.Controllers
{
    public class FunctionsGroupController : Controller
    {
        //
        // GET: /FunctionsGroup/
        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/FunctionsGroup/index";
        public ActionResult Index()
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

        [HttpPost]
        public ActionResult Save(FUNCTION function, string Statust)
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
                    Statust = Statust ?? "off";
                    #region Validation For FunctionName
                    if (function.fNAME == null)
                    {
                        ModelState.AddModelError("fNAME", "Function Name Is Required");
                    }
                    else if (function.fNAME == "")
                    {
                        ModelState.AddModelError("fNAME", "Function Name Is Required");
                    }
                    else if (function.fNAME.Length > 99)
                    {
                        ModelState.AddModelError("fNAME", "Character Should Be Less Than 100");
                    }
                    #endregion

                    #region Validation For Email
                    Regex emailCheck = new Regex("^((\"[\\w-\\s]+\")|([\\w-]+(?:\\.[\\w-]+)*)|(\"[\\w-\\s]+\")([\\w-]+(?:\\.[\\w-]+)*))(@((?:[\\w-]+\\.)*\\w[\\w-]{0,66})\\.([a-z]{2,6}(?:\\.[a-z]{2})?)$)|(@\\[?((25[0-5]\\.|2[0-4][0-9]\\.|1[0-9]{2}\\.|[0-9]{1,2}\\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\\]?$)");
                    if (function.fEMAIL == null)
                    {
                        ModelState.AddModelError("fEMAIL", "Email Is Required");
                    }
                    else if (function.fEMAIL == "")
                    {
                        ModelState.AddModelError("fEMAIL", "Email Is Required");
                    }
                    else if (!emailCheck.IsMatch(function.fEMAIL))
                    {
                        ModelState.AddModelError("fEMAIL", "Invalid Email");
                    }
                    else if (function.fEMAIL.Length > 99)
                    {
                        ModelState.AddModelError("fEMAIL", "Character Should Be Less Than 100");
                    }
                    #endregion

                    #region Validation For Function Department Name

                    if (function.fDepartmentName == null)
                    {
                        ModelState.AddModelError("fDepartmentName", "Department Name Is Required");
                    }
                    else if (function.fDepartmentName == "")
                    {
                        ModelState.AddModelError("fDepartmentName", "Department Name Is Required");
                    }
                    else if (function.fDepartmentName.Length > 99)
                    {
                        ModelState.AddModelError("fDepartmentName", "Character Should Be Less Than 100");
                    }
                    #endregion

                    #region Validation For Department's Person Name
                    if (function.DepartmentPersoneName == null)
                    {
                        ModelState.AddModelError("DepartmentPersoneName", "Department Person Name Is Required");
                    }
                    else if (function.DepartmentPersoneName == "")
                    {
                        ModelState.AddModelError("DepartmentPersoneName", "Department Person Name Is Required");
                    }
                    else if (function.fDepartmentName.Length > 99)
                    {
                        ModelState.AddModelError("DepartmentPersoneName", "Character Should Be Less Than 100");
                    }
                    #endregion

                    #region Duplicate Validations
                    if (db.FUNCTIONS.Any(a => a.fNAME.ToLower().Trim() == function.fNAME.ToLower().Trim() && a.Status !=2 ))
                    {
                        ModelState.AddModelError("fNAME", "Function Is Already Exists");
                    }

                    #endregion


                    if (ModelState.IsValid)
                    {

                        FUNCTION _Function = new FUNCTION();
                        _Function.fNAME = function.fNAME.Trim();
                        _Function.fEMAIL = function.fEMAIL.Trim();
                        _Function.fDepartmentName = function.fDepartmentName.Trim();
                        _Function.DepartmentPersoneName = function.DepartmentPersoneName.Trim();
                        _Function.CreatedDate = DateTime.Now;
                        _Function.CareatedBy = Session["userid"].ToString();
                        if (Statust.ToLower().Trim() == "on")
                        {
                            _Function.Status = 1;
                        }
                        else
                        {
                            _Function.Status = 0;
                        }
                        try
                        {
                            db.FUNCTIONS.AddObject(_Function);
                           int a= db.SaveChanges();
                            if (a > 0)
                            {
                                return Content("<script>alert('Function Has Been Created Sucessfully');location.href='/FunctionsGroup/Index';</script>");
                            }

                            else
                            {
                                return Content("<script>alert('Function Not Created');location.href='/FunctionsGroup/Index';</script>");
                            }
                        }
                        catch (Exception ex)
                        {
                            Exception str = ex.InnerException;
                        }
                    }
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

                }
            }
        }

        public JsonResult FunctionsDetails(int? page)
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
                    #region Get all undeleted functions Lists
                    var FunctionsLists = (from c in db.FUNCTIONS
                                          where c.Status != 2
                                          select c).ToList();
                    #endregion

                    //variable for holding total pages
                    int totalrecord;

                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }
                    var _FunctionsLists = (from c in FunctionsLists
                                           select new
                                           {
                                               Id = c.ID,
                                               Fname = c.fNAME,
                                               Femail = c.fEMAIL,
                                               status = c.Status == 1 ? "Enable" : "Disable",
                                               DepartmentName = c.fDepartmentName,
                                               DepartmentPersoneName = c.DepartmentPersoneName,


                                           }).OrderByDescending(a => a.Id).Skip(page ?? 0).Take(15).ToList();


                    if (FunctionsLists.Count() % 15 == 0)
                    {
                        totalrecord = _FunctionsLists.Count() / 15;
                    }
                    else
                    {
                        totalrecord = (_FunctionsLists.Count() / 15) + 1;
                    }

                    var data = new { result = _FunctionsLists, TotalRecord = totalrecord };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }

        }

        [HttpGet]
        public ActionResult EditFunctions(int id)
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
                    FUNCTION cud = db.FUNCTIONS.Single(a => a.ID == id);
                    ViewBag.status = cud.Status;
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        [HttpPost]
        public ActionResult Update(FUNCTION function, string Statust)
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

                    #region Validation For FunctionName
                    if (function.fNAME == null)
                    {
                        ModelState.AddModelError("fNAME", "Function Is Required");
                    }
                    else if (function.fNAME == "")
                    {
                        ModelState.AddModelError("fNAME", "Function Is Required");
                    }
                    else if (function.fNAME.Length > 99)
                    {
                        ModelState.AddModelError("fNAME", "Character Should Be Less Than 100");
                    }
                    #endregion

                    #region Validation For Email
                    Regex emailCheck = new Regex("^((\"[\\w-\\s]+\")|([\\w-]+(?:\\.[\\w-]+)*)|(\"[\\w-\\s]+\")([\\w-]+(?:\\.[\\w-]+)*))(@((?:[\\w-]+\\.)*\\w[\\w-]{0,66})\\.([a-z]{2,6}(?:\\.[a-z]{2})?)$)|(@\\[?((25[0-5]\\.|2[0-4][0-9]\\.|1[0-9]{2}\\.|[0-9]{1,2}\\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\\]?$)");
                    if (function.fEMAIL == null)
                    {
                        ModelState.AddModelError("fEMAIL", "Email Is Required");
                    }
                    else if (function.fEMAIL == "")
                    {
                        ModelState.AddModelError("fEMAIL", "Email Is Required");
                    }
                    else if (!emailCheck.IsMatch(function.fEMAIL))
                    {
                        ModelState.AddModelError("fEMAIL", "Invalid Email");
                    }
                    else if (function.fEMAIL.Length > 99)
                    {
                        ModelState.AddModelError("fEMAIL", "Character Should Be Less Than 100");
                    }
                    #endregion

                    #region Validation For Function Department Name

                    if (function.fDepartmentName == null)
                    {
                        ModelState.AddModelError("fDepartmentName", "Department Name Is Required");
                    }
                    else if (function.fDepartmentName == "")
                    {
                        ModelState.AddModelError("fDepartmentName", "Department Name Is Required");
                    }
                    else if (function.fDepartmentName.Length > 99)
                    {
                        ModelState.AddModelError("fDepartmentName", "Character Should Be Less Than 100");
                    }
                    #endregion

                    #region Validation For Department's Person Name
                    if (function.DepartmentPersoneName == null)
                    {
                        ModelState.AddModelError("DepartmentPersoneName", "Department Person Name Is Required");
                    }
                    else if (function.DepartmentPersoneName == "")
                    {
                        ModelState.AddModelError("DepartmentPersoneName", "Department Person Name Is Required");
                    }
                    else if (function.fDepartmentName.Length > 99)
                    {
                        ModelState.AddModelError("DepartmentPersoneName", "Character Should Be Less Than 100");
                    }
                    #endregion

                    #region Duplicate Validations
                    if (db.FUNCTIONS.Any(a => a.fNAME.ToLower().Trim() == function.fNAME.ToLower().Trim() && a.ID != function.ID && a.Status !=2))
                    {
                        ModelState.AddModelError("fNAME", "Function Is Already Exists");
                    }
                    #endregion

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                FUNCTION _function = db.FUNCTIONS.Single(a => a.ID == function.ID);

                                #region Save Old Record In FunctionHistory
                                FUNCTIONSHistory functionHistory = new FUNCTIONSHistory();
                                functionHistory.functionId = _function.ID;
                                functionHistory.fNAME = _function.fNAME;
                                functionHistory.fEMAIL = _function.fEMAIL;
                                functionHistory.Status = _function.Status;
                                functionHistory.fDepartmentName = _function.fDepartmentName;
                                functionHistory.DepartmentPersoneName = _function.DepartmentPersoneName;
                                functionHistory.ModifiedBy = Session["userid"].ToString();
                                functionHistory.ModifiedDate = DateTime.Now;
                                db.FUNCTIONSHistories.AddObject(functionHistory);
                                #endregion

                                #region Update Functions
                                _function.fNAME = function.fNAME;
                                _function.fEMAIL = function.fEMAIL;
                                _function.ModifiedDate = DateTime.Now;
                                _function.ModifiedBy = Session["userid"].ToString();
                                Statust = Statust ?? "off";
                                if (Statust == "on")
                                {
                                    _function.Status = 1;
                                }
                                else
                                {
                                    _function.Status = 0;
                                }
                                _function.DepartmentPersoneName = function.DepartmentPersoneName;
                                _function.fDepartmentName = function.fDepartmentName;
                                db.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);
                                ViewBag.Result = "Record Updated Successfully";
                                #endregion

                                scope.Complete();

                                db.AcceptAllChanges();
                                ViewBag.Result = "Record Updated Successfully";
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Result = ex.Message.ToString();
                        }

                    }
                    return View("EditFunctions");

                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }


        [HttpPost]
        public JsonResult Delete(int id)
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
                    try
                    {
                        FUNCTION preFunctionValue = db.FUNCTIONS.Single(a => a.ID == id);
                        #region Save Deleted Data
                        FUNCTIONSHistory Fhistory = new FUNCTIONSHistory();
                        Fhistory.functionId = preFunctionValue.ID;
                        Fhistory.fNAME = preFunctionValue.fNAME;
                        Fhistory.fEMAIL = preFunctionValue.fEMAIL;
                        Fhistory.Status = preFunctionValue.Status;
                        Fhistory.fDepartmentName = preFunctionValue.fDepartmentName;
                        Fhistory.DepartmentPersoneName = preFunctionValue.DepartmentPersoneName;
                        Fhistory.ModifiedBy = Session["userid"].ToString();
                        Fhistory.ModifiedDate = DateTime.Now;
                        db.FUNCTIONSHistories.AddObject(Fhistory);
                        #endregion

                        //Set Status For Delete
                        preFunctionValue.Status = 2;
                        db.SaveChanges();
                        return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
                
            }
        }
    }
}
