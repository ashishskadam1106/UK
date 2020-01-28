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

public partial class Form_ClassMaster : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    int  Employee_Id, ClassId;
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
                Fill_ClassMaster();
            }
        }
    }

    private void Fill_ClassMaster()
    {
        /*string Q = "select row_number() over(order by ClassId desc)SrNo,ClassId,ClassCode,Class,isnull(Remark,'')Remark from tbl_Classes where isdeleted=0 and ClassId=" + ClassId;
        DataSet Ds = CS_Encrypt.GetData(Q, str);
        Gv_ClassMaster.DataSource = Ds;
        Gv_ClassMaster.DataBind();*/
         try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetClass";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "ClassMaster");
            dr = cmd.ExecuteReader();
            Gv_ClassMaster.DataSource = ds.Tables["ClassMaster"];
            Gv_ClassMaster.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occure:" + ex.Message.ToString() + "')", true);
        }
    }
    
    protected void Gv_ClassMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_ClassMaster.PageIndex = e.NewPageIndex;
        Fill_ClassMaster();
    }
    protected void Gv_ClassMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Gv_ClassMaster.SelectedIndex >= 0)
        {
            int ClassId_ToUpdate = Convert.ToInt32(Gv_ClassMaster.SelectedValue.ToString());
            Session["ClassId_ToUpdate"] = ClassId_ToUpdate;
            Response.Redirect(@"~\Form_ClassMaster_AddUpdate.aspx");
        }
    }
    protected void Btn_AddNew_Click(object sender, EventArgs e)
    {
        Session["ClassId_ToUpdate"] = 0;
        Response.Redirect(@"~\Form_ClassMaster_AddUpdate.aspx");
    }
}