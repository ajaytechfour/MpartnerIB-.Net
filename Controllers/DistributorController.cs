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
    public class DistributorController : Controller
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
                return View();
            }
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
                                  id= vs.id,
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


        public JsonResult getStateMasters()
        {
            var getStateMasterslst = (from c in db.States
                                          // where c.status != 0
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
                var echkexist = db.UsersLists.Where(x => x.Dis_Name.Trim().ToLower() == obj.Name.Trim().ToLower() & x.CustomerType == "DISTY").ToList();
                if (echkexist.Count() > 0)
                {
                    data = new { result = new List<UsersListModel>(), TotalRecord = tcount, Message = "", MessageExist = "This Distributor Already Exists" };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }



                UsersList objUsersList = new UsersList();

                objUsersList.Dis_Name = obj.Name;
                objUsersList.Dis_Address1 = obj.Address;
                objUsersList.Dis_ContactNo = obj.ContactNo;
                objUsersList.Dis_Email = obj.Email;
                objUsersList.Dis_State = obj.State;
                objUsersList.Dis_City = obj.City;

                objUsersList.UserId = userid;
                objUsersList.CustomerType = "DISTY";
                objUsersList.Dis_Sap_Code = userid;
                objUsersList.CreatedBY = userid;
                objUsersList.CreatedON = DateTime.Now;

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
            return Json(data, JsonRequestBehavior.AllowGet);
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
