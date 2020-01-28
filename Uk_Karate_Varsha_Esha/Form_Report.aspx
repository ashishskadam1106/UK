<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_Report.aspx.cs" Inherits="Form_Report" %>


<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UK_Karate</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />

    <script type="text/javascript">
        $(function () {
            $('#Dp_FromDate').datepicker({
                //dateFormat: 'dd-mm-yy'
            });
        });
        $(function () {
            $('#Dp_ToDate').datepicker({
                //dateFormat: 'dd-mm-yy'
            });
        });

    </script>
    <script>
        function ValidateDates() {
            if (document.getElementById("Dp_FromDate").value == "") {
                alert("Please Enter From date");
                document.getElementById("Dp_FromDate").focus();
                return false;
            }
            else {
                var EnteredDate = document.getElementById("Dp_FromDate").value;
                var pattern = /^(0?[1-9]|[12][0-9]|3[01])[- /.]((0?[1-9]|1[012])[- /.](19|20)?[0-9]{2})*$/;
                if (!pattern.test(EnteredDate)) {
                    alert("Enter Valid From date");
                    document.getElementById("Dp_FromDate").focus();
                    return false;
                }
                else {
                    var EnteredToDate = document.getElementById("Dp_ToDate").value;
                    if (EnteredToDate != "") {
                        if (!pattern.test(EnteredToDate)) {
                            alert("Enter Valid To date");
                            document.getElementById("Dp_ToDate").focus();
                            return false;

                        } else { return true; }
                    }
                    else {
                        alert("Please Enter To date");
                        document.getElementById("Dp_ToDate").focus();
                        return false;
                    }
                }
            }
        }
    </script>
    <style>
        #Div_Grid {
            font-size: 13px;
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
                        <h3 class="box-title">Report</h3>
                    </div>
                    <div class="box-body c-padding-top-2">

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label3" CssClass="Label" runat="server" Text="Report Name *"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <div class="col-md-2 c-col-size-43">
                                    <asp:DropDownList ID="DD_Report" runat="server" CssClass="form-control select2 "
                                        DataTextField="Report" DataValueField="ReportId" Style="width: 100%" AutoPostBack="true"
                                        OnSelectedIndexChanged="DD_Report_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Lbl_SupplierName" CssClass="Label" runat="server" Text="Supplier Name" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                    <asp:DropDownList ID="Dd_SupplierName" runat="server" CssClass="form-control select2 "
                                        DataTextField="SupplierName" DataValueField="SupplierId" Style="width: 100%" AutoPostBack="false"
                                        Visible="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label4" CssClass="Label" runat="server" Text="From Date"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-2 c-col-size-43">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control pull-right"
                                            ID="Dp_FromDate" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label2" CssClass="Label" runat="server" Text="To Date"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control pull-right"
                                            ID="Dp_ToDate" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Lbl_CustomerName" CssClass="Label" runat="server" Text="Customer Name *" Visible="false"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <div class="col-md-2 c-col-size-43">
                                    <asp:DropDownList ID="Dd_Customer" runat="server" CssClass="form-control select2 "
                                        DataTextField="CutomerName" DataValueField="CustomerId" Style="width: 100%" visible="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label5" CssClass="Label" runat="server" Text="Supplier Name" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control select2 "
                                        DataTextField="SupplierName" DataValueField="SupplierId" Style="width: 100%" AutoPostBack="false"
                                        Visible="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 col-md-offset-3 c-button-box-intermidiate">
                            <div class="col-md-10 no-padding">
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Show" runat="server" Text="Show" OnClientClick=" return ValidateDates();" OnClick="Btn_Show_Click"
                                        CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-with-shadow" />
                                </div>

                              
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

