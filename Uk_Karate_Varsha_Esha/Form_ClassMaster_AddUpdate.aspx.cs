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

public partial class Form_ClassMaster_AddUpdate : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    string UserName = "";
    int ClassId_ToUpdate, EmpolyeeId;
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
            ClassId_ToUpdate = Convert.ToInt32(Session["ClassId_ToUpdate"].ToString());
            //SocietyId = Convert.ToInt32(Session["SocietyId"].ToString());


            if (!IsPostBack)
            {
                if (ClassId_ToUpdate != 0)
                {
                    Lbl_Heading.Text = "Update Class Details";
                    Fill_ClassDetails(ClassId_ToUpdate);

                    // Fill_Building(BuildingId_ToUpdate);
                }
            }

        }
    }
    private void Fill_ClassDetails(int ClassId_ToUpdate)
    {
        try
        {
            con.ConnectionString = str;


            cmd = new SqlCommand();

            cmd.CommandText = "select Class,ClassCode,Remark from tbl_Classes where ClassId=" + ClassId_ToUpdate;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Tb_ClassName.Text = dr[0].ToString();
                Tb_ClassCode.Text = dr[1].ToString();
                Tb_Description.Text = dr[2].ToString();
            }
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occure:" + ex.Message.ToString() + "')", true);
        }
    }

    private int ValidateClass()
    {
        int Go = 1;
        string ValidationMsg = "";

        if (Tb_ClassName.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter class name.\\n";
            Tb_ClassName.BorderColor = Color.Red;
        }
        else
        {
            Tb_ClassName.BorderColor = Color.LightGray;
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
        string Remark = "", ClassName = "", ClassCode = "";
        DateTime ModifiedDate = Convert.ToDateTime("01-01-1900");
        DateTime DeletedDate = Convert.ToDateTime("01-01-1900");
        DateTime ApprovedDate = Convert.ToDateTime("01-01-1900");


        //Go = ValidateInput();

        if (Go == 1)
        {

            ClassName = Tb_ClassName.Text.ToString();
            ClassCode = Tb_ClassCode.Text.ToString();
            Remark = Tb_Description.Text.ToString();

            if (ClassId_ToUpdate == 0)
            {

                try
                {
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "USP_Class_Insert";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Class", ClassName);
                    cmd.Parameters.AddWithValue("@ClassCode", ClassCode);
                    cmd.Parameters.AddWithValue("@Remark", Remark);
                    //cmd.Parameters.AddWithValue("@ClassId", ClassId);
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

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Class added successfully');window.location='Form_ClassMaster.aspx'", true);


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
            else if (ClassId_ToUpdate != 0)
            {
                try
                {
                    con.ConnectionString = str;
                    con.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "USP_Class_Update";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassId", ClassId_ToUpdate);
                    cmd.Parameters.AddWithValue("@Class", ClassName);
                    cmd.Parameters.AddWithValue("@ClassCode", ClassCode);
                    cmd.Parameters.AddWithValue("@Remark", Remark);
                    cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@SocietyId", SocietyId);

                    cmd.ExecuteNonQuery();
                    con.Close();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Class details updated successfully');window.location='Form_ClassMaster.aspx'", true);


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
        Session["ClassId"] = "0";
        Response.Redirect(@"~\Form_ClassMaster_AddUpdate.aspx");
    }
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("Form_ClassMaster.aspx");
    }
}