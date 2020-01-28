<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_OutstandingFees.aspx.cs" Inherits="Form_OutstandingFees" %>

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
            $('#Gv_OutstandingFees').dataTable({
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
            $('#Tb_Month').datepicker({
                dateFormat: 'mm'
            });
        });

        $(function () {
            $('#Tb_Year').datepicker({
                dateFormate: 'yyyy'
            });
        })

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

    <script type="text/javascript">

        function CheckUncheck(StudentId) {
            //alert('hii ' + StudentId.toString());


            //$("#hiddenField").val(); //by id
            //$("[name='hiddenField']").val(); // by name
            //$(".hiddenField").val(); // by class

            //to set
            //$('input[type=hidden]').attr('value', newValue);

            /////////////////////////////
            //var Hf_SelectedStudentIds = document.getElementById("Hf_SelectedStudentIds");
            //if (Hf_SelectedStudentIds == "") {
            //    
            //}


            var SelectedStudentIds = $("#Hf_SelectedStudentIds").val(); //by id
            if (SelectedStudentIds == "") {
                SelectedStudentIds = StudentId;
                $('#Hf_SelectedStudentIds').attr('value', SelectedStudentIds);
            }
            else if (SelectedStudentIds != "") {
                //alert("this is second");

                if (SelectedStudentIds.indexOf(StudentId) != -1) {
                    //console.log(str2 + " found");
                    //alert(SelectedStudentIds.indexOf(StudentId) + " this is index");
                    if (SelectedStudentIds.indexOf(StudentId) == 0) {
                        if (SelectedStudentIds.indexOf(",") != -1) {
                            SelectedStudentIds = SelectedStudentIds.toString().replace(StudentId + ',', '');
                        }
                        else {
                            SelectedStudentIds = SelectedStudentIds.toString().replace(StudentId, '');
                        }
                        $('#Hf_SelectedStudentIds').attr('value', SelectedStudentIds);
                    }
                    else {
                        SelectedStudentIds = SelectedStudentIds.toString().replace("," + StudentId, '');
                        $('#Hf_SelectedStudentIds').attr('value', SelectedStudentIds);
                    }
                }
                else {
                    SelectedStudentIds = SelectedStudentIds + "," + StudentId;
                    $('#Hf_SelectedStudentIds').attr('value', SelectedStudentIds);
                }
            }
            //alert(SelectedStudentIds + " full value");


        }

    </script>

    <%--dont take this style to common.--%>
    <style type="text/css">
        #Gv_OutstandingFees_filter {
            /*width: 20%;*/
            float: right;
            padding-top: 7px;
            text-align: left;
            padding-left: 10px;
        }

        #Gv_OutstandingFees_length {
            /* width: 13%; */
            float: left;
            padding-top: 7px;
            /* margin-left: 7%; */
        }

        #Gv_OutstandingFees_paginate {
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
            #Gv_OutstandingFees_length {
                width: 100%;
                float: left;
                padding-top: 7px;
                text-align: left;
                padding-left: 10px;
            }

            #Gv_OutstandingFees_paginate {
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

                    <div class="box-header c-box-blueish  with-border">
                        <asp:Label ID="Lbl_Header" runat="server" CssClass="box-title" Text="Outstanding Fees"></asp:Label>
                    </div>

                    <%--box body start--%>
                    <div class="box-body c-padding-top-2">
                        <div class="c-btn-Renewal">
                            <asp:Button runat="server" ID="Btn_AnnualMembershipRenewal" CssClass="btn btn-primary bg-purple c-bg-blueish btn-block  btn-flat" Text="Print Annual Membership Renewal " OnClick="Btn_AnnualMembershipRenewal_Click" />
                        </div>
                        <%--<div class="col-md-12">
                            <div class="col-md-12 c-inline-space">
                                <div class="col-md-2 c-col-size-14 c-label-1">
                                    <asp:Label ID="Label5" runat="server" CssClass="Label" Text="From Date"></asp:Label>
                                </div>
                                <div class="col-md-10 no-padding c-col-size-86">
                                    <div class="col-md-4 c-col-size-43">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                                <asp:TextBox runat="server" CssClass="form-control"
                                                    ID="Dp_FromDate" data-date-format="dd/mm/yyyy" AutoCompleteType="Disabled"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2 c-col-size-20 c-label-1 c-padding-left-1">
                                        <asp:Label ID="Label7" CssClass="Label" runat="server" Text="To Date"></asp:Label>
                                    </div>
                                    <div class="col-md-4 c-col-size-43">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                                <asp:TextBox runat="server" CssClass="form-control pull-right"
                                                    ID="Dp_ToDate" data-date-format="dd/mm/yyyy">
                                                </asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-2 c-col-size-40">
                                    </div>
                                </div>
                            </div>

                        </div>--%>
                        <div class="col-md-12  c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label4" CssClass="Label" runat="server" Text="From Date"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-2 c-col-size-24">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control pull-right"
                                            ID="Dp_FromDate" data-date-format="dd/mm/yyyy"></asp:TextBox>
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
                                            ID="Dp_ToDate" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                </div>
                                <div class="col-md-2 c-col-size-24">
                                    <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Search_Click" />

                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div class="table-responsive no-padding c-  -container c-inline-space">
                                <div>
                                    <%--style="border-color:#A5969E;border-width:1px;border-style:solid;"--%>
                                    <table class="table" id="Gv_OutstandingFees">
                                        <thead>
                                            <tr class="c-grid-header">
                                                <th>Membership Number</th>
                                                <th>FullName</th>
                                                <th>Membership Date</th>
                                                <th>Mobile</th>
                                                <th>Dojo</th>
                                                <th>Belt Name</th>
                                                <th>Balance</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tlist" runat="server"></tbody>
                                    </table>
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
