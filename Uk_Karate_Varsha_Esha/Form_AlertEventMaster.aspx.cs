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
using System.Security.Cryptography;
using System.IO;


public partial class Form_AlertEventMaster : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    int Employee_Id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginUsername"] == null || Session["LoginAuthenticated"] == "No")
        {
            Response.Redirect(@"~\Index.aspx");
        }
        else
        {
            //UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());


            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
                Fill_AlertEvent();
            }
        }
    }
    private void Fill_AlertEvent()
    {
        con.ConnectionString = str;
        con.Open();
        cmd = new SqlCommand();
        cmd.CommandText = "USP_GetAlertEvent";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds, "Alert");

        Gv_AlertEventMaster.DataSource = ds.Tables["Alert"];
        Gv_AlertEventMaster.DataBind();
        con.Close();
    }

    protected void Gv_AlertEventMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_AlertEventMaster.PageIndex = e.NewPageIndex;
        Fill_AlertEvent();
    }
    protected void Gv_AlertEventMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        int AlertEventId_ToUpdate= Convert.ToInt32(Gv_AlertEventMaster.SelectedValue.ToString());
        Response.Redirect(@"~\Form_AlertEvent_AddUpdate.aspx?ID=" + encrypt(AlertEventId_ToUpdate.ToString()));
    }
    protected void Btn_Add_Click(object sender, EventArgs e)
    {
        Session["AlertEventId_ToUpdate"] = 0;
        Response.Redirect(@"~\Form_AlertEvent_AddUpdate.aspx");
    }

    #region Encrypt
    public string encrypt(string encryptString)
    {
        string EncryptionKey = "SOFTUKKARATE20192020ENCRYPTPRITAM5578";
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
    #endregion
}