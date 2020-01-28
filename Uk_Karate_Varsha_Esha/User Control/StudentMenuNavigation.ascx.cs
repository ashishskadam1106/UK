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


public partial class User_Control_StudentMenuNavigation : System.Web.UI.UserControl
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    protected void Page_Load(object sender, EventArgs e)
    {
        int Role_Id = 0;

        if (Session["LoginStudentName"] == null || Session["LoginAuthenticated"] == "No")
        {
            Response.Redirect(@"~\Form_StudentLogin.aspx");
        }
        else
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            Role_Id = Convert.ToInt32(Session["LoginStudentUserId"].ToString());
            // Lbl_UserName_InMenu.Text = Session["LoginEmpName"].ToString();
            // Img_UserProfilePic_InMenu.ImageUrl = Session["UserImgPath"].ToString();
            //lblServiceCentreName.Text = Session["ServiceCentre_Name"].ToString();
          
        }
    }

    #region ForMenu
   

    private HtmlGenericControl UList(string id, string cssClass)
    {
        HtmlGenericControl ul = new HtmlGenericControl("ul");
        //ul.ID = id;
        ul.Attributes.Add("class", cssClass);
        return ul;

    }



    private HtmlGenericControl LIList(string innerHtml, string rel, string url, string Icon, string HasChild)
    {
        //string DPclass="dropdown-toggle";

        if (url == "#")
        {
            //url = Request.Url.Authority.ToString() + "/Home.aspx";
            url = "Home.aspx";
        }

        HtmlGenericControl li = new HtmlGenericControl("li");
        li.Attributes.Add("rel", rel);

        //if (innerHtml == "Master" || innerHtml == "Process" || innerHtml == "Admin" || innerHtml == "Report")
        //{
        //    li.Attributes.Add("class", "treeview");
        //    li.InnerHtml = "<a class=" + string.Format("dropdown-toggle") + " data-toggle=" + string.Format("dropdown") + " href=" + string.Format("http://{0}", url) + ">" + innerHtml + "<span class=" + string.Format("caret") + "></span></a>";
        //}
        if (HasChild == "1")
        {
            //li.Attributes.Add("class", "treeview");
            //li.InnerHtml = "<a class=" + string.Format("dropdown-toggle") + " data-toggle=" + string.Format("dropdown") + " href=" + string.Format("http://{0}", url) + ">" + innerHtml + "<span class=" + string.Format("caret") + "></span></a>";

            //li.InnerHtml = "<a class=" + string.Format("dropdown-toggle") + " data-toggle=" + string.Format("dropdown") + " href=" + string.Format(url) + ">" + innerHtml + "<span class=" + string.Format("caret") + "></span></a>";

            li.Attributes.Add("class", "treeview");
            li.InnerHtml = "<a href=" + string.Format("#") + "><i class=\"" + string.Format(Icon) + "\"> </i> <span>" + innerHtml + "</span> <span class=" + string.Format("pull-right-container") + "><i class=\"" + string.Format("fa fa-angle-left pull-right") + "\"></i></span></a>";
        }
        else
        {
            //li.InnerHtml = "<a href=" + string.Format("http://{0}", url) + ">" + innerHtml + "</a>";

            //url = Request.Url.Authority.ToString() + url.ToString();
            //li.InnerHtml = "<a href=http://" + string.Format(url) + ">" + innerHtml + "</a>";

            li.InnerHtml = "<a href=" + string.Format(url) + "><i class=\"" + string.Format(Icon) + "\"></i>" + innerHtml + "</a>";

            //li.InnerHtml = "<a href=~/" + string.Format(url) + "></a>";
        }
        return li;

    }
    #endregion
}