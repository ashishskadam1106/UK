<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_FeeStructure.aspx.cs" Inherits="Form_FeeStructure" %>

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


    <script type="text/ecmascript">
        $(document).ready(function () {
            var touch = $('#touch-menu');
            var menu = $('.menu');
            var submenu = $('.sub-menu');
            var mainmenu = $('.mainmenu');

            $(touch).on('click', function (e) {
                //submenu.show();
                menu.slideToggle();
            });

            //$(mainmenu).on('click', function (e) {
            //menu.slideToggle();
            //     submenu.show();
            //     menu.show();
            // });


        });
    </script>
    <script type="text/javascript">
        function Error(msg) {
            alert('' + msg);
        }

        function scrollToDiv(ControlNameToScroll) {
            alert(ControlNameToScroll);
            document.getElementById('Gv_BudgetOverview_Lbl_CategoryName_13').scrollIntoView();
        }
    </script>
    <style>
        .GridViewHeader th {
            padding-left: 1%;
            padding-top: 1%;
            padding-bottom: 1%;
        }
        .GridViewHeader {
            background: linear-gradient(45deg, #01B9D3, #0191A5) !important;
            color:white;
            font-family: 'Source Sans Pro',Calibri,'Helvetica Neue',Helvetica,Arial,sans-serif;
            margin: 0;
            line-height: 1;
            font-weight:normal !important;
        }


        .GridCategoryOverview {
            background:linear-gradient(45deg, #01B9E4, #019194) !important;
            border: 1px solid white;
            color: white;
            
            padding-left: 1%;
            padding-top: 1%;
            padding-bottom: 1%;
        }

        .GridView3 td {
            padding-left: 1%;
            padding-right: 1%;
            padding-top: 0.5%;
            padding-bottom: 0.5%;
        }

        .Category {
            margin-left: 2%;
        }

        .GridAmounts {
            margin-right: 1%;
            text-align: right;
        }

        .ChildGrid {
            width: 100%;
            /* background:linear-gradient(45deg, #01B9E4, #019194) !important;*/
             background: linear-gradient(45deg, #01B9D3, #0191A5) !important;
        }

            .ChildGrid td {
                /*background-color: #eee !important;*/
                background-color: #f7f6f6 !important;
                color: black;
                padding-left: 1%;
                padding-right: 1%;
                padding-top: 0.5%;
                padding-bottom: 0.5%;
            }

            .ChildGrid th {
                /*background-color: #6C6C6C !important;*/
                
                /*background-color: #30717a !important;*/
                border-color: white;
                color: White;
                font-size: 10pt;
                /*line-height:200%*/
                padding-left: 1%;
                padding-right: 1%;
                padding-top: 0.5%;
                padding-bottom: 0.5%;
            }

        .TimeStamp {
            text-align: center;
        }

        .DetailLabel {
            margin-left: 3%;
        }

        .ShowPanel {
            visibility: visible;
        }

        .GridTextBox {
            /*height:90%;*/
            height: 28px;
            padding: 0px 2%;
            width: 95%;
            margin-left: 2.5%;
            margin-top: 1%;
        }

        .GVFoot_BtnContainer {
            text-align: center;
        }

        .GridBtnTwo {
            height: 30px;
            padding: 0px 10%;
            width: 45%;
        }

        .GridBtnOne {
            height: 30px;
            padding: 0px 10%;
            width: 90%;
        }

        .Btn-CatAction {
            height: 30px;
            padding: 1px 10%;
        }

        .pagination-sm table td {
            padding: 4px 7px;
            background-color: white !important;
        }

            .pagination-sm table td:hover {
                color: blue !important;
                background-color: #205d74 !important;
                color: white !important;
                text-decoration: none;
            }
        /*For Modal PopUp*/
        .modal-dialog {
            width: 70%;
            height: 100%;
        }

        .modal-body {
            height: 60%;
        }

        .modal-content {
            height: 60%;
        }

        .modal-header {
            background-color: #1EA2A7;
            color: white;
            padding-left: 7%;
        }

        .modal-footer {
            border-top: 0px;
        }

        .ModalGridView {
            height: 150px;
            overflow: scroll;
        }

        .ModalMid {
            width: 50%;
        }

        .TextSplit {
            width: 22%;
        }

        .Right {
            text-align: right;
        }

        .Left {
            text-align: left;
        }

        .RightLabel {
            text-align: right;
            padding-right: 0;
        }

        .OverviewTotal {
            background-color: #0c4363;
            color: white;
        }

        .c-btn-width-47 {
            width: 46%;
        }
        /*For Modal PopUp End*/
        /*@media screen and (max-width:1000px) {
            .Btn-CatAction {
                height: 30px;
                padding: 2% 10%;
            }
        }*/
        @media screen and (max-width:900px) {
            .GridBtnTwo {
                width: 90%;
            }

            .TextSplit {
                width: 100%;
            }

            .Right {
                text-align: center;
            }

            .Left {
                text-align: center;
            }

            .modal-dialog {
                width: 90%;
                height: 100%;
            }

            .modal-body {
                height: 100%;
            }

            .modal-content {
                height: 100%;
            }

            .RightLabel {
                text-align: left;
                padding: 0;
                margin: 0;
            }

            .Btn-CatAction {
                height: 30px;
                padding: 1px 10%;
            }

            .c-btn-width-47 {
                width: 46%;
            }
        }
    </style>
    <style>
        .c-col-size-8 {
            width: 8%;
        }

        .c-col-size-12 {
            width: 12%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-16 {
            width: 16%;
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

        .c-header-40 {
            width: 35%;
        }

        .c-btn-height {
            margin: 1%;
            height: 28px;
            padding: 0px;
        }

        .c-btn-width-47 {
            width: 46%;
        }

        #Gv_Quotation {
            font-size: 13px;
        }

            #Gv_Quotation > tbody > tr > td > .form-control {
                padding: 0px;
                height: 30px;
                margin-top: 2px;
                margin-bottom: 2px;
                width: 40px;
            }

        @media screen and (max-width:992px) {
            .c-col-size-12 {
                width: 100%;
                padding-left: 0px;
                padding-right: 0px;
            }

            .c-col-size-16 {
                width: 100%;
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

            .c-btn-width-47 {
                width: 100%;
            }
        }
    </style>


    <script type="text/javascript">
        $(function () {
            $("[src*=Minus-25]").each(function () {
                $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                $(this).next().remove()
            });
        });
    </script>

    <script type="text/javascript">
        function ShowSplitCategoryModal() {
            $('#modalSplitCategory').modal('show');

        }
    </script>

    <script type="text/javascript">
        function Check(rowindex) {
            alert('hi' + rowindex);
        }
    </script>


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
                        <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Fee Management"></asp:Label>
                    </div>
                    <asp:HiddenField ID="HF_Action" runat="server" Value="1" />
                    <%--<asp:HiddenField ID="Hf_FeeCategoryId" runat="server" Value="0" />--%>
                    <asp:HiddenField ID="Hf_FeeIdToUpdate" runat="server" Value="0" />
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12" style="width: 100%">
                            <asp:GridView ID="Gv_FeeCategoryView" runat="server" DataKeyNames="FeeCategoryId"
                                AutoGenerateColumns="false" CellSpacing="1" BorderWidth="1px"
                                HeaderStyle-BorderColor="White" RowStyle-CssClass="GridCategoryOverview"
                                CssClass="GridView3" HeaderStyle-CssClass="GridViewHeader" EmptyDataText="Data not found"
                                OnRowDataBound="Gv_FeeCategoryView_RowDataBound" OnSelectedIndexChanged="Gv_FeeCategoryView_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <%--<img alt = "" style="cursor: pointer" src="Images/Plus-25.png"/>--%>
                                            <asp:ImageButton ID="ImgBtn_ShowHide" runat="server" ImageUrl="~/Images/Plus-25.png"
                                                CommandArgument="Show" OnClick="ImgBtn_ShowHide_Click" />

                                            <asp:Panel ID="Pnl_Fees" runat="server" Visible="false" Style="position: relative">
                                                <%--Child grid view start--%>
                                                <asp:GridView ID="Gv_Fees" runat="server"
                                                    CellSpacing="1" BorderWidth="1px"
                                                    AutoGenerateColumns="false" DataKeyNames="FeeId,FeeCategoryId"
                                                    CssClass="ChildGrid"
                                                    AllowPaging="true"
                                                    PageSize="5" PagerStyle-CssClass="pagination-sm"
                                                   
                                                    OnPageIndexChanging="Gv_Fees_PageIndexChanging"
                                                   OnSelectedIndexChanged="Gv_Fees_SelectedIndexChanged"
                                                    OnRowEditing="Gv_Fees_RowEditing"
                                                    OnRowUpdating="Gv_Fees_RowUpdating"
                                                    ShowFooter="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Fee Name" HeaderStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbl_FeeName" runat="server" Text='<%#Eval("FeeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                           <%-- <EditItemTemplate>
                                                                <asp:TextBox ID="Tb_FeeName" runat="server" Text='<%#Eval("FeeName") %>' CssClass="form-control GridTextBox"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="Tb_FeeName" runat="server" CssClass="form-control GridTextBox"></asp:TextBox>
                                                            </FooterTemplate>--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fee Generation Type" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="Lbl_FeeGenerationType" Text='<%#Eval("FeeGenerationType") %>'></asp:Label>
                                                                <asp:HiddenField ID="Hf_FeeGenerationTypeId" runat="server" Value='<%#Eval("FeeGenerationTypeId") %>' />
                                                            </ItemTemplate>


                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="Lbl_Amount" Text='<%#Eval("Amount") %>'></asp:Label>
                                                            </ItemTemplate>


                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="25%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbl_Remark" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>


                                                        <%--              <asp:TemplateField HeaderText="Action" HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Button ID="Btn_Edit" runat="server" Text="Edit" CommandName="select"
                                                                    CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-grid-button" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="30%">
                                                            <ItemTemplate>
                                                                <asp:Button ID="Btn_Edit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn bg-purple c-bg-blueish btn-flat c-btn-height c-btn-width-47" />
                                                                <asp:Button ID="Btn_Delete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn bg-purple c-bg-blueish btn-flat c-btn-height c-btn-width-47" CommandArgument='<%#Eval("Index") %>' Enabled="false" />
                                                                <%--      OnClientClick="return confirm('Do you want to delete Product Variety?')" OnClick="Btn_Product_Delete_Click"--%>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                                <asp:Button ID="Btn_Update" runat="server" Text="Update" CommandName="Update" CssClass="btn bg-purple c-bg-blueish btn-flat c-btn-height c-btn-width-47" />
                                                                <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn bg-purple c-bg-blueish btn-flat c-btn-height c-btn-width-47" />
                                                            </EditItemTemplate>--%>

                                                        </asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>
                                                
                                                <%--Child grid view End--%>

                                                <div class="col-md-12 c-inline-space">
                                                    <div class="col-md-2">
                                                        <asp:Button ID="Btn_Add" runat="server"
                                                            Text="Add Fee" CssClass="btn btn-primary Btn-CatAction" CommandArgument='<%#Eval("FeeCategoryId") %>'
                                                            OnClick="Btn_Add_Click" data-backdrop="static"
                                                            data-keyboard="false" />


                                                    </div>
                                                </div>

                                            </asp:Panel>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Fee Category" >
                                        <ItemTemplate>
                                            <asp:Label ID="Lbl_CategoryName" runat="server" Text='<%#Eval("FeeCategory") %>' CssClass="Category"></asp:Label>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Fee Collection Stage" HeaderStyle-Width="24%">
                                        <ItemTemplate>
                                            <asp:Label ID="Label54" runat="server" Text='<%#Eval("FeeCollectionStage") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Only One to use" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="Label55" runat="server" Text='<%#Eval("IsOneOfTheGroupText") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="Label56" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>

                    <div class="modal fade" id="Modal_Fee" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header box-header c-box-blueish c-modal-head">
                                    <button type="button" class="close c-close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Add Fee</h4>
                                </div>
                                <div class="modal-body c-modal-height">
                                    <asp:Label ID="Lbl_Message" runat="server"></asp:Label>
                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-2 c-col-size-14 c-label-1">
                                            <asp:Label ID="Lbl_FeeName" runat="server" CssClass="Label" Text="Fee Name*"></asp:Label>
                                        </div>

                                        <div class="col-md-10 no-padding c-col-size-86">
                                            <div class="col-md-4 c-col-size-40">
                                                <asp:TextBox ID="Tb_FeeName" runat="server" CssClass="form-control" placeholder="Fee Name"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                                <asp:Label ID="Lbl_FeeGenrationType" runat="server" CssClass="Label" Text="Fee Genration Type"></asp:Label>
                                            </div>
                                            <div class="col-md-4 c-col-size-40">
                                                <asp:DropDownList ID="Dd_FeeGenrationType" runat="server" DataValueField="FeeGenerationTypeId" DataTextField="FeeGenerationType" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-2 c-col-size-14 c-label-1">
                                            <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Remark"></asp:Label>
                                        </div>
                                        <div class="col-md-10 no-padding c-col-size-86">
                                            <asp:TextBox ID="Tb_Remark" runat="server" CssClass="form-control c-tb-noresize" placeholder="Remark" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-2 c-col-size-14 c-label-1">
                                            <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Amount"></asp:Label>
                                        </div>
                                        <div class="col-md-10 no-padding c-col-size-86">
                                            <div class="col-md-4 c-col-size-40">
                                                <asp:TextBox ID="Tb_Amount" runat="server" CssClass="form-control c-tb-noresize" placeholder="Amount"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12 col-md-offset-3 c-button-box">
                                        <div class="col-md-10 no-padding">
                                            <div class="col-md-6 c-btn-widht-25">
                                                <asp:Button ID="Btn_Save" runat="server" Text="Save" CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-with-shadow" OnClick="Btn_Save_Click" />
                                            </div>
                                            <div class="col-md-4 c-btn-widht-25">
                                                <asp:Button ID="Btn_Back" runat="server" Text="Back" CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Back_Click" />
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

    </form>
</body>
</html>
