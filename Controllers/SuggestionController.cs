using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using PagedList;
using PagedList.Mvc;
using System.Data;
using LuminousMpartnerIB.Models;
using LuminousMpartnerIB.EF;

namespace LuminousMpartnerIB.Controllers
{
    public class SuggestionController : MultiLanguageController
    {
        //
        // GET: /Suggestion/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();

        private DataTable dt = new DataTable();
        public ActionResult SuggestionList(int? page)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                var countryList = db.Countries.Select(x => new { x.CountryID, x.CountryName }).ToList();
                ViewBag.countryList = countryList;

                var cd = (from co in db.contactUsDetails
                                     join c in db.Countries
                                     on co.Country equals c.CountryID
                                     join st in db.States
                                     on co.State equals st.StateID
                                     join ci in db.cities
                                     on co.City equals ci.id
                                     where co.Cstatus==1
                                     select new ContactUsModel
                                     {
                                         ID=co.id,
                                         Name=co.Contact_Us_Type,
                                         Address= co.CAddress,
                                         ContactNo=co.PhoneNumber,
                                         Email=co.Email,
                                         Fax= co.Fax,
                                         Country=c.CountryName,
                                         State=st.StateName,
                                         City=ci.cityname

                                     });     

                return View(cd);


            }
        }

        public JsonResult StateList(int countryId)
        {
            try
            {
                if (countryId > 0)
                {
                   // var StaleList = db.States.Where(x=>x.CountryID == countryId).Select(x => new { x.StateID, x.StateName }).ToList();
                    var StaleList = db.States.Select(x => new { x.StateID, x.StateName }).ToList();
                    if (StaleList.Count > 0)
                    {
                        return Json(StaleList, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);

            }
            return Json("Invalid data", JsonRequestBehavior.AllowGet);

        }

        public JsonResult CityList(int stateId)
        {
            try
            {
                if (stateId > 0)
                {
                    // var StaleList = db.States.Where(x=>x.CountryID == countryId).Select(x => new { x.StateID, x.StateName }).ToList();
                    var CityList = db.cities.Where(x=>x.stateid== stateId).Select(x => new { x.id, x.cityname }).ToList();
                    if (CityList.Count > 0)
                    {
                        return Json(CityList, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);

            }
            return Json("Invalid data", JsonRequestBehavior.AllowGet);

        }

        public ActionResult SaveContact(string[] name,string[] address, string[] contactNo, string[] email, string[] fax, string[] country, string[] state, string[] city)
        {
            try
            {
                for (int i=0;i<name.Length;i++)
                {
                    contactUsDetail cd = new contactUsDetail();
                    cd.Contact_Us_Type = name[i];
                    cd.CAddress = address[i];
                    cd.PhoneNumber = contactNo[i];
                    cd.Email = email[i];
                    cd.Fax = fax[i];
                    cd.Cstatus = 1;
                    cd.Created_date = DateTime.Now;
                    cd.CreatedBy = Session["userid"].ToString();
                    cd.Country = Convert.ToInt32(country[i]);
                    cd.State = Convert.ToInt32(state[i]);
                    cd.City = Convert.ToInt32(city[i]);
                    db.contactUsDetails.Add(cd);
                    db.SaveChanges();
                }
                TempData["message"] = "Record Save Successfully!";

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("SuggestionList", "Suggestion");
            // return RedirectToAction("SuggestionList", "Suggestion");
        }



        public ActionResult Edit(int id)
        {
            try
            {
                var countryList = db.Countries.Select(x => new { x.CountryID, x.CountryName }).ToList();
                ViewBag.countryList = countryList;
                var item = (from c in db.contactUsDetails
                            where c.id == id
                            select new 
                            {
                                ID = c.id,
                                Name = c.Contact_Us_Type,
                                Address = c.CAddress,
                                ContactNo = c.PhoneNumber,
                                Email = c.Email,
                                Fax = c.Fax,
                                Country = c.Country,
                                State =  c.State,
                                City = c.City

                            });

                ViewBag.ContactUsModel = item;

                return View("Edit");
            }
            catch (Exception ex)
            {

            }
            return null;          

        }


        public ActionResult Update(int id,string name, string address, string contactNo, string email, string fax, string country, string state,string city)
        {

            try
            {
                var cd = db.contactUsDetails.Where(x => x.id == id).FirstOrDefault();
                cd.Contact_Us_Type = name;
                cd.CAddress = address;
                cd.PhoneNumber = contactNo;
                cd.Email = email;
                cd.Fax = fax;
                cd.Cstatus = 1;
                cd.ModifiedDate = DateTime.Now;
                cd.ModifiedBy = Session["userid"].ToString();
                cd.Country = Convert.ToInt32(country);
                cd.State = Convert.ToInt32(state);
                cd.City = Convert.ToInt32(city);               
                db.SaveChanges();
                TempData["message"] = "Record Updated Successfully!";
                return RedirectToAction("SuggestionList", "Suggestion");

            }
            catch(Exception ex)
            {

            }


            return null;

        }

        public ActionResult Delete(int id)
        {

            try
            {
                var cd = db.contactUsDetails.Where(x => x.id == id).FirstOrDefault();                
                cd.Cstatus = 0;
                cd.ModifiedDate = DateTime.Now;
                cd.ModifiedBy = Session["userid"].ToString();                
                db.SaveChanges();
                TempData["message"] = "Record Deleted Successfully!";
                return RedirectToAction("SuggestionList", "Suggestion");

            }
            catch (Exception ex)
            {

            }

            return null;

        }

    }
}
