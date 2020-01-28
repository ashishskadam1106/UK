<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_EmployeeMaster.aspx.cs" Inherits="Form_EmployeeMaster" %>

<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<!DOCTYPE html>

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

        .c-grid-col-size-300 {
            width: 350px !important;
            text-align: left;
            padding: 1% !important;
            vertical-align: middle;
        }

        .c-grid-col-size-100 {
            width: 100px !important;
            text-align: center;
            vertical-align: middle;
            padding: 1% !important;
        }

        .c-grid-label-3 {
            font-size: 18px;
            color: tomato;
        }

        .c-grid-label-1 {
            font-size: 14px;
            margin-bottom: 2px;
        }
    </style>
    <script>

        function Confirm() {
            if (confirm('Sure to delete ?') == false) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <link rel="stylesheet" href="plugins/datatables/dataTables.bootstrap.css" />
    <script src="plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>


    <script type="text/javascript">

        $(document).ready(function () {
            $('#Gv_EmployeeMaster').dataTable({
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
    </script>

    <%--dont take this style to common.--%>
    <style type="text/css">
        #Gv_EmployeeMaster_filter {
            /*width: 20%;*/
            float: right;
            padding-top: 7px;
            text-align: left;
            padding-left: 10px;
        }

        #Gv_EmployeeMaster_length {
            /* width: 13%; */
            float: left;
            padding-top: 7px;
            /* margin-left: 7%; */
        }

        #Gv_EmployeeMaster_paginate {
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

        @media screen and (max-width:900px) {
            #Gv_EmployeeMaster_length {
                width: 100%;
                float: left;
                padding-top: 7px;
                text-align: left;
                padding-left: 10px;
            }

            #Gv_EmployeeMaster_paginate {
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
                        <asp:Label ID="Lbl_Header" runat="server" CssClass="box-title" Text="Instructor Master"></asp:Label>
                    </div>

                    <%--box body start--%>
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12">
                            <div class="c-btn-addnew">
                                <asp:Button runat="server" ID="Btn_AddEmployee" CssClass="btn btn-primary bg-purple c-bg-blueish btn-block  btn-flat" Text="Add Instructor" OnClick="Btn_AddEmployee_Click" />
                            </div>
                        </div>

                        <%--<div class="col-md-12"></div>--%>
                        <div class="table-responsive no-padding c-grid-container c-inline-space">
                            <div>
                                <%--style="border-color:#A5969E;border-width:1px;border-style:solid;"--%>
                                <table class="table" id="Gv_EmployeeMaster">
                                    <thead>
                                        <tr class="c-grid-header">
                                            <th>Profile</th>
                                            <th>Personal Info</th>
                                            <th>Other Info</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tlist" runat="server"></tbody>
                                </table>
                            </div>
                        </div>


                    </div>
                    <%--box body end--%>
                </div>
            </div>
            <%--content end--%>

            <FT:Footer runat="server" ID="FT_1" />
        </div>
    </form>
</body>
</html>
