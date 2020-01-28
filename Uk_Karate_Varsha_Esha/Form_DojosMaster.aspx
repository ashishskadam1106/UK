<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_DojosMaster.aspx.cs" Inherits="Form_DojosMaster" %>


<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UK Karate</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />

    <%--<style>

        .c-grid-container {
            width:98%;
            margin-left:1%;
            margin-top:3%;
        }
        .table tbody tr th{
            border-top:1px solid #a5969e;
        }
        .table>tbody>tr>td {
            padding-top:0;
            padding-bottom:0;
            vertical-align:central !important;
        }
        .c-grid-header {
            background-color: #1D5182;
            color: white;
        }
            .c-grid-header:hover {
                background-color: #1D5182;
                color: white;
            }
        .c-grid-button {
            height:28px;
            padding:0;
            margin:1% 1% 1% 1% ; 
        }
        .table > tbody > tr>td:hover {
            background-color:#F4FDFF;
        }
         /*.table > tbody > tr>td:hover {
           background-color: #f5f5f5;
        }*/
         
        .c-grid-alternaterow {
             background-color: #f5f5f5;
        }


        .c-btn-addnew {
            width:15%;
            float:right ;
        }

        @media screen and (max-width:992px) {
            .c-btn-addnew {
                width:23%;
                float:right ;
            }
        }

        @media screen and (max-width:544px) {
            .c-btn-addnew {
                width:40%;
                float:right ;
            }
        }
        @media screen and (max-width:350px) {
            .c-btn-addnew {
                width:60%;
                float:right ;
            }
        }
    </style>--%>
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
                        <h3 class="box-title">Dojos Master</h3>
                    </div>
                    <div class="box-body">
                       <%--gridview start--%>
                        <div class="col-md-12">
                            <div class="c-btn-addnew">
                                <asp:Button runat="server" ID="Btn_AddRole" CssClass="btn bg-purple c-bg-blueish btn-block btn-flat" Text="Add New"
                                    OnClick="Btn_AddRole_Click" />
                            </div>
                        </div>
                        <div class="col-md-12 no-padding">
                            <div class="table-responsive no-padding c-grid-container c-inline-space">
                                <asp:GridView runat="server" ID="Gv_DojoMaster" CssClass="table"
                                    CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                    HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                    DataKeyNames="DojoId"
                                    AllowPaging="true" PageSize="15"
                                    PagerStyle-CssClass="c-paging" 
                                    AutoGenerateColumns="false" OnSelectedIndexChanged="Gv_DojoMaster_SelectedIndexChanged" >
                                    <Columns>
                                         <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_SrNo" Text='<%#Eval("SrNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dojo Code">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_DojoCode" Text='<%#Eval("DojoCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dojo Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_DojoName" Text='<%#Eval("DojoName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dojo Address">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_DojoAddress" Text='<%#Eval("DojoAddress") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="District">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_District" Text='<%#Eval("District") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Country">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Country" Text='<%#Eval("Country") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="Btn_EditRole" runat="server" Text="Edit" CommandName="select"
                                                    CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-grid-button" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>
                        <%--gridview end--%>
                        <%--datatable start--%>
                        <%--<table id="Dt_RoleMaster" class="table table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>Role Name</th>
                                    <th>Remark</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                        </table>--%>
                        <%--datatable end--%>
                    </div>
                </div>
               
            </div>
            <%--content end--%>

            <FT:Footer runat="server" ID="FT_1" />
        </div>
    </form>
</body>
</html>
