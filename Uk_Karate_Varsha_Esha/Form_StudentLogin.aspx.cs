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

public partial class Form_StudentLogin : System.Web.UI.Page
{

    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
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
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {

        try
        {
            string Password = "";
            string UserName = "";
            string StudentName = "";
            string Email = "", Mobile = "",MembershipNumber ="";
            int Result = 0, StudentId = 0, Employee_Id = 0,  User_Id = 0;

            UserName = Tb_UserName.Text.ToString();
            Password = Tb_Password.Text.ToString();

            con.ConnectionString = str;
            cmd = new SqlCommand();
            con.Open();
            cmd.CommandText = "USP_Student_Authentication";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);

            SqlParameter OP_Result = new SqlParameter();
            OP_Result.ParameterName = "@Result";
            OP_Result.DbType = DbType.Int32;
            OP_Result.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OP_Result);

            SqlParameter OP_StudentId = new SqlParameter();
            OP_StudentId.ParameterName = "@StudentId";
            OP_StudentId.DbType = DbType.Int32;
            OP_StudentId.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OP_StudentId);

            SqlParameter OP_StudentName = new SqlParameter();
            OP_StudentName.ParameterName = "@StudentName";
            OP_StudentName.DbType = DbType.String;
            OP_StudentName.Size = 200;
            OP_StudentName.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OP_StudentName);

           

            SqlParameter OP_Email = new SqlParameter();
            OP_Email.ParameterName = "@Email";
            OP_Email.DbType = DbType.String;
            OP_Email.Size = 100;
            OP_Email.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OP_Email);

            //SqlParameter OP_UserId = new SqlParameter();
            //OP_UserId.ParameterName = "@UserId";
            //OP_UserId.DbType = DbType.Int32;
            //OP_UserId.Direction = ParameterDirection.Output;

            //cmd.Parameters.Add(OP_UserId);


            //SqlParameter OP_MembershipNumber = new SqlParameter();
            //OP_MembershipNumber.ParameterName = "@MembershipNumber";
            //OP_MembershipNumber.DbType = DbType.String;
            //OP_MembershipNumber.Direction = ParameterDirection.Output;

            //cmd.Parameters.Add(OP_MembershipNumber);

            cmd.ExecuteNonQuery();
            con.Close();

            Result = Convert.ToInt32(OP_Result.Value.ToString());

            if (Result == 1)
            {
                StudentId = Convert.ToInt32(OP_StudentId.Value.ToString());
                StudentName = OP_StudentName.Value.ToString();
                Email = OP_Email.Value.ToString();
               // User_Id = Convert.ToInt32(OP_UserId.Value.ToString());
              //  MembershipNumber = OP_MembershipNumber.Value.ToString();

                Session["LoginStudentId"] = StudentId;
                Session["LoginStudentName"] = StudentName;
                Session["LoginAuthenticated"] = "Yes";
                Session["LoginStudentEmail"] = Email;
                Session["LoginStudentUserId"] = User_Id;
                Session["LoginMembershipNumber"] = MembershipNumber;

                con.Open();
                cmd.Dispose();

                Response.Redirect(@"~\Form_Welcome.aspx");

            }
            else if (Result == 0)
            {
                //Invalid user name and password
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error Invalid Username or Password')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }


}