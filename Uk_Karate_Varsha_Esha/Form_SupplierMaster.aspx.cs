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


public partial class Form_SupplierMaster : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    int Employee_Id, SupplierId_ToUpdate;
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
                Fill_SupplierMaster();
            }
        }
    }

    protected void Btn_AddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_Supplier_AddUpdate.aspx");
    }
    protected void Gv_Suppliers_SelectedIndexChanged(object sender, EventArgs e)
    {
        String Id = Gv_Suppliers.SelectedValue.ToString();
        Response.Redirect(@"~/Form_Supplier_AddUpdate.aspx?ID=" + CS_Encrypt.Encrypt(Id));
    }
    protected void Gv_Suppliers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_Suppliers.PageIndex = e.NewPageIndex;
        Fill_SupplierMaster();
    }
    private void Fill_SupplierMaster()
    {
        string Q = "select ROW_NUMBER() over(order by SupplierId desc)SrNo,SupplierId,SupplierName,SupplierAddress,ContactMobile  from tbl_Suppliers where IsDeleted=0";
        DataSet Ds = CS_Encrypt.GetData(Q, str);
        Gv_Suppliers.DataSource = Ds;
        Gv_Suppliers.DataBind();
    }
}