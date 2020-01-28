<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test3.aspx.cs" Inherits="Test3" %>

<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <DL:DefaultLinks runat="server" ID="DL_1" />

        <style type='text/css'>
        .divStyle {
            background-color: gray;
            width: 100%;
            height: 100px;
            float: left;
             padding: 1px;
        }

            .divCol {
                background-color: ghostwhite;
                margin: 3px 3px 3px 0;
                padding: 1px;
                float: left;
                width: 194px;
                height: 100px;
                font-size: 0.90em;
                text-align: left;
                text-decoration-style:solid;
            }
    </style>
</head>
<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />
            <div class="content-wrapper">
                <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">

                    <div class="box-header c-box-blueish  with-border">
                        <asp:Label ID="Lbl_Header" runat="server" CssClass="box-title" Text="UI Sortable - Display as grid"></asp:Label>
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
                                        DataTextField="ColoumnName" AutoPostBack="true" OnSelectedIndexChanged="Dd_Coloumn_SelectedIndexChanged">
                                        <asp:ListItem Value="0">---Select---</asp:ListItem>
                                         <asp:ListItem Value="1">2</asp:ListItem>
                                        <asp:ListItem Value="2">3</asp:ListItem>
                                        <asp:ListItem Value="3">4</asp:ListItem>
                                        <asp:ListItem Value="4">5</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div id="MainDiv" runat="server" >

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
