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
using System.IO;

public partial class Form_UserwiseRights : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd, cmd2;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    int RightTypeId;
    string UserName = "";
    int Employee_Id = 0;
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

            //ServiceCentre_Id = Convert.ToInt32(Session["ServiceCentre_Id"].ToString());
            UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            if (!Page.IsPostBack)
            {
                Fill_Employee();
                //     File_MenuMaster();
                Fill_RightType();
                if (Dd_RightType.Items.Count > 1)
                {
                    Dd_RightType.SelectedIndex = 1;
                    Fill_Rights();
                }
                Chk_User.ClearSelection();
                Chk_User.ClearSelection();
            }
        }
    }


    #region Employee
    private void Fill_Employee()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "select E.Employee_Id,isnull(E.FirstName,'')+' '+isnull(E.MiddleName,'')+' '+isnull(E.LastName,'') +' ['+LTRIM(Rtrim(R.Role_Name))+']' Employee_Name,E.Employee_Status_Id,A.Authentication_Id from tbl_Employee E  inner join tbl_Authentication A on e.Employee_Id=A.Reference_Id and A.LoginCategory_Id=1 inner join tbl_Role R on E.Role_Id=R.Role_Id  where Employee_Status_Id<>0 and Is_Default=0";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Employee_Name");
            DataTable dt = new DataTable();
            da.Fill(dt);
            Chk_User.DataSource = dt;
            Chk_User.DataBind();
            // Gv_RoleMaster.DataSource = ds.Tables["RoleName"];
            // Gv_RoleMaster.DataBind();

            con.Close();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    #endregion

    #region Right Type
    private void Fill_RightType()
    {

        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "select Right_Type_Id,Right_Name from tbl_Right_Type where RightMode_Id=2 union Select 0 Right_Type_Id,'---Select---' Right_Name";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Fill(ds, "RightName");
            Dd_RightType.DataSource = dt;
            Dd_RightType.DataBind();
            // Gv_MenuList.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    #endregion

    protected void Dd_RightType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_Rights();
    }

    private void Fill_Rights()
    {
        try
        {
            int RightTypeId = Convert.ToInt32(Dd_RightType.SelectedValue.ToString());

            if (RightTypeId == 2)
            {
                try
                {
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    cmd.CommandText = "Select ProcessMaster_Id,Process_Name,Right_Type_Id,Process_Name+' - '+Remark as Remark from tbl_ProcessMaster where Right_Type_Id=2";
                    cmd.Connection = con;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Fill(ds, "Process_Name");
                    Chk_ProcessMaster.DataSource = ds.Tables["Process_Name"];
                    Chk_ProcessMaster.DataBind();

                    Chk_RightsDescription.DataSource = ds.Tables["Process_Name"];
                    Chk_RightsDescription.DataBind();
                    // Gv_MenuList.DataBind();
                    con.Close();

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                }
            }
            else if(RightTypeId==0)
            {
                con.ConnectionString = str;
                con.Open();
                cmd = new SqlCommand();
                cmd.CommandText = "Select 0 ProcessMaster_Id,'' Process_Name,0 Right_Type_Id from tbl_ProcessMaster where Right_Type_Id=0";
                cmd.Connection = con;
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Fill(ds, "Process_Name");
                Chk_ProcessMaster.DataSource = ds.Tables["Process_Name"];
                Chk_ProcessMaster.DataBind();
                // Gv_MenuList.DataBind();
                con.Close();
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        int UserId = 0;// Convert.ToInt32(Chk_RoleMaster.SelectedValue.ToString());
        int RightType_Id = Convert.ToInt32(Dd_RightType.SelectedValue.ToString());
        //int Process_Id = Convert.ToInt32(Chk_ProcessMaster.Items.ToString());
        int Right_Id;
        int Is_Applicable = 0;

        for (int i = 0; i < Chk_User.Items.Count; i++)
        {
            if (Chk_User.Items[i].Selected)
            {
                UserId = Convert.ToInt32(Chk_User.Items[i].Value.ToString());
                for (int j = 0; j < Chk_ProcessMaster.Items.Count; j++)
                {                    
                    Right_Id = Convert.ToInt32(Chk_ProcessMaster.Items[j].Value);
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    SqlTransaction tran = con.BeginTransaction();
                    cmd.Transaction = tran;
                    try
                    {
                        if (Chk_ProcessMaster.Items[j].Selected)
                        {
                            Is_Applicable = 1;
                        }
                        else
                        {
                            Is_Applicable = 0;
                        }
                       
                        cmd.Connection = con;
                        cmd.CommandText = "USP_UserwiseRights_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", UserId); //AuthenticationID of employee from authentication table
                        cmd.Parameters.AddWithValue("@RightType_Id", RightType_Id);
                        cmd.Parameters.AddWithValue("@Right_Id", Right_Id);
                        cmd.Parameters.AddWithValue("@Is_Applicable", Is_Applicable);

                        //cmd.Parameters.AddWithValue("@ServiceCentre_Id", ServiceCentre_Id);
                        //cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now);
                        //cmd.Parameters.AddWithValue("@Created_By", UserName);
                        cmd.ExecuteNonQuery();
                        tran.Commit();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        con.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error Message", "alert('" + ex.Message + "')", true);
                    }                   
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Rights saved successfully');window.location='Form_UserwiseRights.aspx'", true);
        }
    }

    protected void Btn_Cancel_Click(object sender, EventArgs e)
    {
        Chk_User.ClearSelection();
        Chk_User.ClearSelection();
        Dd_RightType.SelectedIndex = 0;
    }

   
    protected void Chk_User_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Dd_RightType.SelectedIndex > 0)
        {
            try
            {
                Chk_ProcessMaster.ClearSelection();
                for (int i = 0; i < Chk_User.Items.Count; i++)
                {
                    if (Chk_User.Items[i].Selected)
                    {
                        con.ConnectionString = str;
                        con.Open();
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "Select Right_Id,Is_Applicable from tbl_Userwise_Right where UserId='" + Chk_User.SelectedValue.ToString() + "'";
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            int Is_ApplicableId = Convert.ToInt32(dr[1].ToString());
                            if (Is_ApplicableId == 1)
                            {
                                //  Chk_RoleMaster.SelectedValue = true;
                                Chk_ProcessMaster.Items.FindByValue(dr[0].ToString()).Selected = true;
                            }
                        }
                        dr.Close();
                        con.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured:" + ex.Message.ToString() + "')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please select right type')", true);
        }


    }
}