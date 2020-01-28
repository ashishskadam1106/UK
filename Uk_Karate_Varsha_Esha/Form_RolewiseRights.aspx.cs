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

public partial class Form_RolewiseRights : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd,cmd2;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    int RightTypeId;
    string UserName = "";
    int Employee_Id = 0;
    int ServiceCentre_Id = 0;

    List<CS_MenuMaster> List_MenuMaster { get { return (List<CS_MenuMaster>)Session["CS_MenuMaster"]; } set { Session["CS_MenuMaster"] = value; } }
    
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
                List_MenuMaster = new List<CS_MenuMaster>();
                Fill_RoleMaster();
           //     File_MenuMaster();
                Fill_RightType();
                Chk_MenuMaster.ClearSelection();
                Chk_MenuMaster.ClearSelection();
                Fill_MenuRight();
            }
        }
       
    }

    #region RightType
    private void Fill_RightType()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "select Right_Type_Id,Right_Name from tbl_Right_Type where RightMode_Id=1 ";//union Select 0 Right_Type_Id,'---Select---' Right_Name
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


   

    //#region MenuMaster
    //private void File_MenuMaster()
    //{
    //    try
    //    {
    //        con.ConnectionString = str;
    //        con.Open();
    //        cmd = new SqlCommand();
    //        cmd.CommandText = "select Menu_Id,Menu_Name from tbl_Menu_Master";
    //        cmd.Connection = con;
    //        da = new SqlDataAdapter(cmd);
    //        ds = new DataSet();
    //        DataTable dt = new DataTable();
    //        da.Fill(dt);
    //        da.Fill(ds, "MenuName");
    //        Chk_MenuMaster.DataSource = dt;
    //        Chk_MenuMaster.DataBind();
    //       // Gv_MenuList.DataBind();
    //        con.Close();

    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
    //    }
    //}
    //#endregion


    #region RoleMaster
    private void Fill_RoleMaster()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "select Role_ID,Role_Name from tbl_Role where Is_SuperAdmin<>1";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "RoleName");
            DataTable dt = new DataTable();
            da.Fill(dt);
            Chk_RoleMaster.DataSource = dt;
            Chk_RoleMaster.DataBind();
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


    //protected void save_Click(object sender, EventArgs e)
    //{
    //    string k = "";
    //    string s = "";
    //    for (int i = 0; i < Chk_MenuMaster.Items.Count; i++)
    //    {
    //        if (Chk_MenuMaster.Items[i].Selected)
    //        {

    //            k = k + Chk_MenuMaster.Items[i].Value.ToString() + "</br>";
    //        }

    //    }

    //    for (int i = 0; i < Chk_RoleMaster.Items.Count; i++)
    //    {

    //        if (Chk_RoleMaster.Items[i].Selected)
    //        {

    //            s = s + Chk_RoleMaster.Items[i].Value.ToString() + "</br>";
    //        }

    //    }

    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('rights :" + k + "'<br/>'role:" + s + "')", true);
    //}
    protected void Dd_RightType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
          //  Chk_MenuMaster.ClearSelection();
            //con.ConnectionString = str;
            //con.Open();
            //cmd = new SqlCommand();
            //cmd.CommandText = "select Right_Type_Id,Right_Name from tbl_Right_Type where Right_Type_Id='" + Dd_RightType.SelectedIndex.ToString() + "'";
            
            //cmd.Connection = con;
            //dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    RightTypeId =Convert.ToInt32(dr[0].ToString());
            //}

            //dr.Close();
            //con.Close();
            int RightTypeId=Convert.ToInt32(Dd_RightType.SelectedValue.ToString());

            if (RightTypeId == 1)
            {
                  try
                  {
                      List_MenuMaster.Clear();
                      Chk_MenuMaster.Items.Clear();
                      con.ConnectionString = str;
                      con.Open();
                      cmd = new SqlCommand();
                      cmd.CommandText = "select Menu_Id,Menu_Name,Parent_Id,convert(float,(case when Parent_Id=0 then CONVERT(varchar,Menu_Id)+'.'+CONVERT(varchar,Parent_Id) else CONVERT(varchar,Parent_Id)+'.'+CONVERT(varchar,Menu_Id) end)) ForOrder from tbl_Menu_Master order by ForOrder";
                      cmd.Connection = con;
                      dr = cmd.ExecuteReader();
                      while (dr.Read())
                      {
                          List_MenuMaster.Add(new CS_MenuMaster() {Menu_Id=Convert.ToInt32(dr[0].ToString()),Menu_Name=dr[1].ToString(),
                          Parent_Id=Convert.ToInt32(dr[2].ToString()),ForOrder=Convert.ToSingle(dr[3].ToString()) });
                      }
                      dr.Close();
                      con.Close();
                      Chk_MenuMaster.DataSource = List_MenuMaster;
                      Chk_MenuMaster.DataBind();

                      //da = new SqlDataAdapter(cmd);
                      //ds = new DataSet();
                      //DataTable dt = new DataTable();
                      //da.Fill(dt);
                      //da.Fill(ds, "MenuName");
                      //Chk_MenuMaster.DataSource = ds.Tables["MenuName"];
                      //Chk_MenuMaster.DataBind();
                      

                  }
                  catch (Exception ex)
                  {
                      ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                  }
            }
            else if (RightTypeId == 2)
            {
                //Report List
                //Chk_MenuMaster.Visible = false;
                try
                {
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    //Replace the query for respective report menus
                    cmd.CommandText = "select Report_Id as Menu_Id,Report_Name  as Menu_Name from tbl_Report";
                    cmd.Connection = con;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Fill(ds, "ReportMenu");
                    Chk_MenuMaster.DataSource = ds.Tables["ReportMenu"];
                    Chk_MenuMaster.DataBind();
                    // Gv_MenuList.DataBind();
                    con.Close();

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                }
            }
            else
            {
                try
                {
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    //Replace the query for respective report menus
                    cmd.CommandText = "select Menu_Id,Menu_Name from tbl_Menu_Master where 1=2";
                    cmd.Connection = con;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Fill(ds, "ReportMenu");
                    Chk_MenuMaster.DataSource = ds.Tables["ReportMenu"];
                    Chk_MenuMaster.DataBind();
                    // Gv_MenuList.DataBind();
                    con.Close();

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Btn_Cancel_Click(object sender, EventArgs e)
    {
        Chk_MenuMaster.ClearSelection();
        Chk_RoleMaster.ClearSelection();
        Dd_RightType.SelectedIndex = 0;
    }
    protected void Btn_Save_Click(object sender, EventArgs e)
    {

        int Role_Id = 0;// Convert.ToInt32(Chk_RoleMaster.SelectedValue.ToString());
        int RightType_Id = Convert.ToInt32(Dd_RightType.SelectedValue.ToString());
        //int Menu_Id = Convert.ToInt32(Chk_MenuMaster.Items.ToString());
        int Menu_Id;
        int Is_Applicable = 0;

        int IsRoleSelected = 0;
        for (int i = 0; i < Chk_RoleMaster.Items.Count; i++)
        {
            if (Chk_RoleMaster.Items[i].Selected)
            {
                IsRoleSelected = 1;
                break;
            }
        }
        if (IsRoleSelected == 1)
        {
            for (int i = 0; i < Chk_RoleMaster.Items.Count; i++)
            {

                if (Chk_RoleMaster.Items[i].Selected)
                {
                    Role_Id = Convert.ToInt32(Chk_RoleMaster.Items[i].Value.ToString());
                    for (int j = 0; j < Chk_MenuMaster.Items.Count; j++)
                    {
                        // if (Chk_MenuMaster.Items[j].Selected)
                        //  {
                        Menu_Id = Convert.ToInt32(Chk_MenuMaster.Items[j].Value);

                        con.ConnectionString = str;
                        con.Open();
                        cmd = new SqlCommand();
                        SqlTransaction tran = con.BeginTransaction();
                        cmd.Transaction = tran;
                        try
                        {
                            if (Chk_MenuMaster.Items[j].Selected)
                            {
                                Is_Applicable = 1;
                            }
                            else
                            {
                                Is_Applicable = 0;
                            }
                   
                            cmd.Connection = con;
                            cmd.CommandText = "USP_RolewiseRights_InsertUpdate";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Role_Id", Role_Id);
                            cmd.Parameters.AddWithValue("@RightType_Id", RightType_Id);
                            cmd.Parameters.AddWithValue("@Right_Id", Menu_Id);
                            cmd.Parameters.AddWithValue("@Is_Applicable", Is_Applicable);
                            //cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now);
                            //cmd.Parameters.AddWithValue("@Created_By", UserName);
                            //cmd.Parameters.AddWithValue("@ServiceCentre_Id", ServiceCentre_Id);
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
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Rights saved successfully');window.location='Form_RolewiseRights.aspx'", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please select role to save rights')", true);
        }
    }

    protected void Fill_MenuRight()
    {
        try
        {
            int RightTypeId = Convert.ToInt32(Dd_RightType.SelectedValue.ToString());

            if (RightTypeId == 1)
            {
                try
                {
                    List_MenuMaster.Clear();
                    Chk_MenuMaster.Items.Clear();
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    cmd.CommandText = "select Menu_Id,Menu_Name,Parent_Id,convert(float,(case when Parent_Id=0 then CONVERT(varchar,Menu_Id)+'.'+CONVERT(varchar,Parent_Id) else CONVERT(varchar,Parent_Id)+'.'+CONVERT(varchar,Menu_Id) end)) ForOrder from tbl_Menu_Master order by ForOrder";
                    cmd.Connection = con;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        List_MenuMaster.Add(new CS_MenuMaster()
                        {
                            Menu_Id = Convert.ToInt32(dr[0].ToString()),
                            Menu_Name = dr[1].ToString(),
                            Parent_Id = Convert.ToInt32(dr[2].ToString()),
                            ForOrder = Convert.ToSingle(dr[3].ToString())
                        });
                    }
                    dr.Close();
                    con.Close();
                    Chk_MenuMaster.DataSource = List_MenuMaster;
                    Chk_MenuMaster.DataBind();

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                }
            }
            else if (RightTypeId == 2)
            {
                //Report List
                //Chk_MenuMaster.Visible = false;
                try
                {
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    //Replace the query for respective report menus
                    cmd.CommandText = "select Report_Id as Menu_Id,Report_Name  as Menu_Name from tbl_Report";
                    cmd.Connection = con;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Fill(ds, "ReportMenu");
                    Chk_MenuMaster.DataSource = ds.Tables["ReportMenu"];
                    Chk_MenuMaster.DataBind();
                    // Gv_MenuList.DataBind();
                    con.Close();

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                }
            }
            else
            {
                try
                {
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    //Replace the query for respective report menus
                    cmd.CommandText = "select Menu_Id,Menu_Name from tbl_Menu_Master where 1=2";
                    cmd.Connection = con;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Fill(ds, "ReportMenu");
                    Chk_MenuMaster.DataSource = ds.Tables["ReportMenu"];
                    Chk_MenuMaster.DataBind();
                    // Gv_MenuList.DataBind();
                    con.Close();

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Chk_RoleMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Dd_RightType.SelectedIndex >= 0)
        {
            int RightType_Id = Convert.ToInt32(Dd_RightType.SelectedValue.ToString());
            try
            {
                Chk_MenuMaster.ClearSelection();
                for (int i = 0; i < Chk_RoleMaster.Items.Count; i++)
                {
                    if (Chk_RoleMaster.Items[i].Selected)
                    {
                        con.ConnectionString = str;
                        con.Open();
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "Select Right_Id,Is_Applicable from tbl_Rolewise_Right where Right_Type_Id="+RightType_Id+" and Role_Id='" + Chk_RoleMaster.SelectedValue.ToString() + "'";
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            int Is_ApplicableId = Convert.ToInt32(dr[1].ToString());
                            if (Is_ApplicableId == 1)
                            {
                                //  Chk_RoleMaster.SelectedValue = true;
                                Chk_MenuMaster.Items.FindByValue(dr[0].ToString()).Selected = true;
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
    protected void Chk_MenuMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Test')", true);
        int RightType_Id = Convert.ToInt32(Dd_RightType.SelectedValue.ToString());
        if (RightType_Id == 1)
        {
            for (int a = 0; a < Chk_MenuMaster.Items.Count; a++)
            {
                int SelectedIndex = a;// Chk_MenuMaster.Items.SelectedIndex;
                int Parent_Id = List_MenuMaster[SelectedIndex].Parent_Id;
                if (Parent_Id != 0)
                {
                    int ParentIndex = List_MenuMaster.FindIndex(x => x.Menu_Id == Parent_Id);

                    int IsChildTick = 0;
                    //Check if there is any child item of parent is checked or not
                    for (int i = 0; i < List_MenuMaster.Count; i++)
                    {
                        if (List_MenuMaster[i].Parent_Id == Parent_Id)
                        {
                            if (Chk_MenuMaster.Items[i].Selected)
                            {
                                IsChildTick = 1;
                                break;
                            }
                        }
                    }
                    if (IsChildTick == 1)
                    {
                        Chk_MenuMaster.Items[ParentIndex].Selected = true;
                    }
                    else
                    {
                        Chk_MenuMaster.Items[ParentIndex].Selected = false;
                    }
                }
            }
        }
    }
}