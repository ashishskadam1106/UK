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
public partial class Form_DojosAddUpdate : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    SqlDataReader dr;
    DataSet ds;

    string UserName = "";
    int DojoId_ToUpdate, Employee_Id;
   DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);

    public List<CS_Class_Detail> List_CS_Class_Detail { get { return (List<CS_Class_Detail>)Session["CS_Class_Detail"]; } set { Session["CS_Class_Detail"] = value; } }

    public int RowIndex { get { return (int)ViewState["RowIndex"]; } set { ViewState["RowIndex"] = value; } }

    public int IsCopyGridDone { get { return (int)ViewState["IsCopyGridDone"]; } set { ViewState["IsCopyGridDone"] = value; } }

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
            if (Session["LoginEmployee_Id"] != null)
            {
                DojoId_ToUpdate = Convert.ToInt32(Session["DojoId_ToUpdate"].ToString()); //This role id is to update
            }
            if (!IsPostBack)
            {
                List_CS_Class_Detail = new List<CS_Class_Detail>();
                Add_DummyRow();
                Hide_DummyRow();
                Fill_DojoTerm();
                Dp_StartDate.Text = CurrentUtc_IND.ToString("dd/MM/yyyy");

                if (DojoId_ToUpdate != 0)
                {
                    Lbl_Heading.Text = "Update Dojo";
                    Fill_DojoDetail(DojoId_ToUpdate);
                    Gv_Class.FooterRow.Visible = true;
                }

                Fill_Class(-1);
                Fill_Day(-1);
            }
        }

    }

    //private void Fill_DojoMaster(int DojoId_ToUpdate)
    //{
    //    try
    //    {
    //        con.ConnectionString = str;
    //        cmd = new SqlCommand();
    //        cmd.CommandText = "Select isnull(DojoCode,0)DojoCode,isnull(Postcode,0)Posrtcode,isnull(DojoAddress,'')DojoAddress,isnull(Premise,0)Premise,isnull(County,0)County,isnull(Ward,0)Ward,isnull(District,0)District,isnull(Country,0)Country from tbl_Dojos where DojoId=" + DojoId_ToUpdate;
    //        cmd.Connection = con;
    //        con.Open();
    //        dr = cmd.ExecuteReader();
    //        while (dr.Read())
    //        {
    //            Tb_DojoCode.Text = dr[0].ToString();
    //            Tb_PostalCode.Text = dr[1].ToString();
    //            Tb_Address.Text = dr[2].ToString();
    //            Tb_Premise.Text = dr[3].ToString();
    //            Tb_County.Text = dr[4].ToString();
    //            Tb_Ward.Text = dr[5].ToString();
    //            Tb_District.Text = dr[6].ToString();
    //            Tb_Country.Text = dr[7].ToString();


    //        }
    //        dr.Close();
    //        con.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
    //    }
    //    finally
    //    {
    //        dr.Close();
    //        con.Close();
    //    }
    //}

    protected void Fill_DojoDetail(int DojoId_ToUpdate)
    {
        int RentTermId = 0;
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Get_DojoDetialList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            cmd.Parameters.AddWithValue("@DojoId", DojoId_ToUpdate);
            dr = cmd.ExecuteReader();
            if (dr.Read()) //Header
            {
                Tb_DojoName.Text = dr["DojoName"].ToString();
                Tb_DojoCode.Text = dr["DojoCode"].ToString();
                Tb_PostalCode.Text = dr["Postcode"].ToString();
                Tb_Address.Text = dr["DojoAddress"].ToString();
                //Tb_Latitude.Text = dr["Latitude"].ToString();
                //Tb_Longitude.Text = dr["Longitude"].ToString();
                Tb_Premise.Text = dr["Premise"].ToString();
                Tb_County.Text = dr["County"].ToString();
                Tb_Ward.Text = dr["Ward"].ToString();
                Tb_District.Text = dr["District"].ToString();
                Tb_Country.Text = dr["Country"].ToString();
                Tb_Rent.Text = dr["Rent"].ToString();
                Dp_StartDate.Text = dr["StartDate"].ToString();
              
                RentTermId = Convert.ToInt32(dr["RentTermId"].ToString());
                Dd_TermDojo.ClearSelection();
                Dd_TermDojo.SelectedValue = RentTermId.ToString();
            }
            dr.NextResult();

            List_CS_Class_Detail.Clear();
            while (dr.Read())
            {
                int IndexToAdd = List_CS_Class_Detail.Count;
                List_CS_Class_Detail.Add(new CS_Class_Detail()
                {
                    Index = IndexToAdd,
                    DojoClassesScheduleId = Convert.ToInt32(dr["DojoClassesScheduleId"].ToString()),
                    DojoId = Convert.ToInt32(dr["DojoId"].ToString()),
                    ClassId = Convert.ToInt32(dr["ClassId"].ToString()),
                    Class = dr["Class"].ToString(),
                    DayId = Convert.ToInt32(dr["DayId"].ToString()),
                    Day = dr["DaysName"].ToString(),
                    StartTime = dr["StartTime"].ToString(),
                    EndTime = dr["EndTime"].ToString(),
                });
            }
            for (int i = 0; i < List_CS_Class_Detail.Count; i++)
            {
                List_CS_Class_Detail[i].Index = i;
                List_CS_Class_Detail[i].SrNo = i + 1;

            }
            if (List_CS_Class_Detail.Count > 0)
            {
                Gv_Class.DataSource = null;
                Gv_Class.DataSource = List_CS_Class_Detail;
                Gv_Class.DataBind();
                con.Close();
            }
            else
            {
                con.Close();
                Add_DummyRow();
                Hide_DummyRow();
                //Fill_Class(-1);
                //Fill_Day(-1);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }


    private void Fill_Class(int index)
    {
        DropDownList Dd_Class;
        if (index == -1)
        {
            Dd_Class = (DropDownList)Gv_Class.FooterRow.FindControl("Dd_Class");
        }
        else
        {
            Dd_Class = (DropDownList)Gv_Class.Rows[index].FindControl("Dd_Class");
        }
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Select ClassId,Class From tbl_Classes Where IsDeleted=0 union select 0 ClassId,'---Select---' Class";
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Class");
            Dd_Class.DataSource = ds.Tables["Class"];
            Dd_Class.DataBind();
            con.Close();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }

    private void Fill_DojoTerm()
    {

        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Select RentTermId,RentTerm from tbl_DojoRentTerm";
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Class");
            Dd_TermDojo.DataSource = ds.Tables["Class"];
            Dd_TermDojo.DataBind();
            con.Close();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }

    }

    private void Fill_Day(int index)
    {
        DropDownList Dd_Day;
        if (index == -1)
        {
            Dd_Day = (DropDownList)Gv_Class.FooterRow.FindControl("Dd_Day");
        }
        else
        {
            Dd_Day = (DropDownList)Gv_Class.Rows[index].FindControl("Dd_Day");
        }
        try
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            cmd.CommandText = "Select DayId,Day From tbl_Day union select 0 DayId,'---Select---' Day";
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Day");
            Dd_Day.DataSource = ds.Tables["Day"];
            Dd_Day.DataBind();
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
        if (List_CS_Class_Detail.Count == 0)
        {
            List_CS_Class_Detail.Add(new CS_Class_Detail
            {
                Index = -1,
                Class = "",
                Day = "",
                StartTime = "",
                EndTime = "",

            });
        }
        Gv_Class.DataSource = List_CS_Class_Detail;
        Gv_Class.DataBind();
    }

    private void Hide_DummyRow()
    {
        if (List_CS_Class_Detail[0].Index == -1)
        {
            Label Lbl_SrNo = (Label)Gv_Class.Rows[0].FindControl("Lbl_SrNo");
            Label Lbl_Class = (Label)Gv_Class.Rows[0].FindControl("Lbl_Class");
            Label Lbl_Day = (Label)Gv_Class.Rows[0].FindControl("Lbl_Day");
            TextBox Tb_StartTime = (TextBox)Gv_Class.Rows[0].FindControl("Tb_StartTime");
            TextBox Tb_EndTime = (TextBox)Gv_Class.Rows[0].FindControl("Tb_EndTime");

            Button Btn_Delete = (Button)Gv_Class.Rows[0].FindControl("Btn_Delete");

            Btn_Delete.Visible = false;
            Tb_StartTime.Visible = false;
            Tb_EndTime.Visible = false;
            Lbl_SrNo.Visible = false;
        }

    }

    #endregion



    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string DojoName = "", DojoCode = "", DojoAddress = "", PostalCode = "", Premise = "", County = "", Ward = "", District = "", Country = "", Term_Dojo = "", StartDate = "";
        // geography Latitude = "", Longitude = "";
        int ClassId = 0, DayId = 0, RentTermId = 0;
        DateTime ModifiedDate = Convert.ToDateTime("01-01-1900");
        DateTime DeletedDate = Convert.ToDateTime("01-01-1900");
        DateTime ApprovedDate = Convert.ToDateTime("01-01-1900");
        Double Rent = 0;


        int Go = 1;

        if (Go == 1)
        {
            DojoName = Tb_DojoName.Text.ToString();
            DojoCode = Tb_DojoCode.Text.ToString();
            PostalCode = Tb_PostalCode.Text.ToString();
            DojoAddress = Tb_Address.Text.ToString();
            Premise = Tb_Premise.Text.ToString();
            County = Tb_County.Text.ToString();
            Ward = Tb_Ward.Text.ToString();
            District = Tb_District.Text.ToString();
            Country = Tb_Country.Text.ToString();
            if (!String.IsNullOrEmpty(Tb_Rent.Text))
            {
                Rent = Convert.ToDouble(Tb_Rent.Text);
            }
            if (Dd_TermDojo.SelectedIndex >= 0)
            {
                RentTermId = Convert.ToInt32(Dd_TermDojo.SelectedValue);
            }
            StartDate = Dp_StartDate.Text;
            

        }
        if (DojoId_ToUpdate == 0)
        {
            con.ConnectionString = str;
            cmd = new SqlCommand();
            con.Open();

            SqlTransaction tran = con.BeginTransaction();
            cmd.Transaction = tran;
            try
            {
                cmd.CommandText = "USP_DojoInsert";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DojoCode", DojoCode);
                cmd.Parameters.AddWithValue("@DojoName", DojoName);
                cmd.Parameters.AddWithValue("@PostCode", PostalCode);
                cmd.Parameters.AddWithValue("@DojoAddress", DojoAddress);

                cmd.Parameters.AddWithValue("@Premise", Premise);
                cmd.Parameters.AddWithValue("@County", County);
                cmd.Parameters.AddWithValue("@Ward", Ward);
                cmd.Parameters.AddWithValue("@District", District);
                cmd.Parameters.AddWithValue("@Country", Country);
                cmd.Parameters.AddWithValue("@Rent", Rent);
                cmd.Parameters.AddWithValue("@RentTermId", RentTermId);
                cmd.Parameters.AddWithValue("@StartDate",Convert.ToDateTime(StartDate));


                cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ModifiedBy", "");
                cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                cmd.Parameters.AddWithValue("@IsDeleted", 0);
                cmd.Parameters.AddWithValue("@DeletedBy", "");
                cmd.Parameters.AddWithValue("@DeletedDate", DeletedDate);
                cmd.Parameters.AddWithValue("@ApprovedBy", "");
                cmd.Parameters.AddWithValue("@ApprovedDate", ApprovedDate);
             
                SqlParameter OP_DojoId = new SqlParameter();
                OP_DojoId.ParameterName = "@DojoId";
                OP_DojoId.DbType = DbType.Int32;
                OP_DojoId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(OP_DojoId);

                cmd.ExecuteNonQuery();

                int DojoId = Convert.ToInt32(OP_DojoId.Value.ToString());
                for (int i = 0; i < List_CS_Class_Detail.Count; i++)
                {
                    if (List_CS_Class_Detail[i].Index != -1)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "USP_ClassDetailInsert";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DojoId", DojoId);
                        cmd.Parameters.AddWithValue("@ClassId", List_CS_Class_Detail[i].ClassId);
                        cmd.Parameters.AddWithValue("@DayId", List_CS_Class_Detail[i].DayId);
                        cmd.Parameters.AddWithValue("@DayName", List_CS_Class_Detail[i].Day);
                        cmd.Parameters.AddWithValue("@StartTime", List_CS_Class_Detail[i].StartTime);
                        cmd.Parameters.AddWithValue("@EndTime", List_CS_Class_Detail[i].EndTime);
                        cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifiedBy", "");
                        cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                        cmd.Parameters.AddWithValue("@IsDeleted", 0);
                        cmd.Parameters.AddWithValue("@DeletedBy", "");
                        cmd.Parameters.AddWithValue("@DeletedDate", DeletedDate);
                        cmd.Parameters.AddWithValue("@ApprovedBy", "");
                        cmd.Parameters.AddWithValue("@ApprovedDate", ApprovedDate);
                        cmd.ExecuteNonQuery();

                    }
                }

                tran.Commit();
                con.Close();
                cmd.Parameters.Clear();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Dojo added successfully');window.location='Form_DojosMaster.aspx'", true);

                //  Clear();

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
        else if (DojoId_ToUpdate != 0)
        {
            try
            {
                con.ConnectionString = str;
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "USP_Dojo_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DojoId", DojoId_ToUpdate);
                cmd.Parameters.AddWithValue("@DojoName", DojoName);
                cmd.Parameters.AddWithValue("@DojoCode", DojoCode);
                cmd.Parameters.AddWithValue("@PostCode", PostalCode);
                cmd.Parameters.AddWithValue("@DojoAddress", DojoAddress);
                // cmd.Parameters.AddWithValue("@Latitude",Latitude );
                // cmd.Parameters.AddWithValue("@Longitude",Longitude );
                cmd.Parameters.AddWithValue("@Premise", Premise);
                cmd.Parameters.AddWithValue("@County", County);
                cmd.Parameters.AddWithValue("@Ward", Ward);
                cmd.Parameters.AddWithValue("@District", District);
                cmd.Parameters.AddWithValue("@Country", Country);
                cmd.Parameters.AddWithValue("@Rent", Rent);
                cmd.Parameters.AddWithValue("@RentTermId", RentTermId);
                cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(StartDate));

                cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                cmd.ExecuteNonQuery();


                for (int i = 0; i < List_CS_Class_Detail.Count; i++)
                {
                    if (List_CS_Class_Detail[i].DojoClassesScheduleId == 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "USP_ClassDetailInsert";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DojoId", DojoId_ToUpdate);
                        cmd.Parameters.AddWithValue("@ClassId", List_CS_Class_Detail[i].ClassId);
                        cmd.Parameters.AddWithValue("@DayId", List_CS_Class_Detail[i].DayId);
                        cmd.Parameters.AddWithValue("@DayName", List_CS_Class_Detail[i].Day);
                        cmd.Parameters.AddWithValue("@StartTime", List_CS_Class_Detail[i].StartTime);
                        cmd.Parameters.AddWithValue("@EndTime", List_CS_Class_Detail[i].EndTime);
                        cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifiedBy", "");
                        cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                        cmd.Parameters.AddWithValue("@IsDeleted", 0);
                        cmd.Parameters.AddWithValue("@DeletedBy", "");
                        cmd.Parameters.AddWithValue("@DeletedDate", DeletedDate);
                        cmd.Parameters.AddWithValue("@ApprovedBy", "");
                        cmd.Parameters.AddWithValue("@ApprovedDate", ApprovedDate);
                        cmd.ExecuteNonQuery();

                    }
                    else
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "USP_ClassDetailUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DojoClassesScheduleId", List_CS_Class_Detail[i].DojoClassesScheduleId);
                        cmd.Parameters.AddWithValue("@DojoId", List_CS_Class_Detail[i].DojoId);
                        cmd.Parameters.AddWithValue("@ClassId", List_CS_Class_Detail[i].ClassId);
                        cmd.Parameters.AddWithValue("@DayId", List_CS_Class_Detail[i].DayId);
                        cmd.Parameters.AddWithValue("@DayName", List_CS_Class_Detail[i].Day);
                        cmd.Parameters.AddWithValue("@StartTime", List_CS_Class_Detail[i].StartTime);
                        cmd.Parameters.AddWithValue("@EndTime", List_CS_Class_Detail[i].EndTime);
                        cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    }
                }

                con.Close();
                cmd.Parameters.Clear();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Dojo details updated successfully');window.location='Form_DojosMaster.aspx'", true);


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



    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Session["DojoId_ToUpdate"] = "0";
        Response.Redirect(@"~\Form_DojosAddUpdate.aspx");

    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_DojosMaster.aspx");
    }


    private void Copy_GridToList(int IndexToSkip)
    {
        int ClassId = 0, DayId = 0, Go = 1, Go1 = 1, Index = 0;
        string ClassName = "", DayName = "", Unit = "", DiscountType = "", StartTime = "", EndTime = "";

        try
        {
            IsCopyGridDone = 1;

            foreach (GridViewRow row in Gv_Class.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (row.RowIndex != IndexToSkip)
                    {
                        HiddenField Hf_ClassId = (HiddenField)row.FindControl("Hf_ClassId");
                        HiddenField Hf_DayId = (HiddenField)row.FindControl("Hf_DayId");

                        //if not dummy row

                        RowIndex = row.RowIndex;


                        if (Go == 1)
                        {
                            Label Lbl_Class = (Label)row.FindControl("Lbl_Class");
                            Label Lbl_Day = (Label)row.FindControl("Lbl_Day");
                            TextBox Tb_StartTime = (TextBox)row.FindControl("Tb_StartTime");
                            TextBox Tb_EndTime = (TextBox)row.FindControl("Tb_EndTime");



                            ClassId = Convert.ToInt32(Hf_ClassId.Value);
                            ClassName = Lbl_Class.Text.ToString();
                            DayId = Convert.ToInt32(Hf_DayId.Value);
                            DayName = Lbl_Day.Text.ToString();
                            StartTime = Tb_StartTime.Text.ToString();
                            EndTime = Tb_EndTime.Text.ToString();


                            List_CS_Class_Detail[RowIndex].ClassId = ClassId;
                            List_CS_Class_Detail[RowIndex].DayId = DayId;
                            List_CS_Class_Detail[RowIndex].StartTime = StartTime;
                            List_CS_Class_Detail[RowIndex].EndTime = EndTime;



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
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    private int Validate_Class_Details(int CallFor, int RowIndex)
    {
        //if (CallFor == 1) //Footer Row
        //{
        DropDownList Dd_Class = (DropDownList)Gv_Class.FooterRow.FindControl("Dd_Class");
        DropDownList Dd_Day = (DropDownList)Gv_Class.FooterRow.FindControl("Dd_Day");

        int Go = 1;
        string ValidationMsg = "";
        double Rate = 0, DiscountAmt = 0, DiscountPer = 0, Quantity = 0;

        if (Dd_Class.SelectedIndex <= 0)
        {
            Go = 0;
            Dd_Class.BorderColor = Color.Red;
            ValidationMsg += "Please select Class. \\n";
        }
        else
        {
            Dd_Class.BorderColor = Color.LightGray;
        }

        if (Dd_Day.SelectedIndex <= 0)
        {
            Go = 0;
            Dd_Day.BorderColor = Color.Red;
            ValidationMsg += "Please select Day. \\n";
        }
        else
        {
            Dd_Day.BorderColor = Color.LightGray;
        }
        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :\\n" + ValidationMsg.ToString() + "')", true);
        }
        return Go;
    }

    protected void Gv_Class_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int AlreadyExists = -2;

        try
        {
            #region
            if (e.CommandName.Equals("Add"))
            {
                //Find Footer Controls and HFs
                DropDownList Dd_Class = (DropDownList)Gv_Class.FooterRow.FindControl("Dd_Class");
                DropDownList Dd_Day = (DropDownList)Gv_Class.FooterRow.FindControl("Dd_Day");
                TextBox Tb_StartTime = (TextBox)Gv_Class.FooterRow.FindControl("Tb_StartTime");
                TextBox Tb_EndTime = (TextBox)Gv_Class.FooterRow.FindControl("Tb_EndTime");

                int Go = 1;
                Go = Validate_Class_Details(1, 0);
                if (Go == 1)
                {
                    if (List_CS_Class_Detail.Count > 0 && List_CS_Class_Detail[0].Index != -1)
                    {
                        //Copy_HiddenFieldsToTextbox();
                        Copy_GridToList(-5); //par should <0
                    }

                    if (List_CS_Class_Detail.Count == 1 && List_CS_Class_Detail[0].Index == -1)//&& List_PurchaseBill_Detail[0].Quantity == 0
                    {
                        List_CS_Class_Detail.RemoveAt(0);
                    }

                    for (int j = 0; j < List_CS_Class_Detail.Count; j++)
                    {
                        //if (List_CS_QuotationDetails[j].QuotationDetailId != 0)
                        //{
                        //    DiscountAmount = List_CS_QuotationDetails[j].DiscountAmount;
                        //}
                        //else
                        //{
                        //    DiscountAmount = Convert.ToSingle(Tb_DiscountAmount.Text);
                        //}
                    }

                    if (AlreadyExists == -2)
                    {
                        int IndexToAdd = List_CS_Class_Detail.Count;
                        List_CS_Class_Detail.Add(new CS_Class_Detail()
                        {
                            Index = IndexToAdd,

                            ClassId = Convert.ToInt32(Dd_Class.SelectedValue),
                            Class = Dd_Class.SelectedItem.ToString(),
                            DayId = Convert.ToInt32(Dd_Day.SelectedValue),
                            Day = Dd_Day.SelectedItem.ToString(),
                            StartTime = Tb_StartTime.Text.ToString(),
                            EndTime = Tb_EndTime.Text.ToString(),
                            IsDeleted = 0
                        });
                    }
                    else
                    {

                    }


                    Gv_Class.DataSource = null;
                    Gv_Class.DataSource = List_CS_Class_Detail;
                    Gv_Class.DataBind();


                    Gv_Class.EditIndex = -1;

                    Fill_Class(-1);
                    Fill_Day(-1);

                    //sScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "CalculateSubTotal();", true);
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error:" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        Button Btn_Delete = sender as Button;
        int DeleteIndex = Convert.ToInt32(Btn_Delete.CommandArgument.ToString());

        if (DeleteIndex >= 0)
        {
            try
            {
                if (List_CS_Class_Detail[DeleteIndex].DojoClassesScheduleId != 0)
                {
                    con.ConnectionString = str;
                    cmd = new SqlCommand();
                    cmd.CommandText = "USP_ClassDetail_Delete";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@DojoClassesScheduleId", List_CS_Class_Detail[DeleteIndex].DojoClassesScheduleId);
                    cmd.Parameters.AddWithValue("@DeletedBy", UserName);
                    cmd.Parameters.AddWithValue("@DeletedDate", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }


                if (List_CS_Class_Detail.Count > 0 && List_CS_Class_Detail[0].Index != -1)
                {
                    //Copy_HiddenFieldsToTextbox();
                    Copy_GridToList(DeleteIndex);

                }

                if (IsCopyGridDone == 1)
                {
                    List_CS_Class_Detail.RemoveAt(DeleteIndex);
                }
                for (int i = 0; i < List_CS_Class_Detail.Count; i++)
                {
                    List_CS_Class_Detail[i].Index = i;
                    List_CS_Class_Detail[i].SrNo = i + 1;
                }

                Gv_Class.EditIndex = -1;
                Gv_Class.DataSource = List_CS_Class_Detail;
                Gv_Class.DataBind();

                if (List_CS_Class_Detail.Count == 0)
                {
                    Add_DummyRow();
                    Hide_DummyRow();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
            }
        }
    }
}


