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


public partial class Form_MaterialCategoryAddUpdate : System.Web.UI.Page
{

    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    string UserName = "";
    int MaterialCategoryId_ToUpdate, EmpolyeeId;

    //int MaterialCategoryId_ToUpdate { get { return (int)ViewState["MaterialCategoryId_ToUpdate"]; } set { ViewState["MaterialCategoryId_ToUpdate"] = value; } };
    //int EmpolyeeId=0;
    
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
            MaterialCategoryId_ToUpdate = Convert.ToInt32(Session["MaterialCategoryId_ToUpdate"].ToString());

            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
            //MaterialCategoryId_ToUpdate = Convert.ToInt32(Session["MaterialCategoryId_ToUpdate"].ToString());
            //SocietyId = Convert.ToInt32(Session["SocietyId"].ToString());


            if (!IsPostBack)
            {
                if (MaterialCategoryId_ToUpdate != 0)
                {
                    Lbl_Heading.Text = "Update Material Category Details";
                    Fill_MaterialCategoryDetails(MaterialCategoryId_ToUpdate);                    
                }
                Session["MaterialCategoryId_ToUpdate"] = 0;
            }

        }
    }

    private void Fill_MaterialCategoryDetails(int MaterialCategoryId_ToUpdate)
    {
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Select MaterialCategoryId,CategoryName,Remark from tbl_MaterialCategories Where MaterialCategoryId=" + MaterialCategoryId_ToUpdate;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Tb_CategoryName.Text = dr["CategoryName"].ToString();
                Tb_Description.Text = dr["Remark"].ToString();
            }
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occure:" + ex.Message.ToString() + "')", true);
        }
    }
    private int ValidateCategory()
    {
        int Go = 1;
        string ValidationMsg = "";

        if (Tb_CategoryName.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter Category Name.\\n";
            Tb_CategoryName.BorderColor = Color.Red;
        }
        else
        {
            Tb_CategoryName.BorderColor = Color.LightGray;
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
        string Remark = "", CategoryName = "";
        DateTime ModifiedDate = Convert.ToDateTime("01-01-1900");
        DateTime DeletedDate = Convert.ToDateTime("01-01-1900");
        DateTime ApprovedDate = Convert.ToDateTime("01-01-1900");


        //Go = ValidateInput();

        if (Go == 1)
        {

            CategoryName = Tb_CategoryName.Text.ToString();

            Remark = Tb_Description.Text.ToString();

            if (MaterialCategoryId_ToUpdate == 0)
            {

                try
                {
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "USP_MaterialCategoryInsert";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                    cmd.Parameters.AddWithValue("@Remark", Remark);

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

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Category  added successfully');window.location='Form_MaterialCategories.aspx'", true);


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
            else if (MaterialCategoryId_ToUpdate != 0)
            {
                try
                {
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "USP_MaterialCategoryUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaterialCategoryId", MaterialCategoryId_ToUpdate);
                    cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                    cmd.Parameters.AddWithValue("@Remark", Remark);
                    cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@SocietyId", SocietyId);

                    cmd.ExecuteNonQuery();
                    con.Close();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Material Category updated successfully');window.location='Form_MaterialCategories.aspx'", true);


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
        Session["MaterialCategoryId_ToUpdate"] = 0;
        Response.Redirect(@"~\Form_MaterialCategoryAddUpdate.aspx");
    }
    protected void Btn_Back_Click(object sender, EventArgs e)
    {

        Response.Redirect(@"~\Form_MaterialCategories.aspx");
    }
}