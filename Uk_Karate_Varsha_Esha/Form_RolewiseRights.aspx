<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_RolewiseRights.aspx.cs" Inherits="Form_RolewiseRights" %>

<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UK Karate</title>
    
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
        #Chk_RoleMaster >tbody>tr>td> label {
            padding-left:5px;
        }
        #Chk_MenuMaster>tbody>tr>td> label {
            padding-left:5px;
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
                    <div class="box-header c-box-blueish-new with-border">
                        <h3 class="box-title">Assign Privileges</h3>
                    </div>
                     <div class="box-body c-padding-top-2">
                        <div class="col-md-12">
                            <div class="col-md-2 c-label-1">
                                  <asp:Label ID="Lb_RightType" runat="server" class="Label">Privilege Type</asp:Label>
                            </div>
                            <div class="col-md-10 no-padding">
                               <asp:DropDownList ID="Dd_RightType" runat="server" class="form-control"
                                       DataTextField="Right_Name" DataValueField="Right_Type_Id" 
                                       AutoPostBack="true" OnSelectedIndexChanged="Dd_RightType_SelectedIndexChanged"  >
                                  </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space"> 
                            <div class="col-md-5">
                                <asp:Label ID="Label1" runat="server" class="Label"><h4>Roles</h4></asp:Label>
                                <asp:CheckBoxList ID="Chk_RoleMaster" runat="server" RepeatLayout="Table" DataTextField="Role_Name" Width="100%"
                                    DataValueField="Role_Id" AutoPostBack="True" OnSelectedIndexChanged="Chk_RoleMaster_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </div>
                            <div class="col-md-7">
                                <asp:Label ID="Label2" runat="server" class="Label"><h4>Privilege Rights</h4></asp:Label>
                                <asp:CheckBoxList ID="Chk_MenuMaster" runat="server" RepeatLayout="Table" DataTextField="Menu_Name" Width="100%"
                                    DataValueField="Menu_Id" AutoPostBack="True" OnSelectedIndexChanged="Chk_MenuMaster_SelectedIndexChanged">
                                </asp:CheckBoxList>

                            </div>
                        </div>

                          <div class="col-md-12 col-md-offset-2 c-button-box">
                            
                            <div class="col-md-10 no-padding">
                                <div class="col-md-4 c-btn-widht-25">
                                     <asp:Button  CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" ID="Btn_Save" runat="server" Text="Save" OnClick="Btn_Save_Click" />
                                </div>

                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button  CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" ID="Btn_Cancel" runat="server" Text="Cancel" OnClick="Btn_Cancel_Click"/>
                                </div>

                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
          <%--content end--%>

        <FT:Footer runat="server" ID="FT_1" />

    </form>
</body>
</html>
