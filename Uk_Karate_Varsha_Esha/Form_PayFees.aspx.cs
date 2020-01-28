using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class Form_PayFees : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    string UserName = "";
    int Employee_Id = 0;

    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);

    int StudentId { get { return (int)ViewState["StudentId"]; } set { ViewState["StudentId"] = value; } }

    public List<CS_Fees> List_Fees { get { return (List<CS_Fees>)Session["CS_Fees"]; } set { Session["CS_Fees"] = value; } }

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

            // ServiceCentre_Id = Convert.ToInt32(Session["ServiceCentre_Id"].ToString());
            UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            if (Request.QueryString.Count > 0)
            {
                int.Parse(Decrypt(Request.QueryString[0].ToString()));
                StudentId = Convert.ToInt32(Decrypt(Request.QueryString[0].ToString()));
            }
            else
            {
                StudentId = 0;
                Btn_SavePayment.Enabled = false;
            }


            if (!IsPostBack)
            {
                List_Fees = new List<CS_Fees>();
                FillOutstandingFees(StudentId);
                Gv_PayFees.DataSource = List_Fees;
                Gv_PayFees.DataBind();
                Tb_Amount.Text = "0";
                CalculateSummary();
            }
        }

    }

    private void CalculateSummary()
    {
        double OutstandingAmount = 0;
        double NewBalance = 0;
        double ChangeReturn = 0;
        double AllocatedAmount = 0;

        foreach (GridViewRow row in Gv_PayFees.Rows)
        {
            int StudentFeesDetailId = Convert.ToInt32(Gv_PayFees.DataKeys[row.RowIndex].Values[0].ToString());
            Label Lbl_FeeName = (Label)row.FindControl("Lbl_FeeName");
            HiddenField HF_FeeId = (HiddenField)row.FindControl("HF_FeeId");

            HiddenField Hf_FeeGenerationTypeId = (HiddenField)row.FindControl("Hf_FeeGenerationTypeId");
            Label Lbl_FeeGenerationType = (Label)row.FindControl("Lbl_FeeGenerationType");

            HiddenField Hf_FeeCategoryId = (HiddenField)row.FindControl("Hf_FeeCategoryId");
            HiddenField Hf_FeeCategory = (HiddenField)row.FindControl("Hf_FeeCategory");

            Label Lbl_FeeGenerationTypeReference = (Label)row.FindControl("Lbl_FeeGenerationTypeReference");
            HiddenField HF_FeeGenerationTypeReferenceId = (HiddenField)row.FindControl("HF_FeeGenerationTypeReferenceId");

            HiddenField Hf_FeeCollectionStageId = (HiddenField)row.FindControl("Hf_FeeCollectionStageIdS");
            HiddenField Hf_FeeCollectionStage = (HiddenField)row.FindControl("Hf_FeeCollectionStage");

            Label Lbl_Amount = (Label)row.FindControl("Lbl_Amount");
            Label Lbl_DiscountAmount = (Label)row.FindControl("Lbl_DiscountAmount");
            Label Lbl_FinalAmount = (Label)row.FindControl("Lbl_FinalAmount");
            Label Lbl_AmountReceivedTillNow = (Label)row.FindControl("Lbl_AmountReceivedTillNow");
            Label Lbl_TillNowBalance = (Label)row.FindControl("Lbl_TillNowBalance");
            TextBox Tb_AllocatedAmount = (TextBox)row.FindControl("Tb_AllocatedAmount");
            Label Lbl_Balance = (Label)row.FindControl("Lbl_Balance");
            HiddenField HF_Balance = (HiddenField)row.FindControl("HF_Balance");

            AllocatedAmount = AllocatedAmount + Convert.ToDouble(Tb_AllocatedAmount.Text.ToString());
            OutstandingAmount = OutstandingAmount + Convert.ToDouble(Lbl_TillNowBalance.Text.ToString());
            NewBalance = NewBalance + Convert.ToDouble(HF_Balance.Value.ToString());


        }

        if (!String.IsNullOrEmpty(Tb_Amount.Text.ToString()))
        {
            if (Convert.ToDouble(Tb_Amount.Text.ToString()) > 0)
            {
                ChangeReturn = ChangeReturn + (Convert.ToDouble(Tb_Amount.Text.ToString()) - AllocatedAmount);
            }
        }
        Tb_OutstandingAmount.Text = OutstandingAmount.ToString("##.00");
        Tb_Balance.Text = NewBalance.ToString("##.00");
        Tb_ChangeReturn.Text = ChangeReturn.ToString("##.00");
    }

    private void FillOutstandingFees(int StudentId)
    {

        try
        {
            List_Fees.Clear();
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetStudentOutstandingFeeDetailsForPayment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                List_Fees.Add(
                    new CS_Fees()
                    {

                        StudentId = Convert.ToInt32(dr["StudentId"].ToString()),
                        StudentFeesDetailId = Convert.ToInt32(dr["StudentFeesDetailId"].ToString()),
                        FeeId = Convert.ToInt32(dr["FeeId"].ToString()),
                        FeeName = dr["FeeName"].ToString(),

                        FeeGenerationTypeId = Convert.ToInt32(dr["FeeGenerationTypeId"].ToString()),
                        FeeGenerationType = dr["FeeGenerationType"].ToString(),

                        FeeGenerationTypeReferenceId = Convert.ToInt32(dr["FeeGenerationTypeReferenceId"].ToString()),
                        FeeGenerationTypeReference = dr["FeeGenerationTypeReference"].ToString(),

                        FeeCategoryId = Convert.ToInt32(dr["FeeCategoryId"].ToString()),
                        FeeCategory = dr["FeeCategory"].ToString(),
                        FeeCollectionStageId = Convert.ToInt32(dr["FeeCollectionStageId"].ToString()),
                        FeeCollectionStage = dr["FeeCollectionStage"].ToString(),
                        Amount = Convert.ToDouble(dr["Amount"].ToString()),
                        DiscountAmount = Convert.ToDouble(dr["DiscountAmount"].ToString()),
                        FinalAmount = Convert.ToDouble(dr["FinalAmount"].ToString()),
                        AmountReceivedTillNow = Convert.ToDouble(dr["AmountReceivedTillNow"].ToString()),
                        BalanceTillNow = Convert.ToDouble(dr["BalanceTillNow"].ToString()),

                        AllocatedAmount = Convert.ToDouble(dr["AllocatedAmount"].ToString()),
                        Balance = Convert.ToDouble(dr["Balance"].ToString()),

                    });
            }

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }

    public string Decrypt(string cipherText)
    {
        try
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
        }
        catch
        {

        }
        return cipherText;
    }

    protected void Gv_PayFees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int StudentFeesDetailId = Convert.ToInt32(Gv_PayFees.DataKeys[e.Row.RowIndex].Values[0].ToString());
            Label Lbl_FeeName = (Label)e.Row.FindControl("Lbl_FeeName");
            HiddenField HF_FeeId = (HiddenField)e.Row.FindControl("HF_FeeId");

            HiddenField Hf_FeeGenerationTypeId = (HiddenField)e.Row.FindControl("Hf_FeeGenerationTypeId");
            Label Lbl_FeeGenerationType = (Label)e.Row.FindControl("Lbl_FeeGenerationType");

            HiddenField Hf_FeeCategoryId = (HiddenField)e.Row.FindControl("Hf_FeeCategoryId");
            HiddenField Hf_FeeCategory = (HiddenField)e.Row.FindControl("Hf_FeeCategory");

            Label Lbl_FeeGenerationTypeReference = (Label)e.Row.FindControl("Lbl_FeeGenerationTypeReference");
            HiddenField HF_FeeGenerationTypeReferenceId = (HiddenField)e.Row.FindControl("HF_FeeGenerationTypeReferenceId");

            HiddenField Hf_FeeCollectionStageId = (HiddenField)e.Row.FindControl("Hf_FeeCollectionStageIdS");
            HiddenField Hf_FeeCollectionStage = (HiddenField)e.Row.FindControl("Hf_FeeCollectionStage");

            Label Lbl_Amount = (Label)e.Row.FindControl("Lbl_Amount");
            Label Lbl_DiscountAmount = (Label)e.Row.FindControl("Lbl_DiscountAmount");
            Label Lbl_FinalAmount = (Label)e.Row.FindControl("Lbl_FinalAmount");
            Label Lbl_AmountReceivedTillNow = (Label)e.Row.FindControl("Lbl_AmountReceivedTillNow");
            Label Lbl_TillNowBalance = (Label)e.Row.FindControl("Lbl_TillNowBalance");
            TextBox Tb_AllocatedAmount = (TextBox)e.Row.FindControl("Tb_AllocatedAmount");
            Label Lbl_Balance = (Label)e.Row.FindControl("Lbl_Balance");
            HiddenField HF_Balance = (HiddenField)e.Row.FindControl("HF_Balance");

            Tb_AllocatedAmount.Attributes.Add("onChange", "javascript:ChangeAllocatedAmountManually(" + Lbl_TillNowBalance.ClientID + ","
                + Tb_AllocatedAmount.ClientID + ","
                + Lbl_Balance.ClientID + ","
                + HF_Balance.ClientID +
                ");");

        }

    }
    protected void Btn_SavePayment_Click(object sender, EventArgs e)
    {
        CalculateSummary();
        Double ChangeReturn = 0;
        if (!String.IsNullOrEmpty(Tb_ChangeReturn.Text))
        {
            ChangeReturn = Convert.ToDouble(Tb_ChangeReturn.Text.ToString());
        }
        if (ChangeReturn >= 0)
        {
            //save
            if (StudentId != 0)
            {
                foreach (GridViewRow row in Gv_PayFees.Rows)
                {

                    int StudentFeesDetailId = Convert.ToInt32(Gv_PayFees.DataKeys[row.RowIndex].Value.ToString());
                    TextBox Tb_AllocatedAmount = (TextBox)row.FindControl("Tb_AllocatedAmount");
                    Double AllocatedAmount = 0;
                    if (!String.IsNullOrEmpty(Tb_AllocatedAmount.Text))
                    {
                        AllocatedAmount = Convert.ToDouble(Tb_AllocatedAmount.Text.ToString());
                    }
                    if (AllocatedAmount > 0)
                    {
                        con.ConnectionString = str;
                        cmd = new SqlCommand();
                        con.Open();

                        SqlTransaction tran = con.BeginTransaction();
                        cmd.Transaction = tran;

                        try
                        {
                            cmd.CommandText = "USP_StudentOutstandingFeePaidInsert";
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@StudentFeesDetailId", StudentFeesDetailId);
                            cmd.Parameters.AddWithValue("@AmountPaid", AllocatedAmount);
                            cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                            cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);

                            cmd.ExecuteNonQuery();

                            tran.Commit();
                            cmd.Parameters.Clear();
                            con.Close();


                        }

                        catch (Exception ex)
                        {
                            tran.Rollback();
                            con.Close();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                        }
                        finally
                        {

                            con.Close();
                        }
                    }

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Fee paid details saved successfully');window.location='Form_OutstandingFees.aspx';", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please contact administrator')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Change return can not be negative. Please check the allocation')", true);
        }
    }
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_OutstandingFees.aspx");
    }
    protected void Tb_Amount_TextChanged(object sender, EventArgs e)
    {
        double AllocatedAmount = 0, OutstandingAmount = 0;

        if (!String.IsNullOrEmpty(Tb_Amount.Text))
        {
            AllocatedAmount = Convert.ToDouble(Tb_Amount.Text);
        }
        if (!String.IsNullOrEmpty(Tb_OutstandingAmount.Text))
        {
            OutstandingAmount = Convert.ToDouble(Tb_OutstandingAmount.Text);
        }
        if (AllocatedAmount > OutstandingAmount)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Amount Paid is greater than outstanding Amount')", true);
            Tb_Amount.BorderColor = Color.Red;
            Tb_Amount.Focus();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "AllocateAmount();", true);
        }

    }
}