<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_StartGradingEvent.aspx.cs" Inherits="Form_StartGradingEvent" %>

<!DOCTYPE html>
<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>StartGradingEvent</title>

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



        .c-btn-addnew {
            width: 120%;
            float: left;
            padding-top: 20px;
            padding-left: 120px;
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




            .c-btn-addnew {
                padding-top: 0px;
            }
        }
    </style>




    <script type="text/javascript">

        $(function () {
            $('#Dp_StartDate').datepicker({
                dateFormate: 'dd/mm/yyyy'
            });
        })



    </script>


    <link rel="stylesheet" href="plugins/datatables/dataTables.bootstrap.css" />
    <script src="plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>




</head>
<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />

            <div class="content-wrapper">
                <div class="box  box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish  with-border">
                        <h3 class="box-title">StartGradingEvent</h3>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12 no-padding">

                            <div class="col-md-12">

                                <div class="page-title clearfix mb10">
                                    <h1 class="page-header">Start Event</h1>
                                </div>

                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <h4>Event Information</h4>
                                    </div>

                                    <div class="panel-body">
                                        <div class="row">

                                            <div class="tab-pane active" id="tab_EventLabel" runat="server">

                                                <div class="col-md-12">
                                                    <div class="col-md-2 c-col-size-14 c-label-1">
                                                        <asp:Label ID="Label2" runat="server" Text="Event Label *"></asp:Label>
                                                    </div>
                                                    <div class="col-md-10 no-padding c-col-size-86">
                                                        <div class="col-md-4 c-col-size-40">
                                                            <asp:TextBox ID="Tb_EventLabel" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                                            <asp:Label ID="Label3" runat="server" Text="Event Date"></asp:Label>
                                                        </div>
                                                        <div class="col-md-4 c-col-size-40">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <asp:TextBox runat="server" CssClass="form-control pull-right"
                                                                    ID="Dp_StartDate" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="tab-pane active" id="Div1" runat="server">
                                                    <div class="col-md-12 c-inline-space">
                                                        <%-- <div class="col-md-12 c-inline-space">		--%>
                                                    </div>


                                                    <div class="tab-pane active" id="Div3" runat="server">
                                                        <div class="col-md-12 c-inline-space">
                                                            <div class="col-md-2 c-col-size-14 c-label-1">
                                                                <asp:Label ID="Lbl_Eventkyu" runat="server" CssClass="Label" Text="Event Kyu"></asp:Label>
                                                            </div>
                                                            <div class="col-md-10 no-padding c-col-size-86">
                                                                <div class="col-md-4 c-col-size-40">
                                                                    <asp:DropDownList ID="Dd_Title" runat="server" DataValueField="EventKyu_Id" DataTextField="EventKyu_Name" CssClass="form-control select2" ></asp:DropDownList>
                                                                </div>
                                                            </div>


                                                        </div>

                                                        <div class="col-md-12 col-md-offset-2 c-button-box-2">
                                                            <br />
                                                        </div>

                                                        <div class="col-md-12 col-md-offset-2 c-button-box-2">

                                                            <div class="col-md-10 no-padding">
                                                                <div class="col-md-4 c-btn-widht-25">
                                                                </div>

                                                                <div class="col-md-4 c-btn-widht-25">
                                                                    <asp:Button ID="Btn_Search" runat="server" Text="Start" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_Search_Click" />
                                                                    <%--  <input type="submit" name="Btn_Search" value="Start" id="Btn_Search" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="">--%>
                                                                </div>

                                                                <div class="col-md-4 c-btn-widht-25">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
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
