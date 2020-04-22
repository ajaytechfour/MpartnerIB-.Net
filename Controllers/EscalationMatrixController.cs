using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.Data;
using System.IO;
namespace LuminousMpartnerIB.Controllers
{
    public class EscalationMatrixController : Controller
    {

        //
        // GET: /ProductCategory/

        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        //private DataTable dt = new DataTable();
        //private string PageUrl = "/EscalationMatrix/Index";
        public ActionResult Index()
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
                
                    return View();
                
                
            }
        }
        public ActionResult SaveContact(string servicecenter, string statusC, string pcId, string Address)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {


                    int pcid;
                    if (!int.TryParse(pcId, out pcid))
                    {
                        ModelState.AddModelError("Stateid", " State Has Incorrect Value");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("Stateid", "Select State Name");
                    }
                if (servicecenter == "0")
                    {
                        ModelState.AddModelError("ServiceCenterName", "Select ServiceCentre Name");
                    }
                    //if (db.ProductCatergories.Any(a => a.PCode.ToLower() == contactUs.PCode.ToLower() && a.Pstatus != 2))
                    //{
                    //    ModelState.AddModelError("PCode", "Product Code Already Exists");
                    //}
                if (Address == "" || Address == null)
                    {
                        ModelState.AddModelError("Address", "Address is required");
                    }
                    
                    if (ModelState.IsValid)
                    {
                        
                        EscalationMatrix contactusd = new EscalationMatrix();

                        contactusd.StateId = pcid;
                        contactusd.ServiceCenterName = servicecenter;
                        contactusd.Address = Address;
                        
                        contactusd.CreatedBy = Session["userid"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.status = 1;
                        }
                        else
                        {
                            contactusd.status = 0;
                        }

                        db.EscalationMatrices.AddObject(contactusd);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            
                            return Content("<script>alert('Data Successfully Submitted');location.href='../EscalationMatrix/Index';</script>");
                        }
                        else
                        {
                            return Content("<script>alert('Data Not Submitted Successfully');location.href='/EscalationMatrix/Index';</script>");
                        }




                    }
                    return View("Index");

            }
        }

        public JsonResult GetContactDetail()
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {

                    var contactdetails = (from c in db.EscalationMatrices
                                          where c.status != 2
                                          select c).ToList();
                    int totalrecord;
                    //if (page != null)
                    //{
                    //    page = (page - 1) * 15;
                    //}

                    var contactDetails2 = (from c in contactdetails join st in db.States on c.StateId equals st.StateID

                                           select new
                                           {
                                               Statename = st.StateName,
                                               // PCode = c.PCode,
                                               servicecentername = c.ServiceCenterName,
                                               address=c.Address,
                                               status = c.status == 1 ? "Enable" : "Disable",
                                               id = c.Id

                                           }).OrderByDescending(a => a.id).ToList();

                   var data = new { result = contactDetails2, TotalRecord = 0 };

                   return Json(data, JsonRequestBehavior.AllowGet);

            }
        }



        [HttpPost]
        public JsonResult DeleteContact(int id)
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
                //if (result[0]["deleterole"].ToString() == "1")
                //{
                    EscalationMatrix contactUs = db.EscalationMatrices.Single(a => a.Id == id);
                    contactUs.status = 2;
                    contactUs.ModifiedOn = DateTime.Now;
                    contactUs.ModifiedBy = Session["userid"].ToString();
                    int affectedReocrds = db.SaveChanges();
                    if (affectedReocrds > 0)
                    {
                        EscalationMatrix_History CUDHistory = new EscalationMatrix_History();
                        CUDHistory.Pid = contactUs.Id;
                        CUDHistory.ServiceCenterName = contactUs.ServiceCenterName;
                        CUDHistory.Address = contactUs.Address;
                        CUDHistory.status = 2;

                        CUDHistory.CreatedBy = Session["userid"].ToString();
                        CUDHistory.CreatedOn = DateTime.Now;
                        db.EscalationMatrix_History.AddObject(CUDHistory);
                        db.SaveChanges();
                        return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Record Not Deleted", JsonRequestBehavior.AllowGet);
                    }
                //}
                //else
                //{
                //    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                //}
            }
        }

        [HttpGet]
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
                //if (result[0]["editrole"].ToString() == "1")
                //{
                    EscalationMatrix cud = db.EscalationMatrices.Single(a => a.Id == id);
                    ViewBag.status = cud.status;
                    ViewBag.stateid = cud.StateId;
                    ViewBag.level = cud.ServiceCenterName;
                    return View(cud);
                //}
                //else
                //{
                //    return RedirectToAction("snotallowed", "snotallowed");
                //}
            }
        }
        public ActionResult Update(int Id, string servicecenter, string statusC, string pcId, string Address)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {

                    int pcid;
                    ViewBag.status = statusC;
                    ViewBag.Pid = pcId;
                    ViewBag.level = Address;
                 
                    if (!int.TryParse(pcId, out pcid))
                    {
                        ModelState.AddModelError("Stateid", " State Has Incorrect Value");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("Stateid", "Select State Name");
                    }
                    if (servicecenter == "0")
                    {
                        ModelState.AddModelError("ServiceCenterName", "Select ServiceCentre Name");
                    }
                    //if (db.ProductCatergories.Any(a => a.PCode.ToLower() == contactUs.PCode.ToLower() && a.Pstatus != 2))
                    //{
                    //    ModelState.AddModelError("PCode", "Product Code Already Exists");
                    //}
                    if (Address == "" || Address==null)
                    {
                        ModelState.AddModelError("Address", "Address is required");
                    }
                    



                    //if (db.ProductCatergories.Any(a => a.PCode.ToLower() == contactUs.PCode.ToLower() && a.id != contactUs.id && a.Pstatus != 2))
                    //{
                    //    ModelState.AddModelError("PCode", "Product Code Already Exists");
                    //}

                    if (ModelState.IsValid)
                    {


                        EscalationMatrix contactusd = db.EscalationMatrices.Single(a => a.Id==Id);

                        //Save Previous Record In History
                        EscalationMatrix_History CUDHistory = new EscalationMatrix_History();
                        CUDHistory.Pid = contactusd.Id;

                        CUDHistory.StateId = contactusd.StateId;
                        CUDHistory.status = contactusd.status;
                        CUDHistory.Address = contactusd.Address;
                        CUDHistory.ServiceCenterName = contactusd.ServiceCenterName;
                        CUDHistory.CreatedBy = Session["userid"].ToString();
                        CUDHistory.CreatedOn = DateTime.Now;
                        db.EscalationMatrix_History.AddObject(CUDHistory);

                        //Save New Record In Table
                       

                        
                        contactusd.Address = Address;
                        contactusd.ModifiedOn = DateTime.Now;
                        contactusd.StateId = pcid;
                        contactusd.ServiceCenterName = servicecenter;
                        contactusd.ModifiedBy = Session["userid"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.status = 1;
                        }
                        else
                        {
                            contactusd.status = 0;
                        }
                        if (db.SaveChanges() > 0)
                        {
                            
                            ViewBag.Result = "Record Updated Successfully";
                        }
                    }
                    return View("Edit");

            }
        }

        public JsonResult GetState()
        {
            if ((Session["userid"] == null && Session["Ioslogin"] == null))
            {


                return Json("Login", JsonRequestBehavior.AllowGet);

            }

            else
            {
                
                    var getParentCat = (from c in db.States
                                        
                                        select new
                                        {
                                            id = c.StateID,
                                            Name = c.StateName
                                        }).ToList();
                    return Json(getParentCat, JsonRequestBehavior.AllowGet);

            }
        }


    }
}
