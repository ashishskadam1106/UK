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


public partial class Form_FeeCategoryMaster : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    int Employee_Id, ClassId;
    string UserName;
    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);

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

            UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());
            if (!IsPostBack)
            {
                Fill_FeeCategoryMaster();
            }
        }
    }
    private void Fill_FeeCategoryMaster()
    {
        
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetFeeCategoryList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "ClassMaster");
            dr = cmd.ExecuteReader();
            Gv_FeeCategoryMaster.DataSource = ds.Tables["ClassMaster"];
            Gv_FeeCategoryMaster.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occure:" + ex.Message.ToString() + "')", true);
        }
    }
    
    protected void Gv_FeeCategoryMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_FeeCategoryMaster.PageIndex = e.NewPageIndex;
        Fill_FeeCategoryMaster();

    }
    protected void Gv_FeeCategoryMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Gv_FeeCategoryMaster.SelectedIndex >= 0)
        {
            int FeeCategoryIdToUpdate = Convert.ToInt32(Gv_FeeCategoryMaster.SelectedValue.ToString());
            Session["FeeCategoryIdToUpdate"] = FeeCategoryIdToUpdate;
            Response.Redirect(@"~\Form_FeeCategory_AddUpdate.aspx");
        }
    }
    protected void Btn_AddNew_Click(object sender, EventArgs e)
    {
        Session["FeeCategoryIdToUpdate"] = 0;
        Response.Redirect(@"~\Form_FeeCategory_AddUpdate.aspx");
    }
}