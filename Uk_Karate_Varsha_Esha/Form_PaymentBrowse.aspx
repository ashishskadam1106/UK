<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_PaymentBrowse.aspx.cs" Inherits="Form_PaymentBrowse" %>

<!DOCTYPE html>

<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UK_Karaten</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />
    <script type="text/javascript">
        $(function () {
            $('#Dp_PaymentFromDate').datepicker({
                //dateFormat: 'dd-mm-yy'
            });
        });
        $(function () {
            $('#Dp_PaymentToDate').datepicker({
                //dateFormat: 'dd-mm-yy'
            });
        });
        function Confirm() {
            if (confirm('Sure to Cancel ?') == false) {
                return true;
            }
            else {
                return false;
            }
        }

        function ValidateDates() {
            if (document.getElementById("Dp_PaymentFromDate").value == "")
            { alert("Please Enter From date"); }
            else {
                var EnteredDate = document.getElementById("Dp_PaymentFromDate").value;
                var pattern = /^(0?[1-9]|[12][0-9]|3[01])[- /.]((0?[1-9]|1[012])[- /.](19|20)?[0-9]{2})*$/;
                if (!pattern.test(EnteredDate)) {
                    alert("Enter Valid From date");
                    document.getElementById("Dp_PaymentFromDate").focus();
                    return false;
                }
                else {
                    var EnteredToDate = document.getElementById("Dp_PaymentToDate").value;
                    if (EnteredToDate != "") {
                        if (!pattern.test(EnteredToDate)) {
                            alert("Enter Valid To date");
                            document.getElementById("Dp_PaymentToDate").focus();
                            return false;

                        } else { return true; }
                    }
                    else {
                        alert("Please Enter To date");
                        return false;
                    }
                }
            }
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
                <div class="box  box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish-new with-border">
                        <h3 class="box-title">Payment Browse</h3>
                    </div>
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Lbl_Customer" runat="server" CssClass="Label" Text="Supplier Name"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-2 c-col-size-24">
                                    <asp:DropDownList ID="Dd_Supplier" runat="server" DataValueField="SupplierId" 
                                        DataTextField="SupplierName" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label4" CssClass="Label" runat="server" Text="From Date"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-24">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control pull-right"
                                            ID="Dp_PaymentFromDate" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label1" CssClass="Label" runat="server" Text="To Date"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-24">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control pull-right"
                                            ID="Dp_PaymentToDate" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 col-md-offset-2 c-button-box-intermidiate">
                            <div class="col-md-10 no-padding">
                                <div class="col-md-4 c-btn-widht-25">
                                </div>
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Refresh" runat="server" Text="Refresh" OnClick="Btn_Refresh_Click"
                                        CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-with-shadow" OnClientClick="return ValidateDates();" />
                                </div>
                                <div class="col-md-4 c-btn-widht-25">
                                </div>

                            </div>
                        </div>
                        <%--gridview start--%>
                        <div class="col-md-12 no-padding">
                            <div class="table-responsive no-padding c-grid-container c-inline-space">
                                <asp:GridView runat="server" ID="Gv_Payments" CssClass="table"
                                    CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                    HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                    DataKeyNames="PaymentHeaderId" OnSelectedIndexChanged="Gv_Payments_SelectedIndexChanged"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Label1" Text='<%#Eval("SrNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl2" Text='<%#Eval("SupplierName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Mode">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl3" Text='<%#Eval("PaymentInstrument") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Instrument No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl4" Text='<%#Eval("Cheque_Number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Instrument Date">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl5" Text='<%#Eval("Cheque_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Date">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl6" Text='<%#Eval("Payment_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl7" Text='<%#Eval("Bank_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Amount">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl8" Text='<%#Eval("Payment_Amount","{0:0.00}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" CommandName="Select"
                                                    CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-grid-button" OnClientClick="return confirm('Do you want to Cancel This Payment?')" />
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
