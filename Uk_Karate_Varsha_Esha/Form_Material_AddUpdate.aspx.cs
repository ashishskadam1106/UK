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
using CS_Encryption;

public partial class Form_Material_AddUpdate : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    SqlDataReader dr;
    DataSet ds;

    string UserName = "";
    int MaterialMasterId_ToUpdate, Employee_Id;


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

            if (Request.QueryString.Count > 0)
            {
                MaterialMasterId_ToUpdate = Convert.ToInt32(CS_Encrypt.Decrypt(Request.QueryString["ID"].ToString()));
            }



            if (!IsPostBack)
            {
                Fill_Category();
                if (MaterialMasterId_ToUpdate != 0)
                {
                    Fill_MaterialDetails(MaterialMasterId_ToUpdate);
                }
            }
        }
    }

    private void Fill_Category()
    {
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Select MaterialCategoryId,CategoryName from tbl_MaterialCategories Where IsDeleted=0 union select 0 MaterialCategoryId,'--- select---'  CategoryName";
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "CategoryName");
            Dd_CategoryName.DataSource = ds.Tables["CategoryName"];
            Dd_CategoryName.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }

    private int ValidateMaterial()
    {
        int Go = 1;
        string ValidationMsg = "";

        if (Tb_MaterialName.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter material name.\\n";
            Tb_MaterialName.BorderColor = Color.Red;
        }
        else
        {
            Tb_MaterialName.BorderColor = Color.LightGray;
        }


        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :\\n" + ValidationMsg.ToString() + "')", true);
        }
        return Go;
    }
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        int Go = 1, MaterialCategoryId = 0;
        String MaterialName = "";
        double PurchasePrice = 0, SellPrice = 0, SellToInstructorPrice = 0;
        Go = ValidateMaterial();
        if (Go == 1)
        {
            MaterialName = Tb_MaterialName.Text.Trim();

            MaterialCategoryId = Convert.ToInt32(Dd_CategoryName.SelectedValue);
            if (!String.IsNullOrEmpty(Tb_PurchasePrice.Text))
            {
                PurchasePrice = Convert.ToDouble(Tb_PurchasePrice.Text);
            }
            if (!String.IsNullOrEmpty(Tb_SellPrice.Text))
            {
                SellPrice = Convert.ToDouble(Tb_SellPrice.Text);
            }
            if (!String.IsNullOrEmpty(Tb_InstructorPrice.Text))
            {
                SellToInstructorPrice = Convert.ToDouble(Tb_InstructorPrice.Text);
            }
            if (MaterialMasterId_ToUpdate == 0)
            {
                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();

                try
                {
                    cmd.CommandText = "USP_Material_Insert";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaterialCategoryId", MaterialCategoryId);
                    cmd.Parameters.AddWithValue("@MaterialName", MaterialName);
                    cmd.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
                    cmd.Parameters.AddWithValue("@SellPrice", SellPrice);
                    cmd.Parameters.AddWithValue("@SellToInstructorPrice", SellToInstructorPrice);

                    cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                    cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);


                    cmd.ExecuteNonQuery();
                    con.Close();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Material added successfully');window.location='Form_MaterialMaster.aspx'", true);
                }
                catch (Exception ex)
                {

                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :\\n" + ex.Message.ToString() + "')", true);
                }
            }
            else
            {
                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();

                try
                {
                    cmd.CommandText = "USP_Material_Update";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaterialMasterId", MaterialMasterId_ToUpdate);
                    cmd.Parameters.AddWithValue("@MaterialCategoryId", MaterialCategoryId);
                    cmd.Parameters.AddWithValue("@MaterialName", MaterialName);
                    cmd.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
                    cmd.Parameters.AddWithValue("@SellPrice", SellPrice);
                    cmd.Parameters.AddWithValue("@SellToInstructorPrice", SellToInstructorPrice);

                    cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                  

                    cmd.ExecuteNonQuery();
                    con.Close();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Material details updated successfully.');window.location='Form_MaterialMaster.aspx'", true);
                }
                catch (Exception ex)
                {
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :\\n" + ex.Message.ToString() + "')", true);
                }

            }
        }
    }
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_MaterialMaster.aspx");
    }
    protected void Btn_New_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_Material_AddUpdate.aspx");
    }

    private void Fill_MaterialDetails(int MaterialMasterId_ToUpdate)
    {
        con.ConnectionString = str;
        cmd = new SqlCommand();
        con.Open();
        try
        {
            cmd.CommandText = "USP_GetMaterialDetails";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaterialMasterId", MaterialMasterId_ToUpdate);
          
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Dd_CategoryName.ClearSelection();
                Dd_CategoryName.Items.FindByValue(dr["MaterialCategoryId"].ToString()).Selected = true;
                Tb_MaterialName.Text = dr["MaterialName"].ToString();
                Tb_PurchasePrice.Text = dr["PurchasePrice"].ToString();
                Tb_SellPrice.Text = dr["SellPrice"].ToString();
                Tb_InstructorPrice.Text = dr["SellToInstructorPrice"].ToString();
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