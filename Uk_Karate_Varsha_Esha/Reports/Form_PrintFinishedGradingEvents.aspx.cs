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

public partial class Reports_Form_PrintFinishedGradingEvents : System.Web.UI.Page
{


    string str = ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    string UserName;
    string EventLabel;
    int EventHeaderId, EmployeeId, StudentId;
    DateTime EventDate;

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

            try
            {
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






        private void Fill_Report()
    {
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_PrintFinishedGradingEvent.rdlc");
        DS_FinishedGradingEvent dsIncidents = GetData();
        ReportDataSource datasource = new ReportDataSource("DS_FinishedGradingEvent", dsIncidents.Tables[0]); //name of dataset

        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(datasource);
        
    }

        private DS_FinishedGradingEvent GetData()
    {

        cmd = new SqlCommand("USP_Report_FinishedGradingEvent");
        using (con = new SqlConnection(str))
        {
            using (da = new SqlDataAdapter(cmd))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@EventHeaderId ",EventHeaderId);
                //cmd.Parameters.AddWithValue("@EventLabel",EventLabel);
                //cmd.Parameters.AddWithValue("@EventDate",EventDate);

                using (DS_FinishedGradingEvent grade = new DS_FinishedGradingEvent())
                {
                    da.Fill(grade, "USP_Report_FinishedGradingEvent"); //Name of dataTable
                    return grade;
                }
            }
        }
      
    }
}

  