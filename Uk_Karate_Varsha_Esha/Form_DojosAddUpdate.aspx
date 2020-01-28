<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_DojosAddUpdate.aspx.cs" Inherits="Form_DojosAddUpdate" %>


<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>





<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>UK Karate</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />

    <%--select 2 start--%>
    <!-- Select2 -->
    <script src="plugins/select2/select2.full.min.js"></script>
    <!-- bootstrap time picker -->

    <!-- Select2 -->
    <link rel="stylesheet" href="plugins/select2/select2.min.css" />
    <link rel="stylesheet" href="plugins/timepicker/bootstrap-timepicker.min.css">

    <script>
        $(function () {
            //Initialize Select2 Elements
            $(".select2").select2();
        });
        $(function () {
            $('#Dp_StartDate').datepicker({
                dateFormat: 'dd-mm-yy'
            });
        });


        //Timepicker
        $('#input_starttime').pickatime({
            // 12 or 24 hour
            twelvehour: true,
        });



    </script>

    <style>
        .select2-container--default .select2-selection--single {
            border: 1px solid #d2d6de;
            border-radius: 0px;
            height: 34px;
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
        /*.c-dd-height {
        height:30px;
        
        }*/
        .c-width-46 {
            width: 46%;
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
                        <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Add Dojos"></asp:Label>
                    </div>

                    <div class="box-body c-padding-top-2">

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Dojo Name"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_DojoName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Dojo Code"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_DojoCode" runat="server" CssClass="form-control" required="required"
                                        oninvalid="this.setCustomValidity('Please enter Dojo Code')" oninput="this.setCustomValidity('')"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Post Code"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-20">
                                    <asp:TextBox ID="Tb_PostalCode" runat="server" CssClass="form-control" required="required"
                                        oninvalid="this.setCustomValidity('Please enter Postal Code')" oninput="this.setCustomValidity('')"></asp:TextBox>
                                </div>
                                <div class="col-md-1 c-col-size-20 c-padding-left-1">
                                    <asp:Button ID="Btn_GetAddress" runat="server" Text="Get Address" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" />
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Result"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:DropDownList ID="Dd_Result" runat="server" CssClass="form-control select2" Style="width: 100% !important"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Rent"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Rent" runat="server" CssClass="form-control" required="required"
                                        oninvalid="this.setCustomValidity('Please enter Rent')" oninput="this.setCustomValidity('')"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Term Dojo"></asp:Label>
                                </div>

                                <div class="col-md-4 c-col-size-40">
                                    <asp:DropDownList ID="Dd_TermDojo" DataValueField="RentTermId" DataTextField="RentTerm" runat="server" CssClass="form-control select2" Style="width: 100% !important">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Lbl_Address" runat="server" CssClass="Label" Text=" Address"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <asp:TextBox ID="Tb_Address" runat="server" CssClass="form-control c-tb-noresize" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>


                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Lbl_District" runat="server" CssClass="Label" Text="District"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_District" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>


                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label21" runat="server" CssClass="Label" Text="County" ></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_County" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>

                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label19" runat="server" CssClass="Label" Text="Country"></asp:Label>
                            </div>

                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Country" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Start Date"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control"
                                            ID="Dp_StartDate" data-date-format="dd/mm/yyyy" placeholder="Start Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Latitude" Visible="false"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Latitude" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Longitude" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Longitude" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label17" runat="server" CssClass="Label" Text="Premise" Visible="false"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Premise" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Ward" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Ward" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <%--------------------------GRIDVIEW STARTS-----------------%>
                        <div class="col-md-12">
                            <div class="table-responsive no-padding c-grid-container c-inline-space">

                                <asp:GridView runat="server" ID="Gv_Class" CssClass="table"
                                    CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                    HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                    DataKeyNames="Index" AutoGenerateColumns="false" ShowFooter="true"
                                    OnRowCommand="Gv_Class_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SrNo" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_SrNo" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>

                                                <%-- <%# Container.DataItemIndex + 1 %>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Class" Text='<%#Eval("Class") %>'></asp:Label>
                                                <asp:HiddenField ID="Hf_ClassId" runat="server" Value='<%#Eval("ClassId") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="Dd_Class" runat="server" DataValueField="ClassId" DataTextField="Class"
                                                    CssClass="select2" Width="280">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Day" Text='<%#Eval("Day") %>'></asp:Label>
                                                <asp:HiddenField ID="Hf_DayId" runat="server" Value='<%#Eval("DayId") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="Dd_Day" runat="server" DataValueField="DayId" DataTextField="Day"
                                                    CssClass="select2" Width="280">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Start Time">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="Tb_StartTime" CssClass="form-control" placeholder="Start Time" Width="115" Text='<%#Eval("StartTime") %>'></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:TextBox runat="server" ID="Tb_StartTime" CssClass="form-control" placeholder="Start Time" Width="115" TextMode="Time" format="hh:mm:ss"></asp:TextBox>
                                                <%--<input type="time"  runat="server" ID="Tb_StartTime"  CssClass="form-control" placeholder="Start Time" Width="90">--%>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="End Time">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="Tb_EndTime" CssClass="form-control" placeholder="End Time" Width="115" Text='<%#Eval("EndTime") %>'></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:TextBox runat="server" ID="Tb_EndTime" CssClass="form-control" placeholder="End Time" Width="115" TextMode="Time"></asp:TextBox>
                                                <%--<input type="time" runat="server" ID="Tb_EndTime"  CssClass="form-control" placeholder="End Time" Width="90">--%>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="Btn_Delete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn btn-primary c-bg-blueish btn-block btn-flat c-btn-height" CommandArgument='<%#Eval("Index") %>'
                                                    OnClientClick="return confirm('Do you want to delete Class?')" OnClick="Btn_Delete_Click" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="Btn_Add" runat="server" CssClass="btn btn-primary c-bg-blueish btn-block btn-flat c-btn-height" CommandName="Add" Text="Add" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>
                        <%-- <%# Container.DataItemIndex + 1 %>--%>


                        <div class="col-md-12 col-md-offset-2 c-button-box">
                            <div class="col-md-10 no-padding">
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="BtnSave_Click" />
                                </div>
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="BtnNew" runat="server" Text="New" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="BtnNew_Click" />
                                </div>
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="BtnBack" runat="server" Text="Back" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="BtnBack_Click" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <FT:Footer runat="server" ID="FT_1" />


    </form>
</body>
</html>










