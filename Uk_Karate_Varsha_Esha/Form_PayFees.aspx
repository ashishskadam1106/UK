<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_PayFees.aspx.cs" Inherits="Form_PayFees" %>
<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UK_Karate</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />

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

        .c-width-66p {
            width: 65px;
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

            .c-width-66p {
                width: 100%;
                margin-top: 1px;
            }
        }
    </style>

    <script type="text/javascript">
        function Savealert() {
            swal({
                type: 'success',
                title: 'Class Added Successfully ',
                allowOutsideClick: false,

            }).then(function (name) {
                window.location = 'Form_ClassMaster.aspx';
            })
        }
        function Updatealert() {
            swal({
                type: 'success',
                title: 'Class details updated Successfully ',
                allowOutsideClick: false,

            }).then(function (name) {
                window.location = 'Form_ClassMaster.aspx';
            })
        }


        function ChangeAllocatedAmountManually(Lbl_TillNowBalance, Tb_AllocatedAmount, Lbl_Balance, HF_Balance) {
            var Go = true;
            var ValidationMsg = "";
            var BalanceTillNow = 0, AllocatedAmount = 0, NewBalance = 0;

            ReceivedAmount = document.getElementById("Tb_Amount").value;
            BalanceTillNow = Lbl_TillNowBalance.innerText;
            AllocatedAmount = Tb_AllocatedAmount.value;

            if (parseFloat(ReceivedAmount) > 0) {
                if (AllocatedAmount == "") {
                    ValidationMsg += "Please enter allocated amount.\n";
                    Tb_AllocatedAmount.style.borderColor = "Red";
                    Go = false;
                }
                else if (isNaN(AllocatedAmount)) //if not a number
                {
                    ValidationMsg += "Plese enter valid allocated amount.\n";
                    Tb_AllocatedAmount.style.borderColor = "Red";
                    Go = false;
                } else {
                    if (parseFloat(AllocatedAmount) < 0) {
                        ValidationMsg += "Allocated amount should not negative.\n";
                        Tb_AllocatedAmount.style.borderColor = "Red";
                        Go = false;
                    }
                    else if (parseFloat(AllocatedAmount) > parseFloat(BalanceTillNow)) {
                        ValidationMsg += "Allocated amount can not be greater than outstanding amount.\n";
                        Tb_AllocatedAmount.style.borderColor = "Red";
                        Go = false;
                    }

                    else { Tb_AllocatedAmount.style.borderColor = "LightGray"; }
                }



                if (Go == true) {
                    NewBalance = parseFloat(parseFloat(BalanceTillNow) - parseFloat(AllocatedAmount)).toFixed(2);

                    Lbl_Balance.innerHTML = NewBalance.toString();
                    HF_Balance.value = NewBalance.toString();


                    CalculateTotal();
                } else {
                    alert('Validation :\n' + ValidationMsg);
                    DoItCorrectFromAllocatedAmount(Lbl_TillNowBalance, Tb_AllocatedAmount, Lbl_Balance, HF_Balance);

                    return Go;
                }
            }
            else {
                alert('Enter Amount First');
                DoItCorrectFromAllocatedAmount(Lbl_TillNowBalance, Tb_AllocatedAmount, Lbl_Balance, HF_Balance);
            }
        }

        function DoItCorrectFromAllocatedAmount(Lbl_TillNowBalance, Tb_AllocatedAmount, Lbl_Balance, HF_Balance) {

            BalanceTillNow = Lbl_TillNowBalance.innerText;
            AllocatedAmount = 0;

            Tb_AllocatedAmount.value = AllocatedAmount;
            Lbl_Balance.innerHTML = BalanceTillNow.toString();
            HF_Balance.value = BalanceTillNow.toString();
            CalculateTotal();
        }

        function AllocateAmount() {
            var Go = true;
            var ValidationMsg = "";
            var ReceivedAmount = 0;

            ReceivedAmount = document.getElementById("Tb_Amount").value;
            //alert('hi' + ReceivedAmount.toString());
            if (ReceivedAmount == "") {
                ValidationMsg += "Please enter allocated amount.\n";
                
                document.getElementById("Tb_Amount").style.borderColor = "Red";
                Go = false;
            }
            else if (isNaN(ReceivedAmount)) //if not a number
            {
                //alert('hi');
                ValidationMsg += "Plese enter valid allocated amount.\n";
                document.getElementById("Tb_Amount").style.borderColor = "Red";
                Go = false;
            } else {
                if (parseFloat(ReceivedAmount) < 0) {
                    ValidationMsg += "Allocated amount should not negative.\n";
                    document.getElementById("Tb_Amount").style.borderColor = "Red";
                    Go = false;
                }
                
                else {
                    document.getElementById("Tb_Amount").style.borderColor = "LightGray";
                }
            }
            if (Go == true) {
                var tbl = $("[id$=Gv_PayFees]");
       
                var rows = tbl.find('tr');

                var BalanceTillDate = 0, AllocatedAmount = 0, Balance = 0;

                for (var i = 1; i < rows.length; i++) {

                    var row = rows[i];

                    var Lbl_TillNowBalance = GetChildControl(row, "Lbl_TillNowBalance");
                    var Tb_AllocatedAmount = GetChildControl(row, "Tb_AllocatedAmount");
                    var Lbl_Balance = GetChildControl(row, "Lbl_Balance");
                    var HF_Balance = GetChildControl(row, "HF_Balance");

                    if (parseFloat(ReceivedAmount) != 0) {
                        if (ReceivedAmount <= parseFloat(Lbl_TillNowBalance.innerText)) {
                            var BalanceToAssign = 0;
                            
                            BalanceToAssign = ReceivedAmount;
                            //alert('sf ' + BalanceToAssign.toString());
                            Tb_AllocatedAmount.value = BalanceToAssign;
                        }
                        else {
                            var BalanceToAssign = 0;
                            BalanceToAssign = Lbl_TillNowBalance.innerText;
                            //alert('sf ' + Lbl_TillNowBalance.innerText.toString());
                            Tb_AllocatedAmount.value = BalanceToAssign;
                        }
                    }
                    else {
                        Tb_AllocatedAmount.value = 0;
                    }
                    
                    ReceivedAmount = parseFloat(ReceivedAmount) - parseFloat(Tb_AllocatedAmount.value);


                    Balance = parseFloat(Lbl_TillNowBalance.innerText) - parseFloat(Tb_AllocatedAmount.value);
                    Lbl_Balance.innerText = Balance.toFixed(2);
                    HF_Balance.value = Balance.toFixed(2);

                }
                CalculateTotal();
            }

            else {
                //alert('hi');
                alert('Validation :\n' + ValidationMsg);
                //DoItCorrectFromReceivedAmount();
                document.getElementById("Tb_Amount").value = 0;
                AllocateAmount();
                return Go;
            }

        }
        

        function CalculateTotal() {
            var tbl = $("[id$=Gv_PayFees]");
            //var tbl = document.getElementById("<%=Gv_PayFees.ClientID%>");
            var rows = tbl.find('tr');
            //alert('hi' + rows.length.toString());

            //Tb_Amount

            

            var ReceivedAmount=0, BalanceTillDate = 0, AllocatedAmount = 0, Balance = 0;

            for (var i = 1; i < rows.length; i++) {

                var row = rows[i];

                
                var Lbl_TillNowBalance = GetChildControl(row, "Lbl_TillNowBalance");
                var Tb_AllocatedAmount = GetChildControl(row, "Tb_AllocatedAmount");
                var Lbl_Balance = GetChildControl(row, "Lbl_Balance");
                var HF_Balance = GetChildControl(row, "HF_Balance");
                


                BalanceTillDate = parseFloat(BalanceTillDate) + parseFloat(Lbl_TillNowBalance.innerText);
                AllocatedAmount = parseFloat(AllocatedAmount) + parseFloat(Tb_AllocatedAmount.value);
                Balance = parseFloat(Balance) + parseFloat(HF_Balance.value);
                
                
            }

            ReceivedAmount = document.getElementById("Tb_Amount").value;
            if (ReceivedAmount == "") {
                ReceivedAmount = 0;
                document.getElementById("Tb_Amount").value = ReceivedAmount.toFixed(2);
            }
            else if (isNaN(AllocatedAmount)) //if not a number
            {
                ReceivedAmount = 0;
                document.getElementById("Tb_Amount").value = ReceivedAmount.toFixed(2);
            }            

            
            document.getElementById("Tb_OutstandingAmount").value = parseFloat(BalanceTillDate).toFixed(2);
            document.getElementById("Tb_Balance").value = parseFloat(Balance).toFixed(2);
            if (parseFloat(ReceivedAmount) == 0) {
                document.getElementById("Tb_ChangeReturn").value = 0;
            }
            else {
                var ChangeReturn = parseFloat(ReceivedAmount) -  parseFloat(AllocatedAmount);
                document.getElementById("Tb_ChangeReturn").value = parseFloat(ChangeReturn).toFixed(2);
            }
            
        }


        function GetChildControl(element, id) {
            var child_elements = element.getElementsByTagName("*");
            for (var i = 0; i < child_elements.length; i++) {
                if (child_elements[i].id.indexOf(id) != -1) {
                    return child_elements[i];
                }
            }
        }


    </script>
          
</head>

<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />
            <%--content starts--%>
            <div class="content-wrapper">
                <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish-new with-border">
                        <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Pay Fees"></asp:Label>
                    </div>
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label1" runat="server"  Text="Amount"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Amount" runat="server" CssClass="form-control c-tb-noresize" placeholder="Enter Amount" 
                                       OnTextChanged="Tb_Amount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label2" runat="server" Text="Outstanding Amount"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_OutstandingAmount" runat="server" CssClass="form-control c-tb-noresize" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label3" runat="server"  Text="New Balance"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Balance" runat="server" CssClass="form-control c-tb-noresize" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label4" runat="server" Text="Change Return" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_ChangeReturn" runat="server" CssClass="form-control c-tb-noresize" Enabled="false" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>



                      

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-12 no-padding">
                                <div class="table-responsive no-padding c-grid-container c-inline-space">
                                    <asp:GridView ID="Gv_PayFees" runat="server" CssClass="table"
                                        CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                        HeaderStyle-CssClass="c-grid-header"
                                        DataKeyNames="StudentFeesDetailId" AutoGenerateColumns="false" 
                                        OnRowDataBound="Gv_PayFees_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Fee Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Lbl_FeeName" Text='<%#Eval("FeeName") %>' Width="200"></asp:Label>
                                                    <asp:HiddenField ID="HF_FeeId" runat="server" Value='<%#Eval("FeeId") %>' />
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>

                                                    <asp:HiddenField ID="Hf_FeeGenerationTypeId" runat="server" Value='<%#Eval("FeeGenerationTypeId") %>' />

                                                    <asp:Label runat="server" ID="Lbl_FeeGenerationType" Text='<%#Eval("FeeGenerationType") %>'></asp:Label>
                                                    <asp:HiddenField ID="Hf_FeeCategoryId" runat="server" Value='<%#Eval("FeeCategoryId") %>' />
                                                    <asp:HiddenField ID="Hf_FeeCategory" runat="server" Value='<%#Eval("FeeCategory ") %>' />
                                                    
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Fee For">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Lbl_FeeGenerationTypeReference" Text='<%#Eval("FeeGenerationTypeReference") %>' Width="150"></asp:Label>
                                                    <asp:HiddenField ID="HF_FeeGenerationTypeReferenceId" runat="server" Value='<%#Eval("FeeGenerationTypeReferenceId") %>' />
                                                    <asp:HiddenField ID="Hf_FeeCollectionStageId" runat="server" Value='<%#Eval("FeeCollectionStageId") %>' />
                                                    <asp:HiddenField ID="Hf_FeeCollectionStage" runat="server" Value='<%#Eval("FeeCollectionStage") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="50">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Lbl_Amount" Width="50" Text='<%#Eval("Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Discount" HeaderStyle-Width="50">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Lbl_DiscountAmount" Width="50" Text='<%#Eval("DiscountAmount") %>'></asp:Label>
                                                    <%--<asp:TextBox ID="Tb_DiscountAmount" runat="server" Width="50" CssClass="form-control c-height" placeholder="Discount" Text='<%#Eval("DiscountAmount") %>'></asp:TextBox>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Final Amount" HeaderStyle-Width="70">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Lbl_FinalAmount" Text='<%#Eval("FinalAmount") %>'></asp:Label>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Received Amount" HeaderStyle-Width="70">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Lbl_AmountReceivedTillNow" Width="50" Text='<%#Eval("AmountReceivedTillNow") %>'></asp:Label>
                                                    <%--<asp:TextBox runat="server" ID="Tb_ReceivedAmount" Width="70" CssClass="form-control c-height" placeholder="Amount Paid" Text='<%#Eval("AmountReceivedTillNow") %>'></asp:TextBox>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Outstanding" HeaderStyle-Width="70">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Lbl_TillNowBalance" Text='<%#Eval("BalanceTillNow") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Allocated Amount" HeaderStyle-Width="70">
                                                <ItemTemplate>      
                                                    <asp:TextBox runat="server" ID="Tb_AllocatedAmount" Width="70" CssClass="form-control c-height" Text='<%#Eval("AllocatedAmount") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Balance" HeaderStyle-Width="70">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Lbl_Balance" Text='<%#Eval("Balance") %>'></asp:Label>
                                                    <asp:HiddenField ID="HF_Balance" runat="server" Value='<%#Eval("Balance") %>' />
                                                </ItemTemplate>

                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>


                            <div class="col-md-12 col-md-offset-2 c-button-box">
                                <div class="col-md-4 c-btn-widht-15">
                                    <asp:Button ID="Btn_SavePayment" runat="server" Text="Save" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow"  OnClick="Btn_SavePayment_Click"/>
                                </div>


                                <div class="col-md-4 c-btn-widht-15">
                                    <asp:Button ID="Btn_Back" runat="server" Text="Back" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Back_Click" />
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
