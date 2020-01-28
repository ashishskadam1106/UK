using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public partial class Form_PrintCertificateForKyu : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    //int EventHeaderId;
    string UserName = "";
    int Employee_Id = 0;


    int CallFor = 0;
    int EventHeaderId = 0, StudentId = 0;

    DateTime FromDate, Todate;
    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);

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

            UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            if (Request.QueryString.Count > 0)
            {
                try
                {
                    EventHeaderId = Convert.ToInt32(Decrypt(Request.QueryString["ID"].ToString()));
                    CallFor = Convert.ToInt32(Decrypt(Request.QueryString["Call"].ToString()));
                    StudentId = Convert.ToInt32(Decrypt(Request.QueryString["StudentId"].ToString()));
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid Operation');window.location='Home.aspx'", true);
                }
            }

            if (!IsPostBack)
            {
                Fill_Dojo();
                if (EventHeaderId != 0)
                {
                    Fill_GradingEvent(EventHeaderId);
                }
                if (CallFor == 3)
                {
                    BindNote(EventHeaderId, StudentId);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " $('#Modal_Note').modal({backdrop: 'static',keyboard: false})", true);
                }
                if (CallFor == 4)
                {
                    Fill_ViewPayment(StudentId);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " $('#Modal_Payment').modal({backdrop: 'static',keyboard: false})", true);
                }
            }

        }

    }
    private void BindNote(int EventHeaderId, int StudentId)
    {
        int StudentEventNoteId = 0;
        con.ConnectionString = str;
        cmd = new SqlCommand();
        cmd.CommandText = "Select StudentEventNoteId,StudentId,EventHeaderId,Note from tbl_StudentEventNote Where  IsDeleted=0 and EventHeaderId=" + EventHeaderId + "and StudentId=" + StudentId;
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            Tb_Note.Text = dr["Note"].ToString();
            StudentEventNoteId = Convert.ToInt32(dr["StudentEventNoteId"].ToString());
            Hf_StudentEnentNoteId.Value = StudentEventNoteId.ToString();
        }
        dr.Close();
        con.Close();

    }
    protected void Fill_GradingEvent(int EventHeaderId)
    {
        int DojoId = 0;
        if (Dd_DojoCode.SelectedIndex >= 0)
        {
            DojoId = Convert.ToInt32(Dd_DojoCode.SelectedValue);
        }

        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_FinishGradingEventPrintCertificate";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventHeaderId", EventHeaderId);
            cmd.Parameters.AddWithValue("@DojoId", DojoId);
            cmd.Connection = con;

            dr = cmd.ExecuteReader();

            String UnreadText = "";

            Int32 i = 0;

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    UnreadText += "<tr>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["Level"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["DojoCode"].ToString() + "</span></div></td>";

                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["FullName"].ToString() + "</span></div></td>";

                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["Grade"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["Fees"].ToString() + "</span></div></td>";

                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["FeesPaid"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["GradingFeeStatus"].ToString() + "</span></div></td>";

                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["MembershipFee"].ToString() + "</span></div></td>";

                    //  UnreadText += "<td class=\"c-grid-col-size-100\"><div><a  target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large\" title=\"Print Certificate\" href=\"Reports/Form_PrintPrintCertificate.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a> &nbsp<a class=\"fa fa-pencil-square-o inline-icon-size-large  \" title=\"Note\" data-toggle=\"modal\" data-target=\"#Modal_Note\"   href=\"#Modal_Note\" ><span><span></a>&nbsp<a class=\"fa fa-eye inline-icon-size-large\" title=\"View Payments\"\"><span><span></a> </div> </td>";
                    // UnreadText += "<td class=\"c-grid-col-size-100\"><div><a  target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large\" title=\"Print Certificate\" href=\"Reports/Form_PrintPrintCertificate.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a> &nbsp<a class=\"fa fa-pencil-square-o inline-icon-size-large  \" title=\"Note\" href=\"Form_PrintCertificateForKyu.aspx?ID=" + encrypt(dr["EventHeaderId"].ToString()) + "&Call=" + encrypt("3") + "&StudentId=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a>&nbsp<a class=\"fa fa-eye inline-icon-size-large\" title=\"View Payments\"\"><span><span></a> </div> </td>";
                    UnreadText += "<td class=\"c-grid-col-size-100\"><div><a  target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large\" title=\"Print Certificate\" href=\"Reports/Form_PrintPrintCertificate.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a> &nbsp <a class=\"fa fa-pencil-square-o inline-icon-size-large  \" title=\"Note\" href=\"Form_PrintCertificateForKyu.aspx?ID=" + encrypt(dr["EventHeaderId"].ToString()) + "&Call=" + encrypt("3") + "&StudentId=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a>&nbsp<a class=\"fa fa-eye inline-icon-size-large\" title=\"View Payments\" href=\"Form_PrintCertificateForKyu.aspx?ID=" + encrypt(dr["EventHeaderId"].ToString()) + "&Call=" + encrypt("4") + "&StudentId=" + encrypt(dr["StudentId"].ToString()) + " \"><span><span></a> </div> </td>";

                    UnreadText += "</tr>";
                    UnreadText += "</tr>";
                    tlist.InnerHtml = UnreadText;
                    i++;
                }


                dr.Close();
                con.Close();

            }
            else
            {
                UnreadText += "<tr>";
                UnreadText += "     <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "   <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "  <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "<td></td>";
                UnreadText += "</tr>";
                UnreadText += "</tr>";
                tlist.InnerHtml = UnreadText;
                i++;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
        finally
        {
            con.Close();
        }
    }

    public string encrypt(string encryptString)
    {
        string EncryptionKey = "SOFTUKKARATE11245PRITMEGHPAGEISSOFT";
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
        string EncryptionKey = "SOFTUKKARATE11245PRITMEGHPAGEISSOFT";
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
        return cipherText;
    }


    protected void Btn_Print_Click(object sender, EventArgs e)
    {
        int DojoId = 0;
        if (Dd_DojoCode.SelectedIndex >= 0)
        {
            DojoId = Convert.ToInt32(Dd_DojoCode.SelectedValue);
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Reports/Form_PrintCertificateForKYU.aspx?ID=" + encrypt(EventHeaderId.ToString()) + "&DojoId=" + encrypt(DojoId.ToString()) + "','_newtab');", true);
    }

    protected void Dd_DojoCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_GradingEvent(EventHeaderId);
    }
    protected void Btn_ViewEventDetail_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Form_EventDetail.aspx?ID=" + encrypt("0") + "&Call=" + encrypt("1") + "','_newtab');", true);

    }
    protected void Btn_printCertificatesAll_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Openwindow", "window.open('Reports/Form_PrintCertificateForAllStudents.aspx');", true);
    }

    private void Fill_Dojo()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select DojoId,DojoCode from tbl_Dojos  union select 0 DojoId,'All Dojo' DojoCode";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Title");
            Dd_DojoCode.DataSource = ds.Tables["Title"];
            Dd_DojoCode.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void btn_SaveNote_Click(object sender, EventArgs e)
    {
        int EventId = 0, StudId = 0,NoteId=0;
        EventId = Convert.ToInt32(EventHeaderId.ToString());
        StudId = Convert.ToInt32(StudentId.ToString());
        //if (Hf_StudentEnentNoteId.Value != "0")
        //{
            NoteId = Convert.ToInt32(Hf_StudentEnentNoteId.Value);
        //}
        //else
        //{
        //    NoteId = 0;
        //}
        
        string Note = "";
        if (!String.IsNullOrEmpty(Tb_Note.Text))
        {
            Note = Tb_Note.Text;

            if (NoteId == 0)
            {

                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;

                try
                {
                    cmd.CommandText = "USP_StudentEventNoteInsert";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EventHeaderId", EventHeaderId);
                    cmd.Parameters.AddWithValue("@StudentId", StudentId);
                    cmd.Parameters.AddWithValue("@Note", Note);
                    cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                    cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    con.Close();
                    cmd.Parameters.Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Note Save successfully.')", true);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;

                try
                {
                    cmd.CommandText = "USP_StudentEventNoteUpdate";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StudentEventNoteId", NoteId);
                    cmd.Parameters.AddWithValue("@Note", Note);
                    cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                    cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    con.Close();
                    cmd.Parameters.Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Note Updated successfully.')", true);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Enter Note')", true);
        }
    }

    private void Fill_ViewPayment(int StudentId)
    {
        double FeePaid = 0, GradingFee = 0;
        string FeeDate = "";
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_Get_PaymentDetailForView";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                FeePaid = Convert.ToDouble(dr["AmountPaid"].ToString());
                FeeDate = dr["MembershipDate"].ToString();
                GradingFee = Convert.ToDouble(dr["GradingFee"].ToString());
            }
            dr.Close();
            con.Close();
            Tb_FeePaid.Text = FeePaid.ToString();
            Tb_GradingFee.Text = GradingFee.ToString();
            Lbl_Date.Text = FeeDate.ToString();
            Lbl_GradingDate.Text = "";

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
}

