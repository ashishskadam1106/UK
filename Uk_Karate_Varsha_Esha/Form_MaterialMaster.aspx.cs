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


public partial class Form_MaterialMaster : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
     

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

            if (!IsPostBack)
            {
                Fill_MaterialMaster();
            }
        }
    }
    protected void Btn_AddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_Material_AddUpdate.aspx");
    }
    protected void Gv_MaterialMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_MaterialMaster.PageIndex = e.NewPageIndex;
        Fill_MaterialMaster();
    }
    protected void Gv_MaterialMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        String Id = Gv_MaterialMaster.SelectedValue.ToString();
        Response.Redirect(@"~/Form_Material_AddUpdate.aspx?ID=" + CS_Encrypt.Encrypt(Id));
    }
    private void Fill_MaterialMaster()
    {
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetMaterialDetailList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Material");
            dr = cmd.ExecuteReader();
            Gv_MaterialMaster.DataSource = ds.Tables["Material"];
            Gv_MaterialMaster.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occure:" + ex.Message.ToString() + "')", true);
        }
    }

}