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
using CS_Encryption;

public partial class Form_Report : System.Web.UI.Page
{

    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string UserName;
    int CompanyId, Employee_Id, Role_Id;


    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);

    public DateTime FromDate { get { return (DateTime)ViewState["FromDate"]; } set { ViewState["FromDate"] = value; } }
    public DateTime ToDate { get { return (DateTime)ViewState["ToDate"]; } set { ViewState["ToDate"] = value; } }

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

            Role_Id = Convert.ToInt32(Session["LoginRole_Id"].ToString());
            UserName = Session["LoginUsername"].ToString();
            
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            if (!IsPostBack)
            {
                FromDate = CurrentUtc_IND;
                ToDate = CurrentUtc_IND;
                Dp_FromDate.Text = CurrentUtc_IND.ToString("dd/MM/yyyy");
                Dp_ToDate.Text = CurrentUtc_IND.ToString("dd/MM/yyyy");
               
                Fill_Reports();
                Fill_Supplier();
               
            }
        }
    }

    private void Fill_Reports()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select Rp.ReportId,Rp.Report from tbl_Reports RP Union select 0 ReportId,'---Select---' Report";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Reports");
            DD_Report.DataSource = ds.Tables["Reports"];
            DD_Report.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    private void Fill_Supplier()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select SupplierId,SupplierName from tbl_Suppliers union Select 0 as SupplierId,'---Select---'SupplierName";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Supplier");
            Dd_SupplierName.DataSource = ds.Tables["Supplier"];
            Dd_SupplierName.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    

    protected void DD_Report_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Report_ID = Convert.ToInt32(DD_Report.SelectedValue);
        if (Report_ID == 1)
        {
            Lbl_SupplierName.Visible = true;
            Dd_SupplierName.Visible = true;
        }
        else
        {
            Lbl_SupplierName.Visible = false;
            Dd_SupplierName.Visible = false;
        }
    }
    protected void Btn_Show_Click(object sender, EventArgs e)
    {
        int Report_Id = 0;
        int Go = 1;

        if (DD_Report.SelectedIndex <= 0)
        {
            Go = 0;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation : Please select report name.')", true);
            DD_Report.BorderColor = Color.Red;
        }
        else { DD_Report.BorderColor = Color.LightGray; }

        if (Go == 1)
        {
            try
            {
                Report_Id = Convert.ToInt32(DD_Report.SelectedValue);
                FromDate = Convert.ToDateTime(Dp_FromDate.Text);
                ToDate = Convert.ToDateTime(Dp_ToDate.Text);

                if (Report_Id == 1)  //Receipt
                {
                    int SupplierId = 0;
                    if (Dd_SupplierName.SelectedIndex >= 0)
                    {
                        SupplierId = Convert.ToInt32(Dd_SupplierName.SelectedValue);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Reports/Form_PrintDateWisePurchase.aspx?From=" + encrypt(FromDate.ToString()) + "&To=" + encrypt(ToDate.ToString()) + "&SupplierId=" + encrypt(SupplierId.ToString()) + "','_newtab');", true);
                }
                else if (Report_Id == 2)  //bill
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Reports/Form_PrintDateWisePayment.aspx?From=" + encrypt(FromDate.ToString()) + "&To=" + encrypt(ToDate.ToString()) + "','_newtab');", true);
                }
                else if (Report_Id == 3)
                {
                    int SupplierId = 0;
                    if (Dd_SupplierName.SelectedIndex >= 0)
                    {
                        SupplierId = Convert.ToInt32(Dd_SupplierName.SelectedValue);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Reports/Form_PrintDateWisePurchase.aspx?From=" + encrypt(FromDate.ToString()) + "&To=" + encrypt(ToDate.ToString()) + "&SupplierId=" + encrypt(SupplierId.ToString()) + "','_newtab');", true);
                }
                else if (Report_Id == 4)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Reports/Form_PrintDateWiseQuotation.aspx?From=" + encrypt(FromDate.ToString()) + "&To=" + encrypt(ToDate.ToString()) + "','_newtab');", true);
                }

                else if (Report_Id == 5)
                {
                    int CustomerId = 0;
                    if (Dd_Customer.SelectedIndex > 0)
                    {
                        Dd_Customer.BorderColor = Color.LightGray;
                        CustomerId = Convert.ToInt32(Dd_Customer.SelectedValue);

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('Reports/Form_PrintAccountTransactionDetails.aspx?From=" + encrypt(FromDate.ToString()) + "&To=" + encrypt(ToDate.ToString()) + "&CustomerId=" + encrypt(CustomerId.ToString()) + "','_newtab');", true);
                    }
                    else
                    {
                        string ValidationMsg = "";
                        Dd_Customer.BorderColor = Color.Red;
                        ValidationMsg += "Please Select Customer Name. /";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :" + ValidationMsg.ToString() + "')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error: " + ex.Message.ToString() + ".')", true);
            }
        }
    }

    public string encrypt(string encryptString)
    {
        string EncryptionKey = "SOFTUKKARATE11245PRITMEGHPAGEISSOFT";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }
}