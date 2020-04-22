using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LuminousMpartnerIB.Models
{
    public class connectplusmulti
    {
        private static string ApiName { get; set; }

        public List<SelectListItem> ItemLstConnectPlusTest()
        {
            connectplusmultiResult EmpDistDealerData = new connectplusmultiResult();
            Dictionary<Int32, string> dicObj = new Dictionary<Int32, string>();

            List<SelectListItem> itemsDist = new List<SelectListItem>();
            string token = "pass@1234";

            var result2 = GetDataFromApi.getConnectPlusTest("webservice_WRS_Item_Type_Master", token);//item

            if (result2.IsSuccessStatusCode)
            {
                var responseResult2 = result2.Content.ReadAsStringAsync().Result;
                JToken JtokenResult2 = JToken.Parse(responseResult2);
                // string[] arra =JsonConvert.DeserializeObject<string[]>(responseResult2);

                JArray array = JArray.Parse(responseResult2);

                //itemsDist.Add(new SelectListItem
                //{
                //    Text = "All",
                //    Value = "1",
                //    //Selected = true
                //});

                int i = 1;
                foreach (JObject obj in array.Children<JObject>())
                {
                    foreach (JProperty singleProp in obj.Properties())
                    {
                        itemsDist.Add(new SelectListItem
                        {
                            Text = singleProp.Value.ToString(),
                            Value = i.ToString(),

                        });
                        i = i + 1;
                        //dicObj.Add(i, singleProp.Value.ToString());
                        //i = i + 1;
                        //// string value = singleProp.Value.ToString();
                    }
                }

            }
            return itemsDist;
        }

        public List<SelectListItem> StateLstConnectPlusTest()
        {
            connectplusmultiResult EmpDistDealerData = new connectplusmultiResult();
            Dictionary<Int32, string> dicObj = new Dictionary<Int32, string>();

            List<SelectListItem> itemsDist = new List<SelectListItem>();
            string token = "pass@1234";

            var result2 = GetDataFromApi.getConnectPlusTest("webservice_WRS_StateMaster", token); //state         

            if (result2.IsSuccessStatusCode)
            {
                var responseResult2 = result2.Content.ReadAsStringAsync().Result;
                JToken JtokenResult2 = JToken.Parse(responseResult2);
                // string[] arra =JsonConvert.DeserializeObject<string[]>(responseResult2);

                JArray array = JArray.Parse(responseResult2);

                //itemsDist.Add(new SelectListItem
                //{
                //    Text = "All",
                //    Value = "1",
                //    //Selected = true
                //});

                int i = 1;
                foreach (JObject obj in array.Children<JObject>())
                {
                    foreach (JProperty singleProp in obj.Properties())
                    {
                        itemsDist.Add(new SelectListItem
                        {
                            Text = singleProp.Value.ToString(),
                            Value = i.ToString(),

                        });
                        i = i + 1;
                        //dicObj.Add(i, singleProp.Value.ToString());
                        //i = i + 1;
                        //// string value = singleProp.Value.ToString();
                    }
                }

            }
            return itemsDist;
        }

        public List<Item> ItemLstConnectPlus_ItemList(UserDataParameter ud)
        {
            connectplusmultiResult EmpDistDealerData = new connectplusmultiResult();
            Dictionary<Int32, string> dicObj = new Dictionary<Int32, string>();

            List<SelectListItem> itemsDist = new List<SelectListItem>();
            //string token = "pass@1234";
            UploadViewModel vm = new UploadViewModel();

            var result2 = GetDataFromApi.getConnectPlus_ItemList("webservice_WRS_Item_Type_Master", ud);//item

            if (result2.IsSuccessStatusCode)
            {
                var responseResult2 = result2.Content.ReadAsStringAsync().Result;
                JToken JtokenResult2 = JToken.Parse(responseResult2);
                vm.ItemList = JsonConvert.DeserializeObject<List<Item>>(JtokenResult2.ToString());
            }



            return vm.ItemList;
        }
    }

    public class downloadExcel
    {
        public static DataTable ToDataTable<T>(IList<T> items)
        {

            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                if (prop.Name.Contains("Valid"))
                {
                    dataTable.Columns.Add(prop.Name+"(dd/mm/yy)");
                }
                else
                {
                    dataTable.Columns.Add(prop.Name);
                }
                
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
            //ds.Tables.Add(dataTable);
            // return ds;
        }

        public static DataSet ReadCsvFile(string FileSaveWithPath)
        {
            DataSet ds = new DataSet();
            DataTable dtCsv = new DataTable();
            string Fulltext;
            // if (FileUpload1.HasFile)
            // {
            //string FileSaveWithPath = Server.MapPath("\\Files\\Import" + System.DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".csv");
            //  FileUpload1.SaveAs(FileSaveWithPath);
            using (StreamReader sr = new StreamReader(FileSaveWithPath))
            {
                while (!sr.EndOfStream)
                {
                    Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                    for (int i = 0; i < rows.Count() - 1; i++)
                    {
                        string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < rowValues.Count(); j++)
                                {
                                    dtCsv.Columns.Add(rowValues[j]); //add headers  
                                }
                            }
                            else
                            {
                                DataRow dr = dtCsv.NewRow();
                                for (int k = 0; k < rowValues.Count(); k++)
                                {
                                    dr[k] = rowValues[k].ToString();
                                }
                                dtCsv.Rows.Add(dr); //add other rows  
                            }
                        }
                    }
                }
            }
            //}
            //return dtCsv;
            ds.Tables.Add(dtCsv);
            return ds;
        }

        public static void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        public static void DataTableToExcel(DataTable dt, string Filename, string spath = "")
        {
            MemoryStream ms = DataTableToExcelXlsx(dt, "Sheet1", spath);
            if (spath == "")
            {
                ms.WriteTo(HttpContext.Current.Response.OutputStream);
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Filename);
                HttpContext.Current.Response.StatusCode = 200;
                //HttpContext.Current.Response.ClearContent();
                //HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        public static MemoryStream DataTableToExcelXlsx(DataTable table, string sheetName, string spath)
        {
            MemoryStream result = new MemoryStream();
            ExcelPackage excelpack = new ExcelPackage();
            ExcelWorksheet worksheet = excelpack.Workbook.Worksheets.Add(sheetName);

            //worksheet.Cells["A:AZ"].AutoFitColumns();  

            var cellH = worksheet.Cells["A1:N1"];
            cellH.Style.Font.Size = 12;
            cellH.Style.Font.Name = "Calibri";
            cellH.Style.Font.Bold = true;

            int col = 1;
            int row = 1;
            foreach (DataColumn column in table.Columns)
            {
                worksheet.Cells[row, col].Value = column.ColumnName.ToString();
                col++;
            }
            col = 1;
            row = 2;
            foreach (DataRow rw in table.Rows)
            {
                foreach (DataColumn cl in table.Columns)
                {
                    if (rw[cl.ColumnName] != DBNull.Value)
                        worksheet.Cells[row, col].Value = rw[cl.ColumnName].ToString();
                    col++;
                }
                row++;
                col = 1;
            }


            if (spath != "")
            {
                //  Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                // FileStream file = new FileStream("d:\\file13.xlsx", FileMode.Create, FileAccess.ReadWrite);

                FileStream file = new FileStream(spath, FileMode.Create, FileAccess.Write);
                excelpack.SaveAs(file);

                excelpack.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                // System.Runtime.InteropServices.Marshal.ReleaseComObject();
                //  System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);

            }
            else
            {
                excelpack.SaveAs(result);
            }

            //System.Runtime.InteropServices.Marshal.ReleaseComObject(excelpack);
            return result;
        }
    }



    public class connectplusmultiResult
    {
        public string Item_Type { get; set; }

        public string State_Name { get; set; }

    }

}