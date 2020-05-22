using LuminousMpartnerIB.EF;
using LuminousMpartnerIB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TVS;

namespace LuminousMpartnerIB.Controllers
{
    public class DistributorController : MultiLanguageController
    {
        datautility dut = new datautility();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        LuminousMpartnerIBEntities db;
        List<SideBarMenuModel> sideBarMenuLst = new List<SideBarMenuModel>();
        string userid = string.Empty;

        public DistributorController()
        {
            db = new LuminousMpartnerIBEntities();
        }

        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                UsersListModel obj = new UsersListModel();
                obj.ddlSelectLanguage = PopulateLanguage();
                return View(obj);
            }
        }

        private List<SelectListItem> PopulateLanguage()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var getLanguagelst = (from c in db.LanguageMasters
                                  where c.Status != 0
                                  select new
                                  {
                                      id = c.LangCode,
                                      Name = c.Lang
                                  }).ToList();

            foreach (var item in getLanguagelst)
            {
                items.Add(new SelectListItem
                {
                    Text = item.Name.ToString(),
                    Value = item.id.ToString()
                });
            }

            return items;
        }

        public JsonResult GetGridData()
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var getGrid = from vs in db.UsersLists
                              where vs.CustomerType == "DISTY"
                              orderby vs.CustomerType
                              select new
                              {
                                  id = vs.id,
                                  UserId = vs.UserId,
                                  CustomerType = vs.CustomerType,
                                  Name = vs.Dis_Name,
                                  Address = vs.Dis_Address1,
                                  City = vs.Dis_City,
                                  State = vs.Dis_State,
                                  ContactNo = vs.Dis_ContactNo,
                                  Email = vs.Dis_Email,
                              };


                if (getGrid != null)
                {


                    var data = new { result = getGrid, TotalRecord = getGrid.Count() };
                    return Json(data, JsonRequestBehavior.AllowGet);
                    // }
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }

            }
        }


        public JsonResult getLanguagMastere()
        {
            var getLanguagMasterelst = (from c in db.LanguageMasters
                                            // where c.status != 0
                                        select new
                                        {
                                            id = c.Id,
                                            Name = c.Lang
                                        }).ToList();

            if (getLanguagMasterelst != null)
            {
                return Json(getLanguagMasterelst, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Null", JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult getCountryMasters()
        {
            var getStateMasterslst = (from c in db.Countries
                                          // where c.status != 0
                                      select new
                                      {
                                          id = c.CountryID,
                                          Name = c.CountryName
                                      }).ToList();

            if (getStateMasterslst != null)
            {
                return Json(getStateMasterslst, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Null", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getStateByCountry(int countryId)
        {
            var getStateMasterslst = (from c in db.States
                                      where c.CountryID == countryId
                                      select new
                                      {
                                          id = c.StateID,
                                          Name = c.StateName
                                      }).ToList();

            if (getStateMasterslst != null)
            {
                return Json(getStateMasterslst, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Null", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCityBystate(string stateId)
        {
            if (Session["userid"] == null)
            {
                return Json("Login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (stateId != "")
                {
                    int id = Convert.ToInt32(stateId);

                    //var getCity = db.cities.Where(x => x.stateid == id).ToList();

                    var getCity = (from c in db.cities
                                   where c.stateid == id
                                   select new
                                   {
                                       id = c.id,
                                       Name = c.cityname
                                   }).ToList();


                    return Json(getCity, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

            }
        }


        public JsonResult saveData(UsersListModel obj)
        {
            List<UsersListModel> UsersListModellst = new List<UsersListModel>();

            if (Session["userid"] != null)
            {
                userid = Session["userid"].ToString();
            }

            int tcount = 0;
            var data = new { result = new List<UsersListModel>(), TotalRecord = tcount, Message = "", MessageExist = "" };

            if (obj == null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {               
                UsersList objUsersList = new UsersList();

                if (obj.id != null)
                {
                    objUsersList = db.UsersLists.Single(a => a.id == obj.id);

                    objUsersList.Dis_Name = obj.Name;
                    objUsersList.Dis_Address1 = obj.Address;
                    objUsersList.Dis_ContactNo = obj.ContactNo;
                    objUsersList.Dis_Email = obj.Email;
                    objUsersList.Dis_State = obj.State;
                    objUsersList.Dis_City = obj.City;

                    objUsersList.UserId = userid;
                    objUsersList.CustomerType = "DISTY";
                   // objUsersList.Dis_Sap_Code = userid;
                    //objUsersList.CreatedBY = userid;
                    //objUsersList.CreatedON = DateTime.Now;

                    objUsersList.Dis_Sap_Code = obj.SapCode;
                    objUsersList.Country = obj.Country;

                    string selectedLanguagecode = string.Empty;
                    string[] language = obj.ddlSelectLanguageIDs;
                    if (language != null)
                    {
                        foreach (var item in language)
                        {
                            if (selectedLanguagecode != "")
                            {
                                selectedLanguagecode = selectedLanguagecode + "," + item.ToString();
                            }
                            else
                            {
                                selectedLanguagecode = item.ToString();
                            }
                        }
                    }
                    objUsersList.Language = selectedLanguagecode;

                    objUsersList.UpdatedbY = userid;
                    objUsersList.UpdatedOn = DateTime.Now;

                    //db.UsersLists.Add(objUsersList);
                    if (db.SaveChanges() > 0)
                    {

                        var griddata = from vs in db.UsersLists
                                       where vs.CustomerType == "DISTY"
                                       orderby vs.CustomerType
                                       select new UsersListModel
                                       {
                                           UserId = vs.UserId,
                                           CustomerType = vs.CustomerType,
                                           Name = vs.Dis_Name,
                                           Address = vs.Dis_Address1,
                                           City = vs.Dis_City,
                                           State = vs.Dis_State,
                                           ContactNo = vs.Dis_ContactNo,
                                           Email = vs.Dis_Email,
                                           //CreatedBY = vs.CreatedBY,
                                       };

                        UsersListModellst = griddata.ToList();


                        data = new { result = UsersListModellst, TotalRecord = UsersListModellst.Count(), Message = "Data save successfully", MessageExist = "" };

                    }

                    else
                    {
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {

                    //var echkexist = db.UsersLists.Where(x => x.Dis_Name.Trim().ToLower() == obj.Name.Trim().ToLower() & x.CustomerType == "DISTY").ToList();
                    var echkexist = db.UsersLists.Where(x => x.Dis_Sap_Code == obj.SapCode & x.CustomerType == "DISTY").ToList();

                    if (echkexist.Count() > 0)
                    {
                        data = new { result = new List<UsersListModel>(), TotalRecord = tcount, Message = "", MessageExist = "This Sap Code Already Exists" };
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }

                    objUsersList.Dis_Name = obj.Name;
                    objUsersList.Dis_Address1 = obj.Address;
                    objUsersList.Dis_ContactNo = obj.ContactNo;
                    objUsersList.Dis_Email = obj.Email;
                    objUsersList.Dis_State = obj.State;
                    objUsersList.Dis_City = obj.City;

                    objUsersList.UserId = userid;
                    objUsersList.CustomerType = "DISTY";
                   // objUsersList.Dis_Sap_Code = userid;
                    objUsersList.CreatedBY = userid;
                    objUsersList.CreatedON = DateTime.Now;

                    objUsersList.Dis_Sap_Code = obj.SapCode;
                    objUsersList.Country = obj.Country;

                    string selectedLanguagecode = string.Empty;
                    string[] language = obj.ddlSelectLanguageIDs;
                    if (language != null)
                    {
                        foreach (var item in language)
                        {
                            if (selectedLanguagecode != "")
                            {
                                selectedLanguagecode = selectedLanguagecode + "," + item.ToString();
                            }
                            else
                            {
                                selectedLanguagecode = item.ToString();
                            }
                        }
                    }
                    objUsersList.Language = selectedLanguagecode;

                    db.UsersLists.Add(objUsersList);
                    if (db.SaveChanges() > 0)
                    {

                        var griddata = from vs in db.UsersLists
                                       where vs.CustomerType == "DISTY"
                                       orderby vs.CustomerType
                                       select new UsersListModel
                                       {
                                           UserId = vs.UserId,
                                           CustomerType = vs.CustomerType,
                                           Name = vs.Dis_Name,
                                           Address = vs.Dis_Address1,
                                           City = vs.Dis_City,
                                           State = vs.Dis_State,
                                           ContactNo = vs.Dis_ContactNo,
                                           Email = vs.Dis_Email,
                                           //CreatedBY = vs.CreatedBY,
                                       };

                        UsersListModellst = griddata.ToList();


                        data = new { result = UsersListModellst, TotalRecord = UsersListModellst.Count(), Message = "Data save successfully", MessageExist = "" };

                    }

                    else
                    {
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }

                }               
                               
            }
            return Json(data, JsonRequestBehavior.AllowGet);
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
                if (true)
                {

                    UsersListModel obj = new UsersListModel();
                    obj.ddlSelectLanguage = PopulateLanguage();

                    UsersList cud = db.UsersLists.Single(a => a.id == id);


                    ViewBag.id = cud.id;
                    ViewBag.EmployeeName = cud.Dis_Name;
                    ViewBag.EmployeeAddress = cud.Dis_Address1;
                    ViewBag.ContactNo = cud.Dis_ContactNo;
                    ViewBag.Email = cud.Dis_Email;
                    ViewBag.SapCode = cud.Dis_Sap_Code;
                    ViewBag.Country = cud.Country;
                    ViewBag.State = cud.Dis_State;
                    ViewBag.City = cud.Dis_City;

                    return View(obj);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }



        public JsonResult Delete(int id)
        {
            if (Session["userid"] == null)
            {
                return Json("login", JsonRequestBehavior.AllowGet);
            }
            else
            {
                //VisitSchedule contactusd = db.VisitSchedules.Single(a => a.ID == id);

                UsersList usersList = db.UsersLists.Where(x => x.id == id).FirstOrDefault();
                db.UsersLists.Remove(usersList);

                if (db.SaveChanges() > 0)
                {
                    return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Record Not Deleted", JsonRequestBehavior.AllowGet);
                }

            }
        }

    }
}
