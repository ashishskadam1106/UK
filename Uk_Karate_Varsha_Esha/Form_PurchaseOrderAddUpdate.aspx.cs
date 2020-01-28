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
//using System.Net.Http;
using System.Net;
using System.IO;
using CS_Encryption;
using System.Security.Cryptography;

public partial class Form_PurchaseOrderAddUpdate : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    SqlDataReader dr;
    DataSet ds;

    string UserName = "";
    int Employee_Id, SupplierId, PurchaseOrderHeaderId;
    int PurchaseOrderHeaderId_ToUpdate = 0;
    int CallFor = 0;

    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);
    public List<CS_PurchaseOrderDetail> List_PurchaseOrderDetail { get { return (List<CS_PurchaseOrderDetail>)Session["CS_PurchaseOrderDetail"]; } set { Session["CS_PurchaseOrderDetail"] = value; } }

    public int RowIndex { get { return (int)ViewState["RowIndex"]; } set { ViewState["RowIndex"] = value; } }

    public int IsCopyGridDone { get { return (int)ViewState["IsCopyGridDone"]; } set { ViewState["IsCopyGridDone"] = value; } }

    public int FillTBfrom_HF { get { return (int)ViewState["FillTBfrom_HF"]; } set { ViewState["FillTBfrom_HF"] = value; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginUsername"] == null || Session["LoginAuthenticated"] == "No")
        {
            Response.Redirect(@"~\Index.aspx");
        }
        else
        {
            UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();


            if (Request.QueryString.Count > 1)
            {
                try
                {
                    PurchaseOrderHeaderId_ToUpdate = Convert.ToInt32(Decrypt(Request.QueryString["ID"].ToString()));
                    CallFor = Convert.ToInt32(Request.QueryString["Call"]);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid Operation');window.location='Home.aspx'", true);
                }
            }
            if (!IsPostBack)
            {
                RowIndex = 0;
                FillTBfrom_HF = 0;
                IsCopyGridDone = 1;
                CallFor = 0;
                List_PurchaseOrderDetail = new List<CS_PurchaseOrderDetail>();
                Dp_OrderDate.Text = DateTime.Now.ToShortDateString();



                Add_DummyRow();
                Hide_DummyRow();
                Fill_Material(-1);
                Fill_Supplier();
                Fill_DiscountType(-1);
                Gv_PurchaseOrderDetail.FooterRow.Enabled = false;
                Session["Purchase_Call"] = 0;

                if (PurchaseOrderHeaderId_ToUpdate != 0)
                {
                    Lbl_Heading.Text = "Edit Purchase";
                    Fill_PurchaseDetails(PurchaseOrderHeaderId_ToUpdate);

                }
                if (CallFor == 2) //view
                {

                    Lbl_Heading.Text = "View Purchase Order";
                    Gv_PurchaseOrderDetail.Enabled = false;
                    Gv_PurchaseOrderDetail.FooterRow.Visible = false;


                    Btn_Save.Enabled = false;
                    Gv_PurchaseOrderDetail.Columns[8].Visible = false;
                }
                if (CallFor == 3) //Edit
                {
                    Btn_Save.Enabled = true;
                    Gv_PurchaseOrderDetail.Columns[8].Visible = true;
                    Gv_PurchaseOrderDetail.FooterRow.Visible = true;
                }
            }
        }
    }
    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "SOFTUKKARATE11245PRITMEGHPAGEISSOFT";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    #region Fill_Material
    private void Fill_Material(int index)
    {
        DropDownList Dd_material;
        if (index == -1)
        {
            Dd_material = (DropDownList)Gv_PurchaseOrderDetail.FooterRow.FindControl("Dd_Material");
        }
        else
        {
            Dd_material = (DropDownList)Gv_PurchaseOrderDetail.Rows[index].FindControl("Dd_Material");
        }
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "select MaterialMasterId,MaterialName from tbl_MaterialMaster Where IsDeleted=0 union select 0,'---select---' ";
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Material");
            Dd_material.DataSource = ds.Tables["Material"];
            Dd_material.DataBind();
            con.Close();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    #endregion

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
            Dd_SupplierName.DataSource = ds.Tables["Supplier"];
            Dd_SupplierName.DataBind();
            con.Close();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    #region Add & Hide Dummy row
    private void Add_DummyRow()
    {
        if (List_PurchaseOrderDetail.Count == 0)
        {
            List_PurchaseOrderDetail.Add(new CS_PurchaseOrderDetail
            {
                Index = -1,
                MaterialName = "",
                Quantity = 0,
                Rate = 0,
            });
        }
        Gv_PurchaseOrderDetail.DataSource = List_PurchaseOrderDetail;
        Gv_PurchaseOrderDetail.DataBind();
    }

    private void Hide_DummyRow()
    {
        if (List_PurchaseOrderDetail[0].Index == -1 && List_PurchaseOrderDetail[0].Quantity == 0)
        {
            Label Lbl_Material = (Label)Gv_PurchaseOrderDetail.Rows[0].FindControl("Lbl_Material");
            Label Lbl_SrNo = (Label)Gv_PurchaseOrderDetail.Rows[0].FindControl("Lbl_SrNo");
            Label Lbl_Discount_Type = (Label)Gv_PurchaseOrderDetail.Rows[0].FindControl("Lbl_Discount_Type");

            DropDownList Dd_Discount_Type = (DropDownList)Gv_PurchaseOrderDetail.Rows[0].FindControl("Dd_Discount_Type");

            TextBox Tb_Rate = (TextBox)Gv_PurchaseOrderDetail.Rows[0].FindControl("Tb_Rate");
            TextBox Tb_PurchaseQuantity = (TextBox)Gv_PurchaseOrderDetail.Rows[0].FindControl("Tb_PurchaseQuantity");
            TextBox Tb_DiscountAmount = (TextBox)Gv_PurchaseOrderDetail.Rows[0].FindControl("Tb_DiscountAmount");
            TextBox Tb_DiscountPer = (TextBox)Gv_PurchaseOrderDetail.Rows[0].FindControl("Tb_DiscountPer");

            TextBox Tb_Amount = (TextBox)Gv_PurchaseOrderDetail.Rows[0].FindControl("Tb_Amount");
            Button Btn_Delete = (Button)Gv_PurchaseOrderDetail.Rows[0].FindControl("Btn_Delete");

            Lbl_SrNo.Text = "";
            Lbl_Material.Text = "";
            Lbl_Discount_Type.Text = "";

            Tb_Rate.Visible = false;
            Tb_PurchaseQuantity.Visible = false;
            Tb_DiscountAmount.Visible = false;
            Tb_DiscountPer.Visible = false;

            Tb_Amount.Visible = false;

            Dd_Discount_Type.Visible = false;

            Btn_Delete.Visible = false;

        }
    }

    #endregion



    protected void Gv_PurchaseOrderDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Gv_PurchaseOrderDetail.EditIndex = -1;
        Gv_PurchaseOrderDetail.DataSource = List_PurchaseOrderDetail;
        Gv_PurchaseOrderDetail.DataBind();
        Fill_Material(-1);
        Fill_DiscountType(-1);
    }
    protected void Gv_PurchaseOrderDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int AlreadyExists = -2;
        try
        {
            #region
            if (e.CommandName.Equals("Add"))
            {
                //Find Footer Controls and HFs
                DropDownList Dd_Material = (DropDownList)Gv_PurchaseOrderDetail.FooterRow.FindControl("Dd_Material");
                DropDownList Dd_Discount_Type = (DropDownList)Gv_PurchaseOrderDetail.FooterRow.FindControl("Dd_Discount_Type");

                TextBox Tb_Rate = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_Rate");
                TextBox Tb_PurchaseQuantity = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_PurchaseQuantity");
                TextBox Tb_DiscountPer = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_DiscountPer");
                TextBox Tb_DiscountAmount = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_DiscountAmount");
                TextBox Tb_Amount = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_Amount");

                HiddenField HF_DiscAmount = (HiddenField)Gv_PurchaseOrderDetail.FooterRow.FindControl("HF_DiscAmount");
                HiddenField HF_ItemTotal = (HiddenField)Gv_PurchaseOrderDetail.FooterRow.FindControl("HF_ItemTotal");
                HiddenField Hf_DiscountTypeId = (HiddenField)Gv_PurchaseOrderDetail.FooterRow.FindControl("Hf_DiscountTypeId");

                int Go = 1;
                Go = Validate_Material_Details(1, 0); //Footer Row

                if (Go == 1)
                {
                    //get updated footer data from hf.
                    if (HF_DiscAmount.Value != "0")
                    {
                        // Lbl_DiscountAmount.Text = HF_DiscAmount.Value;
                        Tb_DiscountAmount.Text = HF_DiscAmount.Value;
                    }
                    if (HF_ItemTotal.Value != "0")
                    {
                        Tb_TotalAmount.Text = HF_ItemTotal.Value;
                    }

                    if (List_PurchaseOrderDetail.Count > 0 && List_PurchaseOrderDetail[0].Index != -1 && List_PurchaseOrderDetail[0].MaterialMasterId != 0)
                    {
                        Copy_HiddenFieldsToTextbox();
                        Copy_GridToList(-5); //par should <0
                    }

                    if (List_PurchaseOrderDetail.Count == 1 && List_PurchaseOrderDetail[0].Index == -1 && List_PurchaseOrderDetail[0].Quantity == 0)
                    {
                        List_PurchaseOrderDetail.RemoveAt(0);
                    }

                    for (int j = 0; j < List_PurchaseOrderDetail.Count; j++)
                    {
                        if (Convert.ToInt32(Dd_Material.SelectedValue) == List_PurchaseOrderDetail[j].MaterialMasterId)
                        {
                            AlreadyExists = j;
                            break;
                        }
                    }

                    if (AlreadyExists == -2) //different material
                    {
                        int IndexToAdd = List_PurchaseOrderDetail.Count;
                        List_PurchaseOrderDetail.Add(new CS_PurchaseOrderDetail()
                        {
                            Index = IndexToAdd,
                            PurchaseOrderDetailId = 0,
                            MaterialMasterId = Convert.ToInt32(Dd_Material.SelectedValue),
                            MaterialName = Dd_Material.SelectedItem.ToString(),
                            Quantity = Convert.ToDouble(Tb_PurchaseQuantity.Text.ToString()),
                            Rate = Convert.ToSingle(Tb_Rate.Text),
                            DiscountTypeId = Convert.ToInt32(Dd_Discount_Type.SelectedValue),
                            DiscountType = Dd_Discount_Type.SelectedItem.ToString(),
                            DiscountPer = Convert.ToSingle(Tb_DiscountPer.Text),
                            DiscountAmount = Convert.ToSingle(Tb_DiscountAmount.Text),
                            TotalAmount = Math.Round(Convert.ToSingle(Tb_Amount.Text), 2),

                        });
                    }
                    else //same material insert again.    
                    {
                        double DiscountAmount = 0, NewRate = 0, TaxableAmount = 0;
                        double LineTotal = 0;

                        List_PurchaseOrderDetail[AlreadyExists].Quantity += Convert.ToDouble(Tb_PurchaseQuantity.Text.ToString());

                        DiscountAmount = (List_PurchaseOrderDetail[AlreadyExists].Rate * List_PurchaseOrderDetail[AlreadyExists].DiscountPer / 100);
                        NewRate = List_PurchaseOrderDetail[AlreadyExists].Rate - DiscountAmount;
                        TaxableAmount = NewRate * List_PurchaseOrderDetail[AlreadyExists].Quantity;
                        LineTotal = TaxableAmount;

                        List_PurchaseOrderDetail[AlreadyExists].DiscountAmount = Convert.ToSingle(DiscountAmount);
                        List_PurchaseOrderDetail[AlreadyExists].TotalAmount = Convert.ToSingle(LineTotal);
                    }

                    Double TotalAmount = 0;
                    for (int i = 0; i < List_PurchaseOrderDetail.Count; i++)
                    {
                        List_PurchaseOrderDetail[i].Index = i;
                        List_PurchaseOrderDetail[i].SrNo = i + 1;

                        TotalAmount += List_PurchaseOrderDetail[i].TotalAmount;
                    }
                    Gv_PurchaseOrderDetail.DataSource = null;
                    Gv_PurchaseOrderDetail.DataSource = List_PurchaseOrderDetail;
                    Gv_PurchaseOrderDetail.DataBind();

                    Tb_TotalAmount.Text = TotalAmount.ToString();
                    Gv_PurchaseOrderDetail.EditIndex = -1;

                    Fill_Material(-1);
                    Fill_DiscountType(-1);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "CalculateTotal();", true);
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error:" + ex.Message.ToString() + "')", true);
        }
    }


    private void Copy_HiddenFieldsToTextbox()
    {
        try
        {
            foreach (GridViewRow Row in Gv_PurchaseOrderDetail.Rows)
            {
                TextBox Tb_DiscountAmount = (TextBox)Row.FindControl("Tb_DiscountAmount");
                HiddenField HF_DiscAmount = (HiddenField)Row.FindControl("HF_DiscAmount");
                Tb_DiscountAmount.Text = HF_DiscAmount.Value;

                TextBox Tb_Amount = (TextBox)Row.FindControl("Tb_Amount");
                HiddenField HF_ItemTotal = (HiddenField)Row.FindControl("HF_ItemTotal");
                Tb_Amount.Text = HF_ItemTotal.Value;

                Tb_TotalAmount.Text = HF_ItemTotal.Value;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    private void Copy_GridToList(int IndexToSkip)
    {
        int MaterialId = 0, Go = 1, Go1 = 1, DiscountTypeId = 0;
        string Item_Name = "";
        float Quantity = 0, Rate = 0, DiscountPer = 0, DiscountAmount = 0, Total = 0;

        try
        {
            IsCopyGridDone = 1;

            foreach (GridViewRow row in Gv_PurchaseOrderDetail.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (row.RowIndex != IndexToSkip)
                    {
                        HiddenField Hf_MaterialId = (HiddenField)row.FindControl("Hf_MaterialId");

                        if (Hf_MaterialId.Value != "0") //if not dummy row
                        {
                            RowIndex = row.RowIndex;

                            if (Go == 1 && Go1 == 1)
                            {
                                Label Lbl_ItemName = (Label)row.FindControl("Lbl_Material");
                                TextBox Tb_Rate = (TextBox)row.FindControl("Tb_Rate");
                                TextBox Tb_Quantity = (TextBox)row.FindControl("Tb_PurchaseQuantity");
                                TextBox Tb_DiscPer = (TextBox)row.FindControl("Tb_DiscountPer");
                                TextBox Tb_DiscountAmount = (TextBox)row.FindControl("Tb_DiscountAmount");
                                TextBox Tb_Amount = (TextBox)row.FindControl("Tb_Amount");

                                DropDownList Dd_Discount_Type = (DropDownList)row.FindControl("Dd_Discount_Type");

                                MaterialId = Convert.ToInt32(Hf_MaterialId.Value);
                                Item_Name = Lbl_ItemName.Text.ToString();


                                Quantity = Convert.ToSingle(Tb_Quantity.Text);
                                Rate = Convert.ToSingle(Tb_Rate.Text);
                                DiscountTypeId = Convert.ToInt32(Dd_Discount_Type.SelectedValue);
                                DiscountPer = Convert.ToSingle(Tb_DiscPer.Text);
                                DiscountAmount = Convert.ToSingle(Tb_DiscountAmount.Text);
                                Total = Convert.ToSingle(Tb_Amount.Text);

                                List_PurchaseOrderDetail[RowIndex].MaterialMasterId = MaterialId;
                                List_PurchaseOrderDetail[RowIndex].Rate = Rate;
                                List_PurchaseOrderDetail[RowIndex].Quantity = Quantity;
                                List_PurchaseOrderDetail[RowIndex].DiscountTypeId = DiscountTypeId;
                                List_PurchaseOrderDetail[RowIndex].DiscountPer = DiscountPer;
                                List_PurchaseOrderDetail[RowIndex].DiscountAmount = DiscountAmount;
                                List_PurchaseOrderDetail[RowIndex].TotalAmount = Total;
                            }
                            else
                            {
                                IsCopyGridDone = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    private int Validate_Material_Details(int CallFor, int RowIndex)
    {
        //if (CallFor == 1) //Footer Row
        //{
        DropDownList Dd_Material = (DropDownList)Gv_PurchaseOrderDetail.FooterRow.FindControl("Dd_Material");
        TextBox Tb_Rate = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_Rate");
        TextBox Tb_PurchaseQuantity = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_PurchaseQuantity");

        int Go = 1;
        string ValidationMsg = "";
        double Rate = 0, Quantity = 0;

        if (Dd_Material.SelectedIndex <= 0)
        {
            Go = 0;
            Dd_Material.BorderColor = Color.Red;
            ValidationMsg += "Please select Material. \\n";
        }
        else
        {
            Dd_Material.BorderColor = Color.LightGray;
        }

        try
        {
            Rate = Convert.ToDouble(Tb_Rate.Text.ToString());
            if (Rate <= 0)
            {
                Go = 0;
                Tb_Rate.BorderColor = Color.Red;
                ValidationMsg += "Enter Rate Greater than zero.\\n ";
            }
            else
            {
                Tb_Rate.BorderColor = Color.LightGray;
            }
        }
        catch
        {
            Go = 0;
            Tb_Rate.BorderColor = Color.Red;
            ValidationMsg += "Enter valid Rate.\\n";
        }
        try
        {
            Quantity = Convert.ToDouble(Tb_PurchaseQuantity.Text.ToString());
            if (Quantity <= 0)
            {
                Go = 0;
                Tb_PurchaseQuantity.BorderColor = Color.Red;
                ValidationMsg += "Enter Quantity Greater than zero.\\n ";
            }
            else
            {
                Tb_PurchaseQuantity.BorderColor = Color.LightGray;
            }
        }
        catch
        {
            Go = 0;
            Tb_PurchaseQuantity.BorderColor = Color.Red;
            ValidationMsg += "Enter valid quantity.\\n";
        }
        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :\\n" + ValidationMsg.ToString() + "')", true);
        }
        return Go;
    }

    protected void Gv_PurchaseOrderDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void Gv_PurchaseOrderDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gv_PurchaseOrderDetail.PageIndex = e.NewPageIndex;
        Gv_PurchaseOrderDetail.DataSource = List_PurchaseOrderDetail;
        Gv_PurchaseOrderDetail.DataBind();

        Fill_Material(-1);
        Fill_DiscountType(-1);
    }
    protected void Gv_PurchaseOrderDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Index = (e.Row.RowIndex) + 1;
            TextBox Tb_Item_Rate = (TextBox)e.Row.FindControl("Tb_Rate");
            TextBox Tb_DiscPer = (TextBox)e.Row.FindControl("Tb_DiscountPer");
            TextBox Tb_DiscountAmount = (TextBox)e.Row.FindControl("Tb_DiscountAmount");
            TextBox Tb_Quantity = (TextBox)e.Row.FindControl("Tb_PurchaseQuantity");
            TextBox Tb_Amount = (TextBox)e.Row.FindControl("Tb_Amount");

            HiddenField HF_DiscAmount = (HiddenField)e.Row.FindControl("HF_DiscAmount");
            HiddenField HF_ItemTotal = (HiddenField)e.Row.FindControl("HF_ItemTotal");

            HiddenField Hf_DiscountTypeId = (HiddenField)e.Row.FindControl("Hf_DiscountTypeId");
            DropDownList Dd_Discount_Type = (DropDownList)e.Row.FindControl("Dd_Discount_Type");


            string Q = "Select DiscountTypeId,DiscountType FROM tbl_DiscountType";
            DataSet Ds = CS_Encrypt.GetData(Q, str);
            Dd_Discount_Type.DataSource = Ds.Tables[0];
            Dd_Discount_Type.DataBind();


            if (!String.IsNullOrEmpty(Hf_DiscountTypeId.Value))
            {
                Dd_Discount_Type.SelectedIndex = Dd_Discount_Type.Items.IndexOf(Dd_Discount_Type.Items.FindByValue(Hf_DiscountTypeId.Value));
            }


            Tb_Item_Rate.Attributes.Add("onChange", "javascript:CalculateTaxable(" + Index + "," + Tb_Item_Rate.ClientID + "," + Tb_DiscPer.ClientID + "," + Tb_DiscountAmount.ClientID + "," + Tb_Quantity.ClientID + "," + HF_DiscAmount.ClientID + "," + Dd_Discount_Type.ClientID + "," + Tb_Amount.ClientID + "," + HF_ItemTotal.ClientID + ");");
            Tb_Quantity.Attributes.Add("onChange", "javascript:CalculateTaxable(" + Index + "," + Tb_Item_Rate.ClientID + "," + Tb_DiscPer.ClientID + "," + Tb_DiscountAmount.ClientID + "," + Tb_Quantity.ClientID + "," + HF_DiscAmount.ClientID + "," + Dd_Discount_Type.ClientID + "," + Tb_Amount.ClientID + "," + HF_ItemTotal.ClientID + ");");
            Tb_DiscPer.Attributes.Add("onChange", "javascript:CalculateTaxable(" + Index + "," + Tb_Item_Rate.ClientID + "," + Tb_DiscPer.ClientID + "," + Tb_DiscountAmount.ClientID + "," + Tb_Quantity.ClientID + "," + HF_DiscAmount.ClientID + "," + Dd_Discount_Type.ClientID + "," + Tb_Amount.ClientID + "," + HF_ItemTotal.ClientID + ");");
            Tb_DiscountAmount.Attributes.Add("onChange", "javascript:CalculateTaxable(" + Index + "," + Tb_Item_Rate.ClientID + "," + Tb_DiscPer.ClientID + "," + Tb_DiscountAmount.ClientID + "," + Tb_Quantity.ClientID + "," + HF_DiscAmount.ClientID + "," + Dd_Discount_Type.ClientID + "," + Tb_Amount.ClientID + "," + HF_ItemTotal.ClientID + ");");
            Dd_Discount_Type.Attributes.Add("onChange", "javascript:CalculateTaxable(" + Index + "," + Tb_Item_Rate.ClientID + "," + Tb_DiscPer.ClientID + "," + Tb_DiscountAmount.ClientID + "," + Tb_Quantity.ClientID + "," + HF_DiscAmount.ClientID + "," + Dd_Discount_Type.ClientID + "," + Tb_Amount.ClientID + "," + HF_ItemTotal.ClientID + ");");


        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            int Index = (e.Row.RowIndex) + 1;
            TextBox Tb_Item_Rate = (TextBox)e.Row.FindControl("Tb_Rate");
            TextBox Tb_DiscPer = (TextBox)e.Row.FindControl("Tb_DiscountPer");

            TextBox Tb_DiscountAmount = (TextBox)e.Row.FindControl("Tb_DiscountAmount");
            TextBox Tb_Quantity = (TextBox)e.Row.FindControl("Tb_PurchaseQuantity");

            TextBox Tb_Amount = (TextBox)e.Row.FindControl("Tb_Amount");

            HiddenField HF_DiscAmount = (HiddenField)e.Row.FindControl("HF_DiscAmount");
            HiddenField HF_ItemTotal = (HiddenField)e.Row.FindControl("HF_ItemTotal");
            DropDownList Dd_Discount_Type = (DropDownList)e.Row.FindControl("Dd_Discount_Type");
            HiddenField Hf_DiscountTypeId = (HiddenField)e.Row.FindControl("Hf_DiscountTypeId");


            Tb_Item_Rate.Attributes.Add("onChange", "javascript:CalculateTaxable(" + Index + "," + Tb_Item_Rate.ClientID + "," + Tb_DiscPer.ClientID + "," + Tb_DiscountAmount.ClientID + "," + Tb_Quantity.ClientID + "," + HF_DiscAmount.ClientID + "," + Dd_Discount_Type.ClientID + "," + Tb_Amount.ClientID + "," + HF_ItemTotal.ClientID + ");");
            Tb_Quantity.Attributes.Add("onChange", "javascript:CalculateTaxable(" + Index + "," + Tb_Item_Rate.ClientID + "," + Tb_DiscPer.ClientID + "," + Tb_DiscountAmount.ClientID + "," + Tb_Quantity.ClientID + "," + HF_DiscAmount.ClientID + "," + Dd_Discount_Type.ClientID + "," + Tb_Amount.ClientID + "," + HF_ItemTotal.ClientID + ");");
            Tb_DiscPer.Attributes.Add("onChange", "javascript:CalculateTaxable(" + Index + "," + Tb_Item_Rate.ClientID + "," + Tb_DiscPer.ClientID + "," + Tb_DiscountAmount.ClientID + "," + Tb_Quantity.ClientID + "," + HF_DiscAmount.ClientID + "," + Dd_Discount_Type.ClientID + "," + Tb_Amount.ClientID + "," + HF_ItemTotal.ClientID + ");");
            Tb_DiscountAmount.Attributes.Add("onChange", "javascript:CalculateTaxable(" + Index + "," + Tb_Item_Rate.ClientID + "," + Tb_DiscPer.ClientID + "," + Tb_DiscountAmount.ClientID + "," + Tb_Quantity.ClientID + "," + HF_DiscAmount.ClientID + "," + Dd_Discount_Type.ClientID + "," + Tb_Amount.ClientID + "," + HF_ItemTotal.ClientID + ");");
            Dd_Discount_Type.Attributes.Add("onChange", "javascript:CalculateTaxable(" + Index + "," + Tb_Item_Rate.ClientID + "," + Tb_DiscPer.ClientID + "," + Tb_DiscountAmount.ClientID + "," + Tb_Quantity.ClientID + "," + HF_DiscAmount.ClientID + "," + Dd_Discount_Type.ClientID + "," + Tb_Amount.ClientID + "," + HF_ItemTotal.ClientID + ");");


        }
    }
    protected void Dd_Material_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList Dd_Material = (DropDownList)Gv_PurchaseOrderDetail.FooterRow.FindControl("Dd_Material");

        int MaterialMasterId = Convert.ToInt32(Dd_Material.SelectedValue);
        if (MaterialMasterId != 0)
        {
            Gv_PurchaseOrderDetail.FooterRow.Enabled = true;
            Fill_MaterialDetails(MaterialMasterId);
        }
        else
        {
            Gv_PurchaseOrderDetail.FooterRow.Enabled = false;


        }
    }


    private void Fill_MaterialDetails(int MaterialMasterId)
    {

        try
        {
            DropDownList Dd_Material = (DropDownList)Gv_PurchaseOrderDetail.FooterRow.FindControl("Dd_Material");
            TextBox Tb_Rate = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_Rate");
            TextBox Tb_Quantity = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_PurchaseQuantity");

            TextBox Tb_DiscountPer = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_DiscountPer");
            TextBox Tb_DiscountAmount = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_DiscountAmount");
            TextBox Tb_AmountTotal = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_Amount");

            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Select Convert(decimal(18,2),PurchasePrice)PurchasePrice,0 DiscPer,0 DiscAmount from tbl_MaterialMaster Where MaterialMasterId=" + MaterialMasterId;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Tb_Rate.Text = dr["PurchasePrice"].ToString();
                Tb_Quantity.Text = "1";
                Tb_DiscountPer.Text = dr["DiscPer"].ToString();
                Tb_DiscountAmount.Text = dr["DiscAmount"].ToString();
                Tb_AmountTotal.Text = dr["PurchasePrice"].ToString();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    protected void Fill_SupplierDetails(int SupplierId)
    {

        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Select SupplierAddress,Isnull(ContactMobile,ContactOffice)ContactMobile,EMailId from tbl_Suppliers Where SupplierId=" + SupplierId;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Tb_SupplierAddress.Text = dr["SupplierAddress"].ToString();
                Tb_MobileNumber.Text = dr["ContactMobile"].ToString();
                Tb_EmailId.Text = dr["EMailId"].ToString();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }

    protected void Dd_Discount_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DiscountTypeId = 0;

        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;
        DropDownList Dd_DiscountType; TextBox Tb_DiscountPer; TextBox Tb_DiscountAmount;
        // HiddenField Hf_DiscountTypeId;
        if (index == -1)
        {
            Dd_DiscountType = (DropDownList)Gv_PurchaseOrderDetail.FooterRow.FindControl("Dd_Discount_Type");
            Tb_DiscountPer = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_DiscountPer");
            Tb_DiscountAmount = (TextBox)Gv_PurchaseOrderDetail.FooterRow.FindControl("Tb_DiscountAmount");
        }
        else
        {
            Dd_DiscountType = (DropDownList)Gv_PurchaseOrderDetail.Rows[index].FindControl("Dd_Discount_Type");
            Tb_DiscountPer = (TextBox)Gv_PurchaseOrderDetail.Rows[index].FindControl("Tb_DiscountPer");
            Tb_DiscountAmount = (TextBox)Gv_PurchaseOrderDetail.Rows[index].FindControl("Tb_DiscountAmount");

        }


        if (Dd_DiscountType.SelectedIndex >= 0)
        {
            DiscountTypeId = Convert.ToInt32(Dd_DiscountType.SelectedValue);

        }
        if (DiscountTypeId == 1)
        {
            Tb_DiscountPer.Text = "0.00";
            Tb_DiscountPer.Enabled = false;
            Tb_DiscountAmount.Enabled = true;
        }
        else
        {
            if (DiscountTypeId == 2)
            {
                Tb_DiscountPer.Enabled = true;
                Tb_DiscountAmount.Enabled = false;
            }

        }
    }

    private void Fill_DiscountType(int index)
    {
        DropDownList Dd_Discount_Type;
        if (index == -1)
        {
            Dd_Discount_Type = (DropDownList)Gv_PurchaseOrderDetail.FooterRow.FindControl("Dd_Discount_Type");
        }
        else
        {
            Dd_Discount_Type = (DropDownList)Gv_PurchaseOrderDetail.Rows[index].FindControl("Dd_Discount_Type");
        }
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Select DiscountTypeId,DiscountType FROM tbl_DiscountType";
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Service");
            Dd_Discount_Type.DataSource = ds.Tables["Service"];
            Dd_Discount_Type.DataBind();
            con.Close();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }
    protected void Btn_Delete_Click(object sender, EventArgs e)
    {

    }
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        int Go = 1, SupplierId = 0;
        double TotalAmount = 0;
        string PurchaseOrderNumber = "";
        DateTime PurchaseOrderDate = Convert.ToDateTime("01-01-1900");
        Copy_GridToList(-5);
        Copy_HiddenFieldsToTextbox();

        Go = ValidationData();
        if (Go == 1)
        {
            if (Dd_SupplierName.SelectedIndex >= 0)
            {
                SupplierId = Convert.ToInt32(Dd_SupplierName.SelectedValue);
            }
            if (!String.IsNullOrEmpty(Dp_OrderDate.Text))
            {
                PurchaseOrderDate = Convert.ToDateTime(Dp_OrderDate.Text);
            }
            if (!String.IsNullOrEmpty(Tb_PurchaseOrderNumber.Text))
            {
                PurchaseOrderNumber = Tb_PurchaseOrderNumber.Text;
            }

            for (int i = 0; i < List_PurchaseOrderDetail.Count; i++)
            {
                TotalAmount += List_PurchaseOrderDetail[i].TotalAmount;

            }
            TotalAmount = Math.Round(TotalAmount, 2);

            if (PurchaseOrderHeaderId_ToUpdate == 0)
            {
                #region New
                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandText = "USP_PurchaseOrderHeaderInsert";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
                    cmd.Parameters.AddWithValue("@PurchaseOrderDate", PurchaseOrderDate);
                    cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                    cmd.Parameters.AddWithValue("@IsDeleted", 0);
                    cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                    cmd.Parameters.AddWithValue("@CreatedBy", UserName);

                    SqlParameter OP_PurchaseOrderHeaderId = new SqlParameter();
                    OP_PurchaseOrderHeaderId.ParameterName = "@PurchaseOrderHeaderId";
                    OP_PurchaseOrderHeaderId.DbType = DbType.Int32;
                    OP_PurchaseOrderHeaderId.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(OP_PurchaseOrderHeaderId);

                    SqlParameter OP_PurchaseOrderNumber = new SqlParameter();
                    OP_PurchaseOrderNumber.ParameterName = "@PurchaseOrderNumber";
                    OP_PurchaseOrderNumber.DbType = DbType.String;
                    OP_PurchaseOrderNumber.Size = 255;
                    OP_PurchaseOrderNumber.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(OP_PurchaseOrderNumber);
                    cmd.ExecuteNonQuery();

                    PurchaseOrderHeaderId = Convert.ToInt32(OP_PurchaseOrderHeaderId.Value.ToString());
                    PurchaseOrderNumber = OP_PurchaseOrderNumber.Value.ToString();


                    for (int i = 0; i < List_PurchaseOrderDetail.Count; i++)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "USP_PurchaseOrderDetailInsert";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PurchaseOrderHeaderId", PurchaseOrderHeaderId);
                        cmd.Parameters.AddWithValue("@MaterialId", List_PurchaseOrderDetail[i].MaterialMasterId);
                        cmd.Parameters.AddWithValue("@Rate", List_PurchaseOrderDetail[i].Rate);
                        cmd.Parameters.AddWithValue("@Quantity", List_PurchaseOrderDetail[i].Quantity);
                        cmd.Parameters.AddWithValue("@DiscountTypeId", List_PurchaseOrderDetail[i].DiscountTypeId);
                        cmd.Parameters.AddWithValue("@DiscountPercent", List_PurchaseOrderDetail[i].DiscountPer);
                        cmd.Parameters.AddWithValue("@DiscountAmount", List_PurchaseOrderDetail[i].DiscountAmount);
                        cmd.Parameters.AddWithValue("@TotalAmount", List_PurchaseOrderDetail[i].TotalAmount);
                        cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                        cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    con.Close();
                    cmd.Parameters.Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Purchase Added Successfully.');window.location='Form_PurchaseOrderAddUpdate.aspx?ID=" + encrypt(PurchaseOrderHeaderId.ToString()) + "&Call=2'", true);
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Purchase Added Successfully')", true);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                }
                #endregion
            }
            else
            {
                #region Update
                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandText = "USP_PurchaseOrderHeaderUpdate";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
                    cmd.Parameters.AddWithValue("@PurchaseOrderHeaderId",PurchaseOrderHeaderId_ToUpdate);
                    cmd.Parameters.AddWithValue("@PurchaseOrderDate", PurchaseOrderDate);
                    cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                    cmd.Parameters.AddWithValue("@ModifiedDate", CurrentUtc_IND);
                    cmd.Parameters.AddWithValue("@ModifiedBy", UserName);

                    cmd.ExecuteNonQuery();

                    for (int i = 0; i < List_PurchaseOrderDetail.Count; i++)
                    {
                        if (List_PurchaseOrderDetail[i].PurchaseOrderDetailId == 0) //add new item if added
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "USP_PurchaseOrderDetailInsert";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@PurchaseOrderHeaderId", PurchaseOrderHeaderId_ToUpdate);
                            cmd.Parameters.AddWithValue("@MaterialId", List_PurchaseOrderDetail[i].MaterialMasterId);
                            cmd.Parameters.AddWithValue("@Rate", List_PurchaseOrderDetail[i].Rate);
                            cmd.Parameters.AddWithValue("@Quantity", List_PurchaseOrderDetail[i].Quantity);
                            cmd.Parameters.AddWithValue("@DiscountTypeId", List_PurchaseOrderDetail[i].DiscountTypeId);
                            cmd.Parameters.AddWithValue("@DiscountPercent", List_PurchaseOrderDetail[i].DiscountPer);
                            cmd.Parameters.AddWithValue("@DiscountAmount", List_PurchaseOrderDetail[i].DiscountAmount);
                            cmd.Parameters.AddWithValue("@TotalAmount", List_PurchaseOrderDetail[i].TotalAmount);
                            cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                            cmd.Parameters.AddWithValue("@CreatedBy", UserName);

                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "USP_PurchaseOrderDetailUpdate";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@PurchaseOrderHeaderId", PurchaseOrderHeaderId);
                            cmd.Parameters.AddWithValue("@MaterialId", List_PurchaseOrderDetail[i].MaterialMasterId);
                            cmd.Parameters.AddWithValue("@Rate", List_PurchaseOrderDetail[i].Rate);
                            cmd.Parameters.AddWithValue("@Quantity", List_PurchaseOrderDetail[i].Quantity);
                            cmd.Parameters.AddWithValue("@DiscountTypeId", List_PurchaseOrderDetail[i].DiscountTypeId);
                            cmd.Parameters.AddWithValue("@DiscountPer", List_PurchaseOrderDetail[i].DiscountPer);
                            cmd.Parameters.AddWithValue("@DiscountAmount", List_PurchaseOrderDetail[i].DiscountAmount);
                            cmd.Parameters.AddWithValue("@TotalAmount", List_PurchaseOrderDetail[i].TotalAmount);
                            cmd.Parameters.AddWithValue("@ModifiedDate", CurrentUtc_IND);
                            cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
                        }
                    }
                    tran.Commit();
                    con.Close();
                    cmd.Parameters.Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Purchase Added Successfully.');window.location='Form_PurchaseOrderAddUpdate.aspx?ID=" + encrypt(PurchaseOrderHeaderId_ToUpdate.ToString()) + "&Call=2'", true);
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Purchase updated Successfully')", true);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                }

                #endregion
            }
        }

    }
    private int ValidationData()
    {
        int Go = 1;
        string ValidationMsg = "";
        if (Dp_OrderDate.Text == "")
        {
            Go = 0;
            ValidationMsg += "Please Enter Purchase Order Date.\\n";
            Dp_OrderDate.BorderColor = Color.Red;
        }
        else
        {
            Dp_OrderDate.BorderColor = Color.LightGray;
        }

        if (Dd_SupplierName.SelectedIndex <= 0)
        {
            Go = 0;
            ValidationMsg += "Please Select Supplier.\\n";
            Dd_SupplierName.BorderColor = Color.Red;
        }
        else
        {
            Dd_SupplierName.BorderColor = Color.LightGray;
        }
        if (List_PurchaseOrderDetail.Count != 0)
        {
            if (List_PurchaseOrderDetail[0].Index == -1)
            {
                Go = 0;
                ValidationMsg += "Please add atleast one purchase items .\\n";
            }
        }

        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :\\n" + ValidationMsg.ToString() + "')", true);
        }
        return Go;

    }
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_PurchaseOrderBrowse.aspx");
    }
    protected void Dd_SupplierName_SelectedIndexChanged(object sender, EventArgs e)
    {
        int SupplierId = Convert.ToInt32(Dd_SupplierName.SelectedValue);
        if (SupplierId != 0)
        {
            Gv_PurchaseOrderDetail.FooterRow.Enabled = true;
            Fill_SupplierDetails(SupplierId);
        }
        else
        {
            Gv_PurchaseOrderDetail.FooterRow.Enabled = false;
            Fill_SupplierDetails(SupplierId);
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

    private void Fill_PurchaseDetails(int PurchaseHeaderId)
    {

        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "USP_PurchaseOrderDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            cmd.Parameters.AddWithValue("@PurchaseOrderHeaderId", PurchaseHeaderId);

            dr = cmd.ExecuteReader();
            if (dr.Read()) //Header
            {
                SupplierId = Convert.ToInt32(dr["SupplierId"].ToString());
                Dd_SupplierName.ClearSelection();
                Dd_SupplierName.SelectedValue = SupplierId.ToString();

                Tb_PurchaseOrderNumber.Text = dr["PurchaseOrderNumber"].ToString();
                Dp_OrderDate.Text = dr["PurchaseOrderDate"].ToString();
                //   Dp_GRN_Date.Text = dr["GRN_Date"].ToString();
                Tb_SupplierAddress.Text = dr["SupplierAddress"].ToString();
                Tb_MobileNumber.Text = dr["ContactMobile"].ToString();
                Tb_TotalAmount.Text = dr["TotalAmount"].ToString();
                Hf_TotalAmount.Value = dr["TotalAmount"].ToString();


            }
            dr.NextResult();

            List_PurchaseOrderDetail.Clear();
            while (dr.Read()) //Pro Detail
            {
                int IndexToAdd = List_PurchaseOrderDetail.Count;
                List_PurchaseOrderDetail.Add(new CS_PurchaseOrderDetail()
                {
                    Index = IndexToAdd,
                    PurchaseOrderDetailId = Convert.ToInt32(dr["PurchaseOrderDetailId"].ToString()),
                    MaterialMasterId = Convert.ToInt32(dr["MaterialMasterId"].ToString()),
                    MaterialName = dr["MaterialName"].ToString(),
                    Quantity = Convert.ToDouble(dr["Quantity"].ToString()),
                    Rate = Convert.ToSingle(dr["Rate"].ToString()),
                    DiscountTypeId = Convert.ToInt32(dr["DiscountTypeId"].ToString()),
                    DiscountType = dr["DiscountType"].ToString(),
                    DiscountPer = Convert.ToSingle(dr["DiscountPercent"].ToString()),
                    DiscountAmount = Convert.ToSingle(dr["DiscountAmount"].ToString()),
                    TotalAmount = Math.Round(Convert.ToSingle(dr["TotalAmount"].ToString()), 2)

                });
            }
            for (int i = 0; i < List_PurchaseOrderDetail.Count; i++)
            {
                List_PurchaseOrderDetail[i].Index = i;
                List_PurchaseOrderDetail[i].SrNo = i + 1;

            }


            Gv_PurchaseOrderDetail.DataSource = null;
            Gv_PurchaseOrderDetail.DataSource = List_PurchaseOrderDetail;
            Gv_PurchaseOrderDetail.DataBind();



            dr.Close();
            con.Close();
            Fill_SupplierDetails(SupplierId);
            Fill_Material(-1);
            Fill_DiscountType(-1);
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