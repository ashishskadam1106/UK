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
using System.Web.Mail;
using System.Net.Mail;
using System.Text;
//using System.Net.Http;
using System.Net;
using System.IO;
using CS_Encryption;
using System.Security.Cryptography;
using System.Net.Configuration;

public partial class Form_StudentRegistration : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    int Employee_Id, StudentID_ToEdit, StudentId_ToUpdate;
    string UserName;
    int StudentCallFor { get { return (int)ViewState["StudentCallFor"]; } set { ViewState["StudentCallFor"] = value; } }
    //int StudentId_ToUpdate { get { return (int)ViewState["StudentId_ToUpdate"]; } set { ViewState["StudentId_ToUpdate"] = value; } }
    DateTime CurrentUtc_IND = DateTime.UtcNow.AddHours(5).AddMinutes(30);
    public List<CS_Class_Detail> List_Class_Detail { get { return (List<CS_Class_Detail>)Session["CS_Class_Detail"]; } set { Session["CS_Class_Detail"] = value; } }
    public List<CS_Fees> List_Fees { get { return (List<CS_Fees>)Session["CS_Fees"]; } set { Session["CS_Fees"] = value; } }
    public List<CS_Fees> List_ToUpload { get { return (List<CS_Fees>)Session["CS_FeesToUpload"]; } set { Session["CS_FeesToUpload"] = value; } }
    public List<CS_Fees> List_FeesToFeeDD { get { return (List<CS_Fees>)Session["CS_FeesToFeeDD"]; } set { Session["CS_FeesToFeeDD"] = value; } }
    public int RowIndex { get { return (int)ViewState["RowIndex"]; } set { ViewState["RowIndex"] = value; } }
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


            if (!IsPostBack)
            {
                StudentId_ToUpdate = 0;
                HF_StudentId.Value = "0";
                StudentCallFor = 1;
                HF_StudentId_ToPrint.Value = "0";
            }

            try
            {
                if (Request.QueryString.Count > 0)
                {
                    int.Parse(Decrypt(Request.QueryString[0].ToString()));
                    int.Parse(Decrypt(Request.QueryString[1].ToString()));
                    StudentId_ToUpdate = Convert.ToInt32(Decrypt(Request.QueryString[0].ToString()));
                    StudentCallFor = Convert.ToInt32(Decrypt(Request.QueryString[1].ToString()));
                    HF_StudentId.Value = StudentId_ToUpdate.ToString();
                    HF_StudentId_ToPrint.Value = encrypt(HF_StudentId.Value);
                    //StudentId_ToUpdate = StudentId;
                }
                else
                {
                    StudentId_ToUpdate = 0;
                }

            }
            catch
            {
                StudentId_ToUpdate = 0;
            }

            if (!IsPostBack)
            {
                Dp_MembershipDate.Text = CurrentUtc_IND.ToString("dd/MM/yyyy");
                List_Class_Detail = new List<CS_Class_Detail>();
                List_ToUpload = new List<CS_Fees>();
                List_Fees = new List<CS_Fees>();
                Fill_Title();
                GetNextStudentId();
                if (StudentId_ToUpdate == 0)
                {
                    CreateRandomPassword(12);
                    Tb_AccountPassword.Text = CreateRandomPassword(12);
                }
                Fill_Dojo();
                Fill_Belt();
                // Fill_StudentFeeDetails();
                Fill_StudentFees();
                Fill_Term();
                Fill_FeeDetailAmount();

                if (StudentId_ToUpdate != 0) //Fill student details for view or edit
                {
                    Fill_StudentDetails(StudentId_ToUpdate);
                    pnl_Fees.Visible = false;
                    Dd_Dojo.Enabled = false;

                    //   Btn_Update.Visible = true;

                }

                if (StudentCallFor == 2) //view
                {

                }
            }
        }

    }



    private void Fill_StudentDetails(int StudentId)
    {
        Boolean IsAstama = false, IsDisabled = false, IsGlassesOrLense = false;
        con.ConnectionString = str;
        cmd = new SqlCommand();
        con.Open();

        try
        {
            cmd.CommandText = "Get_StudentDetailListForEdit";
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Tb_MembershipNumber.Text = dr["MembershipNumber"].ToString();
                Dp_MembershipDate.Text = dr["MembershipDate"].ToString();
                Dd_Title.SelectedIndex = Dd_Title.Items.IndexOf(Dd_Title.Items.FindByText(dr["Title"].ToString()));
                Tb_FirstName.Text = dr["FirstName"].ToString();
                Tb_LastName.Text = dr["LastName"].ToString();
                Tb_AccountPassword.Text = dr["AccountPassword"].ToString();

                Tb_Mobile.Text = dr["MobileNumber"].ToString();
                Tb_Telephone.Text = dr["TelePhoneNumber"].ToString();
                Tb_Email.Text = dr["EmailId"].ToString();
                Dp_BirthDate.Text = dr["BirthDate"].ToString();

                Tb_PostCode.Text = dr["Postcode"].ToString();
                Tb_Address.Text = dr["StudentAddress"].ToString();


                IsAstama = Convert.ToBoolean(dr["IsAstama"].ToString());
                IsDisabled = Convert.ToBoolean(dr["IsGlassesOrLense"].ToString());
                IsGlassesOrLense = Convert.ToBoolean(dr["IsDisabled"].ToString());



                if (IsAstama == true)
                {
                    Rb_IsAstama1.Checked = true;
                    Rb_IsAstama2.Checked = false;

                }
                else
                {
                    Rb_IsAstama1.Checked = false;
                    Rb_IsAstama2.Checked = true;
                }

                if (IsDisabled == true)
                {
                    RB_Yes.Checked = true;
                    RB_No.Checked = false;

                }
                else
                {
                    RB_Yes.Checked = false;
                    RB_No.Checked = true;
                }

                if (IsGlassesOrLense == true)
                {
                    Rb_1.Checked = true;
                    Rb_2.Checked = false;

                }
                else
                {
                    Rb_1.Checked = false;
                    Rb_2.Checked = true;
                }
                Tb_Extraremark.Text = dr["ExtraRemarks"].ToString();
                Tb_Disability.Text = dr["DisabilityDetails"].ToString();
                Tb_Others.Text = dr["OtherMartialArtStudyGradeDetails"].ToString();

                int DojoId = Convert.ToInt32(dr["DojoId"].ToString());
                Dd_Dojo.ClearSelection();
                Dd_Dojo.SelectedValue = DojoId.ToString();

                int BeltId = Convert.ToInt32(dr["InitialBeltId"].ToString());
                Dd_Belt.ClearSelection();
                Dd_Belt.SelectedValue = BeltId.ToString();


            }

            dr.NextResult();

            List_Class_Detail.Clear();
            while (dr.Read())
            {
                int IndexToAdd = List_Class_Detail.Count;
                List_Class_Detail.Add(new CS_Class_Detail()
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
                    IsChecked = Convert.ToBoolean(dr["IsChecked"].ToString())
                });
            }
            for (int i = 0; i < List_Class_Detail.Count; i++)
            {
                List_Class_Detail[i].Index = i;
                List_Class_Detail[i].SrNo = i + 1;

            }

            Gv_ClassSchedule.DataSource = null;
            Gv_ClassSchedule.DataSource = List_Class_Detail;
            Gv_ClassSchedule.DataBind();
            Gv_ClassSchedule.Visible = true;


            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static string CreateRandomPassword(int PasswordLength)
    {
        string _allowedChars = "0123456789!:=abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
        Random randNum = new Random();
        char[] chars = new char[PasswordLength];
        int allowedCharCount = _allowedChars.Length;
        for (int i = 0; i < PasswordLength; i++)
        {
            chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
        }
        return new string(chars);
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


    private void Fill_Title()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "select Title_id,Title from tbl_Title  union select 0 Title_id,'---Select---' Title";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Title");
            Dd_Title.DataSource = ds.Tables["Title"];
            Dd_Title.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    private void GetNextStudentId()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "select isnull((select MAX(StudentId) from tbl_Students),0)+1 'StudentId' ";
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Tb_MembershipNumber.Text = dr["StudentId"].ToString();
            }

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void RB_Yes_CheckedChanged(object sender, EventArgs e)
    {
        if (RB_Yes.Checked)
        {
            Tb_Disability.Enabled = true;
        }
    }
    protected void RB_No_CheckedChanged(object sender, EventArgs e)
    {
        if (RB_No.Checked)
        {
            Tb_Disability.Enabled = false;
        }
    }
    protected void Gv_ClassSchedule_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void Gv_ClassSchedule_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void Gv_ClassSchedule_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void Dd_Dojo_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DojoId = Convert.ToInt32(Dd_Dojo.SelectedValue);


        Fill_Dojo_Details(DojoId);


        pnl_Dojo.Focus();
        Dd_Dojo.Focus();
    }

    private void Fill_Dojo()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select DojoId,DojoCode from tbl_Dojos  union select 0 DojoId,'---Select---' DojoCode";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Title");
            Dd_Dojo.DataSource = ds.Tables["Title"];
            Dd_Dojo.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    private void Fill_Belt()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select BeltId,BeltName From tbl_belt ";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Title");
            Dd_Belt.DataSource = ds.Tables["Title"];
            Dd_Belt.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    private void Fill_Dojo_Details(int DojoId)
    {

        con.ConnectionString = str;
        con.Open();
        cmd = new SqlCommand();
        cmd.CommandText = "USP_GetDojoDetailList";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@DojoId", DojoId);
        dr = cmd.ExecuteReader();

        List_Class_Detail.Clear();
        while (dr.Read())
        {
            int IndexToAdd = List_Class_Detail.Count;
            List_Class_Detail.Add(new CS_Class_Detail()
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
                IsChecked = Convert.ToBoolean(dr["IsChecked"].ToString())
            });
        }
        for (int i = 0; i < List_Class_Detail.Count; i++)
        {
            List_Class_Detail[i].Index = i;
            List_Class_Detail[i].SrNo = i + 1;

        }

        Gv_ClassSchedule.DataSource = null;
        Gv_ClassSchedule.DataSource = List_Class_Detail;
        Gv_ClassSchedule.DataBind();

        con.Close();




    }
    protected void Btn_SaveFees_Click(object sender, EventArgs e)
    {
        int Go = 1, MembershipNumber = 0, DojoId = 0, InitialBeltId = 0, CurrentBeltId = 0, DojoClassesScheduleId = 0;
        string MembershipDate = "", Title = "", FirstName = "", MiddleName = "", LastName = "", MobileNumber = "", TelePhoneNumber = "", EmailId = "", BirthDate = "",
            Postcode = "", StudentAddress = "", Premise = "", County = "", Ward = "", District = "", Country = "",
            ImageFilePath = "", FullName = "", AccountPassword = "";
        Boolean IsAstama = false, IsGlassesOrLense = false, IsDisabled = false;
        string DisabilityDetails = "", OtherMartialArtStudyGradeDetails = "", ExtraRemarks = "", StartingTerm = "";

        int FeeId = 0, StartingTermId = 0, NumberOfTermsToPay = 0;
        double EnrollmentFeeAmount = 0, EnrollmentFeeDiscountAmount = 0, AnnualMembershipFeeAmount = 0, AnnualMembershipFeeDiscountAmount = 0, MembershipFeeAmount = 0,
MembershipFeeDiscountAmount = 0, BronzePackageFeeAmount = 0, SilverPackageFeeAmount = 0, GoldPackageFeeAmount = 0, DVDAmount = 0, TermFeeForSingleTerm = 0,
TermFeeDiscountAmount = 0, TotalTermFee = 0, TotalAmount = 0, PaidAmount = 0, TotalDiscount = 0, Balance = 0;
        Boolean Isalreadyhave = false, IsBronzePackage = false, IsSilverPackage = false, IsGoldPackage = false, IsDVD = false, IsPermanentDiscount = false;





        if (!(this.RB_Yes.Checked || this.RB_No.Checked))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Select atleast one radio')", true);
        }
        else if (!(this.Rb_1.Checked || this.Rb_2.Checked))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Select atleast one radio')", true);
        }

        else if (!(this.Rb_IsAstama1.Checked || this.Rb_IsAstama2.Checked))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Select atleast one radio')", true);
        }
        if (StudentId_ToUpdate == 0)
        {
            Go = ValidateData();
        }
        else
        {
            Go = ValidateDataToUpdate();
        }
        if (Go == 1)
        {

            MembershipNumber = Convert.ToInt32(Tb_MembershipNumber.Text);
            MembershipDate = Dp_MembershipDate.Text;
            Title = Dd_Title.SelectedItem.Text;
            FirstName = Tb_FirstName.Text;

            LastName = Tb_LastName.Text;
            MobileNumber = Tb_Mobile.Text;
            TelePhoneNumber = Tb_Telephone.Text;
            EmailId = Tb_Email.Text;
            BirthDate = Dp_BirthDate.Text;
            Postcode = Tb_PostCode.Text;
            StudentAddress = Tb_Address.Text;


            FullName = Tb_FirstName.Text + ' ' + Tb_LastName.Text;

            AccountPassword = Tb_AccountPassword.Text;


            if (Chk_IsPermanentDiscount.Checked == true)
            {
                IsPermanentDiscount = true;
            }
            else
            {
                IsPermanentDiscount = false;
            }

            DisabilityDetails = Tb_Disability.Text.ToString();
            OtherMartialArtStudyGradeDetails = Tb_Others.Text.ToString();
            ExtraRemarks = Tb_Extraremark.Text.ToString();
            int StudentId = Convert.ToInt32(HF_StudentId.Value.ToString());

            if (Rb_1.Checked)
            {
                IsGlassesOrLense = true;
            }
            else if (Rb_2.Checked)
            {
                IsGlassesOrLense = false;
            }
            if (Rb_IsAstama1.Checked)
            {
                IsAstama = true;
            }
            else if (Rb_IsAstama2.Checked)
            {
                IsAstama = false;
            }

            if (RB_Yes.Checked)
            {
                IsDisabled = true;
            }
            else if (RB_No.Checked)
            {
                IsDisabled = false;
            }
            if (Dd_Belt.SelectedIndex >= 0)
            {
                InitialBeltId = Convert.ToInt32(Dd_Belt.SelectedValue);
            }
            if (Dd_Dojo.SelectedIndex > 0)
            {
                DojoId = Convert.ToInt32(Dd_Dojo.SelectedValue.ToString());
            }

            if (!String.IsNullOrEmpty(Tb_EnrollmentFee.Text))
            {
                EnrollmentFeeAmount = Convert.ToDouble(Tb_EnrollmentFee.Text);
            }

            EnrollmentFeeDiscountAmount = Convert.ToDouble(HfEnrollmentFee.Value) - EnrollmentFeeAmount;

            if (!String.IsNullOrEmpty(Tb_AnnualMembership.Text))
            {
                AnnualMembershipFeeAmount = Convert.ToDouble(Tb_AnnualMembership.Text);
            }

            AnnualMembershipFeeDiscountAmount = Convert.ToDouble(HfAnnualMembership.Value) - AnnualMembershipFeeAmount;

            if (!String.IsNullOrEmpty(Tb_MembershipFee.Text))
            {
                MembershipFeeAmount = Convert.ToDouble(Tb_MembershipFee.Text);
            }

            MembershipFeeDiscountAmount = Convert.ToDouble(HfMembershipFee.Value) - MembershipFeeAmount;

            if (Rb_AlreadyHave.Checked == true)
            {
                Isalreadyhave = true;
            }
            else
            {
                Isalreadyhave = false;
            }

            if (Rb_BronzePackage.Checked == true)
            {
                IsBronzePackage = true;
                BronzePackageFeeAmount = Convert.ToDouble(Lbl_BronzePackage.Text);
            }
            else
            {
                IsBronzePackage = false;
                BronzePackageFeeAmount = 0;
            }


            if (Rb_SilverPackage.Checked == true)
            {
                IsSilverPackage = true;
                SilverPackageFeeAmount = Convert.ToDouble(Lbl_SilverPackage.Text);

            }
            else
            {
                IsSilverPackage = false;
                SilverPackageFeeAmount = 0;
            }

            if (Rb_GoldPackage.Checked == true)
            {
                IsGoldPackage = true;
                GoldPackageFeeAmount = Convert.ToDouble(Lbl_GoldPackage.Text);
            }
            else
            {
                IsGoldPackage = false;
                GoldPackageFeeAmount = 0;
            }

            if (Chk_IsPermanentDiscount.Checked == true)
            {
                IsPermanentDiscount = true;
            }
            else
            {
                IsPermanentDiscount = true;
            }
            if (!string.IsNullOrEmpty(Tb_TermFeeWithDiscount.Text))
            {
                TermFeeForSingleTerm = Convert.ToDouble(Tb_TermFeeWithDiscount.Text);
            }

            if (Dd_Term.SelectedIndex >= 0)
            {
                StartingTermId = Convert.ToInt32(Dd_Term.SelectedValue);
            }
            if (Dd_Term.SelectedIndex >= 0)
            {
                StartingTerm = Dd_Term.SelectedItem.Text;
            }

            if (Dd_NumberOfTerms.SelectedIndex >= 0)
            {
                NumberOfTermsToPay = Convert.ToInt32(Dd_NumberOfTerms.SelectedValue);
            }

            if (!string.IsNullOrEmpty(Tb_TotalTermFee.Text))
            {
                TotalTermFee = Convert.ToDouble(Tb_TotalTermFee.Text);
            }
            if (!string.IsNullOrEmpty(Tb_TotalPaidAmount.Text))
            {
                PaidAmount = Convert.ToDouble(Tb_TotalPaidAmount.Text);
            }

            if (!string.IsNullOrEmpty(Tb_TotalAmountToPaid.Text))
            {
                TotalAmount = Convert.ToDouble(Tb_TotalAmountToPaid.Text);
            }
            if (!String.IsNullOrEmpty(Tb_DiscountAmount.Text))
            {
                TotalDiscount = Convert.ToDouble(Tb_DiscountAmount.Text);
            }
            if (!String.IsNullOrEmpty(Tb_Balance.Text))
            {
                Balance = Convert.ToDouble(Tb_Balance.Text);
            }



            Copy_GridToList(-5);
            if (StudentId_ToUpdate == 0 && HF_StudentId.Value.ToString() == "0")
            {
                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandText = "USP_StudentDetail_Insert";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MembershipDate", Convert.ToDateTime(MembershipDate));
                    cmd.Parameters.AddWithValue("@Title", Title);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@MobileNumber", MobileNumber);
                    cmd.Parameters.AddWithValue("@TelePhoneNumber", TelePhoneNumber);
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);
                    cmd.Parameters.AddWithValue("@BirthDate", Convert.ToDateTime(BirthDate));
                    cmd.Parameters.AddWithValue("@Postcode", Postcode);
                    cmd.Parameters.AddWithValue("@StudentAddress", StudentAddress);
                    cmd.Parameters.AddWithValue("@Premise", Premise);
                    cmd.Parameters.AddWithValue("@County", County);
                    cmd.Parameters.AddWithValue("@Ward", Ward);
                    cmd.Parameters.AddWithValue("@District", District);
                    cmd.Parameters.AddWithValue("@Country", Country);
                    // cmd.Parameters.AddWithValue("@ImageFilePath", ImageFilePath);
                    cmd.Parameters.AddWithValue("@IsAstama", IsAstama);
                    cmd.Parameters.AddWithValue("@IsGlassesOrLense", IsGlassesOrLense);
                    cmd.Parameters.AddWithValue("@IsDisabled", IsDisabled);
                    cmd.Parameters.AddWithValue("@DisabilityDetails", DisabilityDetails);
                    cmd.Parameters.AddWithValue("@OtherMartialArtStudyGradeDetails", OtherMartialArtStudyGradeDetails);
                    cmd.Parameters.AddWithValue("@ExtraRemarks", ExtraRemarks);
                    cmd.Parameters.AddWithValue("@AccountPassword", AccountPassword);

                    cmd.Parameters.AddWithValue("@CreatedDate", CurrentUtc_IND);
                    cmd.Parameters.AddWithValue("@CreatedBy", UserName);

                    SqlParameter OP_MemberShipNumber = new SqlParameter();
                    OP_MemberShipNumber.Direction = ParameterDirection.Output;
                    OP_MemberShipNumber.ParameterName = "@MemberShipNumber";
                    OP_MemberShipNumber.DbType = DbType.String;
                    OP_MemberShipNumber.Size = 255;
                    cmd.Parameters.Add(OP_MemberShipNumber);

                    SqlParameter OP_StudentId = new SqlParameter();
                    OP_StudentId.Direction = ParameterDirection.Output;
                    OP_StudentId.ParameterName = "@StudentId";
                    OP_StudentId.DbType = DbType.Int32;
                    cmd.Parameters.Add(OP_StudentId);

                    cmd.ExecuteNonQuery();

                    StudentId = Convert.ToInt32(OP_StudentId.Value.ToString());

                    HF_StudentId.Value = OP_StudentId.Value.ToString();

                    MembershipNumber = Convert.ToInt32(OP_MemberShipNumber.Value.ToString());

                    string File_Name = "", File_Path = "";
                    if (Fu_StudentImgae.HasFile == true)
                    {
                        File_Name = Path.GetFileName(Fu_StudentImgae.PostedFile.FileName);
                    }
                    if (File_Name != "")
                    {
                        File_Path = "StudentProfile/" + StudentId.ToString() + "_" + File_Name;
                        Fu_StudentImgae.SaveAs(Server.MapPath("StudentProfile/" + StudentId.ToString() + "_" + File_Name));
                        cmd.Parameters.Clear();
                        cmd.CommandText = "USP_Student_Profile_Insert";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProfileImagePath", File_Path);
                        cmd.Parameters.AddWithValue("@StudentId", StudentId);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        File_Path = "";
                    }


                    if (StudentId != 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "USP_StudentDojoBelt_Update";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@StudentId", StudentId);
                        cmd.Parameters.AddWithValue("@DojoId", DojoId);
                        cmd.Parameters.AddWithValue("@InitialBeltId", InitialBeltId);
                        cmd.Parameters.AddWithValue("@CurrentBeltId", InitialBeltId);
                        cmd.Parameters.AddWithValue("@ModifiedDate", CurrentUtc_IND);
                        cmd.Parameters.AddWithValue("@ModifiedBy", UserName);

                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.CommandText = "USP_StudentDojoClassUncheckAll";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@StudentId", StudentId);
                        cmd.Parameters.AddWithValue("@ModifiedDate", CurrentUtc_IND);
                        cmd.Parameters.AddWithValue("@ModifiedBy", UserName);

                        cmd.ExecuteNonQuery();
                        for (int i = 0; i < Gv_ClassSchedule.Rows.Count; i++)
                        {
                            CheckBox Chk_IsSelected = (CheckBox)Gv_ClassSchedule.Rows[i].FindControl("Chk_IsSelected");
                            if (Chk_IsSelected.Checked == true)
                            {
                                DojoClassesScheduleId = Convert.ToInt32(Gv_ClassSchedule.DataKeys[i].Value.ToString());
                                //try
                                //{
                                cmd.Parameters.Clear();
                                cmd.CommandText = "USP_StudentDojoClassInsertUpdate";
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                                cmd.Parameters.AddWithValue("@DojoClassesScheduleId", DojoClassesScheduleId);
                                cmd.Parameters.AddWithValue("@IsDeleted", 0);
                                cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                                cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                                cmd.Parameters.AddWithValue("@ModifiedDate", CurrentUtc_IND);
                                cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        if (StudentId != 0)
                        {
                            try
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandText = "USP_StudentFeeHeader_Insert";
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                                cmd.Parameters.AddWithValue("@FeeId", FeeId);
                                cmd.Parameters.AddWithValue("@EnrollmentFeeAmount", EnrollmentFeeAmount);
                                cmd.Parameters.AddWithValue("@EnrollmentFeeDiscountAmount", EnrollmentFeeDiscountAmount);

                                cmd.Parameters.AddWithValue("@AnnualMembershipFeeAmount", AnnualMembershipFeeAmount);
                                cmd.Parameters.AddWithValue("@AnnualMembershipFeeDiscountAmount", AnnualMembershipFeeDiscountAmount);
                                cmd.Parameters.AddWithValue("@MembershipFeeAmount", MembershipFeeAmount);
                                cmd.Parameters.AddWithValue("@MembershipFeeDiscountAmount", MembershipFeeDiscountAmount);
                                cmd.Parameters.AddWithValue("@Isalreadyhave", Isalreadyhave);

                                cmd.Parameters.AddWithValue("@IsBronzePackage", IsBronzePackage);
                                cmd.Parameters.AddWithValue("@BronzePackageFeeAmount", BronzePackageFeeAmount);
                                cmd.Parameters.AddWithValue("@IsSilverPackage", IsSilverPackage);
                                cmd.Parameters.AddWithValue("@SilverPackageFeeAmount", SilverPackageFeeAmount);
                                cmd.Parameters.AddWithValue("@IsGoldPackage", IsGoldPackage);
                                cmd.Parameters.AddWithValue("@GoldPackageFeeAmount", GoldPackageFeeAmount);
                                cmd.Parameters.AddWithValue("@IsDVD", IsDVD);
                                cmd.Parameters.AddWithValue("@DVDAmount", DVDAmount);
                                cmd.Parameters.AddWithValue("@IsPermanentDiscount", IsPermanentDiscount);
                                cmd.Parameters.AddWithValue("@TermFeeForSingleTerm", TermFeeForSingleTerm);
                                cmd.Parameters.AddWithValue("@TermFeeDiscountAmount", TermFeeDiscountAmount);
                                cmd.Parameters.AddWithValue("@StartingTermId", StartingTermId);
                                cmd.Parameters.AddWithValue("@StartingTerm", StartingTerm);
                                cmd.Parameters.AddWithValue("@NumberOfTermsToPay", NumberOfTermsToPay);
                                cmd.Parameters.AddWithValue("@TotalTermFee", TotalTermFee);
                                cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                                cmd.Parameters.AddWithValue("@PaidAmount", PaidAmount);
                                cmd.Parameters.AddWithValue("@TotalDiscount", TotalDiscount);
                                cmd.Parameters.AddWithValue("@Balance", Balance);
                                cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                                cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);


                                cmd.ExecuteNonQuery();

                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }


                        foreach (CS_Fees item in List_Fees)
                        {
                            try
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandText = "USP_StudentFeesPaidDetailInsert";
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@StudentFeesDetailId", 0);
                                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                                cmd.Parameters.AddWithValue("@FeeId", item.FeeId);
                                cmd.Parameters.AddWithValue("@FeeGenerationTypeId", item.FeeGenerationTypeId);
                                cmd.Parameters.AddWithValue("@FeeGenerationTypeReferenceId", item.FeeGenerationTypeReferenceId);
                                cmd.Parameters.AddWithValue("@FeeCollectionStageId", 1); //1	StudentRegistrationStage	At the time of Student Registration
                                cmd.Parameters.AddWithValue("@FeeCollectionStageReferenceId", StudentId);//Enter StudentId in tbl_StudentFeesDetails for column FeeCollectionStageReferenceId
                                cmd.Parameters.AddWithValue("@IsInitial", 0);
                                cmd.Parameters.AddWithValue("@Amount", item.Amount);
                                cmd.Parameters.AddWithValue("@DiscountAmount", item.DiscountAmount);
                                cmd.Parameters.AddWithValue("@FinalAmount", item.FinalAmount);
                                cmd.Parameters.AddWithValue("@AmountPaid", item.AmountPaid);
                                cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                                cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                                cmd.Parameters.AddWithValue("@FeeGeneratedAtId", 1); //At Student Registration

                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }


                    }
                    tran.Commit();
                    con.Close();
                    cmd.Parameters.Clear();

                    StudentId = Convert.ToInt32(HF_StudentId.Value);
                    HF_StudentId_ToPrint.Value = encrypt(HF_StudentId.Value);
                    Btn_SaveFees.Enabled = false;


                    SendMail(FullName, EmailId);
                    SendInvitationLink(FullName, EmailId);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "myFunction();", true);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {

                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandText = "USP_StudentDetail_Update";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", StudentId_ToUpdate);
                    cmd.Parameters.AddWithValue("@MemberShipNumber", MembershipNumber);
                    cmd.Parameters.AddWithValue("@MembershipDate", Convert.ToDateTime(MembershipDate));
                    cmd.Parameters.AddWithValue("@Title", Title);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@MobileNumber", MobileNumber);
                    cmd.Parameters.AddWithValue("@TelePhoneNumber", TelePhoneNumber);
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);
                    cmd.Parameters.AddWithValue("@BirthDate", Convert.ToDateTime(BirthDate));
                    cmd.Parameters.AddWithValue("@Postcode", Postcode);
                    cmd.Parameters.AddWithValue("@StudentAddress", StudentAddress);
                    cmd.Parameters.AddWithValue("@Premise", Premise);
                    cmd.Parameters.AddWithValue("@County", County);
                    cmd.Parameters.AddWithValue("@Ward", Ward);
                    cmd.Parameters.AddWithValue("@District", District);
                    cmd.Parameters.AddWithValue("@Country", Country);
                    cmd.Parameters.AddWithValue("@IsAstama", IsAstama);
                    cmd.Parameters.AddWithValue("@IsGlassesOrLense", IsGlassesOrLense);
                    cmd.Parameters.AddWithValue("@IsDisabled", IsDisabled);
                    cmd.Parameters.AddWithValue("@DisabilityDetails", DisabilityDetails);
                    cmd.Parameters.AddWithValue("@OtherMartialArtStudyGradeDetails", OtherMartialArtStudyGradeDetails);
                    cmd.Parameters.AddWithValue("@ExtraRemarks", ExtraRemarks);
                    cmd.Parameters.AddWithValue("@ModifiedDate", CurrentUtc_IND);
                    cmd.Parameters.AddWithValue("@ModifiedBy", UserName);

                    cmd.ExecuteNonQuery();

                    string File_Name, File_Path = "";
                    File_Name = Path.GetFileName(Fu_StudentImgae.PostedFile.FileName);

                    if (File_Name != "")
                    {
                        File_Path = "StudentProfile/" + StudentId_ToUpdate.ToString() + "_" + File_Name;
                        Fu_StudentImgae.SaveAs(Server.MapPath("StudentProfile/" + StudentId_ToUpdate.ToString() + "_" + File_Name));

                        cmd.Parameters.Clear();
                        cmd.CommandText = "USP_Student_Profile_Insert";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProfileImagePath", File_Path);
                        cmd.Parameters.AddWithValue("@StudentId", StudentId_ToUpdate);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        File_Path = "";
                    }


                    if (StudentId != 0 || StudentId_ToUpdate != 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "USP_StudentDojoBelt_Update";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@StudentId", StudentId);
                        cmd.Parameters.AddWithValue("@DojoId", DojoId);
                        cmd.Parameters.AddWithValue("@InitialBeltId", InitialBeltId);
                        cmd.Parameters.AddWithValue("@CurrentBeltId", InitialBeltId);
                        cmd.Parameters.AddWithValue("@ModifiedDate", CurrentUtc_IND);
                        cmd.Parameters.AddWithValue("@ModifiedBy", UserName);

                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.CommandText = "USP_StudentDojoClassUncheckAll";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@StudentId", StudentId);
                        cmd.Parameters.AddWithValue("@ModifiedDate", CurrentUtc_IND);
                        cmd.Parameters.AddWithValue("@ModifiedBy", UserName);

                        cmd.ExecuteNonQuery();
                        for (int i = 0; i < Gv_ClassSchedule.Rows.Count; i++)
                        {
                            CheckBox Chk_IsSelected = (CheckBox)Gv_ClassSchedule.Rows[i].FindControl("Chk_IsSelected");
                            if (Chk_IsSelected.Checked == true)
                            {
                                DojoClassesScheduleId = Convert.ToInt32(Gv_ClassSchedule.DataKeys[i].Value.ToString());
                                //try
                                //{
                                cmd.Parameters.Clear();
                                cmd.CommandText = "USP_StudentDojoClassInsertUpdate";
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                                cmd.Parameters.AddWithValue("@DojoClassesScheduleId", DojoClassesScheduleId);
                                cmd.Parameters.AddWithValue("@IsDeleted", 0);
                                cmd.Parameters.AddWithValue("@CreateDate", CurrentUtc_IND);
                                cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                                cmd.Parameters.AddWithValue("@ModifiedDate", CurrentUtc_IND);
                                cmd.Parameters.AddWithValue("@ModifiedBy", UserName);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    tran.Commit();
                    con.Close();
                    cmd.Parameters.Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "redirectPage();", true);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }


    protected void SendMail(string Fullname, string emailid)
    {
        try
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("poojapm.patil444@gmail.com");
            mail.To.Add(emailid);
            mail.Subject = "Welcome to DO-GOJU-KAI Karate";
            StringBuilder body = new StringBuilder();
            body.Append("Dear:" + ' ' + Fullname.Trim() + " ");
            //body.Append("<h1><b><u>Details of Student Registration</u><b></h1>");
            body.Append("<p>Welcome to Do-Jo-kai karate Thank you for registring with best karate academy in Uk.</p>");
            body.Append("We will strive to ensure you have the best experience.While learning karate at our academy.We have best trainers");
            body.Append(" in UK.Who have been fully certified by the top karate organisation in the world.");
            body.Append("<br>We aim to combine fun learning with dedicated progress plan.</br><br><p>For further information you can contact:07886367649 or Email: amrit.ghatore@gmail.com</p><p>Regards,<br>DO-GOJU-KAI Karate Team.<br></p>");

            string htmlBody = body.ToString();
            mail.Body = htmlBody;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            SmtpServer.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);

            // SmtpServer.Credentials = new System.Net.NetworkCredential("poojapm.patil444@gmail.com", "poojarocket444");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void SendInvitationLink(string Fullname, string emailid)
    {
        try
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("poojapm.patil444@gmail.com");
            mail.To.Add(emailid);
            mail.Subject = "Verify Your Email Address";
            StringBuilder body = new StringBuilder();
            body.Append("Dear User");
            //body.Append("<h1><b><u>Details of Student Registration</u><b></h1>");

            body.Append("<p>Please click on the below activation link to verify your email address.</p>");
            body.Append("<br>http://localhost:64701/Uk_Karate_Varsha_Esha/Form_StudentLogin.aspx </br><br><p>Please <a href=http://localhost:64701/Uk_Karate_Varsha_Esha/Form_StudentLogin.aspx > click here</a>to sign in with the following credentials :</p>");
            body.Append("<br>Username:" + ' ' + emailid + " </br>");
            body.Append("<br>Password:" + ' ' + Tb_AccountPassword.Text + " </br>");

            string htmlBody = body.ToString();
            mail.Body = htmlBody;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            // SmtpServer.Credentials = new System.Net.NetworkCredential("poojapm.patil444@gmail.com", "poojarocket444");

            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            SmtpServer.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);

            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void Btn_FeesBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Form_ManageStudents.aspx");
    }
    protected void Gv_Fee_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    private void FeelFeeForFeeGenerationTypeReference(DropDownList Dd_FeeForFeeGenerationTypeReference, int FeeId)
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetFeeForFeeGenerationTypeReference";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FeeId", FeeId);
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "FeeForFeeGenerationTypeReference");
            Dd_FeeForFeeGenerationTypeReference.DataSource = ds.Tables["FeeForFeeGenerationTypeReference"];
            Dd_FeeForFeeGenerationTypeReference.DataBind();


            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    protected void Dd_Fees_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList Dd_Fees = sender as DropDownList;
        int SelectedIndex = Convert.ToInt32(Dd_Fees.SelectedIndex.ToString());

    }


    private int ValidateData()
    {
        int Go = 1;
        string ValidationMsg = "";
        if (Tb_FirstName.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter First Name.\\n";
            Tb_FirstName.BorderColor = Color.Red;
        }
        else
        {
            Tb_FirstName.BorderColor = Color.LightGray;
        }


        if (Tb_LastName.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter last Name.\\n";
            Tb_LastName.BorderColor = Color.Red;
        }
        else
        {
            Tb_LastName.BorderColor = Color.LightGray;
        }

        if (Tb_Mobile.Text.ToString() == "" && Tb_Telephone.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter mobile number or telephone number.\n";
            if (Tb_Mobile.Text == "")
            {
                Tb_Mobile.BorderColor = Color.Red;
                Tb_Telephone.BorderColor = Color.Red;
            }

        }
        else
        {
            Tb_Mobile.BorderColor = Color.LightGray;
            Tb_Telephone.BorderColor = Color.LightGray;
        }

        if (Tb_Email.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter email adress.\\n";
            Tb_Email.BorderColor = Color.Red;
        }
        else
        {
            Tb_Email.BorderColor = Color.LightGray;
        }
        if (Dp_BirthDate.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter Birth date.\\n";
            Dp_BirthDate.BorderColor = Color.Red;

        }
        else
        {
            Dp_BirthDate.BorderColor = Color.LightGray;
        }

        if (RB_Yes.Checked == true)
        {
            if (Tb_Disability.Text.ToString() == "")
            {
                Go = 0;
                ValidationMsg += "Please Mention Disability.\\n";

                Tb_Disability.BorderColor = Color.Red;
                SetFocus(Tb_Disability);
            }
            else
            {
                Tb_Disability.BorderColor = Color.LightGray;
            }
        }
        else
        {
            Tb_Disability.Enabled = false;
        }

        if (Dd_Dojo.SelectedIndex <= 0)
        {
            Go = 0;
            ValidationMsg += "Please Select Dojo.\\n";
            Dd_Dojo.BorderColor = Color.Red;
        }
        else
        {
            Dd_Dojo.BorderColor = Color.LightGray;
        }


        if (Tb_TotalPaidAmount.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter Total Paid Amount.\\n";
            Tb_TotalPaidAmount.BorderColor = Color.Red;
        }
        else
        {
            Tb_TotalPaidAmount.BorderColor = Color.LightGray;
        }


        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :\\n" + ValidationMsg.ToString() + "')", true);
        }
        return Go;
    }
    private int ValidateDataToUpdate()
    {
        int Go = 1;
        string ValidationMsg = "";
        if (Tb_FirstName.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter First Name.\\n";
            Tb_FirstName.BorderColor = Color.Red;
        }
        else
        {
            Tb_FirstName.BorderColor = Color.LightGray;
        }


        if (Tb_LastName.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter last Name.\\n";
            Tb_LastName.BorderColor = Color.Red;
        }
        else
        {
            Tb_LastName.BorderColor = Color.LightGray;
        }

        if (Tb_Mobile.Text.ToString() == "" && Tb_Telephone.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter mobile number or telephone number.\n";
            if (Tb_Mobile.Text == "")
            {
                Tb_Mobile.BorderColor = Color.Red;
                Tb_Telephone.BorderColor = Color.Red;
            }

        }
        else
        {
            Tb_Mobile.BorderColor = Color.LightGray;
            Tb_Telephone.BorderColor = Color.LightGray;
        }

        if (Tb_Email.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter email adress.\\n";
            Tb_Email.BorderColor = Color.Red;
        }
        else
        {
            Tb_Email.BorderColor = Color.LightGray;
        }
        if (Dp_BirthDate.Text.ToString() == "")
        {
            Go = 0;
            ValidationMsg += "Please enter Birth date.\\n";
            Dp_BirthDate.BorderColor = Color.Red;

        }
        else
        {
            Dp_BirthDate.BorderColor = Color.LightGray;
        }

        if (RB_Yes.Checked == true)
        {
            if (Tb_Disability.Text.ToString() == "")
            {
                Go = 0;
                ValidationMsg += "Please Mention Disability.\\n";

                Tb_Disability.BorderColor = Color.Red;
                SetFocus(Tb_Disability);
            }
            else
            {
                Tb_Disability.BorderColor = Color.LightGray;
            }
        }
        else
        {
            Tb_Disability.Enabled = false;
        }

        if (Go == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :\\n" + ValidationMsg.ToString() + "')", true);
        }
        return Go;
    }

    private void Fill_StudentFees()
    {
        Double TotalAmount = 0;
        int KarateKit = 0;
        if (Rb_AlreadyHave.Checked == true)
        {
            KarateKit = 1;
        }
        if (Rb_BronzePackage.Checked == true)
        {
            KarateKit = 2;
        }
        if (Rb_SilverPackage.Checked == true)
        {
            KarateKit = 3;
        }
        if (Rb_GoldPackage.Checked == true)
        {
            KarateKit = 4;
        }

        try
        {
            List_Fees.Clear();
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_GetStudentFeeDetailsOnStage1";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@KarateKit", KarateKit);
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                List_Fees.Add(
                    new CS_Fees()
                    {
                        FeeId = Convert.ToInt32(dr["FeeId"].ToString()),
                        FeeName = dr["FeeName"].ToString(),
                        FeeTemplateId = Convert.ToInt32(dr["FeeTemplateId"].ToString()),
                        FeeTemplate = dr["FeeTemplate"].ToString(),
                        FeeGenerationTypeId = Convert.ToInt32(dr["FeeGenerationTypeId"].ToString()),
                        FeeGenerationType = dr["FeeGenerationType"].ToString(),
                        FeeCategoryId = Convert.ToInt32(dr["FeeCategoryId"].ToString()),
                        FeeCategory = dr["FeeCategory"].ToString(),
                        FeeCollectionStageId = Convert.ToInt32(dr["FeeCollectionStageId"].ToString()),
                        FeeCollectionStage = dr["FeeCollectionStage"].ToString(),
                        Amount = Convert.ToDouble(dr["Amount"].ToString()),
                        DiscountAmount = Convert.ToDouble(dr["DiscountAmount"].ToString()),
                        FinalAmount = Convert.ToDouble(dr["FinalAmount"].ToString()),
                        AmountPaid = Convert.ToDouble(dr["AmountPaid"].ToString()),
                        Balance = Convert.ToDouble(dr["Balance"].ToString()),
                        FeeGenerationTypeReferenceId = 0,
                        //   FeeGenerationTypeReference = dr["FeeGenerationTypeReference"].ToString(),
                        IsOneOfTheGroup = Convert.ToBoolean(dr["IsOneOfTheGroup"].ToString()),
                        IsEnabled = Convert.ToBoolean(dr["IsEnabled"].ToString()),
                        RadioGroup = dr["FeeCategoryId"].ToString(),
                        IsGroupRBChecked = Convert.ToBoolean(dr["IsGroupRBChecked"].ToString()),

                    });
            }
            for (int i = 0; i < List_Fees.Count; i++)
            {
                List_Fees[i].Index = i;
                List_Fees[i].SrNo = i;
                TotalAmount += List_Fees[i].Amount;
                Hf_TotalAmount.Value = TotalAmount.ToString();
            }
            {
                Gv_FeeDetail.DataSource = List_Fees;
                Gv_FeeDetail.DataBind();
            }

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    private void Fill_Term()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "select QuarterId TermId ,QuarterName Term from Tbl_Quarter";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Title");
            Dd_Term.DataSource = ds.Tables["Title"];
            Dd_Term.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    private void Fill_FeeDetailAmount()
    {
        double TotalAmount = 0, KarateKit = 0, DVDAmount = 0;

        List_Fees.Clear();
        con.ConnectionString = str;
        con.Open();
        cmd = new SqlCommand();
        cmd.CommandText = "Ups_GetFeeDetailForStudentRegistration";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            Lbl_EnrollmentFee.Text = dr["EnrollmentFee"].ToString();
            Tb_EnrollmentFee.Text = dr["EnrollmentFee"].ToString();
            HfEnrollmentFee.Value = dr["EnrollmentFee"].ToString();

        }
        dr.NextResult();
        {
            while (dr.Read())
            {
                Lbl_AnnualMembership.Text = dr["AnnualMembershipFee"].ToString();
                Tb_AnnualMembership.Text = dr["AnnualMembershipFee"].ToString();
                HfAnnualMembership.Value = dr["AnnualMembershipFee"].ToString();
            }
        }
        dr.NextResult();
        {
            while (dr.Read())
            {
                Lbl_MembershipFee.Text = dr["MembershipFee"].ToString();
                Tb_MembershipFee.Text = dr["MembershipFee"].ToString();
                HfMembershipFee.Value = dr["MembershipFee"].ToString();
            }
        }
        dr.NextResult();
        {
            while (dr.Read())
            {
                Lbl_BronzePackage.Text = dr["BronzeKarate"].ToString(); //"- £" +
            }
        }
        dr.NextResult();
        {
            while (dr.Read())
            {
                Lbl_SilverPackage.Text = dr["SilverPackage"].ToString();
            }
        }
        dr.NextResult();
        {
            while (dr.Read())
            {
                Lbl_GoldPackage.Text = dr["GoldPackage"].ToString();
            }
        }
        dr.NextResult();
        {
            while (dr.Read())
            {
                Tb_TermFeeWithDiscount.Text = dr["TermFees"].ToString();
                Tb_TotalTermFee.Text = dr["TermFees"].ToString();
                HfTermFee.Value = dr["TermFees"].ToString();
            }
        }
        dr.NextResult();
        {
            while (dr.Read())
            {
                Lbl_DVD.Text = dr["DVD"].ToString();
                HfDVD.Value = dr["DVD"].ToString();
            }
        }
        if (Rb_BronzePackage.Checked == true)
        {
            KarateKit = Convert.ToDouble(Lbl_BronzePackage.Text);
        }
        else if (Rb_SilverPackage.Checked == true)
        {
            KarateKit = Convert.ToDouble(Lbl_SilverPackage.Text);
        }
        else if (Rb_GoldPackage.Checked == true)
        {
            KarateKit = Convert.ToDouble(Lbl_GoldPackage.Text);
        }
        if (Chk_IsDVD.Checked == true)
        {
            DVDAmount = Convert.ToDouble(HfDVD.Value);
        }
        else
        {
            DVDAmount = 0;
        }

        TotalAmount = Convert.ToDouble(Tb_EnrollmentFee.Text) + Convert.ToDouble(Tb_AnnualMembership.Text) + Convert.ToDouble(Tb_MembershipFee.Text) + KarateKit + Convert.ToDouble(Tb_TotalTermFee.Text) + DVDAmount;
        Tb_TotalAmount.Text = TotalAmount.ToString();
        Hf_TotalAmount.Value = TotalAmount.ToString();
        dr.Close();
        con.Close();
    }

    private void Calculate()
    {
        double EnrollmentFee = 0, AnnualMembershipFee = 0, MembershipFee = 0, TermFeeWithDiscount = 0,
            TotalTermFeeRequired = 0, TotalAmountPaid = 0, TotalAmount = 0, Balance = 0,
            BronzeKarate = 0, SilverPackage = 0, GoldPackage = 0;
        int TermId = 0, NumberOfTerm = 0, IsPermanentDiscount = 0;


        EnrollmentFee = Convert.ToDouble(Tb_EnrollmentFee.Text);
        AnnualMembershipFee = Convert.ToDouble(Tb_AnnualMembership.Text);
        MembershipFee = Convert.ToDouble(Tb_MembershipFee.Text);
        //if (Rb_BronzePackage.Checked == true)
        //{
        //    BronzeKarate = Convert.ToDouble(Lbl_BronzePackage.Text);
        //}
        //else
        //{
        //    BronzeKarate = 0.00;
        //}
        //if (Rb_SilverPackage.Checked == true)
        //{
        //    SilverPackage = Convert.ToDouble(Lbl_SilverPackage.Text);
        //}
        //else
        //{
        //    SilverPackage = 0.00;
        //}
        //if (Rb_GoldPackage.Checked == true)
        //{
        //    GoldPackage = Convert.ToDouble(Lbl_GoldPackage.Text);
        //}
        //else
        //{
        //    GoldPackage = 0.00;
        //}
        BronzeKarate = Convert.ToDouble(Lbl_BronzePackage.Text);
        SilverPackage = Convert.ToDouble(Lbl_SilverPackage.Text);
        GoldPackage = Convert.ToDouble(Lbl_GoldPackage.Text);
        if (Chk_IsPermanentDiscount.Checked == true)
        {
            IsPermanentDiscount = 1;
        }
        else
        {
            IsPermanentDiscount = 0;
        }
        TermFeeWithDiscount = Convert.ToDouble(Tb_TermFeeWithDiscount.Text);
        NumberOfTerm = Convert.ToInt32(Dd_NumberOfTerms.SelectedValue);

        if (Dd_NumberOfTerms.SelectedIndex >= 0)
        {
            TotalTermFeeRequired = TermFeeWithDiscount * NumberOfTerm;
            Tb_TotalTermFee.Text = TotalTermFeeRequired.ToString();
        }

        if (Rb_AlreadyHave.Checked == true)
        {
            TotalAmountPaid = EnrollmentFee + AnnualMembershipFee + MembershipFee + TotalTermFeeRequired;
        }
        if (Rb_BronzePackage.Checked == true)
        {
            TotalAmountPaid = EnrollmentFee + AnnualMembershipFee + MembershipFee + BronzeKarate + TotalTermFeeRequired;
        }
        if (Rb_GoldPackage.Checked == true)
        {
            TotalAmountPaid = EnrollmentFee + AnnualMembershipFee + MembershipFee + GoldPackage + TotalTermFeeRequired;
        }
        if (Rb_SilverPackage.Checked == true)
        {
            TotalAmountPaid = EnrollmentFee + AnnualMembershipFee + MembershipFee + SilverPackage + TotalTermFeeRequired;
        }
        if (TotalAmountPaid != 0)
        {
            Tb_TotalPaidAmount.Text = TotalAmountPaid.ToString();
        }
        else
        {
            TotalAmountPaid = 0.00;
        }

        if (Hf_TotalAmount.Value != "0")
        {
            if (Rb_BronzePackage.Checked == true)
            {
                TotalAmount = Convert.ToDouble(Hf_TotalAmount.Value) - SilverPackage - GoldPackage + TotalTermFeeRequired;
            }
            if (Rb_SilverPackage.Checked == true)
            {
                TotalAmount = Convert.ToDouble(Hf_TotalAmount.Value) - BronzeKarate - GoldPackage + TotalTermFeeRequired;
            }
            if (Rb_GoldPackage.Checked == true)
            {
                TotalAmount = Convert.ToDouble(Hf_TotalAmount.Value) - BronzeKarate - SilverPackage + TotalTermFeeRequired;
            }
        }

        Tb_TotalAmount.Text = TotalAmount.ToString();
        Balance = TotalAmount - TotalAmountPaid;
        Tb_Balance.Text = Balance.ToString();



    }

    protected void Tb_EnrollmentFee_TextChanged(object sender, EventArgs e)
    {
        int FeeId = 3;
        Copy_GridToList1(-5);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "AllocateEnrollmentAmount();", true);
        if (Tb_TotalPaidAmount.Text != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "AllocateAmount();", true);
        }
        CalculateTotal();
    }
    protected void Tb_AnnualMembership_TextChanged(object sender, EventArgs e)
    {
        int FeeId = 1;
        Copy_GridToList1(-5);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "AllocateAnnualMembershipAmount();", true);
        if (Tb_TotalPaidAmount.Text != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "AllocateAmount();", true);
        }
        CalculateTotal();
    }
    protected void Tb_MembershipFee_TextChanged(object sender, EventArgs e)
    {
        int FeeId = 2;
        Copy_GridToList1(-5);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "AllocateMembershipAmount();", true);
        if (Tb_TotalPaidAmount.Text != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "AllocateAmount();", true);
        }
        CalculateTotal();
    }
    protected void Rb_AlreadyHave_CheckedChanged(object sender, EventArgs e)
    {
        Fill_StudentFees();
        CalculateTotal();
    }
    protected void Rb_BronzePackage_CheckedChanged(object sender, EventArgs e)
    {
        Fill_StudentFees();
        CalculateTotal();
    }
    protected void Rb_SilverPackage_CheckedChanged(object sender, EventArgs e)
    {

        Fill_StudentFees();

        CalculateTotal();
    }
    protected void Rb_GoldPackage_CheckedChanged(object sender, EventArgs e)
    {
        Fill_StudentFees();
        CalculateTotal();
    }
    protected void Dd_NumberOfTerms_SelectedIndexChanged(object sender, EventArgs e)
    {
        int NewTermId = 0, TermId = 0;
        string TermIds = "", Delimiter = ",";
        Hf_NumberOfTerms.Value = Dd_NumberOfTerms.SelectedValue;
        int NumberOfTerms = Convert.ToInt32(Dd_NumberOfTerms.SelectedValue);
        TermId = Convert.ToInt32(Dd_Term.SelectedValue);

        if (NumberOfTerms == 1)
        {
            Lbl_SecondTerm.Visible = false;
            Tb_SecondTermFee.Visible = false;

            Lbl_ThirdTerm.Visible = false;
            Tb_ThirdTermFee.Visible = false;

            Lbl_FourthTerm.Visible = false;
            Tb_FourthTerm.Visible = false;
        }
        if (NumberOfTerms == 2)
        {
            NewTermId = TermId + 1;

            for (int i = TermId + 1; i <= NewTermId; i++)
            {
                TermIds = TermIds + "," + i;
            }

            Lbl_SecondTerm.Visible = true;
            Tb_SecondTermFee.Visible = true;

            Lbl_ThirdTerm.Visible = false;
            Tb_ThirdTermFee.Visible = false;

            Lbl_FourthTerm.Visible = false;
            Tb_FourthTerm.Visible = false;
        }
        if (NumberOfTerms == 3)
        {
            NewTermId = TermId + 2;

            for (int i = TermId + 1; i <= NewTermId; i++)
            {
                TermIds = TermIds + "," + i;
            }

            Lbl_SecondTerm.Visible = true;
            Tb_SecondTermFee.Visible = true;

            Lbl_ThirdTerm.Visible = true;
            Tb_ThirdTermFee.Visible = true;

            Lbl_FourthTerm.Visible = false;
            Tb_FourthTerm.Visible = false;
        }
        if (NumberOfTerms == 4)
        {
            NewTermId = TermId + 3;

            for (int i = TermId + 1; i <= NewTermId; i++)
            {
                TermIds = TermIds + "," + i;
            }
            Lbl_SecondTerm.Visible = true;
            Tb_SecondTermFee.Visible = true;

            Lbl_ThirdTerm.Visible = true;
            Tb_ThirdTermFee.Visible = true;

            Lbl_FourthTerm.Visible = true;
            Tb_FourthTerm.Visible = true;
        }

        try
        {
            //cmd.CommandText = "UPS_GetTermFeeCalulation";
            //cmd.Connection = con;
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@QuarterId", TermIds);

            //dr = cmd.ExecuteReader();
            //if (dr.Read())
            //{
                
            //    Tb_SecondTermFee.Text = dr["Term"].ToString();
            //}

            con.ConnectionString = str;
            da = new SqlDataAdapter("select QuarterId TermId ,QuarterName Term from Tbl_Quarter Q	join FN_Split(" + "(" + TermIds + ") s on Q.QuarterId = s.items", con);
            DataTable dttc = new DataTable();

            da.Fill(dttc);
            if (dttc.Rows.Count > 0)
            {
                Tb_SecondTermFee.Text = dttc.Rows[0]["Term"].ToString();
                Tb_ThirdTermFee.Text = dttc.Rows[1]["Term"].ToString();
                Tb_FourthTerm.Text = dttc.Rows[2]["Term"].ToString();
            }

        }

        catch (Exception ex)
        {
            throw ex;
        }



        Copy_GridToList(-5);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "AllocateTermFeeAmount();", true);
        if (Tb_TotalPaidAmount.Text != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "AllocateAmount();", true);
        }

        CalculateTotal();
    }
    protected void Tb_TotalPaidAmount_TextChanged(object sender, EventArgs e)
    {
        Copy_GridToList1(-5);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "AllocateAmount();", true);
        CalculateTotal();

        //  Calculate();
    }

    private void CalculateTotal()
    {
        int NumberofTerm = 0, IsDVD = 0;
        double EnrollmentFee = 0, AnnualMembershipFee = 0, MembershipFee = 0, TermFeeWithDiscount = 0,
            TotalTermFeeRequired = 0, TotalAmountPaid = 0, RequiredTotalAmount = 0, Balance = 0, TotalAmount = 0,
            KarateKit = 0, DVDAmount = 0, RequiredDVDAmount = 0;
        double RequiredEnrollmentFee = 0, RequiredAnnualMembershipFee = 0, RequiredMembershipFee = 0, RequiredTermFee = 0, DiscountAmount = 0, NewDiscountAmount = 0;
        EnrollmentFee = Convert.ToDouble(Tb_EnrollmentFee.Text);
        RequiredEnrollmentFee = Convert.ToDouble(Lbl_EnrollmentFee.Text);

        Copy_GridToList1(-5);
        double Amount = 0;
        Amount = Convert.ToDouble(Hf_TotalAmount.Value);
        if (!String.IsNullOrEmpty(Tb_TotalPaidAmount.Text))
        {
            TotalAmountPaid = Convert.ToDouble(Tb_TotalPaidAmount.Text);
        }
        //if (TotalAmountPaid > Amount)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Paid amount must be less than total value.')", true);
        //    Tb_TotalPaidAmount.BorderColor = Color.Red;
        //}

        //else
        //{
        if (EnrollmentFee < RequiredEnrollmentFee)
        {
            DiscountAmount = RequiredEnrollmentFee - EnrollmentFee;
            Tb_DiscountAmount.Text = DiscountAmount.ToString();
            NewDiscountAmount = DiscountAmount;
        }
        AnnualMembershipFee = Convert.ToDouble(Tb_AnnualMembership.Text);
        RequiredAnnualMembershipFee = Convert.ToDouble(Lbl_AnnualMembership.Text);

        if (AnnualMembershipFee < RequiredAnnualMembershipFee)
        {
            DiscountAmount = RequiredAnnualMembershipFee - AnnualMembershipFee;

            NewDiscountAmount = NewDiscountAmount + DiscountAmount;
            Tb_DiscountAmount.Text = NewDiscountAmount.ToString();
        }

        MembershipFee = Convert.ToDouble(Tb_MembershipFee.Text);
        RequiredMembershipFee = Convert.ToDouble(Lbl_MembershipFee.Text);
        if (MembershipFee < RequiredAnnualMembershipFee)
        {
            DiscountAmount = RequiredMembershipFee - MembershipFee;
            NewDiscountAmount = NewDiscountAmount + DiscountAmount;
            Tb_DiscountAmount.Text = NewDiscountAmount.ToString();
        }

        if (Rb_BronzePackage.Checked == true)
        {
            KarateKit = Convert.ToDouble(Lbl_BronzePackage.Text);
        }
        else if (Rb_SilverPackage.Checked == true)
        {
            KarateKit = Convert.ToDouble(Lbl_SilverPackage.Text);
        }
        else if (Rb_GoldPackage.Checked == true)
        {
            KarateKit = Convert.ToDouble(Lbl_GoldPackage.Text);
        }


        RequiredTermFee = Convert.ToDouble(HfTermFee.Value);
        TotalTermFeeRequired = Convert.ToDouble(Tb_TermFeeWithDiscount.Text);


        if (Dd_NumberOfTerms.SelectedIndex >= 0)
        {
            NumberofTerm = Convert.ToInt32(Dd_NumberOfTerms.SelectedValue);
        }
        TotalTermFeeRequired = TotalTermFeeRequired * NumberofTerm;
        Tb_TotalTermFee.Text = TotalTermFeeRequired.ToString();


        if (TotalTermFeeRequired < RequiredTermFee)
        {
            DiscountAmount = RequiredTermFee - TotalTermFeeRequired;

            NewDiscountAmount = NewDiscountAmount + DiscountAmount;
            Tb_DiscountAmount.Text = NewDiscountAmount.ToString();
        }

        if (Chk_IsDVD.Checked == true)
        {
            IsDVD = 1;
            DVDAmount = Convert.ToDouble(Lbl_DVD.Text);
        }
        else
        {
            IsDVD = 0;
            DVDAmount = 0;
        }


        // TotalAmount = (EnrollmentFee + AnnualMembershipFee + MembershipFee + TotalTermFeeRequired + KarateKit - NewDiscountAmount);
        RequiredTotalAmount = (RequiredEnrollmentFee + RequiredAnnualMembershipFee + RequiredMembershipFee + TotalTermFeeRequired + KarateKit + DVDAmount);

        Tb_TotalAmount.Text = RequiredTotalAmount.ToString();
        TotalAmount = RequiredTotalAmount - NewDiscountAmount;
        Tb_TotalAmountToPaid.Text = TotalAmount.ToString();

        if (TotalAmountPaid > TotalAmount)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Paid amount must be less than total value.')", true);
            Tb_TotalPaidAmount.Text = "";
            Tb_TotalPaidAmount.BorderColor = Color.Red;

        }
        else
        {
            Balance = TotalAmount - TotalAmountPaid;
            Tb_Balance.Text = Balance.ToString();
        }






        //}
    }
    protected void Chk_IsPermanentDiscount_CheckedChanged(object sender, EventArgs e)
    {
        if (Chk_IsPermanentDiscount.Checked == true)
        {
            Tb_TermFeeWithDiscount.Enabled = true;
        }
        else
        {
            Tb_TermFeeWithDiscount.Enabled = false;
        }

    }
    protected void Tb_TermFeeWithDiscount_TextChanged(object sender, EventArgs e)
    {
        double TermFee = 0;
        TermFee = Convert.ToDouble(Tb_TermFeeWithDiscount.Text);
        Tb_TotalTermFee.Text = TermFee.ToString();
        Hf_NumberOfTerms.Value = Dd_NumberOfTerms.SelectedValue;
        int FeeId = 4;
        Copy_GridToList(-5);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "AllocateTermFeeAmount();", true);
        if (Tb_TotalPaidAmount.Text != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "AllocateAmount();", true);
        }
        CalculateTotal();
    }

    private void Copy_GridToList(int IndexToSkip)
    {
        int FeeId = 0, Go = 1, Go1 = 1, DiscountTypeId = 0;
        string FeeName = "";
        float RequiredAmount = 0, PaidFee = 0, DiscountPer = 0, DiscountAmount = 0, Total = 0;

        try
        {


            foreach (GridViewRow row in Gv_FeeDetail.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (row.RowIndex != IndexToSkip)
                    {
                        HiddenField Hf_FeeId = (HiddenField)row.FindControl("Hf_FeeId");
                        HiddenField Hf_RequiredFee = (HiddenField)row.FindControl("Hf_RequiredFee");
                        HiddenField Hf_PaidFee = (HiddenField)row.FindControl("Hf_PaidFee");
                        HiddenField Hf_FeeTemplateId = (HiddenField)row.FindControl("Hf_FeeTemplateId");
                        HiddenField Hf_FeeTemplate = (HiddenField)row.FindControl("Hf_FeeTemplate");
                        HiddenField Hf_FeeGenerationTypeId = (HiddenField)row.FindControl("Hf_FeeGenerationTypeId");
                        HiddenField Hf_FeeGenerationType = (HiddenField)row.FindControl("Hf_FeeGenerationType");
                        HiddenField Hf_FeeCollectionStageId = (HiddenField)row.FindControl("Hf_FeeCollectionStageId");
                        HiddenField Hf_FeeCollectionStage = (HiddenField)row.FindControl("Hf_FeeCollectionStage");
                        HiddenField Hf_IsOneOfTheGroup = (HiddenField)row.FindControl("Hf_IsOneOfTheGroup");

                        if (Hf_FeeId.Value != "0") //if not dummy row
                        {
                            RowIndex = row.RowIndex;

                            if (Go == 1 && Go1 == 1)
                            {


                                Label Lbl_FeeName = (Label)row.FindControl("Lbl_FeeName");
                                Label Lbl_RequiredFee = (Label)row.FindControl("Lbl_RequiredFee");
                                Label Lbl_PaidFee = (Label)row.FindControl("Lbl_PaidFee");
                                if (Hf_RequiredFee.Value != "0")
                                {
                                    Lbl_RequiredFee.Text = Hf_RequiredFee.Value;
                                }
                                if (Hf_PaidFee.Value != "0")
                                {
                                    Lbl_PaidFee.Text = Hf_PaidFee.Value;
                                }
                                if (Hf_RequiredFee.Value != "0")
                                {
                                    Lbl_RequiredFee.Text = Hf_RequiredFee.Value;
                                }
                                FeeId = Convert.ToInt32(Hf_FeeId.Value);
                                FeeName = Lbl_FeeName.Text.ToString();


                                RequiredAmount = Convert.ToSingle(Lbl_RequiredFee.Text);
                                PaidFee = Convert.ToSingle(Lbl_PaidFee.Text);

                                List_Fees.Add(new CS_Fees()
                                {
                                    FeeId = FeeId,
                                    FeeTemplateId = Convert.ToInt32(Hf_FeeTemplateId.Value),
                                    FeeTemplate = Hf_FeeTemplate.Value,
                                    FeeGenerationTypeId = Convert.ToInt32(Hf_FeeGenerationTypeId.Value),
                                    FeeGenerationType = Hf_FeeGenerationType.Value,
                                    FeeCollectionStageId = Convert.ToInt32(Hf_FeeCollectionStageId.Value),
                                    FeeCollectionStage = Hf_FeeCollectionStage.Value,
                                    IsOneOfTheGroup = Convert.ToBoolean(Hf_IsOneOfTheGroup.Value),
                                    FeeName = FeeName,
                                    Amount = RequiredAmount,
                                    AmountPaid = PaidFee,

                                });
                            }
                            else
                            {

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

    private void Copy_GridToList1(int IndexToSkip)
    {
        int FeeId = 0, Go = 1, Go1 = 1, DiscountTypeId = 0;
        string FeeName = "";
        float RequiredAmount = 0, PaidFee = 0, DiscountPer = 0, DiscountAmount = 0, Total = 0;

        try
        {


            foreach (GridViewRow row in Gv_FeeDetail.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (row.RowIndex != IndexToSkip)
                    {
                        HiddenField Hf_FeeId = (HiddenField)row.FindControl("Hf_FeeId");
                        HiddenField Hf_RequiredFee = (HiddenField)row.FindControl("Hf_RequiredFee");
                        HiddenField Hf_PaidFee = (HiddenField)row.FindControl("Hf_PaidFee");
                        HiddenField Hf_FeeTemplateId = (HiddenField)row.FindControl("Hf_FeeTemplateId");
                        HiddenField Hf_FeeTemplate = (HiddenField)row.FindControl("Hf_FeeTemplate");
                        HiddenField Hf_FeeGenerationTypeId = (HiddenField)row.FindControl("Hf_FeeGenerationTypeId");
                        HiddenField Hf_FeeGenerationType = (HiddenField)row.FindControl("Hf_FeeGenerationType");
                        HiddenField Hf_FeeCollectionStageId = (HiddenField)row.FindControl("Hf_FeeCollectionStageId");
                        HiddenField Hf_FeeCollectionStage = (HiddenField)row.FindControl("Hf_FeeCollectionStage");
                        HiddenField Hf_IsOneOfTheGroup = (HiddenField)row.FindControl("Hf_IsOneOfTheGroup");

                        if (Hf_FeeId.Value != "0") //if not dummy row
                        {
                            RowIndex = row.RowIndex;

                            if (Go == 1 && Go1 == 1)
                            {


                                Label Lbl_FeeName = (Label)row.FindControl("Lbl_FeeName");
                                Label Lbl_RequiredFee = (Label)row.FindControl("Lbl_RequiredFee");
                                Label Lbl_PaidFee = (Label)row.FindControl("Lbl_PaidFee");
                                if (Hf_RequiredFee.Value != "0")
                                {
                                    Lbl_RequiredFee.Text = Hf_RequiredFee.Value;
                                }
                                if (Hf_PaidFee.Value != "0")
                                {
                                    Lbl_PaidFee.Text = Hf_PaidFee.Value;
                                }
                                if (Hf_RequiredFee.Value != "0")
                                {
                                    Lbl_RequiredFee.Text = Hf_RequiredFee.Value;
                                }
                                FeeId = Convert.ToInt32(Hf_FeeId.Value);
                                FeeName = Lbl_FeeName.Text.ToString();


                                RequiredAmount = Convert.ToSingle(Lbl_RequiredFee.Text);
                                PaidFee = Convert.ToSingle(Lbl_PaidFee.Text);

                            }
                            else
                            {

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

    protected void Chk_IsDVD_CheckedChanged(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Cal", "DVDAmount();", true);
        Fill_FeeDetailAmount();
    }
}