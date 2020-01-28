<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RightSideBar.ascx.cs" Inherits="User_Control_RightSideBar" %>

<!-- Control Sidebar -->
  <aside class="control-sidebar control-sidebar-dark">
    <!-- Create the tabs -->
    <ul class="nav nav-tabs nav-justified control-sidebar-tabs">
      <li><a href="#control-sidebar-unitview-tab" data-toggle="tab"><i class="fa fa-home"></i></a></li>
    </ul>
    <!-- Tab panes -->
    <div class="tab-content">
      <!-- Home tab content -->
      <div class="tab-pane active" id="control-sidebar-unitview-tab">
        <div>
            <asp:TreeView ID="Society_TreeView" runat="server" NodeIndent="15" ExpandImageUrl="~/Icon/sort_right.png" CollapseImageUrl="~/Icon/sort_down.png">
                <%--<RootNodeStyle><i class="fa fa-amazon fa-3x active"></i></RootNodeStyle>--%>
              <%--  <RootNodeStyle ImageUrl="~/Icon/H.png" />
                <ParentNodeStyle ImageUrl="~/Icon/H2.jpg" />
                <LeafNodeStyle ImageUrl="~/Icon/H.png" />--%>

                <HoverNodeStyle ForeColor="#6666AA" />
                <%--<NodeStyle Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                    NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>--%>
               
                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                    VerticalPadding="0px" />
                

            </asp:TreeView>

        </div>
    </div>
      <!-- /.tab-pane -->
    </div>
  </aside>
  <!-- /.control-sidebar -->
<div class="control-sidebar-bg"></div>