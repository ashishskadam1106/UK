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

public partial class Reports_Form_PrintManageStudentList : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    string UserName;
    int EmployeeId, StudentId;
    DateTime FromDate, ToDate;
    string BeltIds = "", DojoIds = "";
    Boolean IsDorment = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginUsername"] == null || Session["LoginAuthenticated"] == "No")
        {
            Response.Redirect(@"~\Index.aspx");
        }
        else
        {
            UserName = Session["LoginUsername"].ToString();

            EmployeeId = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            if (Request.QueryString.Count > 1)
            {
                try
                {
                    FromDate = Convert.ToDateTime(Decrypt(Request.QueryString["From"]));
                    ToDate = Convert.ToDateTime(Decrypt(Request.QueryString["To"]));
                    BeltIds = Decrypt(Request.QueryString["BeltIds"]);
                    DojoIds = Decrypt(Request.QueryString["DojoIds"]);

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
    private void Fill_Report()
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_PrintManageStudentList.rdlc");
      //  DS_ManageStudent dsIncidents = GetData();
        //ReportDataSource datasource = new ReportDataSource("DS_ManageStudent", dsIncidents.Tables[0]); //name of dataset
        ReportDataSource DS_ManageStudent = new ReportDataSource("DS_ManageStudent", ManageStudent());

        ReportViewer1.LocalReport.DataSources.Clear();
       // ReportViewer1.LocalReport.DataSources.Add(datasource);
        ReportViewer1.LocalReport.DataSources.Add(DS_ManageStudent);
    }


    private DataTable ManageStudent()
    {
        cmd = new SqlCommand("UPS_GetStudentDetails");
        using (con = new SqlConnection(str))
        {
            using (da = new SqlDataAdapter(cmd))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@BeltIds", BeltIds);
                cmd.Parameters.AddWithValue("@DojoIds", DojoIds);
                cmd.Parameters.AddWithValue("@IsDorment", IsDorment);

                using (DataTable Bill = new DataTable())
                {
                    da.Fill(Bill); //Name of dataTable
                    return Bill;
                }
            }
        }
    }

    //private DS_ManageStudent GetData()
    //{
    //    cmd = new SqlCommand("UPS_GetStudentDetails");
    //    using (con = new SqlConnection(str))
    //    {
    //        using (da = new SqlDataAdapter(cmd))
    //        {
    //            cmd.Connection = con;
    //            cmd.CommandType = CommandType.StoredProcedure;

    //            cmd.Parameters.AddWithValue("@FromDate", FromDate);
    //            cmd.Parameters.AddWithValue("@ToDate", ToDate);
    //            cmd.Parameters.AddWithValue("@BeltIds", BeltIds);
    //            cmd.Parameters.AddWithValue("@DojoIds", DojoIds);
    //            cmd.Parameters.AddWithValue("@IsDorment", IsDorment);


    //            using (DS_ManageStudent Bill = new DS_ManageStudent())
    //            {
    //                da.Fill(Bill, "UPS_GetStudentDetails"); //Name of dataTable
    //                return Bill;
    //            }
    //        }
    //    }
    //}
}