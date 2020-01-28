<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<!DOCTYPE html>

<html lang="en">
<head>


    <DL:DefaultLinks runat="server" ID="DL_1" />
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Sortable - Display as grid</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <%--<link rel="stylesheet" href="/resources/demos/style.css">--%>
    <style>
        #sortable {
            list-style-type: none;
            margin: 0;
            padding: 0;
            width: 1200px;
        }

            #sortable li {
                margin: 3px 3px 3px 0;
                padding: 1px;
                float: left;
                width: 200px;
                height: 100px;
                font-size: 0.90em;
                text-align: left;
                text-decoration-style:solid;
            }
    </style>

    <script>
        $(function () {
            $("#sortable").sortable();
            $("#sortable").disableSelection();
        });
    </script>
</head>
<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />
            <div class="content-wrapper">
                <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">

                    <div class="box-header c-box-blueish  with-border">
                        <asp:Label ID="Lbl_Header" runat="server" CssClass="box-title" Text="Seat Alignment"></asp:Label>
                    </div>

                    <%--box body start--%>
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Column "></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:DropDownList ID="Dd_Coloumn" runat="server" CssClass="form-control select2" DataValueField="ColoumnId"
                                        DataTextField="ColoumnName">
                                        <asp:ListItem Value="0">2</asp:ListItem>
                                        <asp:ListItem Value="1">3</asp:ListItem>
                                        <asp:ListItem Value="2">4</asp:ListItem>
                                        <asp:ListItem Value="3">5</asp:ListItem>

                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <ul id="sortable">
                                <li class="ui-state-default">
                                    <div>
                                        <asp:Label ID="lbl_1" runat="server" Text="Student Name : Siham Mohamud"></asp:Label><br />
                                        <asp:Label ID="Label2" runat="server" Text="Membership number : 5202"></asp:Label><br />
                                        <asp:Label ID="Label3" runat="server" Text="Dojo Code :	B"></asp:Label><br />
                                        <asp:Label ID="Label4" runat="server" Text="Seating Number : 1001"></asp:Label><br />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                    <div>
                                        <asp:Label ID="Label6" runat="server" Text="Student Name : Gurveer Singh"></asp:Label><br />
                                        <asp:Label ID="Label7" runat="server" Text="Membership number : 5200"></asp:Label><br />
                                        <asp:Label ID="Label8" runat="server" Text="Dojo Code :	H"></asp:Label><br />
                                        <asp:Label ID="Label9" runat="server" Text="Seating Number : 1002"></asp:Label><br />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                     <div>
                                        <asp:Label ID="Label10" runat="server" Text="Student Name : Benjamin Tejada"></asp:Label><br />
                                        <asp:Label ID="Label11" runat="server" Text="Membership number : 5199"></asp:Label><br />
                                        <asp:Label ID="Label12" runat="server" Text="Dojo Code : B"></asp:Label><br />
                                        <asp:Label ID="Label13" runat="server" Text="Seating Number : 1003"></asp:Label><br />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                    <div>
                                        <asp:Label ID="Label14" runat="server" Text="Student Name : Aarush Singari"></asp:Label><br />
                                        <asp:Label ID="Label15" runat="server" Text="Membership number : 5198"></asp:Label><br />
                                        <asp:Label ID="Label16" runat="server" Text="Dojo Code : H"></asp:Label><br />
                                        <asp:Label ID="Label17" runat="server" Text="Seating Number : 1004"></asp:Label><br />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                    <div>
                                        <asp:Label ID="Label18" runat="server" Text="Student Name : Muhammad Umar"></asp:Label><br />
                                        <asp:Label ID="Label19" runat="server" Text="Membership number : 5197"></asp:Label><br />
                                        <asp:Label ID="Label20" runat="server" Text="Dojo Code : H"></asp:Label><br />
                                        <asp:Label ID="Label21" runat="server" Text="Seating Number : 1005"></asp:Label><br />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                      <div>
                                        <asp:Label ID="Label22" runat="server" Text="Student Name : kirpa Bawa"></asp:Label><br />
                                        <asp:Label ID="Label23" runat="server" Text="Membership number : 5196"></asp:Label><br />
                                        <asp:Label ID="Label24" runat="server" Text="Dojo Code : H"></asp:Label><br />
                                        <asp:Label ID="Label25" runat="server" Text="Seating Number : 1006"></asp:Label><br />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                     <div>
                                        <asp:Label ID="Label1" runat="server" Text="Student Name : Deon Shijo"></asp:Label><br />
                                        <asp:Label ID="Label26" runat="server" Text="Membership number : 5195"></asp:Label><br />
                                        <asp:Label ID="Label27" runat="server" Text="Dojo Code : H"></asp:Label><br />
                                        <asp:Label ID="Label28" runat="server" Text="Seating Number : 1007"></asp:Label><br />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                      <div>
                                        <asp:Label ID="Label29" runat="server" Text="Student Name : Wyndell Coutinho"></asp:Label><br />
                                        <asp:Label ID="Label30" runat="server" Text="Membership number : 5194"></asp:Label><br />
                                        <asp:Label ID="Label31" runat="server" Text="Dojo Code : H"></asp:Label><br />
                                        <asp:Label ID="Label32" runat="server" Text="Seating Number : 1008"></asp:Label><br />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                      <div>
                                        <asp:Label ID="Label33" runat="server" Text="Student Name : Krishna sai Sriniva"></asp:Label><br />
                                        <asp:Label ID="Label34" runat="server" Text="Membership number : 5193"></asp:Label><br />
                                        <asp:Label ID="Label35" runat="server" Text="Dojo Code : H"></asp:Label><br />
                                        <asp:Label ID="Label36" runat="server" Text="Seating Number : 1009"></asp:Label><br />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                     <div>
                                        <asp:Label ID="Label37" runat="server" Text="Student Name : Danvir Singh"></asp:Label><br />
                                        <asp:Label ID="Label38" runat="server" Text="Membership number : 5192"></asp:Label><br />
                                        <asp:Label ID="Label39" runat="server" Text="Dojo Code : H"></asp:Label><br />
                                        <asp:Label ID="Label40" runat="server" Text="Seating Number : 1010"></asp:Label><br />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                     <div>
                                        <asp:Label ID="Label41" runat="server" Text="Student Name : Suleiman aitaf"></asp:Label><br />
                                        <asp:Label ID="Label42" runat="server" Text="Membership number : 5191"></asp:Label><br />
                                        <asp:Label ID="Label43" runat="server" Text="Dojo Code : H"></asp:Label><br />
                                        <asp:Label ID="Label44" runat="server" Text="Seating Number : 1011"></asp:Label><br />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                       <div>
                                        <asp:Label ID="Label45" runat="server" Text="Student Name : Jakub Celinkski"></asp:Label><br />
                                        <asp:Label ID="Label46" runat="server" Text="Membership number : 5190"></asp:Label><br />
                                        <asp:Label ID="Label47" runat="server" Text="Dojo Code : H"></asp:Label><br />
                                        <asp:Label ID="Label48" runat="server" Text="Seating Number : 1012"></asp:Label><br />
                                    </div>
                                </li>
                            </ul>
                        </div>

                        <div class="col-md-12 c-inline-space col-md-offset-2 c-button-box-2">

                            <div class="col-md-10 no-padding">
                                <div class="col-md-6 c-btn-widht-25">
                                    <asp:Button ID="Btn_Submit" runat="server" Text="Submit" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_Submit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <FT:Footer runat="server" ID="FT_1" />
        </div>
    </form>
</body>
</html>
