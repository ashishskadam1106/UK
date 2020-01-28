<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_PurchaseOrderAddUpdate.aspx.cs" Inherits="Form_PurchaseOrderAddUpdate" %>

<!DOCTYPE html>
<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

        .c-btn-width-47 {
            width: 47%;
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

        .c-grid-col-size-300 {
            width: 65%;
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

            .c-btn-width-47 {
                width: 100%;
            }
        }
    </style>

    <script type="text/javascript">

        $(function () {
            $('#Dp_OrderDate').datepicker({
            });
        });

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                if (charCode == 46) {
                    return true;
                } else {
                    return false;
                }
            }
            return true;
        }

        function GetChildControl(element, id) {
            var child_elements = element.getElementsByTagName("*");
            for (var i = 0; i < child_elements.length; i++) {
                if (child_elements[i].id.indexOf(id) != -1) {
                    return child_elements[i];
                }
            }
        }

        function CalculateTaxable(Index, Tb_Item_Rate, Tb_DiscPer, Tb_DiscountAmount, Tb_Quantity, HF_DiscAmount, Dd_Discount_Type, Tb_Amount, HF_ItemTotal) {
            var Go = true;
            var Rate = 0, DiscPer = 0, DiscAmount = 0, Quantity = 0, TotalAmount = 0;
            var ValidationMsg = "";

            Dd_Discount_Type = Dd_Discount_Type.selectedIndex;

            Quantity = Tb_Quantity.value;
            if (Quantity == "") {
                ValidationMsg += "Plese enter Quantity.\n";
                Tb_Quantity.style.borderColor = "Red";
                Go = false;
            }
            else if (isNaN(Quantity)) //if not a number
            {
                ValidationMsg += "Plese enter valid Quantity.\n";
                Tb_Quantity.style.borderColor = "Red";
                Go = false;
            } else {
                if (Quantity <= 0) {
                    ValidationMsg += "Quantity should be greater than zero.\n";
                    Tb_Quantity.style.borderColor = "Red";
                    Go = false;
                } else { Tb_Quantity.style.borderColor = "LightGray"; }
            }

            Rate = Tb_Item_Rate.value;
            //   Rate = Tb_Rate.value;
            if (Rate == "") {
                ValidationMsg += "Plese enter Rate.\n";
                Tb_Item_Rate.style.borderColor = "Red";
                Go = false;
            }
            else if (isNaN(Rate)) //if not a number
            {
                ValidationMsg += "Plese enter valid Rate.\n";
                Tb_Item_Rate.style.borderColor = "Red";
                Go = false;
            } else {
                if (Rate <= 0) {
                    ValidationMsg += "Rate should be greater than zero.\n";
                    Tb_Item_Rate.style.borderColor = "Red";
                    Go = false;
                } else { Tb_Item_Rate.style.borderColor = "LightGray"; }
            }

            DiscPer = Tb_DiscPer.value;
            if (DiscPer == "") {
                ValidationMsg += "Plese enter Discount %.\n";
                Tb_DiscPer.style.borderColor = "Red";
                Go = false;
            }
            else if (isNaN(DiscPer)) //if not a number
            {
                ValidationMsg += "Plese enter valid Discount %.\n";
                Tb_DiscPer.style.borderColor = "Red";
                Go = false;
            } else {
                if (DiscPer < 0) {
                    ValidationMsg += "Discount % should not be negative.\n";
                    Tb_DiscPer.style.borderColor = "Red";
                    Go = false;
                } else { Tb_DiscPer.style.borderColor = "LightGray"; }
            }

            if (Go == true) {
                if (Dd_Discount_Type == "0") {
                    if (Tb_DiscountAmount.value >= 0) {
                        DiscPer = 0;
                        Tb_DiscPer.value = DiscPer;
                        DiscAmount = Tb_DiscountAmount.value;
                        TotalAmount = parseFloat((Quantity * Rate) - DiscAmount).toFixed(2);
                        Tb_DiscountAmount.value = parseFloat(DiscAmount).toString();
                        Tb_Amount.value = TotalAmount.toString();
                        HF_ItemTotal.value = TotalAmount;

                    }
                }
                else {
                    if (Dd_Discount_Type == "1") {
                        DiscAmount = parseFloat(((DiscPer / 100) * Rate)).toFixed(2);
                        TotalAmount = parseFloat(Quantity * (Rate - DiscAmount)).toFixed(2);
                        Tb_DiscountAmount.value = parseFloat(DiscAmount * Quantity).toString();
                        Tb_Amount.value = TotalAmount.toString();
                        HF_ItemTotal.value = TotalAmount;
                    }
                }
                CalculateTotal();

            } else {
                alert('Validation :\n' + ValidationMsg);
                return Go;
            }

        }

        function CalculateTotal() {
            var Total = 0.0, BillAmount = 0.0;


            var tbl = $("[id$=Gv_PurchaseOrderDetail]");
            var rows = tbl.find('tr');

            for (var i = 1; i < rows.length - 1; i++) {
                var row = rows[i];

                var Tb_Amount = GetChildControl(row, "Tb_Amount");

                if (Tb_Amount == undefined) { Total += 0; }
                else if (isNaN(parseFloat(Tb_Amount.value)))
                { Total += parseFloat(0); }
                else
                {
                    Total += parseFloat(Tb_Amount.value);
                }
            }

            Total = parseFloat(Total).toFixed(2);


            //TB PRoduct
            document.getElementById("Tb_TotalAmount").value = Total;
            //HF Product
            document.getElementById("Hf_TotalAmount").value = Total;

        }

    </script>
</head>
<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />
            <asp:ScriptManager ID="ScriptManager_1" runat="server"></asp:ScriptManager>
            <div class="content-wrapper">
                <div class="box  box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish-new with-border">
                        <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Add Purchase Order"></asp:Label>
                    </div>
                    <div class="box-body c-padding-top-2">
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Order Number"></asp:Label>
                            </div>

                            <div class="col-md-10 c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_PurchaseOrderNumber" CssClass="form-control" runat="server" placeholder="Order Number" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Order Date"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control"
                                            ID="Dp_OrderDate" data-date-format="dd/mm/yyyy" placeholder="Order Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Supplier Name"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:DropDownList ID="Dd_SupplierName" runat="server" CssClass="form-control select2" DataValueField="SupplierId"
                                        DataTextField="SupplierName" OnSelectedIndexChanged="Dd_SupplierName_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Mobile Number"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_MobileNumber" CssClass="form-control" runat="server" placeholder="Mobile Number" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Supplier Name"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86">
                                <asp:TextBox ID="Tb_SupplierAddress" CssClass="form-control" runat="server" TextMode="MultiLine" placeholder="Supplier Address" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label7" runat="server" CssClass="Label" Text="EmailId"></asp:Label>
                            </div>
                            <div class="col-md-10 c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_EmailId" CssClass="form-control" runat="server" placeholder="EmailId" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <%--------------------------OtherCharge GRIDVIEW STARTS-----------------%>

                        <asp:UpdatePanel ID="UPnl_Grid" runat="server">
                            <ContentTemplate>
                                <div class="col-md-12">
                                    <div class="table-responsive no-padding c-grid-container c-inline-space">
                                        <asp:GridView runat="server" ID="Gv_PurchaseOrderDetail" CssClass="table"
                                            CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                            HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"
                                            DataKeyNames="Index" AutoGenerateColumns="false" ShowFooter="true"
                                            OnRowCancelingEdit="Gv_PurchaseOrderDetail_RowCancelingEdit"
                                            OnRowCommand="Gv_PurchaseOrderDetail_RowCommand"
                                            OnRowUpdating="Gv_PurchaseOrderDetail_RowUpdating"
                                            AllowPaging="true" PageSize="15"
                                            PagerStyle-CssClass="c-paging"
                                            OnPageIndexChanging="Gv_PurchaseOrderDetail_PageIndexChanging"
                                            OnRowDataBound="Gv_PurchaseOrderDetail_RowDataBound">
                                            <Columns>

                                                <asp:TemplateField HeaderText="SrNo" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbl_SrNo" Text='<%#Eval("SrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Material">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbl_Material" Text='<%#Eval("MaterialName") %>'></asp:Label>
                                                        <asp:HiddenField ID="Hf_MaterialId" runat="server" Value='<%#Eval("MaterialMasterId") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="Dd_Material" runat="server" DataValueField="MaterialMasterId" DataTextField="MaterialName"
                                                            CssClass="select2" Width="350" AutoPostBack="true" OnSelectedIndexChanged="Dd_Material_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="Tb_Rate" onkeypress="return isNumber(event)" CssClass="form-control" placeholder="Rate" Text='<%#Eval("Rate","{0:0.00}")%>' Width="60"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:TextBox runat="server" ID="Tb_Rate" onkeypress="return isNumber(event)" CssClass="form-control" Text="0.00" placeholder="Rate" Width="60"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="Tb_PurchaseQuantity" onkeypress="return isNumber(event)" CssClass="form-control" placeholder="Purchase Quantity" Text='<%#Eval("Quantity","{0:0.00}")%>' Width="60" Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:TextBox runat="server" ID="Tb_PurchaseQuantity" onkeypress="return isNumber(event)" Text="0.00" CssClass="form-control" placeholder="Purchase Quantity" Width="60"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Discount Type" HeaderStyle-Width="60">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Lbl_Discount_Type" Text='<%#Eval("DiscountType") %>' Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="Dd_Discount_Type" runat="server" DataValueField="DiscountTypeId" DataTextField="DiscountType"
                                                            CssClass="select2" Width="60" AutoPostBack="true" name="DD_DiscountType" OnSelectedIndexChanged="Dd_Discount_Type_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="Hf_DiscountTypeId" runat="server" Value='<%#Eval("DiscountTypeId") %>' />
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="Dd_Discount_Type" runat="server" DataValueField="DiscountTypeId" DataTextField="DiscountType"
                                                            CssClass="select2" Width="60" AutoPostBack="true" name="DD_DiscountType" OnSelectedIndexChanged="Dd_Discount_Type_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="Hf_DiscountTypeId" runat="server" Value="0" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount %">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="Tb_DiscountPer" onkeypress="return isNumber(event)" CssClass="form-control" Text='<%#Eval("DiscountPer","{0:0.00}")%>' Width="60"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:TextBox runat="server" ID="Tb_DiscountPer" onkeypress="return isNumber(event)" Text="0.00" CssClass="form-control" placeholder="Discount %" Width="60"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Discount Amount" HeaderStyle-Width="60">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="Tb_DiscountAmount" onkeypress="return isNumber(event)" CssClass="form-control" Width="60" Text='<%#Eval("DiscountAmount","{0:0.00}") %>'></asp:TextBox>
                                                        <asp:HiddenField ID="HF_DiscAmount" runat="server" Value='<%#Eval("DiscountAmount","{0:0.00}") %>' />
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:TextBox runat="server" ID="Tb_DiscountAmount" onkeypress="return isNumber(event)" Text="0.00" Width="60" CssClass="form-control"></asp:TextBox>
                                                        <asp:HiddenField ID="HF_DiscAmount" runat="server" Value="0" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Tb_Amount" Width="80" runat="server" Enabled="false" Text='<%#Eval("TotalAmount","{0:0.00}") %>'></asp:TextBox>
                                                        <asp:HiddenField ID="HF_ItemTotal" runat="server" Value='<%#Eval("TotalAmount","{0:0.00}") %>' />
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:TextBox ID="Tb_Amount" Width="80" onkeypress="return isNumber(event)" Text="0.00" runat="server"></asp:TextBox>
                                                        <asp:HiddenField runat="server" ID="HF_ItemTotal" Value="0" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <%--<asp:Button ID="Btn_Edit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn btn-primary c-bg-blueish btn-block btn-flat c-btn-height" />--%>
                                                        <asp:Button ID="Btn_Delete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn btn-primary c-bg-blueish btn-block btn-flat c-btn-height" CommandArgument='<%#Eval("Index") %>'
                                                            OnClientClick="return confirm('Do you want to delete Material?')" OnClick="Btn_Delete_Click" />
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Button ID="Btn_Add" runat="server" CssClass="btn btn-primary c-bg-blueish btn-block btn-flat c-btn-height" CommandName="Add" Text="Add" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <%--------------------------GRIDVIEW Ends-----------------%>
                        <div class="col-md-12 c-inline-space">
                            <asp:HiddenField ID="Hf_TotalAmount" runat="server" Value="0" />
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Total Amount"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:UpdatePanel ID="Upnl_12" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="Tb_TotalAmount" runat="server" CssClass="form-control" placeholder="Total Amount" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 col-md-offset-3 c-button-box">
                            <div class="col-md-10 no-padding">
                                <div class="col-md-6 c-btn-widht-25">
                                    <asp:Button ID="Btn_Save" runat="server" Text="Save" CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-with-shadow" OnClick="Btn_Save_Click" />
                                </div>

                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Back" runat="server" Text="Back" CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Back_Click" />
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
            <FT:Footer runat="server" ID="FT_1" />
        </div>
    </form>
</body>
</html>
