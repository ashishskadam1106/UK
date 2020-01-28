<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<%--<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>--%>
<%@ Register Src="~/User Control/DashboardLinks.ascx" TagName="DashboardLinks" TagPrefix="DBRDL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>
<%@ Register Src="~/User Control/RightSideBar.ascx" TagName="RightSideBar" TagPrefix="RSB" %>
<%@ Register Src="~/User Control/Dashboard1.ascx" TagName="Dashboard1" TagPrefix="DBRD1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UK Karate</title>
    <DBRDL:DashboardLinks runat="server" ID="DBRD1" />
    <style>
        .c-btn-width-32 {
            width: 31.5%;
            margin: 2px;
        }

        .padding-left-right-15 {
            padding-left: 15px;
            padding-right: 15px;
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

        .c-col-size-80 {
            width: 80%;
            padding-left: 0px;
            padding-right: 0px;
        }

        @media screen and (max-width:991px) {
            .c-btn-width-32 {
                width: 100%;
                margin: 1px;
            }
        }
    </style>
    <%--   
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>--%>
</head>

<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />

            <%--content starts--%>
            <div class="content-wrapper">
                <DBRD1:Dashboard1 runat="server" ID="DBRD1_1" />

                <div class="row padding-left-right-15" style="padding-top: 15px;">
                    <div class="col-md-6">
                        <div class="box box-success">
                            <div class="box-header with-border">
                                <h3 class="box-title"><i class="fa fa-wrench text-green"></i>&nbsp;Rent Outstanding</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i class="fa fa-minus"></i>
                                    </button>
                                </div>
                            </div>


                            <!-- /.box-header -->
                            <div class="box-body">
                                <%--grid--%>
                                <div class="table-responsive no-padding c-grid-container c-inline-space">
                                    <asp:GridView runat="server" ID="Gv_Rent" CssClass="table"
                                        CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                        HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                        DataKeyNames="DojoId"
                                        AutoGenerateColumns="false" OnRowCommand="Gv_Rent_RowCommand"
                                        AllowPaging="true" PageSize="15" PagerStyle-CssClass="c-paging" ShowFooter="true"
                                        OnPageIndexChanging="Gv_Rent_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No." HeaderStyle-Width="39px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Label1" Text='<%#Eval("SrNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dojo Code" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Lbl2" Text='<%#Eval("DojoCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Due Date" HeaderStyle-Width="88px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Lbl3" Text='<%#Eval("DueDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Lbl2" Text='<%#Eval("Balance") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>
                            <!-- /.box-body -->
                            <div class="box-footer text-center">
                                <a href="Form_RentPayment.aspx" class="uppercase">View All</a>
                            </div>
                            <!-- /.box-footer -->
                        </div>
                    </div>

                </div>


            </div>
            <%--content end--%>

            <FT:Footer runat="server" ID="FT_1" />
            <RSB:RightSideBar runat="server" ID="RSB_1" />
        </div>
    </form>
</body>
</html>
