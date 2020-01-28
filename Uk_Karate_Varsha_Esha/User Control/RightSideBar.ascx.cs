using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class User_Control_RightSideBar : System.Web.UI.UserControl
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();

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

            if (!IsPostBack)
            {
                DataTable dt = this.GetData("SELECT Project_Id ,Project_Nm FROM Tbl_Project");
                PopulateTreeView(dt, 0, null);
                Society_TreeView.ExpandDepth = 0;
            }
        }
    }

    private void PopulateTreeView(DataTable dtParent, int parentId, TreeNode treeNode)
    {
        foreach (DataRow row in dtParent.Rows)
        {
            TreeNode ProjectNode = new TreeNode
            {
                Text = row["Project_Nm"].ToString(),
                Value = row["Project_Id"].ToString()
            };
            if (parentId == 0)
            {
                Society_TreeView.Nodes.Add(ProjectNode);

                DataTable dtChild = this.GetData("SELECT Building_Id , Building_Nm  FROM Tbl_Building WHERE Project_Id=" + ProjectNode.Value);

                foreach (DataRow BldgRow in dtChild.Rows)
                {
                    TreeNode BldgNode = new TreeNode
                    {
                        Text = BldgRow["Building_Nm"].ToString(),
                        Value = BldgRow["Building_Id"].ToString()
                    };

                    int BuildingId = Convert.ToInt32(BldgRow["Building_Id"].ToString());
                    DataTable DtUnit = this.GetData("select (Case when ISNULL(BT.Booking_Id,0)=0 then U.Unit_ID else BT.Booking_Id end) as ID,(Case when ISNULL(BT.Booking_Id,0)=0 then U.Unit_Name else U.Unit_Name+' - '+A.Applicant_Name end) as Name from tbl_Unit U left outer join tbl_Booking_Tran BT on U.Unit_ID=BT.Unit_Id and BT.Booking_Status_Id=1 left outer join tbl_Applicant A on BT.Applicant_Id=A.Applicant_Id where U.Building_ID=" + BuildingId);
                    if (DtUnit.Rows.Count != 0)
                    {
                        ProjectNode.ChildNodes.Add(BldgNode);
                        foreach (DataRow UnitRow in DtUnit.Rows)
                        {
                            TreeNode UnitNode = new TreeNode
                            {
                                Text = UnitRow["Name"].ToString(),
                                Value = UnitRow["Id"].ToString()
                            };
                            BldgNode.ChildNodes.Add(UnitNode);
                        }
                    }
                    else
                    {
                        ProjectNode.ChildNodes.Add(BldgNode);
                    }

                }

            }
            else
            {
                treeNode.ChildNodes.Add(ProjectNode);
            }


        }
    }



    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    
                }
            }
            return dt;
        }
    }
    //private void PopulateUnit(DataTable dtParent, int parentId, TreeNode child)
    //{
    //    foreach (DataRow row in dtParent.Rows)
    //    {
    //        TreeNode child1 = new TreeNode
    //        {
    //            Text = row["Name"].ToString(),
    //            Value = row["Id"].ToString()
    //        };
    //        if (parentId != 0)
    //        {
    //            Society_TreeView.Nodes.Add(child1);
    //            DataTable dtChild = this.GetData("SELECT Unit_Id as Id, Unit_Name as Name FROM Tbl_Unit WHERE Building_Id=" + child1.Value);
    //            PopulateUnit(dtChild, int.Parse(child1.Value), child1);
    //        }
    //        else
    //        {
    //            child.ChildNodes.Add(child1);
    //        }
    //    }
    //}
}