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
using System.Drawing;

public partial class Form_EventDetail : System.Web.UI.Page
{

    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    string UserName = "";
    int Employee_Id = 0, EventHeaderId = 0, CallFor = 0;
    DateTime FromDate, Todate;
    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);
    public List<CS_EventDetail> List_EventDetail { get { return (List<CS_EventDetail>)Session["CS_EventDetail"]; } set { Session["CS_EventDetail"] = value; } }
    public int RowIndex { get { return (int)ViewState["RowIndex"]; } set { ViewState["RowIndex"] = value; } }

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

            // ServiceCentre_Id = Convert.ToInt32(Session["ServiceCentre_Id"].ToString());
            UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            if (Request.QueryString.Count > 0)
            {
                try
                {
                    EventHeaderId = Convert.ToInt32(Decrypt(Request.QueryString["ID"].ToString()));
                    CallFor = Convert.ToInt32(Decrypt(Request.QueryString["Call"].ToString()));

                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid Operation');window.location='Home.aspx'", true);
                }
            }

            if (!IsPostBack)
            {
                RowIndex = 0;
                List_EventDetail = new List<CS_EventDetail>();

                if (CallFor == 1)
                {
                    Fill_LiveGrid();
                }
                else
                {
                    Fill_Grid();
                }
                Fill_EventLabel();
              //  Fill_GradingEvent();


            }
        }
    }
    private void Fill_EventLabel()
    {
        String EventLabel = "";

        if (EventHeaderId != 0)
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select EventHeaderId,(EventLabel+' : '+'Pay Grading Fee for Kyu : '+B.Level+'and Start Date : '+Convert(varchar,EventDate,106)) as Label from tbl_EventHeader E inner join Tbl_Belt B on E.EventKyuId=B.BeltId   Where IsDeleted=0  and E.EventHeaderId=" + EventHeaderId;
            // cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                EventLabel = dr["Label"].ToString();
            }
            if (!String.IsNullOrEmpty(EventLabel))
            {
                Lbl_EventLabel.Text = EventLabel.ToString();
            }
            dr.Close();
            con.Close();
        }
    }

    private void Fill_GradingEvent()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_Get_EventDetail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventHeaderId", EventHeaderId);
            cmd.Connection = con;
            dr = cmd.ExecuteReader();

            String UnreadText = "";
            Int32 i = 0;

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    UnreadText += "<tr>";

                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["Label"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["DojoName"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["FullName"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["Grade"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["Fees"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["FeesPaid"].ToString() + "</span></div></td>";

                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-center\" <input type=\"text\" id=\"Tb_Amount\" runat=\"server\" /></div></td>";

                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["MembershipFee"].ToString() + "</span></div></td>";
                    UnreadText += "<td>\"a\"</td>";
                    UnreadText += "</tr>";
                    UnreadText += "</tr>";
                    tlist.InnerHtml = UnreadText;
                    i++;
                }
                dr.Close();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
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


    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        Fill_GradingEvent();
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

    public HtmlForm Form_StartGradingEvent { get; set; }
    protected void Btn_ViewLiveEventDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_LiveEventDetails.aspx?ID=" + encrypt("0") + "&Call=" + encrypt("1"));
    }
    protected void Gv_Event_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void Gv_Event_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "Pay")
        {
            Label Lbl_Fees = (Label)Gv_Event.Rows[RowIndex].FindControl("Lbl_Fees");
            Label Lbl_FeesPaid = (Label)Gv_Event.Rows[RowIndex].FindControl("Lbl_FeesPaid");
            Label Lbl_GradingFee = (Label)Gv_Event.Rows[RowIndex].FindControl("Lbl_GradingFee");
            TextBox Tb_GradingAmount = (TextBox)Gv_Event.Rows[RowIndex].FindControl("Tb_GradingAmount");
            Button Btn_PayLater = (Button)Gv_Event.Rows[RowIndex].FindControl("Btn_PayLater");
            Button Btn_Pay = sender as Button;
            int StudentId = Convert.ToInt32(e.CommandArgument.ToString());
            double GradingAmount = 0, Fees = 0, FeesPaid = 0;
            int EventHeaderId = 0;

            if (!string.IsNullOrEmpty(Tb_GradingAmount.Text.ToString()))
            {
                GradingAmount = Convert.ToDouble(Tb_GradingAmount.Text.ToString());
            }
            if (!string.IsNullOrEmpty(Lbl_FeesPaid.Text.ToString()))
            {
                Fees = Convert.ToDouble(Lbl_Fees.Text.ToString());
            }
            if (!string.IsNullOrEmpty(Lbl_FeesPaid.Text.ToString()))
            {
                FeesPaid = Convert.ToDouble(Lbl_FeesPaid.Text.ToString());
            }
            if (GradingAmount <= Fees && GradingAmount != 0)
            {
                if (FeesPaid == Fees)
                {
                    Lbl_GradingFee.Visible = true;
                    Lbl_GradingFee.Text = "Fully Paid";
                    Tb_GradingAmount.Visible = false;
                    Btn_Pay.Visible = false;
                    Btn_PayLater.Visible = false;
                }
                else
                {
                    Lbl_GradingFee.Visible = false;
                    Lbl_GradingFee.Text = "Fully Paid";
                    Tb_GradingAmount.Visible = true;
                    Btn_Pay.Visible = true;
                    Btn_PayLater.Visible = true;
                }
                FeesPaid = GradingAmount;
                Lbl_FeesPaid.Text = FeesPaid.ToString();

                try
                {
                    con.ConnectionString = str;
                    cmd = new SqlCommand();
                    cmd.CommandText = "Select EventHeaderId,S.StudentId,S.FirstName from tbl_EventHeader Ed left join tbl_Students S on Ed.EventKyuId=S.CurrentBeltId and S.IsDeleted=0 Where StudentId=" + StudentId;
                    cmd.Connection = con;
                    con.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        EventHeaderId = Convert.ToInt32(dr["EventHeaderId"].ToString());
                    }
                    dr.Close();
                    con.Close();

                    con.ConnectionString = str;
                    cmd = new SqlCommand();
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.CommandText = "USP_EventDetailInsert";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventHeaderId", EventHeaderId);
                        cmd.Parameters.AddWithValue("@StudentId", StudentId);
                        cmd.Parameters.AddWithValue("@PaidAmount", GradingAmount);
                        cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                        cmd.Parameters.AddWithValue("@CreatedBy", UserName);

                        cmd.ExecuteNonQuery();

                        dr.Close();
                        con.Close();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert(' Greading Fee must be less than fees')", true);
                Tb_GradingAmount.Text = "0.00";

            }

        }
        if (e.CommandName == "PayLater")
        {

        }

    }
    protected void Gv_Event_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void Gv_Event_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int UpdateIndex = e.RowIndex;

        double GradingAmount = 0, Fees = 0, FeesPaid = 0;
        int EventHeaderId = 0, StudentId = 0;
        Label Lbl_Fees = (Label)Gv_Event.Rows[UpdateIndex].FindControl("Lbl_Fees");
        Label Lbl_FeesPaid = (Label)Gv_Event.Rows[UpdateIndex].FindControl("Lbl_FeesPaid");
        Label Lbl_GradingFee = (Label)Gv_Event.Rows[UpdateIndex].FindControl("Lbl_GradingFee");
        TextBox Tb_GradingAmount = (TextBox)Gv_Event.Rows[UpdateIndex].FindControl("Tb_GradingAmount");
        Button Btn_PayLater = (Button)Gv_Event.Rows[UpdateIndex].FindControl("Btn_PayLater");
        Button Btn_Pay = (Button)Gv_Event.Rows[UpdateIndex].FindControl("Btn_Pay");
        StudentId = Convert.ToInt32(Gv_Event.DataKeys[UpdateIndex].Value.ToString());
        if (!string.IsNullOrEmpty(Tb_GradingAmount.Text.ToString()))
        {
            GradingAmount = Convert.ToDouble(Tb_GradingAmount.Text.ToString());
        }
        if (!string.IsNullOrEmpty(Lbl_FeesPaid.Text.ToString()))
        {
            Fees = Convert.ToDouble(Lbl_Fees.Text.ToString());
        }
        if (!string.IsNullOrEmpty(Lbl_FeesPaid.Text.ToString()))
        {
            FeesPaid = Convert.ToDouble(Lbl_FeesPaid.Text.ToString());
        }
        if (GradingAmount <= Fees && GradingAmount != 0)
        {
            if (FeesPaid == Fees)
            {
                Lbl_GradingFee.Visible = true;
                Lbl_GradingFee.Text = "Fully Paid";
                Tb_GradingAmount.Visible = false;
                Btn_Pay.Visible = false;
                Btn_PayLater.Visible = false;
            }
            else
            {
                Lbl_GradingFee.Visible = false;
                Lbl_GradingFee.Text = "Fully Paid";
                Tb_GradingAmount.Visible = true;
                Btn_Pay.Visible = true;
                Btn_PayLater.Visible = true;
            }
            FeesPaid = GradingAmount;
            Lbl_FeesPaid.Text = FeesPaid.ToString();

            try
            {
                con.ConnectionString = str;
                cmd = new SqlCommand();
                cmd.CommandText = "Select EventHeaderId,S.StudentId,S.FirstName from tbl_EventHeader Ed left join tbl_Students S on Ed.EventKyuId=S.CurrentBeltId and S.IsDeleted=0 Where StudentId=" + StudentId;
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EventHeaderId = Convert.ToInt32(dr["EventHeaderId"].ToString());
                }
                dr.Close();
                con.Close();

                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandText = "USP_EventDetailInsert";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EventHeaderId", EventHeaderId);
                    cmd.Parameters.AddWithValue("@StudentId", StudentId);
                    cmd.Parameters.AddWithValue("@PaidAmount", GradingAmount);
                    cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                    cmd.Parameters.AddWithValue("@CreatedBy", UserName);

                    cmd.ExecuteNonQuery();

                    dr.Close();
                    con.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert(' Greading Fee must be less than fees')", true);
            Tb_GradingAmount.Text = "0.00";

        }


    }



    private void Fill_Grid()
    {


        List_EventDetail.Clear();

        int IndexToAdd = 0;
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_Get_EventDetail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventHeaderId", EventHeaderId);
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                IndexToAdd = List_EventDetail.Count;
                List_EventDetail.Add(new CS_EventDetail()
                {
                    Index = IndexToAdd,
                    EventHeaderId = Convert.ToInt32(dr["EventHeaderId"].ToString()),
                    StudentId = Convert.ToInt32(dr["StudentId"].ToString()),
                    Label = dr["Label"].ToString(),
                    FullName = dr["FullName"].ToString(),
                    Fees = Convert.ToDouble(dr["Fees"].ToString()),
                    FeesPaid = Convert.ToDouble(dr["FeesPaid"].ToString()),
                    MembershipFee = dr["MembershipFee"].ToString(),
                    DojoName = dr["DojoName"].ToString(),
                    Grade = dr["Grade"].ToString(),
                    EventKyuId = Convert.ToInt32(dr["EventKyuId"].ToString()),
                    GradingFee = Convert.ToDouble(dr["GradingFee"].ToString()),
                    // GradingFeeStatus = dr["GradingFeeStatus"].ToString(),

                });
            }
            dr.Close();
            con.Close();

            for (int i = 0; i < List_EventDetail.Count; i++)
            {
                List_EventDetail[i].Index = i;
                List_EventDetail[i].SrNo = List_EventDetail[i].Index + 1;
                Hf_EventHeaderId.Value = List_EventDetail[i].EventHeaderId.ToString();
            }
            Gv_Event.DataSource = List_EventDetail;
            Gv_Event.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    private void Fill_LiveGrid()
    {
        List_EventDetail.Clear();
        int IndexToAdd = 0;
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_Get_LiveEventDetail";
            cmd.CommandType = CommandType.StoredProcedure;
            //  cmd.Parameters.AddWithValue("@EventHeaderId", EventHeaderId);
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                IndexToAdd = List_EventDetail.Count;
                List_EventDetail.Add(new CS_EventDetail()
                {
                    Index = IndexToAdd,
                    EventHeaderId = Convert.ToInt32(dr["EventHeaderId"].ToString()),
                    StudentId = Convert.ToInt32(dr["StudentId"].ToString()),
                    Label = dr["Label"].ToString(),
                    FullName = dr["FullName"].ToString(),
                    Fees = Convert.ToDouble(dr["Fees"].ToString()),
                    FeesPaid = Convert.ToDouble(dr["FeesPaid"].ToString()),
                    MembershipFee = dr["MembershipFee"].ToString(),
                    DojoName = dr["DojoName"].ToString(),
                    Grade = dr["Grade"].ToString(),
                    EventKyuId = Convert.ToInt32(dr["EventKyuId"].ToString()),
                    GradingFee = Convert.ToDouble(dr["GradingFee"].ToString()),
                    GradingFeeStatus = dr["GradingFeeStatus"].ToString()
                });
            }
            dr.Close();
            con.Close();

            for (int i = 0; i < List_EventDetail.Count; i++)
            {
                List_EventDetail[i].Index = i;
                List_EventDetail[i].SrNo = List_EventDetail[i].Index + 1;
            }
            Gv_Event.DataSource = List_EventDetail;
            Gv_Event.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    protected void Gv_Event_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_Event.PageIndex = e.NewPageIndex;
        Fill_Grid();
    }
    protected void Btn_Pay_Click(object sender, EventArgs e)
    {

        string ValidationMsg = "";
        double GradingAmount = 0, Fees = 0, FeesPaid = 0, TotalFee = 0, TotalFeePaid = 0;
        int EventHeaderId = 0, StudentId = 0, EventDetailId = 0;
        var Index = ((GridViewRow)((Control)sender).NamingContainer).RowIndex;

        EventHeaderId = Convert.ToInt32(Hf_EventHeaderId.Value);

        Label Lbl_Fees = (Label)Gv_Event.Rows[Index].FindControl("Lbl_Fees");
        Label Lbl_FeesPaid = (Label)Gv_Event.Rows[Index].FindControl("Lbl_FeesPaid");
        Label Lbl_GradingFee = (Label)Gv_Event.Rows[Index].FindControl("Lbl_GradingFee");
        TextBox Tb_GradingAmount = (TextBox)Gv_Event.Rows[Index].FindControl("Tb_GradingAmount");
        Button Btn_PayLater = (Button)Gv_Event.Rows[Index].FindControl("Btn_PayLater");
        Button Btn_Register = (Button)Gv_Event.Rows[Index].FindControl("Btn_Register");
        Button Btn_Pay = sender as Button;
        StudentId = Convert.ToInt32(Btn_Pay.CommandArgument.ToString());

        Hf_StudentId.Value = StudentId.ToString();
        if (!string.IsNullOrEmpty(Tb_GradingAmount.Text.ToString()))
        {
            GradingAmount = Convert.ToDouble(Tb_GradingAmount.Text.ToString());
        }

        if (!string.IsNullOrEmpty(Lbl_FeesPaid.Text.ToString()))
        {
            Fees = Convert.ToDouble(Lbl_Fees.Text.ToString());
        }
        if (!string.IsNullOrEmpty(Lbl_FeesPaid.Text.ToString()))
        {
            FeesPaid = Convert.ToDouble(Lbl_FeesPaid.Text.ToString());
        }
        if (GradingAmount > 0)
        {
            TotalFee = Convert.ToDouble(GradingAmount + FeesPaid);
        }
        if (TotalFee > Fees)
        {
            Tb_GradingAmount.Text = "0.00";
            Tb_GradingAmount.BorderColor = Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Grading fee must be less than fee')", true);

        }
        else
        {
            if (TotalFee == Fees)
            {
                Lbl_GradingFee.Visible = true;
                Lbl_GradingFee.Text = "Fully Paid";
                Tb_GradingAmount.Visible = false;
                Btn_Pay.Visible = false;
                Btn_PayLater.Visible = false;
                Btn_Register.Visible = true;
            }
            else
            {
                if (TotalFee < Fees)
                {
                    Lbl_GradingFee.Visible = false;
                    Tb_GradingAmount.Visible = true;
                    Btn_Pay.Visible = true;
                    Btn_PayLater.Visible = true;
                    Btn_Register.Visible = false;
                }
            }

            TotalFeePaid = Convert.ToDouble(GradingAmount + FeesPaid);
            Lbl_FeesPaid.Text = TotalFeePaid.ToString();

            try
            {
                con.ConnectionString = str;
                cmd = new SqlCommand();
                cmd.CommandText = "Select EventDetailId from tbl_EventDetail Where StudentId=" + StudentId + "and EventHeaderId=" + EventHeaderId;

                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EventDetailId = Convert.ToInt32(dr["EventDetailId"].ToString());
                }

                dr.Close();
                con.Close();

            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            Save(EventDetailId, EventHeaderId, StudentId, TotalFee);
            Tb_GradingAmount.Text = "0.00";
        }
    }
    protected void Btn_PayLater_Click(object sender, EventArgs e)
    {
        var Index = ((GridViewRow)((Control)sender).NamingContainer).RowIndex;
        Label Lbl_Fees = (Label)Gv_Event.Rows[Index].FindControl("Lbl_Fees");
        Label Lbl_FeesPaid = (Label)Gv_Event.Rows[Index].FindControl("Lbl_FeesPaid");
        Label Lbl_GradingFee = (Label)Gv_Event.Rows[Index].FindControl("Lbl_GradingFee");
        TextBox Tb_GradingAmount = (TextBox)Gv_Event.Rows[Index].FindControl("Tb_GradingAmount");
        Button Btn_Pay = (Button)Gv_Event.Rows[Index].FindControl("Btn_Pay");
        Button Btn_PayLater = sender as Button;
        int StudentId = Convert.ToInt32(Btn_PayLater.CommandArgument.ToString());

        con.ConnectionString = str;
        cmd = new SqlCommand();
        con.Open();
        SqlTransaction tran = con.BeginTransaction();
        cmd.Transaction = tran;

        try
        {
            cmd.CommandText = "USP_RegisterStudentInsert";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventHeaderId", EventHeaderId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
            cmd.Parameters.AddWithValue("@CreatedBy", UserName);
            cmd.ExecuteNonQuery();
            tran.Commit();
            con.Close();
            cmd.Parameters.Clear();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Student has been moved to live pool successfully.')", true);
            Fill_Grid();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        Lbl_GradingFee.Visible = true;
        Lbl_GradingFee.Text = "Pay Later";
        Tb_GradingAmount.Visible = false;
        Btn_Pay.Visible = false;
        Btn_PayLater.Visible = false;

    }


    protected void Save(int EventDetailId, int EventHeaderId, int StudentId, double GradingAmount)
    {
        con.ConnectionString = str;
        cmd = new SqlCommand();
        con.Open();
        SqlTransaction tran = con.BeginTransaction();
        cmd.Transaction = tran;
        if (EventDetailId == 0)
        {
            try
            {
                cmd.CommandText = "USP_EventDetailInsert";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EventHeaderId", EventHeaderId);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                cmd.Parameters.AddWithValue("@PaidAmount", GradingAmount);
                cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                cmd.Parameters.AddWithValue("@CreatedBy", UserName);

                cmd.ExecuteNonQuery();


                tran.Commit();
                con.Close();
                cmd.Parameters.Clear();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Fee Paid successfully.')", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            try
            {
                cmd.CommandText = "USP_EventDetailUpdate";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EventDetailId", EventDetailId);
                cmd.Parameters.AddWithValue("@PaidAmount", GradingAmount);
                cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                cmd.Parameters.AddWithValue("@CreatedBy", UserName);

                cmd.ExecuteNonQuery();


                tran.Commit();
                con.Close();
                cmd.Parameters.Clear();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Fee Paid successfully.')", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    protected void Gv_Event_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int IsFullyPaid = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Index = (e.Row.RowIndex) + 1;
            TextBox Tb_GradingAmount = (TextBox)e.Row.FindControl("Tb_GradingAmount");
            Label Lbl_GradingFee = (Label)e.Row.FindControl("Lbl_GradingFee");
            Button Btn_Pay = (Button)e.Row.FindControl("Btn_Pay");
            Button Btn_PayLater = (Button)e.Row.FindControl("Btn_PayLater");

            try
            {
                con.ConnectionString = str;
                cmd = new SqlCommand();
                cmd.CommandText = "Select Case when Convert(decimal(18,2),ISnull(f.Amount,0))-CONVERT(decimal(18,2),Isnull(Ed.PaidAmount,0))=0 then 1 else 0 end IsFullyPaid  from tbl_Students S left join Tbl_Belt B on S.CurrentBeltId=B.BeltId left join tbl_Fees F on B.GradingFeeId=F.FeeId left join tbl_EventHeader E on B.BeltId=E.EventKyuId left join tbl_EventDetail Ed on S.StudentId=Ed.StudentId Where E.EventHeaderId=" + EventHeaderId + "and S.IsDeleted=0";

                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        IsFullyPaid = Convert.ToInt32(dr["IsFullyPaid"].ToString());

                        if (IsFullyPaid == 1)
                        {
                            //Lbl_GradingFee.Visible = true;
                            //Lbl_GradingFee.Text = "Fully Paid";
                            //Tb_GradingAmount.Visible = false;
                            //Btn_Pay.Visible = false;
                            //Btn_PayLater.Visible = false;
                        }
                        else
                            if (IsFullyPaid == 0)
                            {
                                //Lbl_GradingFee.Visible = false;
                                //Lbl_GradingFee.Text = "";
                                //Tb_GradingAmount.Visible = true;
                                //Btn_Pay.Visible = true;
                                //Btn_PayLater.Visible = true;
                            }
                    }

                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
        }

    }

    protected void Gv_Event_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Btn_Register_Click(object sender, EventArgs e)
    {
        int StudentId = 0, EventHeaderId = 0;
        if (!String.IsNullOrEmpty(Hf_EventHeaderId.Value))
        {
            EventHeaderId = Convert.ToInt32(Hf_EventHeaderId.Value);
        }
        if (!String.IsNullOrEmpty(Hf_StudentId.Value))
        {
            StudentId = Convert.ToInt32(Hf_StudentId.Value);
        }
        con.ConnectionString = str;
        cmd = new SqlCommand();
        con.Open();
        SqlTransaction tran = con.BeginTransaction();
        cmd.Transaction = tran;

        try
        {
            cmd.CommandText = "USP_RegisterStudentInsert";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventHeaderId", EventHeaderId);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
            cmd.Parameters.AddWithValue("@CreatedBy", UserName);
            cmd.ExecuteNonQuery();
            tran.Commit();
            con.Close();
            cmd.Parameters.Clear();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Student has been moved to live pool successfully.')", true);
            Fill_Grid();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void Btn_ViewPayment_Click(object sender, EventArgs e)
    {
        int StudentId = 0, EventId = 0;
        var Index = ((GridViewRow)((Control)sender).NamingContainer).RowIndex;

        LinkButton Btn_ViewPayment = (LinkButton)Gv_Event.Rows[Index].FindControl("Btn_ViewPayment");
        StudentId = Convert.ToInt32(Btn_ViewPayment.CommandArgument.ToString());
       
        Fill_ViewPayment(StudentId);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " $('#Modal_Payment').modal({backdrop: 'static',keyboard: false})", true);
    }
    protected void Btn_Note_Click(object sender, EventArgs e)
    {
        int StudentId = 0, EventId = 0;
        var Index = ((GridViewRow)((Control)sender).NamingContainer).RowIndex;

        Button Btn_Note = (Button)Gv_Event.Rows[Index].FindControl("Btn_PayLater");
        StudentId = Convert.ToInt32(Btn_Note.CommandArgument.ToString());
        EventId = Convert.ToInt32(EventHeaderId);
        BindNote(EventId, StudentId);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " $('#Modal_Note').modal({backdrop: 'static',keyboard: false})", true);

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
    protected void btn_SaveNote_Click(object sender, EventArgs e)
    {

        int EventId = 0, StudId = 0, NoteId = 0;
        EventId = Convert.ToInt32(EventHeaderId.ToString());
        StudId = Convert.ToInt32(Hf_StudentId.Value);

        NoteId = Convert.ToInt32(Hf_StudentEnentNoteId.Value);


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
                    cmd.Parameters.AddWithValue("@StudentId", StudId);
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
        double FeePaid = 0,GradingFee=0;
        string FeeDate="";
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