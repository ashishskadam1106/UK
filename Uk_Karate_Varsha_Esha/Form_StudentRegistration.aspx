<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_StudentRegistration.aspx.cs" Inherits="Form_StudentRegistration" %>


<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UK Karate</title>
    <script src="plugins/crafty_postcode.class.js"></script>
    <DL:DefaultLinks runat="server" ID="DL_1" />
    <style>
        .ui-datepicker-year {
            display: none;
        }

        .select2 {
            width: 100%;
        }

        .c-label-right {
            float: right;
            margin-right: 5px;
        }

        .c-col-size-12 {
            width: 11%;
            padding-left: 0px;
            padding-right: 2px;
        }

        .c-col-size-15 {
            width: 15%;
            padding-left: 0px;
            padding-right: 0px;
        }

        /*.c-col-size-16 {
            width: 16%;
            padding-left: 0px;
            padding-right: 0px;
        }*/

        .c-col-size-23 {
            width: 29%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-30 {
            width: 34.5%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-20 {
            width: 20%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-18 {
            width: 20%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-10 {
            width: 20%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-40 {
            width: 40%;
            padding-left: 0px;
            padding-right: 0px;
        }

        .c-col-size-80 {
            width: 60%;
            padding-left: 0px;
            padding-right: 1px;
        }

        .griddropdown {
            height: 28px !important;
            margin-top: 2px !important;
            margin-bottom: 2px !important;
        }

        .c-height {
            padding: 4px 6px !important;
            height: 28px !important;
            width: 100% !important;
        }

        .c-padding-left-4 {
            padding-left: 4% !important;
        }

        .c-col-size-50 {
            width: 50%;
            padding-left: 0px;
            padding-right: 0px;
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

            .c-col-size-50 {
                width: 50%;
                padding-left: 0px;
                padding-right: 0px;
            }

            .c-width-46 {
                width: 100%;
            }

            #Btn_Refresh {
                margin-top: 1%;
                margin-left: 20%;
                width: 60%;
            }

            #Btn_ReceiptRefresh {
                margin-top: 1%;
                margin-left: 20%;
                width: 60%;
            }

            .c-col-size-12 {
                width: 50%;
                padding-left: 0px;
                padding-right: 0px;
                padding-bottom: 1%;
            }

            .griddropdown {
                height: 28px;
                margin-top: 2px;
                margin-bottom: 2px;
            }

            .c-padding-left-4 {
                padding-left: 4% !important;
            }
        }
    </style>

    <script type="text/javascript">
        function Addcustomclass() {
        }

        function myFunction() {
            var txt;
            var r = confirm("Do you want  to print Starter Pack?!");
            if (r == true) {

                var StudentId = document.getElementById("HF_StudentId_ToPrint").value;
                window.open('Reports/Form_PrintStarterpackfrontpage.aspx?ID=' + document.getElementById('HF_StudentId_ToPrint').value, '_blank');
                // window.location.replace('Reports/Form_PrintStarterpackfrontpage.aspx?ID=' + document.getElementById('HF_StudentId_ToPrint').value);
            } else {

                alert('Student Registered Successfully');
                // window.location= "Form_ManageStudents.aspx";
                window.open('Form_ManageStudents.aspx');
                //window.location.replace('Form_ManageStudents.aspx');
            }
        }


        function redirectPage() {
            alert('Student Information updated successfully');
            window.location.href = 'Form_ManageStudents.aspx';
            return false;
        }


        function GetChildControl(element, id) {
            var child_elements = element.getElementsByTagName("*");
            for (var i = 0; i < child_elements.length; i++) {
                if (child_elements[i].id.indexOf(id) != -1) {
                    return child_elements[i];
                }
            }
        }
        function AllocateAmount() {
            var Go = true;
            var ValidationMsg = "";
            var ReceivedAmount = 0;

            ReceivedAmount = document.getElementById("Tb_TotalPaidAmount").value;
            //alert('hi' + ReceivedAmount.toString());
            if (ReceivedAmount == "") {
                ValidationMsg += "Please enter Total Paid amount.\n";

                document.getElementById("Tb_TotalPaidAmount").style.borderColor = "Red";
                Go = false;
            }
            else if (isNaN(ReceivedAmount)) //if not a number
            {
                //alert('hi');
                ValidationMsg += "Plese enter valid Total Paid amount.\n";
                document.getElementById("Tb_TotalPaidAmount").style.borderColor = "Red";
                Go = false;
            } else {
                if (parseFloat(ReceivedAmount) < 0) {
                    ValidationMsg += "Total Paid amount should not negative.\n";
                    document.getElementById("Tb_TotalPaidAmount").style.borderColor = "Red";
                    Go = false;
                }

                else {
                    document.getElementById("Tb_TotalPaidAmount").style.borderColor = "LightGray";
                }
            }
            if (Go == true) {
                var tbl = $("[id$=Gv_FeeDetail]");

                var rows = tbl.find('tr');

                var RequiredFee = 0, PaidFee = 0, Balance = 0;

                for (var i = 1; i < rows.length; i++) {

                    var row = rows[i];

                    var Lbl_RequiredFee = GetChildControl(row, "Lbl_RequiredFee");
                    var Lbl_PaidFee = GetChildControl(row, "Lbl_PaidFee");
                    var Hf_PaidFee = GetChildControl(row, "Hf_PaidFee");


                    if (parseFloat(ReceivedAmount) != 0) {
                        if (ReceivedAmount <= parseFloat(Lbl_RequiredFee.innerText)) {
                            var BalanceToAssign = 0;

                            BalanceToAssign = ReceivedAmount;
                            //alert('sf ' + BalanceToAssign.toString());
                            Lbl_PaidFee.value = BalanceToAssign;
                            Lbl_PaidFee.innerText = BalanceToAssign;
                            Hf_PaidFee.value = BalanceToAssign;
                        }
                        else {
                            var BalanceToAssign = 0;
                            BalanceToAssign = Lbl_RequiredFee.innerText;
                            //alert('sf ' + Lbl_TillNowBalance.innerText.toString());
                            Lbl_PaidFee.value = BalanceToAssign;
                            Lbl_PaidFee.innerText = BalanceToAssign;
                            Hf_PaidFee.value = BalanceToAssign;
                        }
                    }
                    else {
                        Lbl_PaidFee.value = 0;
                    }

                    ReceivedAmount = parseFloat(ReceivedAmount) - parseFloat(Lbl_PaidFee.value);


                    Balance = parseFloat(Lbl_RequiredFee.innerText) - parseFloat(Lbl_PaidFee.value);


                }
                AllocateEnrollmentAmount();
                AllocateAnnualMembershipAmount();
            }

            else {
                //alert('hi');
                alert('Validation :\n' + ValidationMsg);
                //DoItCorrectFromReceivedAmount();
                document.getElementById("Tb_TotalPaidAmount").value = 0;
                AllocateAmount();
                return Go;
            }

        }



        function AllocateEnrollmentAmount() {
            var Go = true;
            var ValidationMsg = "";
            var ReceivedAmount = 0;

            ReceivedAmount = document.getElementById("Tb_EnrollmentFee").value;
            //alert('hi' + ReceivedAmount.toString());
            if (ReceivedAmount == "") {
                ValidationMsg += "Please enter Enrollment Fee amount.\n";

                document.getElementById("Tb_EnrollmentFee").style.borderColor = "Red";
                Go = false;
            }
            else if (isNaN(ReceivedAmount)) //if not a number
            {
                //alert('hi');
                ValidationMsg += "Plese enter valid Enrollment Fee amount.\n";
                document.getElementById("Tb_EnrollmentFee").style.borderColor = "Red";
                Go = false;
            } else {
                if (parseFloat(ReceivedAmount) < 0) {
                    ValidationMsg += "Enrollment Fee amount should not negative.\n";
                    document.getElementById("Tb_EnrollmentFee").style.borderColor = "Red";
                    Go = false;
                }

                else {
                    document.getElementById("Tb_EnrollmentFee").style.borderColor = "LightGray";
                }
            }
            if (Go == true) {
                var tbl = $("[id$=Gv_FeeDetail]");

                var rows = tbl.find('tr');

                var RequiredFee = 0, PaidFee = 0, Balance = 0;

                for (var i = 1; i < rows.length; i++) {

                    var row = rows[3];

                    var Lbl_RequiredFee = GetChildControl(row, "Lbl_RequiredFee");
                    var Hf_RequiredFee = GetChildControl(row, "Hf_RequiredFee");
                    if (parseFloat(ReceivedAmount) != 0) {
                        if (ReceivedAmount <= parseFloat(Lbl_RequiredFee.innerText)) {
                            var BalanceToAssign = 0;

                            BalanceToAssign = ReceivedAmount;
                            //alert('sf ' + BalanceToAssign.toString());
                            Lbl_RequiredFee.value = BalanceToAssign;
                            Lbl_RequiredFee.innerText = BalanceToAssign;
                            Hf_RequiredFee.value = BalanceToAssign;
                        }
                        else {

                        }
                    }
                }
            }
            else {
                //alert('hi');
                alert('Validation :\n' + ValidationMsg);
                //DoItCorrectFromReceivedAmount();
                document.getElementById("Tb_EnrollmentFee").value = 0;
                AllocateAmount();
                return Go;
            }

        }

        function AllocateAnnualMembershipAmount() {
            var Go = true;
            var ValidationMsg = "";
            var ReceivedAmount = 0;

            ReceivedAmount = document.getElementById("Tb_AnnualMembership").value;
            //alert('hi' + ReceivedAmount.toString());
            if (ReceivedAmount == "") {
                ValidationMsg += "Please enter Annual Membership Fee amount.\n";

                document.getElementById("Tb_AnnualMembership").style.borderColor = "Red";
                Go = false;
            }
            else if (isNaN(ReceivedAmount)) //if not a number
            {
                //alert('hi');
                ValidationMsg += "Plese enter valid Annual Membership Fee amount.\n";
                document.getElementById("Tb_AnnualMembership").style.borderColor = "Red";
                Go = false;
            } else {
                if (parseFloat(ReceivedAmount) < 0) {
                    ValidationMsg += "Annual Membership Fee amount should not negative.\n";
                    document.getElementById("Tb_AnnualMembership").style.borderColor = "Red";
                    Go = false;
                }

                else {
                    document.getElementById("Tb_AnnualMembership").style.borderColor = "LightGray";
                }
            }
            if (Go == true) {
                var tbl = $("[id$=Gv_FeeDetail]");

                var rows = tbl.find('tr');

                var RequiredFee = 0, PaidFee = 0, Balance = 0;

                for (var i = 1; i < rows.length; i++) {

                    var row = rows[1];

                    var Lbl_RequiredFee = GetChildControl(row, "Lbl_RequiredFee");
                    var Hf_RequiredFee = GetChildControl(row, "Hf_RequiredFee");
                    if (parseFloat(ReceivedAmount) != 0) {
                        if (ReceivedAmount <= parseFloat(Lbl_RequiredFee.innerText)) {
                            var BalanceToAssign = 0;

                            BalanceToAssign = ReceivedAmount;
                            //alert('sf ' + BalanceToAssign.toString());
                            Lbl_RequiredFee.value = BalanceToAssign;
                            Lbl_RequiredFee.innerText = BalanceToAssign;
                            Hf_RequiredFee.value = BalanceToAssign;
                        }
                        else {

                        }
                    }
                }
            }
            else {
                //alert('hi');
                alert('Validation :\n' + ValidationMsg);
                //DoItCorrectFromReceivedAmount();
                document.getElementById("Tb_AnnualMembership").value = 0;
                AllocateAmount();
                return Go;
            }

        }
        function AllocateMembershipAmount() {
            var Go = true;
            var ValidationMsg = "";
            var ReceivedAmount = 0;

            ReceivedAmount = document.getElementById("Tb_MembershipFee").value;
            //alert('hi' + ReceivedAmount.toString());
            if (ReceivedAmount == "") {
                ValidationMsg += "Please enter Membership Fee amount.\n";

                document.getElementById("Tb_MembershipFee").style.borderColor = "Red";
                Go = false;
            }
            else if (isNaN(ReceivedAmount)) //if not a number
            {
                //alert('hi');
                ValidationMsg += "Plese enter valid Membership Fee amount.\n";
                document.getElementById("Tb_MembershipFee").style.borderColor = "Red";
                Go = false;
            } else {
                if (parseFloat(ReceivedAmount) < 0) {
                    ValidationMsg += "Membership Fee amount should not negative.\n";
                    document.getElementById("Tb_MembershipFee").style.borderColor = "Red";
                    Go = false;
                }

                else {
                    document.getElementById("Tb_MembershipFee").style.borderColor = "LightGray";
                }
            }
            if (Go == true) {
                var tbl = $("[id$=Gv_FeeDetail]");

                var rows = tbl.find('tr');

                var RequiredFee = 0, PaidFee = 0, Balance = 0;

                for (var i = 1; i < rows.length; i++) {

                    var row = rows[2];

                    var Lbl_RequiredFee = GetChildControl(row, "Lbl_RequiredFee");
                    var Hf_RequiredFee = GetChildControl(row, "Hf_RequiredFee");
                    if (parseFloat(ReceivedAmount) != 0) {
                        if (ReceivedAmount <= parseFloat(Lbl_RequiredFee.innerText)) {
                            var BalanceToAssign = 0;

                            BalanceToAssign = ReceivedAmount;
                            //alert('sf ' + BalanceToAssign.toString());
                            Lbl_RequiredFee.value = BalanceToAssign;
                            Lbl_RequiredFee.innerText = BalanceToAssign;
                            Hf_RequiredFee.value = BalanceToAssign;
                        }
                        else {

                        }
                    }
                }
            }
            else {
                //alert('hi');
                alert('Validation :\n' + ValidationMsg);
                //DoItCorrectFromReceivedAmount();
                document.getElementById("Tb_MembershipFee").value = 0;
                AllocateAmount();
                return Go;
            }

        }

        function DVDAmount() {
            var Go = true;
            var ValidationMsg = "";
            var ReceivedAmount = 0, IsDVD = 0, DVDAmount = 0;

            DVDAmount = document.getElementById("HfDVD").value;

            IsDVD = document.getElementById("Chk_IsDVD").checked;
            if (IsDVD == true) {
                ReceivedAmount = DVDAmount;
            }
            else {
                ReceivedAmount = 0;
            }

            if (Go == true) {
                var tbl = $("[id$=Gv_FeeDetail]");

                var rows = tbl.find('tr');

                var RequiredFee = 0, PaidFee = 0, Balance = 0;

                for (var i = 1; i < rows.length; i++) {

                    var row = rows[6];

                    var Lbl_RequiredFee = GetChildControl(row, "Lbl_RequiredFee");
                    var Hf_RequiredFee = GetChildControl(row, "Hf_RequiredFee");
                    // if (parseFloat(ReceivedAmount) != 0) {
                    if (ReceivedAmount <= parseFloat(Lbl_RequiredFee.innerText)) {
                        var BalanceToAssign = 0;

                        BalanceToAssign = ReceivedAmount;
                        //alert('sf ' + BalanceToAssign.toString());
                        Lbl_RequiredFee.value = BalanceToAssign;
                        Lbl_RequiredFee.innerText = BalanceToAssign;
                        Hf_RequiredFee.value = BalanceToAssign;
                    }
                    else {

                    }
                    //}
                }
            }
            else {
                //alert('hi');
                alert('Validation :\n' + ValidationMsg);
                //DoItCorrectFromReceivedAmount();
                document.getElementById("Tb_MembershipFee").value = 0;
                AllocateAmount();
                return Go;
            }

        }

        function AllocateTermFeeAmount() {
            var Go = true;
            var ValidationMsg = "";
            var ReceivedAmount = 0;
            var NumberOfTerm = 0, TotalTermFee = 0;

            NumberOfTerm = document.getElementById("Hf_NumberOfTerms").value;

            ReceivedAmount = document.getElementById("Tb_TermFeeWithDiscount").value;
            //alert('hi' + ReceivedAmount.toString());
            if (ReceivedAmount == "") {
                ValidationMsg += "Please enter Term Fee amount.\n";

                document.getElementById("Tb_TermFeeWithDiscount").style.borderColor = "Red";
                Go = false;
            }
            else if (isNaN(ReceivedAmount)) //if not a number
            {
                //alert('hi');
                ValidationMsg += "Plese enter valid Term Fee amount.\n";
                document.getElementById("Tb_TermFeeWithDiscount").style.borderColor = "Red";
                Go = false;
            } else {
                if (parseFloat(ReceivedAmount) < 0) {
                    ValidationMsg += "Term Fee amount should not negative.\n";
                    document.getElementById("Tb_TermFeeWithDiscount").style.borderColor = "Red";
                    Go = false;
                }

                else {
                    document.getElementById("Tb_TermFeeWithDiscount").style.borderColor = "LightGray";
                }
            }

            TotalTermFee = parseFloat(NumberOfTerm).toString() * parseFloat(ReceivedAmount).toString();
            ReceivedAmount = TotalTermFee;





            if (Go == true) {
                var tbl = $("[id$=Gv_FeeDetail]");

                var rows = tbl.find('tr');

                var RequiredFee = 0, PaidFee = 0, Balance = 0;

                for (var i = 1; i < rows.length; i++) {

                    var row = rows[4];

                    var Lbl_RequiredFee = GetChildControl(row, "Lbl_RequiredFee");
                    var Hf_RequiredFee = GetChildControl(row, "Hf_RequiredFee");
                    if (parseFloat(ReceivedAmount) != 0) {


                        if (ReceivedAmount > parseFloat(Lbl_RequiredFee.innerText)) {
                            var BalanceToAssign = 0;

                            BalanceToAssign = ReceivedAmount;
                            //alert('sf ' + BalanceToAssign.toString());
                            Lbl_RequiredFee.value = BalanceToAssign;
                            Lbl_RequiredFee.innerText = BalanceToAssign;
                            Hf_RequiredFee.value = BalanceToAssign;
                        }

                        if (ReceivedAmount <= parseFloat(Lbl_RequiredFee.innerText)) {
                            var BalanceToAssign = 0;

                            BalanceToAssign = ReceivedAmount;
                            //alert('sf ' + BalanceToAssign.toString());
                            Lbl_RequiredFee.value = BalanceToAssign;
                            Lbl_RequiredFee.innerText = BalanceToAssign;
                            Hf_RequiredFee.value = BalanceToAssign;
                        }
                        else {

                        }
                    }
                }
            }
            else {
                //alert('hi');
                alert('Validation :\n' + ValidationMsg);
                //DoItCorrectFromReceivedAmount();
                document.getElementById("Tb_TermFeeWithDiscount").value = 0;
                AllocateAmount();
                return Go;
            }

        }

    </script>

</head>


<body class="hold-transition skin-purple-light sidebar-mini fixed" onload="Addcustomclass();">
    <form id="form1" runat="server">
        <div class="wrapper">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />

            <%--content starts--%>
            <div class="content-wrapper">
                <asp:Panel ID="pnl_personal" runat="server">
                    <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                        <div class="box-header c-box-blueish-new with-border">
                            <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Personal Details"></asp:Label>
                        </div>
                        <div class="box-body c-padding-top-2">
                            <div class="col-md-12 c-inline-space">
                                <div class="col-md-2 c-col-size-14 c-label-1">
                                    <asp:Label ID="Label19" runat="server" CssClass="Label" Text="Membership Number"></asp:Label>
                                </div>
                                <div class="col-md-10 no-padding c-col-size-86">
                                    <div class="col-md-4 c-col-size-40">
                                        <asp:TextBox ID="Tb_MembershipNumber" runat="server" CssClass="form-control" placeholder="Auto Generated Customer Number" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="HF_MembershipNumber" runat="server" Value="" />
                                        <asp:HiddenField ID="HF_StudentId" runat="server" Value="0" />
                                        <asp:HiddenField ID="HF_StudentId_ToPrint" runat="server" Value="0" />
                                    </div>
                                    <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                        <asp:Label ID="Lbl_membershipDate" runat="server" CssClass="Label" Text="Membership Date"></asp:Label>
                                    </div>
                                    <div class="col-md-4 c-col-size-40">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control"
                                                ID="Dp_MembershipDate" data-date-format="dd/mm/yyyy" placeholder="Membership Date"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 c-inline-space">
                                <div class="col-md-2 c-col-size-14 c-label-1">
                                    <asp:Label ID="Lbl_FName" runat="server" CssClass="Label" Text="First Name*"></asp:Label>
                                </div>
                                <div class="col-md-10 no-padding c-col-size-86">
                                    <div class="col-md-2 c-col-size-12">
                                        <asp:DropDownList ID="Dd_Title" runat="server" DataTextField="Title" DataValueField="Title_id" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 c-col-size-23">
                                        <asp:TextBox ID="Tb_FirstName" runat="server" CssClass="form-control c-tb-noresize" placeholder="First Name"
                                            required="required"
                                            oninvalid="this.setCustomValidity('Please Enter First Name')" oninput="this.setCustomValidity('')"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 c-col-size-20 c-label-1 c-padding-left-1">
                                        <asp:Label ID="Lbl_LName" runat="server" CssClass="Label" Text="Surname*"></asp:Label>

                                    </div>
                                    <div class="col-md-2 c-col-size-40">
                                        <asp:TextBox ID="Tb_LastName" runat="server" CssClass="form-control c-tb-noresize" placeholder="Last Name"
                                            required="required"
                                            oninvalid="this.setCustomValidity('Please Enter Last Name')" oninput="this.setCustomValidity('')"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12 c-inline-space">
                                <div class="col-md-2 c-col-size-14 c-label-1">
                                    <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Mobile Number"></asp:Label>
                                </div>
                                <div class="col-md-10 no-padding c-col-size-86">
                                    <div class="col-md-4 c-col-size-40">
                                        <asp:TextBox ID="Tb_Mobile" runat="server" CssClass="form-control" Placeholder="Contact Mobile"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                        <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Telephone Number"></asp:Label>
                                    </div>
                                    <div class="col-md-4 c-col-size-40">
                                        <asp:TextBox ID="Tb_Telephone" runat="server" CssClass="form-control" placeholder="Telephone Number"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 c-inline-space">
                                <div class="col-md-2 c-col-size-14 c-label-1">
                                    <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Email Id"></asp:Label>
                                </div>
                                <div class="col-md-10 no-padding c-col-size-86">
                                    <div class="col-md-4 c-col-size-40">
                                        <asp:TextBox ID="Tb_Email" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                        <asp:Label ID="Label27" runat="server" CssClass="Label" Text="Account Password"></asp:Label>

                                    </div>
                                    <div class="col-md-4 c-col-size-40">
                                        <asp:TextBox ID="Tb_AccountPassword" runat="server" CssClass="form-control" Text="h!4og:qa=puG"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12 c-inline-space">
                                <div class="col-md-2 c-col-size-14 c-label-1 ">
                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Birth Date"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-30 c-label-1 ">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control"
                                            ID="Dp_BirthDate" data-date-format="dd/mm/yyyy" placeholder="Birth Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12 c-inline-space">
                                <div class="col-md-2 c-col-size-14 c-label-1">
                                    <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Post Code"></asp:Label>
                                </div>
                                <div class="col-md-10 no-padding c-col-size-86">
                                    <div class="col-md-4 c-col-size-20">
                                        <asp:TextBox ID="Tb_PostCode" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 c-col-size-20 c-padding-left-1">
                                        <%--<asp:Button ID="Btn_GetAddress" runat="server" Text="Get Address" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" />--%>
                                        <button type="button" id="Btn_GetAddress" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">Get Address</button>
                                    </div>
                                    <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                        <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Result"></asp:Label>
                                    </div>
                                    <div class="col-md-4 c-col-size-40">
                                        <%--<asp:DropDownList ID="Dd_Result" runat="server" CssClass="form-control select2" Style="width: 100% !important"></asp:DropDownList>--%>

                                        <select id="Dd_Result" class="form-control select2" style="width: 100% !important">
                                            <option selected="selected">---- please select your address ----</option>
                                        </select>

                                    </div>

                                </div>
                            </div>
                            <div class="col-md-12 c-inline-space">
                                <div class="col-md-2 c-col-size-14 c-label-1">
                                    <asp:Label ID="Lbl_Address" runat="server" CssClass="Label" Text="Student Address"></asp:Label>
                                </div>
                                <div class="col-md-10 no-padding c-col-size-86">
                                    <asp:TextBox ID="Tb_Address" runat="server" CssClass="form-control c-tb-noresize" placeholder="Student Address" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-12 c-inline-space">
                                <div class="col-md-2 c-col-size-14 c-label-1 ">
                                    <asp:Label ID="Lbl_Upload_Photo" runat="server" CssClass="Label" Text="Upload Photo"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-30 c-label-1 ">
                                    <asp:FileUpload ID="Fu_StudentImgae" runat="server" CssClass="form-control" />
                                    <br />

                                </div>
                            </div>
                            <div></div>
                        </div>
                        <%-- --End Of Box--%>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnl_Disabilities" runat="server">
                    <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                        <div class="box-header c-box-blueish-new with-border">
                            <asp:Label runat="server" ID="Label7" CssClass="box-title" Text="Disabilities/Conditions"></asp:Label>
                        </div>
                        <div class="box-body c-padding-top-2">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-4 c-col-size-18 c-label-1">
                                            <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Does the applicant is disabled "></asp:Label>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-label-1">
                                            <asp:RadioButton ID="RB_Yes" runat="server" AutoPostBack="true"
                                                OnCheckedChanged="RB_Yes_CheckedChanged" GroupName="Disable" />
                                            <asp:Label ID="Label10" runat="server" CssClass="Label" Text=" Yes"></asp:Label>
                                            <asp:RadioButton ID="RB_No" runat="server" AutoPostBack="true"
                                                OnCheckedChanged="RB_No_CheckedChanged" GroupName="Disable" />
                                            <asp:Label ID="Label11" runat="server" CssClass="Label" Text="No"></asp:Label>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-label-1">
                                            <asp:Label ID="Label22" runat="server" CssClass="Label" Text="Please mention any disabilities."></asp:Label>
                                        </div>

                                        <div class="col-md-4 c-col-size-40 c-label-1">
                                            <asp:TextBox ID="Tb_Disability" runat="server" CssClass="form-control " TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel runat="server" ID="Upnl1">
                                <ContentTemplate>
                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-4 c-col-size-20 c-label-1">
                                            <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Does the applicant wear Glasses/Lenses "></asp:Label>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-label-1">
                                            <asp:RadioButton ID="Rb_1" runat="server"
                                                GroupName="Group2" />
                                            <asp:Label ID="Label13" runat="server" CssClass="Label" Text=" Yes"></asp:Label>
                                            <asp:RadioButton ID="Rb_2" runat="server"
                                                GroupName="Group2" />
                                            <asp:Label ID="Label14" runat="server" CssClass="Label" Text="No "></asp:Label>
                                        </div>
                                        <div class="col-md-4 c-col-size-18 c-label-1">
                                            <asp:Label ID="Label23" runat="server" CssClass="Label" Text="Does the applicant suffer from Astama "></asp:Label>
                                        </div>
                                        <div class="col-md-2 c-col-size-10 c-label-1">
                                            <asp:RadioButton ID="Rb_IsAstama1" runat="server" AutoPostBack="true"
                                                OnCheckedChanged="RB_Yes_CheckedChanged" GroupName="Group1" />
                                            <asp:Label ID="Label16" runat="server" CssClass="Label" Text=" Yes"></asp:Label>
                                            <asp:RadioButton ID="Rb_IsAstama2" runat="server" AutoPostBack="true"
                                                OnCheckedChanged="RB_No_CheckedChanged" GroupName="Group1" />
                                            <asp:Label ID="Label24" runat="server" CssClass="Label" Text="No"></asp:Label>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-md-12 c-inline-space">
                                <div class="col-md-6 c-col-size-40 c-label-1 ">
                                    <asp:Label ID="Label25" runat="server" CssClass="Label" Text="Please mention any other martial arts studied and any grades obtained."></asp:Label>
                                </div>
                                <div>
                                    <div class="col-md-6 c-col-size-80 c-label-1">
                                        <asp:TextBox ID="Tb_Others" runat="server" CssClass="form-control " TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 c-inline-space">
                                <div class="col-md-6 c-col-size-40 c-label-1">
                                    <asp:Label ID="Label20" runat="server" CssClass="Label" Text="Please mention anything else that you think may help the instructor during coaching. (i.e. shy,lacks confidence etc.)"></asp:Label>
                                </div>
                                <div class="col-md-6 c-col-size-80 c-label-1">
                                    <asp:TextBox ID="Tb_Extraremark" runat="server" CssClass="form-control " TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnl_Dojo" runat="server">
                    <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                        <div class="box-header c-box-blueish-new with-border">
                            <asp:Label runat="server" ID="Label8" CssClass="box-title" Text="Select Dojo & Belt"></asp:Label>
                        </div>
                        <div class="box-body c-padding-top-2">
                            <asp:UpdatePanel ID="up_Dojo" runat="server">
                                <ContentTemplate>
                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-2 c-col-size-14 c-label-1">
                                            <asp:Label ID="Label30" runat="server" CssClass="Label" Text="Dojo"></asp:Label>
                                        </div>
                                        <div class="col-md-10 no-padding c-col-size-86">
                                            <div class="col-md-4 c-col-size-40">
                                                <%--  <ajaxToolkit:ComboBox ID="Dd_Dojo" AutoCompleteMode="SuggestAppend" runat="server" Width="250px"
                                                    AppendDataBoundItems="true" DropDownStyle="DropDownList" CssClass="WindowsStyle"
                                                    AutoPostBack="true" OnSelectedIndexChanged="Dd_Dojo_SelectedIndexChanged">
                                                </ajaxToolkit:ComboBox>--%>

                                                <asp:DropDownList ID="Dd_Dojo" runat="server" DataValueField="DojoId" DataTextField="DojoCode" CssClass="form-control select2" AutoPostBack="true" Width="350px" OnSelectedIndexChanged="Dd_Dojo_SelectedIndexChanged"></asp:DropDownList>


                                            </div>
                                            <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-4">
                                                <asp:Label ID="Label28" runat="server" CssClass="Label" Text="Belt"></asp:Label>
                                            </div>
                                            <div class="col-md-4 c-col-size-40">
                                                <asp:DropDownList ID="Dd_Belt" runat="server" DataValueField="BeltId" DataTextField="BeltName" CssClass="form-control select2" AutoPostBack="false" Width="350px"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="table-responsive no-padding c-grid-container c-inline-space">
                                            <asp:GridView runat="server" ID="Gv_ClassSchedule" CssClass="table"
                                                CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                                HeaderStyle-CssClass="c-grid-header"
                                                DataKeyNames="DojoClassesScheduleId" AutoGenerateColumns="false" ShowFooter="false" OnRowEditing="Gv_ClassSchedule_RowEditing"
                                                OnRowUpdating="Gv_ClassSchedule_RowUpdating" OnRowCancelingEdit="Gv_ClassSchedule_RowCancelingEdit">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SrNo" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="Lbl_SrNo" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            <%-- <%# Container.DataItemIndex + 1 %>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="Lbl_Class" Text='<%#Eval("Class") %>'></asp:Label>
                                                            <asp:HiddenField ID="Hf_ClassId" runat="server" Value='<%#Eval("ClassId") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Day">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="Lbl_Day" Text='<%#Eval("Day") %>'></asp:Label>
                                                            <asp:HiddenField ID="Hf_DayId" runat="server" Value='<%#Eval("DayId") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Time">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="Tb_StartTime" Enabled="false" CssClass="form-control" placeholder="Start Time" Text='<%#Eval("StartTime") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Time">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="Tb_EndTime" Enabled="false" CssClass="form-control" placeholder="End Time" Text='<%#Eval("EndTime") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Selected">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Chk_IsSelected" runat="server" Checked='<%#Eval("IsChecked")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnl_Fees" runat="server">
                    <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                        <div class="box-header c-box-blueish-new with-border">
                            <asp:Label runat="server" ID="Label9" CssClass="box-title" Text="Fees"></asp:Label>
                        </div>
                        <div class="box-body c-padding-top-2">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="col-md-12 c-inline-space">

                                        <div class="col-md-6">
                                            <asp:Label ID="Label17" runat="server" CssClass="Label" Text="Enrollment Fee to be paid:"></asp:Label>&nbsp;
                                    <asp:Label ID="Lbl_EnrollmentFee" runat="server" CssClass="Label" Font-Bold="true" ForeColor="Red"></asp:Label>
                                            <asp:HiddenField ID="HfEnrollmentFee" runat="server" Value="0" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="Label21" runat="server" CssClass="Label" Text="Annual Membership & Insurance to be paid:"></asp:Label>&nbsp;
                                    <asp:Label ID="Lbl_AnnualMembership" runat="server" CssClass="Label" Font-Bold="true" ForeColor="Red"></asp:Label>
                                            <asp:HiddenField ID="HfAnnualMembership" runat="server" Value="0" />
                                        </div>
                                    </div>

                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="Tb_EnrollmentFee" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Tb_EnrollmentFee_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="Tb_AnnualMembership" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Tb_AnnualMembership_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-6">
                                            <asp:Label ID="Label29" runat="server" CssClass="Label" Text="Membership Fee to be paid:"></asp:Label>&nbsp;
                                    <asp:Label ID="Lbl_MembershipFee" runat="server" CssClass="Label" Font-Bold="true" ForeColor="Red"></asp:Label>
                                            <asp:HiddenField ID="HfMembershipFee" runat="server" Value="0" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Karate Suit (Complete Kit)"></asp:Label>&nbsp;
                                        </div>
                                    </div>

                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="Tb_MembershipFee" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Tb_MembershipFee_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:RadioButton ID="Rb_AlreadyHave" runat="server" GroupName="Rb_Kit" Text="I already have a Goju-Kai Karate suite" AutoPostBack="true" OnCheckedChanged="Rb_AlreadyHave_CheckedChanged" />&nbsp;
                                        </div>
                                    </div>
                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-6">
                                            <asp:Label ID="Label26" runat="server" CssClass="Label" Text="Dvd Volume.1 (Optional for beginners)"></asp:Label>&nbsp;
                                        </div>
                                        <div class="col-md-6">
                                            <asp:RadioButton ID="Rb_BronzePackage" runat="server" GroupName="Rb_Kit" Checked="true" Text="Bronze Package" AutoPostBack="true" OnCheckedChanged="Rb_BronzePackage_CheckedChanged" />&nbsp;
                                    <asp:Label ID="Lbl_BronzePackage" runat="server" CssClass="Label"></asp:Label>

                                        </div>
                                    </div>

                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-6">
                                            <asp:CheckBox ID="Chk_IsDVD" runat="server" Text="Get DVD" OnCheckedChanged="Chk_IsDVD_CheckedChanged" AutoPostBack="true" Checked="true" />&nbsp;
                                              <asp:Label ID="Lbl_DVD" runat="server" CssClass="Label"></asp:Label>
                                            <asp:HiddenField ID="HfDVD" runat="server" Value="0" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:RadioButton ID="Rb_SilverPackage" runat="server" GroupName="Rb_Kit" Text="Silver Package" AutoPostBack="true" OnCheckedChanged="Rb_SilverPackage_CheckedChanged" />&nbsp;
                                    <asp:Label ID="Lbl_SilverPackage" runat="server" CssClass="Label"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-6">
                                        </div>
                                        <div class="col-md-6">
                                            <asp:RadioButton ID="Rb_GoldPackage" runat="server" GroupName="Rb_Kit" Text="Gold Package" AutoPostBack="true" OnCheckedChanged="Rb_GoldPackage_CheckedChanged" />&nbsp;
                                    <asp:Label ID="Lbl_GoldPackage" runat="server" CssClass="Label"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-6">
                                            <asp:CheckBox ID="Chk_IsPermanentDiscount" runat="server" Text="Apply Permanent Discount Fee" AutoPostBack="true" OnCheckedChanged="Chk_IsPermanentDiscount_CheckedChanged" />&nbsp;
                                    <asp:TextBox ID="Tb_TermFeeWithDiscount" runat="server" CssClass="form-control c-tb-noresize" AutoPostBack="true" OnTextChanged="Tb_TermFeeWithDiscount_TextChanged" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="HfTermFee" runat="server" Value="0" />
                                        </div>
                                    </div>

                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-2 c-col-size-14 c-label-1">
                                            <asp:Label ID="Label32" runat="server" CssClass="Label" Text="Select Starting Term:"></asp:Label>
                                        </div>
                                        <div class="col-md-10 no-padding c-col-size-86">
                                            <div class="col-md-4 c-col-size-40">
                                                <asp:DropDownList ID="Dd_Term" runat="server" DataValueField="TermId" DataTextField="Term" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2 c-col-size-20 c-label-1 c-padding-left-1">
                                                <asp:Label ID="Label33" runat="server" CssClass="Label" Text="Total Required Fee for this term:"></asp:Label>
                                            </div>
                                            <div class="col-md-4 c-col-size-40">
                                                <asp:TextBox ID="Tb_TotalTermFee" runat="server" CssClass="form-control c-tb-noresize" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-2 c-col-size-14 c-label-1">
                                            <asp:Label ID="Label34" runat="server" CssClass="Label" Text="Select number of terms you want to pay for:"></asp:Label>
                                        </div>
                                        <div class="col-md-10 no-padding c-col-size-86">
                                            <div class="col-md-4 c-col-size-40">
                                                <asp:DropDownList ID="Dd_NumberOfTerms" runat="server" AutoPostBack="true" CssClass="form-control select2" OnSelectedIndexChanged="Dd_NumberOfTerms_SelectedIndexChanged">
                                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="Hf_NumberOfTerms" runat="server" Value="1" />
                                            </div>
                                            <div class="col-md-2 c-col-size-20 c-label-1 c-padding-left-1">
                                                <asp:Label ID="Label35" runat="server" CssClass="Label" Text="Total Amount Paid:"></asp:Label>
                                            </div>
                                            <div class="col-md-4 c-col-size-40">
                                                <asp:TextBox ID="Tb_TotalPaidAmount" runat="server" CssClass="form-control c-tb-noresize" AutoPostBack="true" OnTextChanged="Tb_TotalPaidAmount_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel runat="server" ID="pnl_TermFee">
                                        <div class="col-md-12 c-inline-space">
                                            <div class="col-md-6">
                                                <asp:Label ID="Lbl_SecondTerm" runat="server" CssClass="Label" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="Tb_SecondTermFee" runat="server" CssClass="form-control c-tb-noresize" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-12 c-inline-space">
                                            <div class="col-md-6">
                                                <asp:Label ID="Lbl_ThirdTerm" runat="server" CssClass="Label" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="Tb_ThirdTermFee" runat="server" CssClass="form-control c-tb-noresize" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-12 c-inline-space">
                                            <div class="col-md-6">
                                                <asp:Label ID="Lbl_FourthTerm" runat="server" CssClass="Label" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="Tb_FourthTerm" runat="server" CssClass="form-control c-tb-noresize" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </asp:Panel>



                                    <div class="table-responsive no-padding c-grid-container c-inline-space">
                                        <asp:GridView runat="server" ID="Gv_FeeDetail" CssClass="table"
                                            CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                            HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                            DataKeyNames="FeeId"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="15" PagerStyle-CssClass="c-paging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item" HeaderStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_FeeName" runat="server" CssClass="Lable" Text='<%#Eval("FeeName") %>'></asp:Label>
                                                        <asp:HiddenField ID="Hf_FeeId" runat="server" Value='<%#Eval("FeeId") %>' />
                                                        <asp:HiddenField ID="Hf_FeeTemplateId" runat="server" Value='<%#Eval("FeeTemplateId") %>' />
                                                        <asp:HiddenField ID="Hf_FeeTemplate" runat="server" Value='<%#Eval("FeeTemplate") %>' />
                                                        <asp:HiddenField ID="Hf_FeeGenerationTypeId" runat="server" Value='<%#Eval("FeeGenerationTypeId") %>' />
                                                        <asp:HiddenField ID="Hf_FeeGenerationType" runat="server" Value='<%#Eval("FeeGenerationType") %>' />
                                                        <asp:HiddenField ID="Hf_FeeCollectionStageId" runat="server" Value='<%#Eval("FeeCollectionStageId") %>' />
                                                        <asp:HiddenField ID="Hf_FeeCollectionStage" runat="server" Value='<%#Eval("FeeCollectionStage") %>' />
                                                        <asp:HiddenField ID="Hf_IsOneOfTheGroup" runat="server" Value='<%#Eval("IsOneOfTheGroup") %>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Required Fee" HeaderStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_RequiredFee" runat="server" CssClass="Lable" Text='<%#Eval("Amount") %>'></asp:Label>
                                                        <asp:HiddenField ID="Hf_RequiredFee" runat="server" Value='<%#Eval("Amount") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Paid Fee" HeaderStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lbl_PaidFee" runat="server" CssClass="Lable" Text='<%#Eval("AmountPaid") %>'></asp:Label>
                                                        <asp:HiddenField ID="Hf_PaidFee" runat="server" Value="0" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>


                                    </div>

                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-2 c-col-size-14 c-label-1">
                                            <asp:Label ID="Label36" runat="server" CssClass="Label" Text="Total Amount:"></asp:Label>
                                        </div>
                                        <div class="col-md-10 no-padding c-col-size-86">
                                            <div class="col-md-4 c-col-size-40">
                                                <asp:TextBox ID="Tb_TotalAmount" runat="server" CssClass="form-control c-tb-noresize" Enabled="false"></asp:TextBox>

                                            </div>
                                            <div class="col-md-2 c-col-size-20 c-label-1 c-padding-left-1">
                                                <asp:Label ID="Label31" runat="server" CssClass="Label" Text="Discount Amount:"></asp:Label>

                                            </div>
                                            <div class="col-md-4 c-col-size-40">
                                                <asp:TextBox ID="Tb_DiscountAmount" runat="server" CssClass="form-control c-tb-noresize" Enabled="false"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-12 c-inline-space">
                                        <div class="col-md-2 c-col-size-14 c-label-1">
                                            <asp:Label ID="Label38" runat="server" CssClass="Label" Text="Total Amount to Paid:"></asp:Label>
                                        </div>
                                        <div class="col-md-10 no-padding c-col-size-86">
                                            <div class="col-md-4 c-col-size-40">
                                                <asp:TextBox ID="Tb_TotalAmountToPaid" runat="server" CssClass="form-control c-tb-noresize" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 c-col-size-20 c-label-1 c-padding-left-1">
                                                <asp:Label ID="Label37" runat="server" CssClass="Label" Text="Balance:"></asp:Label>
                                            </div>
                                            <div class="col-md-4 c-col-size-40">
                                                <asp:TextBox ID="Tb_Balance" runat="server" CssClass="form-control c-tb-noresize" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:HiddenField ID="Hf_TotalAmount" runat="server" Value="0" />
                        </div>
                    </div>
                </asp:Panel>


                <asp:Panel ID="pnl_save" runat="server">
                    <div class="col-md-12 col-md-offset-2 c-button-box">
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="Btn_SaveFees" runat="server" Text="Save" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow c-btn-widht-10" OnClick="Btn_SaveFees_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="Btn_FeesBack" runat="server" Text="Back" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow c-btn-widht-10" formnovalidate="" OnClick="Btn_FeesBack_Click" />
                        </div>
                        <%--   <div class="col-md-2" >
                            <asp:Button ID="Btn_Update" runat="server" Visible="false" Text="Update" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow c-btn-widht-10" OnClick="Btn_Update_Click"  />
                        </div>--%>
                    </div>
                </asp:Panel>
            </div>
            <%--  content end--%>
            <FT:Footer runat="server" ID="FT_1" />
        </div>
    </form>
    <script>
        $(function () {
            $('#Tb_PostCode').val('SL6 1QZ');
            function getTestData(postcode) {
                // Pass parameters via JSON
                var parameters = {
                    key: "80538-0b603-6626b-02557",
                    postcode: postcode,
                    response: "data_formatted"
                };
                var url = "http://pcls1.craftyclicks.co.uk/json/rapidaddress";
                // or via GET parameters
                // var url = "http://pcls1.craftyclicks.co.uk/json/rapidaddress?key=xxxxx-xxxxx-xxxxx-xxxxx&postcode=aa11aa&response=data_formatted";

                request = new XMLHttpRequest();
                request.open('POST', url, false);
                // Only needed for the JSON parameter pass
                request.setRequestHeader('Content-Type', 'application/json');
                // Wait for change and then either JSON parse response text or throw exception for HTTP error
                request.onreadystatechange = function () {
                    if (this.readyState === 4) {
                        if (this.status >= 200 && this.status < 400) {
                            // Success!
                            data = JSON.parse(this.responseText);
                        } else {
                            throw 'HTTP Request Error';
                        }
                    }
                };
                // Send request
                request.send(JSON.stringify(parameters));
                return data;
            }
            var data = "";
            $('#Btn_GetAddress').click(function () {
                //alert(getTestData());
                var Tb_PostCode = $('#Tb_PostCode').val();

                data = getTestData(Tb_PostCode);
                console.log(data);
                $('#Dd_Result').empty().append("<option>---- please select your address ----</option>");
                $('#select2-Dd_Result-container').empty()
                $.each(data["delivery_points"], function (key, value) {

                    var organisation_name = value["organisation_name"];
                    var line_1 = value["line_1"];
                    var line_2 = value["line_2"];
                    var town = value["town"];
                    //console.log(value["dps"])
                    $('#Dd_Result')
                        .append($("<option></option>")
                            .val(value["dps"])
                            .text([organisation_name, line_1, line_2, town].filter(Boolean).join(", ")));
                });
            });
            $("#Dd_Result").change(function () {

                var val = $(this).children("option:selected").val();

                var dataArr = data["delivery_points"];
                var price = $.map(dataArr, function (value, key) {
                    if (value.dps == val) {
                        var organisation_name = value["organisation_name"];
                        var line_1 = value.line_1;
                        var line_2 = value.line_2;
                        var town = value.town;
                        return [organisation_name, line_1, line_2, town, data["postal_county"]].filter(Boolean).join(", ");
                    }
                });
                $('#Tb_Address').val(price);
            });
        });
    </script>


</body>
</html>
