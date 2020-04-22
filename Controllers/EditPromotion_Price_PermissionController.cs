using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.Text.RegularExpressions;
using System.Data;
namespace Luminous.Controllers
{
    public class EditPromotion_Price_PermissionController : Controller
    {
        //
        // GET: /EditPromotionPermission/
        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/CreatePermotionPrice_New/Index";
        public ActionResult Index(int id)
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
                    Session["id"] = id;

                    //List<ProductAccessTable> Pat = db.ProductAccessTables.Where(a => a.promotionid == id && (a.deleted != true || a.deleted == null || a.deleted == false)).ToList();
                    List<GetPromotionAccessTable_Price_Scheme_Result> Pat = db.GetPromotionAccessTable_Price_Scheme(id).ToList();
                    return View(Pat);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        public ActionResult Save(string Alls, string rglist, string DistriCheck, string DealCheck)
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
                    rglist = rglist ?? "";
                    DealCheck = DealCheck ?? "off";
                    DistriCheck = DistriCheck ?? "off";
                    int id = Convert.ToInt32(Session["id"].ToString());

                    #region Check Validation for permission
                    //if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (rglist == "" || rglist == null || rglist == "0"))
                    //{
                    //    ModelState.AddModelError("RegionName", "Permission For Has No Value");

                    //}
                    //else if ((Alls.ToLower() == "off" || Alls == "" || Alls == null)&&(DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    //{
                    //    ModelState.AddModelError("RegionName", "Check Eiter Distributor OR Dealer");
                    //}



                    if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (rglist == "" || rglist == null || rglist == "0") && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    {

                        ModelState.AddModelError("RegionName", "Permission For Has No Value");

                    }
                    else if ((Alls.ToLower() == "off" || Alls == "" || Alls == null) && (DealCheck == null || DealCheck == "off") && (DistriCheck == null || DistriCheck == "off"))
                    {
                        ModelState.AddModelError("RegionName", "Check Either Distributor OR Dealer");
                    }

                    #endregion
                    if (ModelState.IsValid)
                    {

                        List<Price_SchemeAccessTable> patDel = db.Price_SchemeAccessTable.Where(a => a.promotionid == id).ToList();
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
                                    Price_SchemeAccessTable pat = new Price_SchemeAccessTable();
                                    pat.promotionid = id;
                                    pat.RegId = int.Parse(s);
                                    pat.createdate = DateTime.Now;
                                    pat.AllDealerAccess = DealCheck == "off" ? false : true;
                                    pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                                    pat.createby = Session["userid"].ToString();
                                    db.Price_SchemeAccessTable.AddObject(pat);
                                    db.SaveChanges();

                                }
                            }
                        }
                        else
                        {
                            Price_SchemeAccessTable pat = new Price_SchemeAccessTable();
                            pat.promotionid = id;
                            pat.AllAcess = true;
                            db.Price_SchemeAccessTable.AddObject(pat);
                            db.SaveChanges();
                        }

                        if ((Alls.ToLower() != "on") && (rglist == "" || rglist == null) && (DistriCheck != "on" || DealCheck != "on"))
                        {
                            Price_SchemeAccessTable pat = new Price_SchemeAccessTable();
                            pat.promotionid = id;
                            pat.RegId = null;
                            pat.createdate = DateTime.Now;
                            pat.AllDealerAccess = DealCheck == "off" ? false : true;
                            pat.AllDestriAccess = DistriCheck == "off" ? false : true;
                            pat.createby = Session["userid"].ToString();
                            db.Price_SchemeAccessTable.AddObject(pat);
                            db.SaveChanges();
                        }
                        #region Commented Code
                        //if (Alls.ToLower() != "on")
                        //{
                        //    if (rglist != "" && rglist != null)
                        //    {
                        //        Regex rg = new Regex(",");
                        //        string[] reglist = rg.Split(rglist);
                        //        foreach (string s in reglist)
                        //        {
                        //            ProductAccessTable pat = new ProductAccessTable();
                        //            pat.promotionid = id;
                        //            pat.RegId = int.Parse(s);
                        //            pat.createdate = DateTime.Now;
                        //            pat.createby = Session["userid"].ToString();
                        //            db.ProductAccessTables.AddObject(pat);
                        //            db.SaveChanges();

                        //        }
                        //    }


                        //    if (disList != "0" && disList != "" && disList != null)
                        //    {
                        //        Regex rg = new Regex(",");
                        //        string[] reglist = rg.Split(disList);
                        //        foreach (string s in reglist)
                        //        {
                        //            ProductAccessTable pat2 = new ProductAccessTable();
                        //            pat2.promotionid = id;
                        //            pat2.DestributorID = int.Parse(s);
                        //            pat2.createdate = DateTime.Now;
                        //            pat2.createby = Session["userid"].ToString();
                        //            db.ProductAccessTables.AddObject(pat2);
                        //            db.SaveChanges();

                        //        }
                        //    }
                        //    else
                        //    {
                        //        ProductAccessTable pat2 = new ProductAccessTable();
                        //        pat2.promotionid = id;
                        //        pat2.AllDestriAccess = true;
                        //        pat2.createdate = DateTime.Now;
                        //        pat2.createby = Session["userid"].ToString();
                        //        db.ProductAccessTables.AddObject(pat2);
                        //        db.SaveChanges();

                        //    }



                        //    if (Dealist != "0" && Dealist != "" && Dealist != null)
                        //    {
                        //        Regex rg = new Regex(",");
                        //        string[] reglist = rg.Split(Dealist);
                        //        foreach (string s in reglist)
                        //        {
                        //            ProductAccessTable pat3 = new ProductAccessTable();
                        //            pat3.promotionid = id;
                        //            pat3.DealerId = int.Parse(s);
                        //            pat3.createdate = DateTime.Now;
                        //            pat3.createby = Session["userid"].ToString();
                        //            db.ProductAccessTables.AddObject(pat3);
                        //            db.SaveChanges();

                        //        }
                        //    }
                        //    else
                        //    {
                        //        ProductAccessTable pat2 = new ProductAccessTable();
                        //        pat2.promotionid = id;
                        //        pat2.AllDealerAccess = true;
                        //        pat2.createdate = DateTime.Now;
                        //        pat2.createby = Session["userid"].ToString();
                        //        db.ProductAccessTables.AddObject(pat2);
                        //        db.SaveChanges();

                        //    }
                        //}
                        //else
                        //{
                        //    ProductAccessTable pat3 = new ProductAccessTable();
                        //    pat3.promotionid = id;
                        //    pat3.AllAcess = true;
                        //    pat3.createdate = DateTime.Now;
                        //    pat3.createby = Session["userid"].ToString();
                        //    db.ProductAccessTables.AddObject(pat3);
                        //    db.SaveChanges();
                        //}
                        #endregion

                        return RedirectToAction("Index", new { id = id });
                    }
                    return Content("<script>alert('Permisson For Has No Value');location.href='../EditPromotion_Price_Permission/index/" + id + "';</script>");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }


    }
}
