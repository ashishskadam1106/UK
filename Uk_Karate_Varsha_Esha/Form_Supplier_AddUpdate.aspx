<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_Supplier_AddUpdate.aspx.cs" Inherits="Form_Supplier_AddUpdate" %>

<!DOCTYPE html>


<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UK_Karate</title>
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


        /*.select2-container--default .select2-selection--single {
            padding-left:2px;
        }*/
        /*.c-btn-widht-37 {
            width: 37%;
        }*/

        /*.c-col-size-8 {
            width: 8%;
            padding-left: 0px;
            padding-right: 0px;
        }*/

        /*.c-height {
            margin: 1%;
            height: 28px;
        }

        .c-btn-height {
            margin: 1%;
            height: 28px;
            padding: 0px;
        }*/
        /*.c-dd-height {
        height:30px;
        
        }*/
        /*.c-width-46 {
            width: 46%;
        }

        .c-width-200p {
            width: 200px;
        }*/

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

        function ValidateNo() {
            var phoneNo = document.getElementById('Tb_ContactMobile');

            if (phoneNo.value == "" || phoneNo.value == null) {
                alert("Please enter your Mobile No.");
                return false;
            }
            if (phoneNo.value.length < 10 || phoneNo.value.length > 10) {
                alert("Mobile No. is not valid, Please Enter 10 Digit Mobile No.");
                return false;
            }

            return true;
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
                        <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Supplier Master"></asp:Label>
                    </div>
                    <div class="box-body c-padding-top-2">


                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Supplier Name*"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <asp:TextBox ID="Tb_SupplierName" runat="server" CssClass="form-control c-tb-noresize" placeholder="Supplier Name"
                                    required="required"
                                    oninvalid="this.setCustomValidity('Please Enter Supplier Name')" oninput="this.setCustomValidity('')"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Contact Mobile"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_ContactMobile" runat="server" CssClass="form-control c-tb-noresize" placeholder="Contact Mobile"
                                        required="required" oninvalid="this.setCustomValidity('Please enter Mobile No.')" oninput="this.setCustomValidity('')" onkeypress="return isNumber(event)" onchange="ValidateNo();"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Contact Office"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_ContactOffice" runat="server" CssClass="form-control c-tb-noresize" placeholder="Contact Office"></asp:TextBox>
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
                                   <%-- <asp:Button ID="Btn_GetAddress" runat="server" Text="Get Address" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" />--%>
                                     <button type="button" id="Btn_GetAddress" class="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow">Get Address</button>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Result"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:DropDownList ID="Dd_Result" runat="server" CssClass="form-control select2" Style="width: 100% !important"></asp:DropDownList>
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
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label17" runat="server" CssClass="Label" Text="Premise"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Premise" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label21" runat="server" CssClass="Label" Text="County"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_County" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Ward"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Ward" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Lbl_District" runat="server" CssClass="Label" Text="District"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_District" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Country"></asp:Label>
                            </div>

                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_Country" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <%--    <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Opening Credit"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 no-padding c-col-size-43">
                                    <div class="col-md-4 c-col-size-43">
                                        <asp:TextBox ID="Tb_OpeningCredit" runat="server" CssClass="form-control" Placeholder="Opening Credit"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 c-col-size-14 c-label-1 c-padding-left-1">
                                        <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Paid"></asp:Label>
                                    </div>
                                    <div class="col-md-4 c-col-size-43">
                                        <asp:TextBox ID="Tb_Paid" runat="server" CssClass="form-control" Placeholder="Paid" Enabled="false"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-md-4 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Opening Debit"></asp:Label>
                                </div>
                                <div class="col-md-4 no-padding c-col-size-43">
                                    <div class="col-md-4 c-col-size-43">
                                        <asp:TextBox ID="Tb_OpeningDebit" runat="server" CssClass="form-control" Placeholder="Opening Debit"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 c-col-size-16 c-label-1 c-padding-left-1">
                                        <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Received"></asp:Label>
                                    </div>
                                    <div class="col-md-4 c-col-size-41">
                                        <asp:TextBox ID="Tb_Received" runat="server" CssClass="form-control" Placeholder="Received" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Current Credit"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-43">
                                    <asp:TextBox ID="Tb_CurrentCredit" runat="server" CssClass="form-control" Placeholder="Current Credit" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-14 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label19" runat="server" CssClass="Label" Text="Current Debit"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-43">
                                    <asp:TextBox ID="Tb_CurrentDebit" runat="server" CssClass="form-control" placeholder="Current Debit" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>--%>

                        <div class="col-md-12 col-md-offset-2 c-button-box">

                            <div class="col-md-10 no-padding">
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Save" runat="server" Text="Save" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" OnClick="Btn_Save_Click" OnClientClick="return ValidateNo();" />
                                </div>

                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_New" runat="server" Text="New" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_New_Click" />
                                </div>

                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Back" runat="server" Text="Back" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Back_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
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
