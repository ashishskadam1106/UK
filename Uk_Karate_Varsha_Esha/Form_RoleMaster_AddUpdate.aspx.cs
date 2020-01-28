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

public partial class Form_RoleMaster_AddUpdate : System.Web.UI.Page
{

    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    SqlDataReader dr;

    string UserName = "";
    int Role_Id_ToUpdate, Employee_Id;
    int ServiceCentre_Id = 0;

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

          //  ServiceCentre_Id = Convert.ToInt32(Session["ServiceCentre_Id"].ToString());
            UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());
            if (Session["RoleId_ToUpdate"].ToString() != "0")
            {
                Role_Id_ToUpdate = Convert.ToInt32(Session["RoleId_ToUpdate"].ToString()); //This role id is to update

            }

            if (!IsPostBack)
            {
                if (Role_Id_ToUpdate != 0)
                {
                    Lbl_Heading.Text = "Update Role";
                    Fill_RoleDetails(Role_Id_ToUpdate);
                }
            }
        }
    }

    private void Fill_RoleDetails(int Role_Id_ToUpdate)
    {
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "select isnull(Role_Name,'') Role_Name, isnull(Remark,'') Remark from tbl_Role where Role_Id=" + Role_Id_ToUpdate;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Tb_RoleName.Text = dr[0].ToString();
                Tb_Description.Text = dr[1].ToString();
            }
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
        finally
        {
            dr.Close();
            con.Close();
        }
    }
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        int Go = 1;
        string RoleName = "", Remark = "";

        RoleName = Tb_RoleName.Text.ToString();
        Remark = Tb_Description.Text.ToString();

        if (Role_Id_ToUpdate == 0)
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            cmd.Transaction = tran;
            try
            {               
                cmd.CommandText = "USP_Role_Insert";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Role_Name", RoleName);
                cmd.Parameters.AddWithValue("@Remark", Remark);
               // cmd.Parameters.AddWithValue("@ServiceCentre_Id", ServiceCentre_Id);
                cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@Created_By", UserName);

                cmd.ExecuteNonQuery();
                tran.Commit();
                con.Close();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Role Created Successfully');window.location='Form_RoleMaster.aspx'", true);
                //Response.Redirect(@"~\Form_RoleMaster.aspx");
                Clear();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
            }
            finally
            {
                con.Close();
            }
        }
        else if (Role_Id_ToUpdate != 0)
        {
            //Update 
            con.ConnectionString = str;
            cmd = new SqlCommand();
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            cmd.Transaction = tran;
            try
            {                
                cmd.CommandText = "USP_Role_Update";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
               
                cmd.Parameters.AddWithValue("@Role_Id", Role_Id_ToUpdate);
                cmd.Parameters.AddWithValue("@Role_Name", RoleName);
                cmd.Parameters.AddWithValue("@Remark", Remark);
               // cmd.Parameters.AddWithValue("@ServiceCentre_Id", ServiceCentre_Id);
                cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@Created_By", UserName);

                cmd.ExecuteNonQuery();
                tran.Commit();
                con.Close();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Role details updated Successfully');window.location='Form_RoleMaster.aspx'", true);
                //Response.Redirect(@"~\Form_RoleMaster.aspx");
                //Clear();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
            }
            finally
            {
                con.Close();
            }
        }
    }
    protected void Btn_New_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        Tb_RoleName.Text="";
 	    Tb_Description.Text="";
    }
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_RoleMaster.aspx");
    }
}