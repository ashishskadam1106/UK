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
using CS_Encryption;

public partial class Form_PaymentTemplateAddUpdate : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    SqlDataReader dr;
    DataSet ds;

    string UserName = "";
    int Employee_Id;


    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);
    public List<CS_PaymentToApplicable> List_PaymentToApplicable { get { return (List<CS_PaymentToApplicable>)Session["CS_PaymentToApplicable"]; } set { Session["CS_PaymentToApplicable"] = value; } }
    public List<CS_FeeGenerationTypeReference> List_FeeGenerationTypeReference { get { return (List<CS_FeeGenerationTypeReference>)Session["CS_FeeGenerationTypeReference"]; } set { Session["CS_FeeGenerationTypeReference"] = value; } }
    int PaymentTemplateId_ToUpdate { get { return (int)ViewState["PaymentTemplateId_ToUpdate"]; } set { ViewState["PaymentTemplateId_ToUpdate"] = value; } }
    
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
                List_PaymentToApplicable = new List<CS_PaymentToApplicable>();
                List_FeeGenerationTypeReference = new List<CS_FeeGenerationTypeReference>();
                
                if (Request.QueryString.Count > 0)
                {
                    PaymentTemplateId_ToUpdate = Convert.ToInt32(CS_Encrypt.Decrypt(Request.QueryString["ID"].ToString()));
                }
                else
                {
                    PaymentTemplateId_ToUpdate = 0;
                }

                Fill_FeeGenerationType();
                Fill_PaymentTo();
                //if (MaterialMasterId_ToUpdate != 0)
                //{
                //    Fill_MaterialDetails(MaterialMasterId_ToUpdate);
                //}
            }
        }
    }

    private void Fill_PaymentTo()
    {
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "select PaymentToId,PaymentTo from tbl_PaymentTo Union select 0 PaymentToId,'---Select---' PaymentTo";
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "PaymentTo");
            Dd_PaymentTo.DataSource = ds.Tables["PaymentTo"];
            Dd_PaymentTo.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    private void Fill_FeeGenerationType()
    {
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "select FeeGenerationTypeId,FeeGenerationType from tbl_FeeGenerationType Union select 0 FeeGenerationTypeId,'---Select---' FeeGenerationType";
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "FeeGenerationType");
            Dd_GenerationType.DataSource = ds.Tables["FeeGenerationType"];
            Dd_GenerationType.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_PaymentTemplate.aspx");
    }
    protected void Btn_New_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_PaymentTemplateAddUpdate.aspx");
    }

   
    protected void Dd_PaymentTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPaymentToApplicable();
    }

    private void FillPaymentToApplicable()
    {
        int GenerationTypeId = 0;
        int PaymentToId = 0;
        double Amount = 0;
        if (Dd_PaymentTo.SelectedIndex > 0)
        {
            if (Dd_GenerationType.SelectedIndex > 0)
            {
                GenerationTypeId = Convert.ToInt32(Dd_GenerationType.SelectedValue.ToString());
            }
            if (!String.IsNullOrEmpty(Tb_Amount.Text))
            {
                try
                {
                    double.Parse(Tb_Amount.Text.ToString());
                    Amount = Convert.ToDouble(Tb_Amount.Text.ToString());
                }
                catch
                {
                    Tb_Amount.Text = "0";
                }
            }

            PaymentToId = Convert.ToInt32(Dd_PaymentTo.SelectedValue.ToString());

            List_PaymentToApplicable.Clear();
            List_FeeGenerationTypeReference.Clear();

            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetPaymentToApplicableDetailsForAdd";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@PaymentToId", PaymentToId);
            cmd.Parameters.AddWithValue("@FeeGenerationTypeId", GenerationTypeId);
            dr = cmd.ExecuteReader();

            int Result = 0;
            while (dr.Read())
            {
                Result = Convert.ToInt32(dr["Result"].ToString());
            }
            if (Result == 1)
            {
                dr.NextResult();
                while (dr.Read())
                {
                    List_PaymentToApplicable.Add(new CS_PaymentToApplicable()
                    {
                        PaymentTemplateApplicableToId = Convert.ToInt32(dr["PaymentTemplateApplicableToId"].ToString()),
                        PaymentTemplateId = Convert.ToInt32(dr["PaymentTemplateId"].ToString()),
                        PaymentToId = Convert.ToInt32(dr["PaymentToId"].ToString()),
                        ReferenceId = Convert.ToInt32(dr["ReferenceId"].ToString()),
                        Reference = dr["Reference"].ToString(),

                        AdditionalInformation = dr["AdditionalInformation"].ToString(),
                        InitialFeeGenerationTypeReferenceId = Convert.ToInt32(dr["InitialFeeGenerationTypeReferenceId"].ToString()),
                        InitialFeeGenerationTypeReference = dr["InitialFeeGenerationTypeReference"].ToString(),
                        IsModificationToConsiderForInitialPayment = Convert.ToBoolean(dr["IsModificationToConsiderForInitialPayment"].ToString()),
                        IsActive = Convert.ToBoolean(dr["IsActive"].ToString()),
                        IsActiveDescription = dr["IsActiveDescription"].ToString(),
                        Amount = Amount,
                    });
                }

                if (GenerationTypeId > 0)
                {
                    dr.NextResult();
                    while (dr.Read())
                    {
                        List_FeeGenerationTypeReference.Add(new CS_FeeGenerationTypeReference()
                        {
                            InitialFeeGenerationTypeReferenceId = Convert.ToInt32(dr["InitialFeeGenerationTypeReferenceId"].ToString()),
                            InitialFeeGenerationTypeReference = dr["InitialFeeGenerationTypeReference"].ToString(),
                        });
                    }
                }

                Gv_PaymentTemplateApplicableTo.DataSource = null;
                Gv_PaymentTemplateApplicableTo.DataSource = List_PaymentToApplicable;
                Gv_PaymentTemplateApplicableTo.DataBind();
            }
            else
            {
                Gv_PaymentTemplateApplicableTo.DataSource = null;
                Gv_PaymentTemplateApplicableTo.DataBind();
            }


            con.Close();
        }
        else
        {
            Gv_PaymentTemplateApplicableTo.DataSource = null;
            Gv_PaymentTemplateApplicableTo.DataBind();
        }
    }
    protected void Gv_PaymentTemplateApplicableTo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList DD_InitialFeeGenerationTypeReference = (DropDownList)e.Row.FindControl("DD_InitialFeeGenerationTypeReference");
            HiddenField Hf_InitialFeeGenerationTypeReferenceId = (HiddenField)e.Row.FindControl("Hf_InitialFeeGenerationTypeReferenceId");
            DD_InitialFeeGenerationTypeReference.DataSource = List_FeeGenerationTypeReference;
            DD_InitialFeeGenerationTypeReference.DataBind();
            DD_InitialFeeGenerationTypeReference.SelectedIndex =DD_InitialFeeGenerationTypeReference.Items.IndexOf(DD_InitialFeeGenerationTypeReference.Items.FindByValue(Hf_InitialFeeGenerationTypeReferenceId.Value.ToString()));
        }
    }
    protected void Dd_GenerationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Gv_PaymentTemplateApplicableTo.Rows.Count > 0)
        {
            FillPaymentToApplicable();
        }
    }
    protected void Btn_Save_Click(object sender, EventArgs e)
    {

    }
}