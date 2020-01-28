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
public partial class Form_forgettingpassword : System.Web.UI.Page
{
    //string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    //SqlConnection con = new SqlConnection();
    //SqlCommand cmd;
    //SqlDataReader dr;



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
    protected void Btn_Email_Click(object sender, EventArgs e)
    {

    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
      



        try
        {
            string UserName = "";
           
            string Email = "";
            Email = Tb_EmailId.Text.ToString();
            UserName = Tb_UserName.Text.ToString();
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            //cmd.CommandText =("select b.Email_Id,a.UserName from tbl_Authentication a inner join tbl_Employee b  on a.Reference_Id =b.Employee_Id  where b.Email_Id=@Email and  a.UserName=@UserName",con );
            
            cmd.CommandText = "USP_ForgotPassword";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email", Tb_EmailId.Text);
            cmd.Parameters.AddWithValue("@UserName", Tb_UserName.Text);

            
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            // ds = new DataSet();
            da.Fill(dt);

            con.Close();



            if (dt.Rows.Count > 0)
            {
                Response.Redirect("RetypePassword.aspx");
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('You're username and EmailId is incorrect')", true);
                //Label1.Text = "You're username and EmailId is incorrect";
                //Label1.ForeColor = System.Drawing.Color.Red;

            }


        }
        //protectedvoid LinkButton1_Click(object sender, EventArgs e) {  

        //    Response.Redirect("ForgetPassword.aspx");  
        //} 


        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
       

    }
}

