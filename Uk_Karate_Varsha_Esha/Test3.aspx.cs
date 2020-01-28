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

using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Text;


public partial class Test3 : System.Web.UI.Page
{
    string str = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataSet ds;

    public List<CS_SeatAlignment> List_SeatAlignment { get { return (List<CS_SeatAlignment>)Session["CS_SeatAlignment"]; } set { Session["CS_SeatAlignment"] = value; } }


    protected void Page_Load(object sender, EventArgs e)
    {
        Fill();
        List_SeatAlignment = new List<CS_SeatAlignment>();
    }
    protected void Dd_Coloumn_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    private void Fill()
    {
        int ColumnNumber = 0;
        decimal row = 0;
        if (Dd_Coloumn.SelectedIndex > 0)
        {

            ColumnNumber = Convert.ToInt32(Dd_Coloumn.SelectedItem.Text);
        }


        try
        {
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "USP_Get_Grids";
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            List_SeatAlignment.Clear();
            //if (dr.HasRows)
            //{
            while (dr.Read())
            {

                int IndexToAdd = List_SeatAlignment.Count;
                List_SeatAlignment.Add(new CS_SeatAlignment()
                {
                    Index = IndexToAdd,
                    StudentName = dr["StudentName"].ToString(),
                    MembershipNumber = dr["MembershipNumber"].ToString(),
                    Dojo = dr["Dojo"].ToString(),
                    SeatingOrderNumber = dr["SeatingOrderNumber"].ToString(),
                    UpdatedSeatingOrderNumber = dr["UpdatedSeatingOrderNumber"].ToString(),
                });
            }
            if (ColumnNumber > 0)
            {
                decimal mod = (List_SeatAlignment.Count % Convert.ToDecimal(ColumnNumber));
                if (mod == 0)
                {
                    row = (List_SeatAlignment.Count / Convert.ToDecimal(ColumnNumber)) + 0;
                }
                else
                {
                    row = (List_SeatAlignment.Count % Convert.ToDecimal(ColumnNumber)) + 1;
                }
                //colno = 100 / ColumnNumber;
            }
            for (int r = 0; r < row; r++)
            {
                HtmlGenericControl DynDivRow = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                DynDivRow.ID = "DivRow" + '_' + r;
                // Set your CSS class that handles all of your styles
                DynDivRow.Attributes["class"] = "divStyle";
                DynDivRow.InnerHtml = "Hi";
                MainDiv.Controls.Add(DynDivRow);


                for (int j = 0; j < ColumnNumber; j++)
                {
                    HtmlGenericControl dynDivCol = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");

                    dynDivCol.ID = "DivRowCol" + '_' + r + j;
                    dynDivCol.Attributes["class"] = "divCol";
                  //  dynDivCol.InnerHtml = "sub div";
                    // MainDiv.Controls.Add(dynDivCol);
                    DynDivRow.Controls.Add(dynDivCol);

                    String UnreadText = "";
                    for (int i = 0; i < List_SeatAlignment.Count; i++)
                    {
                        UnreadText += "<tr>";
                        UnreadText += "     <td><div><span>" + List_SeatAlignment[i].StudentName + "</span></div><div><span>" + List_SeatAlignment[i].MembershipNumber + "</span></div><div><span>" + List_SeatAlignment[i].Dojo + "</span></div><div><span>" + List_SeatAlignment[i].SeatingOrderNumber + "</span></div></td>";
                        UnreadText += "</tr>";
                        dynDivCol.InnerHtml = UnreadText;
                        i++;
                    }
                    //for (int i = 0; i < List_SeatAlignment.Count; i++)
                    //{
                    //    HtmlGenericControl dynDivRowCol = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    //    List_SeatAlignment[i].Index = i;
                    //    dynDivRowCol.ID = "Div_Col_Row" + '_' + r + j + i;
                    //    dynDivRowCol.InnerHtml = "";
                    //    dynDivCol.Controls.Add(dynDivRowCol);
                    //}

                }
            }



            for (int i = 0; i < List_SeatAlignment.Count; i++)
            {
                //HtmlGenericControl dynDivCol = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                //List_SeatAlignment[i].Index = i;
                //dynDivCol.ID = "DivRowCol" + '_' + i;
                //dynDivCol.Attributes["class"] = "divCol";
                //dynDivCol.InnerHtml = "Sub Div";
                //MainDiv.Controls.Add(dynDivCol);
            }

        }
        catch (Exception ex)
        {
        }
    }
}