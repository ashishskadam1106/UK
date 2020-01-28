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

public partial class Form_OutstandingGradingFees : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    string UserName = "";
    int Employee_Id = 0;
    DateTime FromDate, Todate;
    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);
    int EventHeaderId_Update;
    public List<CS_EventDetail> List_EventDetail { get { return (List<CS_EventDetail>)Session["CS_EventDetail"]; } set { Session["CS_EventDetail"] = value; } }
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
            //EventHeaderId_Update = Convert.ToInt32(Session["EventHeaderId_Update"].ToString());

            if (!IsPostBack)
            {
                List_EventDetail = new List<CS_EventDetail>();
                //Fill_GradingEvent();
                Fill_Grid();
                Fill_Dojo();

            }
        }
    }

    private void Fill_Grid()
    {
        int DojoId = 0;

        if (Dd_DojoCode.SelectedIndex >= 0)
        {
            DojoId = Convert.ToInt32(Dd_DojoCode.SelectedValue);
        }

        List_EventDetail.Clear();
        int IndexToAdd = 0;
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_Get_OutsatndingGradingFee";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@DojoId", DojoId);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                IndexToAdd = List_EventDetail.Count;
                List_EventDetail.Add(new CS_EventDetail()
                {
                    Index = IndexToAdd,
                    EventDetailId = Convert.ToInt32(dr["EventDetailId"].ToString()),
                    EventHeaderId = Convert.ToInt32(dr["EventHeaderId"].ToString()),
                    StudentId = Convert.ToInt32(dr["StudentId"].ToString()),
                    Label = dr["Label"].ToString(),
                    FullName = dr["FullName"].ToString(),
                    EventDate = dr["EventDate"].ToString(),
                    FeesPaid = Convert.ToDouble(dr["DueFee"].ToString()),
                    MembershipFee = dr["MembershipFeeStatus"].ToString(),
                    DojoName = dr["DojoName"].ToString(),
                    Grade = dr["Grade"].ToString(),
                    Fees = Convert.ToDouble(dr["PayFee"].ToString()),
                    GradingFee = Convert.ToDouble(dr["DueFee"].ToString())
                });
            }
            dr.Close();
            con.Close();

            for (int i = 0; i < List_EventDetail.Count; i++)
            {
                List_EventDetail[i].Index = i;
                List_EventDetail[i].SrNo = List_EventDetail[i].Index + 1;

            }
            if (List_EventDetail.Count > 0)
            {
                Gv_Event.DataSource = List_EventDetail;
                Gv_Event.DataBind();
            }
            else
            {
                Gv_Event.DataSource = null;
                Gv_Event.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('No Records found')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
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

    private void Fill_GradingEvent()
    {


        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_Get_OutsatndingGradingFee";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Connection = con;


            dr = cmd.ExecuteReader();

            String UnreadText = "";
            Int32 i = 0;

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    UnreadText += "<tr>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["Label"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["DojoName"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["FullName"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["Grade"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + Convert.ToDateTime(dr["EventDate"].ToString()).ToString("dd/MM/yyyy") + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["DueFee"].ToString() + "</span></div></td>";
                    //    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["PayFee"].ToString() + "</span></div></td>";
                    UnreadText += " <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span><input type=\"text\" Id=\"Tb_PayAmount\"></span><br/><span><button id=\"btn_Pay\" class=\"btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow\" type=\"button\" onclick=\"btn_Pay_Click\">Click Me!</button></span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["MembershipFeeStatus"].ToString() + "</span></div></td>";
                    UnreadText += "</tr>";
                    UnreadText += "</tr>";
                    tlist.InnerHtml = UnreadText;
                    i++;


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

    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        Fill_GradingEvent();
    }

    protected void btn_Pay_Click(object sender, EventArgs e)
    {
        double GradingAmount = 0, DueFees = 0, FeesPaid = 0,Payfee=0;
        int EventHeaderId = 0, EventDetailId = 0;
        var Index = ((GridViewRow)((Control)sender).NamingContainer).RowIndex;

        Label Lbl_Due = (Label)Gv_Event.Rows[Index].FindControl("Lbl_Due");
        Label Lbl_GradingFee = (Label)Gv_Event.Rows[Index].FindControl("Lbl_GradingFee");
        TextBox Tb_GradingAmount = (TextBox)Gv_Event.Rows[Index].FindControl("Tb_GradingAmount");
        HiddenField Hf_PayFee=(HiddenField)Gv_Event.Rows[Index].FindControl("Hf_PayFee");
        Button Btn_Pay = sender as Button;
        EventDetailId = Convert.ToInt32(Btn_Pay.CommandArgument.ToString());
        Payfee = Convert.ToDouble(Hf_PayFee.Value);
        if (!string.IsNullOrEmpty(Tb_GradingAmount.Text.ToString()))
        {
            GradingAmount = Convert.ToDouble(Tb_GradingAmount.Text.ToString());
        }

        if (!string.IsNullOrEmpty(Lbl_Due.Text.ToString()))
        {
            DueFees = Convert.ToDouble(Lbl_Due.Text.ToString());
        }



        if (GradingAmount <= DueFees && GradingAmount != 0)
        {
            FeesPaid = Payfee + GradingAmount;
        }
        if (GradingAmount > DueFees)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert(' Greading Fee must be less than fees')", true);
            Tb_GradingAmount.Text = "0.00";
        }
        else
        {
            if (FeesPaid == DueFees)
            {
                Lbl_GradingFee.Visible = true;
                Lbl_GradingFee.Text = "Fully Paid";
                Tb_GradingAmount.Visible = false;
                Btn_Pay.Visible = false;

            }
            else
            {
                Lbl_GradingFee.Visible = false;
                Lbl_GradingFee.Text = "Fully Paid";
                Tb_GradingAmount.Visible = true;
                Btn_Pay.Visible = true;
            }
            // Lbl_Due.Text = FeesPaid.ToString();

            try
            {
                con.ConnectionString = str;
                cmd = new SqlCommand();
                cmd.CommandText = "update tbl_EventDetail set PaidAmount=" + FeesPaid + "Where EventDetailId=" + EventDetailId;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Fill_Grid();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


    }
    protected void Save(int EventDetailId, double GradingAmount)
    {
        con.ConnectionString = str;
        cmd = new SqlCommand();
        con.Open();
        SqlTransaction tran = con.BeginTransaction();
        cmd.Transaction = tran;

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





    protected void Gv_Event_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_Event.PageIndex = e.NewPageIndex;
        Fill_Grid();
    }
    protected void Gv_Event_SelectedIndexChanged(object sender, System.EventArgs e)
    {

    }

    protected void Btn_Print_Click(object sender, System.EventArgs e)
    {
        int DojoId = 0;
        if (Dd_DojoCode.SelectedIndex >= 0)
        {
            DojoId = Convert.ToInt32(Dd_DojoCode.SelectedValue);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Reports/Form_Print_OutstandingGradingFee.aspx?Id=" + encrypt(DojoId.ToString()) + "','_newtab');", true);


    }
    protected void Dd_DojoCode_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Fill_Grid();
    }
}
