<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_UserwiseRights.aspx.cs" Inherits="Form_UserwiseRights" %>

<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Krupa ERP</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />
    <style>
       #Chk_MenuMaster tr {
            width: 200px;
            float: left;
        }
        td {
            border:0px;
        }
        .col-md-5, .col-md-7 {
            padding: 1%;
            border: 1px solid #ccc;
            background-color: white;
        }
        #Chk_User >tbody>tr>td> label {
            padding-left:5px;
        }
        #Chk_ProcessMaster >tbody>tr>td> label {
            padding-left:5px;
        }
        td {
            padding-left:5px;
            padding-right:5px;
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
                <div class="box  box-solid box-info c-border-blueish  c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish-new with-border">
                        <h3 class="box-title">Assign Permissions</h3>
                    </div>
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12">
                            <div class="col-md-2 c-label-1">
                                <asp:Label ID="Lb_RightType" runat="server" class="Label">Permission Type</asp:Label>
                            </div>
                            <div class="col-md-10 no-padding">
                                <asp:DropDownList ID="Dd_RightType" runat="server" class="form-control"
                                    DataTextField="Right_Name" DataValueField="Right_Type_Id"
                                    AutoPostBack="true" OnSelectedIndexChanged="Dd_RightType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-5">
                                <asp:Label ID="Label3" runat="server" class="Label"><h4>Users</h4></asp:Label>
                                <asp:CheckBoxList ID="Chk_User" runat="server" RepeatLayout="Table" DataTextField="Employee_Name" Width="100%"
                                    DataValueField="Authentication_Id" AutoPostBack="True" OnSelectedIndexChanged="Chk_User_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </div>
                            <div class="col-md-7">
                                <asp:Label ID="Label4" runat="server" class="Label"><h4>Permission Rights</h4></asp:Label>
                                <asp:CheckBoxList ID="Chk_ProcessMaster" runat="server" RepeatLayout="Table" DataTextField="Process_Name" Width="100%"
                                    DataValueField="ProcessMaster_Id" AutoPostBack="True">
                                </asp:CheckBoxList>

                            </div>
                        </div>


                        <div class="col-md-12 col-md-offset-2 c-button-box">

                            <div class="col-md-10 no-padding">
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" ID="Btn_Save" runat="server" Text="Save"  OnClick="Btn_Save_Click"/>
                                </div>

                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" ID="Btn_Cancel" runat="server" Text="Cancel" OnClick="Btn_Cancel_Click" />
                                </div>

                            </div>
                        </div>
                        
                        <div class="col-md-12 c-inline-space">
                                <asp:Label ID="Label2" runat="server" class="Label"><h4>Rights Description</h4></asp:Label>
                                <asp:CheckBoxList ID="Chk_RightsDescription" runat="server" RepeatLayout="Table" 
                                    DataTextField="Remark" Enabled="false" style="border:1px solid #ccc; padding:5px;"
                                    DataValueField="ProcessMaster_Id">
                                </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            

        <FT:Footer runat="server" ID="FT1"/>
    </form>
</body>
</html>
