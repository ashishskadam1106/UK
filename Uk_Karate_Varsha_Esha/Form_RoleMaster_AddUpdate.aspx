<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_RoleMaster_AddUpdate.aspx.cs" Inherits="Form_RoleMaster_AddUpdate" %>

<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Krupa ERP</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />


</head>
    
<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />

            <%--content starts--%>
            <div class="content-wrapper">
               <div class="box box-solid box-info c-border-blueish c-container-box"  style="margin-bottom:0">
                    <div class="box-header c-box-blueish-new with-border"  >
                       <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Add Role"></asp:Label> 
                    </div>
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Lbl_Role" runat="server" CssClass="Label" Text="Role Name"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <asp:TextBox ID="Tb_RoleName" runat="server" CssClass="form-control" placeholder="Role Name" required="required" 
                                     oninvalid="this.setCustomValidity('Please enter role name')" oninput="this.setCustomValidity('')"></asp:TextBox>
                            </div>
                        </div>

                         <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Lbl_Remark" runat="server" CssClass="Label" Text="Remark"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <asp:TextBox ID="Tb_Description" runat="server" CssClass="form-control c-tb-noresize" placeholder="Description" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-12 col-md-offset-2 c-button-box">
                            
                            <div class="col-md-10 no-padding">
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Save" runat="server" Text="Save" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_Save_Click" />
                                </div>
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Back" runat="server" Text="Back" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Back_Click" />
                                </div>
                                  <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_New" runat="server" Visible="false" Text="New" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_New_Click" />
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
