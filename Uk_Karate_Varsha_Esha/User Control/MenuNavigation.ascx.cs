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


public partial class User_Control_MenuNavigation : System.Web.UI.UserControl
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

        if (Session["LoginUsername"] == null || Session["LoginAuthenticated"] == "No")
        {
            Response.Redirect(@"~\Index.aspx");
        }
        else
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            Role_Id = Convert.ToInt32(Session["LoginRole_Id"].ToString());
           // Lbl_UserName_InMenu.Text = Session["LoginEmpName"].ToString();
           // Img_UserProfilePic_InMenu.ImageUrl = Session["UserImgPath"].ToString();
            //lblServiceCentreName.Text = Session["ServiceCentre_Name"].ToString();
            FillMenu(Role_Id);
        }
    }

    #region ForMenu
    private void FillMenu(int Role_Id)
    {
        //throw new NotImplementedException();
        con.ConnectionString = str;
        da = new SqlDataAdapter("select MM.Menu_Id,MM.Menu_Name,MM.Menu_Url,Icon,HasChild from tbl_Menu_Master MM inner join tbl_Rolewise_Right RR on MM.Menu_Id=RR.Right_Id where MM.Menu_Level=1 and RR.Right_Type_Id=1 and RR.Is_Applicable=1 and RR.Role_Id=" + Role_Id.ToString(), con);
        DataTable dttc = new DataTable();

        da.Fill(dttc);

        //second dont HtmlGenericControl main = UList("Menuid", "nav navbar-nav");
        HtmlGenericControl main = UList("Menuid", "sidebar-menu");

        foreach (DataRow row in dttc.Rows)
        {

            da = new SqlDataAdapter("select MM.Menu_Id,MM.Menu_Name,MM.Menu_Url,Icon,HasChild from tbl_Menu_Master MM inner join tbl_Rolewise_Right RR on MM.Menu_Id=RR.Right_Id where MM.Menu_Level=2 and RR.Right_Type_Id=1 and RR.Is_Applicable=1 and MM.Parent_Id=" + row["Menu_Id"].ToString() + " and RR.Role_Id=" + Role_Id.ToString()+" order by MM.Menu_id", con);

            DataTable dtDist = new DataTable();
            da.Fill(dtDist);

            if (dtDist.Rows.Count > 0)
            {

                HtmlGenericControl sub_menu = LIList(row["Menu_Name"].ToString(), row["Menu_Id"].ToString(), row["Menu_Url"].ToString(),row["Icon"].ToString(),row["HasChild"].ToString());

                HtmlGenericControl ul = new HtmlGenericControl("ul");

                //Second Done  ul.Attributes.Add("class", "dropdown-menu");
                ul.Attributes.Add("class", "treeview-menu");

                foreach (DataRow r in dtDist.Rows)
                {

                    ul.Controls.Add(LIList(r["Menu_Name"].ToString(), r["Menu_Id"].ToString(), r["Menu_Url"].ToString(),r["Icon"].ToString(),r["HasChild"].ToString()));

                }
                sub_menu.Controls.Add(ul);
                main.Controls.Add(sub_menu);

            }

            else
            {

                main.Controls.Add(LIList(row["Menu_Name"].ToString(), row["Menu_Id"].ToString(), row["Menu_Url"].ToString(),row["Icon"].ToString(),row["HasChild"].ToString()));

            }

        }

        Panel1.Controls.Add(main);

    }

    private HtmlGenericControl UList(string id, string cssClass)
    {
        HtmlGenericControl ul = new HtmlGenericControl("ul");
        //ul.ID = id;
        ul.Attributes.Add("class", cssClass);
        return ul;

    }



    private HtmlGenericControl LIList(string innerHtml, string rel, string url,string Icon,string HasChild)
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
        if (HasChild=="1")
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