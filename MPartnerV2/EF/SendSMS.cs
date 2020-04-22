using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Luminous.EF;
namespace Luminous.EF
{
    public class SendSMS
    {
        string Connstring = ConfigurationManager.ConnectionStrings["LuminousEntities"].ConnectionString;
    
        static SendSMS()
        {

            db = new LuminousEntities();
           // db.DeferredLoadingEnabled = false;

        }
       
        public static  LuminousEntities db;
        OtpClient.ServiceSoapClient otpDb = new OtpClient.ServiceSoapClient();

        public inputmessage BikerLogInCreateOtp(string empid, string phoneno)
        {
            inputmessage msg = new inputmessage();
            try
            {
               var  mhrCreateOtpResult =  db.Sp_MHrCreateOtp(empid,phoneno);
                if (mhrCreateOtpResult != null)
                {
                    SMSbyWebservice sms = SMSbyWebservice.Instance;
                    var smsTemp = mhrCreateOtpResult.Where(m => m != null && !String.IsNullOrEmpty(m.Mob))
                    .Select(m => new
                    {
                        EmployeeId = m.ASMFSEcode,
                        MobileNumber = m.Mob,
                        OTP = m.otp,
                       
                        Status = m.Status,
                        Message = m.Message


                    }).FirstOrDefault();

                    if (smsTemp != null)
                    {
                        sms.EmployeeId = smsTemp.EmployeeId;
                        sms.MobileNumber = smsTemp.MobileNumber;
                        sms.OTP = smsTemp.OTP;
                        string returnValue = sms.SendSMS();
                        //string returnValue = otpDb.sendsms(sms.MobileNumber, BodyMessage(sms.Biker_Name, sms.OTP), sms.Biker_Name, "pass@1234");// 

                        msg.Code = smsTemp.Status;
                        msg.des = smsTemp.Message;

                        if(returnValue.Contains("<ERROR>"))
                        {
                           
                        }
                        else
                        {
                            NewDealerData dealer = new NewDealerData();
                            dealer.ASMEmpCode = sms.EmployeeId;
                            dealer.Mobileno = sms.MobileNumber;
                            dealer.OTP = sms.OTP;
                            dealer.CreatedOn = DateTime.Now;
                            dealer.CreatedBy = sms.EmployeeId;
                            db.NewDealerDatas.AddObject(dealer);
                            db.SaveChanges();
                        }


                        return msg;
                    }

                    //if (!String.IsNullOrEmpty(dt.Rows[0][1].ToString()))
                    //{
                    //    string body = BodyMessage(dt.Rows[0][1].ToString(), dt.Rows[0][4].ToString());
                    //    mailsql m2 = new mailsql(dt.Rows[0][2].ToString(), "not", "not", "Luminous_mobileApp One Time Password", body, "");
                    //    m2.sendMaildb();
                    //}
                }
                else
                {
                    msg.Code = "ERROR";
                    msg.des = "Invalid Employee";
                    return msg;
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BikerLogInCreateOtp, empid | " + empid + "");
            }
            return msg;
        }

        public inputmessage OTPAuthentication(string empid, string phoneno, string otp)
        {
            inputmessage msg = new inputmessage();
            try
            {
                var  mhrVarifyOtpNotificationResult =
                        db.MHrVarifyOtpNotification(empid,phoneno,otp);
                if (mhrVarifyOtpNotificationResult != null)
                {
                    inputmessage inputMessage = mhrVarifyOtpNotificationResult.Where(m => m != null)
                        .Select(m => new inputmessage { Code = m.Code, des = m.Message }).FirstOrDefault();
                    if (inputMessage != null)
                    {
                        msg.Code = inputMessage.Code;
                        msg.des = inputMessage.des;
                    }
                    else
                    {
                        msg.Code = inputMessage.Code;
                        msg.des = inputMessage.des;
                    }
                }
                else
                {
                    msg.Code = "ERROR";
                    msg.des = "Please try again";
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "OTPAuthentication, empid | " + empid + "");
            }
            return msg;
        }

        //public inputmessage sentSms()
        //{
        //    inputmessage msg = new inputmessage();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(db.Database.Connection.ConnectionString))
        //        {

        //            try
        //            {
        //                con.Open();
        //                SqlCommand cmd = new SqlCommand("select * from electricianmasterhistory where uniqueid='" + ElecricianID + "'and PointRedeemed='" + PointRedeemed + "' and TotalBalancePoint='" + TotalBalancePoint + "'", con);
        //                SqlDataAdapter da = new SqlDataAdapter(cmd);
        //                DataSet dataelectricianhistory = new DataSet();
        //                da.Fill(dataelectricianhistory);


        //                SMSbyWebservice sms = SMSbyWebservice.Instance;

        //                if (dataelectricianhistory.Tables[0].Rows.Count != 0)
        //                {
        //                    for (int i = 0; i < dataelectricianhistory.Tables[0].Rows.Count; i++)
        //                    {
        //                        int electricianid = Convert.ToInt32(dataelectricianhistory.Tables[0].Rows[i]["Uniqueid"]);
        //                        string mob = dataelectricianhistory.Tables[0].Rows[i]["Mobile"].ToString();



        //                        if (mob != null)
        //                        {
        //                            sms.EmployeeId = electricianid.ToString();
        //                            sms.MobileNumber = mob;
        //                            sms.PointRedeemed = dataelectricianhistory.Tables[0].Rows[i]["PointRedeemed"].ToString();
        //                            sms.TotalBalancePoint = dataelectricianhistory.Tables[0].Rows[i]["TotalBalancePoint"].ToString();
        //                            string returnValue = sms.SendElectriciansms();

        //                            /*SMS reaponse date*/

        //                            string docarr = "";
        //                            if (returnValue.Contains("<!DOCTYPE"))
        //                            {
        //                                docarr = returnValue.Remove(0, 83);
        //                            }
        //                            XmlDocument XDoc = new XmlDocument();
        //                            XDoc.LoadXml(docarr);
        //                            XmlNodeList elemList = XDoc.GetElementsByTagName("RESULT");
        //                            XmlElement root = XDoc.DocumentElement;
        //                            string reqid = root.Attributes["REQID"].Value;
        //                            string date = "";
        //                            string TID = "";
        //                            for (int j = 0; j < elemList.Count; j++)
        //                            {
        //                                //Console.WriteLine(elemList[i].InnerXml);
        //                                date = elemList[j]["MID"].Attributes["SUBMITDATE"].Value;
        //                                TID = elemList[j]["MID"].Attributes["TID"].Value;
        //                            }
        //                            string Redeemedpoint = "You have redeemed " + PointRedeemed + " points. Your balance point is " + TotalBalancePoint + "";

        //                            using (SqlConnection con1 = new SqlConnection(db.Database.Connection.ConnectionString))
        //                            {
        //                                using (SqlCommand command = new SqlCommand("Update electricianmasterhistory set Message='" + Redeemedpoint + "',Msg_Createdtime='" + System.DateTime.Now + "',Msg_Senttime='" + DateTime.Now + "',RequestId='" + reqid + "',SubmitDate='" + date + "',TID='" + TID + "' where Uniqueid='" + electricianid + "' and PointRedeemed='" + PointRedeemed + "' and TotalBalancePoint='" + TotalBalancePoint + "' ", con1))
        //                                {
        //                                    try
        //                                    {

        //                                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        //                                        dataAdapter.SelectCommand = command;
        //                                        DataTable dataSet = new DataTable();
        //                                        dataAdapter.Fill(dataSet);
        //                                        return msg;
        //                                    }
        //                                    catch (Exception exc)
        //                                    {
        //                                        inputmessage ip = new inputmessage();
        //                                        msg.Code = exc.ToString();
        //                                        msg.des = exc.ToString();
        //                                        LogException(exc, "sentSms, ElectricianId | " + electricianid + ", TotalRedeemedPoint | " + PointRedeemed + ",TotalBalancePoint | " + TotalBalancePoint + "");
        //                                    }
        //                                    finally
        //                                    {
        //                                        if (con.State != ConnectionState.Closed || con1.State != ConnectionState.Closed)
        //                                        {
        //                                            con.Close();
        //                                            con1.Close();
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }

        //                        else
        //                        {
        //                            msg.Code = "ERROR";
        //                            msg.des = "Invalid Employee";
        //                            return msg;
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception exc)
        //            {

        //                inputmessage ip = new inputmessage();
        //                msg.Code = exc.ToString();
        //                msg.des = exc.ToString();
        //                LogException(exc, "sentSms, ElectricianId | " + ElecricianID + ", TotalRedeemedPoint | " + PointRedeemed + ",TotalBalancePoint | " + TotalBalancePoint + "");
        //            }

        //        }

        //    }
        //    catch (Exception exc)
        //    {
        //        inputmessage ip = new inputmessage();
        //        msg.Code = exc.ToString();
        //        msg.des = exc.ToString();
        //        LogException(exc, "sentSms, ElectricianID | " + ElecricianID + ",TotalRedeemedPoint | " + PointRedeemed + ", TotalBalancePoint | " + TotalBalancePoint + "");
        //    }
        //    return msg;

        //}

        //public inputmessage sentSmsElectricianBonus(int ElecricianID, int BonusPoint)
        //{
        //    inputmessage msg = new inputmessage();
        //    try
        //    {
        //        var ElectricianMobile = db.ElectricianMasters.Where(c => c.UniqueId == ElecricianID).Select(c => c.Mobile).SingleOrDefault();
        //                SMSbyWebservice sms = SMSbyWebservice.Instance;

        //                if (ElectricianMobile != null)
        //                        {
        //                            sms.EmployeeId = ElecricianID.ToString();
        //                            sms.MobileNumber = ElectricianMobile;
        //                            sms.Bonus = BonusPoint.ToString();

        //                            string returnValue = sms.SendElectricianBonus();

        //                            /*SMS reaponse date*/

        //                            string docarr = "";
        //                            if (returnValue.Contains("<!DOCTYPE"))
        //                            {
        //                                docarr = returnValue.Remove(0, 83);
        //                            }
        //                            XmlDocument XDoc = new XmlDocument();
        //                            XDoc.LoadXml(docarr);
        //                            XmlNodeList elemList = XDoc.GetElementsByTagName("RESULT");
        //                            XmlElement root = XDoc.DocumentElement;
        //                            string reqid = root.Attributes["REQID"].Value;
        //                            string date = "";
        //                            string TID = "";
        //                            for (int j = 0; j < elemList.Count; j++)
        //                            {
        //                                //Console.WriteLine(elemList[i].InnerXml);
        //                                date = elemList[j]["MID"].Attributes["SUBMITDATE"].Value;
        //                                TID = elemList[j]["MID"].Attributes["TID"].Value;
        //                            }
        //                            string bonuspoint = "Congratulation ! You have earned" + BonusPoint + " bonus point";

        //                            var Maxleadid = db.LeadHistories.Max(c => c.Leadid);

        //                            db.Database.ExecuteSqlCommand("Update leadhistory set CreatedOn='"+DateTime.Now+"', Message='" + bonuspoint + "',Msg_Createdtime='" + System.DateTime.Now + "',Msg_Senttime='" + DateTime.Now + "',RequestId='" + reqid + "',SubmitDate='" + date + "',TID='" + TID + "' where leadid='" + Maxleadid + "'");
        //                            //using (SqlConnection con1 = new SqlConnection(db.Database.Connection.ConnectionString))
        //                            //{
        //                            //    using (SqlCommand command = new SqlCommand("Update leadhistory set Message='" + bonuspoint + "',Msg_Createdtime='" + System.DateTime.Now + "',Msg_Senttime='" + DateTime.Now + "',RequestId='" + reqid + "',SubmitDate='" + date + "',TID='" + TID + "' where leadid='" + electricianid + "' and PointRedeemed='" + PointRedeemed + "' and TotalBalancePoint='" + TotalBalancePoint + "' ", con1))
        //                            //    {
        //                            //        try
        //                            //        {

        //                            //            SqlDataAdapter dataAdapter = new SqlDataAdapter();
        //                            //            dataAdapter.SelectCommand = command;
        //                            //            DataTable dataSet = new DataTable();
        //                            //            dataAdapter.Fill(dataSet);
        //                            //            return msg;
        //                            //        }
        //                            //        catch (Exception exc)
        //                            //        {
        //                            //            inputmessage ip = new inputmessage();
        //                            //            msg.Code = exc.ToString();
        //                            //            msg.des = exc.ToString();
        //                            //            LogException(exc, "sentSms, ElectricianId | " + electricianid + ", TotalRedeemedPoint | " + PointRedeemed + ",TotalBalancePoint | " + TotalBalancePoint + "");
        //                            //        }
        //                            //        finally
        //                            //        {
        //                            //            if (con.State != ConnectionState.Closed || con1.State != ConnectionState.Closed)
        //                            //            {
        //                            //                con.Close();
        //                            //                con1.Close();
        //                            //            }
        //                            //        }
        //                            //    }
        //                            //}
        //                        }

        //                        else
        //                        {
        //                            msg.Code = "ERROR";
        //                            msg.des = "Invalid Employee";
        //                            return msg;
        //                        }
                            
                        
                    

                

        //    }
        //    catch (Exception exc)
        //    {
        //        inputmessage ip = new inputmessage();
        //        msg.Code = exc.ToString();
        //        msg.des = exc.ToString();
        //        LogException(exc, "sentSms, ElectricianID | " + ElecricianID + ",Bonuspoint | " + BonusPoint + "");
        //    }
        //    return msg;

        //}
        private void LogException(Exception ex, string createdBy)
        {
            try
            {
                string message = "";
                string stackTrace = "";
                if (ex != null)
                {
                    message = ex.Message;
                    stackTrace = ex.StackTrace;
                }
                MPartnerServiceLog log = new MPartnerServiceLog { ErrorDescription = message, CreatedOn = DateTime.Now, CreatedBy = createdBy };
                db.MPartnerServiceLogs.AddObject(log);
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
        }
    }
}