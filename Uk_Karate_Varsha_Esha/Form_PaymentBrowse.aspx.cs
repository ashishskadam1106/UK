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


public partial class Form_PaymentBrowse : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    int Employee_Id, ClassId;
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
                Dp_PaymentFromDate.Text = CurrentUtc_IND.Date.ToString().Replace(" 00:00:00", "");
                Dp_PaymentToDate.Text = CurrentUtc_IND.Date.ToString().Replace(" 00:00:00", "");
                Fill_Supplier();
            }
        }
    }

    private void Fill_Supplier()
    {

        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Select SupplierId,SupplierName From tbl_Suppliers Where IsDeleted=0 Union  Select 0,'---Select---'";
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Supplier");
            Dd_Supplier.DataSource = ds.Tables["Supplier"];
            Dd_Supplier.DataBind();
            con.Close();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    protected void Btn_Refresh_Click(object sender, EventArgs e)
    {
        int Go = 1;
        Go = Validate_Dates();
        if (Go == 1)
        {
            int Supplier_Id = Convert.ToInt32(Dd_Supplier.SelectedValue.ToString());
            string From_Date = Dp_PaymentFromDate.Text.ToString();
            string To_Date = Dp_PaymentToDate.Text.ToString();

            Fill_Payments(Supplier_Id, From_Date, To_Date);
        }
    }
    #region Validate_Dates
    private int Validate_Dates()
    {
        int Go = 1;
        string ValidationMsg = "";

        if (Dp_PaymentFromDate.Text.ToString() == "")
        {
            Go = 0;
            Dp_PaymentFromDate.BorderColor = Color.Red;
            ValidationMsg += "Please Enter From Date. /";
        }
        else
        {
            Dp_PaymentFromDate.BorderColor = Color.LightGray;
        }
        if (Dp_PaymentToDate.Text.ToString() == "")
        {
            Go = 0;
            Dp_PaymentToDate.BorderColor = Color.Red;
            ValidationMsg += "Please Enter To Date. /";
        }
        else
        {
            Dp_PaymentToDate.BorderColor = Color.LightGray;
        }
        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :" + ValidationMsg.ToString() + "')", true);
        }
        return Go;
    }
    #endregion
    protected void Gv_Payments_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.ConnectionString = str;
        con.Open();
        cmd = new SqlCommand();
        SqlTransaction tran = con.BeginTransaction();
        cmd.Transaction = tran;
        try
        {
            int PaymentHeader_Id = Convert.ToInt32(Gv_Payments.SelectedValue.ToString());


            cmd.CommandText = "update tbl_PaymentHeader set isCancelled=1 where PaymentHeaderId=" + PaymentHeader_Id;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            tran.Commit();
            con.Close();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

        int Supplier_Id = Convert.ToInt32(Dd_Supplier.SelectedValue.ToString());
        Fill_Payments(Supplier_Id, "01/01/1990", CurrentUtc_IND.Date.ToString().Replace(" 00:00:00", ""));
    }

    #region Fill_Payments
    private void Fill_Payments(int Supplier_IdToSearch, string FromDate, string ToDate)
    {
        DateTime DateFrom, DateTo;
        try
        {
            DateFrom = Convert.ToDateTime(FromDate);
            DateTo = Convert.ToDateTime(ToDate);

            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            string Query = "select ROW_NUMBER() over(order by PH.PaymentHeaderId)SrNo,PH.PaymentHeaderId, S.SupplierName,P.PaymentInstrument,case when PH.ChequeNumber='' then '-' else PH.ChequeNumber end as Cheque_Number,case when PH.ChequeDate='' then '-' else Convert(varchar,PH.ChequeDate,105) end as Cheque_Date,Convert(varchar,PH.PaymentDate,105) Payment_Date,case when PH.BankName='' then '-' else PH.BankName end as Bank_Name,PH.PaymentAmount-ISNULL(PH.ChangeReturnAmount,0)Payment_Amount  from tbl_PaymentHeader PH inner join tbl_Suppliers S on S.SupplierId=PH.ReferenceId and Ph.ReferenceTypeId=1 inner join tbl_PaymentInstruments P on P.PaymentInstrumentId=PH.PaymentModeId  where PH.IsCancelled=0  and " + Supplier_IdToSearch + " in (0,S.SupplierId) and PH.PaymentDate>=@FromDate and  PH.PaymentDate<=@ToDate";
            cmd.CommandText = Query;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@FromDate", DateFrom);
            cmd.Parameters.AddWithValue("@ToDate", DateTo);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Payments");
            if (ds.Tables["Payments"].Rows.Count > 0)
            {
                Gv_Payments.Visible = true;
                Gv_Payments.DataSource = ds.Tables["Payments"];
                Gv_Payments.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('No Data Found')", true);
                Gv_Payments.Visible = false;
            }
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
    #endregion   
}