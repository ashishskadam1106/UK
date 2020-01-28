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

using System.Text;
using System;
//using System.Net.Http;
using System.Web;
using System.Net;
using System.IO;


public partial class Schedules_Form_DailyAlert : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ExecuteDailyAlert();
        ExecuteSendSMS();
    }
    private void ExecuteDailyAlert()
    {
        con.ConnectionString = str;
        con.Open();
        cmd = new SqlCommand();
        cmd.CommandText = "select ServiceCentre_Id from tbl_ServiceCentre where Is_Active=1 and ServiceCentreCompany_Id in ( select ServiceCentreCompany_Id from tbl_ServiceCentreCompany where Is_Active=1)";
        cmd.Connection = con;
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);        
        con.Close();

        foreach (DataRow Row in ds.Tables[0].Rows) 
        {
            int ServiceCentre_Id = Convert.ToInt32( Row[0].ToString());

            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "USP_DailyAlert";            
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@ServiceCentre_Id", ServiceCentre_Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }                    
    }


    private void ExecuteSendSMS()
    {
        con.ConnectionString = str;
        con.Open();
        cmd = new SqlCommand();
        cmd.CommandText = "select AlertId,ServiceCentre_Id from tbl_Alert where  SMSStatusId=0 and IsSMSApplicable=1 and ServiceCentre_Id in (select ServiceCentre_Id from tbl_ServiceCentre where Is_Active=1)";
        cmd.Connection = con;
        dr = cmd.ExecuteReader();
        List<CS_SMSAlertExecution> SMSAlertList = new List<CS_SMSAlertExecution>();
        while (dr.Read())
        {
            SMSAlertList.Add(new CS_SMSAlertExecution() { 
                AlertId=Convert.ToInt32(dr["AlertId"].ToString()),
                ServiceCentre_Id = Convert.ToInt32(dr["ServiceCentre_Id"].ToString()),
            });
        }
        dr.Close();
        con.Close();
        con.Dispose();
        dr.Dispose();
        cmd.Dispose();
        if (SMSAlertList.Count > 0)
        { 
            for(int i=0;i<SMSAlertList.Count;i++)
            {
                SendSMS(SMSAlertList[i].AlertId);
            }
        }
    }

    private void SendSMS(int Alert_Id)
    {
        try
        {
            //List<CS_SMSConfiguration> SMSConfigList = new List<CS_SMSConfiguration>();
            //SMSConfigList = (List<CS_SMSConfiguration>)Session["SMS_Config"];
            //if (SMSConfigList.Count > 0)
            //{

            //}

            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetDetailsToSendSMS";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            string ToMobile = "", SMSMessage = "", APIUrl = "", UserName = "", UserPassword = "", SenderUserName = "", RouteType = "";
            int ToSend = 0;
            cmd.Parameters.AddWithValue("@Alert_Id", Alert_Id);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ToSend = 1;
                ToMobile = dr["ToMobile"].ToString();
                SMSMessage = dr["SMSMessage"].ToString();
                APIUrl = dr["APIUrl"].ToString();
                UserName = dr["UserName"].ToString();
                UserPassword = dr["UserPassword"].ToString();
                SenderUserName = dr["SenderUserName"].ToString();
                RouteType = dr["RouteType"].ToString();
            }
            if (ToSend == 1)
            {
                String Call = APIUrl + "username=" + UserName + "&password=" + UserPassword + "&mobile=" + ToMobile + "&message=" + SMSMessage + "&sendername=" + SenderUserName + "&routetype=" + RouteType;


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Call);
                request.Method = "POST";
                request.ContentType = "application/json";
                WebResponse webResponse = request.GetResponse();
                //request.ContentLength = DATA.Length;
                //StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                //requestWriter.Write(DATA);
                //requestWriter.Close();
            }
        }
        catch (Exception e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Message not sent to customer. Please contact administrator')", true);
        }
    }
}
