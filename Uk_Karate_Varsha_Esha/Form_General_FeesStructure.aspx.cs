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
using System.IO;


public partial class Form_General_FeesStructure : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        Fill_Grade();
    }



    private void Fill_Grade()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select Level from Tbl_Belt";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            Dd_Title.DataSource = ds.Tables[0];
            Dd_Title.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }

    protected void Dd_Title_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Dd_Title_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Btn_AnnualFee_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_AnnualFee.Text + "'  where FeeId='1'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_AnnualFee.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    protected void Btn_TornamentFee_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_TornamentFee.Text + "'  where FeeId='2'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_TornamentFee.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Btn_EnrollmentFee_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_EnrolmentFee.Text + "'  where FeeId='3'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_EnrolmentFee.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }


    }
    protected void Btn_TermFee_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_TermFee.Text + "'  where FeeId='4'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_TermFee.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    protected void Btn_GradeFee_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_GradeFee.Text + "'  where FeeId='8'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_GradeFee.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    protected void Btn_GradingFeeOrng_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_GradingFeeOrng.Text + "'  where FeeId='9'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_GradingFeeOrng.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    protected void Btn_GradingFeeBrn_Click(object sender, EventArgs e)
    {

        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_GradingFeeBrn.Text + "'  where FeeId='11'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_GradingFeeBrn.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Tb_GradingFeeBlk_TextChanged(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_GradingFeeBlk.Text + "'  where FeeId='12'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_GradingFeeBlk.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Btn_JpSeminar_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_JpSeminar.Text + "'  where FeeId='5'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_JpSeminar.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    protected void Btn_TournamentFee_Click(object sender, EventArgs e)
    {
        
            
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_TournamentFee.Text + "'  where FeeId='6'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_TournamentFee.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    protected void Btn_TournamentEFee_Click(object sender, EventArgs e)
    {

        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_TournamentEFee.Text + "'  where FeeId='13'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_TournamentEFee.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    protected void Btn_BronzeKit_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_BronzeKit.Text + "'  where FeeId='7'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_BronzeKit.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Btn_SilverPkg_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_SilverPkg.Text + "'  where FeeId='14'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_SilverPkg.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Btn_GoldPkg_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_GoldPkg.Text + "'  where FeeId='15'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_GoldPkg.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Btn_YearlyInstrFee_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_YearlyInstrFee.Text + "'  where FeeId='16'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_YearlyInstrFee.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    protected void Btn_SeiwakaiFee_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_SeiwakaiFee.Text + "'  where FeeId='17'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_SeiwakaiFee.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Btn_TimeFrame_Click(object sender, EventArgs e)
    {
        
            try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_TimeFrame.Text + "'  where FeeId='18'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_TimeFrame.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    protected void Btn_LateFee_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_LateFee.Text + "'  where FeeId='19'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_LateFee.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    protected void Btn_Dojo_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Update tbl_Fees set Amount='" + Tb_LateFee.Text + "'  where FeeId='19'";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Amount", Tb_LateFee.Text);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
}

   