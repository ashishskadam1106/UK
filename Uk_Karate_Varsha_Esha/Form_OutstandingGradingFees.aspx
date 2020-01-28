<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_OutstandingGradingFees.aspx.cs" Inherits="Form_OutstandingGradingFees" %>


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

        .c-btn-width-55 {
            width: 55%;
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

            .c-btn-width-55 {
                width: 55%;
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


        $(document).ready(function () {
            var table = $('#example').DataTable({
                scrollX: true,
                scrollCollapse: true,
                autoWidth: true,
                paging: true,
                columnDefs: [
                { "width": "150px", "targets": [0, 1] },
                { "width": "40px", "targets": [4] }
                ]
            });
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
                        <asp:Label ID="Lbl_Header" runat="server" CssClass="box-title" Text="Outstanding Grading Fee"></asp:Label>
                    </div>

                    <%--box body start--%>
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-2 c-col-size-24">
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label4" CssClass="Label" runat="server" Text="Dojo"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-24">
                                    <asp:DropDownList ID="Dd_DojoCode" runat="server" CssClass="form-control select2 "
                                        DataTextField="DojoCode" DataValueField="DojoId" Style="width: 100%" OnSelectedIndexChanged="Dd_DojoCode_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                </div>
                                <div class="col-md-2 c-col-size-24">
                                    <asp:Button runat="server" ID="Btn_Print" CssClass="btn btn-primary bg-purple c-bg-blueish btn-block  btn-flat" Text="Print" OnClick="Btn_Print_Click" />
                                </div>
                            </div>
                        </div>


                        <div class="col-md-12">
                            <div class="table-responsive no-padding c-  -container" id="a" runat="server" visible="false">
                                <div>
                                    <%--style="border-color:#A5969E;border-width:1px;border-style:solid;"--%>
                                    <table class="table" id="Gv_GradingEvent">
                                        <thead>
                                            <tr class="c-grid-header">
                                                <th>Level</th>
                                                <th>Dojo Code</th>
                                                <th>Full Name</th>
                                                <th>Grade</th>
                                                <th>Event StartDate</th>
                                                <th>Due</th>
                                                <th>PayFee</th>
                                                <th>Membership Fee</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tlist" runat="server"></tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="table-responsive no-padding c-grid-container c-inline-space">
                                <asp:GridView runat="server" ID="Gv_Event" CssClass="table"
                                    CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                    HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                    DataKeyNames="EventDetailId"
                                    AllowPaging="true" PageSize="5"
                                    PagerStyle-CssClass="c-paging" OnPageIndexChanging="Gv_Event_PageIndexChanging"
                                    AutoGenerateColumns="false" OnSelectedIndexChanged="Gv_Event_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Label" HeaderStyle-CssClass="c-col-size-8">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Label" Text='<%#Eval("Label") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DojoCode">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_DojoName" Text='<%#Eval("DojoName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="FullName">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_FullName" Text='<%#Eval("FullName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Grade">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Grade" Text='<%#Eval("Grade") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Event Start date">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Fees" Text='<%#Eval("EventDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Due">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Due" Text='<%#Eval("FeesPaid") %>'></asp:Label>
                                                <asp:HiddenField ID="Hf_PayFee" runat="server" Value='<%#Eval("Fees") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grading Fee" HeaderStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_GradingFee" Text='<%#Eval("GradingFeeStatus") %>' Visible="false"></asp:Label>
                                                &nbsp;
                                                <asp:TextBox ID="Tb_GradingAmount" runat="server" Width="60px" Text='<%#Eval("GradingFee") %>'></asp:TextBox>
                                                <asp:Button ID="Btn_Pay" runat="server" Text="Pay" CommandName="Pay"
                                                    CssClass="btn bg-purple c-bg-blueish btn-flat c-btn-height c-btn-width-55" CommandArgument='<%#Eval("EventDetailId") %>' OnClick="btn_Pay_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Membership Fee">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_MembershipFee" Text='<%#Eval("MembershipFee") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

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
