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

public partial class Index : System.Web.UI.Page
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
    protected void Btn_Login_Click(object sender, EventArgs e)
    {
        try
        {
            string Password = "";
            string UserName = "";
            string Employee_Name = "";
            string Email = "", Mobile = "", UserImgPath="";
            int Result = 0, Role_Id = 0, Employee_Id = 0, Is_SuperAdmin = 0, User_Id = 0, ServiceCentre_Id = 0, ServiceCentreCompany_Id=0;

            UserName = Tb_UserName.Text.ToString();
            Password = Tb_Password.Text.ToString();

            con.ConnectionString = str;
            cmd = new SqlCommand();
            con.Open();
            cmd.CommandText = "USP_User_Authentication";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);

            SqlParameter OP_Result = new SqlParameter();
            OP_Result.ParameterName = "@Result";
            OP_Result.DbType = DbType.Int32;
            OP_Result.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OP_Result);

            SqlParameter OP_Role_Id = new SqlParameter();
            OP_Role_Id.ParameterName = "@Role_Id";
            OP_Role_Id.DbType = DbType.Int32;
            OP_Role_Id.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OP_Role_Id);

            SqlParameter OP_Employee_Id = new SqlParameter();
            OP_Employee_Id.ParameterName = "@Employee_Id";
            OP_Employee_Id.DbType = DbType.Int32;
            OP_Employee_Id.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OP_Employee_Id);


            SqlParameter OP_IsSuperAdmin = new SqlParameter();
            OP_IsSuperAdmin.ParameterName = "@Is_SuperAdmin";
            OP_IsSuperAdmin.DbType = DbType.Int32;
            OP_IsSuperAdmin.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OP_IsSuperAdmin);

            SqlParameter OP_Employee_Name = new SqlParameter();
            OP_Employee_Name.ParameterName = "@Employee_Name";
            OP_Employee_Name.DbType = DbType.String;
            OP_Employee_Name.Size = 200;
            OP_Employee_Name.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OP_Employee_Name);

            SqlParameter OP_Contact_Mobile = new SqlParameter();
            OP_Contact_Mobile.ParameterName = "@Contact_Mobile";
            OP_Contact_Mobile.DbType = DbType.String;
            OP_Contact_Mobile.Size = 50;
            OP_Contact_Mobile.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OP_Contact_Mobile);

            SqlParameter OP_Email = new SqlParameter();
            OP_Email.ParameterName = "@Email";
            OP_Email.DbType = DbType.String;
            OP_Email.Size = 100;
            OP_Email.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OP_Email);

            SqlParameter OP_UserId = new SqlParameter();
            OP_UserId.ParameterName = "@User_Id";
            OP_UserId.DbType = DbType.Int32;
            OP_UserId.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OP_UserId);

            SqlParameter OP_UserImg = new SqlParameter();
            OP_UserImg.ParameterName = "@UserImgPath";
            OP_UserImg.DbType = DbType.String;
            OP_UserImg.Size = 8000;
            OP_UserImg.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(OP_UserImg);

            //SqlParameter OP_ServiceCentre_Id = new SqlParameter();
            //OP_ServiceCentre_Id.ParameterName = "@ServiceCentre_Id";
            //OP_ServiceCentre_Id.DbType = DbType.Int32;
            //OP_ServiceCentre_Id.Direction = ParameterDirection.Output;

            //cmd.Parameters.Add(OP_ServiceCentre_Id);           

            //SqlParameter OP_ServiceCentreCompany_Id = new SqlParameter();
            //OP_ServiceCentreCompany_Id.ParameterName = "@ServiceCentreCompany_Id";
            //OP_ServiceCentreCompany_Id.DbType = DbType.Int32;
            //OP_ServiceCentreCompany_Id.Direction = ParameterDirection.Output;

            //cmd.Parameters.Add(OP_ServiceCentreCompany_Id);

            //SqlParameter OP_ServiceCentre_Name = new SqlParameter();
            //OP_ServiceCentre_Name.ParameterName = "@ServiceCentre_Name";
            //OP_ServiceCentre_Name.DbType = DbType.String;
            //OP_ServiceCentre_Name.Size = 255;
            //OP_ServiceCentre_Name.Direction = ParameterDirection.Output;

            //cmd.Parameters.Add(OP_ServiceCentre_Name);           

            cmd.ExecuteNonQuery();
            con.Close();

            Result = Convert.ToInt32(OP_Result.Value.ToString());

            if (Result == 1)
            {
                Role_Id = Convert.ToInt32(OP_Role_Id.Value.ToString());
                Employee_Id = Convert.ToInt32(OP_Employee_Id.Value.ToString());
                Is_SuperAdmin = Convert.ToInt32(OP_IsSuperAdmin.Value.ToString());
                Employee_Name = OP_Employee_Name.Value.ToString();
                Email = OP_Email.Value.ToString();
                Mobile = OP_Contact_Mobile.Value.ToString();
                User_Id = Convert.ToInt32(OP_UserId.Value.ToString());
                UserImgPath = OP_UserImg.Value.ToString();
                //ServiceCentre_Id = Convert.ToInt32(OP_ServiceCentre_Id.Value.ToString());
                //ServiceCentreCompany_Id = Convert.ToInt32(OP_ServiceCentreCompany_Id.Value.ToString());
                //Create session and call home page
                Session["LoginEmployee_Id"] = Employee_Id;
                Session["LoginRole_Id"] = Role_Id;
                Session["LoginUsername"] = UserName;
                Session["LoginAuthenticated"] = "Yes";
                Session["LoginIs_SuperAdmin"] = Is_SuperAdmin;
                Session["LoginEmpName"] = Employee_Name;
                Session["LoginEmpEmail"] = Email;
                Session["LoginEmpMobile"] = Mobile;
                Session["LoginUserId"] = User_Id;
                Session["UserImgPath"] = UserImgPath;
                //Session["ServiceCentre_Id"] = ServiceCentre_Id;
                //Session["ServiceCentreCompany_Id"] = ServiceCentreCompany_Id;
                //Session["Customer_Id_ToUpdate"] = 0;
                //Session["ServiceCentre_Name"] = OP_ServiceCentre_Name.Value.ToString();
                //Session["Password"] = Password;
                //Response.Redirect(@"~\Home.aspx");

                //////con.Open();
                //////cmd.CommandText = "USP_GetSMSConfiguration";
                //////cmd.Connection = con;
                //////cmd.CommandType = CommandType.StoredProcedure;
                //////cmd.Parameters.AddWithValue("@ServiceCentre_Id", ServiceCentre_Id);
                //////dr = cmd.ExecuteReader();
                //////List<CS_SMSConfiguration> SMSConfigList = new List<CS_SMSConfiguration>();
                //////while (dr.Read())
                //////{
                //////    SMSConfigList.Add(new CS_SMSConfiguration() { 
                //////        SMSConfigurationId=Convert.ToInt32(dr["SMSConfigurationId"].ToString()),
                //////        ServiceCentre_Id = Convert.ToInt32(dr["ServiceCentre_Id"].ToString()),
                //////        APIUrl = dr["APIUrl"].ToString(),
                //////        UserName = dr["UserName"].ToString(),
                //////        UserPassword = dr["UserPassword"].ToString(),
                //////        SenderUserName = dr["SenderUserName"].ToString(),
                //////        RouteType = Convert.ToInt32(dr["RouteType"].ToString()),
                //////    });
                //////}
                //////con.Close();

                //////Session["SMS_Config"] = SMSConfigList;


                con.Open();
                cmd.Dispose();
                cmd = new SqlCommand();
                cmd.CommandText = "USP_GetUserRights";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Authentication_Id", User_Id);
                cmd.Parameters.AddWithValue("@Is_SuperAdmin", Is_SuperAdmin);
                dr = cmd.ExecuteReader();
                List<CS_UserWiseRights> UserWiseRights = new List<CS_UserWiseRights>();
                while (dr.Read())
                {
                    UserWiseRights.Add(new CS_UserWiseRights()
                    {
                        Employee_Id = Convert.ToInt32(dr["Employee_Id"].ToString()),
                        User_Id = Convert.ToInt32(dr["User_Id"].ToString()),
                        ProcessMaster_Id = Convert.ToInt32(dr["ProcessMaster_Id"].ToString()),
                        Process_Name = dr["Process_Name"].ToString(),
                        Right_Type_Id = Convert.ToInt32(dr["Right_Type_Id"].ToString()),
                        Remark = dr["Remark"].ToString(),
                        Userwise_Right_Id = Convert.ToInt32(dr["Userwise_Right_Id"].ToString()),
                        Is_Applicable = Convert.ToInt32(dr["Is_Applicable"].ToString()),
                    });
                }
                con.Close();

                Session["UserWiseRightsList"] = UserWiseRights;

                Response.Redirect(@"~\Home.aspx");
                //Session.Abandon();
                //Session.RemoveAll();
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