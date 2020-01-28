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

public partial class Form_RoleMaster : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

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

           // ServiceCentre_Id = Convert.ToInt32(Session["ServiceCentre_Id"].ToString());
            if (!IsPostBack)
            {
                Fill_RoleMaster();
            }
        }
    }
    private void Fill_RoleMaster()
    {
        // throw new NotImplementedException();
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "select Role_Id,Role_Name,ISNULL(Remark,'') 'Remark' from tbl_Role where Is_SuperAdmin=0";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "RoleMaster");
            Gv_RoleMaster.DataSource = ds.Tables["RoleMaster"];
            Gv_RoleMaster.DataBind();
            con.Close();
            //Gv_SiteMaster.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
            //Gv_SiteMaster.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            //Gv_SiteMaster.HeaderRow.Cells[5].Attributes["data-class"] = "phone";
            //Gv_SiteMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Gv_RoleMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        int RoleId_ToUpdate = Convert.ToInt32(Gv_RoleMaster.SelectedValue.ToString());
        Session["RoleId_ToUpdate"] = RoleId_ToUpdate;
        Response.Redirect(@"~\Form_RoleMaster_AddUpdate.aspx");
    }
    protected void Btn_AddRole_Click(object sender, EventArgs e)
    {
        Session["RoleId_ToUpdate"] = 0;
        Response.Redirect(@"~\Form_RoleMaster_AddUpdate.aspx");
    }
    protected void Gv_RoleMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_RoleMaster.PageIndex = e.NewPageIndex;
        Fill_RoleMaster();
    }
}