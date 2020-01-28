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
using CS_Encryption;

public partial class Form_AlertEvent_AddUpdate : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
    DataTable dt;

    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);
    int AlertId_ToUpdate, Employee_Id;
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

            if (Request.QueryString.Count > 0)
            {
                AlertId_ToUpdate = Convert.ToInt32(Decrypt(Request.QueryString[0].ToString()));
            }
            

            UserName = Session["LoginUsername"].ToString();

            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            if (!IsPostBack)
            {
                if (AlertId_ToUpdate > 0)
                {
                    Fill_AlertEventDetails();
                    Fill_AlertEventTag();
                }
                else
                {
                    FillEmailID(0);
                }
            }
        }
    }

    #region Validation
    private int Validation()
    {
        {
            int Go = 1;
            string ValidationMsg = "";

            if (Tb_EventTitle.Text == "")
            {
                Go = 0;
                Tb_EventTitle.BorderColor = Color.Red;
                ValidationMsg += "Please Enter Event Title. /";
            }
            else
            {
                Tb_EventTitle.BorderColor = Color.LightGray;
            }

            if (Dd_IsSMSApplicable.SelectedValue == "Yes")
            {
                if (Tb_SMSMessage.Text == "")
                {
                    Go = 0;
                    Tb_SMSMessage.BorderColor = Color.Red;
                    ValidationMsg += "Please Enter SMS Message. /";
                }
                else
                {
                    Tb_SMSMessage.BorderColor = Color.LightGray;
                }
            }
            else
            {
                Tb_SMSMessage.BorderColor = Color.LightGray;
            }

            if (Dd_Is_Email_Applicable.SelectedValue == "Yes")
            {
                if (CKE_MailMessage.Text == "")
                {
                    Go = 0;
                    CKE_MailMessage.BorderColor = Color.Red;
                    ValidationMsg += "Please Enter Email Message. /";
                }
                else
                {
                    CKE_MailMessage.BorderColor = Color.LightGray;
                }
            }
            else
            {
                CKE_MailMessage.BorderColor = Color.LightGray;
            }

            if (tb_MailSubject.Text == "")
            {
                Go = 0;
                tb_MailSubject.BorderColor = Color.Red;
                ValidationMsg += "Please Enter Mail Subject. /";
            }
            else
            {
                tb_MailSubject.BorderColor = Color.LightGray;
            }

            if (Tb_AttachmentFileName.Text == "")
            {
                Go = 0;
                Tb_AttachmentFileName.BorderColor = Color.Red;
                ValidationMsg += "Please Enter Attachment Name. /";
            }
            else
            {
                Tb_AttachmentFileName.BorderColor = Color.LightGray;
            }

            if (tb_SignatureFileName.Text == "")
            {
                Go = 0;
                tb_SignatureFileName.BorderColor = Color.Red;
                ValidationMsg += "Please Enter Attachment Name. /";
            }
            else
            {
                tb_SignatureFileName.BorderColor = Color.LightGray;
            }


            if (Go == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :" + ValidationMsg.ToString() + "')", true);
            }
            return Go;

        }
    }
    #endregion

    private void Fill_AlertEventTag()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetAlertEventTag";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@AlertEventId", AlertId_ToUpdate);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Tags");
 
            Gv_AlertEventTag.DataSource=ds.Tables["Tags"];
            Gv_AlertEventTag.DataBind();
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
    private void Fill_AlertEventDetails()
    {
        try
        {
            FillEmailID(AlertId_ToUpdate);
            string Emailid = "";
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Select A.EventTitle,A.SMSMessage,A.MailSubject,A.MailMessage,A.AttachmentName,A.AttachmentPath,A.IsSMSApplicable,A.IsMailApplicable,A.IsSignatureImage,A.SignatureAttachmentName,A.SignatureAttachmentPath,A.EventTypeId,A.Isdeleted,E.EmailAccountName,E.EmailId,E.IsDeleted From tbl_AlertEvent  A inner join tbl_EmailAccounts E on A.EmailAccountId=E.EmailAccountId Where  A.Isdeleted=0 and A.AlertEventId=" + AlertId_ToUpdate;
            
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Tb_EventTitle.Text = dr["EventTitle"].ToString();
                Emailid = dr["EmailId"].ToString();               
            //    dd_EmailId.Text = Emailid;
                Dd_IsSMSApplicable.Text =Convert.ToInt32(dr["IsSMSApplicable"]).ToString();
                Dd_Is_Email_Applicable.Text = Convert.ToInt32(dr["IsMailApplicable"]).ToString();
                Tb_SMSMessage.Text = dr["SMSMessage"].ToString();
                tb_MailSubject.Text = dr["MailSubject"].ToString();
                CKE_MailMessage.Text = dr["MailMessage"].ToString();
                //GridView Bind
                Tb_AttachmentFileName.Text = dr["AttachmentName"].ToString();
                tb_SignatureFileName.Text = dr["SignatureAttachmentName"].ToString();
            //    Fu_AttachmentPath.FileName = dr["SMTPPortNumber"].ToString();
          //      tb_MailSubject.Text = dr["SMTPPortNumber"].ToString();
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

    private void FillEmailID(int AlertId_ToUpdate)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            if (AlertId_ToUpdate > 0)
            {
                cmd.CommandText = "Select E.EmailAccountId,E.EmailAccountName,E.EmailId,E.IsDeleted From tbl_AlertEvent A inner join tbl_EmailAccounts E on A.EmailAccountId=E.EmailAccountId Where A.Isdeleted=0 and A.AlertEventId=" + AlertId_ToUpdate;
            }
            else
            {
            //    cmd.CommandText = "Select ISNULL(EmailAccountId,'') as 'Email Account Id',ISNULL(EmailAccountName,'') as 'Email Account Name',ISNULL(EmailId,'') as 'Email id',ISNULL(IsDeleted,'') as 'Is Deleted' from tbl_EmailAccounts";
              //  cmd.CommandText = "Select EmailId,EmailAccountId from tbl_EmailAccounts";
                cmd.CommandText = "Select EmailId,EmailAccountId from tbl_EmailAccounts";
                      
            }          
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            dd_EmailId.DataSource = dt;
            dd_EmailId.DataBind();
            dd_EmailId.DataTextField = "EmailId";
            dd_EmailId.DataValueField = "EmailAccountId";
            dd_EmailId.DataBind();
            //   ds = new DataSet();
         //   da.Fill(ds, "EmailAccounts");
         //   dd_EmailId.DataSource = ds.Tables["EmailAccounts"];
         //   dd_EmailId.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occure:" + ex.Message.ToString() + "')", true);
        }
    }

    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        DateTime ModifiedDate = Convert.ToDateTime("01-01-1900");
        DateTime DeletedDate = Convert.ToDateTime("01-01-1900");
        DateTime ApprovedDate = Convert.ToDateTime("01-01-1900");

        int Go = 1;
        string EventTitle = "", SMSMessage = "", MailSubject = "", MailMessageContent = "", GetAttachmentName = "", GetSignatureName = "", SignatureAttachmentPath = "", EmailId = "";
        int IsSMSApplicable = 0, IsEmailApplicable = 0, IsSignatureImage=0;
        string File_Name,SignatureFileName,GetExtension, Attachment_Path = "";
        Go = Validation();

        if (Go == 1)
        {
            EventTitle = Tb_EventTitle.Text.ToString();
            EmailId = dd_EmailId.SelectedItem.ToString();
            IsSMSApplicable = Convert.ToInt32(Dd_IsSMSApplicable.SelectedValue.ToString());
            IsEmailApplicable = Convert.ToInt32(Dd_Is_Email_Applicable.SelectedValue.ToString());
            SMSMessage = Tb_SMSMessage.Text.ToString();
            MailSubject = tb_MailSubject.Text.ToString();
            MailMessageContent = CKE_MailMessage.Text.ToString();
            GetAttachmentName = Tb_AttachmentFileName.Text.ToString();
            GetSignatureName = tb_SignatureFileName.Text.ToString();
            string[] validFileTypes = { ".bmp", ".gif", ".png", ".jpg", ".jpeg" };

            if (AlertId_ToUpdate == 0)//Add
            {
                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                   // string EmailIDData = dd_EmailId.Text.ToString();
                    cmd.CommandText = "USP_AlertEvent_Insert";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EventTitle", EventTitle);
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);
                    cmd.Parameters.AddWithValue("@IsSMSApplicable", IsSMSApplicable);
                    cmd.Parameters.AddWithValue("@IsMailApplicable", IsEmailApplicable);
                    cmd.Parameters.AddWithValue("@SMSMessage", SMSMessage);
                    cmd.Parameters.AddWithValue("@MailSubject", MailSubject);
                    cmd.Parameters.AddWithValue("@MailMessage", MailMessageContent);
                    cmd.Parameters.AddWithValue("@AttachmentName", GetAttachmentName);
                    cmd.Parameters.AddWithValue("@SignatureAttachmentName", GetSignatureName);
                    cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ModifiedBy", "");
                    cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);                    
                    cmd.Parameters.AddWithValue("@ApprovedBy", "");
                    cmd.Parameters.AddWithValue("@ApprovedDate", ApprovedDate);
                    cmd.Parameters.AddWithValue("@IsDeleted", 0);
                    cmd.Parameters.AddWithValue("@DeletedBy", "");
                    cmd.Parameters.AddWithValue("@DeletedDate", DeletedDate);


                    SqlParameter OP_AlertEventId = new SqlParameter();
                    OP_AlertEventId.Direction = ParameterDirection.Output;
                    OP_AlertEventId.ParameterName = "@AlertEventId";
                    OP_AlertEventId.DbType = DbType.Int32;
                    cmd.Parameters.Add(OP_AlertEventId);

                    cmd.ExecuteNonQuery();
                    //Attachment Upload
                    int AlertEventId = Convert.ToInt32(OP_AlertEventId.Value.ToString());
                 
                    File_Name = Path.GetFileName(Fu_Attachment.PostedFile.FileName);
                    SignatureFileName = Path.GetFileName(Fu_SignatureImage.PostedFile.FileName);
                    GetExtension = Path.GetExtension(SignatureFileName).ToLower();

                    if (File_Name != "")
                    {                        
                                Attachment_Path = "Upload/AlertEvent/" + AlertEventId.ToString() + "_" + File_Name;
                                Fu_Attachment.SaveAs(Server.MapPath("Upload/AlertEvent/" + AlertEventId.ToString() + "_" + GetAttachmentName));

                                cmd.Parameters.Clear();
                                cmd.CommandText = "USP_AlertEventAttachmentUpdate";
                                //cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                //con.Open();
                                cmd.Parameters.AddWithValue("@AlertEventId", AlertEventId);
                                cmd.Parameters.AddWithValue("@AttachmentName", GetAttachmentName);
                                cmd.Parameters.AddWithValue("@Attachment_Path", Attachment_Path);
                                cmd.ExecuteNonQuery();                                                                                                  
                    }
                    else
                    {
                        Attachment_Path = "";
                    }

                    if (SignatureFileName != "")
                    {
                        int flag = 0;
                        for (int i = 0; i < validFileTypes.Length; i++)
                        {                        
                            if (GetExtension == validFileTypes[i])
                            {
                                flag = 1;
                                SignatureAttachmentPath = "Upload/Signature/" + AlertEventId.ToString() + "_" + SignatureFileName;
                                Fu_SignatureImage.SaveAs(Server.MapPath("Upload/Signature/" + AlertEventId.ToString() + "_" + GetSignatureName));
                                cmd.Parameters.Clear();
                                cmd.CommandText = "USP_AlertEventSignatureUpdate";
                                //cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                //con.Open();
                                cmd.Parameters.AddWithValue("@AlertEventId", AlertEventId);
                                cmd.Parameters.AddWithValue("@IsSignatureImage", IsSignatureImage);
                                cmd.Parameters.AddWithValue("@SignatureAttachmentName", GetSignatureName);
                                cmd.Parameters.AddWithValue("@SignatureAttachmentPath", SignatureAttachmentPath);
                                cmd.ExecuteNonQuery();
                            }                           
                        }
                        if (flag==0)
                        {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid Image Extension.Please upload image with .bmp,.gif,.png,.jpg,.jpeg');window.location='Form_AlertEvent_AddUpdate.aspx'", true);
                        }
                    }
                    else
                    {
                        SignatureAttachmentPath = "";
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Alert Event Added Successfully');window.location='Form_EmailAccountMaster.aspx'", true);
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
                    tran.Commit();
                    con.Close();
                }
            }

            else if (AlertId_ToUpdate != 0)
            {
                //Update 
                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;

                try
                {
                    cmd.CommandText = "USP_AlertEvent_Update";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AlertEventId", AlertId_ToUpdate);
;                    cmd.Parameters.AddWithValue("@EventTitle", EventTitle);
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);
                    cmd.Parameters.AddWithValue("@IsSMSApplicable", IsSMSApplicable);
                    cmd.Parameters.AddWithValue("@IsMailApplicable", IsEmailApplicable);
                    cmd.Parameters.AddWithValue("@SMSMessage", SMSMessage);
                    cmd.Parameters.AddWithValue("@MailSubject", MailSubject);
                    cmd.Parameters.AddWithValue("@MailMessage", MailMessageContent);
                    cmd.Parameters.AddWithValue("@AttachmentName", GetAttachmentName);
                    cmd.Parameters.AddWithValue("@SignatureAttachmentName", GetSignatureName);                 
                    cmd.Parameters.AddWithValue("@ModifiedBy",UserName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                    cmd.ExecuteNonQuery();

                    //Attachment Upload
                    int AlertEventId = AlertId_ToUpdate;

                    File_Name = Path.GetFileName(Fu_Attachment.PostedFile.FileName);
                    SignatureFileName = Path.GetFileName(Fu_SignatureImage.PostedFile.FileName);
                    GetExtension = Path.GetExtension(SignatureFileName).ToLower();

                    if (File_Name != "")
                    {
                        Attachment_Path = "Upload/AlertEvent/" + AlertEventId.ToString() + "_" + File_Name;
                        Fu_Attachment.SaveAs(Server.MapPath("Upload/AlertEvent/" + AlertEventId.ToString() + "_" + GetAttachmentName));

                        cmd.Parameters.Clear();
                        cmd.CommandText = "USP_AlertEventAttachmentUpdate";
                        //cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        //con.Open();
                        cmd.Parameters.AddWithValue("@AlertEventId", AlertEventId);
                        cmd.Parameters.AddWithValue("@AttachmentName", GetAttachmentName);
                        cmd.Parameters.AddWithValue("@Attachment_Path", Attachment_Path);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        Attachment_Path = "";
                    }

                    if (SignatureFileName != "")
                    {
                        int flag = 0;
                        for (int i = 0; i < validFileTypes.Length; i++)
                        {
                            if (GetExtension == validFileTypes[i])
                            {
                                flag = 1;
                                SignatureAttachmentPath = "Upload/Signature/" + AlertEventId.ToString() + "_" + SignatureFileName;
                                Fu_SignatureImage.SaveAs(Server.MapPath("Upload/Signature/" + AlertEventId.ToString() + "_" + GetSignatureName));
                                cmd.Parameters.Clear();
                                cmd.CommandText = "USP_AlertEventSignatureUpdate";
                                //cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                //con.Open();
                                cmd.Parameters.AddWithValue("@AlertEventId", AlertEventId);
                                cmd.Parameters.AddWithValue("@IsSignatureImage", IsSignatureImage);
                                cmd.Parameters.AddWithValue("@SignatureAttachmentName", GetSignatureName);
                                cmd.Parameters.AddWithValue("@SignatureAttachmentPath", SignatureAttachmentPath);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        if (flag == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid Image Extension.Please upload image with .bmp,.gif,.png,.jpg,.jpeg');window.location='Form_AlertEvent_AddUpdate.aspx'", true);
                        }
                    }
                    else
                    {
                        SignatureAttachmentPath = "";
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Alert Event Updated Successfully');window.location='Form_EmailAccountMaster.aspx'", true);
                   
                
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    tran.Commit();
                    con.Close();
                }
            }

        }
    }
    protected void Btn_New_Click(object sender, EventArgs e)
    {

    }
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_AlertEventMaster.aspx");
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
}