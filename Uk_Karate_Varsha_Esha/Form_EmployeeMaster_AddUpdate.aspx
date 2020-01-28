<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_EmployeeMaster_AddUpdate.aspx.cs" Inherits="Form_EmployeeMaster_AddUpdate" %>

<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UK_Karate</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />

    <%--select 2 start--%>
    <!-- Select2 -->
    <script src="plugins/select2/select2.full.min.js"></script>
    <!-- Select2 -->
    <link rel="stylesheet" href="plugins/select2/select2.min.css" />
    <script>
        $(function () {
            //Initialize Select2 Elements
            $(".select2").select2();
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $('#Dp_DBSRenewalDate').datepicker({
                //dateFormat: 'dd-mm-yy'
            });
        });
        $(function () {
            $('#Dp_InsuranceRenewalDate').datepicker({
                //dateFormat: 'dd-mm-yy'
            });
        });

    </script>

    <style>
        .select2-container--default .select2-selection--single {
            border: 1px solid #d2d6de;
            border-radius: 0px;
            height: 34px;
        }
    </style>

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
        /*.c-dd-height {
        height:30px;
        
        }*/
        .c-width-46 {
            width: 46%;
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

            .c-width-46 {
                width: 100%;
            }
        }
    </style>

    <%--again add select2 to dropdown class--%>
    <%--select 2 end--%>
</head>

<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />

            <%--content starts--%>
            <div class="content-wrapper">
                <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">

                    <div class="box-header c-box-blueish-new with border">
                        <asp:Label ID="Lbl_Header" runat="server" CssClass="box-title" Text="Add Instructor"></asp:Label>
                    </div>
                    <asp:HiddenField ID="Hf_InstructorId" runat="server" Value="0" />
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Lbl_FName" runat="server" CssClass="Label" Text="First Name*"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-2 c-col-size-20">
                                    <asp:TextBox ID="Tb_FirstName" runat="server" CssClass="form-control c-tb-noresize" placeholder="First Name" required="required"
                                        oninvalid="this.setCustomValidity('Please enter First name')" oninput="this.setCustomValidity('')"></asp:TextBox>
                                </div>
                                <div class="col-md-2 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Lbl_MName" runat="server" CssClass="Label" Text="Middle Name"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-20">
                                    <asp:TextBox ID="Tb_MiddleName" runat="server" CssClass="form-control c-tb-noresize" placeholder="Middle Name"></asp:TextBox>
                                </div>
                                <div class="col-md-2 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Lbl_LName" runat="server" CssClass="Label" Text="Last Name*"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-20">
                                    <asp:TextBox ID="Tb_LastName" runat="server" CssClass="form-control c-tb-noresize" placeholder="Last Name" required="required"
                                        oninvalid="this.setCustomValidity('Please enter Last name')" oninput="this.setCustomValidity('')"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label1" CssClass="Label" runat="server" Text="Address Line 1"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <asp:TextBox ID="Tb_AddressLine1" runat="server" CssClass="form-control" placeholder="Address Line 1"></asp:TextBox>
                            </div>

                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label2" CssClass="Label" runat="server" Text="Address Line 2"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <asp:TextBox ID="Tb_AddressLine2" runat="server" CssClass="form-control" placeholder="Address Line 2"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label3" CssClass="Label" runat="server" Text="City"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <div class="col-md-2 c-col-size-43">
                                    <%--<asp:DropDownList ID="DD_City" runat="server" CssClass="form-control select2 "
                                        DataTextField="City" DataValueField="City_Id" style="width:100%"></asp:DropDownList>--%>
                                    <asp:TextBox ID="Tb_City" runat="server" CssClass="form-control" placeholder="City"></asp:TextBox>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label4" CssClass="Label" runat="server" Text="Post Code"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                    <asp:TextBox ID="Tb_PostalCode" runat="server" CssClass="form-control" placeholder="Post Code"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label5" CssClass="Label" runat="server" Text="Contact Mobile"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <div class="col-md-2 c-col-size-43">
                                    <asp:TextBox ID="Tb_ContactMobile" runat="server" CssClass="form-control" placeholder="Mobile Number" TextMode="Number"></asp:TextBox>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label6" CssClass="Label" runat="server" Text="Email ID"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                    <asp:TextBox ID="Tb_EmailID" runat="server" CssClass="form-control" placeholder="Email Address" TextMode="Email"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label7" CssClass="Label" runat="server" Text="Role *"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <div class="col-md-2 c-col-size-43">
                                    <asp:DropDownList ID="Dd_Role" runat="server" CssClass="form-control" DataTextField="Role_Name" DataValueField="Role_Id"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label8" CssClass="Label" runat="server" Text="Working Status"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                    <asp:DropDownList ID="DD_EmployeeStatus" runat="server" class="form-control">
                                        <asp:ListItem Value="0">Suspend</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label9" CssClass="Label" runat="server" Text="User Name*"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <div class="col-md-2 c-col-size-43">
                                    <asp:TextBox ID="Tb_UserName" runat="server" CssClass="form-control" placeholder="User Name/Login ID" required="required"
                                        oninput="this.setCustomValidity('')" oninvalid="this.setCustomValidity('Please enter User Name')"></asp:TextBox>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Lbl_Password" CssClass="Label" runat="server" Text="Password *"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                    <asp:TextBox ID="Tb_Password" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password" required="required"
                                        oninput="this.setCustomValidity('')" oninvalid="this.setCustomValidity('Please enter Password')"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Lbl_Upload_Photo" runat="server" CssClass="Label" Text="Upload Photo"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <div class="col-md-4 c-col-size-43">
                                    <asp:FileUpload ID="Fu_EmployeeImgae" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label10" CssClass="Label" runat="server" Text="Gender"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                    <asp:DropDownList ID="DD_Gender" runat="server" class="form-control">
                                        <asp:ListItem Value="0">Male</asp:ListItem>
                                        <asp:ListItem Value="1">Female</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Reporting Manager"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <div class="col-md-4 c-col-size-43">
                                    <asp:DropDownList ID="Dd_ReportingManager" runat="server" class="form-control" DataTextField="Employee_Name" DataValueField="Employee_Id"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Dojo"></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                    <asp:DropDownList ID="DdDojo" runat="server" class="form-control" DataTextField="DojoCode" DataValueField="DojoId"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Lbl_DBS" runat="server" CssClass="Label" Text="DBS" ></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <div class="col-md-4 c-col-size-43">
                                    <asp:FileUpload ID="DBSFile" runat="server" CssClass="form-control"  />
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Lbl_DBSRenewalDate" runat="server" CssClass="Label" Text="DBS Renewal Date" ></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                    <div class="input-group date">
                                        <div id="Div_DBSRenewalDate" class="input-group-addon"  runat="server">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control pull-right"
                                            ID="Dp_DBSRenewalDate" data-date-format="dd/mm/yyyy" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Lbl_Insurance" runat="server" CssClass="Label" Text="Insurance" ></asp:Label>

                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <div class="col-md-4 c-col-size-43">
                                    <asp:FileUpload ID="Fu_Insurance" runat="server" CssClass="form-control"  />
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Lbl_InsuranceRenewalDate" runat="server" CssClass="Label" Text="Insurance Renewal  Date" ></asp:Label>
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                    <div id="Div_InsuranceRenewalDate" class="input-group date"  runat="server">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control pull-right"
                                            ID="Dp_InsuranceRenewalDate" data-date-format="dd/mm/yyyy" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label14" CssClass="Label" runat="server" Text="Is Instructor" Visible="false"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86 no-padding">
                                <div class="col-md-4 c-col-size-43">
                                    <asp:CheckBox ID="Chb_IncludeInOtherCharges" runat="server" value="checked" AutoPostBack="true" OnCheckedChanged="Chb_IncludeInOtherCharges_CheckedChanged" Enabled="false" Visible="false" />
                                </div>
                                <div class="col-md-2 c-col-size-14 c-label-1 c-padding-left-1">
                                </div>
                                <div class="col-md-2 c-col-size-43">
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-md-offset-2 c-button-box">
                            <div class="col-md-10 no-padding">
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Save" runat="server" Text="Save" CssClass="btn btn-primary bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_Save_Click" />
                                </div>
                             
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Back" runat="server" Text="Back" CssClass="btn btn-primary bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Back_Click" />
                                </div>
                                   <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_New" runat="server" Text="New" CssClass="btn btn-primary bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_New_Click"  Visible
                                        ="false"/>
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
