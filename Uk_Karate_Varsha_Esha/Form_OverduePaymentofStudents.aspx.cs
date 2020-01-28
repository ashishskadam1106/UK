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

public partial class Form_OverduePaymentofStudents : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    string UserName = "";
    int Employee_Id = 0;

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
                Fill_OverDuePayment();
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
            cmd.CommandText = "Select DojoId,(DojoCode +'-'+ DojoAddress) DojoCode from tbl_Dojos Union Select 0 DojoId,'All' DojoCode";
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
    protected void Btn_SendEmail_Click(object sender, EventArgs e)
    {

    }
    protected void Btn_SendTextMessage_Click(object sender, EventArgs e)
    {

    }
    protected void Btn_PrintReminderSlip_Click(object sender, EventArgs e)
    {
        int StudentId = 0;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Reports/Form_PrintReminderSlip_ForAllStudents.aspx?StudentId=" + encrypt(StudentId.ToString()) + "','_newtab');", true);

    }
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        Fill_OverDuePayment();
    }

    private void Fill_OverDuePayment()
    {
        int DojoId = 0;
        if (Dd_DojoName.SelectedIndex >= 0)
        {
            DojoId = Convert.ToInt32(Dd_DojoName.SelectedValue);
        }
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_Get_OverduePaymentofStudents";
            cmd.CommandType = CommandType.StoredProcedure;
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

                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["MembershipNumber"].ToString() + "</span></div></td>";
                    //UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["Title"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["StudentName"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["DojoCode"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["Beltname"].ToString() + "</span></div></td>";
                    //UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + Convert.ToDateTime(dr["MembershipDate"].ToString()).ToString("dd/MM/yyyy") + "</span></div></td>";

                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["FeeGenerationTypeReference"].ToString() + "</span></div></td>";

                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + Convert.ToDouble(dr["DueFee"].ToString()).ToString("##.00") + "</span></div></td>";
                    //UnreadText += "<td  class=\"c-grid-col-size-200\"><div><a  target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large fa-lg\" title=\"Print Reminder Slip\" href=\"Reports/Form_PrintReminderSlip.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a>&nbsp<a target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large fa-lg\" title=\"Pay Fee\" href=\"Reports/Form_PrintStarterpackfrontpage.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a>&nbsp<a class=\"fa fa-pencil-square-o inline-icon-size-large fa-lg\" title=\"Send Message\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("3") + "\"><span><span></a>&nbsp<a class=\"fa fa-pencil-square-o inline-icon-size-large fa-lg\" title=\"Send Text Message\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("3") + "\"><span><span></a>&nbsp<a class=\"fa fa-eye inline-icon-size-large fa-lg\" title=\"View Payments\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("2") + "\"><span><span></a>&nbsp </div> </td>";
                    UnreadText += "<td  class=\"c-grid-col-size-200\"><div><a  target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large fa-2x\" title=\"Print Reminder Slip\" href=\"Reports/Form_PrintReminderSlip.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a>&nbsp<a target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large fa-2x\" title=\"Pay Fee\" href=\"Reports/Form_PrintStarterpackfrontpage.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a>&nbsp<a class=\"fa fa-pencil-square-o inline-icon-size-large fa-2x\" title=\"Send Message\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("3") + "\"><span><span></a>&nbsp<a class=\"fa fa-pencil-square-o inline-icon-size-large fa-2x\" title=\"Send Text Message\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("3") + "\"><span><span></a>&nbsp<a class=\"fa fa-eye inline-icon-size-large fa-lg\" title=\"View Payments\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("2") + "\"><span><span></a>&nbsp </div> </td>";


                  //  UnreadText += "<td  class=\"c-grid-col-size-100\"><div><a  target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large\" title=\"Print Reminder Slip\" href=\"Reports/Form_PrintReminderSlip.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a>&nbsp<a target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large\" title=\"Pay Fee\" href=\"Reports/Form_PrintStarterpackfrontpage.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a></div><div>&nbps<a class=\"fa fa-pencil-square-o inline-icon-size-large\" title=\"Send Message\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("3") + "\"><span><span></a>&nbps<a class=\"fa fa-pencil-square-o inline-icon-size-large\" title=\"Send Text Message\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("3") + "\"><span><span></a></div><div>&nbps<a class=\"fa fa-eye inline-icon-size-large\" title=\"View Payments\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("2") + "\"><span><span></a> </div> </td>";




                   // UnreadText += "<td  class=\"c-grid-col-size-100\"><div><a class=\"fa fa-print inline-icon-size-large\" title=\"Print Reminder Slip\" href=\"Reports/Form_PrintReminderSlip.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("2") + "\"><span><span></a><a class=\"fa fa-print inline-icon-size-large\" title=\"Pay Fee\" href=\"Reports/Form_PrintStarterpackfrontpage.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("3") + "\"><span><span></a></div><div><a class=\"fa fa-pencil-square-o inline-icon-size-large\" title=\"Send Message\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("3") + "\"><span><span></a><a class=\"fa fa-pencil-square-o inline-icon-size-large\" title=\"Send Text Message\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("3") + "\"><span><span></a></div><div><a class=\"fa fa-eye inline-icon-size-large\" title=\"View Payments\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("2") + "\"><span><span></a> </div> </td>";




                  //  UnreadText += "<td  class=\"c-grid-col-size-100\"><div><a class=\"fa fa-eye inline-icon-size-large \" title=\"View Event Details\" href=\"Form_PrintCertificateForKyu.aspx?ID=" + encrypt(dr["EventHeaderId"].ToString()) + "&Call=" + encrypt("2") + "\"><span><span></a> <a class=\"fa fa-pencil-square-o inline-icon-size-large\" title=\"Live Event Details\" href=\"Form_PrintCertificateForKyu.aspx?ID=" + encrypt(dr["EventHeaderId"].ToString()) + "&Call=" + encrypt("3") + "\"><span><span></a> <a class=\"fa fa-sticky-note-o inline-icon-size-large\" title=\"Delete Event\" href=\"Form_PrintCertificateForKyu.aspx?ID=" + encrypt(dr["EventHeaderId"].ToString()) + "\"><span><span></a> <a class=\"fa fa-print inline-icon-size-large\" title=\"Print Certificates\" href=\"Form_Print_DojoStudent.aspx?ID=" + encrypt(dr["EventHeaderId"].ToString()) + "\"><span><span></a> </div>  <div><a target=\"_blank\" rel\"><span><span></a></div> </td>";




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
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "</tr>";


                UnreadText += "</tr>";
                tlist.InnerHtml = UnreadText;
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
    protected void Btn_Print_Click(object sender, EventArgs e)
    {
        int DojoId = 0;
        if (Dd_DojoName.SelectedIndex >= 0)
        {
            DojoId = Convert.ToInt32(Dd_DojoName.SelectedValue);
        }
       // ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Reports/Form_PrintOverduePaymentsOfStudents.aspx?DojoId=" + encrypt(DojoId.ToString()) + "','_newtab');", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Reports/Form_PrintOverduePaymentsOfStudents.aspx?DojoId=" + encrypt(DojoId.ToString()) + "','_newtab');", true);

    }
}