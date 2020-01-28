<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StudentHeader.ascx.cs" Inherits="User_Control_StudentHeader" %>
<header class="main-header">

       <ul class="nav navbar-nav">
                    <li >  <img style="width:70px; height:60px" src="dist/img/mainlogo1.jpg" alt="mainlogo"></li>
                    <li><a style="font-size:15px" class="logo" href="Home.aspx"><b>UK Karate- Do Goju-Kai</b></a></li>
                  
                </ul>       
             
    <nav class="navbar navbar-static-top">
        <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
            <span class="sr-only">Toggle navigation</span>
        </a>
        <div class="navbar-custom-menu">
            <ul class="nav navbar-nav">
                <%--for user start--%>
                <li class="dropdown user user-menu">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <div>
                            <asp:Image runat="server" ID="Img_UserProfilePic_Small" class="user-image" alt="User Image" />
                            <asp:Label runat="server" ID="Lbl_UserName_Small">Not Available</asp:Label>
                        </div>
                    </a>
                    <ul class="dropdown-menu">
                        <!-- User image -->
                        <li class="user-header" style="height: 185px">
                            <asp:Image runat="server" ID="Img_UserProfilePic_Large" class="img-circle" alt="User Image" />
                            <p>
                                <asp:Label runat="server" CssClass="Label" ID="Lbl_UserName_Large">Not Available</asp:Label>
                                <br />
                              
                                <asp:Label runat="server" CssClass="Label" Font-Size="14px" ID="Lbl_EmpEmailId">Not Available</asp:Label>
                            </p>
                        </li>
                        <li class="user-footer">
                            <div class="pull-left">
                            </div>
                            <div class="pull-right">
                                <a href="Form_Logout.aspx" class="btn btn-default btn-flat"><i class="fa  fa-sign-out text-red"></i>  Sign out</a>
                            </div>
                        </li>
                    </ul>
                </li>
                <%--<li>
                    <a href="#" data-toggle="control-sidebar"><i class="fa fa-gears"></i></a>
                </li>--%>
            </ul>
        </div>
    </nav>

</header>