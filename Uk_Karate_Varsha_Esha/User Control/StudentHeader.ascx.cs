using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class User_Control_StudentHeader : System.Web.UI.UserControl
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    int StudentId = 0;



    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["LoginStudentName"] == null || Session["LoginAuthenticated"] == "No")
        {
            Response.Redirect(@"~\Form_StudentLogin.aspx");
        }
        else
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            Lbl_UserName_Small.Text = Session["LoginStudentName"].ToString();
            Lbl_UserName_Large.Text = Session["LoginStudentName"].ToString();

            Lbl_EmpEmailId.Text = Session["LoginStudentEmail"].ToString();
            //Img_UserProfilePic_Small.ImageUrl = Session["UserImgPath"].ToString();
            //Img_UserProfilePic_Large.ImageUrl = Session["UserImgPath"].ToString();
            //Img_UserProfilePic_Small. = Session["UserImgPath"].ToString();


            StudentId = Convert.ToInt32(Session["LoginStudentId"].ToString());

            //Fill_Notification();
        }
    }
}