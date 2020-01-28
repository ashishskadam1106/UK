<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_PrintCertificateForKyu.aspx.cs" Inherits="Form_PrintCertificateForKyu" %>

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

        .c-btn-addnew {
            width: 19%;
            float: right;
            padding-right: 16px;
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

        .c-col-size-8 {
            width: 8%;
            padding-left: 0px;
            padding-right: 0px;
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

            .c-col-size-8 {
                width: 8%;
                padding-left: 0px;
                padding-right: 0px;
            }
        }
    </style>

    <link rel="stylesheet" href="plugins/datatables/dataTables.bootstrap.css" />
    <script src="plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#Gv_GradingEvent').dataTable({
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

    <%--dont take this style to common.--%>
    <style type="text/css">
        #Gv_GradingEvent_filter {
            /*width: 20%;*/
            float: right;
            padding-top: 7px;
            text-align: left;
            padding-left: 10px;
        }

        #Gv_GradingEvent_length {
            /* width: 13%; */
            float: left;
            padding-top: 7px;
            /* margin-left: 7%; */
        }

        #Gv_GradingEvent_paginate {
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
            #Gv_GradingEvent_length {
                width: 100%;
                float: left;
                padding-top: 7px;
                text-align: left;
                padding-left: 10px;
            }

            #Gv_GradingEvent_paginate {
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
                        <asp:Label ID="Lbl_Header" runat="server" CssClass="box-title" Text="Print Certificate For Kyu"></asp:Label>
                    </div>
                    <asp:HiddenField ID="Hf_StudentEnentNoteId" runat="server" Value="0" />

                    <%--box body start--%>
                    <div class="box-body c-padding-top-2">

                        <div class="col-md-12">
                            <div class="col-md-2 c-col-size-8 c-label-1">
                                <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Dojo"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-2 c-col-size-24">
                                    <asp:DropDownList ID="Dd_DojoCode" runat="server" CssClass="form-control select2 "
                                        DataTextField="DojoCode" DataValueField="DojoId" Style="width: 100%" OnSelectedIndexChanged="Dd_DojoCode_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 c-col-size-24">
                                    <asp:Button runat="server" ID="Button1" CssClass="btn btn-primary bg-purple c-bg-blueish btn-block  btn-flat" Text="Print" OnClick="Btn_Print_Click" />
                                </div>
                                <div class="col-md-2 c-col-size-24">
                                    <asp:Button runat="server" ID="Btn_ViewEventDetail" CssClass="btn btn-primary bg-purple c-bg-blueish btn-block  btn-flat" Text="View Event Detail" OnClick="Btn_ViewEventDetail_Click" />
                                </div>
                                <div class="col-md-2 c-col-size-24">
                                    <asp:Button runat="server" ID="Btn_printCertificatesAll" CssClass="btn btn-primary bg-purple c-bg-blueish btn-block  btn-flat" Text="Print Certificates All" OnClick="Btn_printCertificatesAll_Click" />
                                </div>
                            </div>
                        </div>


                        <div class="col-md-12">
                            <div class="table-responsive no-padding c-  -container">
                                <div>
                                    <%--style="border-color:#A5969E;border-width:1px;border-style:solid;"--%>
                                    <table class="table" id="Gv_GradingEvent">
                                        <thead>
                                            <tr class="c-grid-header">

                                                <th>Level</th>
                                                <th>Dojo Code</th>
                                                <th>Full Name</th>
                                                <th>Grade</th>
                                                <th>Fees</th>
                                                <th>FeesPaid</th>
                                                <th>Grading Fee</th>
                                                <th>Membership Fee</th>

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
                                        <h4 class="modal-title">Note</h4>
                                    </div>
                                    <div class="modal-body c-modal-height">
                                        <div class="col-md-12 c-inline-space">
                                            <div class="col-md-2 c-col-size-14 c-label-1">
                                                <asp:Label ID="Label37" CssClass="Label" runat="server" Text="Note"></asp:Label>
                                            </div>
                                            <div class="col-md-10 no-padding c-col-size-86">
                                                <asp:TextBox runat="server" ID="Tb_Note" CssClass="form-control" placeholder="Note" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-md-offset-4 c-button-box-intermidiate">
                                            <div class="col-md-10 no-padding">
                                                <div class="col-md-4 c-btn-widht-25">
                                                    <asp:Button ID="btn_SaveNote" runat="server" Text="Save" OnClick="btn_SaveNote_Click"
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

                        <div class="modal fade" id="Modal_Payment" role="dialog">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header box-header c-box-blueish c-modal-head">
                                        <button type="button" class="close c-close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">View Payments</h4>
                                    </div>
                                    <div class="modal-body c-modal-height">
                                        <div class="col-md-12 c-inline-space">
                                            <asp:Label ID="Label1" CssClass="Label" runat="server" Text="Term and Membership Fees" Font-Size="Large"></asp:Label>
                                        </div>
                                        <div class="col-md-12 c-inline-space">
                                            <div class="col-md-6">
                                                <asp:Label ID="Label3" CssClass="Label" runat="server" Text="Fees Paid" Font-Bold="true"></asp:Label><br />
                                                <asp:TextBox ID="Tb_FeePaid" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                             <div class="col-md-6">
                                                <asp:Label ID="Label4" CssClass="Label" runat="server" Text="Date" Font-Bold="true"></asp:Label><br />
                                                <asp:Label ID="Lbl_Date" CssClass="Label" runat="server"></asp:Label><br />
                                            </div>
                                        </div>
                                        <div class="col-md-12 c-inline-space">
                                            <asp:Label ID="Label2" CssClass="Label" runat="server" Text="Grading Fees" Font-Size="Large"></asp:Label>
                                        </div>
                                        <div class="col-md-12 c-inline-space">
                                             <div class="col-md-6">
                                                <asp:Label ID="Label5" CssClass="Label" runat="server" Text="Fees Paid" Font-Bold="true"></asp:Label><br />
                                                <asp:TextBox ID="Tb_GradingFee" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                             <div class="col-md-6">
                                                <asp:Label ID="Label7" CssClass="Label" runat="server" Text="Date" Font-Bold="true"></asp:Label><br />
                                                <asp:Label ID="Lbl_GradingDate" CssClass="Label" runat="server"></asp:Label><br />
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
            </div>
        </div>



    </form>
</body>
</html>

