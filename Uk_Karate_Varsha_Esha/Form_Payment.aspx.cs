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
//using System.Net.Http;
using System.Net;
using System.IO;
using CS_Encryption;
using System.Security.Cryptography;
public partial class Form_Payment : System.Web.UI.Page
{

    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
    string UserName = "";
    int Employee_Id = 0, CompanyId = 0;
    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);

    public int ReceiptHeader_Id { get { return (Int32)ViewState["ReceiptHeader_Id"]; } set { ViewState["ReceiptHeader_Id"] = value; } }
    public List<CS_OutstandingBills> List_OutstandingBills { get { return (List<CS_OutstandingBills>)Session["CS_OutstandingBills"]; } set { Session["CS_OutstandingBills"] = value; } }
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
            CompanyId = Convert.ToInt32(Session["CompanyId"]);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
                ReceiptHeader_Id = 0;
                List_OutstandingBills = new List<CS_OutstandingBills>();
                // Fill_DdCustomer();
                // Fill_DdDistributor();
                Fill_Supplier();
                Fill_PaymentInstrument();
            }
        }
    }
    private void Fill_PaymentInstrument()
    {
        try
        {
            string Q = "select PaymentInstrumentId,PaymentInstrument,PaymentInstrument as OrderFor from tbl_PaymentInstruments where IsDeleted=0 union select 0 PaymentInstrumentId,'---Select---' PaymentInstrument,'.aaaaaa' union select 99 PaymentInstrumentId,'Other' PaymentInstrument,'ZZZZZZZZZ' OrderFor order by OrderFor";
            DataSet Ds = CS_Encrypt.GetData(Q, str);

            Dd_Payment_Mode.DataSource = Ds.Tables[0];
            Dd_Payment_Mode.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    #region FillSupplier
    private void Fill_Supplier()
    {
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "select SupplierId,SupplierName from tbl_Suppliers where Isdeleted =0 union select 0,'---select---'";
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
    #endregion
    protected void Dd_Supplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Supplier_Id = Convert.ToInt32(Dd_Supplier.SelectedValue);
        Fill_Gv_Payment_Bills(Supplier_Id);
    }
    protected void Tb_Receipt_Amount_TextChanged(object sender, EventArgs e)
    {
        int Go = 1;
        double Total_Outstanding = 0, Change_Return = 0, Total_AllocatedAmount = 0;
        double Receipt_Amount = 0;

        if (Tb_Receipt_Amount.Text != "")
        {
            try
            {
                Receipt_Amount = Convert.ToDouble(Tb_Receipt_Amount.Text.ToString());
                if (Receipt_Amount < 0)
                {
                    Go = 0;
                    Tb_Receipt_Amount.BorderColor = Color.Red;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Payment Amount Should Greater Than Zero.');", true);
                }
                else { Tb_Receipt_Amount.BorderColor = Color.LightGray; }
            }
            catch
            {
                Go = 0;
                Tb_Receipt_Amount.BorderColor = Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Enter Valid Payment Amount.');", true);
            }
        }
        else
        {
            Go = 0;
            Tb_Receipt_Amount.BorderColor = Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Enter Payment Amount.');", true);
        }
        if (Go == 1)
        {
            if (Receipt_Amount > 0)
            {
                for (int i = 0; i < List_OutstandingBills.Count; i++)
                {
                    List_OutstandingBills[i].AllocatedAmount = 0;
                    List_OutstandingBills[i].CurrentBalance = List_OutstandingBills[i].BalanceAmount - List_OutstandingBills[i].AllocatedAmount;

                    if (Receipt_Amount > 0 && List_OutstandingBills[i].CurrentBalance > 0)
                    {
                        if (Receipt_Amount > List_OutstandingBills[i].BalanceAmount)
                        {
                            List_OutstandingBills[i].AllocatedAmount = List_OutstandingBills[i].BalanceAmount;
                        }
                        else if (Receipt_Amount <= List_OutstandingBills[i].BalanceAmount)
                        {
                            List_OutstandingBills[i].AllocatedAmount = Receipt_Amount;
                        }
                        List_OutstandingBills[i].CurrentBalance = List_OutstandingBills[i].BalanceAmount - List_OutstandingBills[i].AllocatedAmount;
                        Receipt_Amount = Receipt_Amount - List_OutstandingBills[i].AllocatedAmount;
                    }
                    Total_Outstanding += List_OutstandingBills[i].CurrentBalance;
                    Total_AllocatedAmount += List_OutstandingBills[i].AllocatedAmount;
                }
            }
            Change_Return = Convert.ToDouble(Tb_Receipt_Amount.Text) - Total_AllocatedAmount;
            Tb_Change_Return.Text = Change_Return.ToString();
            HF_ChangeReturn.Value = Change_Return.ToString();
            Tb_Outstanding.Text = Total_Outstanding.ToString();
            //Tb_Overpayment.Text = "0";

            //Tb_Overpayment.Text = "";
            Gv_Payment_Bills.DataSource = null;
            Gv_Payment_Bills.DataSource = List_OutstandingBills;
            Gv_Payment_Bills.DataBind();
        }
    }
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        DateTime InstrumentDate = Convert.ToDateTime("01-01-1900");
        DateTime ReceiptDate = Convert.ToDateTime("01-01-1900");
        int Go = 1, IsCancelled = 0, SupplierId=0,PaymentModeID=0;
        string InstrumentNo = "", BankName = "",ReceiptNumber="";
        double ReceiptAmount = 0, ChangeReturnAmount = 0;
        Go = validate_Receipt_Details();
        
        if (Go == 1)
        {
            if (Dd_Supplier.SelectedIndex >= 0)
            {
                SupplierId = Convert.ToInt32(Dd_Supplier.SelectedValue);
            }
            if (Dd_Payment_Mode.SelectedIndex >= 0)
            {
                PaymentModeID = Convert.ToInt32(Dd_Payment_Mode.SelectedValue.ToString());
            }
            if (PaymentModeID != 1) //Not Cash
            {
                InstrumentNo = Tb_Instrument_No.Text.ToString();
                InstrumentDate = Convert.ToDateTime(DP_Instrument_Date.Text.ToString());
                BankName = Tb_Bank_Name.Text.ToString();
            }
            if (!String.IsNullOrEmpty(Dp_Receipt_Date.Text.ToString()))
            {
                ReceiptDate = Convert.ToDateTime(Dp_Receipt_Date.Text.ToString());
            }
            if (!String.IsNullOrEmpty(Tb_Receipt_Amount.Text.ToString()))
            {
                 ReceiptAmount = Convert.ToDouble(Tb_Receipt_Amount.Text.ToString());
            }
            if (!String.IsNullOrEmpty(Tb_Change_Return.Text.ToString()))
            {
                 ChangeReturnAmount = Convert.ToDouble(Tb_Change_Return.Text);
            }
            #region New
            con.ConnectionString = str;
            cmd = new SqlCommand();
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            cmd.Transaction = tran;
            try
            {
                cmd.CommandText = "USP_PaymentHeaderInsert";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ReferenceId", SupplierId);
                cmd.Parameters.AddWithValue("@PaymentModeId", PaymentModeID);
                cmd.Parameters.AddWithValue("@ChequeNumber", InstrumentNo);
                cmd.Parameters.AddWithValue("@ChequeDate", InstrumentDate);
                cmd.Parameters.AddWithValue("@BankName", BankName);
                cmd.Parameters.AddWithValue("@PaymentDate", ReceiptDate);
                cmd.Parameters.AddWithValue("@PaymentAmount", ReceiptAmount);
                cmd.Parameters.AddWithValue("@ChangeReturnAmount", ChangeReturnAmount);
                cmd.Parameters.AddWithValue("@IsCancelled", IsCancelled);
                cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                cmd.Parameters.AddWithValue("@CreatedBy", UserName);


                SqlParameter OP_PaymentHeaderId = new SqlParameter();
                OP_PaymentHeaderId.ParameterName = "@PaymentHeaderId";
                OP_PaymentHeaderId.DbType = DbType.Int32;
                OP_PaymentHeaderId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(OP_PaymentHeaderId);

                SqlParameter OP_PaymentReceiptNumber = new SqlParameter();
                OP_PaymentReceiptNumber.ParameterName = "@PaymentReceiptNumber";
                OP_PaymentReceiptNumber.DbType = DbType.String;
                OP_PaymentReceiptNumber.Size = 255;
                OP_PaymentReceiptNumber.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(OP_PaymentReceiptNumber);
                cmd.ExecuteNonQuery();

                int PaymentHeaderId = Convert.ToInt32(OP_PaymentHeaderId.Value.ToString());
                ReceiptNumber = OP_PaymentReceiptNumber.Value.ToString();


                for (int i = 0; i < List_OutstandingBills.Count; i++)
                {
                    if (List_OutstandingBills[i].AllocatedAmount != 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "USP_PaymentDetailInsert";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PaymentHeaderId", PaymentHeaderId);
                        cmd.Parameters.AddWithValue("@PaymentDetailReferenceId", List_OutstandingBills[i].PurchaseBillHeaderId);
                        cmd.Parameters.AddWithValue("@Amount", List_OutstandingBills[i].AllocatedAmount);
                        cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                        cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                        cmd.ExecuteNonQuery();
                    }
                }
                tran.Commit();
                con.Close();
                cmd.Parameters.Clear();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Payment Received Successfully:')", true);
                Lbl_Receipt_No.Text = "Payment Number:" + ReceiptNumber.ToString();
                Fill_Receipt_Detail_Grid(ReceiptHeader_Id);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
            }
            #endregion
        }

    }
  
    protected void Btn_New_Click(object sender, EventArgs e)
    {

    }
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_PaymentBrowse.aspx");
    }

    #region ValidateAllocatedAmmount
    private int ValidateAllocatedAmmount(int RowIndex)
    {
        int Go = 1;
        string ValidationMsg = "";
        TextBox Tb_Allocate_Amount = (TextBox)Gv_Payment_Bills.Rows[RowIndex].FindControl("Tb_Allocate_Amount");

        if (Tb_Receipt_Amount.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please Enter Receipt Amount.\\n";
            Tb_Receipt_Amount.BorderColor = Color.Red;
        }
        else
        {
            Tb_Receipt_Amount.BorderColor = Color.LightGray;
        }
        if (Tb_Allocate_Amount.Text.ToString() != "")
        {
            try
            {
                double AllocatedAmount = double.Parse(Tb_Allocate_Amount.Text);
                if (AllocatedAmount < 0)
                {
                    Go = 0;
                    ValidationMsg += "Allocated Amount Should Greater Than Zero.\\n";
                    Tb_Allocate_Amount.BorderColor = Color.Red;
                }
                else
                {
                    Tb_Allocate_Amount.BorderColor = Color.LightGray;
                }
            }
            catch
            {
                Go = 0;
                ValidationMsg += "Please Enter Valid Allocated Amount.\\n";
                Tb_Allocate_Amount.BorderColor = Color.Red;
            }
        }
        else
        {
            Go = 0;
            ValidationMsg += "Please Enter Allocated Amount.\\n";
            Tb_Allocate_Amount.BorderColor = Color.Red;
        }
        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :\\n" + ValidationMsg.ToString() + "')", true);
        }
        return Go;
    }
    #endregion
    protected void Tb_Allocate_Amount_TextChanged(object sender, EventArgs e)
    {
        int Go = 1;
        GridViewRow CurrentRow = (GridViewRow)(sender as TextBox).Parent.Parent;
        TextBox Tb_Allocate_Amount = (TextBox)CurrentRow.FindControl("Tb_Allocate_Amount");
        int RowIndex = CurrentRow.RowIndex;

        double Total_Outstanding = 0, Change_Return = 0, Total_AllocatedAmount = 0;
        double Receipt_Amount = 0, EditedAllocated_Amount = 0;

        TextBox Tb_AllocatedAmount = sender as TextBox;

        Go = ValidateAllocatedAmmount(RowIndex);

        if (Go == 1)
        {
            EditedAllocated_Amount = Convert.ToDouble(Tb_AllocatedAmount.Text.ToString());
            Receipt_Amount = Convert.ToDouble(Tb_Receipt_Amount.Text.ToString());

            if (Tb_AllocatedAmount.Text.ToString() == "")
            {
                Tb_AllocatedAmount.Text = "0";
            }
            if (EditedAllocated_Amount > Receipt_Amount)
            {
                Go = 0;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Entered allocation amount should be less than receipt amount')", true);
            }
            if (Receipt_Amount > 0 && EditedAllocated_Amount >= 0 && Go == 1)
            {
                List_OutstandingBills[RowIndex].AllocatedAmount = 0;
                List_OutstandingBills[RowIndex].CurrentBalance = List_OutstandingBills[RowIndex].BalanceAmount - List_OutstandingBills[RowIndex].AllocatedAmount;

                if (Receipt_Amount > 0 && EditedAllocated_Amount >= 0 && List_OutstandingBills[RowIndex].CurrentBalance > 0)
                {
                    if (EditedAllocated_Amount > List_OutstandingBills[RowIndex].BalanceAmount)
                    {
                        List_OutstandingBills[RowIndex].AllocatedAmount = List_OutstandingBills[RowIndex].BalanceAmount;
                    }
                    else if (EditedAllocated_Amount <= List_OutstandingBills[RowIndex].BalanceAmount)
                    {
                        List_OutstandingBills[RowIndex].AllocatedAmount = EditedAllocated_Amount;
                    }
                    List_OutstandingBills[RowIndex].CurrentBalance = List_OutstandingBills[RowIndex].BalanceAmount - List_OutstandingBills[RowIndex].AllocatedAmount;
                }
            }

            for (int i = 0; i < List_OutstandingBills.Count; i++)
            {
                Total_Outstanding += List_OutstandingBills[i].CurrentBalance;
                Total_AllocatedAmount += List_OutstandingBills[i].AllocatedAmount;
            }
            Change_Return = Convert.ToDouble(Tb_Receipt_Amount.Text) - Total_AllocatedAmount;
            Tb_Change_Return.Text = Change_Return.ToString();
            HF_ChangeReturn.Value = Change_Return.ToString();


            Tb_Outstanding.Text = Total_Outstanding.ToString();

            Gv_Payment_Bills.DataSource = null;
            Gv_Payment_Bills.DataSource = List_OutstandingBills;
            Gv_Payment_Bills.DataBind();

            if (Change_Return < 0)
            {
                Tb_Allocate_Amount.BorderColor = Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Entered allocation amount should be greater than receipt amount. Allocated amount is greater than receipt amount by " + Change_Return.ToString() + " Ruppes.')", true);
            }
        }
    }

    #region validate_Receipt_Details
    private int validate_Receipt_Details()
    {
        int Go = 1;
        string ValidationMsg = "";
        if (Dd_Supplier.SelectedIndex <= 0)
        {
            Go = 0;
            Dd_Supplier.BorderColor = Color.Red;
            ValidationMsg += "Please select Supplier Name.\\n";
        }
        else
        {
            Dd_Supplier.BorderColor = Color.LightGray;
        }

        if (Dd_Payment_Mode.SelectedIndex <= 0)
        {
            Go = 0;
            Dd_Payment_Mode.BorderColor = Color.Red;
            ValidationMsg += "Please Select Payment Instrument.\\n";
            Tb_Bank_Name.BorderColor = Color.LightGray;
            DP_Instrument_Date.BorderColor = Color.LightGray;
            Tb_Instrument_No.BorderColor = Color.LightGray;
        }
        else if (Dd_Payment_Mode.SelectedValue != "1") //Not Cash
        {
            if (Tb_Instrument_No.Text.ToString() == "")
            {
                Go = 0;
                Tb_Instrument_No.BorderColor = Color.Red;
                ValidationMsg += "Please Enter Instrument No.\\n";
            }
            else
            {
                Tb_Instrument_No.BorderColor = Color.LightGray;
            }
            if (DP_Instrument_Date.Text.ToString() == "")
            {
                Go = 0;
                DP_Instrument_Date.BorderColor = Color.Red;
                ValidationMsg += "Please Enter Instrument Date.\\n";
            }
            else
            {
                DP_Instrument_Date.BorderColor = Color.LightGray;

            }
            if (Tb_Bank_Name.Text.ToString() == "")
            {
                Go = 0;
                Tb_Bank_Name.BorderColor = Color.Red;
                ValidationMsg += "Please Enter Bank Name.\\n";
            }
            else
            {
                Tb_Bank_Name.BorderColor = Color.LightGray;
            }
        }
        else
        {
            Dd_Payment_Mode.BorderColor = Color.LightGray;
            Tb_Bank_Name.BorderColor = Color.LightGray;
            DP_Instrument_Date.BorderColor = Color.LightGray;
            Tb_Instrument_No.BorderColor = Color.LightGray;
        }

        if (Dp_Receipt_Date.Text.ToString() == "")
        {
            Go = 0;
            Dp_Receipt_Date.BorderColor = Color.Red;
            ValidationMsg += "Please Enter Payment Date.\\n";
        }
        else
        {
            Dp_Receipt_Date.BorderColor = Color.LightGray;
        }

        if (Tb_Receipt_Amount.Text.ToString() == "")
        {
            Go = 0;
            Tb_Receipt_Amount.BorderColor = Color.Red;
            ValidationMsg += "Please Enter Payment Amount.\\n";
        }
        else
        {
            try
            {
                double Amount = double.Parse(Tb_Receipt_Amount.Text.ToString());
                if (Amount <= 0)
                {
                    Go = 0;
                    Tb_Receipt_Amount.BorderColor = Color.Red;
                    ValidationMsg += "Receipt Amount Should Greater Than Zero.\\n";
                }
                else
                {
                    Tb_Receipt_Amount.BorderColor = Color.LightGray;
                }
            }
            catch
            {
                Go = 0;
                Tb_Receipt_Amount.BorderColor = Color.Red;
                ValidationMsg += "Please Enter Valid Payment Amount.\\n";
            }
        }
        if (Tb_Change_Return.Text.ToString() != "")
        {
            if (Convert.ToDouble(Tb_Change_Return.Text.ToString()) < 0)
            {
                Go = 0;
                Tb_Change_Return.BorderColor = Color.Red;
                ValidationMsg += "Allocated amount can not be greater than Payment amount.\\n";
            }
        }
        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :\\n" + ValidationMsg.ToString() + "')", true);
        }
        return Go;
    }
    #endregion

    private void Fill_Gv_Payment_Bills(int Supplier_Id)
    {

        try
        {
            List_OutstandingBills.Clear();
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Get_PurchaseBillDetailListForPayment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            cmd.Parameters.AddWithValue("@SupplierId", Supplier_Id);

            dr = cmd.ExecuteReader();
            int IndexToAdd = 0;
            while (dr.Read())
            {
                IndexToAdd = List_OutstandingBills.Count;
                List_OutstandingBills.Add(new CS_OutstandingBills()
                {
                    Index = IndexToAdd,
                    PurchaseBillHeaderId = Convert.ToInt32(dr["PurchaseOrderHeaderId"].ToString()),
                    SupplierId = Convert.ToInt32(dr["SupplierId"].ToString()),
                    BillNumber = dr["PurchaseOrderNumber"].ToString(),
                    BillDate = Convert.ToDateTime(dr["BillDate"].ToString()),
                    TotalBillAmount = Convert.ToDouble(dr["BillAmount"].ToString()),
                    PaidAmount = Convert.ToDouble(dr["PaidAmount"].ToString()),
                    BalanceAmount = Convert.ToDouble(dr["Balance"].ToString()),
                    AllocatedAmount = 0,
                    CurrentBalance = Convert.ToDouble(dr["Balance"].ToString())
                });
            }
            dr.Close();
            con.Close();
            Double TotalOutstanding_Amount = 0;
            for (int i = 0; i < List_OutstandingBills.Count; i++)
            {
                List_OutstandingBills[i].Index = i;
                List_OutstandingBills[i].SrNo = i + 1;
                TotalOutstanding_Amount += List_OutstandingBills[i].BalanceAmount;
            }
            Tb_Outstanding.Text = TotalOutstanding_Amount.ToString();
            Gv_Payment_Bills.DataSource = null;
            Gv_Payment_Bills.DataSource = List_OutstandingBills;
            Gv_Payment_Bills.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
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

    #region Show_Receipt_Detail_After_Save
    private void Fill_Receipt_Detail_Grid(int ReceiptHeader_Id)
    {
        for (int i = 0; i < List_OutstandingBills.Count; i++)
        {
            if (List_OutstandingBills[i].AllocatedAmount <= 0)
            {
                List_OutstandingBills.RemoveAt(i);
            }
        }
        Gv_Payment_Bills.DataSource = null;
        Gv_Payment_Bills.DataSource = List_OutstandingBills;
        Gv_Payment_Bills.DataBind();
        for (int i = 0; i < Gv_Payment_Bills.Rows.Count; i++)
        {
            TextBox Tb_Allocate_Amount = (TextBox)Gv_Payment_Bills.Rows[i].FindControl("Tb_Allocate_Amount");
            Tb_Allocate_Amount.Enabled = false;
        }
    }
    #endregion
}