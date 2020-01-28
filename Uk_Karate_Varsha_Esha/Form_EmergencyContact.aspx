<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_EmergencyContact.aspx.cs" Inherits="Form_EmergencyContact" %>


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

        .SumoSelect {
            width: 100% !important;
        }

            .SumoSelect .select-all {
                border-radius: 3px 3px 0 0;
                position: relative;
                border-bottom: 1px solid #ddd;
                background-color: #fff;
                padding: 8px 0 3px 35px;
                height: 38px !important;
                cursor: pointer;
                /*display: block;
            width: 100%;*/
                /*height: 34px !important;
            padding: 6px 12px !important;
            font-size: 14px !important;
            line-height: 1.42857143 !important;
            color: #555 !important;
            background-color: #fff !important;
            background-image: none !important;
            border: 1px solid #ccc !important;*/
            }

        .SumoSelect {
            height: 34px !important;
            padding: 0px 0px !important;
            font-size: 14px !important;
            line-height: 1.42857143 !important;
            color: #555 !important;
            background-color: #fff !important;
            background-image: none !important;
            border: 1px solid #ccc !important;
        }

            .SumoSelect > .CaptionCont {
                position: relative;
                height: 100% !important;
                border: 0px !important;
                min-height: 14px;
                background-color: #fff;
                border-radius: 0px !important;
                margin: 0;
            }

        .c-button-box-2 {
            margin-top: 1%;
            margin-bottom: 1%;
            padding-left: 10%;
        }

        .resultbox {
            border: 1px solid;
            padding-bottom: 1.5%;
            background-color: #f6feff;
        }

        .inline-icon-size-large {
            padding: 3px;
            font-size: large;
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
<%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>--%>
    <script src="sumoselect/jquery.sumoselect.min.js"></script>
    <link href="sumoselect/sumoselect.css" rel="stylesheet">

    <link rel="stylesheet" href="plugins/datatables/dataTables.bootstrap.css" />
    <script src="plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#Gv_ManageStudent').dataTable({
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

        $(function () {
            $('#Tb_FromDate').datepicker({
                dateFormat: 'dd/mm/yyyy'
            });
        });

        $(function () {
            $('#Tb_ToDate').datepicker({
                dateFormate: 'dd/mm/yyyy'
            });
        })



    </script>
 <style type="text/css">
        #Gv_ManageStudent_filter {
            /*width: 20%;*/
            float: right;
            padding-top: 7px;
            text-align: left;
            padding-left: 10px;
        }

        #Gv_ManageStudent_length {
            /* width: 13%; */
            float: left;
            padding-top: 7px;
            /* margin-left: 7%; */
        }

        #Gv_ManageStudent_info {
            float: left;
            padding-top: 14px;
            margin-left: 9px;
        }

        #Gv_ManageStudent_paginate {
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
            #Gv_ManageStudent_length {
                width: 100%;
                float: left;
                padding-top: 7px;
                text-align: left;
                padding-left: 10px;
            }

            #Gv_ManageStudent_paginate {
                width: 100%;
                text-align: left;
                padding-left: 10px;
            }
        }
    </style>

    <style>
        .ui-datepicker-year {
            display: none;
        }

        .select2 {
            width: 100%;
        }

        .c-label-right {
            float: right;
            margin-right: 5px;
        }

        .c-col-size-12 {
            width: 11%;
            padding-left: 0px;
            padding-right: 2px;
        }

        .c-col-size-15 {
            width: 15%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-23 {
            width: 29%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-30 {
            width: 34.5%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-20 {
            width: 20%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-18 {
            width: 20%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-10 {
            width: 20%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-40 {
            width: 40%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-80 {
            width: 60%;
            padding-left: 0px;
            padding-right: 1px;
        }

        .griddropdown {
            height: 28px !important;
            margin-top: 2px !important;
            margin-bottom: 2px !important;
        }

        .c-height {
            padding: 4px 6px !important;
            height: 28px !important;
            width: 100% !important;
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

            .c-width-46 {
                width: 100%;
            }

            #Btn_Refresh {
                margin-top: 1%;
                margin-left: 20%;
                width: 60%;
            }

            #Btn_ReceiptRefresh {
                margin-top: 1%;
                margin-left: 20%;
                width: 60%;
            }

            .c-col-size-12 {
                width: 50%;
                padding-left: 0px;
                padding-right: 0px;
                padding-bottom: 1%;
            }

            .griddropdown {
                height: 28px;
                margin-top: 2px;
                margin-bottom: 2px;
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
                        <asp:Label ID="Lbl_Header" runat="server" CssClass="box-title" Text="Emergency Contact"></asp:Label>
                    </div>
                    <div class="box-body c-padding-top-2">
                        
                        <div class="col-md-12">
                            <div class="col-md-4 c-btn-widht-25">
                            </div>
                            <div class="col-md-4 c-btn-widht-25">
                            </div>
                            <div class="col-md-4 c-btn-widht-20">
                                <asp:Button runat="server" ID="Btn_DeleteStudent" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" Text="Search" />
                            </div>
                            <div class="col-md-4 c-btn-widht-20">
                               
                            </div>
                        </div>

                        <br />



                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Search"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox runat="server" CssClass="form-control"
                                        ID="Tb_Search" data-date-format="dd/mm/yyyy" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                   
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    
                                </div>
                            </div>
                        </div>



                            <%--<asp:Panel ID="PnlGradeResult" runat="server"></asp:Panel>--%>
                        <div class="col-md-12 c-inline-space">
                            <div class="table-responsive no-padding c-grid-container c-inline-space">
                                <div>
                                    <%--style="border-color:#A5969E;border-width:1px;border-style:solid;"--%>
                                    <table class="table" id="Gv_ManageStudent">
                                        <thead>
                                            <tr class="c-grid-header">
                                                <th></th>
                                                <th>Dojo Code</th>
                                                <th>Dojo Name</th>
                                                <th>Student Mobile No</th>
                                               
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
