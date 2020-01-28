<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_MaterialMaster.aspx.cs" Inherits="Form_MaterialMaster" %>


<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UK_Karate</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />


    <style>
        .c-label-right {
            float: right;
            margin-right: 5px;
        }

        .c-col-size-15 {
            width: 15%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-16 {
            width: 16%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-23 {
            width: 23%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-20 {
            width: 20%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-40 {
            width: 40%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-btn-widht-37 {
            width: 37%;
        }

        .c-col-size-8 {
            width: 8%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-height {
            margin: 1%;
            height: 28px;
        }

        .c-btn-height {
            margin: 1%;
            height: 28px;
            padding: 0px;
        }

        .c-width-66p {
            width: 65px;
        }

        .c-width-200p {
            width: 200px;
        }

        @media screen and (max-width:992px) {
            .c-col-size-8 {
                width: 8%;
                padding-left: 0px;
                padding-right: 0px;
            }

            .c-col-size-20 {
                width: 100%;
            }

            .c-col-size-40 {
                width: 100%;
            }

            .c-btn-widht-37 {
                width: 70%;
                margin-left: 15%;
                margin-bottom: 2%;
            }

            .c-width-66p {
                width: 100%;
                margin-top: 1px;
            }
        }
    </style>
</head>

<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />

            <%--content starts--%>
            <div class="content-wrapper">
                <div class="box  box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish  with-border">
                        <h3 class="box-title">Material Master</h3>
                    </div>
                    <div class="box-body">
                        <%--gridview start--%>

                        <div class="col-md-12">
                            <div class="c-btn-addnew">
                                <asp:Button runat="server" ID="Btn_AddNew" CssClass="btn bg-purple c-bg-blueish btn-block btn-flat" Text="Add New"
                                    OnClick="Btn_AddNew_Click" />
                            </div>
                        </div>

                        <div class="col-md-12 no-padding">
                            <div class="table-responsive no-padding c-grid-container c-inline-space">
                                <asp:GridView runat="server" ID="Gv_MaterialMaster" CssClass="table"
                                    CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                    HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                    DataKeyNames="MaterialMasterId" AutoGenerateColumns="false"
                                    AllowPaging="true" PageSize="15" PagerStyle-CssClass="c-paging"
                                    OnPageIndexChanging="Gv_MaterialMaster_PageIndexChanging"
                                    OnSelectedIndexChanged="Gv_MaterialMaster_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No." HeaderStyle-CssClass="c-col-size-8">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_SrNo" Text='<%#Eval("SrNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_MaterialName" Text='<%#Eval("MaterialName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Category">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Category" Text='<%#Eval("CategoryName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase Price">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_PurchasePrice" Text='<%#Eval("PurchasePrice") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sell Price">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_SellPrice" Text='<%#Eval("SellPrice") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Instructor Price">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_SellToInstructorPrice" Text='<%#Eval("SellToInstructorPrice") %>'></asp:Label>
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
