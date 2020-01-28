<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_Payment.aspx.cs" Inherits="Form_Payment" %>

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
        $(function () {
            $('#DP_Instrument_Date').datepicker({
                //dateFormat: 'dd-mm-yy'
            });
        });

        $(function () {
            $('#Dp_Receipt_Date').datepicker({
                //dateFormat: 'dd-mm-yy'
            });
        });

        function SelectFromPanel() {
            $(".select2").select2();

        }

        function ValidateReceiptDates() {
            var EnteredDate = document.getElementById("Dp_Receipt_Date").value;
            var pattern = /^((0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.](19|20)?[0-9]{2})*$/;
            if (!pattern.test(EnteredDate)) {
                alert("Enter Valid Payment date");
                document.getElementById("Dp_Receipt_Date").focus();
                return false;

            } else {
                var EnteredChequeDate = document.getElementById("DP_Instrument_Date").value;
                if (EnteredChequeDate != "") {
                    if (!pattern.test(EnteredChequeDate)) {
                        alert("Enter Valid Instrument date");
                        document.getElementById("DP_Instrument_Date").focus();
                        return false;

                    } else { return true; }
                }
            }
        }
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
            <asp:ScriptManager ID="ScriptManager_1" runat="server"></asp:ScriptManager>
            <%--content starts--%>
            <div class="content-wrapper">
                <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish-new with-border">
                        <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Payment"></asp:Label>
                    </div>
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12 c-inline-space">
                            <asp:Label ID="Lbl_Receipt_No" runat="server" CssClass="Label c-label-right" Text="Payment Number:"></asp:Label>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Lbl_SupplierName" runat="server" CssClass="Label" Text="Supplier Name *"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <asp:UpdatePanel ID="UpdatePanel_CustomerName" runat="server">
                                    <ContentTemplate>
                                        <script type="text/javascript">
                                            Sys.Application.add_load(SelectFromPanel);
                                        </script>
                                        <asp:DropDownList ID="Dd_Supplier" runat="server" DataValueField="SupplierId" DataTextField="SupplierName" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="Dd_Supplier_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Payment Instrument*"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:DropDownList ID="Dd_Payment_Mode" runat="server" DataTextField="PaymentInstrument" DataValueField="PaymentInstrumentId" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Instrument No"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Instrument_No" runat="server" CssClass="form-control c-tb-noresize" placeholder="Instrument No"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Instrument Date"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <link rel="stylesheet" href="plugins/datepicker/datepicker3.css" />

                                        <asp:TextBox runat="server" CssClass="form-control"
                                            ID="DP_Instrument_Date" data-date-format="dd/mm/yyyy" placeholder="Instrument Date"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label20" runat="server" CssClass="Label" Text="Bank Name"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Bank_Name" runat="server" placeholder="Bank Name" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Payment Date *"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control"
                                            ID="Dp_Receipt_Date" data-date-format="dd/mm/yyyy" placeholder="Payment Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Amount *"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:UpdatePanel ID="UpdatePanelAmount" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="Tb_Receipt_Amount" runat="server" CssClass="form-control c-tb-noresize"
                                                placeholder="Amount" AutoPostBack="true"
                                                OnTextChanged="Tb_Receipt_Amount_TextChanged"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label22" runat="server" CssClass="Label" Text="Outstanding Amount"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Tb_Receipt_Amount" EventName="TextChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:TextBox ID="Tb_Outstanding" runat="server" placeholder="Outstanding Amount"
                                                Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label23" runat="server" CssClass="Label" Text="Change Return"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Tb_Receipt_Amount" EventName="TextChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:TextBox ID="Tb_Change_Return" runat="server" placeholder="Change Return"
                                                Enabled="false" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="HF_ChangeReturn" Value="0" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <%--Grid View Starts-------------------------------------%>
                        <div class="col-md-12 no-padding c-inline-space">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive no-padding c-grid-container c-inline-space">
                                        <asp:GridView runat="server" ID="Gv_Payment_Bills" CssClass="table"
                                            CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                            HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                            DataKeyNames="PurchaseBillHeaderId" AutoGenerateColumns="false">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="c-col-size-8">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbl_SrNo" Text='<%#Eval("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bill No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbl_Bill_Number" Text='<%#Eval("BillNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bill Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbl_Bill_Date" Text='<%#Eval("BillDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bill Amount">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbl_Total_Bill_Amount" Text='<%#Eval("TotalBillAmount","{0:0.00}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Paid Amount">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbl_Paid_Amount" Text='<%#Eval("PaidAmount","{0:0.00}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="Advance Paid">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbl_Advance" Text='<%#Eval("Advance_Paid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Balance Amount">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbl_Balance_Amount" Text='<%#Eval("BalanceAmount","{0:0.00}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Allocate Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Tb_Allocate_Amount" runat="server" AutoPostBack="true" Text='<%#Eval("AllocatedAmount","{0:0.00}") %>' OnTextChanged="Tb_Allocate_Amount_TextChanged" CssClass="form-control c-height"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Current Balance">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbl_Current_Balance" Text='<%#Eval("CurrentBalance","{0:0.00}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <%--Grid View Ends-------------------------------------%>



                        <div class="col-md-12 col-md-offset-1 c-button-box">
                            <div class="col-md-10 no-padding">
                                <div class="col-md-3 c-btn-widht-25">
                                    <asp:Button ID="Btn_Save" runat="server" Text="Save" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" ValidationGroup="GroupDate" OnClick="Btn_Save_Click" OnClientClick="return ValidateReceiptDates();" />
                                </div>
                                <div class="col-md-3 c-btn-widht-25">
                                    <asp:Button ID="Btn_New" runat="server" Text="New" CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_New_Click" />
                                </div>
                                <div class="col-md-3 c-btn-widht-25">
                                    <asp:Button ID="Btn_Back" runat="server" Text="Back" CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Back_Click" />
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <FT:Footer runat="server" ID="FT_1" />
    </form>
</body>
</html>
