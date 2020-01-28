<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_PurchaseOrderBrowse.aspx.cs" Inherits="Form_PurchaseOrderBrowse" %>

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
        $(function () {
            $('#Dp_OrderFromDate').datepicker({
                //dateFormat: 'dd-mm-yy'
            });
        });
        $(function () {
            $('#Dp_OrderToDate').datepicker({
                //dateFormat: 'dd-mm-yy'
            });
        });

        function ValidateDates() {
            var EnteredDate = document.getElementById("Dp_OrderFromDate").value;
            var pattern = /^(0?[1-9]|[12][0-9]|3[01])[- /.]((0?[1-9]|1[012])[- /.](19|20)?[0-9]{2})*$/;
            if (EnteredDate == "") {
                alert("Please Enter From date");
                document.getElementById("Dp_OrderFromDate").focus();
                return false;
            }
            else if (!pattern.test(EnteredDate)) {
                alert("Enter Valid From date");
                document.getElementById("Dp_OrderFromDate").focus();
                return false;
            }
            else {
                var EnteredDate = document.getElementById("Dp_OrderToDate").value;
                if (EnteredDate != "") {
                    if (!pattern.test(EnteredDate)) {
                        alert("Enter Valid To date");
                        document.getElementById("Dp_OrderToDate").focus();
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else {
                    alert("Please Enter To date");
                    document.getElementById("Dp_OrderToDate").focus();
                    return false;
                }
            }
        }
    </script>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#GV_PurchaseBrowse').dataTable({
                "bLengthChange": true,
                "paging": true,
                "sPaginationType": "full_numbers",                    //For Different Paging  Style
                // "scrollY": 400,                                     // For Scrolling
                "jQueryUI": true,                                      //Enabling JQuery UI(User InterFace)
                "aaSorting": [[1, "desc"]],                               //To sort by created date column
                "aaSorting": [],
                //"dom": '<"top"flp>rt<"bottom"i><"clear">',
                "lengthMenu": [[20, 25, 50, 100, 200, -1], [20, 25, 50, 100, 200, "All"]]
                //"bFilter": false

            });

            $('div.dataTables_filter input').addClass('form-control');
            $('div.dataTables_length select').addClass('form-control');
        });


        $(document).ready(function () {
            $('#GV_PurchaseBrowse').dataTable({
                "dom": '<"top"lfi>rt<"bottom"p><"clear">',
                "bLengthChange": true,
                "paging": true,
                "sPaginationType": "full_numbers",                    //For Different Paging  Style
                // "scrollY": 400,                                     // For Scrolling
                "jQueryUI": true,                                      //Enabling JQuery UI(User InterFace)
                //"aaSorting":[[2,"desc"]]                               //To sort by created date column
                "aaSorting": [],
                //"dom": '<"top"flp>rt<"bottom"i><"clear">',
                "lengthMenu": [[5, 10, 25, 50, 100, 200, -1], [5, 10, 25, 50, 100, 200, "All"]]
                //"bFilter": false

            });

            $('div.dataTables_filter input').addClass('form-control');
            $('div.dataTables_length select').addClass('form-control');
            //$('div.dataTables_info').addClass('form-control');
        });


    </script>

    <style type="text/css">
        #GV_PurchaseBrowse_filter {
            /*width: 20%;*/
            float: right;
            padding-top: 7px;
            text-align: right;
            padding-left: 10px;
        }
        /*#example_filter {
            visibility:hidden; //To hide the global search
            height:0;
        }*/

        #GV_PurchaseBrowse_length {
            /* width: 13%; */
            float: left;
            padding-top: 7px;
            /* margin-left: 7%; */
        }

        #GV_PurchaseBrowse_paginate {
            /*width:60%;*/
            float: right;
        }

        .odd {
            border-bottom: 2px solid #E1E5F2;
        }

        .even {
            border-bottom: 2px solid #E1E5F2;
        }

        @media screen and (max-width:900px) {
            #GV_PurchaseBrowse_length {
                width: 100%;
                float: left;
                padding-top: 7px;
                text-align: left;
                padding-left: 10px;
            }

            #GV_PurchaseBrowse_paginate {
                width: 100%;
                text-align: left;
                padding-left: 10px;
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
                        <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Purchase Order Browse"></asp:Label>
                    </div>
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label3" CssClass="Label" runat="server" Text="Supplier Name"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <div class="col-md-2 c-col-size-43">
                                    <asp:DropDownList ID="DD_Supplier" runat="server" CssClass="form-control select2 "
                                        DataTextField="SupplierName" DataValueField="SupplierId" Style="width: 100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Lbl_SupplierName" CssClass="Label" runat="server" Text="Order Number"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                    <asp:DropDownList ID="Dd_OrderNumber" runat="server" CssClass="form-control select2 "
                                        DataTextField="PurchaseOrderNumber" DataValueField="PurchaseOrderHeaderId" Style="width: 100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label2" CssClass="Label" runat="server" Text="From Date"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <div class="col-md-2 c-col-size-43">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control pull-right"
                                            ID="Dp_OrderFromDate" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label5" CssClass="Label" runat="server" Text="To Date"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control pull-right"
                                            ID="Dp_OrderToDate" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 col-md-offset-4 c-button-box-intermidiate c-inline-space">
                            <div class="col-md-10 no-padding">
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Refresh" runat="server" Text="Refresh" OnClick="Btn_Refresh_Click"
                                        CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-with-shadow" OnClientClick="return ValidateDates()" />
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 no-padding">
                            <div class="table-responsive no-padding c-grid-container">
                                <table class="table no-margin OrderTable" id="GV_PurchaseBrowse">
                                    <thead>
                                        <tr class="c-grid-header">
                                            <th class="c-grid-col-size-50">Sr.No.</th>
                                            <th class="c-grid-col-size-150" style="text-align: left;">Supplier Name&nbsp&nbsp</th>
                                            <th class="c-grid-col-size-100">Order Number</th>
                                            <th class="c-grid-col-size-100">Order Date</th>
                                            <th class="c-grid-col-size-100">Total Amount</th>
                                            <th class="c-grid-col-size-100">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody id="PurchaseBrowse_Body" runat="server">
                                    </tbody>
                                </table>
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
