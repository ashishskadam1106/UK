using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class User_Control_Header : System.Web.UI.UserControl
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    int Employee_Id = 0;

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

            Lbl_UserName_Small.Text = Session["LoginEmpName"].ToString();
            Lbl_UserName_Large.Text = Session["LoginEmpName"].ToString();
            Lbl_EmpMobileNo.Text = Session["LoginEmpMobile"].ToString();
            Lbl_EmpEmailId.Text = Session["LoginEmpEmail"].ToString();
            Img_UserProfilePic_Small.ImageUrl = Session["UserImgPath"].ToString();
            Img_UserProfilePic_Large.ImageUrl = Session["UserImgPath"].ToString();
            //Img_UserProfilePic_Small. = Session["UserImgPath"].ToString();
            

            Employee_Id = Convert.ToInt32(Session["LoginEmployee_Id"].ToString());

            //Fill_Notification();
        }
    }

    //private void Fill_Notification()
    //{
    //    //throw new NotImplementedException();

    //    con.ConnectionString = str;
    //    da = new SqlDataAdapter("select Notification_Id,Remark,NotificationType_Id from tbl_EmployeeNotification where Is_Unread=1 and ToEmployee_Id=" + Employee_Id.ToString() + " order by Notification_Id desc", con);
    //    DataTable dttc = new DataTable();

    //    da.Fill(dttc);
    //    int TotalNotification = dttc.Rows.Count;
    //    Lbl_NotificationHeader.Text = "You have " + TotalNotification.ToString() + " new notifications";
    //    Lbl_ShowNotiNumber.Text = TotalNotification.ToString();

    //    HtmlGenericControl ul = new HtmlGenericControl("ul");
    //    ul.Attributes.Add("class", "menu");
    //    foreach (DataRow row in dttc.Rows)
    //    {
    //        ul.Controls.Add(LIList(row["Notification_Id"].ToString(), row["Remark"].ToString(), row["NotificationType_Id"].ToString()));
    //    }
    //    NotificationPanel.Controls.Add(ul);
    //}

    //private HtmlGenericControl LIList(string Notification_Id, string Remark,string NotificationType_Id )
    //{
        

      
    //    HtmlGenericControl li = new HtmlGenericControl("li");
    //    //li.Attributes.Add("rel", rel);

    //    //li.InnerHtml = "<a href=" + string.Format(url) + "><i class=\"" + string.Format(Icon) + "\"></i>" + innerHtml + "</a>";
    //    if (NotificationType_Id == "1")
    //    {
    //        li.InnerHtml = "<a href=\"Form_Notifications.aspx?ID="+Notification_Id+"\"><i class=\"" + string.Format("fa fa-file-text text-aqua") + "\"></i>" + Remark + "</a>";
    //    }
    //    else if (NotificationType_Id == "2")
    //    {
    //        li.InnerHtml = "<a href=\"Form_Notifications.aspx?ID=" + Notification_Id + "\"><i class=\"" + string.Format("fa fa-file-text text-success") + "\"></i>" + Remark + "</a>";
    //    }

    //    return li;

    //}
    protected void Btn_Profile_Click(object sender, EventArgs e)
    {

    }
}