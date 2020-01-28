<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Dashboard1.ascx.cs" Inherits="User_Control_Dashboard1" %>

<%--<style>
    .OrderTable td {
        padding: 8px !important;
        line-height: 1.42857143;
        vertical-align: top;
    }
</style>--%>

<script src="../JS/DashBoard/bootstrap.min.js"></script>
<script src="../JS/DashBoard/Chart.min.js"></script>
<script src="../JS/DashBoard/fastclick.min.js"></script>
<script src="../JS/DashBoard/jquery-2.2.3.min.js"></script>
<link href="../JS/DashBoard/blue.css" rel="stylesheet" />
<script src="../JS/DashBoard/app.min.js"></script>
<style>
    .skin-blue .content-header {
        background: transparent;
    }

    .content-header {
        position: relative;
        padding: 15px 15px 0 15px;
    }

        .content-header > .breadcrumb {
            float: right;
            background: transparent;
            margin-top: 0;
            margin-bottom: 0;
            font-size: 12px;
            padding: 7px 5px;
            position: absolute;
            top: 15px;
            right: 10px;
            border-radius: 2px;
            list-style: none;
        }

    .row {
        margin-right: -15px;
        margin-left: -15px;
    }

    .small-box {
        border-radius: 2px;
        position: relative;
        display: block;
        margin-bottom: 20px;
        box-shadow: 0 1px 1px rgba(0,0,0,0.1);
        color: #fff !important;
    }

        .small-box > .inner {
            padding: 10px;
        }

        .small-box .icon {
            -webkit-transition: all .3s linear;
            -o-transition: all .3s linear;
            transition: all .3s linear;
            position: absolute;
            top: -10px;
            right: 10px;
            z-index: 0;
            font-size: 90px;
            color: rgba(0,0,0,0.15);
        }

        .small-box > .small-box-footer {
            position: relative;
            text-align: center;
            padding: 3px 0;
            color: #fff;
            color: rgba(255,255,255,0.8);
            display: block;
            z-index: 10;
            background: rgba(0,0,0,0.1);
            text-decoration: none;
        }

    .ion {
        display: inline-block;
        font-family: "Ionicons";
        speak: none;
        font-style: normal;
        font-weight: normal;
        font-variant: normal;
        text-transform: none;
        text-rendering: auto;
        line-height: 1;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
    }

    .bg-aqua {
        background-color: #00c0ef !important;
        color: #fff !important;
    }
</style>
<style>
    .ion-bag:before {
        content: "\f110";
    }
</style>
<style>
  table>tbody>tr>td
{
	padding:5px !Important;
}
</style>
<%--added Later--%>
<%--<div class="box box-primary"> 
            <div class="box-header">
              <i class="ion ion-clipboard"></i>

              <h3 class="box-title">To Do List</h3>

              <div class="box-tools pull-right">
                <ul class="pagination pagination-sm inline">
                  <li><a href="#">&laquo;</a></li>
                  <li><a href="#">1</a></li>
                  <li><a href="#">2</a></li>
                  <li><a href="#">3</a></li>
                  <li><a href="#">&raquo;</a></li>
                </ul>
              </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <ul class="todo-list">
                <li>
                  <!-- drag handle -->
                      <span class="handle">
                        <i class="fa fa-ellipsis-v"></i>
                        <i class="fa fa-ellipsis-v"></i>
                      </span>
                  <!-- checkbox -->
                  <input type="checkbox" value="">
                  <!-- todo text -->
                  <span class="text">Design a nice theme</span>
                  <!-- Emphasis label -->
                  <small class="label label-danger"><i class="fa fa-clock-o"></i> 2 mins</small>
                  <!-- General tools such as edit or delete-->
                  <div class="tools">
                    <i class="fa fa-edit"></i>
                    <i class="fa fa-trash-o"></i>
                  </div>
                </li>
                <li>
                      <span class="handle">
                        <i class="fa fa-ellipsis-v"></i>
                        <i class="fa fa-ellipsis-v"></i>
                      </span>
                  <input type="checkbox" value="">
                  <span class="text">Make the theme responsive</span>
                  <small class="label label-info"><i class="fa fa-clock-o"></i> 4 hours</small>
                  <div class="tools">
                    <i class="fa fa-edit"></i>
                    <i class="fa fa-trash-o"></i>
                  </div>
                </li>
                <li>
                      <span class="handle">
                        <i class="fa fa-ellipsis-v"></i>
                        <i class="fa fa-ellipsis-v"></i>
                      </span>
                  <input type="checkbox" value="">
                  <span class="text">Let theme shine like a star</span>
                  <small class="label label-warning"><i class="fa fa-clock-o"></i> 1 day</small>
                  <div class="tools">
                    <i class="fa fa-edit"></i>
                    <i class="fa fa-trash-o"></i>
                  </div>
                </li>
                <li>
                      <span class="handle">
                        <i class="fa fa-ellipsis-v"></i>
                        <i class="fa fa-ellipsis-v"></i>
                      </span>
                  <input type="checkbox" value="">
                  <span class="text">Let theme shine like a star</span>
                  <small class="label label-success"><i class="fa fa-clock-o"></i> 3 days</small>
                  <div class="tools">
                    <i class="fa fa-edit"></i>
                    <i class="fa fa-trash-o"></i>
                  </div>
                </li>
                <li>
                      <span class="handle">
                        <i class="fa fa-ellipsis-v"></i>
                        <i class="fa fa-ellipsis-v"></i>
                      </span>
                  <input type="checkbox" value="">
                  <span class="text">Check your messages and notifications</span>
                  <small class="label label-primary"><i class="fa fa-clock-o"></i> 1 week</small>
                  <div class="tools">
                    <i class="fa fa-edit"></i>
                    <i class="fa fa-trash-o"></i>
                  </div>
                </li>
                <li>
                      <span class="handle">
                        <i class="fa fa-ellipsis-v"></i>
                        <i class="fa fa-ellipsis-v"></i>
                      </span>
                  <input type="checkbox" value="">
                  <span class="text">Let theme shine like a star</span>
                  <small class="label label-default"><i class="fa fa-clock-o"></i> 1 month</small>
                  <div class="tools">
                    <i class="fa fa-edit"></i>
                    <i class="fa fa-trash-o"></i>
                  </div>
                </li>
              </ul>
            </div>
            <!-- /.box-body -->
            <div class="box-footer clearfix no-border">
              <button type="button" class="btn btn-default pull-right"><i class="fa fa-plus"></i> Add item</button>
            </div>
          </div>--%>

<div>
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Dashboard
       
            <%--<small>Control panel</small>--%>
      </h1>
        <%--<ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Dashboard</li>
      </ol>--%>
    </section>

    <!-- Main content -->
    <section class="content">
        <!-- Small boxes (Stat box) -->
        <div class="row" style="margin-bottom: 10px;">
            <div class="col-lg-3 col-xs-6">
                <asp:DropDownList ID="Dd_Duration" runat="server" CssClass="form-control" OnSelectedIndexChanged="Dd_Duration_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="1">Today</asp:ListItem>
                    <asp:ListItem Value="2">This Week</asp:ListItem>
                    <asp:ListItem Value="3">This Month</asp:ListItem>
                    <asp:ListItem Value="4">Quarter</asp:ListItem>
                    <asp:ListItem Value="5">Half Year</asp:ListItem>
                    <asp:ListItem Value="6">Year</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-3 col-xs-6" style="padding-top: 2px;">
                <asp:Label ID="Lbl_DataDate" runat="server" Text="" CssClass="success"></asp:Label>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-3 col-xs-6">
                <!-- small box -->
                <div class="small-box bg-aqua">
                    <div class="inner">
                        <h3 runat="server" id="Lbl_NewStudents">0</h3>

                        <p>New Students</p>
                    </div>
                    <div class="icon">
                       <i class="fa fa-user-plus"></i>
                    </div>
                    <%--<div class="icon">
              <i class="ion ion-bag"></i>
            </div>--%>
                    <a class="small-box-footer" data-toggle="modal" data-target="#Modal_Student" style="cursor: pointer">More info <i class="fa fa-arrow-circle-right"></i></a>
                </div>
            </div>
            <!-- ./col -->
            <div class="col-lg-3 col-xs-6">
                <!-- small box -->
                <div class="small-box bg-green">
                    <div class="inner">
                        <h3 runat="server" id="Lbl_StudentDojoBalance">0</h3>

                        <p>Student Balance</p>
                    </div>
                    <div class="icon">
                        <i class="fa fa-pencil-square-o"></i>
                    </div>
                    <%--<div class="icon">
              <i class="ion ion-stats-bars"></i>
            </div>--%>
                    <a class="small-box-footer" data-toggle="modal" data-target="#Modal_StdentBalance" style="cursor:pointer;">More info <i class="fa fa-arrow-circle-right"></i></a>
                </div>
            </div>
            <!-- ./col -->
            <div class="col-lg-3 col-xs-6">
                <!-- small box -->
                <div class="small-box bg-yellow">
                    <div class="inner">
                        <h3 runat="server" id="Lbl_ReceiptsGenerated">0</h3>

                        <p>Test</p>
                    </div>
                    <div class="icon">
                        <i class="fa fa-rupee"></i>
                    </div>
                    <%--<div class="icon">
              <i class="ion ion-person-add"></i>
            </div>--%>
                    <a class="small-box-footer" data-toggle="modal" data-target="#Modal_ReceiptsGenerated" style="cursor:pointer;">More info <i class="fa fa-arrow-circle-right"></i></a>
                </div>
            </div>
            <!-- ./col -->
            <div class="col-lg-3 col-xs-6">
                <!-- small box -->
                <div class="small-box bg-red">
                    <div class="inner">
                        <h3 runat="server" id="Lbl_NewCustomers">0</h3>

                        <p>Test</p>
                    </div>
                    <div class="icon">
                        <i class="fa fa-user-plus"></i>
                    </div>
                    <%--<div class="icon">
              <i class="ion ion-pie-graph"></i>
            </div>--%>
                    <a class="small-box-footer" data-toggle="modal" data-target="#Modal_NewCustomers" style="cursor:pointer">More info <i class="fa fa-arrow-circle-right"></i></a>
                </div>
            </div>
            <!-- ./col -->
        </div>

        <%--modal 1 Vehicles--%>
        <div class="modal fade" id="Modal_Student" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">New Students</h4>
                    </div>
                    <div class="modal-body">
                        <div class="table-responsive no-padding c-grid-container c-inline-space">
                            <asp:GridView runat="server" ID="Gv_Student" CssClass="table"
                                CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px" EmptyDataText="No data to show"
                                HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                DataKeyNames="StudentId" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" 
                                PagerStyle-CssClass="c-paging" OnPageIndexChanging="Gv_Student_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="No." HeaderStyle-Width="39px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Label1" Text='<%#Eval("SrNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Student">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl2" Text='<%#Eval("StudentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Membership Number ">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl3" Text='<%#Eval("MembershipNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Membership Date">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl6" Text='<%#Eval("MembershipDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                       
                                </Columns>

                            </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <%--end modal 1 Vehicles--%>

        <%--modal 2 Bills--%>
        <div class="modal fade" id="Modal_StdentBalance" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Student Balance</h4>
                    </div>
                    <div class="modal-body">
                        <div class="table-responsive no-padding c-grid-container c-inline-space">
                            <asp:GridView runat="server" ID="Gv_StudentBalance" CssClass="table"
                                CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                DataKeyNames="StudentId" EmptyDataText="No data to show"
                                AutoGenerateColumns="false" AllowPaging="true" PageSize="15" 
                                PagerStyle-CssClass="c-paging" OnPageIndexChanging="Gv_StudentBalance_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="No." HeaderStyle-Width="39px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Label1" Text='<%#Eval("SrNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Membership Number">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl2" Text='<%#Eval("MembershipNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl3" Text='<%#Eval("FullName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Membership Date">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl5" Text='<%#Eval("MembershipDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                      <asp:TemplateField HeaderText="Dojo Code">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl3" Text='<%#Eval("DojoCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Amount Paid">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl6" Text='<%#Eval("AmountPaid","{0:00.00}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>    
                                    
                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl7" Text='<%#Eval("Balance","{0:00.00}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                       
                                </Columns>

                            </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <%--end modal 2 Bills--%>

        <%--modal 3 Receipts--%>
        <div class="modal fade" id="Modal_ReceiptsGenerated" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Test</h4>
                    </div>
                    <div class="modal-body">
                        <div class="table-responsive no-padding c-grid-container c-inline-space">
                            <%--<asp:GridView runat="server" ID="Gv_Receipts" CssClass="table"
                                CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                DataKeyNames="ReceiptHeader_Id" EmptyDataText="No data to show"
                                AutoGenerateColumns="false" AllowPaging="true" PageSize="15" 
                                PagerStyle-CssClass="c-paging" OnPageIndexChanging="Gv_Receipts_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="No." HeaderStyle-Width="39px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Label1" Text='<%#Eval("SrNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl2" Text='<%#Eval("Customer") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl3" Text='<%#Eval("Receipt_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Payment Mode">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl5" Text='<%#Eval("Payment_Mode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    
                                     <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl6" Text='<%#Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  

                                     <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl7" Text='<%#Eval("ReceiptStatus") %>' CssClass='<%#Eval("ReceiptClass")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                       
                                </Columns>

                            </asp:GridView>--%>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <%--end modal 3 Receipts--%>

        <%--modal 4 New Customers --%>
        <div class="modal fade" id="Modal_NewCustomers" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Test</h4>
                    </div>
                    <div class="modal-body">
                        <div class="table-responsive no-padding c-grid-container c-inline-space">
                           <%-- <asp:GridView runat="server" ID="Gv_NewCustomers" CssClass="table"
                                CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                DataKeyNames="Customer_Id" EmptyDataText="No data to show"
                                AutoGenerateColumns="false" 
                                AllowPaging="true" PageSize="15" PagerStyle-CssClass="c-paging" OnPageIndexChanging="Gv_NewCustomers_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="No." HeaderStyle-Width="39px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Label1" Text='<%#Eval("SrNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Number" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl2" Text='<%#Eval("Customer_Number") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl3" Text='<%#Eval("Customer") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Customer Address">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl5" Text='<%#Eval("Customer_Address") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                    
                                     <asp:TemplateField HeaderText="Contact">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Lbl6" Text='<%#Eval("Contact_Mobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                       
                                </Columns>

                            </asp:GridView>--%>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <%--modal end--%>

        <!----!>
      <!-- /.row -->
    </section>

    <!-- /.content -->
</div>










