using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Luminous.OtpClient;
using System.Linq;

namespace Luminous.EF
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
                ServiceSoapClient ser = new ServiceSoapClient();
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
                    LuminousEntities db = new LuminousEntities();
                   // BikerDataClassesDataContext db = new BikerDataClassesDataContext();
                    MPartnerServiceLog log = new MPartnerServiceLog { ErrorDescription = ex.Message, CreatedOn = DateTime.Now, CreatedBy = createdBy };
                    db.MPartnerServiceLogs.Attach(log);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion[Public Methods]
    }
    
}