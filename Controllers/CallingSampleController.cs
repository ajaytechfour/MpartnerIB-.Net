using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luminous.EF;
using System.Data;

namespace Luminous.Controllers
{
    public class CallingSampleController : Controller
    {
        //
        // GET: /CallingSample/

        private LuminousEntities db = new LuminousEntities();
        private DataTable dt = new DataTable();

        private string PageUrl = "/CallingSample/Index";
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

        public JsonResult getSerialNum()
        {

            var getserialno = db.Callingsamples.Where(c => c.Flag != 0).Select(c => new { c.id, c.Product_Serial_Number }).OrderBy(x => Guid.NewGuid()).ToList();
            return Json(getserialno, JsonRequestBehavior.AllowGet);


        }

        public JsonResult Viewdata(string serialno)
        {

            var getSerNoData = db.Wc_Warranty_Collection_Data.Where(c => c.Product_Serial_Number == serialno).ToList();
            return Json(getSerNoData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(string serialno, string prdtype, string custname, string custphone, string custaddress, string saledate)
        {
            try
            {
                //int checkCallingSampleRejCode = 0;
                Wc_Warranty_Collection_Data UpdateSampledata = db.Wc_Warranty_Collection_Data.Single(a => a.Product_Serial_Number == serialno);

                //Insert Wc_Warranty_Collection_Data_History  Table//

                //Change logic for updating Calling sample data by Ravi on 02-08-2018 Task Id - 4068
                Wc_Warranty_Collection_Data_History Wc_history = new Wc_Warranty_Collection_Data_History();

                Wc_history.Product_Serial_Number = UpdateSampledata.Product_Serial_Number;
                Wc_history.Product_Type = UpdateSampledata.Product_Type;
                Wc_history.Product_Model = UpdateSampledata.Product_Model;
                Wc_history.Dis_Name = UpdateSampledata.Dis_Name;
                Wc_history.Dis_Sap_Code = UpdateSampledata.Dis_Sap_Code;

                Wc_history.Dis_City = UpdateSampledata.Dis_City;
                Wc_history.Dis_State = UpdateSampledata.Dis_State;
                Wc_history.Dis_Name_Warrnty_Card = UpdateSampledata.Dis_Name_Warrnty_Card;
                Wc_history.Dis_Sap_Code_Warrnty_Card = UpdateSampledata.Dis_Sap_Code_Warrnty_Card;
                Wc_history.Dis_Address_Warrnty_Card = UpdateSampledata.Dis_Address_Warrnty_Card;
                Wc_history.Dis_State_Warrnty_Card = UpdateSampledata.Dis_State_Warrnty_Card;
                Wc_history.Dis_City_Warrnty_Card = UpdateSampledata.Dis_City_Warrnty_Card;
                Wc_history.Dis_Address_ContactNo = UpdateSampledata.Dis_Address_ContactNo;
                Wc_history.Dlr_Name = UpdateSampledata.Dlr_Name;
                Wc_history.Dlr_Sap_Code = UpdateSampledata.Dlr_Sap_Code;
                Wc_history.Dlr_Address = UpdateSampledata.Dlr_Address;
                Wc_history.Dlr_City = UpdateSampledata.Dlr_City;
                Wc_history.Dlr_State = UpdateSampledata.Dlr_State;
                Wc_history.Dlr_Phone = UpdateSampledata.Dlr_Phone;
                Wc_history.Product_Purchase_Date = UpdateSampledata.Product_Purchase_Date; ;
                Wc_history.Customer_Name = UpdateSampledata.Customer_Name;
                Wc_history.Customer_Phone = UpdateSampledata.Customer_Phone;

                Wc_history.Customer_Address = UpdateSampledata.Customer_Address;
                Wc_history.Customer_State = UpdateSampledata.Customer_State;
                Wc_history.Customer_City = UpdateSampledata.Customer_City;
                Wc_history.Customer_email = UpdateSampledata.Customer_email;
                Wc_history.ModBy = Session["Id"].ToString();
                Wc_history.ModDate = DateTime.Now;
                Wc_history.E_Status = UpdateSampledata.E_Status;
                Wc_history.Customer_LandLineNo = UpdateSampledata.Customer_LandLineNo;
                Wc_history.Form_Number = UpdateSampledata.Form_Number;
                Wc_history.Scheme_Name = UpdateSampledata.Scheme_Name;
                Wc_history.Pin_Code = UpdateSampledata.Pin_Code;
                Wc_history.Remark = UpdateSampledata.Remark;
                Wc_history.Docket_No = UpdateSampledata.Docket_No;
                Wc_history.Sender_Address = UpdateSampledata.Sender_Address;
                Wc_history.Agency_Remark = UpdateSampledata.Agency_Remark;
                Wc_history.Entry_Status = UpdateSampledata.Entry_Status;
                Wc_history.Verification_Status = UpdateSampledata.Verification_Status;
                Wc_history.Customer_Name_V = UpdateSampledata.Customer_Name_V;
                Wc_history.Customer_Address_V = UpdateSampledata.Customer_Address_V;
                Wc_history.Customer_Phone_V = UpdateSampledata.Customer_Phone_V;
                Wc_history.Product_Type_V = UpdateSampledata.Product_Type_V;
                Wc_history.Product_Purchase_Date_V = UpdateSampledata.Product_Purchase_Date_V;
                db.Wc_Warranty_Collection_Data_History.AddObject(Wc_history);

                //Change logic for updating Calling sample data by Ravi on 02-08-2018 Task Id - 4068
                int count = 0;
                string verifyremark = "";
                if (UpdateSampledata.Product_Type != prdtype)
                {
                    UpdateSampledata.Product_Type_V = prdtype;
                    UpdateSampledata.Verify_status = "Rejected";
                    verifyremark += "Wrong Product Type Found in Call verification,";

                }
                else
                {
                    count++;
                    UpdateSampledata.Product_Type_V = "";
                }
                if (UpdateSampledata.Customer_Name != custname)
                {
                    UpdateSampledata.Customer_Name_V = custname;
                    UpdateSampledata.Verify_status = "Rejected";
                    verifyremark += "Wrong Customer Name Found in Call verification,";

                }
                else
                {
                    count++;
                    UpdateSampledata.Customer_Name_V = "";
                }
                if (UpdateSampledata.Customer_Phone != custphone)
                {
                    UpdateSampledata.Customer_Phone_V = custphone;
                    UpdateSampledata.Verify_status = "Rejected";
                    verifyremark += "Wrong Customer Number Found in Call verification,";

                }
                else
                {
                    count++;
                    UpdateSampledata.Customer_Phone_V = "";
                }
                if (UpdateSampledata.Customer_Address != custaddress)
                {
                    UpdateSampledata.Customer_Address_V = custaddress;
                    UpdateSampledata.Verify_status = "Rejected";
                    verifyremark += "Wrong Customer Address Found in Call verification,";

                }
                else
                {
                    count++;
                    UpdateSampledata.Customer_Address_V = "";
                }
                if (UpdateSampledata.Product_Purchase_Date != Convert.ToDateTime(saledate))
                {
                    UpdateSampledata.Product_Purchase_Date_V = Convert.ToDateTime(saledate);
                    UpdateSampledata.Verify_status = "Rejected";
                    verifyremark += "Wrong Product Date Of Sale Found in Call verification,";

                }
                else
                {
                    count++;
                    UpdateSampledata.Product_Purchase_Date_V = null;
                }

                UpdateSampledata.Verify_Remark = verifyremark.TrimEnd(',');
                UpdateSampledata.Verify_By = Session["Id"].ToString();
                UpdateSampledata.Verify_date = DateTime.Now;

                // UpdateSampledata.Product_Serial_Number = serialno;
                if (count == 5)
                {
                    UpdateSampledata.Verify_status = "Accepted";
                }

                int row = db.WC_Connect_Update_customer_detail(serialno, UpdateSampledata.Product_Type_V, UpdateSampledata.Customer_Name_V, UpdateSampledata.Customer_Phone_V, UpdateSampledata.Customer_Address_V, UpdateSampledata.Product_Purchase_Date_V, UpdateSampledata.Verify_status, UpdateSampledata.Verify_Remark, UpdateSampledata.Verify_By);


                //Added status variable for checking WC_Connect_Update_customer_detail status by Ravi on 03-08-2018 Task id - 4068.
                if (row > 0)
                {
                    db.ExecuteStoreCommand("Update callingsample set Flag=0 where Product_Serial_Number='" + serialno + "'");


                    string status = "Success";

                    WC_Connect_Update_customer_detail_log(status);

                    return Json("Record Updated Successfully", JsonRequestBehavior.AllowGet);

                }
                else
                {
                    string status = "failure";
                    WC_Connect_Update_customer_detail_log(status);

                    return Json("Record Not Updated", JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception exc)
            {
                //Added status variable for checking WC_Connect_Update_customer_detail exception by Ravi on 03-08-2018 Task id - 4068.
                WC_Connect_Update_customer_detail_log _log = new WC_Connect_Update_customer_detail_log();
                _log.Proc_CallDate = DateTime.Now;
                _log.Status = 0;
                _log.ErrorDescription = exc.InnerException.ToString();
                _log.LogBy = Session["Id"].ToString();

                db.WC_Connect_Update_customer_detail_log.AddObject(_log);
                db.SaveChanges();
                return Json("Some exception has occurred", JsonRequestBehavior.AllowGet);
            }
        }

        //Added code for maintaining log for calling WC_Connect_Update_customer_detail procedure by Ravi on 03-08-2018 Tak id - 4068.

        public void WC_Connect_Update_customer_detail_log(string status)
        {
            if (status == "Success")
            {
                WC_Connect_Update_customer_detail_log _log = new WC_Connect_Update_customer_detail_log();
                _log.Proc_CallDate = DateTime.Now;
                _log.Status = 1;
                _log.ErrorDescription = "";
                _log.LogBy = Session["Id"].ToString();
                db.WC_Connect_Update_customer_detail_log.AddObject(_log);
                db.SaveChanges();
            }
            if (status == "failure")
            {
                WC_Connect_Update_customer_detail_log _log = new WC_Connect_Update_customer_detail_log();
                _log.Proc_CallDate = DateTime.Now;
                _log.Status = 0;
                _log.ErrorDescription = "Failure";
                _log.LogBy = Session["Id"].ToString();
                db.WC_Connect_Update_customer_detail_log.AddObject(_log);
                db.SaveChanges();
            }


        }
    }
}
