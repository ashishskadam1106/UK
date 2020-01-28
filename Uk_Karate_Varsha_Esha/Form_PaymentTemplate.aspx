<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_PaymentTemplate.aspx.cs" Inherits="Form_PaymentTemplate" %>


<!DOCTYPE html>


<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

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

    <script type="text/javascript">
        function Savealert() {
            swal({
                type: 'success',
                title: 'Class Added Successfully ',
                allowOutsideClick: false,

            }).then(function (name) {
                window.location = 'Form_ClassMaster.aspx';
            })
        }
        function Updatealert() {
            swal({
                type: 'success',
                title: 'Class details updated Successfully ',
                allowOutsideClick: false,

            }).then(function (name) {
                window.location = 'Form_ClassMaster.aspx';
            })
        }

    </script>

</head>
<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />
            <%--content starts--%>
            <div class="content-wrapper">
                <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish-new with-border">
                        <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Payment Template"></asp:Label>
                    </div>
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12">
                            <div class="c-btn-addnew">
                                <asp:Button runat="server" ID="Btn_AddNew" CssClass="btn bg-purple c-bg-blueish btn-block btn-flat" Text="Add New"
                                    OnClick="Btn_AddNew_Click" />
                            </div>
                        </div>

                        <div class="col-md-12 no-padding">
                            <div class="table-responsive no-padding c-grid-container c-inline-space">
                                <asp:GridView runat="server" ID="Gv_PaymentTemplate" CssClass="table"
                                    CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                    HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                    DataKeyNames="PaymentTemplateId" AutoGenerateColumns="false"
                                    AllowPaging="true" PageSize="15" PagerStyle-CssClass="c-paging"
                                    OnPageIndexChanging="Gv_PaymentTemplate_PageIndexChanging"
                                    OnSelectedIndexChanged="Gv_PaymentTemplate_SelectedIndexChanged">
                                    <Columns>
                                       <%-- <asp:TemplateField HeaderText="Sr.No." HeaderStyle-CssClass="c-col-size-8">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_SrNo" Text='<%#Eval("SrNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Template">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_PaymentTemplateName" Text='<%#Eval("PaymentTemplateName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Generation">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_FeeGenerationType" Text='<%#Eval("FeeGenerationType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PaymentTo">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_PaymentTo" Text='<%#Eval("PaymentTo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Amount" Text='<%#Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_IsActiveDescription" Text='<%#Eval("IsActiveDescription") %>'></asp:Label>
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
        </div>
    </form>
</body>
</html>
