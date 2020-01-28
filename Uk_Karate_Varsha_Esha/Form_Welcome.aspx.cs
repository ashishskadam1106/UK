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
using CS_Encryption;


public partial class Form_Welcome : System.Web.UI.Page
{

    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    int StudentId;
    string UserName;
    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);
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

            UserName = Session["LoginStudentName"].ToString();
            StudentId = Convert.ToInt32(Session["LoginStudentId"].ToString());
            if (!IsPostBack)
            {
                if (Session["LoginStudentName"] != null)
                {
                    Lbl_Welcome.Text ="Welcome" +" " + Session["LoginStudentName"].ToString();
                    Label4.Text = "your email has been verified";
                }
            }
        }

    }
}