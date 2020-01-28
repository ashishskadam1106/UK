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
using System.Security.Cryptography;
using System.IO;
using System.Text;


public partial class Form_FeeStructure : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlConnection con1 = new SqlConnection();
    SqlCommand cmd;
    SqlCommand cmd1;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
    SqlDataReader dr1;


    int Employee_Id;
    int FeeId_ToUpdate { get { return (int)ViewState["FeeId_ToUpdate"]; } set { ViewState["FeeId_ToUpdate"] = value; } }
    int FeeCategoryId { get { return (int)ViewState["FeeCategoryId"]; } set { ViewState["FeeCategoryId"] = value; } }
    string UserName;
    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);
    public List<CS_FeeCategoryView> List_FeeCategoryView { get { return (List<CS_FeeCategoryView>)Session["CS_FeeCategoryView"]; } set { Session["CS_FeeCategoryView"] = value; } }
    public List<CS_Fees> List_Fees { get { return (List<CS_Fees>)Session["CS_Fees"]; } set { Session["CS_Fees"] = value; } }
    public List<CS_FeeGenerationType> List_FeeGenerationType { get { return (List<CS_FeeGenerationType>)Session["CS_FeeGenerationType"]; } set { Session["CS_FeeGenerationType"] = value; } }

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

            if (List_FeeCategoryView == null)
            {
                List_FeeCategoryView = new List<CS_FeeCategoryView>();
            }
            if (List_Fees == null)
            {
                List_Fees = new List<CS_Fees>();
            }
            if (List_FeeGenerationType == null)
            {
                List_FeeGenerationType = new List<CS_FeeGenerationType>();
            }
            



            if (!IsPostBack)
            {
                /*
                if (List_FeeCategoryView == null)
                {
                    List_FeeCategoryView = new List<CS_FeeCategoryView>();
                }
                 * */
                FeeId_ToUpdate = 0;
                Fill_FeeCategory_Details();
                FeelFeeGenerationType();
                //if (ViewState["FeeCategoryId"] != null)
                //{
                //    ExpandOnRefresh(FeeCategoryId);
                //}
                ExpandAll();
                
            }
        }
    }
    public void testc()
    {

    }
    protected void Gv_FeeCategoryView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
  
    protected void Gv_Fees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void ImgBtn_ShowHide_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("Pnl_Fees").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/Images/Minus-25.png";
            int FeeCategoryId = Convert.ToInt32(Gv_FeeCategoryView.DataKeys[row.RowIndex].Value.ToString());
            
            GridView Gv_Fees = row.FindControl("Gv_Fees") as GridView;


            Bind_Fees(FeeCategoryId, Gv_Fees);
            //  FeelFeeGenerationType(-1, Gv_Fees);


            /*
                 DropDownList Dd_FeeGenerationType = (DropDownList)Gv_Fees.FooterRow.FindControl("Dd_FeeGenerationType");
                 Dd_FeeGenerationType.DataSource = List_FeeGenerationType;
                 Dd_FeeGenerationType.DataBind();
                 Dd_FeeGenerationType.Enabled = true;
            
            */

        }
        else
        {
            row.FindControl("Pnl_Fees").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/Images/Plus-25.png";
        }


    }

    //private void FeelFeeGenerationType(int Index, GridView Gv_Fees)
    //{
    //    AjaxControlToolkit.ComboBox Dd_AjaxFeeGenerationType;

    //    DropDownList Dd_FeeGenerationType;
    //    if (Index == -1)
    //    {

    //        //Dd_AjaxFeeGenerationType = (AjaxControlToolkit.ComboBox)Gv_Fees.FooterRow.FindControl("Dd_AjaxFeeGenerationType");
    //        Dd_FeeGenerationType = (DropDownList)Gv_Fees.FooterRow.FindControl("Dd_FeeGenerationType");
    //        // Dd_material = (DropDownList)Gv_PurchaseDetail.FooterRow.FindControl("Dd_Material");
    //    }
    //    else
    //    {
    //        //Dd_AjaxFeeGenerationType = (AjaxControlToolkit.ComboBox)Gv_Fees.Rows[Index].FindControl("Dd_AjaxFeeGenerationType");
    //        Dd_FeeGenerationType = (DropDownList)Gv_Fees.Rows[Index].FindControl("Dd_FeeGenerationType");
    //        //Dd_material = (DropDownList)Gv_PurchaseDetail.Rows[index].FindControl("Dd_Material");
    //    }

    //    con.ConnectionString = str;
    //    cmd = new SqlCommand();
    //    //cmd.CommandText = "select MM.MaterialMaster_Id,MM.Material_Name+'	['+rtrim(ltrim(VM.Model_Name))+'-'+rtrim(ltrim(vc.VehicleCompany_Name))+']' as Part_Name   from tbl_MaterialMaster MM left outer join  tbl_VehicleModel VM on MM.VehicleModel_Id=VM.VehicleModel_Id left outer join  tbl_VehicleCompany VC on VM.VehicleCompany_Id=VC.VehicleCompany_Id  where VC.ServiceCentre_Id= " + ServiceCentre_Id + " union select 0,'---select---'";
    //    //cmd.CommandText = "select MM.MaterialMaster_Id,MM.Material_Name+' ['+rtrim(ltrim(isnull(MM.Part_Number,'')))+' ] '+' ['+rtrim(ltrim(isnull(VM.Model_Name,'')))+'-'+rtrim(ltrim(isnull(vc.VehicleCompany_Name,'')))+']' as Part_Name   from tbl_MaterialMaster MM left outer join  tbl_VehicleModel VM on MM.VehicleModel_Id=VM.VehicleModel_Id left outer join  tbl_VehicleCompany VC on VM.VehicleCompany_Id=VC.VehicleCompany_Id and vc.ServiceCentre_Id="+ServiceCentre_Id+" where MM.ServiceCentre_Id="+ServiceCentre_Id+" union select 0,'---select---' order by 2 asc";
    //    string query1 = "select FeeGenerationTypeId,FeeGenerationType from tbl_FeeGenerationType union  select 0, '--Select--'";
    //    cmd.CommandText = query1;
    //    cmd.Connection = con;
    //    con.Open();
    //    da = new SqlDataAdapter(cmd);
    //    ds = new DataSet();
    //    da.Fill(ds, "FeeGenerationType");
    //    Dd_FeeGenerationType.DataSource = ds.Tables["FeeGenerationType"];
    //    Dd_FeeGenerationType.DataTextField = "FeeGenerationType";
    //    Dd_FeeGenerationType.DataValueField = "FeeGenerationTypeId";
    //    Dd_FeeGenerationType.DataBind();
    //    Dd_FeeGenerationType.SelectedIndex = 0;
    //    con.Close();
    //    /*
    //    List_FeeGenerationType.Clear();
    //    con1.ConnectionString = str;
    //    cmd1 = new SqlCommand();
    //    string query1 = "select FeeGenerationTypeId,FeeGenerationType from tbl_FeeGenerationType union  select 0, '--Select--'";
    //    cmd1.CommandText = query1;
    //    cmd1.Connection = con1;
    //    con1.Open();
    //    dr1 = cmd1.ExecuteReader();

    //    while (dr1.Read())
    //    {
    //        List_FeeGenerationType.Add(
    //            new CS_FeeGenerationType()
    //            {
    //                FeeGenerationTypeId = Convert.ToInt32(dr1["FeeGenerationTypeId"].ToString()),
    //                FeeGenerationType = dr1["FeeGenerationType"].ToString()

    //            });
          
    //    }

    //    dr1.Close();
    //    con1.Close();
    //    */

    //    //Dd_AjaxFeeGenerationType.DataSource = List_FeeGenerationType;
    //    //Dd_AjaxFeeGenerationType.DataTextField = "FeeGenerationType";
    //    //Dd_AjaxFeeGenerationType.DataValueField = "FeeGenerationTypeId";
    //    //Dd_AjaxFeeGenerationType.DataBind();
    //    //Dd_AjaxFeeGenerationType.SelectedIndex = 0;




    //}

    private void Bind_Fees(int FeeCategoryId, GridView Gv_Fees)
    {
        List_Fees.Clear();
        int IndexToAdd = 0;
        con1.ConnectionString = str;
        cmd1 = new SqlCommand();
        string query1 = "select FeeId,F.FeeCategoryId,FeeName,F.FeeGenerationTypeId,F.Remark,FC.FeeCategory,FGT.FeeGenerationType,F.Amount from tbl_Fees F  inner join tbl_FeeCategories FC on F.FeeCategoryId=FC.FeeCategoryId inner join tbl_FeeGenerationType FGT on F.FeeGenerationTypeId=FGT.FeeGenerationTypeId where F.FeeCategoryId=" + FeeCategoryId.ToString() + " order by F.FeeId";
        cmd1.CommandText = query1;
        cmd1.Connection = con1;
        con1.Open();
        dr1 = cmd1.ExecuteReader();
        if (dr1.HasRows)
        {
            while (dr1.Read())
            {
                List_Fees.Add(
                    new CS_Fees()
                    {
                        Index = IndexToAdd,
                        FeeId = Convert.ToInt32(dr1["FeeId"].ToString()),
                        FeeCategoryId = Convert.ToInt32(dr1["FeeCategoryId"].ToString()),
                        FeeName = dr1["FeeName"].ToString(),
                        FeeGenerationTypeId = Convert.ToInt32(dr1["FeeGenerationTypeId"].ToString()),
                        Remark = dr1["Remark"].ToString(),
                        FeeCategory = dr1["FeeCategory"].ToString(),
                        FeeGenerationType = dr1["FeeGenerationType"].ToString(),
                        Amount=Convert.ToDouble(dr1["Amount"].ToString())

                    });
                IndexToAdd = IndexToAdd + 1;
            }
            if (List_Fees.Count > 0)
            {
                Gv_Fees.DataSource = List_Fees;
                Gv_Fees.DataBind();
            }



        }
        //else
        //{

        //    List_Fees.Add(
        //    new CS_Fees()
        //    {
        //        Index = 0,
        //        FeeId = 0,
        //        FeeCategoryId =0,
        //        FeeName = "",
        //        FeeGenerationTypeId = 0,
        //        Remark = "",
        //        FeeCategory = "",
        //        FeeGenerationType = ""

        //    });


        //    Gv_Fees.DataSource = List_Fees;
        //    Gv_Fees.DataBind();



        //    /*
        //    //if (Is_AddExpenseRight == 0)
        //    //{
        //        Button Btn_AddFees = (Button)Gv_Fees.FooterRow.FindControl("btnAddFees");
        //        //Btn_AddFees.Enabled = false;
        //        Btn_AddFees.Visible = false;
        //        Gv_Fees.FooterRow.Visible = false;
        //    //}
        //     * 
        //     * * */

        //    //Button Btn_EditFees = (Button)Gv_Fees.Rows[0].FindControl("Btn_EditFees");
        //    //Button Btn_RemoveFees = (Button)Gv_Fees.Rows[0].FindControl("Btn_RemoveFees");
        //    //Btn_EditFees.Visible = false;
        //    //Btn_RemoveFees.Visible = false;


        //}

        dr1.Close();
        con1.Close();

        //da = new SqlDataAdapter(cmd1);
        //ds = new DataSet();
        //da.Fill(ds, "BudgetExpense");
        //Gv_BudgetOverview_Expense.DataSource = ds.Tables["BudgetExpense"];
        //Gv_BudgetOverview_Expense.DataBind();
    }

    private void FeelFeeGenerationType()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "select FeeGenerationTypeId,FeeGenerationType from tbl_FeeGenerationType union  select 0 as FeeGenerationTypeId, '--Select--' as FeeGenerationType";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Employee");
            Dd_FeeGenrationType.DataSource = ds.Tables["Employee"];
            Dd_FeeGenrationType.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    private void Fill_FeeCategory_Details()
    {

        con.ConnectionString = str;
        con.Open();
        cmd = new SqlCommand();
        cmd.CommandText = "USP_GetFeeCategoryList";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;

        dr = cmd.ExecuteReader();

        List_FeeCategoryView.Clear();
        while (dr.Read())
        {
            int IndexToAdd = List_FeeCategoryView.Count;
            List_FeeCategoryView.Add(new CS_FeeCategoryView()
            {
                Index = IndexToAdd,
                FeeCategoryId = Convert.ToInt32(dr["FeeCategoryId"].ToString()),

                FeeCategory = dr["FeeCategory"].ToString(),
                IsOneOfTheGroup = Convert.ToBoolean(dr["IsOneOfTheGroup"].ToString()),
                IsOneOfTheGroupText = dr["IsOneOfTheGroupText"].ToString(),
                Remark = dr["Remark"].ToString(),
                FeeCollectionStage = dr["FeeCollectionStage"].ToString(),
            });
        }

        for (int i = 0; i < List_FeeCategoryView.Count; i++)
        {
            List_FeeCategoryView[i].Index = i;
            List_FeeCategoryView[i].SrNo = i + 1;

        }

        Gv_FeeCategoryView.DataSource = null;
        Gv_FeeCategoryView.DataSource = List_FeeCategoryView;
        Gv_FeeCategoryView.DataBind();

        con.Close();




    }

    protected void Gv_Fees_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void Btn_Add_Click(object sender, EventArgs e)
    {
        
        Btn_Save.Text = "Save";
        HF_Action.Value = "AddNew";
        Hf_FeeIdToUpdate.Value = "0";
        FeeId_ToUpdate = 0;
        //Hf_FeeCategoryId.Value = FeeCategoryId.ToString();
        Button Btn_Add = sender as Button;
        FeeCategoryId = Convert.ToInt32(Btn_Add.CommandArgument.ToString());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " $('#Modal_Fee').modal({backdrop: 'static',keyboard: false})", true);
    }

    private int ValidateFeeName()
    {
        int Go = 1;
        string ValidationMsg = "";

        if (Tb_FeeName.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter Fee name.\\n";
            Tb_FeeName.BorderColor = Color.Red;
        }
        else
        {
            Tb_FeeName.BorderColor = Color.LightGray;
        }
        if (Dd_FeeGenrationType.SelectedIndex <= 0)
        {
            Go = 0;
            ValidationMsg += "Please Select Fee Genration Type.\\n";
            Dd_FeeGenrationType.BorderColor = Color.Red;
        }
        else
        {
            Dd_FeeGenrationType.BorderColor = Color.LightGray;
        }

        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :\\n" + ValidationMsg.ToString() + "')", true);
        }
        return Go;
    }
    protected void Btn_Save_Click(object sender, EventArgs e)
    {

       
        String HFActionText = "";
        String BtnSaveActionText = "";
        int Hf_FeeIdToUpdateValue = 0;

        HFActionText = HF_Action.Value.ToString();
        BtnSaveActionText = Btn_Save.Text.ToString();
        Hf_FeeIdToUpdateValue = Convert.ToInt32(Hf_FeeIdToUpdate.Value.ToString()); 
        

        int Go = 1, FeeGenerationTypeId = 0;
        string Remark = "", FeeName = "";
        double Amount = 0;
        DateTime ModifiedDate = Convert.ToDateTime("01-01-1900");
        DateTime DeletedDate = Convert.ToDateTime("01-01-1900");
        Go = ValidateFeeName();

        if (Go == 1)
        {
            Remark = Tb_Remark.Text;
            FeeName = Tb_FeeName.Text;
            Amount = Convert.ToDouble(Tb_Amount.Text);
            //if (!String.IsNullOrEmpty(Hf_FeeCategoryId.Value))
            //{
            //    FeeCategoryId = Convert.ToInt32(Hf_FeeCategoryId.Value);
            //}
            if (Dd_FeeGenrationType.SelectedIndex >= 0)
            {
                FeeGenerationTypeId = Convert.ToInt32(Dd_FeeGenrationType.SelectedValue);
            }

            //For Add new following conditions to be passed
            //Btn_Save.Text = "Save";
            //HF_Action.Value = "AddNew";
            //-- Consider following two for now
            //Hf_FeeIdToUpdate.Value = "0"; 
            //FeeId_ToUpdate = 0;
            if (FeeId_ToUpdate == 0 && Hf_FeeIdToUpdateValue == 0)
            {
                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandText = "USP_Fee_Insert";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FeeName", FeeName);
                    cmd.Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                    cmd.Parameters.AddWithValue("@Amount", Amount);
                    cmd.Parameters.AddWithValue("@Remark", Remark);
                    cmd.Parameters.AddWithValue("@FeeGenerationTypeId", FeeGenerationTypeId);
                    cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                 

                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    con.Close();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Fee Name added successfully');window.location='Form_FeeStructure.aspx'", true);
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
            //For Updatefollowing conditions to be passed
            //Btn_Save.Text = "Save";
            //HF_Action.Value = "UpdateExisting";
            //-- Consider following two for now
            //Hf_FeeIdToUpdate.Value = "0"; 
            //FeeId_ToUpdate = 0;
            if (FeeId_ToUpdate != 0 && Hf_FeeIdToUpdateValue != 0 && FeeId_ToUpdate == Hf_FeeIdToUpdateValue)
            {
                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandText = "USP_Fee_Update";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FeeId", FeeId_ToUpdate);
                    cmd.Parameters.AddWithValue("@FeeName", FeeName);
                    cmd.Parameters.AddWithValue("@FeeCategoryId", FeeCategoryId);
                    cmd.Parameters.AddWithValue("@Amount", Amount);
                    cmd.Parameters.AddWithValue("@Remark", Remark);
                    cmd.Parameters.AddWithValue("@FeeGenerationTypeId", FeeGenerationTypeId);
                    cmd.Parameters.AddWithValue("@VersionBy", UserName);
                    cmd.Parameters.AddWithValue("@VersionDate", DateTime.Now);

                    cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    con.Close();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Fee Name updated Successfully');window.location='Form_FeeStructure.aspx'", true);
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
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Something went wrong. Please contact administrator')", true);
            }

            ExpandOnRefresh(FeeCategoryId);
        }
    }
    public void ExpandOnRefresh(int FeeCategoryIdToExpand)
    {
        int RowIndex = List_FeeCategoryView.FindIndex(x => x.FeeCategoryId == FeeCategoryIdToExpand);

        GridView Gv_Fees = (GridView)Gv_FeeCategoryView.Rows[RowIndex].FindControl("Gv_Fees");

        Bind_Fees(FeeCategoryIdToExpand, Gv_Fees);

        Gv_FeeCategoryView.Rows[RowIndex].FindControl("Pnl_Fees").Visible = true;
        ImageButton imgShowHide = (ImageButton)Gv_FeeCategoryView.Rows[RowIndex].FindControl("ImgBtn_ShowHide");
        imgShowHide.CommandArgument = "Hide";
        imgShowHide.ImageUrl = "~/Images/Minus-25.png";

        

        
    }
    public void ExpandAll()
    {
        for (int RowIndex = 0; RowIndex < List_FeeCategoryView.Count; RowIndex++)
        {
            

            GridView Gv_Fees = (GridView)Gv_FeeCategoryView.Rows[RowIndex].FindControl("Gv_Fees");

            Bind_Fees(List_FeeCategoryView[RowIndex].FeeCategoryId, Gv_Fees);

            Gv_FeeCategoryView.Rows[RowIndex].FindControl("Pnl_Fees").Visible = true;
            ImageButton imgShowHide = (ImageButton)Gv_FeeCategoryView.Rows[RowIndex].FindControl("ImgBtn_ShowHide");
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/Images/Minus-25.png";
        }

        

        
    }

     

    protected void Btn_Back_Click(object sender, EventArgs e)
    {

    }
    protected void Gv_Fees_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView Gv_Fees = Gv_FeeCategoryView.FindControl("Gv_Fees") as GridView;
        int FeeId_ToUpdate = Convert.ToInt32(Gv_Fees.SelectedValue);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " $('#Modal_Fee').modal({backdrop: 'static',keyboard: false})", true);
        //Fill_Fee_Details();
    }

    private void Fill_Fee_Details(int FeeId)
    {
        int FeeGenerationTypeId = 0;
            double Amount=0;

        con.ConnectionString = str;
        con.Open();
        cmd = new SqlCommand();
        cmd.CommandText = "USP_GetFeeDetailList";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FeeId", FeeId);
        cmd.Connection = con;

        dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            Tb_FeeName.Text = dr["FeeName"].ToString();
            FeeGenerationTypeId =Convert.ToInt32(dr["FeeGenerationTypeId"].ToString());
            Dd_FeeGenrationType.ClearSelection();
            Dd_FeeGenrationType.SelectedValue = FeeGenerationTypeId.ToString();
            Tb_Remark.Text = dr["Remark"].ToString();
            Amount = Convert.ToDouble(dr["Amount"].ToString());
            Tb_Amount.Text = Amount.ToString("##.00");

        }
        dr.Close();
        con.Close();



    }

    protected void Gv_Fees_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView Gv_Fees = Gv_FeeCategoryView.FindControl("Gv_Fees") as GridView;
        GridView Gv_Fees = sender as GridView;
        //Gv_Fees.EditIndex = e.NewEditIndex;

        int EditIndex = e.NewEditIndex;
        int FeeId = Convert.ToInt32(Gv_Fees.DataKeys[EditIndex].Values[0].ToString());
        FeeCategoryId = Convert.ToInt32(Gv_Fees.DataKeys[EditIndex].Values[1].ToString());

        Btn_Save.Text = "Update";
        HF_Action.Value = "UpdateExisting";
        Hf_FeeIdToUpdate.Value = FeeId.ToString();
        FeeId_ToUpdate = FeeId;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " $('#Modal_Fee').modal({backdrop: 'static',keyboard: false})", true);
        Fill_Fee_Details(FeeId);
    }
    protected void Gv_Fees_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void Gv_FeeCategoryView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}