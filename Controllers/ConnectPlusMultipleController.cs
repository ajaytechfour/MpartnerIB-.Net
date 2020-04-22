using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using LuminousMpartnerIB.EF;
using LuminousMpartnerIB.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Data.OleDb;
using System.Text;
using System.Web.UI;
using System.Reflection;
using System.Globalization;
using System.Configuration;

namespace LuminousMpartnerIB.Controllers
{
    public class ConnectPlusMultipleController : Controller
    {
        private LuminousMpartnerIBEntities db = new LuminousMpartnerIBEntities();
        private DataTable dt = new DataTable();
        public ActionResult Index()
        {
            Itemlst();
            statelst();
            return View();
        }


        public string ScheduleUpdateDataBaseRecord()
        {
            try
            {            

            UserDataParameter ud = new UserDataParameter();

            ud.token = "pass@1234";


            var result1 = GetDataFromApi.ItemApi("webservice_WRS_Item_Type_Master", ud);


            if (result1.IsSuccessStatusCode)
            {
                var responseResult = result1.Content.ReadAsStringAsync().Result;

                JToken JtokenResult1 = JToken.Parse(responseResult);

                var List = JsonConvert.DeserializeObject<List<ItemName>>(JtokenResult1.ToString());


                StringBuilder sb = new StringBuilder();
                int j = 1;

                foreach (var temp in List)
                {

                    sb.Append(temp.Item_Type);
                    if (j < List.Count())
                    {
                        sb.Append(",");
                        j++;
                    }


                }
                ud.Item_Type = sb.ToString();

                var result2 = GetDataFromApi.ItemListApi("webservice_WRS_Item_Type_Master", ud);


                if (result2.IsSuccessStatusCode)
                {
                    var responseResult2 = result2.Content.ReadAsStringAsync().Result;

                    JToken JtokenResult2 = JToken.Parse(responseResult2);

                    var ItemList = JsonConvert.DeserializeObject<List<ItemMaster>>(JtokenResult2.ToString());
                    int i = 1;
                    db.ExecuteStoreCommand("Delete from WRS_ItemMaster");

                    foreach (var temp in ItemList)
                    {
                        EF.WRS_ItemMaster im = new WRS_ItemMaster();
                        im.Id = i;
                        im.ItemType = temp.Item_Type;
                        im.ItemCode = temp.ItemCode;
                        im.ItemDescription = temp.ItemDesc;                        
                        db.WRS_ItemMaster.AddObject(im);
                        db.SaveChanges();
                        i++;

                    }
                }
            }
               
            
            

                //
            }
            catch (Exception ex)
            {
                return ex.ToString();

            }

            return "Data Updated Successfully";
            


           


            //ud.token = "pass@1234";
            //ud.Item_Type = "ACCESSORIES,AMAZE,AMAZE BATTERY,BATTERY,CARE PACK,CHARGE CONTROLLER,CHARGER,CRACKER,EXPORT,FAN,GTI,HKVA,BATTEN,HOME SYSTEM,HUPS,LAMP,LANTERN,LIGHT SYSTEM";

            //var result2 = GetDataFromApi.Api("webservice_WRS_Item_Type_Master", ud);


            //return View();
        }

        [HttpPost]
        public ActionResult Index(string itemId, string stateId, HttpPostedFileBase FileUpload1, string hdnMaterialcode, string hdnsdate, string hdnedate)
        {
            OleDbConnection conn = null;
            System.Data.DataTable dt = null;

            try
            {
                if (FileUpload1 == null)
                {
                    string fileName = "StockReport" + "_" + DateTime.Now.ToString("dd-MM-yyyy-T-HH_mm_ss") + ".csv";

                    string arrItemResult = arrItemResultdata(itemId);
                    string[] arrStateResult = arrStateResultdata(stateId);
                    string[] arrItemResultNew = { };
                    string[] arrItemMetCode = { };

                    if (hdnMaterialcode != "")
                    {
                        arrItemMetCode = hdnMaterialcode.Split(',');
                    }
                    else
                    {
                        arrItemResultNew = arrItemResultdataNew(itemId);
                    }

                    UploadViewModel vm = new UploadViewModel();

                    //Log Download
                    DateTime sDate = new DateTime();
                    DateTime eDate = new DateTime();

                    if (hdnsdate != "" && hdnedate != "")
                    {
                        string[] hdnStartdate = hdnsdate.Split('/');
                        string sdate = hdnStartdate[2].ToString() + hdnStartdate[0].ToString() + hdnStartdate[1].ToString();
                        string[] hdnEnddate = hdnedate.Split('/');
                        string edate = hdnEnddate[2].ToString() + hdnEnddate[0].ToString() + hdnEnddate[1].ToString();

                        string result1 = DateTime.ParseExact(sdate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        string result2 = DateTime.ParseExact(edate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        sDate = Convert.ToDateTime(result1);
                        eDate = Convert.ToDateTime(result2);
                    }


                    if (hdnsdate != "" && hdnedate != "" && stateId == "" && hdnMaterialcode == "" && itemId == "")
                    {
                        vm.Item_NewList = vm.getDataWithOutStateWithOutItemsLst(sDate, eDate);

                        if (vm.Item_NewList.Count() == 0)
                        {
                            TempData["NoData"] = "Record Not Found.";
                            return View("Index");
                        }

                    }

                    else if (hdnsdate != "" && hdnedate != "" && stateId != "" && hdnMaterialcode == "" && itemId == "")
                    {
                        vm.Item_NewList = vm.getDataWithStateWithOutItemsLst(arrStateResult, sDate, eDate);

                        if (vm.Item_NewList.Count() == 0)
                        {
                            TempData["NoData"] = "Record Not Found.";
                            return View("Index");
                        }
                    }

                    else if (hdnsdate != "" && hdnedate != "" && stateId == "" && (hdnMaterialcode != "" || itemId != ""))
                    {
                        vm.Item_NewList = vm.getDataWithOutStateLst(hdnMaterialcode, arrItemMetCode, arrItemResultNew, sDate, eDate);

                        if (vm.Item_NewList.Count() == 0)
                        {
                            TempData["NoData"] = "Record Not Found.";
                            return View("Index");
                        }
                    }

                    else if (hdnsdate != "" && hdnedate != "" && stateId != "" && (hdnMaterialcode != "" || itemId != ""))
                    {
                        // var query = new UploadViewModel();
                        vm.Item_NewList = vm.getDataWithStateLst(hdnMaterialcode, arrItemMetCode, arrStateResult, arrItemResultNew, sDate, eDate);

                        if (vm.Item_NewList.Count() == 0)
                        {
                            TempData["NoData"] = "Record Not Found.";
                            return View("Index");
                        }
                    }

                    //StringBuilder sb = new StringBuilder();
                    if (vm.Item_NewList.Count() > 0)
                    {
                        dt = new DataTable();
                        dt = downloadExcel.ToDataTable(vm.Item_NewList);
                        Donloadfile(dt, sDate, eDate);
                    }

                    //End Log

                    //int? k;
                    //int j = 0;
                    //var all = (from c in db.WRS_Multiplier_Master_Temp
                    //           select c);


                    for (int i = 0; i < arrStateResult.Length; i++)
                    {
                        vm.StateList.Add(new LuminousMpartnerIB.Models.State
                        {
                            State_Name = arrStateResult[i]
                        });
                    }
                    UserDataParameter ud = new UserDataParameter();
                    ud.token = "pass@1234";
                    ud.Item_Type = arrItemResult;

                    connectplusmulti con = new connectplusmulti();
                    var ItemList = con.ItemLstConnectPlus_ItemList(ud);

                    //if (all.Count() == 0)
                    //{
                    //    //k =1;
                    //    k = 1;
                    //    //  j = 0;
                    //}
                    //else
                    //{
                    //    var lastId = db.WRS_Multiplier_Master_Temp.OrderBy(x => x.Sr_No).Skip(db.WRS_Multiplier_Master_Temp.Count() - 1).Select(x => new { x.Sr_No }).FirstOrDefault();

                    //    k = Convert.ToInt32(lastId.Sr_No) + 1;
                    //    //k = all.Count() + 1;
                    //    // j = all.Count();
                    //}

                    foreach (var temp1 in vm.StateList)
                    {
                        foreach (var temp2 in ItemList)
                        {
                            //WRS_Multiplier_Master_Temp temp = new WRS_Multiplier_Master_Temp();
                            //temp.Id = (long)k;
                            //temp.Material_Code = temp2.itemcode;
                            //temp.State_Name = temp1.State_Name;
                            //temp.FileName = fileName;
                            //db.WRS_Multiplier_Master_Temp.AddObject(temp);
                            //db.SaveChanges();
                            //k++;

                            vm.Item_NewList.Add(new Item_New
                            {
                                //Sr_No = k.ToString(),
                                Material_Code = temp2.itemcode,
                                Item_Type = temp2.Item_Type,
                                itemdesc = temp2.itemdesc,
                                State_Name = temp1.State_Name
                            });
                        }
                    }

                    //Download 
                    if (vm.Item_NewList.Count() > 0)
                    {
                        string sfilename = fileName + DateTime.Now + ".csv";
                        dt = new DataTable();
                        dt = downloadExcel.ToDataTable(vm.Item_NewList);

                        StringBuilder sb = new StringBuilder();
                        // dt.Columns[0].DataType = typeof(string);

                        IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                  Select(column => column.ColumnName);

                        sb.AppendLine(string.Join(",", columnNames));
                        //sb.AppendLine(string.Join("\"{0}\"",columnNames));

                        foreach (DataRow row in dt.Rows)
                        {

                            IEnumerable<string> fields = row.ItemArray.Select(field =>
                              string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));

                            //sb.AppendLine(string.Format("{0:000}",string.Join(",",fields)));
                            //  sb.AppendLine(string.Join(",",);
                            sb.AppendLine(string.Join(",", fields));
                            // sb.AppendLine(string.Join("\"{0}\"", fields));
                        }

                        byte[] bytes = Encoding.ASCII.GetBytes(sb.ToString());

                        if (bytes != null)
                        {
                            Response.Clear();
                            Response.ContentType = "text/csv";
                            Response.AddHeader("Content-Length", bytes.Length.ToString());
                            Response.AddHeader("Content-disposition", "attachment; filename=\"" + fileName + "\"");
                            Response.BinaryWrite(bytes);
                            Response.Flush();
                            Response.End();
                        }

                        //downloadExcel.GenerateCSVString(vm.Item_NewList);

                        //var grid = new System.Web.UI.WebControls.GridView();
                        //grid.DataSource = vm.Item_NewList;
                        //grid.DataBind();
                        //Response.ClearContent();
                        //Response.AddHeader("content-disposition", "attachement; filename=fileName" + DateTime.Now + ".csv");
                        //Response.ContentType = "text/csv";
                        //StringWriter sw = new StringWriter();
                        //HtmlTextWriter htw = new HtmlTextWriter(sw);
                        //grid.RenderControl(htw);
                        //Response.Output.Write(sw.ToString());
                        //Response.Flush();
                        //Response.End();

                        //downloadExcel.DataTableToExcel(dt, fileName);
                        // downloadExcel.ToCSV(dt, fileName);

                        //var ss = Content("<script language='javascript' type='text/javascript'>alert('Data Already Exists');</script>");

                    }
                }
                else
                {
                    if (FileUpload1 != null)
                    {
                        string newFileName = "Error_" + FileUpload1.FileName;

                        string newErrorFileName = "Error_" + FileUpload1.FileName;
                        string newSuccessFileName = "Success_" + FileUpload1.FileName;

                        DataTable table = new DataTable();
                        IList<WRSMultiplierMasterError> WRSMultiplierMasterErrorlst = new List<WRSMultiplierMasterError>();

                        IList<WRSMultiplierMasterError> WRSMultiplierMasterSuccesslst = new List<WRSMultiplierMasterError>();

                        WRS_Multiplier_Master_Temp WRS_Multiplier_Master_Temp = new WRS_Multiplier_Master_Temp();
                        // var Temp_List_WRS_Multiplier_Master_Temp = db.WRS_Multiplier_Master_Temp.Where(x => x.FileName == FileUpload1.FileName).ToList();

                        if (FileUpload1.ContentLength > 0)   // if (Temp_List_WRS_Multiplier_Master_Temp.Count() > 0)
                        {


                            string connString = "";
                            string ext = System.IO.Path.GetExtension(Path.GetFileName(FileUpload1.FileName));
                            var originalfilename = Path.GetFileName(FileUpload1.FileName);
                            //if (ext == ".xlsx" || ext == ".xls")
                            if (ext == ".csv")
                            {
                                TempData["extension"] = null;
                                TempData["Successfully"] = null;
                                TempData["Blankfile"] = null;
                                string xlsextension = string.Empty;
                                string[] changeextension = originalfilename.Split('.');
                                if (ext == ".csv")
                                {
                                    xlsextension = changeextension[0] + ".csv";
                                }
                                else
                                {
                                    xlsextension = changeextension[0] + ".csv";
                                }

                                string path = Path.Combine(Path.GetDirectoryName(xlsextension)
                                    , string.Concat(Path.GetFileNameWithoutExtension(xlsextension)
                                    //, DateTime.Now.ToString("_yyyy_MM_dd_HH_MM_ss")
                                    , Path.GetExtension(xlsextension)
                                    ));

                                var pathfolder = Path.Combine(Server.MapPath("~/Upload/"), path);

                                if (System.IO.File.Exists(pathfolder))
                                {
                                    System.IO.File.Delete(pathfolder);
                                }


                                FileUpload1.SaveAs(pathfolder);


                                //Connection String to Excel Workbook

                                if (ext.Trim() == ".csv")
                                {
                                    //connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathfolder + ";Extended Properties='Text;HDR=Yes;FMT=CSVDelimited'";

                                    //connString = "Provider=Microsoft.ACE.OLEDB.12.0;"
                                    //   + "Data Source=" + pathfolder + " +
                                    //   ";Extended Properties='Text;HDR=Yes;FMT=CSVDelimited'";

                                    var fileName = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Upload\\");

                                    connString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; Extended Properties=""text;HDR=YES;FMT=Delimited""", fileName);


                                }





                                //else if (ext.Trim() == ".csv")
                                // {
                                //     connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathfolder + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                                // }




                                //OleDbConnection oledbConn = new OleDbConnection(connectionString);
                                //oledbConn.Open();
                                //var cmd = new OleDbCommand("SELECT * FROM [countrylist.csv]", oledbConn);


                                conn = new OleDbConnection(connString);
                                if (conn.State == ConnectionState.Closed)

                                    conn.Open();

                                String[] sheetName = GetExcelSheetNames(pathfolder, ext);

                                string query = "SELECT * FROM " + "[" + originalfilename + "]";

                                OleDbCommand cmd = new OleDbCommand(query, conn);
                                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                da.Fill(ds);

                                //conn.Close();



                                //DataSet ds = new DataSet();
                                //ds = downloadExcel.ReadCsvFile(pathfolder);


                                //var minIndexCount = Convert.ToInt32(Temp_List_WRS_Multiplier_Master_Temp[0].Id);
                                //var maxIndexCount = Temp_List_WRS_Multiplier_Master_Temp.Skip(Temp_List_WRS_Multiplier_Master_Temp.Count() - 1).Select(x => x.Id).FirstOrDefault();
                                var minIndexCount = 0;
                                var maxIndexCount = ds.Tables[0].Rows.Count;



                                var itemdesc = "";
                                var Item_Type = "";

                                int SuccessCount = 0;
                                int ErrorCount = 0;
                                DateTime InputDateTime = DateTime.Now;


                                for (int i = 0; i < ds.Tables[0].Rows.Count && minIndexCount <= maxIndexCount; i++, minIndexCount++)
                                {
                                    WRSMultiplierMasterError WRSMultiplierMasterSuccess = new WRSMultiplierMasterError();
                                    WRSMultiplierMasterError WRSMultiplierMasterError = new WRSMultiplierMasterError();

                                    string str_Entries_Count = null;
                                    string str_Multiplier_Count = null;
                                    string str_Multiplier_Type = "";
                                    string stcode = ds.Tables[0].Rows[i]["State_Name"].ToString();
                                    //long srno = Convert.ToInt64(ds.Tables[0].Rows[i]["Sr_No"]);

                                    int int_Entries_Count;
                                    double dec_Multiplier_Count;

                                    itemdesc = ds.Tables[0].Rows[i]["itemdesc"].ToString();
                                    Item_Type = ds.Tables[0].Rows[i]["Item_Type"].ToString();

                                    //if (ds.Tables[0].Rows[i]["Sr_No"].ToString() == null || ds.Tables[0].Rows[i]["Sr_No"].ToString() == "")
                                    //{
                                    //    WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Sr_No is Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "Sr_No is Required.";

                                    //}
                                    var Material_Code = ds.Tables[0].Rows[i]["Material_Code"].ToString();

                                    if (Material_Code == null || Material_Code == "")
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Material Code is Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "Material Code is Required.";
                                    }
                                    else
                                    {
                                        if (db.WRS_ItemMaster.Where(x => x.ItemCode == Material_Code).Any())
                                        {
                                            //if (db.WRS_Multiplier_Master_Temp.Where(x => x.Material_Code == Material_Code && x.State_Name == stcode && x.RecordStatus != "Success" && x.Sr_No == srno).Any())
                                            //{


                                            //}
                                            //else
                                            //{
                                            //WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Material Code Already Entered" : WRSMultiplierMasterError.ErrorDescription + ", " + "Material Code Already Entered";

                                            // }

                                        }
                                        else
                                        {
                                            WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Invalid Material Code." : WRSMultiplierMasterError.ErrorDescription + ", " + "Invalid Material Code";

                                        }

                                    }



                                    var State_Name = ds.Tables[0].Rows[i]["State_Name"].ToString();

                                    if (State_Name == null || State_Name == "")
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "State Name is Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "State Name is Required.";
                                    }

                                    //else
                                    //{
                                    //    if (db.WRS_ItemMasters.Where(x => x.ItemCode == Material_Code).Any())
                                    //    {

                                    //    }
                                    //    else
                                    //    {
                                    //        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Material Code is Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "Invalid Material Code";

                                    //    }

                                    //}

                                    //if (Temp_List_WRS_Multiplier_Master_Temp[i].Material_Code != Material_Code)
                                    //{
                                    //    WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Material Code is Not Valid." : WRSMultiplierMasterError.ErrorDescription + ", " + "Material Code is Not Valid.";
                                    //}

                                    //if (Temp_List_WRS_Multiplier_Master_Temp[i].State_Name != State_Name)
                                    //{
                                    //    WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "State Name is Not Valid." : WRSMultiplierMasterError.ErrorDescription + ", " + "State Name is Not Valid.";
                                    //}

                                    string Valid_Start_Date = Convert.ToString(ds.Tables[0].Rows[i]["Valid_Start_Date(dd/mm/yy)"]);




                                    if (Valid_Start_Date == null || Valid_Start_Date == "" || Convert.ToString(ds.Tables[0].Rows[i]["Valid_End_Date(dd/mm/yy)"]) == null || Convert.ToString(ds.Tables[0].Rows[i]["Valid_End_Date(dd/mm/yy)"].ToString()) == "")
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Valid Start Date is Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "Valid Start Date is Required.";
                                    }
                                    else
                                    {
                                        if (Valid_Start_Date.Contains('-'))
                                        {
                                            DateTime dat= Convert.ToDateTime(Valid_Start_Date);
                                            var Split_Valid_Start_Date = Valid_Start_Date.Split('-');
                                            Valid_Start_Date = Split_Valid_Start_Date[0] + "-" + Split_Valid_Start_Date[1] + "-" + Split_Valid_Start_Date[2];

                                            var tempSDate = DateTime.ParseExact(Valid_Start_Date, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                                            var currentDate = DateTime.Now.Date;
                                            if (tempSDate.Day > currentDate.Day && tempSDate.Year >= currentDate.Year)
                                            {



                                            }
                                            else
                                            {
                                                WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Valid Start Date is greater then  from Now." : WRSMultiplierMasterError.ErrorDescription + ", " + "Valid Start Date is greater then  from Now.";

                                            }

                                        }
                                        else
                                        {
                                            var Split_Valid_Start_Date = Valid_Start_Date.Split('/');
                                            Valid_Start_Date = Split_Valid_Start_Date[1] + "/" + Split_Valid_Start_Date[0] + "/" + Split_Valid_Start_Date[2];

                                            var tempSDate = DateTime.ParseExact(Valid_Start_Date, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                                            var currentDate = DateTime.Now.Date;
                                            if (tempSDate.Day > currentDate.Day && tempSDate.Year >= currentDate.Year)
                                            {



                                            }
                                            else
                                            {
                                                WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Valid Start Date is greater then  from Now." : WRSMultiplierMasterError.ErrorDescription + ", " + "Valid Start Date is greater then  from Now.";

                                            }

                                        }

                                    }

                                    string Valid_End_Date = Convert.ToString(ds.Tables[0].Rows[i]["Valid_End_Date(dd/mm/yy)"].ToString());




                                    if (Valid_End_Date == null || Valid_End_Date == "" || Convert.ToString(ds.Tables[0].Rows[i]["Valid_Start_Date(dd/mm/yy)"]) == null || Convert.ToString(ds.Tables[0].Rows[i]["Valid_Start_Date(dd/mm/yy)"]) == "")
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Valid End Date is Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "Valid End Date is Required.";
                                    }
                                    else
                                    {
                                        if (Valid_End_Date.Contains('-'))
                                        {
                                            var Split_Valid_End_Date = Valid_End_Date.Split('-');
                                            Valid_End_Date = Split_Valid_End_Date[0] + "-" + Split_Valid_End_Date[1] + "-" + Split_Valid_End_Date[2];

                                            var tempESDate = DateTime.ParseExact(Valid_End_Date, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                                            var SDate = DateTime.ParseExact(Valid_Start_Date, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);



                                            if (tempESDate.Day < SDate.Day && tempESDate.Year < SDate.Year)
                                            {
                                                WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "End Date should be greater than StartDate Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "End Date should be greater than StartDate Required.";
                                            }
                                            else
                                            {

                                            }
                                        }



                                        else
                                        {
                                            var Split_Valid_End_Date = Valid_End_Date.Split('/');
                                            Valid_End_Date = Split_Valid_End_Date[0] + "/" + Split_Valid_End_Date[1] + "/" + Split_Valid_End_Date[2];

                                            var tempESDate = DateTime.ParseExact(Valid_End_Date, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                                            var SDate = DateTime.ParseExact(Valid_Start_Date, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);



                                            if (tempESDate.Day < SDate.Day && tempESDate.Year < SDate.Year)
                                            {
                                                WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "End Date should be greater than StartDate Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "End Date should be greater than StartDate Required.";
                                            }
                                            else
                                            {

                                            }

                                        }

                                    }

                                    if (Valid_Start_Date != "" && Valid_End_Date != "")
                                    {
                                        DateTime Valid_Start_Date1 = Convert.ToDateTime(ds.Tables[0].Rows[i]["Valid_Start_Date(dd/mm/yy)"].ToString());
                                        DateTime Valid_End_Date2 = Convert.ToDateTime(ds.Tables[0].Rows[i]["Valid_End_Date(dd/mm/yy)"].ToString());

                                        if (Valid_Start_Date1.Date > Valid_End_Date2.Date)
                                        {
                                            WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Valid Start Date Should be less then Valid End Date." : WRSMultiplierMasterError.ErrorDescription + ", " + "Valid Start Date Should be less then Valid End Date.";

                                        }
                                    }

                                    string Sale_Start_Date = Convert.ToString(ds.Tables[0].Rows[i]["Sale_Start_Date"].ToString());
                                    if (Sale_Start_Date == null || Sale_Start_Date == "")
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Sale Start Date is Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "Sale Start Date is Required.";
                                    }

                                    string Sale_End_Date = Convert.ToString(ds.Tables[0].Rows[i]["Sale_End_Date"].ToString());
                                    if (Sale_End_Date == null || Sale_End_Date == "")
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Sale End Date is Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "Sale End Date is Required.";
                                    }

                                    if (Sale_Start_Date != "" && Sale_End_Date != "")
                                    {
                                        DateTime Sale_Start_Date1 = Convert.ToDateTime(ds.Tables[0].Rows[i]["Sale_Start_Date"].ToString());
                                        DateTime Sale_End_Date2 = Convert.ToDateTime(ds.Tables[0].Rows[i]["Sale_End_Date"].ToString());

                                        if (Sale_Start_Date1.Date > Sale_End_Date2.Date)
                                        {
                                            if (WRSMultiplierMasterError.ErrorDescription == null)
                                            {

                                                WRSMultiplierMasterError.ErrorDescription = "Sale Start Date Should be less then Sale End Date.";

                                            }
                                            else
                                            {
                                                WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription + ", " + " Sale Start Date Should be less then Sale End Date.";
                                            }

                                        }
                                    }



                                    string SerialNo_Entry_Start_Date = Convert.ToString(ds.Tables[0].Rows[i]["SerialNo_Entry_Start_Date"].ToString());
                                    if (SerialNo_Entry_Start_Date == null || SerialNo_Entry_Start_Date == "")
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "SerialNo Entry Start_Date is Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "SerialNo Entry Start_Date is Required.";
                                    }

                                    string SerialNo_Entry_End_Date = Convert.ToString(ds.Tables[0].Rows[i]["SerialNo_Entry_End_Date"].ToString());
                                    if (SerialNo_Entry_End_Date == null || SerialNo_Entry_End_Date == "")
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "SerialNo Entry End Date is Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "SerialNo Entry End Date is Required.";
                                    }

                                    if (SerialNo_Entry_Start_Date != "" && SerialNo_Entry_End_Date != "")
                                    {
                                        DateTime SerialNo_Entry_Start_Date1 = Convert.ToDateTime(ds.Tables[0].Rows[i]["SerialNo_Entry_Start_Date"].ToString());
                                        DateTime SerialNo_Entry_End_Date2 = Convert.ToDateTime(ds.Tables[0].Rows[i]["SerialNo_Entry_End_Date"].ToString());

                                        if (SerialNo_Entry_Start_Date1.Date > SerialNo_Entry_End_Date2.Date)
                                        {
                                            if (WRSMultiplierMasterError.ErrorDescription == null)
                                            {
                                                WRSMultiplierMasterError.ErrorDescription = "SerialNo Entry Start Date Should be less then  SerialNo Entry End.";
                                            }
                                            else
                                            {
                                                WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription + ", " + "SerialNo Entry Start Date Should be less then  SerialNo Entry End.";
                                            }
                                        }
                                    }


                                    //int_Entries_Count;
                                    //dec_Multiplier_Count;

                                    str_Entries_Count = ds.Tables[0].Rows[i]["Entries_Count"].ToString();
                                    if (str_Entries_Count == null || str_Entries_Count == "")
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Entries Count is Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "Entries Count is Required.";
                                    }
                                    if (str_Entries_Count != null || str_Entries_Count != "")
                                    {
                                        int.TryParse(str_Entries_Count, out int_Entries_Count);
                                        if (int_Entries_Count == 0)
                                        {
                                            WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Multiplier Countt is Allowed Integer Value." : WRSMultiplierMasterError.ErrorDescription + ", " + "Entries Countt is Allowed Integer Value.";
                                        }
                                    }


                                    str_Multiplier_Count = ds.Tables[0].Rows[i]["Multiplier_Count"].ToString();
                                    if (str_Multiplier_Count == null || str_Multiplier_Count == "")
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Multiplier Countt is Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "Multiplier Countt is Required.";
                                    }
                                    if (str_Multiplier_Count != null || str_Multiplier_Count != "")
                                    {

                                        double.TryParse(str_Multiplier_Count, out dec_Multiplier_Count);
                                        if (dec_Multiplier_Count == 0)
                                        {
                                            WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Multiplier Countt is Allowed Decimal Value." : WRSMultiplierMasterError.ErrorDescription + ", " + "Multiplier Countt is Allowed Decimal Value.";
                                        }
                                        else
                                        {
                                            CultureInfo cultures = new CultureInfo("en-US");
                                            string ss = String.Format("{0:0.00}", dec_Multiplier_Count);

                                            dec_Multiplier_Count = Math.Round(dec_Multiplier_Count, 2);
                                        }

                                    }



                                    str_Multiplier_Type = ds.Tables[0].Rows[i]["Multiplier_Type"].ToString();
                                    if (str_Multiplier_Type == null || str_Multiplier_Type == "")
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Multiplier Typeis Required." : WRSMultiplierMasterError.ErrorDescription + ", " + "Multiplier Typeis Required.";
                                    }
                                    if (str_Multiplier_Type.Trim().ToLower() != "add" && str_Multiplier_Type.Trim().ToLower() != "mul")
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Multiplier Type only allowed - Add/Mul." : WRSMultiplierMasterError.ErrorDescription + ", " + "Multiplier Type only allowed - Add/Mul.";
                                    }

                                    //if (str_Multiplier_Type.Trim().ToLower() != "mul")
                                    //{
                                    //    WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription == null ? "Multiplier Type only allowed - Add/Mul." : WRSMultiplierMasterError.ErrorDescription + ", " + "Multiplier Type only allowed - Add/Mul.";
                                    //}


                                    if (WRSMultiplierMasterError.ErrorDescription == null)
                                    {
                                        SuccessCount++;

                                        //if (FileUpload1.FileName.Contains("Error"))   //  if (FileUpload1.FileName.Contains("Error")) 
                                        //{
                                        //var New_UpdateSuccess = from c in db.WRS_Multiplier_Master_Temp
                                        //                        where c.Material_Code == Material_Code && c.State_Name == State_Name && c.FileName != FileUpload1.FileName
                                        //                        select c;

                                        //foreach (var upd in New_UpdateSuccess)
                                        //{

                                        //    upd.RecordStatus = "Success";
                                        //    upd.UploadedDate = InputDateTime;

                                        //}Sr_No



                                        //db.SaveChanges();

                                        var maxIndexCount_Temp = db.WRS_Multiplier_Master_Temp.Count();
                                        WRS_Multiplier_Master_Temp wrs_multiplier_master_temp = new WRS_Multiplier_Master_Temp();
                                        wrs_multiplier_master_temp.Id = maxIndexCount_Temp + 1;
                                        // wrs_multiplier_master_temp.Sr_No = Convert.ToInt64(ds.Tables[0].Rows[i]["Sr_No"]);
                                        wrs_multiplier_master_temp.Material_Code = Material_Code;
                                        wrs_multiplier_master_temp.State_Name = State_Name;
                                        wrs_multiplier_master_temp.FileName = newSuccessFileName;
                                        wrs_multiplier_master_temp.RecordStatus = "Success";
                                        wrs_multiplier_master_temp.UploadedBy = "Admin";
                                        wrs_multiplier_master_temp.UploadedDate = InputDateTime;
                                        db.WRS_Multiplier_Master_Temp.AddObject(wrs_multiplier_master_temp);
                                        db.SaveChanges();

                                        //}
                                        WRS_Multiplier_Master multiplier_Master = new WRS_Multiplier_Master();

                                        multiplier_Master.Material_Code = Material_Code;
                                        WRSMultiplierMasterSuccess.Material_Code = Material_Code;

                                        WRSMultiplierMasterSuccess.itemdesc = itemdesc;
                                        WRSMultiplierMasterSuccess.Item_Type = Item_Type;

                                        multiplier_Master.itemdesc = itemdesc;
                                        multiplier_Master.Item_Type = Item_Type;

                                        multiplier_Master.State_Name = State_Name;
                                        WRSMultiplierMasterSuccess.State_Name = State_Name;


                                        if (Valid_Start_Date != "" && Valid_End_Date != "")
                                        {
                                            multiplier_Master.Valid_Start_Date = Convert.ToDateTime(Valid_Start_Date);
                                            multiplier_Master.Valid_End_Date = Convert.ToDateTime(Valid_End_Date);

                                            WRSMultiplierMasterSuccess.Valid_Start_Date = Convert.ToDateTime(Valid_Start_Date).ToString();
                                            WRSMultiplierMasterSuccess.Valid_End_Date = Convert.ToDateTime(Valid_End_Date).ToString();
                                        }

                                        if (Sale_Start_Date != "" && Sale_End_Date != "")
                                        {
                                            multiplier_Master.Sale_Start_Date = Convert.ToDateTime(Sale_Start_Date);
                                            multiplier_Master.Sale_End_Date = Convert.ToDateTime(Sale_End_Date);

                                            WRSMultiplierMasterSuccess.Sale_Start_Date = Convert.ToDateTime(Sale_Start_Date).ToString();
                                            WRSMultiplierMasterSuccess.Sale_End_Date = Convert.ToDateTime(Sale_End_Date).ToString();
                                        }

                                        if (SerialNo_Entry_Start_Date != "" && SerialNo_Entry_End_Date != "")
                                        {
                                            multiplier_Master.SerialNo_Entry_Start_Date = Convert.ToDateTime(SerialNo_Entry_Start_Date);
                                            multiplier_Master.SerialNo_Entry_End_Date = Convert.ToDateTime(SerialNo_Entry_End_Date);

                                            WRSMultiplierMasterSuccess.SerialNo_Entry_Start_Date = Convert.ToDateTime(SerialNo_Entry_Start_Date).ToString();
                                            WRSMultiplierMasterSuccess.SerialNo_Entry_End_Date = Convert.ToDateTime(SerialNo_Entry_End_Date).ToString();


                                        }

                                        //int_Entries_Count;
                                        //dec_Multiplier_Count;

                                        if (str_Entries_Count != "")
                                        {
                                            multiplier_Master.Entries_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Entries_Count"].ToString());
                                            WRSMultiplierMasterSuccess.Entries_Count = ds.Tables[0].Rows[i]["Entries_Count"].ToString();

                                            WRSMultiplierMasterSuccess.Multiplier_Count = ds.Tables[0].Rows[i]["Entries_Count"].ToString();
                                        }


                                        if (str_Multiplier_Count != "")
                                        {
                                            multiplier_Master.Multiplier_Count = Convert.ToDouble(ds.Tables[0].Rows[i]["Multiplier_Count"].ToString());

                                            WRSMultiplierMasterSuccess.Multiplier_Count = ds.Tables[0].Rows[i]["Multiplier_Count"].ToString();


                                        }

                                        if (str_Multiplier_Type != "")
                                        {
                                            multiplier_Master.Multiplier_Type = ds.Tables[0].Rows[i]["Multiplier_Type"].ToString();

                                            WRSMultiplierMasterSuccess.Multiplier_Type = ds.Tables[0].Rows[i]["Multiplier_Type"].ToString();
                                        }

                                        multiplier_Master.IsActive = true;
                                        multiplier_Master.Created_Date = DateTime.Now;
                                        multiplier_Master.Create_By = "admin";

                                        WRSMultiplierMasterSuccesslst.Add(WRSMultiplierMasterSuccess);

                                        db.WRS_Multiplier_Master.AddObject(multiplier_Master);

                                        //var deleteRow = from c in db.WRS_Multiplier_Master_Temp
                                        //                where c.Material_Code == Material_Code && c.State_Name == State_Name && c.FileName == FileUpload1.FileName
                                        //                select c;

                                        //foreach (var del in deleteRow)
                                        //{
                                        //    db.WRS_Multiplier_Master_Temp.DeleteObject(del);
                                        //}
                                        //if (!FileUpload1.FileName.Contains("Error"))
                                        //{


                                        //    var UpdateSuccess = from c in db.WRS_Multiplier_Master_Temp
                                        //                        where c.Material_Code == Material_Code && c.State_Name == State_Name && c.FileName == FileUpload1.FileName
                                        //                        select c;

                                        //    foreach (var upd in UpdateSuccess)
                                        //    {
                                        //        upd.FileName = newSuccessFileName;
                                        //        upd.RecordStatus = "Success";
                                        //        upd.UploadedDate = InputDateTime;
                                        //        upd.UploadedBy = "Admin";
                                        //    }


                                        //    db.SaveChanges();
                                        //}
                                    }
                                    else
                                    {
                                        ErrorCount++;
                                        //if (FileUpload1.FileName.Contains("Error")) if (FileUpload1.FileName.Contains("Error"))
                                        //{
                                        //if (!db.WRS_Multiplier_Master_Temp.Where(x => x.Material_Code == Material_Code && x.State_Name == stcode && x.RecordStatus == "Success" && x.Sr_No == srno).Any())
                                        //{
                                        var maxIndexCount_Temp = db.WRS_Multiplier_Master_Temp.Count();
                                        WRS_Multiplier_Master_Temp wrs_multiplier_master_temp = new WRS_Multiplier_Master_Temp();
                                        wrs_multiplier_master_temp.Id = maxIndexCount_Temp + 1;
                                        // wrs_multiplier_master_temp.Sr_No = Convert.ToInt64(ds.Tables[0].Rows[i]["Sr_No"]);
                                        wrs_multiplier_master_temp.Material_Code = Material_Code;
                                        wrs_multiplier_master_temp.State_Name = State_Name;
                                        wrs_multiplier_master_temp.FileName = newErrorFileName;
                                        wrs_multiplier_master_temp.RecordStatus = "Error";
                                        wrs_multiplier_master_temp.UploadedBy = "Admin";
                                        wrs_multiplier_master_temp.UploadedDate = InputDateTime;
                                        db.WRS_Multiplier_Master_Temp.AddObject(wrs_multiplier_master_temp);
                                        db.SaveChanges();

                                        //}

                                        //else
                                        //{
                                        //var matCode = Temp_List_WRS_Multiplier_Master_Temp[i].Material_Code;
                                        //var StateName = Temp_List_WRS_Multiplier_Master_Temp[i].State_Name;
                                        //var UpdateError = from c in db.WRS_Multiplier_Master_Temp
                                        //                  where c.Material_Code == matCode && c.State_Name == StateName && c.FileName == FileUpload1.FileName
                                        //                  select c;

                                        //foreach (var upd in UpdateError)
                                        //{
                                        //    upd.FileName = newErrorFileName;
                                        //    upd.RecordStatus = "Error";
                                        //    upd.UploadedDate = InputDateTime;
                                        //    upd.UploadedBy = "Admin";
                                        //}

                                        //db.SaveChanges();
                                        //}




                                        //var matCode = Temp_List_WRS_Multiplier_Master_Temp[i].Material_Code;
                                        //var StateName = Temp_List_WRS_Multiplier_Master_Temp[i].State_Name;

                                        //var updateRow = from c in db.WRS_Multiplier_Master_Temp
                                        //                where c.Material_Code == matCode && c.State_Name == StateName && c.FileName == FileUpload1.FileName
                                        //                select c;

                                        //foreach (var upd in updateRow)
                                        //{
                                        //    upd.FileName = newFileName;
                                        //}


                                        //var UpdateError = from c in db.WRS_Multiplier_Master_Temp
                                        //                  where c.Material_Code == matCode && c.State_Name == StateName && c.FileName == FileUpload1.FileName
                                        //                  select c;

                                        //foreach (var upd in UpdateError)
                                        //{
                                        //    upd.FileName = newErrorFileName;
                                        //    upd.RecordStatus = "Error";
                                        //    upd.UploadedDate = InputDateTime;
                                        //    upd.UploadedBy = "Admin";
                                        //}



                                        WRSMultiplierMasterError.Material_Code = Material_Code;
                                        WRSMultiplierMasterError.State_Name = State_Name;

                                        WRSMultiplierMasterError.itemdesc = itemdesc;
                                        WRSMultiplierMasterError.Item_Type = Item_Type;

                                        WRSMultiplierMasterError.Valid_Start_Date = Valid_Start_Date;
                                        WRSMultiplierMasterError.Valid_End_Date = Valid_End_Date;

                                        WRSMultiplierMasterError.Sale_Start_Date = Sale_Start_Date;
                                        WRSMultiplierMasterError.Sale_End_Date = Sale_End_Date;

                                        WRSMultiplierMasterError.SerialNo_Entry_Start_Date = SerialNo_Entry_Start_Date;
                                        WRSMultiplierMasterError.SerialNo_Entry_End_Date = SerialNo_Entry_End_Date;

                                        WRSMultiplierMasterError.Entries_Count = str_Entries_Count;//double//flood
                                        WRSMultiplierMasterError.Multiplier_Count = str_Multiplier_Count;//double//flood
                                        WRSMultiplierMasterError.Multiplier_Type = str_Multiplier_Type;//Add or Mul


                                        WRSMultiplierMasterErrorlst.Add(WRSMultiplierMasterError);
                                        //}
                                    }
                                }

                                //download
                                string errorpath = Path.Combine(Server.MapPath("~/Upload/") + newErrorFileName);

                                if (WRSMultiplierMasterErrorlst.Count() > 0)
                                {
                                    dt = new DataTable();
                                    dt = downloadExcel.ToDataTable(WRSMultiplierMasterErrorlst);
                                    // downloadExcel.DataTableToExcel(dt, newFileName, spath);
                                    downloadExcel.ToCSV(dt, errorpath);
                                    TempData["Message"] = "Data Upload Successfully.";
                                }
                                else
                                {
                                    ViewBag.Message = "Data Upload Successfully.";
                                }

                                string successpath = Path.Combine(Server.MapPath("~/Upload/") + newSuccessFileName);

                                if (WRSMultiplierMasterSuccesslst.Count() > 0)
                                {
                                    dt = new DataTable();
                                    dt = downloadExcel.ToDataTable(WRSMultiplierMasterSuccesslst);
                                    //downloadExcel.DataTableToExcel(dt, newFileName, spath);
                                    downloadExcel.ToCSV(dt, successpath);
                                    TempData["Message"] = "Data Upload Successfully.";
                                }
                                else
                                {
                                    ViewBag.Message = "Data Upload Successfully.";
                                }


                                //if (WRSMultiplierMasterErrorlst.Count() > 0)
                                //{

                                //    //int year = Convert.ToInt32(DateTime.Now.Year.ToString());
                                //    var grid = new System.Web.UI.WebControls.GridView();
                                //    grid.DataSource = WRSMultiplierMasterErrorlst;
                                //    grid.DataBind();
                                //    Response.ClearContent();
                                //    Response.AddHeader("content-disposition", "attachement; filename=" + newFileName);
                                //    Response.ContentType = "application/excel";
                                //    StringWriter sw = new StringWriter();
                                //    HtmlTextWriter htw = new HtmlTextWriter(sw);
                                //    grid.RenderControl(htw);
                                //    string renderedGridView = sw.ToString();
                                //    System.IO.File.WriteAllText(Path.Combine(Server.MapPath("~/Upload/") + newErrorFileName), renderedGridView);

                                //    //var pathfolder1 = Path.Combine(Server.MapPath("~/Upload/") + newErrorFileName);

                                //    //if (System.IO.File.Exists(pathfolder1))
                                //    //{
                                //    //    System.IO.File.Delete(pathfolder1);
                                //    //}

                                //    //FileUpload1.SaveAs(pathfolder1);
                                //    // Response.Output.Write(sw.ToString());
                                //    // Response.Flush();
                                //    // Response.End();
                                //}


                                //// save success list in upload folder
                                //if (WRSMultiplierMasterSuccesslst.Count() > 0)
                                //{
                                //    int year = Convert.ToInt32(DateTime.Now.Year.ToString());
                                //    var grid = new System.Web.UI.WebControls.GridView();
                                //    grid.DataSource = WRSMultiplierMasterSuccesslst;
                                //    grid.DataBind();
                                //    Response.ClearContent();
                                //    Response.AddHeader("content-disposition", "attachement; filename=" + newFileName);
                                //    Response.ContentType = "application/excel";
                                //    StringWriter sw = new StringWriter();
                                //    HtmlTextWriter htw = new HtmlTextWriter(sw);
                                //    grid.RenderControl(htw);
                                //    string renderedGridView = sw.ToString();
                                //    System.IO.File.WriteAllText(Path.Combine(Server.MapPath("~/Upload/") + newSuccessFileName), renderedGridView);
                                //    // Response.Output.Write(sw.ToString());
                                //    // Response.Flush();
                                //    // Response.End();

                                //    //var pathfolder1 = Path.Combine(Server.MapPath("~/Upload/") + newSuccessFileName);

                                //    //if (System.IO.File.Exists(pathfolder1))
                                //    //{
                                //    //    System.IO.File.Delete(pathfolder1);
                                //    //}


                                //    //FileUpload1.SaveAs(pathfolder1);
                                //}





                                Itemlst();
                                statelst();
                                ConnectPlusMultipleGrid(0);

                            }
                        }
                        else
                        {
                            Itemlst();
                            statelst();
                            TempData["Message"] = "File Not Found.";
                            //ViewBag.Message = "File Not Found.";

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Itemlst();
                statelst();
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                ViewBag.Exception = ex.Message;
                //  TempData["Exception"] = "<script>alert('Some exception has been occurred');</script>";
                //TempData["Exception"] = "<script>alert('Some exception has been occurred');</script>";
                return View("Index");
            }

            //return View("Index");
            return RedirectToAction("Index");
        }

        public JsonResult ConnectPlusMultipleGrid(int? page, int id = 0) //public JsonResult ConnectPlusMultipleGrid(int? page, List<MuliplierModel> MuliplierModels, int id = 0)
        {
            int? PageId = page;
            int totalrecord;
            int totaldetails = 0;
            //if (MuliplierModels != null)
            //{
            //totaldetails = MuliplierModels.Count();//db.WRS_Multiplier_Master_Temp.ToList().Count();
            //  totaldetails =db.WRS_Multiplier_Master_Temp.ToList().Count();
            // }

            List<MuliplierModel> MuliplierModelList = new List<MuliplierModel>();

            List<MuliplierModel> SuccessModel = new List<MuliplierModel>();
            List<MuliplierModel> ErrorModel = new List<MuliplierModel>();

            var datelist = db.WRS_Multiplier_Master_Temp.Select(x => new { x.UploadedDate }).Distinct().OrderByDescending(x => x.UploadedDate);


            foreach (var temp1 in datelist)
            {
                if (!string.IsNullOrEmpty(temp1.UploadedDate.ToString()))
                {
                    int i = 0;
                    var SuccessList = (from a in db.WRS_Multiplier_Master_Temp
                                       where a.UploadedDate == temp1.UploadedDate && a.RecordStatus == "Success"
                                       group a by new
                                       {
                                           a.RecordStatus,
                                           a.FileName,
                                           a.UploadedBy,
                                           a.UploadedDate
                                       } into grp

                                       select new
                                       {
                                           SuccessStatus = grp.Key.RecordStatus,
                                           SuccessCount = grp.Count(),
                                           SuccesFile = grp.Key.FileName,
                                           Success_UploadedBy = grp.Key.UploadedBy,
                                           Success_UploadedDate = grp.Key.UploadedDate

                                       });

                    var ErrorList = (from a in db.WRS_Multiplier_Master_Temp
                                     where a.UploadedDate == temp1.UploadedDate && a.RecordStatus == "Error"
                                     group a by new
                                     {
                                         a.RecordStatus,
                                         a.FileName,
                                         a.UploadedBy,
                                         a.UploadedDate
                                     } into grp1

                                     select new
                                     {
                                         ErrorStatus = grp1.Key.RecordStatus,
                                         ErrorCount = grp1.Count(),
                                         ErrorFile = grp1.Key.FileName,
                                         Error_UploadedBy = grp1.Key.UploadedBy,
                                         Error_UploadedDate = grp1.Key.UploadedDate
                                     });

                    MuliplierModel mul1 = new MuliplierModel();
                    if (SuccessList.Count() > 0)
                    {
                        mul1.SuccessStatus = SuccessList.AsEnumerable().ElementAt(i).SuccessStatus;
                        mul1.SuccessCount = SuccessList.AsEnumerable().ElementAt(i).SuccessCount;
                        mul1.SuccessFile = SuccessList.AsEnumerable().ElementAt(i).SuccesFile;
                        mul1.Success_UploadedBy = SuccessList.AsEnumerable().ElementAt(i).Success_UploadedBy;
                        mul1.Success_UploadedDate = SuccessList.AsEnumerable().ElementAt(i).Success_UploadedDate.ToString();

                    }
                    if (ErrorList.Count() > 0)
                    {
                        mul1.ErrorStatus = ErrorList.AsEnumerable().ElementAt(0).ErrorStatus;
                        mul1.ErrorCount = ErrorList.AsEnumerable().ElementAt(0).ErrorCount;
                        mul1.ErrorFile = ErrorList.AsEnumerable().ElementAt(0).ErrorFile;
                        mul1.Error_UploadedBy = ErrorList.AsEnumerable().ElementAt(0).Error_UploadedBy;
                        mul1.Error_UploadedDate = ErrorList.AsEnumerable().ElementAt(0).Error_UploadedDate.ToString();
                        //MuliplierModelList.Add(mul1);

                    }
                    MuliplierModelList.Add(mul1);

                    i = i + 1;
                }
            }






            //var SFile = db.WRS_Multiplier_Master_Temp.Where(x => x.FileName.Contains("Success")).OrderByDescending(x=>x.UploadedDate).Select(x => new { x.FileName }).Distinct();

            //var SFile = (from a in db.WRS_Multiplier_Master_Temp
            //             where a.RecordStatus == "Success"
            //             group a by new
            //             {
            //                 a.FileName,
            //                 a.UploadedDate
            //             } into g1
            //             select new
            //             {
            //                 FileName = g1.Key.FileName,
            //                 UploadedDate = g1.Key.UploadedDate
            //             }).OrderByDescending(x => x.UploadedDate);

            //var EFile = db.WRS_Multiplier_Master_Temp.Where(x => x.FileName.Contains("Error")).OrderByDescending(x=>x.UploadedDate).Select(x => new { x.FileName }).Distinct();

            //var EFile = (from a in db.WRS_Multiplier_Master_Temp
            //             where a.RecordStatus == "Error"
            //             group a by new
            //             {
            //                 a.FileName,
            //                 a.UploadedDate
            //             } into g1
            //             select new
            //             {
            //                 FileName = g1.Key.FileName,
            //                 UploadedDate = g1.Key.UploadedDate
            //             }).OrderByDescending(x => x.UploadedDate);


            //foreach (var temp1 in SFile)
            //{
            //    var SuccessList = (from a in db.WRS_Multiplier_Master_Temp
            //                       where a.FileName == temp1.FileName && a.RecordStatus == "Success"
            //                       group a by new
            //                       {
            //                           a.RecordStatus,
            //                           a.FileName,
            //                           a.UploadedBy,
            //                           a.UploadedDate
            //                       } into grp

            //                       select new
            //                       {
            //                           SuccessStatus = grp.Key.RecordStatus,
            //                           SuccessCount = grp.Count(),
            //                           SuccesFile = grp.Key.FileName,
            //                           Success_UploadedBy = grp.Key.UploadedBy,
            //                           Success_UploadedDate = grp.Key.UploadedDate

            //                       });

            //    MuliplierModel mul1 = new MuliplierModel();
            //    mul1.SuccessStatus = SuccessList.AsEnumerable().ElementAt(0).SuccessStatus;
            //    mul1.SuccessCount = SuccessList.AsEnumerable().ElementAt(0).SuccessCount;
            //    mul1.SuccessFile = SuccessList.AsEnumerable().ElementAt(0).SuccesFile;
            //    mul1.Success_UploadedBy = SuccessList.AsEnumerable().ElementAt(0).Success_UploadedBy;
            //    mul1.Success_UploadedDate = SuccessList.AsEnumerable().ElementAt(0).Success_UploadedDate.ToString();
            //    SuccessModel.Add(mul1);
            //}

            //foreach (var temp2 in EFile)
            //{
            //    var ErrorList = (from a in db.WRS_Multiplier_Master_Temp
            //                     where a.FileName == temp2.FileName && a.RecordStatus == "Error"
            //                     group a by new
            //                     {
            //                         a.RecordStatus,
            //                         a.FileName,
            //                         a.UploadedBy,
            //                         a.UploadedDate
            //                     } into grp1

            //                     select new
            //                     {
            //                         ErrorStatus = grp1.Key.RecordStatus,
            //                         ErrorCount = grp1.Count(),
            //                         ErrorFile = grp1.Key.FileName,
            //                         Error_UploadedBy = grp1.Key.UploadedBy,
            //                         Error_UploadedDate = grp1.Key.UploadedDate
            //                     });

            //    MuliplierModel mul2 = new MuliplierModel();
            //    mul2.ErrorStatus = ErrorList.AsEnumerable().ElementAt(0).ErrorStatus;
            //    mul2.ErrorCount = ErrorList.AsEnumerable().ElementAt(0).ErrorCount;
            //    mul2.ErrorFile = ErrorList.AsEnumerable().ElementAt(0).ErrorFile;
            //    mul2.Error_UploadedBy = ErrorList.AsEnumerable().ElementAt(0).Error_UploadedBy;
            //    mul2.Error_UploadedDate = ErrorList.AsEnumerable().ElementAt(0).Error_UploadedDate.ToString();
            //    ErrorModel.Add(mul2);
            //}

            //var list = (from a in db.WRS_Multiplier_Master_Temp
            //            group a by new
            //            {
            //                a.FileName,
            //                a.UploadedDate,
            //                a.RecordStatus
            //            } into g1
            //            select new
            //            {
            //                FileName = g1.Key.FileName,
            //                UploadedDate = g1.Key.UploadedDate,
            //                Count = g1.Count(),
            //                RecordStatus = g1.Key.RecordStatus
            //            }).Distinct().OrderByDescending(x => x.UploadedDate).ToList();

            //for (int i = 0, j = 0; i < list.Count(); i++, j++)
            //{
            //    MuliplierModel mul = new MuliplierModel();
            //    //if ((list[i].FileName.Contains("Error_StockReport_") && list[i + 1].FileName.Contains("Success_Error_StockReport_")))
            //    //{


            //        if (list[i].RecordStatus == "Success")
            //        {
            //            mul.ErrorStatus = null;
            //            mul.ErrorCount = 0;
            //            mul.ErrorFile = null;
            //            mul.Error_UploadedBy = null;
            //            mul.Error_UploadedDate = null;

            //            mul.SuccessStatus = list[i].RecordStatus;
            //            mul.SuccessCount = list[i].Count;
            //            mul.SuccessFile = list[i].FileName;
            //            mul.Success_UploadedBy = "Admin";
            //            mul.Success_UploadedDate = list[i].UploadedDate.ToString();


            //        }
            //        if (list[i].RecordStatus == "Error")
            //        {
            //            mul.ErrorStatus = list[i].RecordStatus;
            //            mul.ErrorCount = list[i].Count;
            //            mul.ErrorFile = list[i].FileName;
            //            mul.Error_UploadedBy = "Admin";
            //            mul.Error_UploadedDate = list[i].UploadedDate.ToString();

            //            mul.SuccessStatus = null;
            //            mul.SuccessCount = 0;
            //            mul.SuccessFile = null;
            //            mul.Success_UploadedBy = null;
            //            mul.Success_UploadedDate = null;

            //        }

            //    //}
            //    //if ((list[i].FileName.Contains("Error_StockReport_") && list[i + 1].FileName.Contains("Success_StockReport_")))
            //    //{



            //    //}

            //    MuliplierModelList.Add(mul);
            //}






            //for (int i = 0, j = 0; i < SuccessModel.Count() || j < ErrorModel.Count(); i++, j++)
            //{
            //    MuliplierModel mul = new MuliplierModel();

            //    if (i < ErrorModel.Count())
            //    {

            //        mul.ErrorStatus = ErrorModel.AsEnumerable().ElementAt(i).ErrorStatus;
            //        if (i == 0)
            //        {
            //            mul.ErrorCount = ErrorModel.AsEnumerable().ElementAt(i).ErrorCount;
            //        }
            //        else
            //        {
            //            mul.ErrorCount = MuliplierModelList[i - 1].SuccessCount + MuliplierModelList[i - 1].ErrorCount;
            //        }

            //        mul.ErrorFile = ErrorModel.AsEnumerable().ElementAt(i).ErrorFile;
            //        mul.Error_UploadedBy = ErrorModel.AsEnumerable().ElementAt(i).Error_UploadedBy;
            //        mul.Error_UploadedDate = ErrorModel.AsEnumerable().ElementAt(i).Error_UploadedDate.ToString();
            //    }
            //    if (j < SuccessModel.Count())
            //    {
            //        mul.SuccessStatus = SuccessModel.AsEnumerable().ElementAt(j).SuccessStatus;
            //        mul.SuccessCount = SuccessModel.AsEnumerable().ElementAt(j).SuccessCount;

            //        if (j >0)
            //        {
            //            mul.ErrorCount = MuliplierModelList[j - 1].SuccessCount + MuliplierModelList[j - 1].ErrorCount;
            //        }
            //        mul.SuccessFile = SuccessModel.AsEnumerable().ElementAt(j).SuccessFile;
            //        mul.Success_UploadedBy = SuccessModel.AsEnumerable().ElementAt(j).Success_UploadedBy;
            //        mul.Success_UploadedDate = SuccessModel.AsEnumerable().ElementAt(j).Success_UploadedDate.ToString();
            //    }

            //    MuliplierModelList.Add(mul);
            //}
            //for (int i = 0, j = 0; i <= SuccessModel.Count() || j <= ErrorModel.Count(); i++, j++)
            //{
            //    MuliplierModel mul = new MuliplierModel();
            //    var sfile=ErrorModel.AsEnumerable().ElementAt(i).ErrorFile;
            //    var efile=SuccessModel.AsEnumerable().ElementAt(j).SuccessFile;
            //    int ecount=ErrorModel.AsEnumerable().ElementAt(i).ErrorCount;
            //    int scount=SuccessModel.AsEnumerable().ElementAt(j).SuccessCount;

            //    int escount = ErrorModel.AsEnumerable().ElementAt(i).SuccessCount;
            //    int secount = SuccessModel.AsEnumerable().ElementAt(j).ErrorCount;


            //    if ((ecount != 0 && escount == 0) || (scount != 0 && secount == 0))
            //    {
            //        if(SuccessModel.AsEnumerable().ElementAt(j).SuccessCount==0)
            //        {
            //            mul.ErrorStatus = ErrorModel.AsEnumerable().ElementAt(i).ErrorStatus;
            //            mul.ErrorCount = ErrorModel.AsEnumerable().ElementAt(i).ErrorCount;
            //            mul.ErrorFile = ErrorModel.AsEnumerable().ElementAt(i).ErrorFile;
            //            mul.Error_UploadedBy = ErrorModel.AsEnumerable().ElementAt(i).Error_UploadedBy;
            //            mul.Error_UploadedDate = ErrorModel.AsEnumerable().ElementAt(i).Error_UploadedDate.ToString();

            //            mul.SuccessStatus = null;
            //            mul.SuccessCount = 0;
            //            mul.SuccessFile = null;
            //            mul.Success_UploadedBy = null;
            //            mul.Success_UploadedDate = null;

            //        }
            //        if (SuccessModel.AsEnumerable().ElementAt(j).ErrorCount == 0)
            //        {                       

            //            mul.SuccessStatus = SuccessModel.AsEnumerable().ElementAt(j).SuccessStatus;
            //            mul.SuccessCount = SuccessModel.AsEnumerable().ElementAt(j).SuccessCount;                        
            //            mul.SuccessFile = SuccessModel.AsEnumerable().ElementAt(j).SuccessFile;
            //            mul.Success_UploadedBy = SuccessModel.AsEnumerable().ElementAt(j).Success_UploadedBy;
            //            mul.Success_UploadedDate = SuccessModel.AsEnumerable().ElementAt(j).Success_UploadedDate.ToString();

            //            mul.ErrorStatus = null;
            //            mul.ErrorCount = 0;
            //            mul.ErrorFile = null;
            //            mul.Error_UploadedBy = null;
            //            mul.Error_UploadedDate = null;

            //        }


            //    }

            //    if ((sfile.Contains("Success_StockReport") && efile.Contains("Error_StockReport")) || (sfile.Contains("Success_Error_StockReport") && efile.Contains("Error_Error_StockReport")) || (sfile.Contains("Success_Success_Error_StockReport") && string.IsNullOrEmpty(efile)))
            //    {
            //        if (i < ErrorModel.Count())
            //        {

            //            mul.ErrorStatus = ErrorModel.AsEnumerable().ElementAt(i).ErrorStatus;

            //            if (i == 0)
            //            {
            //                mul.ErrorCount = ErrorModel.AsEnumerable().ElementAt(i).ErrorCount;
            //            }
            //            else
            //            {
            //                mul.ErrorCount = MuliplierModelList[i - 1].SuccessCount + MuliplierModelList[i - 1].ErrorCount;
            //            }


            //            mul.ErrorFile = ErrorModel.AsEnumerable().ElementAt(i).ErrorFile;
            //            mul.Error_UploadedBy = ErrorModel.AsEnumerable().ElementAt(i).Error_UploadedBy;
            //            mul.Error_UploadedDate = ErrorModel.AsEnumerable().ElementAt(i).Error_UploadedDate.ToString();
            //        }
            //        if (j < SuccessModel.Count())
            //        {
            //            mul.SuccessStatus = SuccessModel.AsEnumerable().ElementAt(j).SuccessStatus;
            //            mul.SuccessCount = SuccessModel.AsEnumerable().ElementAt(j).SuccessCount;
            //            if (j > 0)
            //            {
            //                mul.ErrorCount = MuliplierModelList[j - 1].SuccessCount + MuliplierModelList[j - 1].ErrorCount;
            //            }
            //            mul.SuccessFile = SuccessModel.AsEnumerable().ElementAt(j).SuccessFile;
            //            mul.Success_UploadedBy = SuccessModel.AsEnumerable().ElementAt(j).Success_UploadedBy;
            //            mul.Success_UploadedDate = SuccessModel.AsEnumerable().ElementAt(j).Success_UploadedDate.ToString();
            //        }

            //    }



            //    MuliplierModelList.Add(mul);
            //}


            totaldetails = MuliplierModelList.Count();



            if (page != null)
            {
                page = (page - 1) * 15;
            }
            if (totaldetails % 15 == 0)
            {
                totalrecord = totaldetails / 15;
            }
            else
            {
                totalrecord = (totaldetails / 15) + 1;
            }


            if (totaldetails != 0)
            {
                //var contactDetails2 = (from c in db.WRS_Multiplier_Master_Temp
                //                       select c).ToList();

                var contactDetails2 = MuliplierModelList;


                var data = new { result = contactDetails2, TotalRecord = totalrecord };
                return Json(data, JsonRequestBehavior.AllowGet);

            }

            //if (id != 0)
            //{





            //}

            return Json("Null", JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Download(string fileName)
        {
            //  fileName = "StockReport_23-12-2019-T-10_57_43_2019_12_23_11_12_34.xlsx";

            //Get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath("~/Upload"), fileName);
            byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            //System.IO.File.Delete(fullPath);
            return File(fileByteArray, "text/csv", fileName);
        }

        [HttpGet]
        public ActionResult Download1(string fileName)
        {
            Itemlst();
            statelst();
            //  fileName = "StockReport_23-12-2019-T-10_57_43_2019_12_23_11_12_34.xlsx";
            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                    //Get the temp folder and file path in server
                    string fullPath = Path.Combine(Server.MapPath("~/Upload"), fileName);
                    byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
                    ////System.IO.File.Delete(fullPath);
                    //return File(fileByteArray, "application/vnd.ms-excel", fileName);


                    //string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //var fileName1 = Path.GetFileName(fullPath);
                    //return File(fileName, contentType, fileName1);



                    System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                    response.Clear();
                    response.Buffer = true;
                    response.Charset = "";
                    response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    response.BinaryWrite(fileByteArray);
                    //add following code
                    response.Flush();
                    response.End();

                }

                catch (Exception ex)
                {
                    TempData["Message"] = "File Not Found.";


                }

            }
            else
            {
                TempData["Message"] = "File Not Found.";
                //   ViewBag.Message 
            }

            return View("Index");
        }


        public string arrItemResultdata(string itemId)
        {
            Itemlst();
            var data = ViewBag.itemslst;
            string[] arr = itemId.ToString().Split(',');
            string result = "";
            //  string[] arrResult = new string[] { };            
            foreach (var itm in arr)
            {
                string val = itm;
                foreach (var item in data)
                {
                    if (item.Value == val.ToString())
                    {
                        if (result != "")
                        {
                            result = result + "," + item.Text;
                        }
                        else
                        {
                            result = item.Text;
                        }
                    }

                }
            }
            // arrResult = result.Split(',');
            return result;
        }


        public string[] arrStateResultdata(string stateId)
        {
            statelst();
            var data = ViewBag.statelst;
            string[] arr = stateId.ToString().Split(',');
            string result = "";
            string[] arrResult = new string[] { };

            foreach (var itm in arr)
            {
                string val = itm;
                foreach (var item in data)
                {
                    if (item.Value == val.ToString())
                    {
                        if (result != "")
                        {
                            result = result + "," + item.Text;
                        }
                        else
                        {
                            result = item.Text;
                        }
                    }

                }
            }

            arrResult = result.Split(',');
            return arrResult;
        }



        public string[] arrItemResultdataNew(string itemId)
        {
            Itemlst();
            var data = ViewBag.itemslst;
            string[] arr = itemId.ToString().Split(',');
            string result = "";
            string[] arrResult = new string[] { };
            foreach (var itm in arr)
            {
                string val = itm;
                foreach (var item in data)
                {
                    if (item.Value == val.ToString())
                    {
                        if (result != "")
                        {
                            result = result + "," + item.Text;
                        }
                        else
                        {
                            result = item.Text;
                        }
                    }
                }
            }
            arrResult = result.Split(',');
            return arrResult;
        }

        public void Itemlst()
        {
            connectplusmulti objTest = new connectplusmulti();
            ViewBag.itemslst = objTest.ItemLstConnectPlusTest(); ;
        }

        public void statelst()
        {
            connectplusmulti objTest = new connectplusmulti();
            ViewBag.statelst = objTest.StateLstConnectPlusTest(); ;
        }


        public ActionResult UploadMultiplier()
        {

            return View();
        }

        //public ActionResult CouponMappingBulkImport(HttpPostedFileBase FileUpload)
        [HttpPost]
        public ActionResult UploadMultiplier(HttpPostedFileBase FileUpload1)
        {
            IList<WRSMultiplierMasterError> WRSMultiplierMasterErrorlst = new List<WRSMultiplierMasterError>();
            WRS_Multiplier_Master_Temp WRS_Multiplier_Master_Temp = new WRS_Multiplier_Master_Temp();
            var Temp_List_WRS_Multiplier_Master_Temp = db.WRS_Multiplier_Master_Temp.ToList();

            DataTable table = new DataTable();
            try
            {
                if (FileUpload1 != null)
                {
                    string connString = "";
                    string ext = System.IO.Path.GetExtension(Path.GetFileName(FileUpload1.FileName));
                    var originalfilename = Path.GetFileName(FileUpload1.FileName);
                    if (ext == ".xlsx" || ext == ".xls")
                    {
                        TempData["extension"] = null;
                        TempData["Successfully"] = null;
                        TempData["Blankfile"] = null;
                        string xlsextension = string.Empty;
                        string[] changeextension = originalfilename.Split('.');
                        if (ext == ".xlsx")
                        {
                            xlsextension = changeextension[0] + ".xlsx";
                        }
                        else
                        {
                            xlsextension = changeextension[0] + ".xls";
                        }

                        string path =
Path.Combine(Path.GetDirectoryName(xlsextension)
, string.Concat(Path.GetFileNameWithoutExtension(xlsextension)
, DateTime.Now.ToString("_yyyy_MM_dd_HH_MM_ss")
, Path.GetExtension(xlsextension)
)
);

                        var pathfolder = Path.Combine(Server.MapPath("~/Upload/"), path);

                        if (System.IO.File.Exists(pathfolder))
                        {
                            System.IO.File.Delete(pathfolder);
                        }


                        FileUpload1.SaveAs(pathfolder);


                        //Connection String to Excel Workbook

                        if (ext.Trim() == ".xls")
                        {
                            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathfolder + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
                        }
                        else if (ext.Trim() == ".xlsx")
                        {
                            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathfolder + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                        }

                        OleDbConnection conn = new OleDbConnection(connString);
                        if (conn.State == ConnectionState.Closed)

                            conn.Open();


                        String[] sheetName = GetExcelSheetNames(pathfolder, ext);

                        //string query = "SELECT * FROM [Sheet1$]";

                        string query = "SELECT * FROM " + "[" + sheetName[0] + "]";

                        OleDbCommand cmd = new OleDbCommand(query, conn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            WRSMultiplierMasterError WRSMultiplierMasterError = new WRSMultiplierMasterError();
                            string str_Multiplier_Count = "";
                            string str_Multiplier_Type = "";
                            string str_Entries_Count = "";

                            var Material_Code = ds.Tables[0].Rows[i]["Material_Code"].ToString();

                            //var itemdesc = ds.Tables[0].Rows[i]["itemdesc"].ToString();
                            //var Item_Type = ds.Tables[0].Rows[i]["Item_Type"].ToString();

                            var State_Name = ds.Tables[0].Rows[i]["State_Name"].ToString();

                            if (Temp_List_WRS_Multiplier_Master_Temp[i].Material_Code != Material_Code)
                            {
                                WRSMultiplierMasterError.ErrorDescription = "Material Code is Not Valid ";
                            }

                            if (Temp_List_WRS_Multiplier_Master_Temp[i].State_Name != State_Name)
                            {
                                WRSMultiplierMasterError.ErrorDescription = "State Name is Not Valid ";
                            }

                            string Valid_Start_Date = Convert.ToString(ds.Tables[0].Rows[i]["Valid_Start_Date"].ToString());
                            string Valid_End_Date = Convert.ToString(ds.Tables[0].Rows[i]["Valid_End_Date"].ToString());
                            if (Valid_Start_Date != "" && Valid_End_Date != "")
                            {
                                DateTime Valid_Start_Date1 = Convert.ToDateTime(ds.Tables[0].Rows[i]["Valid_Start_Date"].ToString());
                                DateTime Valid_End_Date2 = Convert.ToDateTime(ds.Tables[0].Rows[i]["Valid_End_Date"].ToString());

                                if (Valid_Start_Date1.Date > Valid_End_Date2.Date)
                                {
                                    WRSMultiplierMasterError.ErrorDescription = "Valid Start Date Should be less then Valid End Date.";

                                }
                            }

                            string Sale_Start_Date = Convert.ToString(ds.Tables[0].Rows[i]["Sale_Start_Date"].ToString());
                            string Sale_End_Date = Convert.ToString(ds.Tables[0].Rows[i]["Sale_End_Date"].ToString());
                            if (Sale_Start_Date != "" && Sale_End_Date != "")
                            {
                                DateTime Sale_Start_Date1 = Convert.ToDateTime(ds.Tables[0].Rows[i]["Sale_Start_Date"].ToString());
                                DateTime Sale_End_Date2 = Convert.ToDateTime(ds.Tables[0].Rows[i]["Sale_End_Date"].ToString());

                                if (Sale_Start_Date1.Date > Sale_End_Date2.Date)
                                {
                                    if (WRSMultiplierMasterError.ErrorDescription == null)
                                    {

                                        WRSMultiplierMasterError.ErrorDescription = "Sale Start Date Should be less then Sale End Date.";

                                    }
                                    else
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription + ", " + " Sale Start Date Should be less then Sale End Date.";
                                    }

                                }
                            }



                            string SerialNo_Entry_Start_Date = Convert.ToString(ds.Tables[0].Rows[i]["SerialNo_Entry_Start_Date"].ToString());
                            string SerialNo_Entry_End_Date = Convert.ToString(ds.Tables[0].Rows[i]["SerialNo_Entry_End_Date"].ToString());
                            if (SerialNo_Entry_Start_Date != "" && SerialNo_Entry_End_Date != "")
                            {
                                DateTime SerialNo_Entry_Start_Date1 = Convert.ToDateTime(ds.Tables[0].Rows[i]["SerialNo_Entry_Start_Date"].ToString());
                                DateTime SerialNo_Entry_End_Date2 = Convert.ToDateTime(ds.Tables[0].Rows[i]["SerialNo_Entry_End_Date"].ToString());

                                if (SerialNo_Entry_Start_Date1.Date > SerialNo_Entry_End_Date2.Date)
                                {
                                    if (WRSMultiplierMasterError.ErrorDescription == null)
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = "SerialNo Entry Start Date Should be less then  SerialNo Entry End.";
                                    }
                                    else
                                    {
                                        WRSMultiplierMasterError.ErrorDescription = WRSMultiplierMasterError.ErrorDescription + ", " + "SerialNo Entry Start Date Should be less then  SerialNo Entry End.";
                                    }
                                }
                            }

                            str_Entries_Count = ds.Tables[0].Rows[i]["Entries_Count"].ToString();
                            str_Multiplier_Count = ds.Tables[0].Rows[i]["Multiplier_Count"].ToString();
                            str_Multiplier_Type = ds.Tables[0].Rows[i]["Multiplier_Type"].ToString();


                            if (WRSMultiplierMasterError.ErrorDescription == null)
                            {
                                WRS_Multiplier_Master multiplier_Master = new WRS_Multiplier_Master();

                                multiplier_Master.Material_Code = Material_Code;
                                multiplier_Master.State_Name = State_Name;

                                if (Valid_Start_Date != "" && Valid_End_Date != "")
                                {
                                    multiplier_Master.Valid_Start_Date = Convert.ToDateTime(Valid_Start_Date);
                                    multiplier_Master.Valid_End_Date = Convert.ToDateTime(Valid_End_Date);
                                }

                                if (Sale_Start_Date != "" && Sale_End_Date != "")
                                {
                                    multiplier_Master.Sale_Start_Date = Convert.ToDateTime(Sale_Start_Date);
                                    multiplier_Master.Sale_End_Date = Convert.ToDateTime(Sale_End_Date);
                                }

                                if (SerialNo_Entry_Start_Date != "" && SerialNo_Entry_End_Date != "")
                                {
                                    multiplier_Master.SerialNo_Entry_Start_Date = Convert.ToDateTime(SerialNo_Entry_Start_Date);
                                    multiplier_Master.SerialNo_Entry_End_Date = Convert.ToDateTime(SerialNo_Entry_End_Date);
                                }


                                if (str_Entries_Count != "")
                                {
                                    multiplier_Master.Entries_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Entries_Count"].ToString());
                                }


                                if (str_Multiplier_Count != "")
                                {
                                    multiplier_Master.Multiplier_Count = Convert.ToDouble(ds.Tables[0].Rows[i]["Multiplier_Count"].ToString());
                                }

                                if (str_Multiplier_Type != "")
                                {
                                    multiplier_Master.Multiplier_Type = ds.Tables[0].Rows[i]["Multiplier_Type"].ToString();
                                }

                                multiplier_Master.IsActive = true;
                                multiplier_Master.Created_Date = DateTime.Now;
                                multiplier_Master.Create_By = "admin";

                                db.WRS_Multiplier_Master.AddObject(multiplier_Master);

                                var deleteRow = Temp_List_WRS_Multiplier_Master_Temp.Where(x => x.Material_Code == Material_Code).FirstOrDefault();

                                db.WRS_Multiplier_Master_Temp.DeleteObject(deleteRow);
                                db.SaveChanges();
                            }
                            else
                            {
                                WRSMultiplierMasterError.Material_Code = Material_Code;
                                WRSMultiplierMasterError.State_Name = State_Name;

                                WRSMultiplierMasterError.Valid_Start_Date = Valid_Start_Date;
                                WRSMultiplierMasterError.Valid_End_Date = Valid_End_Date;

                                WRSMultiplierMasterError.Sale_Start_Date = Sale_Start_Date;
                                WRSMultiplierMasterError.Sale_End_Date = Sale_End_Date;

                                WRSMultiplierMasterError.SerialNo_Entry_Start_Date = SerialNo_Entry_Start_Date;
                                WRSMultiplierMasterError.SerialNo_Entry_End_Date = SerialNo_Entry_End_Date;

                                WRSMultiplierMasterError.Entries_Count = str_Entries_Count;
                                WRSMultiplierMasterError.Multiplier_Count = str_Multiplier_Count;
                                WRSMultiplierMasterError.Multiplier_Type = str_Multiplier_Type;

                                WRSMultiplierMasterErrorlst.Add(WRSMultiplierMasterError);
                            }
                        }

                        ////download
                        //if (WRSMultiplierMasterErrorlst.Count() > 0)
                        //{
                        //    DataTable dt = downloadExcel.ToDataTable(WRSMultiplierMasterErrorlst);
                        //    downloadExcel.DataTableToExcel(dt, "Error_" + FileUpload1.FileName);
                        //}

                    }
                }
                return View();
            }
            catch (Exception exc)
            {
                //if (conn != null)
                //{
                //    conn.Close();
                //    conn.Dispose();
                //}
                //if (dt != null)
                //{
                //    dt.Dispose();
                //}
                TempData["Exception"] = "<script>alert('Some exception has been occurred');</script>";
                return View("UploadMultiplier");
            }

        }


        private String[] GetExcelSheetNames(string excelFile, string ext)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            try
            {
                String connString = "";
                if (ext.Trim() == ".xls")
                {
                    //  connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelFile + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFile + ";Extended Properties=\"Excel 12.0;\"";
                }
                else if (ext.Trim() == ".xlsx")
                {
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFile + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                }


                //String connString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                //    "Data Source=" + excelFile + ";Extended Properties=Excel 8.0;";

                objConn = new OleDbConnection(connString);

                objConn.Open();

                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }

                // Loop through all of the sheets if you want too...
                for (int j = 0; j < excelSheets.Length; j++)
                {
                    // Query each excel sheet.
                }

                return excelSheets;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }


        private void Donloadfile(DataTable dt, DateTime sDate, DateTime eDate)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                            Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));
            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field =>
                  string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                sb.AppendLine(string.Join(",", fields));
            }
            string sfilename = "Log_" + sDate.ToShortDateString() + "to" + eDate.ToShortDateString() + ".csv";
            var response = System.Web.HttpContext.Current.Response;
            response.BufferOutput = true;
            response.Clear();
            response.ClearHeaders();
            response.ContentEncoding = Encoding.Unicode;
            response.AddHeader("content-disposition", "attachment;filename=\"" + sfilename + "\" ");
            response.ContentType = "text/csv";
            response.Write(sb.ToString());
            response.End();
        }




    }
}