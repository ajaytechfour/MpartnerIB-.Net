
#region[Directives]

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Net;
using LuminousMpartnerIB.EF;
#endregion[Directives]

namespace LuminousMpartnerIB.MpartnerIB_Api.Model
{
    public class SMSbyWebservice
    {
        #region[PrivateData]
        private static volatile SMSbyWebservice smsSend = null;
        private static object _lock = new object();
        #endregion[PrivateData]
        #region[Constructor]

        private SMSbyWebservice()
        {
        }

        #endregion[Constructor]
        #region[Poperties]

        /// <summary>
        /// Get Instace of the object
        /// </summary>
        public static SMSbyWebservice Instance
        {
            get
            {
                lock (_lock)
                {
                    if (smsSend == null)
                    {
                        lock (_lock)
                        {
                            smsSend = new SMSbyWebservice();
                        }
                    }
                }

                return smsSend;
            }
        }

        /// <summary>
        /// Gets or Sets MailTo
        /// </summary>
        public string MobileNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets MailSubject
        /// </summary>
        public string OTP
        {
            get;
            set;
        }
        public string LoyaltyPoint
        {
            get;
            set;
        }

        public string EmployeeId { get; set; }



        #endregion[Poperties]
        #region[Public Methods]
        /// <summary>
        /// Send Mail To the specified mail id
        /// </summary>
        public string SendSMS()
        {
            #region[Default Setting]
            //string token = ConfigurationManager.AppSettings["Token"].ToString();
            #endregion[Default Setting]
            string result = string.Empty;
            string smsMsg = "Your one time password is " + OTP + ". It will expire in 30 minutes";
            try
            {
                // ServiceSoapClient ser = new ServiceSoapClient();
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
               (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                //ServiceSoapClient ser = new ServiceSoapClient();
                com.luminousindia.lumapi.Service ser = new com.luminousindia.lumapi.Service();
                 result = ser.sendsms(MobileNumber, smsMsg, EmployeeId, "pass@1234");
            }
            catch (Exception ex)
            {
                LogException(ex, "SendSMS, " + ",smsMsg | " + smsMsg + ",result | " + result);
            }
            return result;
        }
        public string SendElectriciansms()
        {
            #region[Default Setting]
            //string token = ConfigurationManager.AppSettings["Token"].ToString();
            #endregion[Default Setting]
            string result = string.Empty;
            string smsMsg = "You have earned " + LoyaltyPoint + " points.";
            try
            {
                //  ServiceSoapClient ser = new ServiceSoapClient();
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
               (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                //ServiceSoapClient ser = new ServiceSoapClient();
                com.luminousindia.lumapi.Service ser = new com.luminousindia.lumapi.Service();
                result = ser.sendsms(MobileNumber, smsMsg, EmployeeId, "pass@1234");


            }
            catch (Exception ex)
            {
                LogException(ex, "SendSMS, " + ",smsMsg | " + smsMsg + ",result | " + result);
            }
            return result;
        }
        
        private void LogException(Exception ex, string createdBy)
        {
            try
            {
                if (ex != null)
                {
                    LuminousMpartnerIBEntities logg = new LuminousMpartnerIBEntities();

                    Logger log = new Logger { Message = ex.Message, StackTrace = ex.StackTrace, CreatedOn = DateTime.Now, CreatedBy = createdBy };
                    logg.Loggers.Attach(log);
                    logg.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion[Public Methods]
    }
}

