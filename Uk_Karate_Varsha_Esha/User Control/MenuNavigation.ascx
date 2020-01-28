<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuNavigation.ascx.cs" Inherits="User_Control_MenuNavigation" %>
<%--left menu starts--%>
<style>
    .sidebar {
        background: #fff9e6 !important; 
        opacity: 0.95 !important;
    
       
    }
    .main-sidebar {
       /*background-image:url('dist/img/karate3.jpg') !important;
        background-size: cover !important;
        background-position: top center !important; */
      
    }
    .sidebar-menu {
        opacity:1.0;
        
    }
</style>
<aside class="main-sidebar">
    <section class="sidebar">

        <div class="user-panel" style="margin-top:5px;margin-bottom:5px;">
            <div class="pull-left image">
              <%-- <asp:Image runat="server" ID="Img_UserProfilePic_InMenu" class="img-circle" alt="User Image"/>--%>
            </div>
            <div class="pull-left info">
                <%--<p><asp:Label runat="server" ID="Lbl_UserName_InMenu">Not Available</asp:Label></p>--%>
                <%--<a href="#"><i class="fa fa-circle text-success"></i>Logged In</a>--%>
                <a href="#"><asp:Label runat="server" ID="lblServiceCentreName"></asp:Label></a>
            </div>
        </div>
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
    </section>
</aside>

<%--left menu ends--%>