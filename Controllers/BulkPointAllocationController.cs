using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.OleDb;
using System.Data;
using Luminous.EF;
using System.Drawing;
namespace Luminous.Controllers
{
    public class BulkPointAllocationController : Controller
    {
        //
        // GET: /BulkPointAllocation/
        private LuminousEntities db = new LuminousEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PointBulkUpload(HttpPostedFileBase FileUpload)
        {
            string ms = "";
            try
            {
                //var dt = FileUpload.ContentLength;
                if (FileUpload!=null)
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
                      
                        FileUpload.SaveAs(pathfolder);
                     
                        //Connection String to Excel Workbook
                        if (ext.Trim() == ".xls")
                        {
                            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathfolder + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        }
                        else if (ext.Trim() == ".xlsx")
                        {
                            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathfolder + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }
                        string query = "SELECT [Image Path],[Dealer Name],[Dealer City],[Dealer Phone],[Dealer Email],[Distributor Code],[Emp Code/FSE Code],[Dealer Id],[Rating] FROM [Sheet1$]";
                        OleDbConnection conn = new OleDbConnection(connString);
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        OleDbCommand cmd = new OleDbCommand(query, conn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                        {
                            var data = ds.Tables[0].Rows[i]["Image Path"].ToString();

                           


                            //Running code//

                            //ms = "Hell00" ;
                            //byte[] b = System.IO.File.ReadAllBytes(data);

                            //ms = "Hel";

                            string filename = Path.GetFileNameWithoutExtension(data)+Path.GetExtension(data);
                            //string str = Path.Combine(Server.MapPath("~/ProfileImages/"), filename);
                            //BinaryWriter bw = new BinaryWriter(new FileStream(str, FileMode.Create, FileAccess.Write));

                            //bw.Write(b);
                            //bw.Close();
                           
                            var DealerName = ds.Tables[0].Rows[i]["Dealer Name"].ToString();
                            var DealerCity = ds.Tables[0].Rows[i]["Dealer City"].ToString();
                            var DealerPhone = ds.Tables[0].Rows[i]["Dealer Phone"].ToString();
                            var DealerEmail = ds.Tables[0].Rows[i]["Dealer Email"].ToString();
                            var DistributorCode = ds.Tables[0].Rows[i]["Distributor Code"].ToString();
                            var EmpCode = ds.Tables[0].Rows[i]["Emp Code/FSE Code"].ToString();
                            var DealerId = ds.Tables[0].Rows[i]["Dealer Id"].ToString();
                            var Rating = ds.Tables[0].Rows[i]["Rating"].ToString();
                            
                            var Contest = new InsertContext()
                            {
                                ImageEncode = filename,
                                DealerName = DealerName.ToString(),
                                DealerCity = DealerCity.ToString(),
                                DealerEmail = DealerEmail.ToString(),
                                DealerPhone = DealerPhone,
                                DistributorCode = DistributorCode.ToString(),
                                EmpCode = EmpCode.ToString(),
                                DealerId = DealerId.ToString(),
                                CreatedBy = Session["Id"].ToString(),
                                CreatedOn = DateTime.Now,
                                Rating = Rating.ToString()

                            };
                            db.InsertContexts.AddObject(Contest);
                            db.SaveChanges();

                            //db.ExecuteStoreCommand("Insert into InsertContext(ImageEncode,DealerName,DealerCity,DealerEmail,DealerPhone,DistributorCode,EmpCode,DealerId,Rating) values ('" + filename + "','" + DealerName + "','" + DealerCity + "','" + DealerEmail + "','" + DealerPhone + "','" + DistributorCode + "','" + EmpCode + "','" + DealerId + "','" + Rating + "')");
                           
                        }
                        TempData["Successfully"] = "Excel Uploaded Successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Blankfile"] = null;
                        TempData["extension"] = "Please upload only excel file.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["extension"] = null;
                    TempData["Blankfile"] = "Please select file.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exc)
            {
                TempData["Exception"] = "Some exception has occurred";
                return View("Index");
            }

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
