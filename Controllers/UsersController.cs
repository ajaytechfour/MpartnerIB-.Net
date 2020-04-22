using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
namespace LuminousMpartnerIB.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        DataTable dt = new DataTable();
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Users/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                try
                {
                    if (result[0]["uview"].ToString() == "1")
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("mnotallowed", "mnotallowed");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("mnotallowed", "mnotallowed");
                }

            }
        }

        public JsonResult GetRole()
        {
            if (Session["userid"] == null)
            {
                return Json("session expire", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Users/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                try
                {
                    if (result[0]["uview"].ToString() == "1")
                    {
                        
                            var DList = (from c in db.Regions
                                         where c.status == 1
                                         select new
                                         {
                                             id = c.id,
                                             name = c.name
                                         }).ToList();
                            return Json(DList, JsonRequestBehavior.AllowGet);
                       
                        

                    }
                    else
                    {
                        return Json("You do not have view permission", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json("You do not have view permission", JsonRequestBehavior.AllowGet);
                }

            }
        }
        public ActionResult Save(UsersList ul, string stateName, string cityName, HttpPostedFileBase ProfileImage, string CustomerName, string RoleName,string IsAvtiveName)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Users/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                try
                {
                    if (result[0]["createrole"].ToString() == "1")
                    {
#region All Validation
                        #region Name Validation
                        if (ul.Dis_Name == null)
                        {
                            ModelState.AddModelError("Dis_Name", "Name Is Empty");
                        }
                        else if (ul.Dis_Name == "")
                        {
                            ModelState.AddModelError("Dis_Name", "Name Is Empty");
                        }
                        else if (ul.Dis_Name.Length > 99)
                        {
                            ModelState.AddModelError("Dis_Name", "Character Should Be Less Than 100");
                        }
                        #endregion

                        #region Email Validation
                        Regex emailCheck = new Regex("^[^@]{1,64}@[^@]{1,255}$");
                        if (ul.Dis_Email == null)
                        {
                            ModelState.AddModelError("Dis_Email", "Eamil Is Empty");
                        }
                        else if (ul.Dis_Email == "")
                        {
                            ModelState.AddModelError("Dis_Email", "Eamil Is Empty");
                        }
                        else if (ul.Dis_Email.Length > 99)
                        {
                            ModelState.AddModelError("Dis_Email", "Character Should Be Less Than 100");
                        }
                        else if (!emailCheck.IsMatch(ul.Dis_Email))
                        {
                            ModelState.AddModelError("Dis_Email", "Invalid Email ID");

                        }
                        #endregion

                        #region State Validation
                        if (stateName == null)
                        {
                            ModelState.AddModelError("Dis_State", "State Is Empty");
                        }
                        else if (stateName == "0")
                        {
                            ModelState.AddModelError("Dis_State", "State Is Empty");
                        }
                        #endregion

                        #region City Validation
                        if (cityName == null)
                        {
                            ModelState.AddModelError("Dis_City", "City Is Empty");
                        }
                        else if (cityName == "0")
                        {
                            ModelState.AddModelError("Dis_City", "City Is Empty");
                        }
                        #endregion

                        #region District Validation
                        if (ul.Dis_District == null)
                        {
                            ModelState.AddModelError("Dis_District", "District Is Empty");
                        }
                        else if (ul.Dis_District == "")
                        {
                            ModelState.AddModelError("Dis_District", "District Is Empty");
                        }
                        else if (ul.Dis_District.Length > 99)
                        {
                            ModelState.AddModelError("Dis_District", "Character Should Be Less Than 100");
                        }
                        #endregion

                        #region Contact Validation
                        if (ul.Dis_ContactNo == null)
                        {
                            ModelState.AddModelError("Dis_ContactNo", "Contact Is Empty");
                        }
                        else if (ul.Dis_ContactNo == "")
                        {
                            ModelState.AddModelError("Dis_ContactNo", "Contact Is Empty");
                        }
                        else if (ul.Dis_ContactNo.Length > 99)
                        {
                            ModelState.AddModelError("Dis_ContactNo", "Character Should Be Less Than 100");
                        }
                        #endregion

                        #region Address One Validation
                        if (ul.Dis_Address1 == null)
                        {
                            ModelState.AddModelError("Dis_Address1", "Address One Is Empty");
                        }
                        else if (ul.Dis_Address1 == "")
                        {
                            ModelState.AddModelError("Dis_Address1", "Address One Is Empty");
                        }
                        #endregion

                        #region ProfileImage Validation
                        if (ProfileImage == null)
                        {
                            ModelState.AddModelError("ProfileImage", "Profile Image Is Empty");
                        }
                        #endregion

                        #region SAP Code
                        if (ul.Dis_Sap_Code == null)
                        {
                            ModelState.AddModelError("Dis_Sap_Code", "SAP Code Is Empty");
                        }
                        else if (ul.Dis_Sap_Code == "")
                        {
                            ModelState.AddModelError("Dis_Sap_Code", "SAP Code Is Empty");
                        }
                        else if (ul.Dis_Sap_Code.Length > 99)
                        {
                            ModelState.AddModelError("Dis_Sap_Code", "Character Should Be Less Than 100");
                        }
                        if (db.UsersLists.Any(a => a.Dis_Sap_Code.ToLower() == ul.Dis_Sap_Code.ToLower() && a.isActive !=2))
                        {
                            ModelState.AddModelError("Dis_Sap_Code", "SAP Code Already Exists");
                        }
                        #endregion

                        #region Customer Type Validation
                        if (CustomerName == null)
                        {
                            ModelState.AddModelError("CustomerType", "Customer Type Is Empty");
                        }
                        else if (CustomerName == "0")
                        {
                            ModelState.AddModelError("CustomerType", "Customer Type Is Empty");
                        }
                        else if (CustomerName != "DISTY")
                        {
                            if (CustomerName != "Dealer")
                            {
                                ModelState.AddModelError("CustomerType", "Customer Type Has Invalid Value");
                            }
                        }
                        #endregion

                        #region Role Validation
                        if (RoleName == null)
                        {
                            ModelState.AddModelError("RegId", "Role Is Empty");
                        }
                        else if (RoleName == "0")
                        {
                            ModelState.AddModelError("RegId", "Role Is Empty");
                        }
                   
                        #endregion

#endregion
                        #region Save User
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                string Filename = Path.GetFileNameWithoutExtension(ProfileImage.FileName) + DateTime.Now.ToString("ddMMyyyymmsshh") + Path.GetExtension(ProfileImage.FileName);
                                UsersList ulist = new UsersList();
                                ulist.Dis_Name = ul.Dis_Name.Trim();
                                ulist.UserId = ul.Dis_Sap_Code.Trim();
                                ulist.Dis_Sap_Code = ul.Dis_Sap_Code.Trim();
                                ulist.Dis_Email = ul.Dis_Email.Trim();
                                ulist.Dis_ContactNo = ul.Dis_ContactNo.Trim();
                                ulist.Dis_District = ul.Dis_District.Trim();
                                int stateId = int.Parse(stateName);
                                //var StateNames = db.allstates.Single(a => a.id == stateId);
                                ulist.Dis_Address1 = ul.Dis_Address1;
                                ulist.Dis_Address2 = ul.Dis_Address2;
                                //ulist.Dis_State = StateNames.statename;
                                ulist.Dis_City = cityName.Trim();
                                ulist.isActive = IsAvtiveName.ToLower() == "on" ? 1 : 0;
                                ulist.ProfileImage = Filename;
                                ulist.CustomerType = CustomerName;
                                int Rolid = int.Parse(RoleName);
                                ulist.RegId = Rolid;

                                //if (CustomerName == "DISTY")
                                //{
                                //    var Destributor = db.DestributorLists.Single(a => a.id == Rolid);
                                //    ulist.DestriId = Destributor.id;
                                //    ulist.RegId = Destributor.RegionId;
                                //}
                                //else {
                                //    var Dealer = db.Dealers.Single(a => a.id == Rolid);
                                //    ulist.DealerId = Dealer.id;
                                //    ulist.DestriId = Dealer.DestributorId;
                                //    ulist.RegId = Dealer.RegionId;
                                //}
                                ulist.CreatedBY = Session["userid"].ToString();
                                ulist.CreatedON = DateTime.Now;
                                ulist.ActivatedBY = Session["userid"].ToString();
                                db.UsersLists.Add(ulist);
                                if (db.SaveChanges() > 0)
                                {
                                    ProfileImage.SaveAs(Server.MapPath("~/ProfileImages/") + Filename);
                                    return Content("<script>alert('Record Saved Successfully');location.href='../Users/index';</script>");
                                }
                                else
                                {
                                    return Content("<script>alert('Record Has Not Been Saved Successfully');location.href='../Users/index';</script>");
                                }
                            }
                            catch                                
                            {
                                return Content("<script>alert('Record Has Not Been Saved Successfully');location.href='../Users/index';</script>");
                            }

                          
                        }
                        return View("Index");
                        #endregion
                    }
                    else
                    {
                        return RedirectToAction("mnotallowed", "mnotallowed");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("mnotallowed", "mnotallowed");
                }
            }

        }
        public JsonResult GetUsersDetail(int? page)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Users/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    var usersdetails = (from c in db.UsersLists
                                          where c.isActive != 2
                                          select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }

                    var usersDetails2 = (from c in usersdetails

                                           select new
                                           {
                                               id=c.id,
                                               name=c.Dis_Name,
                                               sapcode=c.Dis_Sap_Code,
                                               CustomerType=c.CustomerType,
                                               status=c.isActive==1?"Active":"Deactiv"
                                           }).OrderByDescending(a => a.id).Skip(page ?? 0).Take(15).ToList();
                    if (usersdetails.Count() % 15 == 0)
                    {
                        totalrecord = usersdetails.Count() / 15;
                    }
                    else
                    {
                        totalrecord = (usersdetails.Count() / 15) + 1;
                    }
                    var data = new { result = usersDetails2, TotalRecord = totalrecord };

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
                string pageUrl2 = "/Users/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    UsersList cud = db.UsersLists.Single(a => a.id == id);
                    ViewBag.status = cud.isActive;
                    
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        public ActionResult Update(UsersList ul, string stateName, string cityName, HttpPostedFileBase ProfileImage, string CustomerName, string RoleName, string IsAvtiveName)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Users/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                try
                {
                    if (result[0]["createrole"].ToString() == "1")
                    {
                        #region All Validation
                        #region Name Validation
                        if (ul.Dis_Name == null)
                        {
                            ModelState.AddModelError("Dis_Name", "Name Is Empty");
                        }
                        else if (ul.Dis_Name == "")
                        {
                            ModelState.AddModelError("Dis_Name", "Name Is Empty");
                        }
                        else if (ul.Dis_Name.Length > 99)
                        {
                            ModelState.AddModelError("Dis_Name", "Character Should Be Less Than 100");
                        }
                        #endregion

                        #region Email Validation
                        Regex emailCheck = new Regex("^[^@]{1,64}@[^@]{1,255}$");
                        if (ul.Dis_Email == null)
                        {
                            ModelState.AddModelError("Dis_Email", "Eamil Is Empty");
                        }
                        else if (ul.Dis_Email == "")
                        {
                            ModelState.AddModelError("Dis_Email", "Eamil Is Empty");
                        }
                        else if (ul.Dis_Email.Length > 99)
                        {
                            ModelState.AddModelError("Dis_Email", "Character Should Be Less Than 100");
                        }
                        else if (!emailCheck.IsMatch(ul.Dis_Email))
                        {
                            ModelState.AddModelError("Dis_Email", "Invalid Email ID");

                        }
                        #endregion

                        #region State Validation
                        if (stateName == null)
                        {
                            ModelState.AddModelError("Dis_State", "State Is Empty");
                        }
                        else if (stateName == "0")
                        {
                            ModelState.AddModelError("Dis_State", "State Is Empty");
                        }
                        #endregion

                        #region City Validation
                        if (cityName == null)
                        {
                            ModelState.AddModelError("Dis_City", "City Is Empty");
                        }
                        else if (cityName == "0")
                        {
                            ModelState.AddModelError("Dis_City", "City Is Empty");
                        }
                        #endregion

                        #region District Validation
                        if (ul.Dis_District == null)
                        {
                            ModelState.AddModelError("Dis_District", "District Is Empty");
                        }
                        else if (ul.Dis_District == "")
                        {
                            ModelState.AddModelError("Dis_District", "District Is Empty");
                        }
                        else if (ul.Dis_District.Length > 99)
                        {
                            ModelState.AddModelError("Dis_District", "Character Should Be Less Than 100");
                        }
                        #endregion

                        #region Contact Validation
                        if (ul.Dis_ContactNo == null)
                        {
                            ModelState.AddModelError("Dis_ContactNo", "Contact Is Empty");
                        }
                        else if (ul.Dis_ContactNo == "")
                        {
                            ModelState.AddModelError("Dis_ContactNo", "Contact Is Empty");
                        }
                        else if (ul.Dis_ContactNo.Length > 99)
                        {
                            ModelState.AddModelError("Dis_ContactNo", "Character Should Be Less Than 100");
                        }
                        #endregion

                        #region Address One Validation
                        if (ul.Dis_Address1 == null)
                        {
                            ModelState.AddModelError("Dis_Address1", "Address One Is Empty");
                        }
                        else if (ul.Dis_Address1 == "")
                        {
                            ModelState.AddModelError("Dis_Address1", "Address One Is Empty");
                        }
                        #endregion

                        #region SAP Code
                        if (ul.Dis_Sap_Code == null)
                        {
                            ModelState.AddModelError("Dis_Sap_Code", "SAP Code Is Empty");
                        }
                        else if (ul.Dis_Sap_Code == "")
                        {
                            ModelState.AddModelError("Dis_Sap_Code", "SAP Code Is Empty");
                        }
                        else if (ul.Dis_Sap_Code.Length > 99)
                        {
                            ModelState.AddModelError("Dis_Sap_Code", "Character Should Be Less Than 100");
                        }
                        if (db.UsersLists.Any(a => a.Dis_Sap_Code.ToLower() == ul.Dis_Sap_Code.ToLower() && a.id != ul.id && a.isActive !=2))
                        {
                            ModelState.AddModelError("Dis_Sap_Code", "SAP Code Already Exists");
                        }
                        #endregion

                        #region Customer Type Validation
                        if (CustomerName == null)
                        {
                            ModelState.AddModelError("CustomerType", "Customer Type Is Empty");
                        }
                        else if (CustomerName == "0")
                        {
                            ModelState.AddModelError("CustomerType", "Customer Type Is Empty");
                        }
                        else if (CustomerName != "DISTY")
                        {
                            if (CustomerName != "Dealer")
                            {
                                ModelState.AddModelError("CustomerType", "Customer Type Has Invalid Value");
                            }
                        }
                        #endregion

                        #region Role Validation
                        if (RoleName == null)
                        {
                            ModelState.AddModelError("RegId", "Role Is Empty");
                        }
                        else if (RoleName == "0")
                        {
                            ModelState.AddModelError("RegId", "Role Is Empty");
                        }

                        #endregion

                        #endregion
                        #region Save User
                        if (ModelState.IsValid)
                        {
                            
                            UsersList ulist = db.UsersLists.Single(a => a.id == ul.id);

                            #region Save Record In History
                            UsersListHistory uhistory = new UsersListHistory();
                            uhistory.UserlistId = ulist.id;
                            uhistory.CustomerType = ulist.CustomerType;
                            uhistory.DealerId = ulist.DestriId;
                            uhistory.DestriId = ulist.DestriId;
                            uhistory.Dis_Address1 = ulist.Dis_Address1;
                            uhistory.Dis_Address2 = ulist.Dis_Address2;
                            uhistory.Dis_City = ulist.Dis_City;
                            uhistory.Dis_ContactNo = ulist.Dis_ContactNo;
                            uhistory.Dis_District = ulist.Dis_District;
                            uhistory.Dis_Email = ulist.Dis_Email;
                            uhistory.Dis_Name = ulist.Dis_Name;
                            uhistory.Dis_Sap_Code = ulist.Dis_Sap_Code;
                            uhistory.Dis_State = ulist.Dis_State;
                            uhistory.isActive = ulist.isActive;
                            uhistory.ProfileImage = ulist.ProfileImage;
                            uhistory.RegId = ulist.RegId;
                            uhistory.UpdatedbY = Session["userid"].ToString();
                            uhistory.UpdatedOn = DateTime.Now;
                            uhistory.UserId = ulist.UserId;
                            db.UsersListHistories.Add(uhistory);
                            #endregion

                            #region IF Prfile Image Is NOT Null
                            
                            if (ProfileImage != null)
                            {
                                string Filename = Path.GetFileNameWithoutExtension(ProfileImage.FileName) + DateTime.Now.ToString("ddMMyyyymmsshh") + Path.GetExtension(ProfileImage.FileName);
                                ulist.Dis_Name = ul.Dis_Name.Trim();
                                ulist.UserId = ul.Dis_Sap_Code.Trim();
                                ulist.Dis_Sap_Code = ul.Dis_Sap_Code.Trim();
                                ulist.Dis_Email = ul.Dis_Email.Trim();
                                ulist.Dis_ContactNo = ul.Dis_ContactNo.Trim();
                                ulist.Dis_District = ul.Dis_District.Trim();
                                int stateId = int.Parse(stateName);
                              //  var StateNames = db.allstates.Single(a => a.id == stateId);
                                ulist.Dis_Address1 = ul.Dis_Address1;
                                ulist.Dis_Address2 = ul.Dis_Address2;
                              //  ulist.Dis_State = StateNames.statename;
                                ulist.Dis_City = cityName.Trim();
                                ulist.isActive = IsAvtiveName.ToLower() == "on" ? 1 : 0;
                                ulist.ProfileImage = Filename;
                                ulist.CustomerType = CustomerName;
                                int Rolid = int.Parse(RoleName);
                                ulist.RegId = Rolid;
                                //if (CustomerName == "DISTY")
                                //{
                                //    var Destributor = db.DestributorLists.Single(a => a.id == Rolid);
                                //    ulist.DestriId = Destributor.id;
                                //    ulist.RegId = Destributor.RegionId;
                                //}
                                //else
                                //{
                                //    var Dealer = db.Dealers.Single(a => a.id == Rolid);
                                //    ulist.DealerId = Dealer.id;
                                //    ulist.DestriId = Dealer.DestributorId;
                                //    ulist.RegId = Dealer.RegionId;
                                //}
                                ulist.UpdatedbY = Session["userid"].ToString();
                                ulist.UpdatedOn = DateTime.Now;
                                // ulist.ActivatedBY = Session["userid"].ToString();

                                if (db.SaveChanges() > 0)
                                {
                                    ProfileImage.SaveAs(Server.MapPath("~/ProfileImages/") + Filename);
                                    return Content("<script>alert('Record Saved Successfully');location.href='../Users/index';</script>");
                                }
                                else
                                {
                                    return Content("<script>alert('Record Has Not Been Saved Successfully');location.href='../Users/index';</script>");
                                }
                            }
                            #endregion

                            #region If Profile Image Is Null
                            else {

                                ulist.Dis_Name = ul.Dis_Name.Trim();
                                ulist.UserId = ul.Dis_Sap_Code.Trim();
                                ulist.Dis_Sap_Code = ul.Dis_Sap_Code.Trim();
                                ulist.Dis_Email = ul.Dis_Email.Trim();
                                ulist.Dis_ContactNo = ul.Dis_ContactNo.Trim();
                                ulist.Dis_District = ul.Dis_District.Trim();
                                int stateId = int.Parse(stateName);
                              //  var StateNames = db.allstates.Single(a => a.id == stateId);
                                ulist.Dis_Address1 = ul.Dis_Address1;
                                ulist.Dis_Address2 = ul.Dis_Address2;
                              //  ulist.Dis_State = StateNames.statename;
                                ulist.Dis_City = cityName.Trim();
                                ulist.isActive = IsAvtiveName.ToLower() == "on" ? 1 : 0;
                                
                                ulist.CustomerType = CustomerName;
                                int Rolid = int.Parse(RoleName);
                                ulist.RegId = Rolid;
                                //if (CustomerName == "DISTY")
                                //{
                                //    var Destributor = db.DestributorLists.Single(a => a.id == Rolid);
                                //    ulist.DestriId = Destributor.id;
                                //    ulist.RegId = Destributor.RegionId;
                                //    ulist.DealerId = null;
                                //}
                                //else
                                //{
                                //    var Dealer = db.Dealers.Single(a => a.id == Rolid);
                                //    ulist.DealerId = Dealer.id;
                                //    ulist.DestriId = Dealer.DestributorId;
                                //    ulist.RegId = Dealer.RegionId;
                                //}
                                ulist.UpdatedbY = Session["userid"].ToString();
                                ulist.UpdatedOn = DateTime.Now;
                                if (db.SaveChanges() > 0)
                                {                                   
                                    return Content("<script>alert('Record Saved Successfully');location.href='../Users/index';</script>");
                                }
                                else
                                {
                                    return Content("<script>alert('Record Has Not Been Saved Successfully');location.href='../Users/index';</script>");
                                }
                            }
                            #endregion



                        }
                        return View("Edit", db.UsersLists.Single(a => a.id == ul.id));
                        #endregion
                    }
                    else
                    {
                        return RedirectToAction("mnotallowed", "mnotallowed");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("mnotallowed", "mnotallowed");
                }
            }
        
        }

        public JsonResult Delete(int id)
        { 
        if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Users/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                try
                {
                    if (result[0]["deleterole"].ToString() == "1")
                    {

                        UsersList ulist = db.UsersLists.Single(a => a.id == id);

                        #region Save Record In History
                        UsersListHistory uhistory = new UsersListHistory();
                        uhistory.UserlistId = ulist.id;
                        uhistory.CustomerType = ulist.CustomerType;
                        uhistory.DealerId = ulist.DestriId;
                        uhistory.DestriId = ulist.DestriId;
                        uhistory.Dis_Address1 = ulist.Dis_Address1;
                        uhistory.Dis_Address2 = ulist.Dis_Address2;
                        uhistory.Dis_City = ulist.Dis_City;
                        uhistory.Dis_ContactNo = ulist.Dis_ContactNo;
                        uhistory.Dis_District = ulist.Dis_District;
                        uhistory.Dis_Email = ulist.Dis_Email;
                        uhistory.Dis_Name = ulist.Dis_Name;
                        uhistory.Dis_Sap_Code = ulist.Dis_Sap_Code;
                        uhistory.Dis_State = ulist.Dis_State;
                        uhistory.isActive = ulist.isActive;
                        uhistory.ProfileImage = ulist.ProfileImage;
                        uhistory.RegId = ulist.RegId;
                        uhistory.UpdatedbY = Session["userid"].ToString();
                        uhistory.UpdatedOn = DateTime.Now;
                        uhistory.UserId = ulist.UserId;
                        db.UsersListHistories.Add(uhistory);
                        #endregion

                        ulist.isActive = 2;
                        ulist.UpdatedbY = Session["userid"].ToString();
                        ulist.UpdatedOn = DateTime.Now;
                        db.SaveChanges();
                    }
                    return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Exception exs = ex.InnerException;
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);
                }
            }
        

        }

        public ActionResult Details(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/Users/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                try
                {
                    if (result[0]["uview"].ToString() == "1")
                    {
                        return View(db.UsersLists.Single(a => a.isActive != 2 && a.id==id));
                    }
                    else
                    {
                        return RedirectToAction("mnotallowed", "mnotallowed");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("mnotallowed", "mnotallowed");
                }

            }
        }
    }
}
