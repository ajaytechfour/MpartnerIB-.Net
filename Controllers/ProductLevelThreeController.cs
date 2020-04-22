using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuminousMpartnerIB.EF;
using System.Data;
namespace LuminousMpartnerIB.Controllers
{
    public class ProductLevelThreeController : Controller
    {
        //
        // GET: /ProductLevelThree/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        private string PageUrl = "/ProductLevelThree/index";
        public JsonResult GetProductLevel3(string productid,string ProductlvlOneid,string ProductLvlTwoId)
        {
            int pid,poneId,PtwoId;

            int.TryParse(productid,out pid);
            int.TryParse(ProductlvlOneid,out poneId);
            int.TryParse(ProductLvlTwoId,out PtwoId);

            var Company = (from c in db.ProductLevelThrees
                           where c.PlTwStatus != 2 && c.PlTwStatus != 0 && c.productCategoryid == pid && c.ProductLevelOne == poneId && c.pc_Lv2_oneId == PtwoId
                           select new
                           {
                               id = c.id,
                               Name = c.Name
                           }).ToList();
            return Json(Company, JsonRequestBehavior.AllowGet);
        }
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

        public ActionResult SaveContact(ProductLevelThreef contactUs, string statusC, string pcId, string ProductCat1, string ProductCat2)
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
                    int pcid;
                    int productCat1;
                    int productCat2;
                    

                    if (!int.TryParse(pcId, out pcid))
                    {
                        ModelState.AddModelError("PrductID", "Select Product Category");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("PrductID", "Select Product Category");
                    }

                    if (!int.TryParse(ProductCat1, out productCat1))
                    {
                        ModelState.AddModelError("pc_Lvl_Id", "Select Product Level One");

                    }
                    if (productCat1 == 0)
                    {
                        ModelState.AddModelError("pc_Lvl_Id", "Select Product Level One");
                    }




                    if (!int.TryParse(ProductCat2, out productCat2))
                    {
                        ModelState.AddModelError("pc_Lv2_Id", "Select Product Level Two");

                    }
                    if (productCat2 == 0)
                    {
                        ModelState.AddModelError("pc_Lv2_Id", "Select Product Level Two");
                    }




                    if (db.ProductLevelThreefs.Any(a => a.PrductID == pcid && a.pc_Lvl_Id == productCat1 && a.Name.ToLower() == contactUs.Name.ToLower() && a.pc_Lv2_Id ==productCat2 && a.PlTwStatus != 2))
                    {
                        ModelState.AddModelError("Name", "Product Name Already Exists");
                    }

                    if (contactUs.Name == "" || contactUs.Name == null)
                    {
                        ModelState.AddModelError("Name", "Product Level Theree Is Empty");
                    }
                    if (contactUs.Name != null)
                    {
                        if (contactUs.Name.Length > 99)
                        {
                            ModelState.AddModelError("Name", "Character Should Be Less Than 100");
                        }
                    }

                    if (ModelState.IsValid)
                    {
                        ProductLevelThreef contactusd = new ProductLevelThreef();
                        contactusd.Name = contactUs.Name;
                        contactusd.PrductID = pcid;
                        contactusd.pc_Lvl_Id = productCat1;
                        contactusd.pc_Lv2_Id = productCat2;

                        contactusd.CreatedBy = Session["userid"].ToString();
                        contactusd.CreatedDate = DateTime.Now;
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.PlTwStatus = 1;
                        }
                        else
                        {
                            contactusd.PlTwStatus = 0;
                        }
                        db.ProductLevelThreefs.AddObject(contactusd);
                        int affectedValue = db.SaveChanges();
                        if (affectedValue > 0)
                        {
                            ViewBag.result = "Record Saved Successfully";
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
                    var contactdetails = (from c in db.ProductLevelThreefs
                                          where c.PlTwStatus != 2
                                          select c).ToList();
                    int totalrecord;
                    if (page != null)
                    {
                        page = (page - 1) * 15;
                    }

                    var contactDetails2 = (from c in contactdetails

                                           select new
                                           {
                                               Productid = c.ProductCatergory.PName,
                                               ProductlevelOne = c.ProductLevelOne.Name,
                                               ProductlevelTwo = c.ProductLevelTwo.Name,
                                               PName = c.Name,
                                               status = c.PlTwStatus == 1 ? "Enable" : "Disable",
                                               id = c.id

                                           }).OrderByDescending(a => a.id).Skip(page ?? 0).Take(15).ToList();
                    if (contactdetails.Count() % 15 == 0)
                    {
                        totalrecord = contactdetails.Count() / 15;
                    }
                    else
                    {
                        totalrecord = (contactdetails.Count() / 15) + 1;
                    }
                    TempData["paging"] = totalrecord;

                    return Json(contactDetails2, JsonRequestBehavior.AllowGet);
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
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    ProductLevelThreef cud = db.ProductLevelThreefs.Single(a => a.id == id);

                    ViewBag.status = cud.PlTwStatus;
                    ViewBag.ProductCat = cud.PrductID;
                    ViewBag.ProductCat1 = cud.pc_Lvl_Id;
                    ViewBag.ProductCat2 = cud.pc_Lv2_Id;
                    return View(cud);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        public ActionResult Update(ProductLevelThreef contactUs, string statusC, string pcId, string ProductCat1, string ProductCat2)
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
                    ViewBag.ProductCat = pcId;
                    ViewBag.ProductCat1 = ProductCat1;
                    ViewBag.ProductCat2 = ProductCat2;
                    int pcid;
                    int productCat1;
                    int productCat2;
                    if (!int.TryParse(pcId, out pcid))
                    {
                        ModelState.AddModelError("PrductID", "Select Product Category");

                    }
                    if (pcid == 0)
                    {
                        ModelState.AddModelError("PrductID", "Select Product Category");
                    }

                    if (!int.TryParse(ProductCat1, out productCat1))
                    {
                        ModelState.AddModelError("pc_Lvl_Id", "Select Product level One");

                    }
                    if (productCat1 == 0)
                    {
                        ModelState.AddModelError("pc_Lvl_Id", "Select Product level One");
                    }

                    if (!int.TryParse(ProductCat2, out productCat2))
                    {
                        ModelState.AddModelError("pc_Lv2_Id", "Select Product level Two");

                    }
                    if (productCat2 == 0)
                    {
                        ModelState.AddModelError("pc_Lv2_Id", "Select Product level Two");
                    }

                    if (db.ProductLevelThreefs.Any(a => a.PrductID == pcid && a.pc_Lvl_Id == productCat1 && a.Name.ToLower() == contactUs.Name.ToLower() && a.pc_Lv2_Id == productCat2 && a.id != contactUs.id && a.PlTwStatus !=2))
                    {
                        ModelState.AddModelError("Name", "Product Name Already Exists");
                    }

                    if (contactUs.Name == "" || contactUs.Name == null)
                    {
                        ModelState.AddModelError("Name", "Product Level Theree Is Empty");
                    }
                    if (contactUs.Name != null)
                    {
                        if (contactUs.Name.Length > 99)
                        {
                            ModelState.AddModelError("Name", "Character Should Be Less Than 100");
                        }
                    }
                    if (ModelState.IsValid)
                    {
                        ProductLevelThreef contactusd = db.ProductLevelThreefs.Single(a => a.id == contactUs.id);

                        ProductLevelThreefHistory CUDHistory = new ProductLevelThreefHistory();

                        CUDHistory.Name = contactusd.Name;
                        CUDHistory.ProductLevelThreeId = contactusd.id;
                        CUDHistory.PrductID = contactusd.PrductID;
                        CUDHistory.pc_Lvl_Id = contactusd.pc_Lvl_Id;
                        CUDHistory.pc_Lv2_Id = contactusd.pc_Lv2_Id;
                        CUDHistory.ModifiedDate = DateTime.Now;
                        CUDHistory.ModifiedBy = Session["userid"].ToString();
                        CUDHistory.PlTwStatus = contactusd.PlTwStatus;                      
                        db.ProductLevelThreefHistories.AddObject(CUDHistory);
                        

                        contactusd.Name = contactUs.Name;
                        contactusd.PrductID = pcid;
                        contactusd.pc_Lvl_Id = productCat1;
                        contactusd.pc_Lv2_Id = productCat2;
                        contactusd.ModifiedDate = DateTime.Now;
                        contactusd.ModifiedBy = Session["userid"].ToString();
                        string status = statusC ?? "off";
                        if (status == "on")
                        {
                            contactusd.PlTwStatus = 1;
                        }
                        else
                        {
                            contactusd.PlTwStatus = 0;
                        }


                        int affectedRows = db.SaveChanges();
                        if (affectedRows > 0)
                        {


                            ViewBag.Result = "Record Updated Successfully";

                           
                        }


                    }


                    return View("Edit");
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
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
                dt = Session["permission"] as DataTable;
                string pageUrl2 = PageUrl;
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["deleterole"].ToString() == "1")
                {
                    ProductLevelThreef contactUs = db.ProductLevelThreefs.Single(a => a.id == id);
                    ProductLevelThreefHistory CUDHistory = new ProductLevelThreefHistory();
                    CUDHistory.Name = contactUs.Name;
                    CUDHistory.ProductLevelThreeId = contactUs.id;
                    CUDHistory.PrductID = contactUs.PrductID;
                    CUDHistory.pc_Lvl_Id = contactUs.pc_Lvl_Id;
                    CUDHistory.pc_Lv2_Id = contactUs.pc_Lv2_Id;
                    CUDHistory.ModifiedDate = DateTime.Now;
                    CUDHistory.ModifiedBy = Session["userid"].ToString();
                    CUDHistory.PlTwStatus = contactUs.PlTwStatus;

                    db.ProductLevelThreefHistories.AddObject(CUDHistory);

                    contactUs.PlTwStatus = 2;
                    contactUs.ModifiedDate = DateTime.Now;
                    contactUs.ModifiedBy = Session["userid"].ToString();
                    db.SaveChanges();
                    return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }

        }

    }
}
