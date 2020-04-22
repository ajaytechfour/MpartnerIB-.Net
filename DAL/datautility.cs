using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace TVS
{
    public class datautility
    {
        SqlConnection mCon;
        SqlCommand mDataCom;


        private void openConnection()
        {
            //string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
           // mCon = new SqlConnection(ConfigurationManager.ConnectionStrings["techfourPMSConnectionString"].ConnectionString);
            //mCon = new SqlConnection( ConfigurationManager.AppSettings["ConnectionString"]);
             mCon = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            mCon.Open();
            mDataCom = new SqlCommand();
            mDataCom.Connection = mCon;
        }
        private void CloseConnection()
        {
            mCon.Close();
        }

        private void DisposeCon()
        {
            if (mCon != null)
            {
                mCon.Dispose();
                mCon = null;
            }
        }
        public int ExeuteSql(string strSql)
        {
            openConnection();
            mDataCom.CommandType = CommandType.Text;
            mDataCom.CommandText = strSql;
            int intRows;
            intRows = mDataCom.ExecuteNonQuery();
            CloseConnection();
            DisposeCon();
            return intRows;
        }
        public int ExeuteSqlandgetidentity(string strSql)
        {
            openConnection();
            //mDataCom.CommandType=CommandType.Text;
            mDataCom.CommandText = strSql;
            int intRows;
            //intRows=mDataCom.ExecuteNonQuery();
            intRows = Convert.ToInt32(mDataCom.ExecuteScalar());
            CloseConnection();
            DisposeCon();
            return intRows;
        }
        public int ExeuteStoredproc(string strSql)
        {
            openConnection();
            mDataCom.CommandType = CommandType.StoredProcedure;
            mDataCom.CommandText = strSql;
            int intRows;
            intRows = mDataCom.ExecuteNonQuery();
            CloseConnection();
            DisposeCon();
            return intRows;
        }

        public bool isExist(string strSql)
        {
            openConnection();
            mDataCom.CommandType = CommandType.Text;
            mDataCom.CommandText = strSql;
            int intRows;
            intRows = (int)mDataCom.ExecuteScalar();
            CloseConnection();
            DisposeCon();
            if (intRows == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataTable getDataTable(string strSql)
        {
            openConnection();
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(strSql, mCon);
            da.Fill(dt);
            CloseConnection();
            DisposeCon();
            return dt;
        }
        public SqlDataReader getdatareader(string strSql)
        {
            openConnection();
            mDataCom.CommandType = CommandType.Text;
            mDataCom.CommandText = strSql;
            SqlDataReader dr = null;
            dr = mDataCom.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;

        }

        public int ExecuteDMLProcedure(string procName, SqlParameter[] param)
        {
            openConnection();
            mDataCom.CommandType = CommandType.StoredProcedure;
            mDataCom.CommandText = procName;
            foreach (SqlParameter sqlp in param)
            {
                mDataCom.Parameters.Add(sqlp);
            }
            int rows = mDataCom.ExecuteNonQuery();
            return rows;
        }

        

        public DataTable ExecuteDTProcedure(string procName, SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            openConnection();
            mDataCom.CommandType = CommandType.StoredProcedure;
            mDataCom.CommandText = procName;
            foreach (SqlParameter sqlp in param)
            {
                mDataCom.Parameters.Add(sqlp);
            }
            SqlDataAdapter sDa = new SqlDataAdapter();
            sDa.SelectCommand = mDataCom;
            sDa.Fill(dt);
            CloseConnection();
            DisposeCon();
            return dt;
        }

        public DataTable ExecuteDTProcedure(string procName)
        {
            DataTable dt = new DataTable();
            openConnection();
            mDataCom.CommandType = CommandType.StoredProcedure;
            mDataCom.CommandText = procName;
            // mDataCom.Parameters.Add(param);
            /*foreach (SqlParameter sqlp in param)
            {
                mDataCom.Parameters.Add(sqlp);
            }*/
            SqlDataAdapter sDa = new SqlDataAdapter();
            sDa.SelectCommand = mDataCom;
            sDa.Fill(dt);
            return dt;
        }



        //////////// dataset
        public DataSet ExecuteDSProcedure(string procName, SqlParameter[] param)
        {

            DataSet ds = new DataSet();
            openConnection();
            try
            {
                mDataCom.CommandType = CommandType.StoredProcedure;
                mDataCom.CommandText = procName;
                foreach (SqlParameter sqlp in param)
                {
                    mDataCom.Parameters.Add(sqlp);
                }
                SqlDataAdapter sDa = new SqlDataAdapter();
                sDa.SelectCommand = mDataCom;
                sDa.Fill(ds);
            }
            finally {
                CloseConnection();
                DisposeCon();
            }

            return ds;
        }

        public SqlCommand getuser(string Emailuser)
        {

            mCon = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]);
            mCon.Open();
            mDataCom = new SqlCommand();
            mDataCom.Connection = mCon;
            string query = "select a.user_ids,a.password,a.Profile_ID,b.Profile_ID,b.First_Name,b.Candidate_Surname  from  user_login a, Register_Profile b  where  a.user_ids=@user_id   and a.Profile_ID=b.Profile_ID";

            SqlCommand cmd = new SqlCommand(query, mCon);
            cmd.Parameters.Add("@user_id", SqlDbType.NVarChar);
            cmd.Parameters["@user_id"].Value = Emailuser; 
            return cmd;

        }
        public DataSet ExecuteDSProcedure(string procName)
        {
            DataSet ds = new DataSet();
            openConnection();
            mDataCom.CommandType = CommandType.StoredProcedure;
            mDataCom.CommandText = procName;
            // mDataCom.Parameters.Add(param);
            /*foreach (SqlParameter sqlp in param)
            {
                mDataCom.Parameters.Add(sqlp);
            }*/
            SqlDataAdapter sDa = new SqlDataAdapter();
            sDa.SelectCommand = mDataCom;
            sDa.Fill(ds);
            return ds;
        }

        public void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)

                    ((TextBox)ctrl).Enabled = false;
                ClearInputs(ctrl.Controls);
            }
        }

        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
           
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

 
        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}
