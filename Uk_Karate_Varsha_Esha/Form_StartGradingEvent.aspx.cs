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
using System.Security.Cryptography;
using System.Text;

public partial class Form_StartGradingEvent : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;
    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);
    string UserName = "";
    int Employee_Id;

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

            //  ServiceCentre_Id = Convert.ToInt32(Session["ServiceCentre_Id"].ToString());
            UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());



            if (!IsPostBack)
            {
                Fill_Grade();
                Dp_StartDate.Text = CurrentUtc_IND.ToString("dd/MM/yyyy");

            }
        }
    }


    private void Fill_Grade()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select EventKyu_Id,EventKyu_Name from tbl_EventKyuMaster ";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds,"Belt");
            Dd_Title.DataSource = ds.Tables["Belt"];
            Dd_Title.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }




    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        Response.Redirect("Form_EventDetail.aspx");
    }

    protected void Dd_Title_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_Grade();
    }
    protected void Dd_Title_TextChanged(object sender, EventArgs e)
    {
        Fill_Grade();
    }
    protected void Btn_Search_Click(object sender, EventArgs e)
     {
        DateTime date = Convert.ToDateTime(Dp_StartDate.Text.ToString());
        int KyuId = 0;
        if (Dd_Title.SelectedIndex >= 0)
        {
            KyuId = Convert.ToInt32(Dd_Title.SelectedValue);
        }
       
        string EventLabel = Tb_EventLabel.Text;
        int EventHeaderId = 0;

        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "USP_StartEventInsert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventLabel", EventLabel);
            cmd.Parameters.AddWithValue("@EventDate", date);
            cmd.Parameters.AddWithValue("@EventKyuId", KyuId);
            cmd.Parameters.AddWithValue("@CreatedBy", UserName);
            cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);

            SqlParameter OP_EventHeaderId = new SqlParameter();
            OP_EventHeaderId.ParameterName = "@EventHeaderId";
            OP_EventHeaderId.DbType = DbType.Int32;
            OP_EventHeaderId.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(OP_EventHeaderId);


            cmd.ExecuteNonQuery();
            EventHeaderId =Convert.ToInt32(OP_EventHeaderId.Value.ToString());

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "window.location='Form_EventDetail.aspx?ID=" + encrypt(EventHeaderId.ToString()) + "&Call=" + encrypt("2") +"'", true);


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
}