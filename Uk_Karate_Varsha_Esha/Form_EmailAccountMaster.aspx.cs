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

public partial class Form_EmailAccountMaster : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    int Employee_Id = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginUsername"] == null || Session["LoginAuthenticated"] == "No")
        {
            Response.Redirect(@"~\Index.aspx");
        }
        else
        {
            //UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());            
           

            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
                Fill_EmailAccounts();
            }
        }
    }

    private void Fill_EmailAccounts()
    {
        // throw new NotImplementedException();
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select EmailAccountId,EmailAccountName,EmailId From tbl_EmailAccounts  Where IsDeleted=0";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Email Configuration");
            Gv_EmailAccountMaster.DataSource = ds.Tables["Email Configuration"];
            Gv_EmailAccountMaster.DataBind();
            con.Close();
          
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Btn_Add_Click(object sender, EventArgs e)
    {
        Session["EmailAccountId_ToUpdate"] = 0;
        Response.Redirect(@"~\Form_EmailAccountAddUpdate.aspx");
    }
    protected void Gv_EmailAccountMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_EmailAccountMaster.PageIndex = e.NewPageIndex;
        Fill_EmailAccounts();

      
    }
    protected void Gv_EmailAccountMaster_SelectedIndexChanged(object sender, EventArgs e)
    {

        int EmailAccountId_ToUpdate = Convert.ToInt32(Gv_EmailAccountMaster.SelectedValue.ToString());
        Session["EmailAccountId_ToUpdate"] = EmailAccountId_ToUpdate;
        Response.Redirect(@"~\Form_EmailAccountAddUpdate.aspx");
    }
   
}