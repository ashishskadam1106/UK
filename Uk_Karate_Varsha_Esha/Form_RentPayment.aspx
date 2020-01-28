<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_RentPayment.aspx.cs" Inherits="Form_RentPayment" %>

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
        .c-grid-profile-pic {
            width: 120px;
            text-align: center;
            padding-top: 1% !important;
            padding-bottom: 1% !important;
            vertical-align: top;
        }

        .c-btn-Renewal {
            width: 25%;
            float: right;
        }

        .c-grid-col-size-300 {
            width: 350px !important;
            text-align: left;
            padding: 1% !important;
            vertical-align: middle;
        }

        .c-grid-col-size-100 {
            width: 100px !important;
            text-align: left;
            vertical-align: middle;
            padding: 1% !important;
        }

        .c-grid-col-size-150 {
            width: 150px !important;
            text-align: left;
            vertical-align: middle;
            padding: 1% !important;
        }

        .c-grid-col-size-50 {
            width: 50px !important;
            text-align: left;
            vertical-align: middle;
            padding: 1% !important;
        }

        .c-grid-col-size-200 {
            width: 200px !important;
            text-align: left;
            vertical-align: middle;
            padding: 1% !important;
        }

        .c-grid-col-size-250 {
            width: 250px !important;
            text-align: left;
            vertical-align: middle;
            padding: 1% !important;
        }

        .c-grid-col-size-275 {
            width: 275px !important;
            text-align: left;
            vertical-align: middle;
            padding: 1% !important;
        }


        .c-grid-label-3 {
            font-size: 18px;
            color: tomato;
        }

        .c-grid-label-center {
            text-align: center !important;
            vertical-align: middle !important;
        }

        .c-grid-label-left {
            text-align: left !important;
            vertical-align: middle !important;
        }

        .c-grid-label-green {
            color: green;
        }

        .c-grid-label-red {
            color: red;
        }

        .c-grid-label-1 {
            font-size: 14px;
            margin-bottom: 2px;
        }

        .c-btn-search {
            width: 15%;
            float: right;
        }

        .c-btn-widht-15 {
            width: 15%;
            float: right;
        }

        .c-btn-widht-20 {
            width: 20%;
            float: right;
        }

        .c-grid-row {
            background-color: #B2BABB !important;
            color: white;
        }

        .c-col-size-43 {
            width: 43%;
        }

        @media screen and (max-width:900px) {
            .c-btn-widht-15 {
                width: 100%;
                float: right;
            }

            .c-btn-widht-20 {
                width: 50%;
                float: right;
            }

            .c-col-size-40 {
                width: 100%;
            }

            .c-col-size-43 {
                width: 100%;
            }
        }
    </style>

    <link rel="stylesheet" href="plugins/datatables/dataTables.bootstrap.css" />
    <script src="plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript">


        $(document).ready(function () {
            $('#Gv_Rent').dataTable({
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
        });


        $(function () {
            $('#Dp_FromDate').datepicker({
                dateFormat: 'dd/mm/yyyy'
            });
        });

        $(function () {
            $('#Dp_ToDate').datepicker({
                dateFormate: 'dd/mm/yyyy'
            });
        })


    </script>

    <style type="text/css">
        #Gv_Rent_filter {
            /*width: 20%;*/
            float: right;
            padding-top: 7px;
            text-align: left;
            padding-left: 10px;
        }

        #Gv_Rent_length {
            /* width: 13%; */
            float: left;
            padding-top: 7px;
            /* margin-left: 7%; */
        }

        #Gv_Rent_paginate {
            /*width:60%;*/
            float: right;
        }
        /*#example_filter {
            visibility:hidden; //To hide the global search
            height:0;
        }*/
        .row {
            margin-right: 0px;
        }

        .odd {
            border-bottom: 2px solid #E1E5F2;
        }

        .even {
            border-bottom: 2px solid #E1E5F2;
        }

        #Gv_TotalStudents {
            margin-bottom: 0px !important;
            text-align: left;
            padding-left: 5px;
        }

        #Gv_GradeResult {
            margin-bottom: 0px !important;
            text-align: left;
            padding-left: 5px;
        }

        #Gv_DojoResults {
            margin-bottom: 0px !important;
            text-align: left;
            padding-left: 5px;
        }

        @media screen and (max-width:900px) {
            #Gv_Rent_length {
                width: 100%;
                float: left;
                padding-top: 7px;
                text-align: left;
                padding-left: 10px;
            }

            #Gv_Rent_paginate {
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
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />

            <%--content starts--%>
            <div class="content-wrapper">
                <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish-new with-border">
                        <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Rent Payment"></asp:Label>
                    </div>

                    <div class="box-body c-padding-top-2">
                        <asp:HiddenField ID="Hf_Rent" runat="server" Value="0" />
                        <asp:HiddenField ID="Hf_RentPaymentId" runat="server" Value="0" />
                        <asp:HiddenField ID="Hf_PaidAmount" runat="server" Value="0" />
                        <div class="col-md-12">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label4" CssClass="Label" runat="server" Text="Dojo"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-2 c-col-size-24">
                                    <asp:DropDownList ID="Dd_DojoCode" runat="server" CssClass="form-control select2 "
                                        DataTextField="DojoCode" DataValueField="DojoId">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label1" CssClass="Label" runat="server" Text="Dojo Term"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-24">
                                    <asp:DropDownList ID="Dd_Term_Dojo" runat="server" CssClass="form-control select2 "
                                        DataTextField="RentTerm" DataValueField="RentTermId">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 c-col-size-24 c-label-1 c-padding-left-1">
                                    <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Search_Click" />
                                </div>

                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="table-responsive no-padding c-  -container" id="a" runat="server">
                                <div>
                                    <%--style="border-color:#A5969E;border-width:1px;border-style:solid;"--%>
                                    <table class="table" id="Gv_Rent">
                                        <thead>
                                            <tr class="c-grid-header">
                                                <th style="width: 8%">SrNo</th>
                                                <th>Dojo Code</th>
                                                <th>Term</th>
                                                <th>Rent</th>
                                                <th>Balance</th>
                                                <th>Start Date</th>
                                                <th>Due Date</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tlist" runat="server"></tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade" id="Modal_Note" role="dialog">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header box-header c-box-blueish c-modal-head">
                                        <button type="button" class="close c-close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Pay Rent</h4>
                                    </div>
                                    <div class="modal-body c-modal-height">
                                        <div class="col-md-12 c-inline-space">
                                            <div class="col-md-2 c-col-size-14 c-label-1">
                                                <asp:Label ID="Label37" CssClass="Label" runat="server" Text="Dojo"></asp:Label>
                                            </div>
                                            <div class="col-md-10 no-padding c-col-size-86">
                                                <div class="col-md-4 c-col-size-40">
                                                    <asp:TextBox runat="server" ID="Tb_Dojo" CssClass="form-control" placeholder="Dojo" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 c-col-size-14 c-label-1 c-padding-left-1">
                                                    <asp:Label ID="Label2" CssClass="Label" runat="server" Text="Due Date"></asp:Label>
                                                </div>
                                                <div class="col-md-4 c-col-size-40">
                                                    <asp:TextBox runat="server" ID="Tb_DueDate" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-md-12 c-inline-space">
                                            <div class="col-md-2 c-col-size-14 c-label-1">
                                                <asp:Label ID="Label3" CssClass="Label" runat="server" Text="Rent"></asp:Label>
                                            </div>
                                            <div class="col-md-10 no-padding c-col-size-86">
                                                <div class="col-md-4 c-col-size-40">
                                                    <asp:TextBox runat="server" ID="Tb_Rent" CssClass="form-control" placeholder="Rent"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 c-col-size-40 c-label-1 c-padding-left-1">
                                                    <asp:Button ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click"
                                                        CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
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
