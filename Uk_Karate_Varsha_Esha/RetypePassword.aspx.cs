using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class RetypePassword : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            //Response.Redirect(@"~\Index.aspx");
        }
    }
    protected void Btn_Login_Click(object sender, EventArgs e)
    {
      

        string Password = "";
        string ConfirmPassword = "";
        string UserName = "";
        Password = Tb_Password.Text.ToString();
        ConfirmPassword = Tb_ConfirmPassword.Text.ToString();
        UserName = Tb_UserName.Text.ToString();
    //Address = Tb_StudentAddress.Text.ToString();
    //PhoneNo = Convert.ToInt32(Tb_PhoneNo.Text.ToString());
        if (Tb_Password.Text.Trim() == Tb_ConfirmPassword.Text.Trim())
       
        {

            con.ConnectionString = str;
            con.Open();

            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "USP_RetypePassword";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@ConfirmPassword", ConfirmPassword);


            cmd.ExecuteNonQuery();




            con.Close();
            Response.Write("Password Changed");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Password Changed!!!!!!',Please login again)", true);


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Password Changed!!!!!!',Please login again);window.location='Index.aspx';", true);

            Response.Redirect("Index.aspx");
        }
        else
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Password  and confirmpassword are not same')", true);
        }


    }
}