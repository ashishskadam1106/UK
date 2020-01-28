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
using System.IO;
using Microsoft.Reporting.WebForms;
using CS_Encryption;
using System.Security.Cryptography;
using System.Text;

public partial class Reports_Form_PrintPrintCertificate : System.Web.UI.Page
{

    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
    string UserName = "";
    int Employee_Id = 0, CompanyId = 0;
    int StudentId = 0;


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


            if (Request.QueryString.Count > 0)
            {
                try
                {
                    StudentId = Convert.ToInt32(Decrypt(Request.QueryString[0].ToString()));
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid Opration');window.location='Home.aspx'", true);
                }
            }

            if (!IsPostBack)
            {
                Fill_Report();
            }
        }

    }

    private void Fill_Report()
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_PrintCertificate.rdlc");
        DS_PrintCertificatet dsIncidents = GetData();
        ReportDataSource datasource = new ReportDataSource("DS_PrintCertificatet", dsIncidents.Tables[0]);
        //ReportDataSource datasource = new ReportDataSource("DataSet1", dsIncidents.Tables[0]);
        //ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(datasource);
    }

    private DS_PrintCertificatet GetData()
    {
        cmd = new SqlCommand("USP_PrintCertificateForReport");
        using (con = new SqlConnection(str))
        {
            using (da = new SqlDataAdapter(cmd))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StudentId ", StudentId);

                using (DS_PrintCertificatet Receipt = new DS_PrintCertificatet ())
                {
                    da.Fill(Receipt, "USP_PrintCertificateForReport"); //Name of dataTable
                    return Receipt;
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