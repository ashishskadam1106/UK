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


public partial class Form_MaterialCategories : System.Web.UI.Page
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
    public List<CS_MaterialCategory> List_MaterialCategory { get { return (List<CS_MaterialCategory>)Session["CS_MaterialCategory"]; } set { Session["CS_MaterialCategory"] = value; } }



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
                List_MaterialCategory = new List<CS_MaterialCategory>();
                Fill_MaterialCategory();
                 Add_DummyRow();
                 Hide_DummyRow();
            }
        }
    }
    private void Fill_MaterialCategory()
    {
        List_MaterialCategory.Clear();
        int IndexToAdd = 0;
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select MaterialCategoryId,CategoryName,isnull(Remark,'')Remark  from tbl_MaterialCategories Where IsDeleted=0";
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                IndexToAdd = List_MaterialCategory.Count;
                List_MaterialCategory.Add(new CS_MaterialCategory()
                {
                    Index = IndexToAdd,
                    MaterialCategoryId = Convert.ToInt32(dr[0].ToString()),
                    CategoryName = dr[1].ToString(),
                    Remark = dr[2].ToString()

                });
            }
            con.Close();

            for (int i = 0; i < List_MaterialCategory.Count; i++)
            {
                List_MaterialCategory[i].Index = i;
                List_MaterialCategory[i].SrNo = List_MaterialCategory[i].Index + 1;
            }
            Gv_MaterialCategory.DataSource = List_MaterialCategory;
            Gv_MaterialCategory.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    #region Add & Hide Dummy row
    private void Add_DummyRow()
    {
        if (List_MaterialCategory.Count == 0)
        {
            List_MaterialCategory.Add(new CS_MaterialCategory
            {
                Index = -1,
                CategoryName = "Dummy",
                MaterialCategoryId = 0
            });
        }
        Gv_MaterialCategory.DataSource = List_MaterialCategory;
        Gv_MaterialCategory.DataBind();
    }

    private void Hide_DummyRow()
    {
        if (List_MaterialCategory[0].Index == -1 && List_MaterialCategory[0].MaterialCategoryId == 0)
        {
            Label Lbl_SrNo = (Label)Gv_MaterialCategory.Rows[0].FindControl("Lbl_SrNo");
            Label Lbl_CategoryName = (Label)Gv_MaterialCategory.Rows[0].FindControl("Lbl_CategoryName");
            Label Lbl_Remark = (Label)Gv_MaterialCategory.Rows[0].FindControl("Lbl_Remark");

            Button Btn_Edit = (Button)Gv_MaterialCategory.Rows[0].FindControl("Btn_Edit");
            Button Btn_Delete = (Button)Gv_MaterialCategory.Rows[0].FindControl("Btn_Delete");

            Lbl_SrNo.Text = "";
            Lbl_CategoryName.Text = "";
            Lbl_Remark.Text = "";

            Btn_Edit.Visible = false;
            Btn_Delete.Visible = false;
        }
    }

    #endregion
    protected void Gv_MaterialCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Gv_MaterialCategory.EditIndex = -1;
        Gv_MaterialCategory.DataSource = List_MaterialCategory;
        Gv_MaterialCategory.DataBind();
    }
    protected void Gv_MaterialCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int Go = 1;
        TextBox Tb_CategoryName = (TextBox)Gv_MaterialCategory.FooterRow.FindControl("Tb_CategoryName");
        TextBox Tb_Remark = (TextBox)Gv_MaterialCategory.FooterRow.FindControl("Tb_Remark");

        if (e.CommandName.Equals("Add"))
        {
            if (Tb_CategoryName.Text == "")
            {
                Go = 0;
                Tb_CategoryName.BorderColor = Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Enter Category Name')", true);
            }
            if (Go == 1)
            {

                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandText = "USP_MaterialCategoryInsert";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CategoryName", Tb_CategoryName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Remark", Tb_Remark.Text.ToString());
                    cmd.Parameters.AddWithValue("@IsDeleted", 0);
                    cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                    cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);

                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    cmd.Parameters.Clear();
                    con.Close();

                    Fill_MaterialCategory();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Category Name Added Successfully')", true);
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
    }
    protected void Gv_MaterialCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Gv_MaterialCategory.EditIndex = e.NewEditIndex;
        Gv_MaterialCategory.DataSource = List_MaterialCategory;
        Gv_MaterialCategory.DataBind();
        int EditIndex = e.NewEditIndex;
    }
    protected void Gv_MaterialCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int UpdateIndex = e.RowIndex;
        int Go = 1;
        TextBox Tb_CategoryName = (TextBox)Gv_MaterialCategory.Rows[UpdateIndex].FindControl("Tb_CategoryName");
        TextBox Tb_Remark = (TextBox)Gv_MaterialCategory.Rows[UpdateIndex].FindControl("Tb_Remark");
        int MaterialCategoryId = Convert.ToInt32(Gv_MaterialCategory.DataKeys[UpdateIndex].Value.ToString());

        if (Tb_CategoryName.Text.ToString() == "")
        {
            Go = 0;
            Tb_CategoryName.BorderColor = Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Enter Category Name')", true);
        }
        else
        {
            Tb_CategoryName.BorderColor = Color.LightGray;
        }

        if (Go == 1)
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            con.Open();

            SqlTransaction tran = con.BeginTransaction();
            cmd.Transaction = tran;
            try
            {
                string CategoryName = "", Remark = "";

                CategoryName = Tb_CategoryName.Text.Trim().ToString();
                if (Tb_Remark.Text.ToString() != "")
                {
                    Remark = Tb_Remark.Text.ToString();
                }

                cmd.CommandText = "USP_MaterialCategoryUpdate";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaterialCategoryId", MaterialCategoryId);
                cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                cmd.Parameters.AddWithValue("@Remark", Remark);

                cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                cmd.ExecuteNonQuery();

                tran.Commit();
                con.Close();
                cmd.Parameters.Clear();
                Gv_MaterialCategory.EditIndex = -1;
                List_MaterialCategory.Clear();
                Fill_MaterialCategory();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Category Name Updated Successfully')", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
            }

        }
    }
    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        Button Btn_Delete = sender as Button;
        int MaterialCategoryId = Convert.ToInt32(Btn_Delete.CommandArgument.ToString());
        con.ConnectionString = str;
        cmd = new SqlCommand();
        cmd.CommandText = "update tbl_MaterialCategories set IsDeleted=1 where  MaterialCategoryId=" + MaterialCategoryId;
        cmd.Connection = con;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        Gv_MaterialCategory.EditIndex = -1;
        List_MaterialCategory.Clear();
        Fill_MaterialCategory();
    }
}