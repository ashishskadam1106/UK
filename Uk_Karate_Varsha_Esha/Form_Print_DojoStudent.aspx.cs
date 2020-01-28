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

using System.Text;
//using System.Net.Http;
using System.Net;
using System.IO;
using CS_Encryption;
using System.Security.Cryptography;

public partial class Form_Print_DojoStudent : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    string UserName = "";
    int Employee_Id = 0;

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

            // ServiceCentre_Id = Convert.ToInt32(Session["ServiceCentre_Id"].ToString());
            UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            if (!IsPostBack)
            {
                Fill_Dojo();
                Fill_Term();
                Fill_DojoStudents();
            }
        }
    }

    private void Fill_Dojo()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select DojoId,(DojoCode +'-'+ DojoAddress) DojoCode from tbl_Dojos";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Dojo");
            Dd_DojoName.DataSource = ds.Tables["Dojo"];
            Dd_DojoName.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    private void Fill_Term()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select TermId,Term From tbl_Term";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Dojo");
            Dd_Term.DataSource = ds.Tables["Dojo"];
            Dd_Term.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    //private void Fill_DojoStudents()
    //{
    //    int TermId = 0, DojoId = 0;
    //    if (Dd_Term.SelectedIndex >= 0)
    //    {
    //        TermId = Convert.ToInt32(Dd_Term.SelectedValue);
    //    }
    //    if (Dd_DojoName.SelectedIndex >= 0)
    //    {
    //        DojoId = Convert.ToInt32(Dd_DojoName.SelectedValue);
    //    }

    //    con.ConnectionString = str;
    //    con.Open();
    //    cmd = new SqlCommand();
    //    cmd.CommandText = "USP_GetDojoStudentsList";
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@DojoId", DojoId);
    //    cmd.Connection = con;
    //    dr = cmd.ExecuteReader();

    //    String UnreadText = "";
    //    Int32 i = 0;

    //    if (dr.HasRows)
    //    {
    //        while (dr.Read())
    //        {

    //            if (TermId == 1)
    //            {

    //                tlist1.Visible = true;
    //                tlist2.Visible = false;
    //                UnreadText += "<tr>";
    //                UnreadText += "<td class=\"c-grid-col-size-50\">" + dr["Title"].ToString() +"</td>";
    //                UnreadText += "<td class=\"c-grid-col-size-50\">" + dr["StudentName"].ToString() + "</td>";
    //                UnreadText += "<td class=\"c-grid-col-size-50\"></td>";
    //                UnreadText += "<td class=\"c-grid-col-size-50\"></td>";
    //                UnreadText += "<td class=\"c-grid-col-size-50\"></td>";
    //                UnreadText += "<td class=\"c-grid-col-size-50\"></td>";
    //                UnreadText += "<td class=\"c-grid-col-size-50\"></td>";
    //                UnreadText += "<td class=\"c-grid-col-size-50\"></td>";
    //                tlist1.InnerHtml = UnreadText;
    //                i++;
    //            }
    //            else
    //            {
    //                tlist1.Visible = false;
    //                tlist2.Visible = true;
    //            }
    //        }
    //    }
    //    con.Close();
    //}
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        Fill_DojoStudents();

    }
    protected void Gv_JobCards_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_JobCards.PageIndex = e.NewPageIndex;
        Fill_DojoStudents();
    }

    private void Fill_DojoStudents()
    {
        int TermId = 0, DojoId = 0;
        if (Dd_Term.SelectedIndex >= 0)
        {
            TermId = Convert.ToInt32(Dd_Term.SelectedValue);
        }
        if (Dd_DojoName.SelectedIndex >= 0)
        {
            DojoId = Convert.ToInt32(Dd_DojoName.SelectedValue);
        }
        if (TermId == 1)
        {

            Gv_JobCards.Columns[2].Visible = true;
            Gv_JobCards.Columns[3].Visible = true;
            Gv_JobCards.Columns[4].Visible = true;
            Gv_JobCards.Columns[5].Visible = true;
            Gv_JobCards.Columns[6].Visible = true;
            Gv_JobCards.Columns[7].Visible = true;

            Gv_JobCards.Columns[8].Visible = false;
            Gv_JobCards.Columns[9].Visible = false;
            Gv_JobCards.Columns[10].Visible = false;
            Gv_JobCards.Columns[11].Visible = false;
            Gv_JobCards.Columns[12].Visible = false;
            Gv_JobCards.Columns[13].Visible = false;
        }
        else
        {
            Gv_JobCards.Columns[2].Visible = false;
            Gv_JobCards.Columns[3].Visible = false;
            Gv_JobCards.Columns[4].Visible = false;
            Gv_JobCards.Columns[5].Visible = false;
            Gv_JobCards.Columns[6].Visible = false;
            Gv_JobCards.Columns[7].Visible = false;


            Gv_JobCards.Columns[8].Visible = true;
            Gv_JobCards.Columns[9].Visible = true;
            Gv_JobCards.Columns[10].Visible = true;
            Gv_JobCards.Columns[11].Visible = true;
            Gv_JobCards.Columns[12].Visible = true;
            Gv_JobCards.Columns[13].Visible = true;
        }

        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetDojoStudentsList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DojoId", DojoId);
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "ClassMaster");
            dr = cmd.ExecuteReader();

            Gv_JobCards.DataSource = ds.Tables["ClassMaster"];
            Gv_JobCards.DataBind();
            con.Close();
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
    protected void Btn_Print_Click(object sender, EventArgs e)
    {

        int TermId = 0,DojoId=0;
        string Month1 = "", Month2 = "", Month3 = "", Month4 = "", Month5 = "", Month6 = "";
        if (Dd_Term.SelectedIndex >= 0)
        {
            TermId = Convert.ToInt32(Dd_Term.SelectedValue);
        }
        if (Dd_DojoName.SelectedIndex >= 0)
        {
            DojoId = Convert.ToInt32(Dd_DojoName.SelectedValue);
        }
        if (TermId == 1)
        {
            Month1 = "January";
            Month2 = "February";
            Month3 = "March";
            Month4 = "April";
            Month5 = "May";
            Month6 = "June";
        }
        else
        {
            if (TermId == 2)
            {
                Month1 = "July";
                Month2 = "August";
                Month3 = "September";
                Month4 = "October";
                Month5 = "November";
                Month6 = "December";
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Reports/Form_Print_DojoStudentReport.aspx?Month1=" + encrypt(Month1.ToString()) + "&Month2=" + encrypt(Month2.ToString()) + "&Month3=" + encrypt(Month3.ToString()) + "&Month4=" + encrypt(Month4.ToString()) + "&Month5=" + encrypt(Month5.ToString()) + "&Month6=" + encrypt(Month6.ToString()) + "&DojoId=" + encrypt(DojoId.ToString()) + "','_newtab');", true);
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
}