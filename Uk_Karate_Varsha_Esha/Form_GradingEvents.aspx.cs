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

public partial class Form_GradingEvents : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    string UserName = "";
    int Employee_Id = 0, EventHeaderId_ToDelete = 0;
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

            // ServiceCentre_Id = Convert.ToInt32(Session["ServiceCentre_Id"].ToString());
            UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());


            if (Request.QueryString.Count > 0)
            {
                try
                {
                    EventHeaderId_ToDelete = Convert.ToInt32(Decrypt(Request.QueryString[0].ToString()));

                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid Operation');window.location='Home.aspx'", true);
                }
            }

            if (!IsPostBack)
            {
                Fill_GradingEvent();
            }
        }
    }

    private void Delete_Event(int EventHeaderId_ToDelete)
    {

        try
        {


            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "USP_EventDelete";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            cmd.Parameters.AddWithValue("@EventHeaderId", EventHeaderId_ToDelete);

            cmd.ExecuteNonQuery();
            con.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Event Deleted successfully');window.location='Form_GradingEvents.aspx'", true);


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
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

    private void Fill_GradingEvent()
    {
        string BeltIds = "", DojoIds = "";
        Boolean IsDorment = false;

        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();  //Query changed 22/5/2017
            //cmd.CommandText = "Select EventHeaderId,EventLabel,Convert(varchar,EventDate,103)EventDate,B.Level from tbl_EventHeader E inner join Tbl_Belt B on E.EventKyuId=B.BeltId  Where IsDeleted=0  and  EventDate>=GetDate()";
            cmd.CommandText = "select  E.EventHeaderId,E.EventLabel,E.EventDate,B.EventKyu_Name from tbl_EventHeader E inner join tbl_EventKyuMaster B  ON E.EventKyuId=B.EventKyu_Id Where IsDeleted=0";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            dr = cmd.ExecuteReader();

            String UnreadText = "";
            Int32 i = 0;

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    UnreadText += "<tr>";

                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["EventLabel"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + Convert.ToDateTime(dr["EventDate"]).ToString("dd/MM/yyyy") + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["EventKyu_Name"].ToString() + "</span></div></td>";
                    UnreadText += "<td  class=\"c-grid-col-size-100\"><div><a class=\"fa fa-eye inline-icon-size-large \" title=\"View Event Details\" href=\"Form_EventDetail.aspx?ID=" + encrypt(dr["EventHeaderId"].ToString()) + "&Call=" + encrypt("2") + "\"><span><span></a> <a class=\"fa fa-pencil-square-o inline-icon-size-large\" title=\"Live Event Details\" href=\"Form_LiveEventDetails.aspx?ID=" + encrypt("0") + "&Call=" + encrypt("1") + "\"><span><span></a> <a class=\"fa fa-sticky-note-o inline-icon-size-large\" title=\"Delete Event\" onclick=\"return confirm('Are you sure?')\" href=\"Form_GradingEvents.aspx?ID=" + encrypt(dr["EventHeaderId"].ToString()) + "\"><span><span></a> </div>  </td>";
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
    protected void Btn_Print_Click(object sender, EventArgs e)
    {

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Openwindow", "window.open('Reports/Form_PrintManageStudentList.aspx?From=" + encrypt(FromDate.ToString()) + "&To=" + encrypt(ToDate.ToString()) + "&BeltIds=" + encrypt(BeltIds.ToString()) + "&DojoIds=" + encrypt(DojoIds.ToString()) + "','_newtab');", true);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Openwindow", "window.open('Reports/Form_PrintGradingEvents.aspx');", true);

    }
}
