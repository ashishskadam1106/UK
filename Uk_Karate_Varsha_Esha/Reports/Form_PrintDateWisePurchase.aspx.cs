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
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;

public partial class Reports_Form_PrintDateWisePurchase : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    string UserName;
    int CompanyId, Employee_Id,SupplierId;
    DateTime FromDate, ToDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginUsername"] == null || Session["LoginAuthenticated"] == "No")
        {
            Response.Redirect(@"~\Index.aspx");
        }
        else
        {
            UserName = Session["LoginUsername"].ToString();
           
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            if (Request.QueryString.Count > 1)
            {
                try
                {
                    FromDate = Convert.ToDateTime(Decrypt(Request.QueryString["From"]));
                    ToDate = Convert.ToDateTime(Decrypt(Request.QueryString["To"]));
                    SupplierId = Convert.ToInt32(Decrypt(Request.QueryString["SupplierId"]));
                    if (!IsPostBack)
                    {
                        Fill_Report();
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid Operation');window.location='Form_Report.aspx'", true);
                }
            }

        }
    }

    private void Fill_Report()
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_DateWisePurchase.rdlc");
        DS_DateWisePurchase dsIncidents = GetData();
        ReportDataSource datasource = new ReportDataSource("DS_DateWisePurchase", dsIncidents.Tables[0]); //name of dataset

        ReportParameter Par1 = new ReportParameter("FromDate", FromDate.ToString());
        this.ReportViewer1.LocalReport.SetParameters(Par1);
        ReportParameter Par2 = new ReportParameter("ToDate", ToDate.ToString());
        this.ReportViewer1.LocalReport.SetParameters(Par2);
  
        
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(datasource);
    }

    private DS_DateWisePurchase GetData()
    {
        cmd = new SqlCommand("USP_Report_DatewisePurchase");
        using (con = new SqlConnection(str))
        {
            using (da = new SqlDataAdapter(cmd))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
                using (DS_DateWisePurchase Bill = new DS_DateWisePurchase())
                {
                    da.Fill(Bill, "USP_Report_DatewisePurchase"); //Name of dataTable
                    return Bill;
                }
            }
        }
    }

    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "SOFTUKKARATE11245PRITMEGHPAGEISSOFT";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
}