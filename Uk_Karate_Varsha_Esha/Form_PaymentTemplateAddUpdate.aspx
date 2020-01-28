<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_PaymentTemplateAddUpdate.aspx.cs" Inherits="Form_PaymentTemplateAddUpdate" %>

<!DOCTYPE html>
<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UK_Karate</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />
    <script>
        function Confirm() {
            if (confirm('Sure to delete ?') == false) {
                return true;
            }
            else {
                return false;
            }
        }


        $(function () {
            $('#Dp_FromDate').datepicker({
                dateFormat: 'dd-mm-yy'
            });
        });
        $(function () {
            $('#Dp_ToDate').datepicker({
                //dateFormat: 'dd-mm-yy'
            });
        });

    </script>
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

        .c-btn-width-47 {
            width: 47%;
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

        .c-grid-col-size-300 {
            width: 65%;
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

            .c-btn-width-47 {
                width: 100%;
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
                <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish-new with-border">
                        <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Add Payment Template"></asp:Label>
                    </div>
                    <div class="box-body c-padding-top-2">

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Template Name*"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <asp:TextBox ID="Tb_PaymentTemplateName" runat="server" CssClass="form-control c-tb-noresize" placeholder="Payment Template Name"
                                    required="required"
                                    oninvalid="this.setCustomValidity('Please Enter Template Name')" oninput="this.setCustomValidity('')"></asp:TextBox>
                            </div>
                        </div>


                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Generation Type"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:DropDownList ID="Dd_GenerationType" runat="server" CssClass="form-control select2" DataValueField="FeeGenerationTypeId"
                                        DataTextField="FeeGenerationType" AutoPostBack="true" OnSelectedIndexChanged="Dd_GenerationType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Amount *"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Amount" runat="server" CssClass="form-control" placeholder="Amount"
                                    onkeypress="return isNumber(event);" required="required"
                                    oninvalid="this.setCustomValidity('Please Enter Amount')" oninput="this.setCustomValidity('')"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="From Date"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control"
                                            ID="Dp_FromDate" data-date-format="dd/mm/yyyy" placeholder="From Date" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <%--<asp:TextBox ID="Tb_PurchasePrice" runat="server" CssClass="form-control" Placeholder="Purchase Price" onkeypress="return isNumber(event);"></asp:TextBox>--%>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label6" runat="server" CssClass="Label" Text="To Date"></asp:Label>
                                </div>

                                <div class="col-md-4 c-col-size-40">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control"
                                            ID="Dp_ToDate" data-date-format="dd/mm/yyyy" placeholder="To Date" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Is Active"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                     <asp:DropDownList ID="Dd_IsActive" runat="server" CssClass="form-control select2">
                                         <asp:ListItem Value="0" Text="No" Selected="False"></asp:ListItem>
                                         <asp:ListItem Value="1" Text="Yes" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Payment To"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:DropDownList ID="Dd_PaymentTo" runat="server" CssClass="form-control select2" AutoPostBack="true" DataValueField="PaymentToId"
                                        DataTextField="PaymentTo" OnSelectedIndexChanged="Dd_PaymentTo_SelectedIndexChanged">
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>

                         <div class="col-md-12 no-padding">
                            <div class="table-responsive no-padding c-grid-container c-inline-space">
                                <asp:GridView runat="server" ID="Gv_PaymentTemplateApplicableTo" CssClass="table"
                                    CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                    HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                    DataKeyNames="PaymentTemplateId,PaymentTemplateApplicableToId" AutoGenerateColumns="false"
                                     OnRowDataBound="Gv_PaymentTemplateApplicableTo_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="c-col-size-8" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>  
                                                <asp:CheckBox runat="server" ID="Cb_IsSelected" Checked='<%#Eval("IsActive") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment To">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="Hf_PaymentToId" Value='<%#Eval("PaymentToId")%>' />
                                                <asp:HiddenField runat="server" ID="Hf_ReferenceId" Value='<%#Eval("ReferenceId")%>' />
                                                <asp:Label runat="server" ID="Lbl_Reference" Text='<%#Eval("Reference") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                          <asp:TemplateField HeaderText="Initial Generation" HeaderStyle-Width="250">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="Hf_InitialFeeGenerationTypeReferenceId" Value='<%#Eval("InitialFeeGenerationTypeReferenceId")%>' />
                                                <asp:DropDownList runat="server" ID="DD_InitialFeeGenerationTypeReference" Width="250" CssClass="select2" DataValueField="InitialFeeGenerationTypeReferenceId" DataTextField="InitialFeeGenerationTypeReference"></asp:DropDownList>
                                                <asp:HiddenField runat="server" ID="Hf_IsModificationToConsiderForInitialPayment" Value='<%#Eval("IsModificationToConsiderForInitialPayment")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Additional Info">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_AdditionalInfo" Text='<%#Eval("AdditionalInformation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Amount" Text='<%#Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        

                        <div class="col-md-12 col-md-offset-2 c-button-box">
                            <div class="col-md-10 no-padding">
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Save" runat="server" Text="Save" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_Save_Click" />
                                </div>

                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Back" runat="server" Text="Back" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Back_Click" />
                                </div>
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_New" runat="server" Text="New" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_New_Click" />
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
