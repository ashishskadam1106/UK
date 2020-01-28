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

public partial class Form_RentPayment : System.Web.UI.Page
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
    int DojoId, CallFor = 0;


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
                    DojoId = Convert.ToInt32(Decrypt(Request.QueryString["ID"].ToString()));
                    CallFor = Convert.ToInt32(Decrypt(Request.QueryString["Call"].ToString()));

                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid Operation');window.location='Home.aspx'", true);
                }
            }


            if (!IsPostBack)
            {

                Fill_Dojo();
                Fill_Dojo_Term();
                Fill_Rent();
                if (CallFor == 2)
                {
                    BindPayment(DojoId);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " $('#Modal_Note').modal({backdrop: 'static',keyboard: false})", true);
                }
            }
        }
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

    private void Fill_Dojo_Term()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "select RentTermId,RentTerm from tbl_DojoRentTerm  union select 0 RentTermId,'---Select---' RentTerm";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Title");
            Dd_Term_Dojo.DataSource = ds.Tables["Title"];
            Dd_Term_Dojo.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    private void Fill_Rent()
    {
        int DojoId = 0, RentTermId = 0;

        if (Dd_DojoCode.SelectedIndex >= 0)
        {
            DojoId = Convert.ToInt32(Dd_DojoCode.SelectedValue);
        }
        if (Dd_Term_Dojo.SelectedIndex >= 0)
        {
            RentTermId = Convert.ToInt32(Dd_Term_Dojo.SelectedValue);
        }

        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetRentDetails";
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@DojoId", DojoId);
            cmd.Parameters.AddWithValue("@RentTermId", RentTermId);
            cmd.Connection = con;


            dr = cmd.ExecuteReader();

            String UnreadText = "";
            Int32 i = 0;

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    UnreadText += "<tr>";
                    UnreadText += "     <td class=\"c-grid-col-size-50\"><div class=\"c-grid-label-left\"><span>" + dr["SrNo"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["DojoCode"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["RentTerm"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["Rent"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["Balance"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["StartDate"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["DueDate"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><a class=\"btn btn-primary bg-purple c-bg-blueish btn-block  btn-flat\" href=\"Form_RentPayment.aspx?ID=" + encrypt(dr["DojoId"].ToString()) + "&Call=" + encrypt("2") + "\"><span>Pay<span></a></td>";
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
                UnreadText += "     <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "   <td></td>";
                UnreadText += "   <td></td>";
                UnreadText += "   <td></td>";
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

    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        Fill_Rent();
    }


    private void BindPayment(int DojoId)
    {
        int RentPaymentId = 0;
        Double Rent = 0;
        con.ConnectionString = str;
        cmd = new SqlCommand();
        cmd.CommandText = "Select D.DojoId,ISNULL(R.RentPaymentId,0)RentPaymentId,D.DojoCode,Convert(decimal(18,2),ISnull(D.Rent,0))Rent, Convert(decimal(18,2),(ISnull(D.Rent,0)-Isnull(R.PaidAmount,0)))Balance,Isnull(R.PaidAmount,0)PaidAmount,	case when d.RentTermId=1 then  Convert(varchar,(select dateadd(m, 1, StartDate)),103) else 	Convert(varchar,(SELECT DATEADD(qq, DATEDIFF(qq, 0, StartDate) + 1, 0)),103) end DueDate from tbl_Dojos D left join tbl_DojoRentTerm Dt on D.RentTermId=Dt.RentTermId left join tbl_RentPayment R on D.DojoId=R.DojoId  Where D.DojoId=" + DojoId + "and D.IsDeleted=0";
        cmd.Connection = con;
        con.Open();

        dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            Tb_Dojo.Text = dr["DojoCode"].ToString();
            Tb_DueDate.Text = dr["DueDate"].ToString();
            Tb_Rent.Text = dr["Balance"].ToString();
            Rent = Convert.ToDouble(dr["Rent"].ToString());
            Hf_Rent.Value = dr["Rent"].ToString();
            RentPaymentId = Convert.ToInt32(dr["RentPaymentId"].ToString());
            Hf_RentPaymentId.Value = RentPaymentId.ToString();
            Hf_PaidAmount.Value = dr["PaidAmount"].ToString();
        }
        dr.Close();
        con.Close();

    }
    protected void btn_Save_Click(object sender, System.EventArgs e)
    {
        int DojoId_ToUpdate = 0,RentPaymentId=0;
        Double RentPayment =Convert.ToDouble(Hf_Rent.Value);
        RentPaymentId = Convert.ToInt32(Hf_RentPaymentId.Value);
        double RentPaid=0,PaidAmount=0,TotalPaid=0;
        PaidAmount=Convert.ToDouble(Hf_PaidAmount.Value);
        DojoId_ToUpdate = Convert.ToInt32(DojoId.ToString());

        if (!string.IsNullOrEmpty(Tb_Rent.Text))
        {
            RentPaid = Convert.ToDouble(Tb_Rent.Text);
            TotalPaid = PaidAmount + RentPaid;

            if (TotalPaid > RentPayment)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Paid amount should be less than rent.')", true);
            }
            else
            {
                if (RentPaymentId == 0)
                {
                    con.ConnectionString = str;
                    cmd = new SqlCommand();
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    cmd.Transaction = tran;

                    try
                    {
                        cmd.CommandText = "USP_RentPaymentInsert";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DojoId", DojoId);
                        cmd.Parameters.AddWithValue("@RentPaid", TotalPaid);
                        cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                        cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                        cmd.ExecuteNonQuery();
                        tran.Commit();
                        con.Close();
                        cmd.Parameters.Clear();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Rent payment successfully.')", true);

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
                        cmd.CommandText = "USP_RentPaymentUpdate";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@RentPaymentId", RentPaymentId);
                        cmd.Parameters.AddWithValue("@RentPaid", TotalPaid);
                        cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                        cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                        cmd.ExecuteNonQuery();
                        tran.Commit();
                        con.Close();
                        cmd.Parameters.Clear();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Rent payment successfully.')", true);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                Fill_Rent();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Enter Amount')", true);
        }
    }
}