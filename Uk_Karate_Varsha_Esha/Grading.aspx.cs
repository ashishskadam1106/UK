using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

[System.Web.Script.Services.ScriptService]
public partial class Grading : System.Web.UI.Page
{

    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;
    //string eventID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //Fill_LiveGrid();
            //Fill_EventLabel();
        }
    }

    private void Fill_EventLabel()
    {
        String EventLabel = "";


        con.ConnectionString = str;
        con.Open();
        cmd = new SqlCommand();
        cmd.CommandText = "Select EventHeaderId,(EventLabel+' : '+'Pay Grading Fee for Kyu : '+B.Level+'and Start Date : '+Convert(varchar,EventDate,106)) as Label from tbl_EventHeader E inner join Tbl_Belt B on E.EventKyuId=B.BeltId   Where IsDeleted=0  and E.EventHeaderId=" + 1;
        // cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            EventLabel = dr["Label"].ToString();
        }
        if (!String.IsNullOrEmpty(EventLabel))
        {
        }
        dr.Close();
        con.Close();
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

    private string Fill_LiveGrid(string eID)
    {
        //List_EventDetail.Clear();
        int IndexToAdd = 0;
        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_Get_LiveEventDetail";
            cmd.CommandType = CommandType.StoredProcedure;

            if (!string.IsNullOrEmpty(eID))
            {
                int EventHeaderId = Convert.ToInt32(Decrypt(eID));
                cmd.Parameters.AddWithValue("@EventHeaderId", EventHeaderId); // Decrypt(Request.QueryString["EventHeaderId"]); 
            }

            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            var list = new List<CS_EventDetail>();
            while (dr.Read())
            {
                try
                {
                    list.Add(new CS_EventDetail()
                                    {
                                        Index = IndexToAdd,
                                        EventHeaderId = Convert.ToInt32(dr["EventHeaderId"].ToString()),
                                        StudentId = Convert.ToInt32(dr["StudentId"].ToString()),
                                        FullName = dr["FullName"].ToString(),
                                        Fees = Convert.ToDouble(dr["Fees"].ToString()),
                                        FeesPaid = Convert.ToDouble(dr["FeesPaid"].ToString()),
                                        MembershipFee = dr["MembershipFee"].ToString(),
                                        DojoName = dr["DojoName"].ToString(),
                                        Grade = dr["Grade"].ToString(),
                                        EventKyuId = Convert.ToInt32(dr["EventKyuId"].ToString()),
                                        GradingFeeStatus = dr["GradingFeeStatus"].ToString(),
                                        BeltId = Convert.ToInt32(dr["BeltId"])
                                    });
                }
                catch (Exception)
                {
                     
                }

            }

            dr.NextResult();
            var gradelist = new List<Grade>();
            while (dr.Read())
            {
                gradelist.Add(new Grade()
                {
                    id = dr["StudentId"].ToString(),
                    belt = (string.IsNullOrEmpty(Convert.ToString(dr["CurrentBeltId"])) ? dr["InitialBeltId"].ToString() :
                    dr["CurrentBeltId"].ToString()),
                    propvisionalFlag = dr["isProvission"].ToString(),
                    initialBelt = dr["InitialBeltId"].ToString(),
                    FullName = dr["FullName"].ToString(),
                    row = dr["row_no"].ToString()
                });
            }
            dr.Close();
            con.Close();

            var obj = new
            {
                data = list,
                grade = gradelist,
                Permission = Session["LoginRole_Id"] != null ? Convert.ToInt32(Session["LoginRole_Id"]) : 2  //0 - both, 1 - admin, 2 - trainer
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured :" + ex.Message.ToString() + "')", true);
        }
        return "";
    }
    [WebMethod]
    public static string GetData(string eventID)
    {
        Grading h = new Grading();
        return h.Fill_LiveGrid(eventID);
    }

    [WebMethod]

    public static string SaveGradeData(List<Grade> data)//Dictionary<string,string> data
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Id", typeof(int)),
                    new DataColumn("belt", typeof(int)),
                    new DataColumn("propvisionalFlag",typeof(bool)),
                    new DataColumn("row",typeof(int)),
                    new DataColumn("initialBelt",typeof(int))
            });

        foreach (Grade grade in data)
        {
            int id = int.Parse(grade.id);
            int belt = int.Parse(grade.belt);
            bool propvisionalFlag = bool.Parse(grade.propvisionalFlag);
            int row = int.Parse(grade.row);
            int initialBelt = int.Parse(grade.initialBelt);
            dt.Rows.Add(id, belt, propvisionalFlag, row, initialBelt);
        }

        if (dt.Rows.Count > 0)
        {
            string consString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertGradingData"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@GradingData", dt);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        return Newtonsoft.Json.JsonConvert.SerializeObject(data);
    }

    [WebMethod]
    public static string FinishGrading()
    {
        string consString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlConnection conn;
        SqlCommand comm;
        conn = new SqlConnection(consString);
        conn.Open();
        comm = new SqlCommand("update tbl_grade  set isgradefinish=1 ,Updated_date = getdate()  where [event_id] =  1", conn);
        try
        {
            comm.ExecuteNonQuery();
            return ("Updated..");
        }
        catch (Exception ex)
        {
            return (ex.Message);
        }
        finally
        {
            conn.Close();
        }
    }

    [WebMethod]
    public static string SaveGrading(Notes data)
    {
        string consString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(consString))
        {
            using (SqlCommand cmd = new SqlCommand("USP_InsertUpdateNotes"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@studentId", data.studentId);
                cmd.Parameters.AddWithValue("@eventID", 1);//data.eventID
                cmd.Parameters.AddWithValue("@note", data.note);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        return "Save successfully !";
    }

    [WebMethod]
    public static string GetGrading(string data)
    {
        string consString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlConnection conn;
        SqlCommand comm;
        SqlDataReader dreader;
        conn = new SqlConnection(consString);
        conn.Open();
        comm = new SqlCommand("select top 1 note from tbl_gradeNote where studentId = " + data + " ", conn);
        try
        {
            dreader = comm.ExecuteReader();
            if (dreader.Read())
            {
                return Convert.ToString(dreader[0]);
            }

            dreader.Close();
        }
        catch (Exception)
        {

        }
        finally
        {
            conn.Close();
        }
        return "";
    }

    public class Notes
    {
        public int studentId { get; set; }
        public string note { get; set; }
        public int eventID { get; set; }
    }

    public class Grade
    {
        public string belt { get; set; }
        public string id { get; set; }
        public string propvisionalFlag { get; set; }
        public string row { get; set; }
        public string initialBelt { get; set; }
        public string FullName { get; set; }
    }
}