using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.Data;
namespace Luminous.Controllers
{
    public class ContactUsController : Controller
    {
        //App
        // GET: /ContactUs/
        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/ContactUs/Index";
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/ContactUs/Index";
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
        public ActionResult SaveContact(contactUsDetail contactUs, string statusC, string CType)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/ContactUs/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["createrole"].ToString() == "1")
                {
                    long Phone;
                    #region Check Validation
                    if (CType == "0")
                    {
                        ModelState.AddModelError("Contact_Us_Type", "Select Contact US Type");
                    }

                    if (!long.TryParse(contactUs.PhoneNumber, out Phone))
                    {
                        ModelState.AddModelError("PhoneNumber", "Phone No Is Not Valid");
                    }
                    if (contactUs.PhoneNumber == null)
                    {
                        ModelState.AddModelError("PhoneNumber", "Phone No Is Not Valid");
                    }
                    else if (contactUs.PhoneNumber.Length != 10)
                    {
                        ModelState.AddModelError("PhoneNumber", "Phone No Is Not Valid");
                    }
                    if (db.contactUsDetails.Any(a => a.Contact_Us_Type.ToLower() == CType.ToLower() && a.Cstatus != 2))
                    {
                        ModelState.AddModelError("Contact_Us_Type", "Contact Us Type Already Exists");
                    }
                    #endregion
                    if (ModelState.IsValid)
                    {
                        contactUsDetail contactusd = new contactUsDetail();
                        contactusd.CAddress = contactUs.CAddress;
                        contactusd.Contact_Us_Type = CType.ToUpper();
                        contactusd.Created_date = DateTime.Now;
                        contactusd.CreatedBy = Session["userid"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.Cstatus = 1;
                        }
                        else
                        {
                            contactusd.Cstatus = 0;
                        }
                        contactusd.Email = contactUs.Email;
                        contactusd.Fax = contactUs.Fax;
                        contactusd.PhoneNumber = contactUs.PhoneNumber;
                        db.contactUsDetails.AddObject(contactusd);
                        db.SaveChanges();
                        return Content("<script>alert('Data Successfully Submitted');location.href='../ContactUs/Index';</script>");

                    }
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

                }
            }
        }

        public JsonResult GetContactDetail(int? page)
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
                    var contactdetails = (from c in db.contactUsDetails
                                          where c.Cstatus != 2
                                          select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }
                   
                    var contactDetails2 = (from c in contactdetails
                                           select new
                                           {
                                               addrs = c.CAddress,
                                               ctype = c.Contact_Us_Type,
                                               status = c.Cstatus == 1 ? "Enable" : "Disable",
                                               email = c.Email,
                                               fax = c.Fax,
                                               id = c.id,
                                               phno = c.PhoneNumber
                                           }).OrderByDescending(a => a.id).Skip(page ?? 0).Take(15).ToList();
                    if (contactdetails.Count() % 15 == 0)
                    {
                        totalrecord = contactdetails.Count() / 15;
                    }
                    else
                    {
                        totalrecord = (contactdetails.Count() / 15) + 1;
                    }
                   
                    var data = new { result = contactDetails2, TotalRecord = totalrecord };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }

        public void EditContact(int id)
        {

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
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["deleterole"].ToString() == "1")
                {
                    contactUsDetail contactUs = db.contactUsDetails.Single(a => a.id == id);
                    //Save Previous Data In History Table
                    contactUsDetail_History CUDHistory = new contactUsDetail_History();
                    CUDHistory.ContId = contactUs.id;
                    CUDHistory.CAddress = contactUs.CAddress;
                    CUDHistory.Contact_Us_Type = contactUs.Contact_Us_Type;
                    CUDHistory.Cstatus = 2;
                    CUDHistory.Email = contactUs.Email;
                    CUDHistory.PhoneNumber = contactUs.PhoneNumber;
                    CUDHistory.Fax = contactUs.Fax;
                    CUDHistory.ModifiedBy = Session["userid"].ToString();
                    CUDHistory.ModifiedDate = DateTime.Now;
                    db.contactUsDetail_History.AddObject(CUDHistory);

                    //Update Data With New Value
                    contactUs.Cstatus = 2;
                    contactUs.ModifiedBy = Session["userid"].ToString();
                    contactUs.ModifiedDate = DateTime.Now;


                    int affectedReocrds = db.SaveChanges();
                    if (affectedReocrds > 0)
                    {
                        return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Record Not Deleted", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
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
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    contactUsDetail cud = db.contactUsDetails.Single(a => a.id == id);
                    ViewBag.status = cud.Cstatus;
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }
        public ActionResult Update(contactUsDetail contactUs, string statusC, string CType)
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
                    long Phone;
                    if (CType == "0")
                    {
                        ModelState.AddModelError("Contact_Us_Type", "Select Contact US Type");
                    }
                    if (!long.TryParse(contactUs.PhoneNumber, out Phone))
                    {
                        ModelState.AddModelError("PhoneNumber", "Phone No Is Not Valid");
                    }

                    if (contactUs.PhoneNumber.Length != 10)
                    {
                        ModelState.AddModelError("PhoneNumber", "Phone No Is Not Valid");
                    }
                    if (db.contactUsDetails.Any(a => a.Contact_Us_Type.ToLower() == CType.ToLower() && a.id != contactUs.id && a.Cstatus != 2))
                    {
                        ModelState.AddModelError("Contact_Us_Type", "Contact Us Type Already Exists");
                    }

                    if (ModelState.IsValid)
                    {
                        contactUsDetail contactusd = db.contactUsDetails.Single(a => a.id == contactUs.id);

                        //Save Record In History table
                        contactUsDetail_History CUDHistory = new contactUsDetail_History();
                        CUDHistory.ContId = contactusd.id;
                        CUDHistory.CAddress = contactusd.CAddress;
                        CUDHistory.Contact_Us_Type = contactusd.Contact_Us_Type;
                        CUDHistory.Cstatus = contactusd.Cstatus;
                        CUDHistory.Email = contactusd.Email;
                        CUDHistory.PhoneNumber = contactusd.PhoneNumber;
                        CUDHistory.Fax = contactusd.Fax;
                        CUDHistory.ModifiedBy = Session["userid"].ToString();
                        CUDHistory.ModifiedDate = DateTime.Now;
                        db.contactUsDetail_History.AddObject(CUDHistory);

                        //Save Record
                        contactusd.CAddress = contactUs.CAddress;
                        contactusd.Contact_Us_Type = CType.Trim();
                        contactusd.ModifiedDate = DateTime.Now;
                        contactusd.ModifiedBy = Session["userid"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.Cstatus = 1;
                        }
                        else
                        {
                            contactusd.Cstatus = 0;
                        }
                        contactusd.Email = contactUs.Email;
                        contactusd.Fax = contactUs.Fax;
                        contactusd.PhoneNumber = contactUs.PhoneNumber;

                        ViewBag.Result = "Record Updated Successfully";                 
                        db.SaveChanges();                        
                    }


                    return View("Edit", db.contactUsDetails.Single(a => a.id ==contactUs.id));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

    }
}
