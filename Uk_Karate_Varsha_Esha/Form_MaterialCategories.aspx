<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_MaterialCategories.aspx.cs" Inherits="Form_MaterialCategories" %>

<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UK_Karate</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />
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
    <style>
        .c-label-right {
            float:right;
            margin-right:5px;
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
        .c-btn-width-47 {
            width: 47%;
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
            padding:0px;
        }
        .c-width-66p {
            width:65px;
        }
        .c-width-200p {
            width: 200px;
        }
        .c-grid-col-size-300 
        {
            width:65%;
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
                margin-top:1px;
            }
            .c-btn-width-47 {
            width: 100%;
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
                <div class="box  box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish with-border">
                        <h3 class="box-title">Material Category Master</h3>
                    </div>
                    <div class="box-body">
                        <%--gridview start--%>
                        <div class="col-md-12 no-padding">                     
                            <div class="table-responsive no-padding c-grid-container c-inline-space">
                                <asp:GridView runat="server" ID="Gv_MaterialCategory" CssClass="table"
                                    CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                    HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                    DataKeyNames="MaterialCategoryId" AutoGenerateColumns="false" ShowFooter="true"
                                    OnRowCancelingEdit="Gv_MaterialCategory_RowCancelingEdit"
                                    OnRowCommand="Gv_MaterialCategory_RowCommand"
                                    OnRowEditing="Gv_MaterialCategory_RowEditing"
                                    OnRowUpdating="Gv_MaterialCategory_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No." HeaderStyle-CssClass="c-col-size-8">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_SrNo" Text='<%#Eval("SrNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Category Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_CategoryName" Text='<%#Eval("CategoryName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="Tb_CategoryName" runat="server" CssClass="form-control c-height" Text='<%#Eval("CategoryName") %>'></asp:TextBox>
                                                <asp:HiddenField ID="HF_MaterialCategoryId" runat="server" Value='<%#Eval("MaterialCategoryId") %>' />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="Tb_CategoryName" runat="server" CssClass="form-control c-height" placeholder="Category Name"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                       
                                         <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Remark" Text='<%#Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="Tb_Remark" runat="server" CssClass="form-control c-height" Text='<%#Eval("Remark") %>'></asp:TextBox>                                                
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="Tb_Remark" runat="server" CssClass="form-control c-height" placeholder="Description"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        
                                        
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="c-width-200p">
                                            <ItemTemplate>
                                                <asp:Button ID="Btn_Edit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn bg-purple c-bg-blueish btn-flat c-btn-height c-btn-width-47" />                                                
                                                     <asp:Button ID="Btn_Delete" runat="server" Text="Delete" CssClass="btn bg-purple c-bg-blueish btn-flat c-btn-height c-btn-width-47" 
                                                          CommandName="Delete" CommandArgument='<%#Eval("MaterialCategoryId") %>' OnClientClick="return confirm('Do you want to delete this type?')" OnClick="Btn_Delete_Click" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="Btn_Update" runat="server" Text="Update" CommandName="Update" CssClass="btn bg-purple c-bg-blueish btn-flat c-btn-height c-btn-width-47" />
                                                <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn bg-purple c-bg-blueish btn-flat c-btn-height c-btn-width-47" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="Btn_Add" runat="server" CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-btn-height" CommandName="Add" Text="Add" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                          
                            </div>
                        </div>
                    </div>
                </div>
                <%--content end--%>                
            </div>
            <FT:Footer runat="server" ID="FT_1" />
        </div>
    </form>
</body>
</html>
