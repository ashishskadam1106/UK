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
using System.Drawing;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public partial class Home : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    int ServiceCentre_Id, Employee_Id;
    string UserName;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginUsername"] == null || Session["LoginAuthenticated"] == "No")
        {
            Response.Redirect(@"~\Index.aspx");
        }
        else
        {
            UserName = Session["LoginUsername"].ToString();
            //ServiceCentre_Id = Convert.ToInt32(Session["ServiceCentre_Id"].ToString());
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
            if (!IsPostBack)
            {
                Fill_Rent();
                //Fill_Recent_Grids(1);
            }
        }
    }

    private void Fill_Rent()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetRentDetailsForDashbord";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
          
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "JobCards");

            Gv_Rent.DataSource = ds.Tables["JobCards"];
            Gv_Rent.DataBind();


            decimal total = ds.Tables["JobCards"].AsEnumerable().Sum(row => row.Field<decimal>("Balance"));
            Gv_Rent.FooterRow.Cells[2].Text = "Total Due";
            Gv_Rent.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            Gv_Rent.FooterRow.Cells[2].Font.Bold = true;
            Gv_Rent.FooterRow.Cells[3].Text = total.ToString("N2");
            Gv_Rent.FooterRow.Cells[3].Font.Bold = true;



            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected void Gv_Rent_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void Gv_Rent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}