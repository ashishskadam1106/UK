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

public partial class User_Control_Dashboard1 : System.Web.UI.UserControl
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    int Employee_Id = 0, ServiceCentre_Id = 0, ServiceCentreCompany_Id=0;
    string UserName;
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
           

            int DurationId = Convert.ToInt32(Dd_Duration.SelectedValue);
            Fill_NewStudent(DurationId);
            Fill_StudentBalance(DurationId);
            Fill_StudentModal(DurationId);
            Fill_StudentBalanceModal(DurationId);
        }
    }
    protected void Dd_Duration_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DurationId = Convert.ToInt32(Dd_Duration.SelectedValue);
        Fill_NewStudent(DurationId);
        Fill_StudentBalance(DurationId);
        Fill_StudentModal(DurationId);
        Fill_StudentBalanceModal(DurationId);
    }
    #region Fill_ModalGrids
  
    private void Fill_StudentModal(int Duration_Id)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_Get_StudentRegistrationData";
            cmd.Parameters.AddWithValue("@Duration_Id", Duration_Id);
           

            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Vehicles");
            Gv_Student.DataSource = ds.Tables["Vehicles"];
            Gv_Student.DataBind();
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
    private void Fill_StudentBalanceModal(int Duration_Id)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_Get_StudentDojoBalanceData";
            cmd.Parameters.AddWithValue("@Duration_Id", Duration_Id);


            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Vehicles");
            Gv_StudentBalance.DataSource = ds.Tables["Vehicles"];
            Gv_StudentBalance.DataBind();
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

    #region Fill Counts
  
    private void Fill_NewStudent(int Duration_Id)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Get_Student_Count";
            cmd.Parameters.AddWithValue("@Duration_Id", Duration_Id);

            SqlParameter OP_FromDate = new SqlParameter();
            OP_FromDate.Direction = ParameterDirection.Output;
            OP_FromDate.ParameterName = "@From";
            OP_FromDate.DbType = DbType.DateTime;
            cmd.Parameters.Add(OP_FromDate);

            SqlParameter OP_ToDate = new SqlParameter();
            OP_ToDate.Direction = ParameterDirection.Output;
            OP_ToDate.ParameterName = "@To";
            OP_ToDate.DbType = DbType.DateTime;
            cmd.Parameters.Add(OP_ToDate);

            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Lbl_NewStudents.InnerText = dr[0].ToString();
            }
            dr.Close();
            string From = OP_FromDate.Value.ToString();
            string To = OP_ToDate.Value.ToString();
            if (Duration_Id != 1)
            {
                Lbl_DataDate.Text = "Data from " + From.Replace("00:00:00", "") + " to " + To.Replace("00:00:00", "");
            }
            else
            {
                Lbl_DataDate.Text = "Data For date " + DateTime.Now.ToShortDateString();
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

    private void Fill_StudentBalance(int Duration_Id)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Get_StudentDojoBalance_Count";
            cmd.Parameters.AddWithValue("@Duration_Id", Duration_Id);

            SqlParameter OP_FromDate = new SqlParameter();
            OP_FromDate.Direction = ParameterDirection.Output;
            OP_FromDate.ParameterName = "@From";
            OP_FromDate.DbType = DbType.DateTime;
            cmd.Parameters.Add(OP_FromDate);

            SqlParameter OP_ToDate = new SqlParameter();
            OP_ToDate.Direction = ParameterDirection.Output;
            OP_ToDate.ParameterName = "@To";
            OP_ToDate.DbType = DbType.DateTime;
            cmd.Parameters.Add(OP_ToDate);

            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Lbl_StudentDojoBalance.InnerText = dr[0].ToString();
            }
            dr.Close();
            string From = OP_FromDate.Value.ToString();
            string To = OP_ToDate.Value.ToString();
            if (Duration_Id != 1)
            {
                Lbl_DataDate.Text = "Data from " + From.Replace("00:00:00", "") + " to " + To.Replace("00:00:00", "");
            }
            else
            {
                Lbl_DataDate.Text = "Data For date " + DateTime.Now.ToShortDateString();
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

    protected void Gv_Student_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int Duration_Id = Convert.ToInt32(Dd_Duration.SelectedValue);
        Gv_Student.PageIndex = e.NewPageIndex;
        Fill_StudentModal(Duration_Id);
    }
    protected void Gv_StudentBalance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int DuratioId = Convert.ToInt32(Dd_Duration.SelectedValue);
        Gv_StudentBalance.PageIndex = e.NewPageIndex;
        Fill_StudentBalanceModal(DuratioId);
    }
}