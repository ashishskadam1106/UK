using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class Form_EmailAccountAddUpdate : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);
    int EmailAccountId_ToUpdate, Employee_Id;
    string UserName;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginUsername"] == null || Session["LoginAuthenticated"] == "No")
        {
            Response.Redirect(@"~\Index.aspx");
        }
        else
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            EmailAccountId_ToUpdate = Convert.ToInt32(Session["EmailAccountId_ToUpdate"].ToString());

            UserName = Session["LoginUsername"].ToString();

            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            if (!IsPostBack)
            {
                Fill_EmailAccountDetails();
            }
        }
    }

    #region Validation
    private int Validation()
    {
        int Go = 1;
        string ValidationMsg = "";

        if (Tb_EmailAccountName.Text.ToString() == "")
        {
            Go = 0;
            Tb_EmailAccountName.BorderColor = Color.Red;
            ValidationMsg += "Please Enter Email Account Name. /";
        }
        else
        {
            Tb_EmailAccountName.BorderColor = Color.LightGray;
        }

        if (Tb_EmailId.Text.ToString() == "")
        {
            Go = 0;
            Tb_EmailId.BorderColor = Color.Red;
            ValidationMsg += "Please Enter EmailId. /";
        }
        else
        {
            Tb_EmailId.BorderColor = Color.LightGray;
        }

        if (Tb_SMTPHostName.Text.ToString() == "")
        {
            Go = 0;
            Tb_SMTPHostName.BorderColor = Color.Red;
            ValidationMsg += "Please Enter Tb_SMTP Host Name. /";
        }
        else
        {
            Tb_SMTPHostName.BorderColor = Color.LightGray;
        }

        if (Tb_SMTPPortNumber.Text.ToString() == "")
        {
            Go = 0;
            Tb_SMTPPortNumber.BorderColor = Color.Red;
            ValidationMsg += "Please Enter SMTP Port Number. /";
        }
        else
        {
            Tb_SMTPPortNumber.BorderColor = Color.LightGray;
        }

        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :" + ValidationMsg.ToString() + "')", true);
        }
        return Go;

    }
    #endregion

 

    private void Fill_EmailAccountDetails()
    {
        try
        {
            Boolean IsEnablePass=true;
            con.ConnectionString = str;
            cmd = new SqlCommand();
            //cmd.CommandText = "select isnull(RoleName,'') RoleName, isnull(Remark,'') Remark from tbl_Roles where RoleId=" + Role_Id_ToUpdate;
           // cmd.CommandText = "select isnull(RoleName,'') RoleName, isnull(Remark,'') Remark from tbl_Roles where RoleId=" + Role_Id_ToUpdate + " and CompanyId=" + Company_Id;
            cmd.CommandText = "Select EmailAccountId,EmailAccountName,EmailId,EmailPassWord,SMTPHostName,SMTPPortNumber,IsEnableSSL from tbl_EmailAccounts Where EmailAccountId=" + EmailAccountId_ToUpdate;
            
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Tb_EmailAccountName.Text = dr["EmailAccountName"].ToString();
                Tb_EmailId.Text = dr["EmailId"].ToString();
                Tb_EmailPassWord.Text = dr["EmailPassWord"].ToString();
                Tb_SMTPHostName.Text = dr["SMTPHostName"].ToString();
                Tb_SMTPPortNumber.Text = dr["SMTPPortNumber"].ToString();
                IsEnablePass = Convert.ToBoolean(dr["IsEnableSSL"].ToString());
            }
            if (IsEnablePass == true)
            {
                Dd_IsEnableSSL.SelectedIndex = 0;
            }
            if (IsEnablePass == false)
            {
                Dd_IsEnableSSL.SelectedIndex = 1;
            }
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
        finally
        {
            dr.Close();
            con.Close();
        }
    }
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        int Go = 1, SMTPPortNumber = 0;
        string EmailAccountName = "", EmailId = "", EmailPassWord = "", SMTPHostName = "";
        Boolean IsEnableSSL;


        Go = Validation();

        if (Go == 1)
        {
            EmailAccountName = Tb_EmailAccountName.Text.ToString();
            EmailId = Tb_EmailId.Text.ToString();
            EmailPassWord = Tb_EmailPassWord.Text.ToString();
            SMTPHostName = Tb_SMTPHostName.Text.ToString();
            SMTPPortNumber = Convert.ToInt32(Tb_SMTPPortNumber.Text.ToString());
            if (Dd_IsEnableSSL.SelectedIndex == 0)
            {
                IsEnableSSL = true;
            }
            else
            {
                IsEnableSSL = false;
            }

            if (EmailAccountId_ToUpdate == 0)//Add
            {
                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandText = "USP_EmailAccount_Insert";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailAccountName", EmailAccountName);
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);
                    cmd.Parameters.AddWithValue("@EmailPassWord", EmailPassWord);
                    cmd.Parameters.AddWithValue("@SMTPHostName", SMTPHostName);
                    cmd.Parameters.AddWithValue("@SMTPPortNumber", SMTPPortNumber);
                    cmd.Parameters.AddWithValue("@IsEnableSSL", IsEnableSSL);
                    cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                    cmd.Parameters.AddWithValue("@CreatedDate", CurrentUtc_IND);


                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    con.Close();
                    cmd.Parameters.Clear();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Email Account Added Successfully');window.location='Form_EmailAccountMaster.aspx'", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Savealert();", true);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                }
                finally
                {
                    con.Close();
                }
            }
            else if (EmailAccountId_ToUpdate != 0)
            {
                //Update 
                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandText = "USP_EmailAccount_Update";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailAccountId", EmailAccountId_ToUpdate);
                    cmd.Parameters.AddWithValue("@EmailAccountName", EmailAccountName);
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);
                    cmd.Parameters.AddWithValue("@EmailPassWord", EmailPassWord);
                    cmd.Parameters.AddWithValue("@SMTPHostName", SMTPHostName);
                    cmd.Parameters.AddWithValue("@SMTPPortNumber", SMTPPortNumber);
                    cmd.Parameters.AddWithValue("@IsEnableSSL", IsEnableSSL);
                    cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", CurrentUtc_IND);

                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    con.Close();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Email Account updated Successfully');window.location='Form_EmailAccountMaster.aspx'", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Updatealert();", true);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
    protected void Btn_New_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_EmailAccountAddUpdate.aspx");
    }
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_EmailAccountMaster.aspx");
    }

    #region Encrypt , decrypt
    public string encrypt(string encryptString)
    {
        string EncryptionKey = "SOFTUKKARATE20192020ENCRYPTPRITAM5578";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }


    public string Decrypt(string cipherText)
    {
        try
        {
            string EncryptionKey = "SOFTUKKARATE20192020ENCRYPTPRITAM5578";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
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
        }
        catch
        {

        }
        return cipherText;
    }
    #endregion
}