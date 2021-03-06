﻿using System;
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
using CS_Encryption;

public partial class Form_PurchaseOrderBrowse : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    int Employee_Id, SupplierId_ToUpdate, SupplierID, PurchaseOrderHeaderId;
    string UserName, FromDate, ToDate;
    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);

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

            if (!IsPostBack)
            {
                Fill_Supplier();
                Fill_OrderNumber();
                if (DD_Supplier.SelectedIndex >= 0)
                {
                    SupplierID = Convert.ToInt32(DD_Supplier.SelectedValue.ToString());
                }
                if (Dd_OrderNumber.SelectedIndex >= 0)
                {
                    PurchaseOrderHeaderId = Convert.ToInt32(Dd_OrderNumber.SelectedValue.ToString());
                }
                Dp_OrderFromDate.Text = CurrentUtc_IND.Date.ToString().Replace(" 00:00:00", "");
                Dp_OrderToDate.Text = CurrentUtc_IND.Date.ToString().Replace(" 00:00:00", "");

                FromDate = Dp_OrderFromDate.Text.ToString();
                ToDate = Dp_OrderToDate.Text.ToString();
                Fill_PurchaseOrders(SupplierID, FromDate, ToDate,PurchaseOrderHeaderId, 2);
            }
        }
    }

    private void Fill_Supplier()
    {

        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Select SupplierId,SupplierName From tbl_Suppliers Where IsDeleted=0 Union  Select 0,'---Select---'";
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Supplier");
            DD_Supplier.DataSource = ds.Tables["Supplier"];
            DD_Supplier.DataBind();
            con.Close();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }

    private void Fill_OrderNumber()
    {

        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Select PurchaseOrderHeaderId,PurchaseOrderNumber from tbl_PurchaseOrderHeader Where IsDeleted=0 Union Select 0 as PurchaseOrderHeaderId,'---Select---' as PurchaseOrderNumber";
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Supplier");
            Dd_OrderNumber.DataSource = ds.Tables["Supplier"];
            Dd_OrderNumber.DataBind();
            con.Close();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }

    #region Validate_Dates
    private int Validate_Dates()
    {
        int Go = 1;
        string ValidationMsg = "";
        if (Dp_OrderFromDate.Text == "")
        {
            Go = 0;
            ValidationMsg += "Please Enter to date./";
            Dp_OrderFromDate.BorderColor = Color.Red;
        }
        else
        {
            Dp_OrderFromDate.BorderColor = Color.LightGray;
        }
        if (Dp_OrderToDate.Text == "")
        {
            Go = 0;
            ValidationMsg += "Please Enter to date";
            Dp_OrderToDate.BorderColor = Color.Red;
        }
        else
        {
            Dp_OrderToDate.BorderColor = Color.LightGray;
        }
        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :" + ValidationMsg.ToString() + "')", true);
        }
        return Go;
    }
    #endregion

    #region Fill_PurchaseOrders
    private void Fill_PurchaseOrders(int SupplierID, string InwardFromDate, string InwardToDate,int PurchaseOrderHeaderId, int CallFrom)
    {
         
        try
        {
            DateTime FromDate, ToDate;
            FromDate = Convert.ToDateTime(InwardFromDate);
            ToDate = Convert.ToDateTime(InwardToDate);
            con.ConnectionString = str;
            con.Open();

            cmd = new SqlCommand();
            cmd.CommandText = "USP_Get_PurchaseOrderDetail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
            cmd.Parameters.AddWithValue("@PurchaseOrderHeaderId", PurchaseOrderHeaderId);
            cmd.Parameters.AddWithValue("@FromDate", FromDate);
            cmd.Parameters.AddWithValue("@ToDate", ToDate);
            cmd.Connection = con;
            dr = cmd.ExecuteReader();

            String UnreadText = "";
            Int32 i = 0;

            if (dr.HasRows)
            {
                double Amount = 0;
                while (dr.Read())
                {
                    Amount = Convert.ToDouble(dr["TotalAmount"].ToString());
                    UnreadText += "<tr>";

                    UnreadText += "      <td class=\"c-grid-col-size-100\"><a href=\"#\">" + dr["SrNo"].ToString() + "</a></td>";
                    UnreadText += "      <td class=\"c-grid-col-size-150\">" + dr["SupplierName"].ToString() + "</td>";
                    UnreadText += "      <td class=\"c-grid-col-size-100\">" + dr["PurchaseOrderNumber"].ToString() + "</td>";
                    UnreadText += "      <td class=\"c-grid-col-size-100\">" + dr["PurchaseOrderDate"].ToString() + "</td>";
                    UnreadText += "      <td class=\"c-grid-col-size-100\">" + Amount.ToString("0.00") + "</td>";
                    UnreadText += "      <td class=\"c-grid-col-size-100\"><a class=\"btn bg-purple c-bg-blueish btn-block btn-flat\" href=\"Form_PurchaseOrderAddUpdate.aspx?ID=" + encrypt(dr["PurchaseOrderHeaderId"].ToString()) + "&Call=" + 3 + "\"><span>Edit Purchase<span></a></td>";

                    UnreadText += "</tr>";

                    UnreadText += "</tr>";
                    PurchaseBrowse_Body.InnerHtml = UnreadText;
                    i++;
                }
                dr.Close();
                con.Close();
            }
            else
            {
                PurchaseBrowse_Body.InnerHtml = UnreadText;
            }
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


    protected void Btn_Refresh_Click(object sender, EventArgs e)
    {
        int Go = 1;
        Go = Validate_Dates();

        if (Go == 1)
        {
            string FromDate = Dp_OrderFromDate.Text.ToString();
            string ToDate = Dp_OrderToDate.Text.ToString();
            int SupplierID = Convert.ToInt32(DD_Supplier.SelectedValue);
            int PurchaseOrderHeaderId = Convert.ToInt32(Dd_OrderNumber.SelectedValue);
            Fill_PurchaseOrders(SupplierID, FromDate, ToDate,PurchaseOrderHeaderId, 2);//for dates
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