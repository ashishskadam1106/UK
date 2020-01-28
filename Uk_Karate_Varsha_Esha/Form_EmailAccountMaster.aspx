<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_EmailAccountMaster.aspx.cs" Inherits="Form_EmailAccountMaster" %>

<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UK_Karate</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />
</head>
<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />

            <%--content starts--%>
            <div class="content-wrapper">
                <div class="box  box-solid box-info c-border-blueish c-container-box"  style="margin-bottom:0">
                    <div class="box-header c-box-blueish  with-border" >
                        <h3 class="box-title">Email Account Master</h3>
                    </div>
                    <div class="box-body">
                       <%--gridview start--%>
                        <div class="col-md-12">
                            <div class="c-btn-addnew">
                                <asp:Button runat="server" ID="Btn_Add" CssClass="btn bg-purple c-bg-blueish btn-block btn-flat" Text="Add New"
                                     OnClick="Btn_Add_Click"/>
                            </div>
                        </div>
                        <div class="col-md-12 no-padding">
                            <div class="table-responsive no-padding c-grid-container c-inline-space">
                                <asp:GridView runat="server" ID="Gv_EmailAccountMaster" CssClass="table"
                                    CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                    HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                    DataKeyNames="EmailAccountId"
                                    AllowPaging="true" PageSize="15"
                                    PagerStyle-CssClass="c-paging" OnPageIndexChanging="Gv_EmailAccountMaster_PageIndexChanging"
                                    AutoGenerateColumns="false" OnSelectedIndexChanged="Gv_EmailAccountMaster_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Email Account Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_EmailAccountName" Text='<%#Eval("EmailAccountName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EmailId">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_EmailId" Text='<%#Eval("EmailId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="Btn_Edit" runat="server" Text="Edit" CommandName="select"
                                                    CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-grid-button" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>
                      
                    </div>
                </div>
               
            </div>
            <%--content end--%>

            <FT:Footer runat="server" ID="FT_1" />
        </div>
    </form>
</body>
</html>
