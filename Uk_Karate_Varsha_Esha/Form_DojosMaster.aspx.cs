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

public partial class Form_DojosMaster : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    SqlDataReader dr;
    DataSet ds;

    string UserName = "";
    int Employee_Id;

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

        }
        if (!IsPostBack)
        {
            Fill_DojoMaster();
        }

    }

    private void Fill_DojoMaster()
    {
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "USP_Get_Dojos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "DojoMaster");
            dr = cmd.ExecuteReader();
            Gv_DojoMaster.DataSource = ds.Tables["DojoMaster"];
            Gv_DojoMaster.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occure:" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Btn_AddRole_Click(object sender, EventArgs e)
    {
        Session["DojoId_ToUpdate"] = 0;
        Response.Redirect("Form_DojosAddUpdate.aspx");
    }
    protected void Gv_DojoMaster_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Gv_DojoMaster.SelectedIndex >= 0)
        {
            int DojoId_ToUpdate = Convert.ToInt32(Gv_DojoMaster.SelectedValue.ToString());
            Session["DojoId_ToUpdate"] = DojoId_ToUpdate;
            Response.Redirect(@"~\Form_DojosAddUpdate.aspx");
        }
    }
}