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
    public class GiftMappingController : Controller
    {
        //
        // GET: /GiftMapping/
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExportToExcel(string activation)
        {

            int i = Convert.ToInt32(activation);

            dynamic DealerData = null;
           

            // var DealerData = db.LSD_Master.Where(c => c.GiftId == 0).Select(c => new { c.QrCode, c.Barcode, c.SecretCode, c.BundleCode, c.GiftId }).ToList();

            if(i==0)
            {
                 DealerData = db.LSD_Master.Where(c => c.ActivationDistCode != null && c.RedemptionDealerCode == null).Select(c => new { c.QrCode, c.Barcode, c.SecretCode, c.BundleCode, c.GiftId }).ToList();
            }

            if(i==1)
            {
                DealerData = db.LSD_Master.Where(c => c.ActivationDistCode == null && c.RedemptionDealerCode == null).Select(c => new { c.QrCode, c.Barcode, c.SecretCode, c.BundleCode, c.GiftId }).ToList();

            }

          //  var DealerData = db.LSD_Master.Where(c => c.ActivationDistCode != null && c.RedemptionDealerCode == null).Select(c => new { c.QrCode, c.Barcode, c.SecretCode, c.BundleCode, c.GiftId }).ToList();

            if (DealerData!=null)
            {
                DataTable table = new DataTable();
                table.Columns.Add("QrCode", typeof(string));
                table.Columns.Add("BarCode", typeof(string));
                table.Columns.Add("SecretCode", typeof(string));
                table.Columns.Add("BundleCode", typeof(string));
                table.Columns.Add("GiftId", typeof(int));
                foreach (var str in DealerData)
                {
                    DataRow row = table.NewRow();
                    row["QrCode"] = str.QrCode;
                    row["BarCode"] = str.Barcode;
                    row["SecretCode"] = str.SecretCode;
                    row["BundleCode"] = str.BundleCode;
                    row["GiftId"] = str.GiftId;
                    table.Rows.Add(row);
                }

                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet sheet = pck.Workbook.Worksheets.Add("GiftMapping");

                    #region Header_Format
                    sheet.Cells["A1"].Value = "QrCode";
                    sheet.Cells["B1"].Value = "BarCode";
                    sheet.Cells["C1"].Value = "SecretCode";
                    sheet.Cells["D1"].Value = "BundleCode";
                    sheet.Cells["E1"].Value = "GiftId";

                    var cellH = sheet.Cells["A1:E1"];
                    cellH.Style.Font.Size = 12;
                    cellH.Style.Font.Name = "Calibri";
                    #endregion Header_Format
                    int intRowCount = 2;
                    foreach (DataColumn col in table.Columns)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            sheet.Cells["A" + Convert.ToString(intRowCount)].Value = row["QrCode"].ToString();
                            sheet.Cells["B" + Convert.ToString(intRowCount)].Value = row["BarCode"].ToString();
                            sheet.Cells["C" + Convert.ToString(intRowCount)].Value = row["SecretCode"].ToString();
                            sheet.Cells["D" + Convert.ToString(intRowCount)].Value = row["BundleCode"].ToString();
                            sheet.Cells["E" + Convert.ToString(intRowCount)].Value = row["GiftId"].ToString();

                            intRowCount++;
                        }
                        break;
                    }
                    sheet.Cells.AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", String.Format(CultureInfo.InvariantCulture, "attachment; filename=GiftMapping_" + DateTime.Now + ".xlsx"));
                    Response.BinaryWrite(pck.GetAsByteArray());
                    Response.End();

                    return View();
                }
            }
            else
            {
                TempData["NoData"] = "Gift Mapped with all Barcode.";
                return View("Index");
            }
        }


        public ActionResult GiftMappingBulkImport(HttpPostedFileBase FileUpload)
        {
            string msg = "";
            DataTable table = new DataTable();
            table.Columns.Add("QrCode", typeof(string));
            table.Columns.Add("BarCode", typeof(string));
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
                        string query = "SELECT [QrCode],[BarCode],[SecretCode],[BundleCode],[GiftId] FROM [GiftMapping$]";

                        OleDbCommand cmd = new OleDbCommand(query, conn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            var Qrcode = ds.Tables[0].Rows[i]["QrCode"].ToString();
                            var Barcode = ds.Tables[0].Rows[i]["BarCode"].ToString();
                            var Secretcode = ds.Tables[0].Rows[i]["SecretCode"].ToString();
                            var Bundlecode = ds.Tables[0].Rows[i]["BundleCode"].ToString();
                            var giftid = ds.Tables[0].Rows[i]["giftId"].ToString();

                            var check_couponactivate = db.LSD_Master.Where(c => c.Barcode == Barcode && c.ActivationDistCode != null).ToList();

                            if (check_couponactivate.Count > 0)
                            {
                                var check_reedemedDealer = db.LSD_Master.Where(c => c.Barcode == Barcode && c.ActivationDistCode != null && c.RedemptionDealerCode == null).ToList();

                                if (check_reedemedDealer.Count > 0)
                                {
                                    var lsdMaster_Main = db.LSD_Master.Single(c => c.Barcode == Barcode && c.SecretCode == Secretcode && c.QrCode == Qrcode);
                                    if (Convert.ToInt32(giftid) != lsdMaster_Main.GiftId)
                                    {
                                        LSD_Master_History lm_history = new LSD_Master_History();
                                        lm_history.Lsd_MainId = lsdMaster_Main.Id;
                                        lm_history.QrCode = lsdMaster_Main.QrCode;
                                        lm_history.Barcode = lsdMaster_Main.Barcode;
                                        lm_history.SecretCode = lsdMaster_Main.SecretCode;
                                        lm_history.BundleCode = lsdMaster_Main.BundleCode;
                                        lm_history.GiftId = lsdMaster_Main.GiftId;
                                        lm_history.CreatedOn = lsdMaster_Main.CreatedOn;
                                        lm_history.CreatedBy = lsdMaster_Main.CreatedBy;
                                        lm_history.ModifiedOn = DateTime.Now;
                                        lm_history.ModifiedBy = Session["userid"].ToString();
                                        lm_history.ActivationDistCode = lsdMaster_Main.ActivationDistCode;
                                        lm_history.ActivationDistName = lsdMaster_Main.ActivationDistName;
                                        lm_history.ActivatedQrCode = lsdMaster_Main.ActivatedQrCode;
                                        lm_history.ActivationDistOn = lsdMaster_Main.ActivationDistOn;
                                        lm_history.RedemptionDealerCode = lsdMaster_Main.RedemptionDealerCode;
                                        lm_history.RedemptionDealerName = lsdMaster_Main.RedemptionDealerName;

                                        lm_history.RedemtionDealerBarCode = lsdMaster_Main.RedemtionDealerBarCode;

                                        lm_history.RedemptionDealerSecretCode = lsdMaster_Main.RedemptionDealerSecretCode;

                                        lm_history.RedemptionDealerOn = lsdMaster_Main.RedemptionDealerOn;

                                        lm_history.ClaimDistCode = lsdMaster_Main.ClaimDistCode;
                                        lm_history.ClaimDistOn = lsdMaster_Main.ClaimDistOn;
                                        lm_history.Status = lsdMaster_Main.Status;
                                        db.LSD_Master_History.AddObject(lm_history);
                                        db.SaveChanges();



                                    }

                                    db.ExecuteStoreCommand("Update LSD_Master set GiftId='" + giftid + "' where QrCode='" + Qrcode + "' and Barcode='" + Barcode + "' and SecretCode='" + Secretcode + "' and BundleCode='" + Bundlecode + "'");
                                }
                                else
                                {
                                    DataRow row = table.NewRow();
                                    row["QrCode"] = Qrcode;
                                    row["BarCode"] = Barcode;
                                    row["Message"] = "Barcode already redeemed";

                                    table.Rows.Add(row);
                                }
                            }
                            else
                            {
                                DataRow row = table.NewRow();
                                row["QrCode"] = Qrcode;
                                row["BarCode"] = Barcode;
                                row["Message"] = "Coupon not activated";

                                table.Rows.Add(row);
                            }
                        }
                        if (table.Rows.Count > 0)
                        {
                            var grid = new System.Web.UI.WebControls.GridView();
                            grid.DataSource = table;

                            Response.ClearContent();
                            Response.AddHeader("content-disposition", "attachement; filename=GiftMappingErrorlog" + DateTime.Now + ".xls");
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
                        }
                        TempData["Successfully"] = "<script>alert('Gift Mapped Successfully');</script>";
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
                TempData["Exception"] = exc.ToString() + "_" + msg;
                return View("Index");
            }

        }

        public JsonResult getGiftfilter(string FilterType)
        {
            try
            {

                if (Session["Typevalue"].ToString() == "QrCode")
                {

                   
                            var qrcode = (from c in db.LSD_Master
                                          where c.QrCode.Contains(FilterType)
                                          select new
                                          {

                                              name = c.QrCode
                                          }).Take(20).ToList();

                            //  var qrdata = db.LSD_Master.Select(c => new { c.Id, c.QrCode }).ToList();

                            // var abcde = "'" + abcd + "'";

                            return Json(qrcode, JsonRequestBehavior.AllowGet);
                        
                }
                if (Session["Typevalue"].ToString() == "BarCode")
                {

                            var Barcode = (from c in db.LSD_Master
                                           where c.Barcode.Contains(FilterType)
                                           select new
                                           {

                                               name = c.Barcode
                                           }).Take(20).ToList();

                            return Json(Barcode, JsonRequestBehavior.AllowGet);

                }
                if (Session["Typevalue"].ToString() == "Alpha Numeric Code")
                {
                    
                            var SecretCode = (from c in db.LSD_Master
                                              where c.SecretCode.Contains(FilterType)
                                              select new
                                              {

                                                  name = c.SecretCode
                                              }).Take(20).ToList();

                            return Json(SecretCode, JsonRequestBehavior.AllowGet);
                       
                }
                if (Session["Typevalue"].ToString() == "Bundle")
                {
                    
                            var BundleCode = (from c in db.LSD_Master
                                              where c.BundleCode.Contains(FilterType)
                                              select new
                                              {

                                                  name = c.BundleCode
                                              }).Distinct().Take(20).ToList();

                            return Json(BundleCode, JsonRequestBehavior.AllowGet);
                        

                   


                }
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json("Some exception has occurred", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getGiftMapData(string Filtervalue)
        {
            try
            {

                if (Session["Typevalue"].ToString() == "QrCode")
                {
                                        var check_couponactivate = db.LSD_Master.Where(c => c.QrCode == Filtervalue && c.ActivationDistCode != null).ToList();
                    if (check_couponactivate.Count > 0)
                    {
                        var check_couponredeemed = db.LSD_Master.Where(c => c.QrCode == Filtervalue && c.ActivationDistCode != null && c.RedemptionDealerCode == null).ToList();
                        if (check_couponredeemed.Count > 0)
                        {

                    var qrcode = (from c in db.LSD_Master
                                  where c.QrCode == Filtervalue
                                  select new
                                  {
                                      Id = c.Id,
                                      qrcode = c.QrCode,
                                      barcode = c.Barcode,
                                      secretcode = c.SecretCode,
                                      bundlecode = c.BundleCode,
                                      giftid = c.GiftId

                                  }).ToList();

                    //  var qrdata = db.LSD_Master.Select(c => new { c.Id, c.QrCode }).ToList();

                    // var abcde = "'" + abcd + "'";

                    return Json(qrcode, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("Barcode already redeemed", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json("Coupon not activated", JsonRequestBehavior.AllowGet);
                    }
                }
                if (Session["Typevalue"].ToString() == "BarCode")
                {
                                                            var check_couponactivate = db.LSD_Master.Where(c => c.Barcode == Filtervalue && c.ActivationDistCode != null).ToList();
                    if (check_couponactivate.Count > 0)
                    {
                        var check_couponredeemed = db.LSD_Master.Where(c => c.Barcode == Filtervalue && c.ActivationDistCode != null && c.RedemptionDealerCode == null).ToList();
                        if (check_couponredeemed.Count > 0)
                        {

                    var Barcode = (from c in db.LSD_Master
                                   where c.Barcode == Filtervalue
                                   select new
                                   {
                                       Id = c.Id,
                                       qrcode = c.QrCode,
                                       barcode = c.Barcode,
                                       secretcode = c.SecretCode,
                                       bundlecode = c.BundleCode,
                                       giftid = c.GiftId

                                   }).ToList();

                    return Json(Barcode, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("Barcode already redeemed", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json("Coupon not activated", JsonRequestBehavior.AllowGet);
                    }
                }
                if (Session["Typevalue"].ToString() == "Alpha Numeric Code")
                {
                      var check_couponactivate = db.LSD_Master.Where(c => c.SecretCode == Filtervalue && c.ActivationDistCode != null).ToList();
                    if (check_couponactivate.Count > 0)
                    {
                        var check_couponredeemed = db.LSD_Master.Where(c => c.SecretCode == Filtervalue && c.ActivationDistCode != null && c.RedemptionDealerCode == null).ToList();
                        if (check_couponredeemed.Count > 0)
                        {
                    var SecretCode = (from c in db.LSD_Master
                                      where c.SecretCode == Filtervalue
                                      select new
                                      {
                                          Id = c.Id,
                                          qrcode = c.QrCode,
                                          barcode = c.Barcode,
                                          secretcode = c.SecretCode,
                                          bundlecode = c.BundleCode,
                                          giftid = c.GiftId

                                      }).ToList();

                    return Json(SecretCode, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("Barcode already redeemed", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json("Coupon not activated", JsonRequestBehavior.AllowGet);
                    }
                }
                if (Session["Typevalue"].ToString() == "Bundle")
                {
                    var check_couponactivate = db.LSD_Master.Where(c => c.BundleCode == Filtervalue && c.ActivationDistCode != null).ToList();
                    if (check_couponactivate.Count > 0)
                    {
                        var check_couponredeemed = db.LSD_Master.Where(c => c.BundleCode == Filtervalue && c.ActivationDistCode != null && c.RedemptionDealerCode == null).ToList();
                        if (check_couponredeemed.Count > 0)
                        {
                            var BundleCode = (from c in db.LSD_Master
                                              where c.BundleCode == Filtervalue
                                              select new
                                              {
                                                  Id = c.Id,
                                                  qrcode = c.QrCode,
                                                  barcode = c.Barcode,
                                                  secretcode = c.SecretCode,
                                                  bundlecode = c.BundleCode,
                                                  giftid = c.GiftId

                                              }).ToList();

                            return Json(BundleCode, JsonRequestBehavior.AllowGet);

                        }
                        else
                        {
                            return Json("Barcode already redeemed", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json("Coupon not activated", JsonRequestBehavior.AllowGet);
                    }


                    
                }
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json("Some exception has occurred", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getFilterRadio(string Typevalue)
        {
            Session["Typevalue"] = Typevalue.ToString();
            return View();
        }

        public JsonResult editGiftMapping(string id, string giftid)
        {
            // Session["Typevalue"] = Typevalue.ToString();
            if (id != "" && giftid != "")
            {
                var Gid = Convert.ToInt32(giftid);
                var checkgiftidexist = db.Lsd_GiftMaster.Where(c => c.GiftId == Gid).Count();
                if (checkgiftidexist > 0)
                {
                    db.ExecuteStoreCommand("Update LSD_Master set GiftId='" + giftid + "'where Id='" + id + "'");
                    return Json("Gift Id Mapped Successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Gift Id Not Exist", JsonRequestBehavior.AllowGet);
                }

            }

            else
            {
                return Json("Gift Id cannot be  blank", JsonRequestBehavior.AllowGet);
            }
            return Json("Index", JsonRequestBehavior.AllowGet);
        }
    }
}

