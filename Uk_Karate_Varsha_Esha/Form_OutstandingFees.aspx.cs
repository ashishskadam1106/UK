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

public partial class Form_OutstandingFees : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    string UserName = "";
    int Employee_Id = 0;
    //DateTime FromDate, Todate;
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
           
                Dp_FromDate.Text = CurrentUtc_IND.Date.ToString().Replace(" 00:00:00", "");
                Dp_ToDate.Text = CurrentUtc_IND.Date.ToString().Replace(" 00:00:00", "");
                if (Dp_FromDate.Text != "" && Dp_ToDate.Text != "")
                {
                    Fill_OutstandingFees1();
                }
                else
                {
                    SetFocus(Dp_FromDate);
                }
  
            }
        }
    }

    private void Fill_OutstandingFees1()
    {
        string StrFromdate = Dp_FromDate.Text.ToString();
        string StrToDate = Dp_ToDate.Text.ToString();

        DateTime FromDate = Convert.ToDateTime(StrFromdate);
        DateTime ToDate = Convert.ToDateTime(StrToDate);
 

        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();  //Query changed 22/5/2017
            //cmd.CommandText = "select case when patindex('%~/dist/img/%',ProfileImage_Path)>0 then Replace(ProfileImage_Path,'~/','') else isnull(ProfileImage_Path,'~/dist/img/avatar5.png') end ProfileImage_Path,isnull(E.FirstName,'')+' '+isnull(E.MiddleName,'')+' '+isnull(E.LastName,'') as 'Employee_Name',E.Address_Line1,E.Address_Line2,isnull(E.City_Text,'') City,E.Contact_Mobile,E.Email_Id,isnull(R.Role_Name,'') Role_Name,Employee_Id from tbl_Employee E left outer join tbl_City C on E.City_Id=C.City_Id left outer join tbl_Role R on E.Role_Id=R.Role_Id where E.Employee_Status_Id=1 and E.Is_Default=0 and E.ServiceCentre_Id=" + ServiceCentre_Id;
            //cmd.CommandText = "Select case when patindex('%~/%',ImageFilePath)>0 then Replace(ImageFilePath,'~/','') else isnull(ImageFilePath,'~/dist/img/avatar5.png') end ProfileImage_Path,isnull(S.FirstName,'')+' '+isnull(S.MiddleName,'')+' '+isnull(S.LastName,'') as 'StudentName',S.MobileNumber,S.EmailId,MembershipNumber,D.DojoCode,B.BeltName,S.MembershipDate from tbl_Students S Left join tbl_Dojos D on S.DojoId=S.DojoId  left join Tbl_Belt B On S.CurrentBeltId=B.BeltId Where S.IsDeleted=0";
            cmd.CommandText = "USP_GetOutstandingStudents_Temp4";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FromDate", FromDate);
            cmd.Parameters.AddWithValue("@ToDate", ToDate);
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
                    UnreadText += "     <td class=\"c-grid-col-size-250\"><div class=\"c-grid-label-left\"><span>" + dr["FullName"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + Convert.ToDateTime(dr["MembershipDate"].ToString()).ToString("dd/MM/yyyy") + "</span></div></td>";

                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + dr["MobileNumber"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-150\"><div class=\"c-grid-label-left\"><span>" + dr["DojoCode"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-150\"><div class=\"c-grid-label-left\"><span>" + dr["BeltName"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-left\"><span>" + Convert.ToDouble(dr["Balance"].ToString()).ToString("##.00") + "</span></div></td>";

                    UnreadText += "     <td class=\"c-grid-col-size-100\"><a class=\"btn btn-primary bg-purple c-bg-blueish btn-block  btn-flat\" href=\"Form_PayFees.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span>Pay<span></a></td>";

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
                UnreadText += "     <td></td>";
                UnreadText += "     <td></td>";
                UnreadText += "     <td></td>";
                UnreadText += "     <td></td>";
                UnreadText += "     <td></td>";
                UnreadText += "     <td></td>";

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

    //protected void Btn_Pay_Click(object sender, EventArgs e)
    //{

    //}
    
protected void Btn_AnnualMembershipRenewal_Click(object sender, EventArgs e)
{

    Fill_OutstandingFees1();
    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Reports/Form_PrintAnnualMembershipRenewalReport.aspx','_newtab');", true);

}
protected void Btn_Search_Click(object sender, EventArgs e)
{
    Fill_OutstandingFees1();
}
protected void Btn_AnnualMembershipRenewal_Click1(object sender, EventArgs e)
{

}
protected void Btn_AnnualMembershipRenewal_Click2(object sender, EventArgs e)
{

}
protected void Btn_AnnualMembershipRenewal_Click3(object sender, EventArgs e)
{

}
}