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
using System.Drawing;
public partial class Form_EmployeeMaster_AddUpdate : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    string UserName = "";
    int Employee_Id = 0;
    int EmployeeId_ToUpdate = 0;
    int CallFor = 0;

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

            //ServiceCentre_Id = Convert.ToInt32(Session["ServiceCentre_Id"].ToString());
            UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());
            // EmployeeId_ToUpdate = Convert.ToInt32(Request.QueryString[0].ToString());

            if (Request.QueryString.Count > 0)
            {
                try
                {
                    EmployeeId_ToUpdate = Convert.ToInt32(Request.QueryString[0].ToString());
                    CallFor = Convert.ToInt32(Request.QueryString["Call"].ToString());

                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid Operation');window.location='Home.aspx'", true);
                }
            }

            if (!IsPostBack)
            {
                //Fill_City();
                Fill_Role();
                Fill_ReportingManager();
                Fill_DojoID();
                // Fill_ServiceCentre();

                if (EmployeeId_ToUpdate != 0)
                {
                    Tb_UserName.Enabled = false;
                    Lbl_Password.Visible = false;
                    Tb_Password.Visible = false;
                    Fill_EmployeeDetails();
                }
                if (CallFor == 2) //view
                {
                    Tb_FirstName.Enabled = false;
                    Tb_MiddleName.Enabled = false;
                    Tb_LastName.Enabled = false;

                    Tb_AddressLine1.Enabled = false;
                    Tb_AddressLine2.Enabled = false;
                    Tb_City.Enabled = false;
                    Tb_PostalCode.Enabled = false;
                    Tb_ContactMobile.Enabled = false;
                    Tb_EmailID.Enabled = false;
                    Dd_Role.Enabled = false;
                    DD_EmployeeStatus.Enabled = false;
                    Tb_UserName.Enabled = false;
                    Tb_Password.Enabled = false;
                    Fu_EmployeeImgae.Enabled = false;
                    DD_Gender.Enabled = false;
                    Dd_ReportingManager.Enabled = false;
                    Btn_Save.Enabled = false;
                    Btn_New.Enabled = false;
                    Btn_Back.Enabled = false;

                    Fill_EmployeeDetails();
                }
                if (CallFor == 3) //Edit
                {
                    Fill_EmployeeDetails();
                }
            }
        }

    }


    private void Fill_ReportingManager()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "select Employee_Id,isnull(FirstName,'')+' '+isnull(MiddleName,'')+' '+isnull(LastName,'') Employee_Name From tbl_Employee where  Employee_Status_Id=1 Union select 0 Employee_Id,'---select---' Employee_Name";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "EmployeeMaster1");
            //Dd_ReportingManager.DataSource = ds.Tables["EmployeeMaster1"];
            //Dd_ReportingManager.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }


    #region RoleDD
    private void Fill_Role()
    {
        //throw new NotImplementedException();
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "select Role_Id,Role_Name From Tbl_Role Union select 0 Role_Id,'---select---' Role_Name";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "RoleMaster");
            Dd_Role.DataSource = ds.Tables["RoleMaster"];
            Dd_Role.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    #endregion

    private void Fill_EmployeeDetails()
    {
        if (CallFor == 2)
        {
            EmployeeId_ToUpdate = Employee_Id;
        }
        Boolean IsInstructor = false;
        int  InstructorId;
        try
        {
            //string Password = "";
            string Gender = "";
            con.ConnectionString = str;
            cmd = new SqlCommand();
          //  cmd.CommandText = "select isnull(E.FirstName,'')+' '+isnull(E.MiddleName,'')+' '+isnull(E.LastName,'') Employee_Name,isnull(E.Address_Line1,'')Address_Line1, isnull(E.Address_Line2,'') Address_Line2,isnull(E.Postal_Code,'') Postal_Code, isnull(E.Contact_Mobile,'') Contact_Mobile, isnull(E.Email_Id,'') Email_Id, isnull(E.Role_Id,0) Role_Id ,ISNULL(A.UserName,'') UserName, ISNULL(A.UserPassword,'') UserPassword,E.Employee_Status_Id,isnull(E.Gender,'')Gender,isnull(City_Id,0) City_Id,E.Parent_Id,E.City_Text,isnull(E.FirstName,'') FirstName, isnull(E.MiddleName,'') MiddleName,isnull(E.LastName,'') LastName,  ISnull(I.IsInstructor,0)IsInstructor,I.DBSFilePath,I.InsuranceFilePath,Convert(varchar,I.DBSRenewalDate,103)DBSRenewalDate, convert(varchar,I.InsuranceRenewalDate,103)InsuranceRenewalDate, Isnull(I.InstructorId,0)InstructorId,E.DojoId  from tbl_Employee E  inner join  tbl_Authentication A on e.Employee_Id=A.Reference_Id  and A.LoginCategory_Id=1 left join tbl_Instructors I on e.Employee_Id=I.EmployeeId  where E.Employee_Status_Id=1 and E.Employee_Id=" + EmployeeId_ToUpdate;
            cmd.CommandText = "select isnull(E.FirstName,'')+' '+isnull(E.MiddleName,'')+' '+isnull(E.LastName,'') Employee_Name,isnull(E.Address_Line1,'')Address_Line1, isnull(E.Address_Line2,'') Address_Line2,isnull(E.Postal_Code,'') Postal_Code, isnull(E.Contact_Mobile,'') Contact_Mobile, isnull(E.Email_Id,'') Email_Id, isnull(E.Role_Id,0) Role_Id , ISNULL(A.UserName,'') UserName, ISNULL(A.UserPassword,'') UserPassword,E.Employee_Status_Id,isnull(E.Gender,'')Gender, isnull(City_Id,0) City_Id,E.Parent_Id,E.City_Text,isnull(E.FirstName,'') FirstName, isnull(E.MiddleName,'') MiddleName, isnull(E.LastName,'') LastName,  DBSFilePath,InsuranceFilePath, Convert(varchar,DBSRenewalDate,103)DBSRenewalDate,  convert(varchar,InsuranceRenewalDate,103)InsuranceRenewalDate,  E.DojoId  from tbl_Employee E inner join  tbl_Authentication A on e.Employee_Id=A.Reference_Id and A.LoginCategory_Id=1 where E.Employee_Status_Id=1 and E.Employee_Id=" + EmployeeId_ToUpdate;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //Tb_EmployeeName.Text = dr[0].ToString();
                Tb_AddressLine1.Text = dr[1].ToString();
                Tb_AddressLine2.Text = dr[2].ToString();
                Tb_PostalCode.Text = dr[3].ToString();
                Tb_ContactMobile.Text = dr[4].ToString();
                Tb_EmailID.Text = dr[5].ToString();
                Dd_Role.SelectedValue = dr[6].ToString();
                Tb_UserName.Text = dr[7].ToString();
                DD_EmployeeStatus.SelectedValue = dr[8].ToString();
                Gender = dr[10].ToString();
                Tb_City.Text = dr[13].ToString();
                //DD_City.SelectedValue = dr[11].ToString();
                //Dd_ReportingManager.SelectedValue = dr[12].ToString();
                Tb_FirstName.Text = dr[14].ToString();
                Tb_MiddleName.Text = dr[15].ToString();
                Tb_LastName.Text = dr[16].ToString();

               
              

                Dp_DBSRenewalDate.Text = dr["DBSRenewalDate"].ToString();
                Dp_InsuranceRenewalDate.Text = dr["InsuranceRenewalDate"].ToString();

                DdDojo.SelectedValue = dr["DojoId"].ToString();
            }
            if (Gender == "Female")
            {
                DD_Gender.SelectedIndex = 1;
            }
            else
            {
                DD_Gender.SelectedIndex = 0;
            }

            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
        finally
        {
            dr.Close();
            con.Close();
        }
    }
    private void Fill_DojoID()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "select DojoId,DojoCode from tbl_Dojos union select 0 as DojoId,'---Select---'DojoCode ";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            DdDojo.DataSource = ds.Tables[0];
            DdDojo.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            int Go = 1;
            string EmployeeName = "", AddressLine1 = "", AddressLine2 = "", ContactHome = "";
            string PostalCode = "", ContactMobile = "", Email = "", City_Text = "";
            string UserId = "", Password = "";
            int Role_Id = 0, City_Id = 0, ParentId = 0;
            string FirstName = "", MiddleName = "", LastName = "";
            int InstructorIdToUpdate = 0;
            string ValidationMsg = "", Gender = "";
            int EmployeeStatus_Id = 1, Is_Default = 0;
            int IsInstructor = 0;
            int DojoId = 0;
            string DbsFileName = "";
            string Insurance = "";

            if (Hf_InstructorId.Value != "0")
            {
                InstructorIdToUpdate = Convert.ToInt32(Hf_InstructorId.Value);
            }
            DateTime DbsRenewalDt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            DateTime InsuranceRenewalDt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string DBSFile_Name, DBSFile_Path = "", InsuranceFile_Name, InsuranceFile_Path = "";

            if (Tb_FirstName.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please enter first name')", true);
                Go = 0;
            }
            if (Tb_LastName.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please enter last name')", true);
                Go = 0;
            }
            if (Dd_Role.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please select role')", true);
                Go = 0;
            }

        

            if (Chb_IncludeInOtherCharges.Checked == true)
            {
                IsInstructor = 1;
            }
            else
            {
                IsInstructor = 0;
            }

            if (DdDojo.SelectedIndex >= 0)
            {
                DojoId = Convert.ToInt32(DdDojo.SelectedValue.ToString());
            }

            if (!String.IsNullOrEmpty(Dp_DBSRenewalDate.Text))
            {
                DbsRenewalDt = Convert.ToDateTime(Dp_DBSRenewalDate.Text);
            }

            if (!String.IsNullOrEmpty(Dp_InsuranceRenewalDate.Text))
            {
                InsuranceRenewalDt = Convert.ToDateTime(Dp_InsuranceRenewalDate.Text);
            }
            FirstName = Tb_FirstName.Text.ToString();
            MiddleName = Tb_MiddleName.Text.ToString();
            LastName = Tb_LastName.Text.ToString();
            AddressLine1 = Tb_AddressLine1.Text.ToString();
            AddressLine2 = Tb_AddressLine2.Text.ToString();
            City_Id = 0;// Convert.ToInt32(DD_City.SelectedValue.ToString());
            City_Text = Tb_City.Text.ToString();
            PostalCode = Tb_PostalCode.Text.ToString();
            ContactHome = "";
            ContactMobile = Tb_ContactMobile.Text.ToString();
            Email = Tb_EmailID.Text.ToString();
            Role_Id = Convert.ToInt32(Dd_Role.SelectedValue.ToString());
            //ParentId = 0;
            //ParentId = Convert.ToInt32(Dd_ReportingManager.SelectedValue.ToString());
            Is_Default = 0;
            EmployeeStatus_Id = Convert.ToInt32(DD_EmployeeStatus.SelectedValue.ToString());
            UserId = Tb_UserName.Text.ToString();
            Password = Tb_Password.Text.ToString();
            Gender = DD_Gender.SelectedItem.ToString();

            DBSFile_Name = Path.GetFileName(DBSFile.PostedFile.FileName);
            DBSFile_Path = "StudentProfile/" + DbsFileName;

            InsuranceFile_Name = Path.GetFileName(Fu_Insurance.PostedFile.FileName);
            InsuranceFile_Path = "StudentProfile/" + InsuranceFile_Name;

            Go = Validation();
            if (Go == 1)
            {
                if (EmployeeId_ToUpdate == 0)
                {
                    con.ConnectionString = str;
                    cmd = new SqlCommand();
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.CommandText = "WUSP_Employee_Insert";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        //cmd.Parameters.AddWithValue("@Employee_Name", EmployeeName);
                        cmd.Parameters.AddWithValue("@FirstName", FirstName);
                        cmd.Parameters.AddWithValue("@MiddleName", MiddleName);
                        cmd.Parameters.AddWithValue("@LastName", LastName);
                        cmd.Parameters.AddWithValue("@Address_Line1", AddressLine1);
                        cmd.Parameters.AddWithValue("@Address_Line2", AddressLine2);
                        cmd.Parameters.AddWithValue("@City_Id", City_Id);
                        cmd.Parameters.AddWithValue("@Postal_Code", PostalCode);
                        cmd.Parameters.AddWithValue("@Contact_Home", ContactHome);
                        cmd.Parameters.AddWithValue("@Contact_Mobile", ContactMobile);
                        cmd.Parameters.AddWithValue("@Email_Id", Email);
                        cmd.Parameters.AddWithValue("@Role_Id", Role_Id);
                        cmd.Parameters.AddWithValue("@Parent_Id", ParentId);
                        cmd.Parameters.AddWithValue("@Is_Default", Is_Default);
                        cmd.Parameters.AddWithValue("@Employee_Status_Id", EmployeeStatus_Id);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Created_By", UserName);
                        cmd.Parameters.AddWithValue("@DojoId", DojoId);

                        cmd.Parameters.AddWithValue("@DBSFileName", DbsFileName);
                        cmd.Parameters.AddWithValue("@DBSFilePath", DBSFile_Path);
                        cmd.Parameters.AddWithValue("@InsuranceFileName", InsuranceFile_Name);
                        cmd.Parameters.AddWithValue("@InsuranceFilePath", InsuranceFile_Path);
                        cmd.Parameters.AddWithValue("@DBSRenewalDate", DbsRenewalDt);
                        cmd.Parameters.AddWithValue("@InsuranceRenewalDate", InsuranceRenewalDt);

                        SqlParameter OP_EmployeeId = new SqlParameter();
                        OP_EmployeeId.ParameterName = "@Employee_Id";
                        OP_EmployeeId.DbType = DbType.Int32;
                        OP_EmployeeId.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(OP_EmployeeId);

                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@City_Text", City_Text);

                        cmd.ExecuteNonQuery();

                        string File_Name, File_Path = "";
                        int Employee_Id = Convert.ToInt32(OP_EmployeeId.Value.ToString());

                        File_Name = Path.GetFileName(Fu_EmployeeImgae.PostedFile.FileName);

                     

                        if (File_Name != "")
                        {
                            File_Path = "EmployeeProfile/" + Employee_Id.ToString() + "_" + File_Name;
                            Fu_EmployeeImgae.SaveAs(Server.MapPath("EmployeeProfile/" + Employee_Id.ToString() + "_" + File_Name));
                            cmd.Parameters.Clear();
                            cmd.CommandText = "WUSP_Employee_Profile_Insert";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ProfileImagePath", File_Path);
                            cmd.Parameters.AddWithValue("@Employee_Id", Employee_Id);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            File_Path = "";
                        }

                        //if (Chb_IncludeInOtherCharges.Checked == true)
                        //{
                        //    cmd.Parameters.Clear();
                        //    cmd.CommandText = "WUSP_Instructor_Insert";
                        //    cmd.CommandType = CommandType.StoredProcedure;
                        //    cmd.Parameters.AddWithValue("@IsInstructor", IsInstructor);
                        //    cmd.Parameters.AddWithValue("@EmployeeId", Employee_Id);
                        //    cmd.Parameters.AddWithValue("@DojoId", DojoId);
                        //    cmd.Parameters.AddWithValue("@DBSFileName", DbsFileName);
                        //    cmd.Parameters.AddWithValue("@DBSFilePath", DBSFile_Path);
                        //    cmd.Parameters.AddWithValue("@InsuranceFileName", InsuranceFile_Name);
                        //    cmd.Parameters.AddWithValue("@InsuranceFilePath", InsuranceFile_Path);
                        //    cmd.Parameters.AddWithValue("@DBSRenewalDate", DbsRenewalDt);
                        //    cmd.Parameters.AddWithValue("@InsuranceRenewalDate", InsuranceRenewalDt);
                        //    cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                        //    cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                        //    cmd.ExecuteNonQuery();
                        //}
                        //For image upload end
                        tran.Commit();
                        con.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Employee added successfully');window.location='Form_EmployeeMaster.aspx'", true);
                        Clear();

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
                else //Update Employee start
                {
                    con.ConnectionString = str;
                    cmd = new SqlCommand();
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.CommandText = "WUSP_Employee_Update";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Employee_Id", EmployeeId_ToUpdate);
                        //cmd.Parameters.AddWithValue("@Employee_Name", EmployeeName);
                        cmd.Parameters.AddWithValue("@FirstName", FirstName);
                        cmd.Parameters.AddWithValue("@MiddleName", MiddleName);
                        cmd.Parameters.AddWithValue("@LastName", LastName);
                        cmd.Parameters.AddWithValue("@Address_Line1", AddressLine1);
                        cmd.Parameters.AddWithValue("@Address_Line2", AddressLine2);
                        cmd.Parameters.AddWithValue("@City_Id", City_Id);
                        cmd.Parameters.AddWithValue("@Postal_Code", PostalCode);
                        cmd.Parameters.AddWithValue("@Contact_Home", ContactHome);
                        cmd.Parameters.AddWithValue("@Contact_Mobile", ContactMobile);
                        cmd.Parameters.AddWithValue("@Email_Id", Email);
                        cmd.Parameters.AddWithValue("@Role_Id", Role_Id);
                        cmd.Parameters.AddWithValue("@Parent_Id", ParentId);
                        cmd.Parameters.AddWithValue("@Employee_Status_Id", EmployeeStatus_Id);
                        cmd.Parameters.AddWithValue("@Modified_Date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Modified_By", UserName);
                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@City_Text", City_Text);
                        cmd.Parameters.AddWithValue("@DojoId", DojoId);


                        cmd.ExecuteNonQuery();
                        // con.Close();

                        //Profile image upload for employee
                        string File_Name, File_Path = "";

                        File_Name = Path.GetFileName(Fu_EmployeeImgae.PostedFile.FileName);
                        DBSFile_Name = Path.GetFileName(DBSFile.PostedFile.FileName);
                        InsuranceFile_Name = Path.GetFileName(Fu_Insurance.PostedFile.FileName);

                        // DbsFileName = Path.GetFileName(DBSFile.PostedFile.FileName);
                        DBSFile_Path = "StudentProfile/" + DbsFileName;

                        //  InsuranceFile_Name = Path.GetFileName(Fu_Insurance.PostedFile.FileName);
                        InsuranceFile_Path = "StudentProfile/" + InsuranceFile_Name;
                        //try
                        //{

                        if (File_Name != "")
                        {
                            File_Path = "EmployeeProfile/" + EmployeeId_ToUpdate.ToString() + "_" + File_Name;
                            Fu_EmployeeImgae.SaveAs(Server.MapPath("EmployeeProfile/" + EmployeeId_ToUpdate.ToString() + "_" + File_Name));

                            //con.ConnectionString = str;
                            //cmd = new SqlCommand();
                            cmd.CommandText = "WUSP_Employee_Profile_Insert";
                            // cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            //con.Open();
                            cmd.Parameters.AddWithValue("@ProfileImagePath", File_Path);
                            cmd.Parameters.AddWithValue("@Employee_Id", EmployeeId_ToUpdate);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            File_Path = "";
                        }

                        if (Chb_IncludeInOtherCharges.Checked == true)
                        {
                            if (InstructorIdToUpdate == 0)
                            {

                                cmd.Parameters.Clear();
                                cmd.CommandText = "WUSP_Instructor_Insert";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@IsInstructor", IsInstructor);
                                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId_ToUpdate);
                                cmd.Parameters.AddWithValue("@DojoId", DojoId);
                                cmd.Parameters.AddWithValue("@DBSFileName", DBSFile_Name);
                                cmd.Parameters.AddWithValue("@DBSFilePath", DBSFile_Path);
                                cmd.Parameters.AddWithValue("@InsuranceFileName", InsuranceFile_Name);
                                cmd.Parameters.AddWithValue("@InsuranceFilePath", InsuranceFile_Path);
                                cmd.Parameters.AddWithValue("@DBSRenewalDate", DbsRenewalDt);
                                cmd.Parameters.AddWithValue("@InsuranceRenewalDate", InsuranceRenewalDt);
                                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@CreatedBy", UserName);

                                cmd.ExecuteNonQuery();

                            }
                            else
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandText = "WUSP_Instructor_Update";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@IsInstructor", IsInstructor);
                                cmd.Parameters.AddWithValue("@InstructorId", InstructorIdToUpdate);
                                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId_ToUpdate);
                                cmd.Parameters.AddWithValue("@DojoId", DojoId);
                                cmd.Parameters.AddWithValue("@DBSFileName", DBSFile_Name);
                                cmd.Parameters.AddWithValue("@DBSFilePath", DBSFile_Path);
                                cmd.Parameters.AddWithValue("@InsuranceFileName", InsuranceFile_Name);
                                cmd.Parameters.AddWithValue("@InsuranceFilePath", InsuranceFile_Path);
                                cmd.Parameters.AddWithValue("@DBSRenewalDate", DbsRenewalDt);
                                cmd.Parameters.AddWithValue("@InsuranceRenewalDate", InsuranceRenewalDt);
                                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@CreatedBy", UserName);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();
                        con.Close();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Employee details updated successfully');window.location='Form_EmployeeMaster.aspx'", true);
                        Clear();
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
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }



    private int Validation()
    {
        {
            int Go = 1;
            string ValidationMsg = "";

            if (Tb_FirstName.Text == "")
            {
                Go = 0;
                Tb_FirstName.BorderColor = Color.Red;
                ValidationMsg += "Please Enter First name. /";
            }
            else
            {
                Tb_FirstName.BorderColor = Color.LightGray;
            }
            if (Tb_LastName.Text == "")
            {
                Go = 0;
                Tb_LastName.BorderColor = Color.Red;
                ValidationMsg += "Please Enter Last Name. /";
            }
            else
            {
                Tb_LastName.BorderColor = Color.LightGray;
            }
            if (Tb_EmailID.Text == "")
            {
                Go = 0;
                Tb_EmailID.BorderColor = Color.Red;
                ValidationMsg += "Please Enter Email ID. /";
            }
            else
            {
                Tb_EmailID.BorderColor = Color.LightGray;
            }
            if (Tb_ContactMobile.Text == "")
            {
                Go = 0;
                Tb_ContactMobile.BorderColor = Color.Red;
                ValidationMsg += "Please Enter Contact Mobile. /";
            }
            else
            {
                Tb_ContactMobile.BorderColor = Color.LightGray;
            }



            if (Go == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Validation :" + ValidationMsg.ToString() + "')", true);
            }
            return Go;

        }
    }

    protected void Btn_New_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        Tb_FirstName.Text = "";
        Tb_MiddleName.Text = "";
        Tb_LastName.Text = "";
        Tb_AddressLine1.Text = "";
        Tb_AddressLine2.Text = "";
        //DD_City.SelectedIndex = 0;
        Tb_City.Text = "";
        Tb_PostalCode.Text = "";
        Tb_ContactMobile.Text = "";
        Tb_EmailID.Text = "";
        Dd_Role.SelectedIndex = 0;
        DD_EmployeeStatus.SelectedIndex = 1;
        Tb_UserName.Text = "";
        Tb_Password.Text = "";
    }
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~\Form_EmployeeMaster.aspx");
    }
    protected void Chb_IncludeInOtherCharges_CheckedChanged(object sender, EventArgs e)
    {
        if (Chb_IncludeInOtherCharges.Checked == true)
        {
            enable();
        }
        else
        {
            Disable();
        }

    }
    public void enable()
    {
        if (Chb_IncludeInOtherCharges.Checked == true)
        {
            ////Dp_Next_FollowupDate.Enabled = true;
            Fu_Insurance.Visible = true;
            Lbl_DBS.Visible = true;
            DBSFile.Visible = true;
            Lbl_Insurance.Visible = true;
            Lbl_DBSRenewalDate.Visible = true;
            Lbl_InsuranceRenewalDate.Visible = true;
            //Label12.Visible = true;
            Dp_DBSRenewalDate.Visible = true;
            Dp_InsuranceRenewalDate.Visible = true;
            Fu_Insurance.Visible = true;
            DBSFile.Visible = true;
           // DdDojo.Visible = true;
            Div_DBSRenewalDate.Visible = true;
            Div_InsuranceRenewalDate.Visible = true;
        }
    }

    public void Disable()
    {
        if (Chb_IncludeInOtherCharges.Checked == false)
        {
            Fu_Insurance.Visible = false;
            Lbl_DBS.Visible = false;
            DBSFile.Visible = false;
            Lbl_Insurance.Visible = false;
            Lbl_DBSRenewalDate.Visible = false;
            Lbl_InsuranceRenewalDate.Visible = false;
            //Label12.Visible = false;
            Dp_InsuranceRenewalDate.Visible = false;
            Dp_DBSRenewalDate.Visible = false;
            Fu_Insurance.Visible = false;
            DBSFile.Visible = false;
           // DdDojo.Visible = false;
            Div_DBSRenewalDate.Visible = false;
            Div_InsuranceRenewalDate.Visible = false;
        }
    }
    //protected void Dd_Role_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Dd_Role.SelectedIndex == 4 || Dd_Role.SelectedItem.Text == "Instructor")
    //    {
    //        Chb_IncludeInOtherCharges.Enabled = true;
    //        enable();
    //    }
    //    else
    //    {
    //        Chb_IncludeInOtherCharges.Enabled = false;
    //        Chb_IncludeInOtherCharges.Checked = false;
    //        Disable();
    //    }

    //}
}