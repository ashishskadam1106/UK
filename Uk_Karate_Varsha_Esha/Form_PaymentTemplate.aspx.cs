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


public partial class Form_PaymentTemplate : System.Web.UI.Page
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
                Fill_PaymentTemplate();
            }
        }
    }

    protected void Btn_AddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_PaymentTemplateAddUpdate.aspx");
    }
 
    private void Fill_PaymentTemplate()
    {
        string Q = "select  ROW_NUMBER() over(order by PaymentTemplateId desc)SrNo,PaymentTemplateId,PaymentTemplateName,PT.FeeGenerationTypeId,ISNULL(FGT.FeeGenerationType,'') FeeGenerationType,PT.PaymentToId,ISNULL(PTO.PaymentTo,'') PaymentTo,PT.Amount,PT.FromDate,PT.ToDate,PT.IsActive, Case when PT.IsActive=0 then 'No' else 'Yes' end IsActiveDescription from tbl_PaymentTemplate PT left outer join tbl_FeeGenerationType FGT on PT.FeeGenerationTypeId=FGT.FeeGenerationTypeId left outer join tbl_PaymentTo PTO on PT.PaymentToId=PTO.PaymentToId where PT.IsDeleted=0";
        DataSet Ds = CS_Encrypt.GetData(Q, str);
        Gv_PaymentTemplate.DataSource = Ds;
        Gv_PaymentTemplate.DataBind();
    }
    protected void Gv_PaymentTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_PaymentTemplate.PageIndex = e.NewPageIndex;
        Fill_PaymentTemplate();
    }
    protected void Gv_PaymentTemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        String Id = Gv_PaymentTemplate.SelectedValue.ToString();
        Response.Redirect(@"~/Form_PaymentTemplateAddUpdate.aspx?ID=" + CS_Encrypt.Encrypt(Id));
    }
}