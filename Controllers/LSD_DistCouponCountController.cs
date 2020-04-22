using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Data;
using LuminousMpartnerIB.EF;
using System.IO;
using System.Data.OleDb;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Globalization;
using System.Web.UI;
using System.Text;
namespace LuminousMpartnerIB.Controllers
{
    public class LSD_DistCouponCountController : Controller
    {
        //
        // GET: /LSD_DistCouponCount/

         private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        public ActionResult Index(string Search)
        {

            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                if (Search != null && Search != "")
                {
                    Session["Search"] = Search;

                }
                else
                {
                    Session.Remove("Search");
                }
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LSD_DistCouponCount/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    return View(db.Lsd_DistCouponCount.Where(a => a.Id == null).OrderByDescending(a => a.Id).ToList().ToPagedList(1, 1));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        public ActionResult ExportToExcel()
        {


            // var DealerData = db.LSD_Master.Where(c => c.GiftId == 0).Select(c => new { c.QrCode, c.Barcode, c.SecretCode, c.BundleCode, c.GiftId }).ToList();

            var Distcouponcount = db.Lsd_DistCouponCount.Select(c => new { c.DistCode, c.EligibleCoupon}).ToList();

            if (Distcouponcount.Count() > 0)
            {
                DataTable table = new DataTable();
                table.Columns.Add("DistCode", typeof(string));
                table.Columns.Add("EligibleCoupon", typeof(string));

                foreach (var str in Distcouponcount)
                {
                    DataRow row = table.NewRow();
                    row["DistCode"] = str.DistCode;
                    row["EligibleCoupon"] = str.EligibleCoupon;

                    table.Rows.Add(row);
                }

                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet sheet = pck.Workbook.Worksheets.Add("EligibleCoupon");

                    #region Header_Format
                    sheet.Cells["A1"].Value = "DistCode";
                    sheet.Cells["B1"].Value = "EligibleCoupon";
                    

                    var cellH = sheet.Cells["A1:B1"];
                    cellH.Style.Font.Size = 12;
                    cellH.Style.Font.Name = "Calibri";
                    #endregion Header_Format
                    int intRowCount = 2;
                    foreach (DataColumn col in table.Columns)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            sheet.Cells["A" + Convert.ToString(intRowCount)].Value = row["DistCode"].ToString();
                            sheet.Cells["B" + Convert.ToString(intRowCount)].Value = row["EligibleCoupon"].ToString();
                            

                            intRowCount++;
                        }
                        break;
                    }
                    sheet.Cells.AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", String.Format(CultureInfo.InvariantCulture, "attachment; filename=EligibleCoupon.xlsx"));
                    Response.BinaryWrite(pck.GetAsByteArray());
                    Response.End();

                    return View();
                }
            }
            else
            {
                TempData["NoData"] = "There is some issue in excel download format";
                return View("Index");
            }
        }

        public ActionResult CouponMappingBulkImport(HttpPostedFileBase FileUpload)
        {
            string msg = "";


            DataTable table = new DataTable();
            table.Columns.Add("DistCode", typeof(string));
            table.Columns.Add("Message", typeof(string));
            try
            {
                //var dt = FileUpload.ContentLength;
                if (FileUpload != null)
                {
                    string connString = "";
                    //string strFileType = Path.GetExtension(FileUpload.FileName).ToLower();
                    //string path = FileUpload.FileName;
                    string ext = System.IO.Path.GetExtension(Path.GetFileName(FileUpload.FileName));
                    var originalfilename = Path.GetFileName(FileUpload.FileName);
                    if (ext == ".xlsx")
                    {
                        TempData["extension"] = null;
                        TempData["Successfully"] = null;
                        TempData["Blankfile"] = null;
                        string[] changeextension = originalfilename.Split('.');
                        string xlsextension = changeextension[0] + ".xlsx";

                        string path =
Path.Combine(Path.GetDirectoryName(xlsextension)
, string.Concat(Path.GetFileNameWithoutExtension(xlsextension)
, DateTime.Now.ToString("_yyyy_MM_dd_HH_MM_ss")
, Path.GetExtension(xlsextension)
)
);

                        //var pathfolder = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/"), path);
                        var pathfolder = Path.Combine(Server.MapPath("~/Upload/"), path);

                        if (System.IO.File.Exists(pathfolder))
                        {
                            System.IO.File.Delete(pathfolder);
                        }


                        FileUpload.SaveAs(pathfolder);


                        //Connection String to Excel Workbook

                        if (ext.Trim() == ".xls")
                        {
                            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathfolder + ";Extended Properties='Excel 8.0;HDR=Yes'";
                        }
                        else if (ext.Trim() == ".xlsx")
                        {
                            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathfolder + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                        }
                        //connString = String.Format(connString, pathfolder, "Yes");
                        OleDbConnection conn = new OleDbConnection(connString);
                        if (conn.State == ConnectionState.Closed)

                            msg = "aaa";
                        conn.Open();
                        msg = "bbb";
                        string query = "SELECT [DistCode],[EligibleCoupon] FROM [EligibleCoupon$]";

                        OleDbCommand cmd = new OleDbCommand(query, conn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            var distcode = ds.Tables[0].Rows[i]["DistCode"].ToString();
                            var eligiblecoupon = ds.Tables[0].Rows[i]["EligibleCoupon"].ToString();

                            var distcodeexist = db.Lsd_DistCouponCount.Where(c => c.DistCode == distcode).ToList();
                            //var check_validdistributor = db.UsersLists.Where(c => c.UserId == distcode).Count();
                            //if (check_validdistributor > 0)
                            //{
                                if (distcodeexist.Count > 0)
                                {
                                    var distactivatedcount = db.Lsd_DistCouponCount.Where(c => c.DistCode == distcode).Select(c => c.DistActivatedCount).SingleOrDefault();
                                    if (Convert.ToInt32(eligiblecoupon) >= distactivatedcount)
                                    {

                                        var lsd_previuoscouponcount = db.Lsd_DistCouponCount.Single(c => c.DistCode == distcode);
                                        if (lsd_previuoscouponcount.EligibleCoupon != Convert.ToInt32(eligiblecoupon))
                                        {
                                            Lsd_DistCouponCount_History lsdcount = new Lsd_DistCouponCount_History();
                                            lsdcount.DistCode = lsd_previuoscouponcount.DistCode;
                                            lsdcount.EligibleCoupon = lsd_previuoscouponcount.EligibleCoupon;
                                            lsdcount.P_DistCouponCountId = lsd_previuoscouponcount.Id;
                                            lsdcount.DistBalanceCount = Convert.ToInt32(lsd_previuoscouponcount.EligibleCoupon);
                                            lsdcount.DistActivatedCount = lsd_previuoscouponcount.DistActivatedCount;
                                            lsdcount.DistClaimedCount = lsd_previuoscouponcount.DistClaimedCount;
                                            lsdcount.DealerRedeemedCount = lsd_previuoscouponcount.DealerRedeemedCount;
                                            lsdcount.CreatedBy = lsd_previuoscouponcount.CreatedBy;
                                            lsdcount.CreatedOn = lsd_previuoscouponcount.CreatedOn;
                                            lsdcount.ModifiedBy = Session["userid"].ToString();
                                            lsdcount.ModifiedOn = DateTime.Now;
                                            lsdcount.Status = 1;
                                            db.Lsd_DistCouponCount_History.AddObject(lsdcount);
                                            db.SaveChanges();
                                        }

                                        var distbalancecount = Convert.ToInt32(eligiblecoupon) - Convert.ToInt32(distactivatedcount);

                                        db.ExecuteStoreCommand("Update Lsd_DistCouponCount set EligibleCoupon='" + eligiblecoupon + "',DistBalanceCount='" + distbalancecount + "' where DistCode='" + distcode + "'");



                                    }
                                    else
                                    {

                                        DataRow row = table.NewRow();
                                        row["DistCode"] = distcode;
                                        row["Message"] = "Eligible coupon cannot be less than activated coupon";

                                        table.Rows.Add(row);



                                    }
                                }
                                else
                                {
                                    Lsd_DistCouponCount lsdcount = new Lsd_DistCouponCount();
                                    lsdcount.DistCode = distcode;
                                    lsdcount.EligibleCoupon = Convert.ToInt32(eligiblecoupon);
                                    lsdcount.DistActivatedCount = 0;
                                    lsdcount.DistBalanceCount = Convert.ToInt32(eligiblecoupon);
                                    lsdcount.Gold_ActivatedCouponCount = 0;
                                    lsdcount.Gold_BalanceCouponCount = 0;
                                    lsdcount.Gold_EligibleCouponCount = 0;
                                    lsdcount.DistClaimedCount = 0;
                                    lsdcount.DealerRedeemedCount = 0;
                                    lsdcount.CreatedBy = "Admin";
                                    lsdcount.CreatedOn = DateTime.Now;
                                    lsdcount.Status = 1;
                                    db.Lsd_DistCouponCount.AddObject(lsdcount);
                                    db.SaveChanges();

                                }


                            

                        }
                        if (table.Rows.Count > 0)
                        {
                            var grid = new System.Web.UI.WebControls.GridView();
                            grid.DataSource = table;

                            Response.ClearContent();
                            Response.AddHeader("content-disposition", "attachement; filename=Errorlog" + DateTime.Now + ".xls");
                            Response.Charset = "";
                            Response.ContentType = "application//vnd.xls";
                            Response.ContentEncoding = Encoding.Unicode;
                            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
                            Response.Buffer = false;
                            grid.AllowPaging = false;
                            grid.DataBind();
                            StringWriter sw = new StringWriter();
                            HtmlTextWriter htw = new HtmlTextWriter(sw);
                            grid.RenderControl(htw);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();
                            //TempData["ErrorLog"] = "Coupon Mapped Successfully";
                           // return RedirectToAction("Index");
                        }

                        TempData["Successfully"] = "<script>alert('Coupon Mapped Successfully');</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Blankfile"] = null;
                        TempData["extension"] = "<script>alert('Please upload only excel file');</script>";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["extension"] = null;
                    TempData["Blankfile"] = "<script>alert('Please select file.');</script>";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exc)
            {
                
                TempData["Exception"] = "<script>alert('Some exception has been occurred');</script>";
                return View("Index");
            }

        }

        public ActionResult Save(string DistCode, int?EligibleCoupon, string Status)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LSD_DistCouponCount/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["createrole"].ToString() == "1")
                {
                    Status = Status ?? "off";

                    #region Check Validation
                    if (DistCode == "" || DistCode == null)
                    {
                        // ModelState.AddModelError("Marqueetext", "*");
                        ModelState.AddModelError("DistCode", "Distributor Code Is Required");
                        // ViewBag.StartDate = "Marquee text cannot be empty";
                    }
                    if (EligibleCoupon == 0 || EligibleCoupon == null)
                    {
                        // ModelState.AddModelError("Marqueetext", "*");
                        ModelState.AddModelError("EligibleCoupon", "Coupon Is Required");
                        // ViewBag.StartDate = "Marquee text cannot be empty";
                    }
                   
                   

                    #endregion

                    if (ModelState.IsValid)
                    {

                        Lsd_DistCouponCount distcoupon = new Lsd_DistCouponCount();
                        distcoupon.DistCode = DistCode;
                        distcoupon.EligibleCoupon = EligibleCoupon;
                        distcoupon.DistActivatedCount = 0;
                        distcoupon.DistBalanceCount = EligibleCoupon;
                        distcoupon.DealerRedeemedCount = 0;
                        distcoupon.DealerRedeemedCount = 0;
                        distcoupon.DistClaimedCount = 0;
                        distcoupon.Gold_ActivatedCouponCount = 0;
                        distcoupon.Gold_BalanceCouponCount = 0;
                        distcoupon.Gold_EligibleCouponCount = 0;
                        if (Status.ToLower() == "on")
                        {
                            distcoupon.Status = 1;

                        }
                        else
                        {
                            distcoupon.Status = 0;
                        }
                        distcoupon.CreatedOn = DateTime.Now;
                        distcoupon.CreatedBy = Session["userid"].ToString();
                        db.Lsd_DistCouponCount.AddObject(distcoupon);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                          

                            ViewBag.SaveStatus = "Record Saved Successfully";
                        }

                    }
                    return View("Index", db.Lsd_DistCouponCount.ToList().ToPagedList(1, 5));
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");

                }
            }
        }

        //LS Details//


        public JsonResult GetLSD_DistCouponCount()
        {
            if (Session["userid"] == null)
            {
                return Json("login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LSD_DistCouponCount/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["uview"].ToString() == "1")
                {
                    var Lsd_DistCouponData = (from c in db.Lsd_DistCouponCount
                                      where  c.Status!=2

                                      select c).ToList();
                    int totalrecord;
                    //if (page != null)
                    //{
                    //    page = (page - 1) * 15;
                    //} 

                   
                  
                        var LsdDistCouponData2 = (from c in Lsd_DistCouponData


                                                  select new
                                                  {
                                                      DistCode = c.DistCode,
                                                      Coupon = c.EligibleCoupon,
                                                      Activated=c.DistActivatedCount,
                                                      Balance=c.DistBalanceCount,
                                                      status = c.Status == 1 ? "Enable" : "Disable",


                                                      id = c.Id,


                                                  }).OrderByDescending(a => a.id).ToList();
                        //if (Lsd_DistCouponData.Count() % 15 == 0)
                        //{
                        //    totalrecord = Lsd_DistCouponData.Count() / 15;
                        //}
                        //else
                        //{
                        //    totalrecord = (Lsd_DistCouponData.Count() / 15) + 1;
                        //}
                        var data = new { result = LsdDistCouponData2, TotalRecord = LsdDistCouponData2.Count };

                      //  var data = LsdDistCouponData2;

                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
               
                else
                {
                    return Json("snotallowed", JsonRequestBehavior.AllowGet);

                }
            }
        }

        //Edit LSD details//

        public ActionResult Edit(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LSD_DistCouponCount/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {
                    try
                    {
                        Lsd_DistCouponCount lsdcoupon = db.Lsd_DistCouponCount.Single(a => a.Id == id);
                        ViewBag.distcode = lsdcoupon.DistCode;
                        ViewBag.coupon = lsdcoupon.EligibleCoupon;
                      
                        ViewBag.preStatus = lsdcoupon.Status;
                        //ViewBag.bundlecode = lsdedit.BundleCode;
                       // ViewBag.giftid = lsdedit.GiftId;
                        ViewBag.Update = "";
                        return View(db.Lsd_DistCouponCount.Single(a => a.Id == id));
                    }
                    catch
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }
        }

        //Edit LSD Update//

        [HttpPost]
        public ActionResult Update(string Id,string DistCode, int EligibleCoupon, string Status)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                dt = Session["permission"] as DataTable;
                string pageUrl2 = "/LSD_DistCouponCount/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["editrole"].ToString() == "1")
                {

                    int intid = int.Parse(Id);
                   Status = Status ?? "off";

                    #region Check Validation
                   if (DistCode == "" || DistCode == null)
                    {
                        // ModelState.AddModelError("Marqueetext", "*");
                        ModelState.AddModelError("DistCode", "Distributor Code Is Required");
                        // ViewBag.StartDate = "Marquee text cannot be empty";
                    }
                   if (EligibleCoupon == 0 || EligibleCoupon == null)
                    {
                        // ModelState.AddModelError("Marqueetext", "*");
                        ModelState.AddModelError("EligibleCoupon", "Coupon Is Required");
                        // ViewBag.StartDate = "Marquee text cannot be empty";
                    }
                   
                   

                    #endregion
                   

                            if (ModelState.IsValid)
                            {
                                try
                                {

                                    Lsd_DistCouponCount couponcount = db.Lsd_DistCouponCount.Single(a => a.Id == intid);

                                    //Save Previous Record In History
                                    Lsd_DistCouponCount_History lsdcount = new Lsd_DistCouponCount_History();
                                    lsdcount.DistCode = couponcount.DistCode;
                                    lsdcount.EligibleCoupon = couponcount.EligibleCoupon;
                                    lsdcount.P_DistCouponCountId = couponcount.Id;
                                    lsdcount.DistBalanceCount = Convert.ToInt32(couponcount.EligibleCoupon);
                                    lsdcount.DistActivatedCount = couponcount.DistActivatedCount;
                                    lsdcount.DistClaimedCount = couponcount.DistClaimedCount; ;
                                    lsdcount.DealerRedeemedCount = couponcount.DealerRedeemedCount;
                                    lsdcount.CreatedBy = "Admin";
                                    lsdcount.CreatedOn = DateTime.Now;
                                    lsdcount.ModifiedBy = Session["userid"].ToString();
                                    lsdcount.ModifiedOn = DateTime.Now;
                                    lsdcount.Status = 1;
                                    db.Lsd_DistCouponCount_History.AddObject(lsdcount);
                                    db.SaveChanges();


                                    //Save New Record
                                    couponcount.DistCode = DistCode;
                                    couponcount.EligibleCoupon = EligibleCoupon;
                                 
                                    var distactivatedcount = db.Lsd_DistCouponCount.Where(c => c.Id == intid).Select(c => c.DistActivatedCount).SingleOrDefault();
                                    if (couponcount.EligibleCoupon >= Convert.ToInt32(distactivatedcount))
                                    {
                                        var distbalancecount = couponcount.EligibleCoupon - Convert.ToInt32(distactivatedcount);
                                        couponcount.DistBalanceCount = distbalancecount;
                                        if (Status.ToLower() == "on")
                                        {
                                            couponcount.Status = 1;

                                        }
                                        else
                                        {
                                            couponcount.Status = 0;
                                        }

                                        couponcount.ModifiedBy = Session["userid"].ToString();
                                        couponcount.ModifiedOn = DateTime.Now;
                                        if (db.SaveChanges() > 0)
                                        {
                                            ViewBag.Update = "Done";

                                        }
                                        else
                                        {
                                            return Content("<script>alert('Record Has Not Been Saved');</script>");
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.notupdate = "notupdate";
                                       // return Content("<script>alert('Eligible coupon cannot be less than activated coupon');</script>");
                                    }


                                }
                                catch
                                {
                                    return View("Index");
                                }
                            }
                      
                




                    Lsd_DistCouponCount couponcountid = db.Lsd_DistCouponCount.Single(a => a.Id == intid);
                    //ViewBag.preStartDate = Convert.ToDateTime(banner2.stardate).ToString("dd-MM-yyyy");
                    //ViewBag.PreEndDate = Convert.ToDateTime(banner2.ExpriyDate).ToString("dd-MM-yyyy");
                    ViewBag.preStatus = couponcountid.Status;

                    return View("Edit", couponcountid);
                }
                else
                {
                    return RedirectToAction("snotallowed", "snotallowed");
                }
            }

            //return RedirectToAction("Edit", new {id=id});

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
                string pageUrl2 = "/LSD_DistCouponCount/Index";
                DataRow[] result = dt.Select("pageurl ='" + pageUrl2 + "'");
                if (result[0]["deleterole"].ToString() == "1")
                {
                    try
                    {
                        Lsd_DistCouponCount couponcount = db.Lsd_DistCouponCount.Single(a => a.Id == id);

                        //Save Previous Record In History
                        Lsd_DistCouponCount_History couponHisotry = new Lsd_DistCouponCount_History();
                        couponHisotry.DistCode = couponcount.DistCode;
                        couponHisotry.EligibleCoupon = couponcount.EligibleCoupon;
                       
                        couponHisotry.P_DistCouponCountId = couponcount.Id;

                        couponHisotry.Status = couponcount.Status;

                        couponHisotry.ModifiedBy = Session["userid"].ToString();
                        couponHisotry.ModifiedOn = DateTime.Now;

                        db.Lsd_DistCouponCount_History.AddObject(couponHisotry);

                        //Delete Record From Table
                        couponcount.Status = 2;
                        couponcount.ModifiedOn = DateTime.Now;
                        couponcount.ModifiedBy = Session["userid"].ToString();
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

                }
                else
                {
                    return Json("You Have No Delete Permission", JsonRequestBehavior.AllowGet);

                }
            }
        }


        //Check exist Dist Code

        public JsonResult checkexist_DistCode(string DistCode)
        {
            var existdistcode = db.Lsd_DistCouponCount.Where(c => c.DistCode==DistCode).Count();

            return Json(existdistcode, JsonRequestBehavior.AllowGet);
          
          //  return Json("", JsonRequestBehavior.AllowGet);
        }
      
    }

    
}
