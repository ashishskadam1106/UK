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

public partial class Form_Supplier_AddUpdate : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
    string UserName = "";
    int Employee_Id = 0, SupplierId_ToUpdate = 0, CompanyId = 0;
    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);

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

            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            if (Request.QueryString.Count > 0)
            {
                SupplierId_ToUpdate = Convert.ToInt32(CS_Encrypt.Decrypt(Request.QueryString["ID"].ToString()));
            }


            if (!IsPostBack)
            {

                // Fill_City();
                if (SupplierId_ToUpdate != 0)
                {
                    Lbl_Heading.Text = "Update supplier details";
                     Fill_SupplierDetails(SupplierId_ToUpdate);

                }
            }
        }
    }

    private int ValidateData()
    {
        int Go = 1;
        string ValidationMsg = "";
        if (Tb_SupplierName.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter Supplier Name.\\n";
            Tb_SupplierName.BorderColor = Color.Red;
        }
        else
        {
            Tb_SupplierName.BorderColor = Color.LightGray;
        }

        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :\\n" + ValidationMsg.ToString() + "')", true);
        }
        return Go;
    }



    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        int Go = 1;
        string SupplierName = "", SupplierAddress = "", ContactMobile = "", ContactOffice = "";
        string PostalCode = "", Premise = "", County = "", Ward = "", District = "", Country = "", EmailId = "";
        double OpeningCredit = 0, OpeningDebit = 0;
        // geography Latitude = "", Longitude = "";
        DateTime ModifiedDate = Convert.ToDateTime("01-01-1900");
        DateTime DeletedDate = Convert.ToDateTime("01-01-1900");
        DateTime ApprovedDate = Convert.ToDateTime("01-01-1900");
        DateTime BirthDate = Convert.ToDateTime("01-01-1900");

        Go = ValidateData();
        if (Go == 1)
        {
            SupplierName = Tb_SupplierName.Text;
            ContactMobile = Tb_ContactMobile.Text;
            ContactOffice = Tb_ContactOffice.Text;
            PostalCode = Tb_PostCode.Text;
            Premise = Tb_Premise.Text;
            County = Tb_County.Text;
            Country = Tb_Country.Text;
            EmailId = Tb_Email.Text;
            //if (!String.IsNullOrEmpty(Tb_OpeningCredit.Text))
            //{
            //    OpeningCredit = Convert.ToDouble(Tb_OpeningCredit.Text);
            //}
            //if (!String.IsNullOrEmpty(Tb_OpeningDebit.Text))
            //{
            //    OpeningDebit = Convert.ToDouble(Tb_OpeningDebit.Text);
            //}
            SupplierAddress = Tb_Address.Text;

            if (SupplierId_ToUpdate == 0)
            {
                try
                {
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "USP_SupplierInsert";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SupplierName", SupplierName);
                    cmd.Parameters.AddWithValue("@SupplierAddress", SupplierAddress);

                    cmd.Parameters.AddWithValue("@Premise", Premise);
                    cmd.Parameters.AddWithValue("@County", County);
                    cmd.Parameters.AddWithValue("@Ward", Ward);
                    cmd.Parameters.AddWithValue("@District", District);
                    cmd.Parameters.AddWithValue("@Country", Country);
                    cmd.Parameters.AddWithValue("@ContactMobile", ContactMobile);
                    cmd.Parameters.AddWithValue("@ContactOffice", ContactOffice);
                    cmd.Parameters.AddWithValue("@EMailId", EmailId);
                    //cmd.Parameters.AddWithValue("@OpeningCredit", OpeningCredit);
                    //cmd.Parameters.AddWithValue("@OpeningDebit", OpeningDebit);


                    cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                    cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ModifiedBy", "");
                    cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                    cmd.Parameters.AddWithValue("@IsDeleted", 0);
                    cmd.Parameters.AddWithValue("@DeletedBy", "");
                    cmd.Parameters.AddWithValue("@DeletedDate", DeletedDate);
                    cmd.Parameters.AddWithValue("@IsApproved", 1);
                    cmd.Parameters.AddWithValue("@ApprovedBy", "");
                    cmd.Parameters.AddWithValue("@ApprovedDate", ApprovedDate);

                    cmd.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Supplier  added successfully');window.location='Form_SupplierMaster.aspx'", true);


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
            else
            {

                con.ConnectionString = str;
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "USP_SupplierUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SupplierId", SupplierId_ToUpdate);
                cmd.Parameters.AddWithValue("@SupplierName", SupplierName);
                cmd.Parameters.AddWithValue("@SupplierAddress", SupplierAddress);

                cmd.Parameters.AddWithValue("@Premise", Premise);
                cmd.Parameters.AddWithValue("@County", County);
                cmd.Parameters.AddWithValue("@Ward", Ward);
                cmd.Parameters.AddWithValue("@District", District);
                cmd.Parameters.AddWithValue("@Country", Country);
                cmd.Parameters.AddWithValue("@ContactMobile", ContactMobile);
                cmd.Parameters.AddWithValue("@ContactOffice", ContactOffice);
                cmd.Parameters.AddWithValue("@EMailId", EmailId);
                //cmd.Parameters.AddWithValue("@OpeningCredit", OpeningCredit);
                //cmd.Parameters.AddWithValue("@OpeningDebit", OpeningDebit);
                cmd.Parameters.AddWithValue("@ModifiedBy", "");
                cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
               

                cmd.ExecuteNonQuery();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Supplier  Updated successfully');window.location='Form_SupplierMaster.aspx'", true);

                con.Close();

            }
        }
    }
    protected void Btn_New_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_Supplier_AddUpdate.aspx");
    }
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_SupplierMaster.aspx");
    }

    private void Fill_SupplierDetails(int SupplierId_ToUpdate)
    {
        con.ConnectionString = str;
        cmd = new SqlCommand();
        con.Open();
        try
        {
            cmd.CommandText = "USP_GetSupplierDetails";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SupplierId", SupplierId_ToUpdate);

            dr = cmd.ExecuteReader();

            if (dr.Read())
            {

                Tb_SupplierName.Text = dr["SupplierName"].ToString();
                Tb_ContactOffice.Text = dr["ContactOffice"].ToString();
                Tb_ContactMobile.Text = dr["ContactMobile"].ToString();
                Tb_Email.Text = dr["EMailId"].ToString();
                //Tb_PostCode.Text = dr["SellToInstructorPrice"].ToString();
                Tb_Address.Text = dr["SupplierAddress"].ToString();
                Tb_Premise.Text = dr["Premise"].ToString();
                Tb_County.Text = dr["County"].ToString();
                Tb_Country.Text = dr["Country"].ToString();
                Tb_Ward.Text = dr["Ward"].ToString();
                Tb_District.Text = dr["District"].ToString();
                //Tb_OpeningCredit.Text = dr["OpeningCredit"].ToString();
                //Tb_OpeningDebit.Text = dr["OpeningDebit"].ToString();
            }
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            dr.Close();
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :\\n" + ex.Message.ToString() + "')", true);
        }
    }
}