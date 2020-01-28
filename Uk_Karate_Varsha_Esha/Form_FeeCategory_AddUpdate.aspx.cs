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
using CS_Encryption;
using System.Drawing;

public partial class Form_FeeCategory_AddUpdate : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    string UserName = "";
    int FeeCategoryIdToUpdate, EmpolyeeId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginUsername"] == null || Session["LoginAuthenticated"] == "No")
        {
            Response.Redirect(@"~\Index.aspx");
        }
        else
        {
            EmpolyeeId = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());
            UserName = Session["LoginUsername"].ToString();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            FeeCategoryIdToUpdate = Convert.ToInt32(Session["FeeCategoryIdToUpdate"].ToString());



            if (!IsPostBack)
            {
                if (FeeCategoryIdToUpdate != 0)
                {
                    Lbl_Heading.Text = "Update Class Details";
                    Fill_FeeCategoryDetails(FeeCategoryIdToUpdate);
                }
                Fill_FeeCollectionStage();
            }

        }
    }
    private void Fill_FeeCategoryDetails(int FeeCategoryIdToUpdate)
    {
        int FeeCollectionStageId = 0;
        Boolean IsOneOfTheGroup = false;
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "select FeeCategoryId,FeeCategory,FeeCollectionStageId,IsOneOfTheGroup,Remark from tbl_FeeCategories Where FeeCategoryId=" + FeeCategoryIdToUpdate;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Tb_FeeCategory.Text = dr["FeeCategory"].ToString();

                FeeCollectionStageId = Convert.ToInt32(dr["FeeCollectionStageId"].ToString());
                Dd_FeeCollectionStage.ClearSelection();
                Dd_FeeCollectionStage.SelectedValue = FeeCollectionStageId.ToString();

                Tb_Description.Text = dr["Remark"].ToString();

                IsOneOfTheGroup = Convert.ToBoolean(dr["IsOneOfTheGroup"].ToString());

                if (IsOneOfTheGroup == false)
                {
                    Chk_IsOneOfTheGroup.Checked = false;
                }
                else
                {
                    Chk_IsOneOfTheGroup.Checked = true;
                }
            }
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occure:" + ex.Message.ToString() + "')", true);
        }
    }

    private void Fill_FeeCollectionStage()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select FeeCollectionStageId,FeeCollectionStage from tbl_FeeCollectionStages union Select 0 as FeeCollectionStageId,'---SELECT---' as FeeCollectionStage";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Employee");
            Dd_FeeCollectionStage.DataSource = ds.Tables["Employee"];
            Dd_FeeCollectionStage.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    private int ValidateCategory()
    {
        int Go = 1;
        string ValidationMsg = "";

        if (Tb_FeeCategory.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter Fee Category .\\n";
            Tb_FeeCategory.BorderColor = Color.Red;
        }
        else
        {
            Tb_FeeCategory.BorderColor = Color.LightGray;
        }

        if (Dd_FeeCollectionStage.SelectedIndex <= 0)
        {
            Go = 0;
            ValidationMsg += "Please Select Fee Collection Stage .\\n";
            Dd_FeeCollectionStage.BorderColor = Color.Red;
        }
        else
        {
            Dd_FeeCollectionStage.BorderColor = Color.LightGray;
        }
        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :\\n" + ValidationMsg.ToString() + "')", true);
        }
        return Go;
    }
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        int Go = 1, FeeCollectionStageId = 0;
        string Remark = "", FeeCategory = "";
        Boolean IsOneOfTheGroup = false;
        DateTime ModifiedDate = Convert.ToDateTime("01-01-1900");
        DateTime DeletedDate = Convert.ToDateTime("01-01-1900");
        DateTime ApprovedDate = Convert.ToDateTime("01-01-1900");

        if (Go == 1)
        {

            FeeCategory = Tb_FeeCategory.Text.ToString();
            if (Dd_FeeCollectionStage.SelectedIndex >= 0)
            {
                FeeCollectionStageId = Convert.ToInt32(Dd_FeeCollectionStage.SelectedValue);
            }
            if (Chk_IsOneOfTheGroup.Checked == true)
            {
                IsOneOfTheGroup = true;
            }
            else
            {
                IsOneOfTheGroup = false;
            }
            Remark = Tb_Description.Text.ToString();

            if (FeeCategoryIdToUpdate == 0)
            {
                try
                {
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "USP_FeeCategory_Insert";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FeeCategory", FeeCategory);
                    cmd.Parameters.AddWithValue("@FeeCollectionStageId", FeeCollectionStageId);
                    cmd.Parameters.AddWithValue("@Remark", Remark);
                    cmd.Parameters.AddWithValue("@IsOneOfTheGroup", IsOneOfTheGroup);
                    cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                    cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ModifiedBy", "");
                    cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                    cmd.Parameters.AddWithValue("@IsDeleted", 0);
                    cmd.Parameters.AddWithValue("@DeletedBy", "");
                    cmd.Parameters.AddWithValue("@DeletedDate", DeletedDate);
                    cmd.Parameters.AddWithValue("@ApprovedBy", "");
                    cmd.Parameters.AddWithValue("@ApprovedDate", ApprovedDate);

                    cmd.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Fee Category added successfully');window.location='Form_FeeCategoryMaster.aspx'", true);


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
            else if (FeeCategoryIdToUpdate != 0)
            {
                try
                {
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "USP_FeeCategory_Update";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FeeCategoryId", FeeCategoryIdToUpdate);
                    cmd.Parameters.AddWithValue("@FeeCategory", FeeCategory);
                    cmd.Parameters.AddWithValue("@FeeCollectionStageId", FeeCollectionStageId);
                    cmd.Parameters.AddWithValue("@Remark", Remark);
                    cmd.Parameters.AddWithValue("@IsOneOfTheGroup", IsOneOfTheGroup);

                    cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                    cmd.ExecuteNonQuery();
                    con.Close();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Fee Category details updated successfully');window.location='Form_FeeCategoryMaster.aspx'", true);


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
        }
    }
    protected void Btn_New_Click(object sender, EventArgs e)
    {
        Session["FeeCategoryIdToUpdate"] = "0";
        Response.Redirect(@"~\Form_FeeCategory_AddUpdate.aspx");
    }
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("Form_FeeCategoryMaster.aspx");
    }
}