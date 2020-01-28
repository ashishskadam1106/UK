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

public partial class Form_EmployeeMaster : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    string UserName = "";
    int Employee_Id = 0;
    int EmployeeId_ForDelete = 0;

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
                    EmployeeId_ForDelete = Convert.ToInt32(Request.QueryString[0].ToString());

                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid Operation');window.location='Home.aspx'", true);
                }
            }
            if (!IsPostBack)
            {
                if (EmployeeId_ForDelete != 0)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Confirm", "Confirm();", true);
                   
                    Delete_Student(EmployeeId_ForDelete);
                }
                Fill_EmployeeMaster();

            }
        }
    }

    private void Fill_EmployeeMaster()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();  //Query changed 22/5/2017
            //cmd.CommandText = "select case when patindex('%~/dist/img/%',ProfileImage_Path)>0 then Replace(ProfileImage_Path,'~/','') else isnull(ProfileImage_Path,'~/dist/img/avatar5.png') end ProfileImage_Path,isnull(E.FirstName,'')+' '+isnull(E.MiddleName,'')+' '+isnull(E.LastName,'') as 'Employee_Name',E.Address_Line1,E.Address_Line2,isnull(E.City_Text,'') City,E.Contact_Mobile,E.Email_Id,isnull(R.Role_Name,'') Role_Name,Employee_Id from tbl_Employee E left outer join tbl_City C on E.City_Id=C.City_Id left outer join tbl_Role R on E.Role_Id=R.Role_Id where E.Employee_Status_Id=1 and E.Is_Default=0 and E.ServiceCentre_Id=" + ServiceCentre_Id;
            cmd.CommandText = "select case when patindex('%~/%',ProfileImage_Path)>0 then Replace(ProfileImage_Path,'~/','') else isnull(ProfileImage_Path,'~/dist/img/avatar5.png') end ProfileImage_Path,isnull(E.FirstName,'')+' '+isnull(E.MiddleName,'')+' '+isnull(E.LastName,'') as 'Employee_Name',E.Address_Line1,E.Address_Line2,isnull(E.City_Text,'') City,E.Contact_Mobile,E.Email_Id,isnull(R.Role_Name,'') Role_Name,Employee_Id from tbl_Employee E left outer join tbl_City C on E.City_Id=C.City_Id left outer join tbl_Role R on E.Role_Id=R.Role_Id where E.Employee_Status_Id=1 and IsDeleted=0 and E.Is_Default=0";
            cmd.Connection = con;
            dr = cmd.ExecuteReader();

            String UnreadText = "";
            Int32 i = 0;

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    UnreadText += "<tr>";

                    UnreadText += "     <td class=\"c-grid-profile-pic\"><img src=\"" + dr[0].ToString() + "\" class=\"img-circle\" alt=\"User Image\" height=\"100\" width=\"100\"></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-300\"><div><span class=\"c-grid-label-3\">" + dr[1].ToString() + "</span><br/><span class=\"c-grid-label-1\">" + dr[7].ToString() + "<span></div> <div class=\"col-md-12 no-padding c-inline-space\"> <div class=\"col-md-2 c-col-size-14 no-padding\"><span>[A]</span></div> <div class=\"col-md-10 c-col-size-86 no-padding c-padding-left-1\"> <span>" + dr[2].ToString() + " " + dr[3].ToString() + " </span></div></div> <div class=\"c-col-md-12 no-padding\"><div class=\"col-md-2 c-col-size-14 no-padding\"><span>[C]</span></div><div class=\"col-md-10 c-col-size-86 no-padding c-padding-left-1\"><span>" + dr[4].ToString() + "</span></div></div> </td>";
                    UnreadText += "     <td class=\"c-grid-col-size-300\"><div class=\"col-md-12 no-padding\"><div class=\"col-md-2 c-col-size-14 no-padding\"><span>[E]</span></div><div class=\"col-md-10 c-col-size-86 no-padding\"><span>" + dr[6].ToString() + "</span></div></div>  <div class=\"col-md-12 no-padding\"><div class=\"col-md-2 c-col-size-14 no-padding\"><span>[M]</span></div><div class=\"col-md-10 c-col-size-86 no-padding\"><span>" + dr[5].ToString() + "</span></div></div> </td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><a class=\"btn btn-primary bg-purple c-bg-blueish btn-block  btn-flat\" href=\"Form_EmployeeMaster_AddUpdate.aspx?ID=" + dr[8].ToString() + "&Call=" + 3 + "\"><span>Edit<span></a> <a class=\"btn btn-primary bg-purple c-bg-blueish btn-block  btn-flat\" onclick=\"return confirm('Are you sure?')\"  href=\"Form_EmployeeMaster.aspx?ID=" + dr[8].ToString() + "\"><span>Delete<span></a></td>";

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
    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
    }
    protected void Btn_AddEmployee_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_EmployeeMaster_AddUpdate.aspx?ID=0" + "&Call=1");
    }
    private void Delete_Student(int EmployeeId)
    {

        try
        {
            

            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "USP_EmployeeDelete";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);

            cmd.ExecuteNonQuery();
            con.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Employee Deleted successfully');window.location='Form_EmployeeMaster.aspx'", true);


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

}