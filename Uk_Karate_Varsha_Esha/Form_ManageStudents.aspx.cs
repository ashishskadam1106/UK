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

using System.Text;
//using System.Net.Http;
using System.Net;
using System.IO;
using CS_Encryption;
using System.Security.Cryptography;

public partial class Form_ManageStudents : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    string UserName = "";
    int Employee_Id = 0, StudentId, CallFor = 0;

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

            // ServiceCentre_Id = Convert.ToInt32(Session["ServiceCentre_Id"].ToString());
            UserName = Session["LoginUsername"].ToString();
            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            if (Request.QueryString.Count > 0)
            {
                try
                {
                    StudentId = Convert.ToInt32(Decrypt(Request.QueryString["ID"].ToString()));
                    CallFor = Convert.ToInt32(Decrypt(Request.QueryString["Call"].ToString()));

                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid Operation');window.location='Home.aspx'", true);
                }
            }


            if (!IsPostBack)
            {
                Fill_StudentDetail(0);
                Fill_Dojo();
                Fill_Grade();

                if (CallFor == 2)
                {
                    BindNote(StudentId);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " $('#Modal_Note').modal({backdrop: 'static',keyboard: false})", true);
                }
            }
        }
    }

    private void Fill_StudentDetail(int CallFrom)
    {
        //int Month = 0, Year = 0;
        DateTime FromDate = Convert.ToDateTime("1900-01-01"), ToDate = Convert.ToDateTime("1900-01-01");
        string BeltIds = "", DojoIds = "";
        Boolean IsDorment = false;

        if (CallFrom == 1) //For given conditional search
        {
            if (!String.IsNullOrEmpty(Tb_FromDate.Text))
            {
                FromDate = Convert.ToDateTime(Tb_FromDate.Text.ToString());
            }

            if (!String.IsNullOrEmpty(Tb_ToDate.Text))
            {
                ToDate = Convert.ToDateTime(Tb_ToDate.Text.ToString());
            }

            //string message = "";
            foreach (ListItem item in Lst_Grades.Items)
            {
                if (item.Selected)
                {
                    // message += item.Text + " " + item.Value + "\\n";
                    if (BeltIds == "")
                    {
                        BeltIds = item.Value.ToString();
                    }
                    else
                    {
                        BeltIds = BeltIds + "," + item.Value.ToString();
                    }
                }
            }
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + message + "');", true);

            foreach (ListItem item in Lst_Dojos.Items)
            {
                if (item.Selected)
                {
                    // message += item.Text + " " + item.Value + "\\n";
                    if (DojoIds == "")
                    {
                        DojoIds = item.Value.ToString();
                    }
                    else
                    {
                        DojoIds = DojoIds + "," + item.Value.ToString();
                    }
                }
            }


        }

        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();  //Query changed 22/5/2017
            //cmd.CommandText = "select case when patindex('%~/dist/img/%',ProfileImage_Path)>0 then Replace(ProfileImage_Path,'~/','') else isnull(ProfileImage_Path,'~/dist/img/avatar5.png') end ProfileImage_Path,isnull(E.FirstName,'')+' '+isnull(E.MiddleName,'')+' '+isnull(E.LastName,'') as 'Employee_Name',E.Address_Line1,E.Address_Line2,isnull(E.City_Text,'') City,E.Contact_Mobile,E.Email_Id,isnull(R.Role_Name,'') Role_Name,Employee_Id from tbl_Employee E left outer join tbl_City C on E.City_Id=C.City_Id left outer join tbl_Role R on E.Role_Id=R.Role_Id where E.Employee_Status_Id=1 and E.Is_Default=0 and E.ServiceCentre_Id=" + ServiceCentre_Id;
            //cmd.CommandText = "Select case when patindex('%~/%',ImageFilePath)>0 then Replace(ImageFilePath,'~/','') else isnull(ImageFilePath,'~/dist/img/avatar5.png') end ProfileImage_Path,isnull(S.FirstName,'')+' '+isnull(S.MiddleName,'')+' '+isnull(S.LastName,'') as 'StudentName',S.MobileNumber,S.EmailId,MembershipNumber,D.DojoCode,B.BeltName,S.MembershipDate from tbl_Students S Left join tbl_Dojos D on S.DojoId=S.DojoId  left join Tbl_Belt B On S.CurrentBeltId=B.BeltId Where S.IsDeleted=0";
            cmd.CommandText = "UPS_GetStudentDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FromDate", FromDate);
            cmd.Parameters.AddWithValue("@ToDate", ToDate);
            cmd.Parameters.AddWithValue("@BeltIds", BeltIds);
            cmd.Parameters.AddWithValue("@DojoIds", DojoIds);
            cmd.Parameters.AddWithValue("@IsDorment", IsDorment);
            cmd.Connection = con;
            dr = cmd.ExecuteReader();

            String UnreadText = "";
            Int32 i = 0;

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    UnreadText += "<tr>";
                    UnreadText += "     <td class=\"c-grid-col-size-50\"><input id=CbSelectStudent_\"" + dr["StudentId"].ToString() + "\" type=\"checkbox\" onclick=CheckUncheck(\"" + dr["StudentId"].ToString() + "\") name=\"CbSelectStudent_" + dr["StudentId"].ToString() + "\" > <input type=\"hidden\" name=\"HfStudentId_" + dr["StudentId"].ToString() + "\" id=\"HfStudentId_" + dr["StudentId"].ToString() + "\" value=\"" + dr["StudentId"].ToString() + "\"></td>";
                 //UnreadText += "     <td class=\"c-grid-col-size-275\"><div><span class=\"c-grid-label-3\">" + dr["FullName"].ToString() + "</span></div> <div class=\"col-md-12 no-padding c-inline-space\"> <div class=\"col-md-2 c-col-size-14 no-padding\"></div><div class=\"col-md-12 no-padding c-inline-space\"> <div class=\"col-md-2 c-col-size-14 no-padding\"><span>[M]</span></div> <div class=\"col-md-10 c-col-size-86 no-padding c-padding-left-1\"> <span>" + dr["MobileNumber"].ToString() + " </span></div></div> <div class=\"col-md-12 no-padding c-inline-space\"> <div class=\"col-md-2 c-col-size-14 no-padding\"><span>[E]</span></div> <div class=\"col-md-10 c-col-size-86 no-padding c-padding-left-1\"> <span>" + dr["EmailId"].ToString() + " </span></div></div> <div class=\"col-md-12 no-padding c-inline-space\"> <div class=\"col-md-2 c-col-size-14 no-padding\"></div></div> </td>";
                    UnreadText += "<td class=\"c-grid-col-size-275\"><div><span><a  class=\"c-grid-label-3\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("2") + "\">" + dr["FullName"].ToString() + "</a></span></div> <div class=\"col-md-12 no-padding c-inline-space\"> <div class=\"col-md-2 c-col-size-14 no-padding\"></div><div class=\"col-md-12 no-padding c-inline-space\"> <div class=\"col-md-2 c-col-size-14 no-padding\"><span>[M]</span></div> <div class=\"col-md-10 c-col-size-86 no-padding c-padding-left-1\"> <span>" + dr["MobileNumber"].ToString() + " </span></div></div> <div class=\"col-md-12 no-padding c-inline-space\"> <div class=\"col-md-2 c-col-size-14 no-padding\"><span>[E]</span></div> <div class=\"col-md-10 c-col-size-86 no-padding c-padding-left-1\"> <span>" + dr["EmailId"].ToString() + " </span></div></div> <div class=\"col-md-12 no-padding c-inline-space\"> <div class=\"col-md-2 c-col-size-14 no-padding\"></div></div> </td>";
                    
                    
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-center\"><span>" + dr["MembershipNumber"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-center\"><span>" + dr["DojoCode"].ToString() + "</span></div></td>";
                    UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-center\"><span>" + dr["BeltName"].ToString() + "</span></div></td>";
                    if (dr["IsVerified"].ToString().ToLower() == "yes")
                    {
                        UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-center\"><span  class=\"c-grid-label-green\">" + dr["IsVerified"].ToString() + "</span></div></td>";
                    }
                    else
                    {
                        UnreadText += "     <td class=\"c-grid-col-size-100\"><div class=\"c-grid-label-center\"><span  class=\"c-grid-label-red\">" + dr["IsVerified"].ToString() + "</span></div></td>";
                    }

                    //    UnreadText += "<td  class=\"c-grid-col-size-100\"><div><a class=\"fa fa-eye inline-icon-size-large\" title=\"View Student Details\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("2") + "\"><span><span></a> <a class=\"fa fa-pencil-square-o inline-icon-size-large\" title=\"Edit Student Details\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("3") + "\"><span><span></a> <a class=\"fa fa-sticky-note-o inline-icon-size-large\" title=\"Notes\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a> </div>  <div><a target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large\" title=\"Print Starter pack front page\" href=\"Reports/Form_PrintStarterpackfrontpage.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a> <a target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large\" title=\"Print Garding booklet\" href=\"Reports/Form_Print_Garding_booklet.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a></div> </td>";
                //    UnreadText += "<td  class=\"c-grid-col-size-100\"><div><a class=\"fa fa-eye inline-icon-size-large\" title=\"View Student Details\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("2") + "\"><span><span></a><a class=\"fa fa-pencil-square-o inline-icon-size-large\" title=\"Edit Student Details\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("3") + "\"><span><span></a> <a class=\"fa fa-sticky-note-o inline-icon-size-large\" title=\"Notes\" href=\"Form_ManageStudents.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("2") + "\"><span><span></a> </div>  <div><a target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large\" title=\"Print Starter pack front page\" href=\"Reports/Form_PrintStarterpackfrontpage.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a> <a target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large\" title=\"Print Garding booklet\" href=\"Reports/Form_Print_Garding_booklet.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a></div> </td>";

                    UnreadText += "<td  class=\"c-grid-col-size-100\"><div><a class=\"fa fa-eye inline-icon-size-large\" title=\"View Student Details\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("2") + "\"><span><span></a><a class=\"fa fa-pencil-square-o inline-icon-size-large\" title=\"Edit Student Details\" href=\"Form_StudentRegistration.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("3") + "\"><span><span></a> <a class=\"fa fa-sticky-note-o inline-icon-size-large\" title=\"Notes\" href=\"Form_ManageStudents.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "&Call=" + encrypt("2") + "\"><span><span></a> </div>  <div><a target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large\" title=\"Print Starter pack front page\"  onclick=\"return confirm('Do you want  to print Starter Pack?')\" href=\"Reports/Form_PrintStarterpackfrontpage.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a> <a target=\"_blank\" rel=\"noopener noreferrer\" class=\"fa fa-print inline-icon-size-large\" title=\"Print Garding booklet\" href=\"Reports/Form_Print_Garding_booklet.aspx?ID=" + encrypt(dr["StudentId"].ToString()) + "\"><span><span></a></div> </td>";



                    UnreadText += "</tr>";
                    UnreadText += "</tr>";
                    tlist.InnerHtml = UnreadText;
                    i++;
                }
            }
            else
            {
                UnreadText += "<tr>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "    <td></td>";
                UnreadText += "</tr>";


                UnreadText += "</tr>";
                tlist.InnerHtml = UnreadText;


            }

            //dr.NextResult();
            //while (dr.Read())
            //{
            //    Tb_TotalStudents.Text = dr["Count"].ToString();
            //}

            /*
            dr.NextResult();
            String TotalStudentResult = "";
            while (dr.Read())
            {
                TotalStudentResult += "<tr>";


                TotalStudentResult += "     <td class=\"c-grid-col-size-50 c-grid-row\"><div class=\"c-grid-label-left\"><span>" + dr["Description"].ToString() + "</span></div></td>";
                TotalStudentResult += "     <td class=\"c-grid-col-size-50 c-grid-row\"><div class=\"c-grid-label-left\"><span>" + dr["Count"].ToString() + "</span></div></td>";

                TotalStudentResult += "</tr>";


                TotalStudentResult += "</tr>";
                TbodyTotalStudent.InnerHtml = TotalStudentResult;
                i++;

                Hf_TotalStudents.Value = dr["Count"].ToString();
            }

            dr.NextResult();
            String GradeResult = "";
            while (dr.Read())
            {
                GradeResult += "<tr>";


                GradeResult += "     <td class=\"c-grid-col-size-50 c-grid-row\"><div class=\"c-grid-label-left \"><span>" + dr["Description"].ToString() + "</span></div></td>";
                GradeResult += "     <td class=\"c-grid-col-size-50 c-grid-row\"><div class=\"c-grid-label-left \"><span>" + dr["Count"].ToString() + "</span></div></td>";

                GradeResult += "</tr>";


                GradeResult += "</tr>";
                TbodyGradeResult.InnerHtml = GradeResult;
                i++;
            }

            dr.NextResult();
            String DojoResult = "";
            while (dr.Read())
            {
                DojoResult += "<tr>";


                DojoResult += "     <td class=\"c-grid-col-size-50 c-grid-row\"><div class=\"c-grid-label-left \"><span>" + dr["Description"].ToString() + "</span></div></td>";
                DojoResult += "     <td class=\"c-grid-col-size-50 c-grid-row\"><div class=\"c-grid-label-left \"><span>" + dr["Count"].ToString() + "</span></div></td>";

                DojoResult += "</tr>";


                DojoResult += "</tr>";
                TbodyDojoResult.InnerHtml = DojoResult;
                i++;
            }
            */
            dr.Close();
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
    protected void Btn_DeleteStudent_Click(object sender, EventArgs e)
    {

        String StudentIds = Hf_SelectedStudentIds.Value.ToString();
        if (!String.IsNullOrEmpty(StudentIds))
        {
            String[] SplitStudentIds = StudentIds.Split(',');
            for (int i = 0; i < SplitStudentIds.Length; i++)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + SplitStudentIds[i].ToString() + "')", true);
                int StudentId = Convert.ToInt32(SplitStudentIds[i].ToString());

                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;

                try
                {
                    cmd.CommandText = "USP_StudentDelete";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StudentId", StudentId);
                    cmd.Parameters.AddWithValue("@DeletedDate", CurrentUtc_IND);
                    cmd.Parameters.AddWithValue("@DeletedBy", UserName);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                }
                finally
                {
                    tran.Commit();
                    con.Close();
                }
            }
            Hf_SelectedStudentIds.Value = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Selected students moved successfully');window.location='Form_ManageStudents.aspx';", true);
            //Fill_StudentDetail(0);
        }


    }
    protected void Btn_MoveToDorement_Click(object sender, EventArgs e)
    {
        //Following method works correct by for pagination it dosen't work as we do not get the request.form elements of another page

        /*
        int TotalStudents = 0;
        if (!String.IsNullOrEmpty(Hf_TotalStudents.Value.ToString()))
        {
            TotalStudents = Convert.ToInt32(Hf_TotalStudents.Value.ToString());
        }
        for (int i = 1; i <= TotalStudents; i++)
        {
            //CheckBox CbSelectStudent = (CheckBox)Page.FindControl("CbSelectStudent_"+i.ToString());

            String Selected = Request.Form["CbSelectStudent_" + i.ToString()];
            String StudentId = Request.Form["HfStudentId_" + i.ToString()];
            if (!String.IsNullOrEmpty(Selected))
            {
                if (Selected.ToLower() == "on")
                {
                    con.ConnectionString = str;
                    cmd = new SqlCommand();
                    con.Open();

                    SqlTransaction tran = con.BeginTransaction();
                    cmd.Transaction = tran;

                    try
                    {
                        //cmd.CommandText = "USP_StudentISDormentStudent";
                        cmd.CommandText = "USP_StudentMoveToDorment";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@StudentId", Convert.ToInt32(StudentId));
                        cmd.Parameters.AddWithValue("@DormantDate", CurrentUtc_IND);
                        cmd.Parameters.AddWithValue("@DormantBy", UserName);

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        con.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
                    }
                    finally
                    {
                        tran.Commit();
                        con.Close();
                    }
                }
            }
        }
         * */

        //Method 2 we have check uncheck method which add or remove respective student id in hidden field based on check or uncheck

        String StudentIds = Hf_SelectedStudentIds.Value.ToString();
        if (!String.IsNullOrEmpty(StudentIds))
        {
            String[] SplitStudentIds = StudentIds.Split(',');
            for (int i = 0; i < SplitStudentIds.Length; i++)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + SplitStudentIds[i].ToString() + "')", true);
                int StudentId = Convert.ToInt32(SplitStudentIds[i].ToString());

                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;

                try
                {
                    //cmd.CommandText = "USP_StudentISDormentStudent";
                    cmd.CommandText = "USP_StudentMoveToDorment";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StudentId", Convert.ToInt32(StudentId));
                    cmd.Parameters.AddWithValue("@DormantDate", CurrentUtc_IND);
                    cmd.Parameters.AddWithValue("@DormantBy", UserName);

                    cmd.ExecuteNonQuery();
                    tran.Commit();
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
            Hf_SelectedStudentIds.Value = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Selected students moved successfully');window.location='Form_ManageStudents.aspx';", true);
            //Fill_StudentDetail(0);
        }

    }

    private void Fill_Grade()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "Select BeltId,(BeltName+''+ISNULL(Level,''))BeltName From tbl_belt ";//union Select 0 as BeltId,'---Select---'BeltName";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Grade");
            Lst_Grades.DataSource = ds.Tables["Grade"];
            Lst_Grades.DataBind();
            Lst_Grades.DataValueField = "BeltId";
            Lst_Grades.DataTextField = "BeltName";

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }

    private void Fill_Dojo()
    {
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            //cmd.CommandText = "Select DojoId,DojoCode from tbl_Dojos  union select 0 DojoId,'---Select---' DojoCode";
            cmd.CommandText = "Select DojoId,DojoCode from tbl_Dojos where IsDeleted=0  ";//union select 0 DojoId,'---Select---' DojoCode";
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "Dojo");
            Lst_Dojos.DataSource = ds.Tables["Dojo"];
            Lst_Dojos.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
    }
    protected void Dd_Grade_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string BeltId=Dd_Grade.SelectedValue;
        //string[] values = null;
        //for (int i = 0; i < values.Length; i++)
        //{
        //    values[i] = values[i].Trim();
        //}

        //string courses = string.Empty;
        //foreach (ListItem item in Dd_Grade.Items)
        //{
        //    if (item.Selected)
        //    {
        //        courses += item.Text + ",";
        //    }
        //}
        //Response.Write(courses.Remove(courses.Length - 1));

    }
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        Fill_StudentDetail(1);
    }


    public string Decrypt(string cipherText)
    {
        try
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
        }
        catch
        {

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

    protected void Btn_Print_Click(object sender, EventArgs e)
    {
        DateTime FromDate = Convert.ToDateTime("1900-01-01"), ToDate = Convert.ToDateTime("1900-01-01");
        string BeltIds = "", DojoIds = "";
        Boolean IsDorment = false;

        if (!String.IsNullOrEmpty(Tb_FromDate.Text))
        {
            FromDate = Convert.ToDateTime(Tb_FromDate.Text.ToString());
        }

        if (!String.IsNullOrEmpty(Tb_ToDate.Text))
        {
            ToDate = Convert.ToDateTime(Tb_ToDate.Text.ToString());
        }

        //string message = "";
        foreach (ListItem item in Lst_Grades.Items)
        {
            if (item.Selected)
            {
                // message += item.Text + " " + item.Value + "\\n";
                if (BeltIds == "")
                {
                    BeltIds = item.Value.ToString();
                }
                else
                {
                    BeltIds = BeltIds + "," + item.Value.ToString();
                }
            }
        }

        foreach (ListItem item in Lst_Dojos.Items)
        {
            if (item.Selected)
            {
                // message += item.Text + " " + item.Value + "\\n";
                if (DojoIds == "")
                {
                    DojoIds = item.Value.ToString();
                }
                else
                {
                    DojoIds = DojoIds + "," + item.Value.ToString();
                }
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Openwindow", "window.open('Reports/Form_PrintManageStudentList.aspx?From=" + encrypt(FromDate.ToString()) + "&To=" + encrypt(ToDate.ToString()) + "&BeltIds=" + encrypt(BeltIds.ToString()) + "&DojoIds=" + encrypt(DojoIds.ToString()) + "','_newtab');", true);

    }

    private void BindNote(int StudentId)
    {
        int ManageStudentNoteId = 0;
        con.ConnectionString = str;
        cmd = new SqlCommand();
        cmd.CommandText = "Select ManageStudentNoteId,StudentId,Note From tbl_ManageStudentNote Where StudentId=" + StudentId;
        cmd.Connection = con;
        con.Open();
        dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            Tb_Note.Text = dr["Note"].ToString();
            ManageStudentNoteId = Convert.ToInt32(dr["ManageStudentNoteId"].ToString());
            Hf_ManageStudentNoteId.Value = ManageStudentNoteId.ToString();
        }
        dr.Close();
        con.Close();

    }
    protected void btn_SaveNote_Click(object sender, EventArgs e)
    {
        int StudId = 0, NoteId = 0;
        StudId = Convert.ToInt32(StudentId);

        NoteId = Convert.ToInt32(Hf_ManageStudentNoteId.Value);


        string Note = "";
        if (!String.IsNullOrEmpty(Tb_Note.Text))
        {
            Note = Tb_Note.Text;

            if (NoteId == 0)
            {

                con.ConnectionString = str;
                cmd = new SqlCommand();
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;

                try
                {
                    cmd.CommandText = "USP_StudentNoteInsert";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StudentId", StudId);
                    cmd.Parameters.AddWithValue("@Note", Note);
                
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    con.Close();
                    cmd.Parameters.Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Note Save successfully.')", true);

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
                    cmd.CommandText = "USP_StudentNoteUpdate";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ManageStudentNoteId", NoteId);
                    cmd.Parameters.AddWithValue("@Note", Note);
              
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    con.Close();
                    cmd.Parameters.Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Note Updated successfully.')", true);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Enter Note')", true);
        }
    }
}