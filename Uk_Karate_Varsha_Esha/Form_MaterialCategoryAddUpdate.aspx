<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_MaterialCategoryAddUpdate.aspx.cs" Inherits="Form_MaterialCategoryAddUpdate" %>


<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        function Savealert() {
            swal({
                type: 'success',
                title: 'Material Category Added Successfully ',
                allowOutsideClick: false,

            }).then(function (name) {
                window.location = 'Form_MaterialCategories.aspx';
            })
        }
        function Updatealert() {
            swal({
                type: 'success',
                title: 'Material Category updated Successfully ',
                allowOutsideClick: false,

            }).then(function (name) {
                window.location = 'Form_MaterialCategories.aspx';
            })
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
                        <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Add Material Category"></asp:Label>
                    </div>
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label1" runat="server"  Text="Category Name *"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_CategoryName" runat="server" CssClass="form-control c-tb-noresize" placeholder="Category Name" required="required"
                                        oninvalid="this.setCustomValidity('Please enter Category name')" oninput="this.setCustomValidity('')"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Lbl_Remark" runat="server" Text="Remark"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <asp:TextBox ID="Tb_Description" runat="server" CssClass="form-control c-tb-noresize" placeholder="Remark" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <%--buttons--%>
                        <div class="col-md-12 col-md-offset-2 c-button-box">

                            <div class="col-md-10 no-padding">
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Save" runat="server" Text="Save" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_Save_Click" />
                                </div>

                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_New" runat="server" Text="New" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_New_Click" />
                                </div>

                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Back" runat="server" Text="Back" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Back_Click" />
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
7