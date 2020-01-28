<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_Print_DojoStudent.aspx.cs" Inherits="Form_Print_DojoStudent" %>

<!DOCTYPE html>
<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

        .c-col-size-5 {
            width: 5%;
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
            <div class="content-wrapper">
                <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish  with-border">
                        <asp:Label ID="Lbl_Header" runat="server" CssClass="box-title" Text="Print Dojo Students"></asp:Label>
                    </div>
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Term"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:DropDownList ID="Dd_Term" runat="server" CssClass="form-control select2" DataValueField="TermId"
                                        DataTextField="Term">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label6" CssClass="Label" runat="server" Text="Dojo"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:DropDownList ID="Dd_DojoName" runat="server" CssClass="form-control select2" DataValueField="DojoId"
                                        DataTextField="DojoCode">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-12 col-md-offset-2 c-button-box-2">

                                <div class="col-md-10 no-padding">
                                    <div class="col-md-4 c-btn-widht-25">
                                        <%--<asp:Button ID="Btn_Save" runat="server" Text="Save and Next" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_Save_Click" />--%>
                                    </div>

                                    <div class="col-md-4 c-btn-widht-25">
                                        <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Search_Click" />
                                    </div>

                                    <div class="col-md-4 c-btn-widht-25">
                                        <asp:Button ID="Btn_Print" runat="server" Text="Print" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Print_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div class="table-responsive no-padding c-grid-container c-inline-space">
                                <asp:GridView runat="server" ID="Gv_JobCards" CssClass="table"
                                    CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                    HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                    DataKeyNames="StudentId"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true" PageSize="15" PagerStyle-CssClass="c-paging"
                                    OnPageIndexChanging="Gv_JobCards_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="c-col-size-5">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Label1" Text='<%#Eval("Title") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl2" Text='<%#Eval("StudentName") %>'></asp:Label><br />
                                                <asp:Label runat="server" ID="Label3" Text='<%#Eval("MembershipDate") %>'></asp:Label><br />
                                                <asp:Label runat="server" ID="Label2" Text='<%#Eval("MobileNumber") %>'></asp:Label><br />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="January"></asp:TemplateField>
                                        <asp:TemplateField HeaderText="February"></asp:TemplateField>
                                        <asp:TemplateField HeaderText="March"></asp:TemplateField>
                                        <asp:TemplateField HeaderText="April"></asp:TemplateField>
                                        <asp:TemplateField HeaderText="May"></asp:TemplateField>
                                        <asp:TemplateField HeaderText="June"></asp:TemplateField>
                                        <asp:TemplateField HeaderText="July" Visible="false"></asp:TemplateField>
                                        <asp:TemplateField HeaderText="August" Visible="false"></asp:TemplateField>
                                        <asp:TemplateField HeaderText="September" Visible="false"></asp:TemplateField>
                                        <asp:TemplateField HeaderText="October" Visible="false"></asp:TemplateField>
                                        <asp:TemplateField HeaderText="November" Visible="false"></asp:TemplateField>
                                        <asp:TemplateField HeaderText="December" Visible="false"></asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
