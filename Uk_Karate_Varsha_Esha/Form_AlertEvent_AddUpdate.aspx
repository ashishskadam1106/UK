<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_AlertEvent_AddUpdate.aspx.cs" Inherits="Form_AlertEvent_AddUpdate" %>

<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/Header.ascx" TagName="Header" TagPrefix="HD" %>
<%@ Register Src="~/User Control/MenuNavigation.ascx" TagName="MenuNavigation" TagPrefix="MNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>


<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html>

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

        .c-col-size-30 {
            width: 30%;
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

            .c-col-size-30 {
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

            .inline label {
                display: inline;
            }
        }
    </style>

    <script type="text/javascript">
        function Confirm() {
            if (confirm('Sure to delete ?') == false) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

   
</head>
<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <HD:Header runat="server" ID="HD_1" />
            <MNU:MenuNavigation runat="server" ID="MNU_1" />

            <%--content starts--%>
            <div class="content-wrapper">
                <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">
                    <div class="box-header c-box-blueish-new with-border">
                        <asp:Label runat="server" ID="Lbl_Heading" CssClass="box-title" Text="Add Alert Event"></asp:Label>
                    </div>
                    <div class="box-body c-padding-top-2">
                        <asp:HiddenField ID="Hf_AlertEventId" runat="server" />
                        <div class="col-md-12 c-inline-space">                         
                              <div class="col-md-2 c-col-size-14 c-label-1">
                                    <asp:Label ID="Lbl_EventTitle" runat="server" CssClass="Label" Text="Event Title"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_EventTitle" runat="server" CssClass="form-control c-tb-noresize" placeholder="Event Title" required="required"
                                    oninvalid="this.setCustomValidity('Please enter Event Title.')" oninput="this.setCustomValidity('')"></asp:TextBox>                              
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="lb_Email" runat="server" CssClass="Label" Text="Email Id"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:DropDownList ID="dd_EmailId" DataValueField="EmailAccountId" DataTextField="EmailId" runat="server" CssClass="form-control select2">                                                                             
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 c-inline-space" runat="server">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Is SMS Applicable"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:DropDownList ID="Dd_IsSMSApplicable" runat="server" CssClass="form-control select2">                                        
                                         <asp:ListItem Text="Yes" Selected="True" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Is Email Applicable"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:DropDownList ID="Dd_Is_Email_Applicable" runat="server" CssClass="form-control select2">                                         
                                        <asp:ListItem Text="Yes" Selected="True" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                                           
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label40" CssClass="Label" runat="server" Text="SMS Message"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <asp:TextBox runat="server" ID="Tb_SMSMessage" CssClass="form-control c-tb-noresize" placeholder="SMS Message" TextMode="MultiLine"
                                   rows="5" ></asp:TextBox>
                            </div>
                        </div>

                          <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label3" CssClass="Label" runat="server" Text="Mail Subject"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <asp:TextBox runat="server" ID="tb_MailSubject" CssClass="form-control c-tb-noresize" placeholder="Mail Subject" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
     
                          <div class="col-md-12 c-inline-space">
                                <div class="col-md-2 c-col-size-14 c-label-1">
                                    <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Mail Message*"></asp:Label>
                                </div>
                                <div class="col-md-10 no-padding c-col-size-86">
                                    <%--<asp:TextBox ID="Tb_PublishedIn" runat="server" CssClass="form-control c-resize-vertical"
                                    placeholder="Published In"
                                    required="" oninput="this.setCustomValidity('')"
                                    oninvalid="this.setCustomValidity('Please enter short description about product')"></asp:TextBox>--%>
                                    <CKEditor:CKEditorControl ID="CKE_MailMessage" runat="server" Height="500"></CKEditor:CKEditorControl>
                                </div>
                            </div>
                            
                                     <div class="col-md-12 no-padding">

                            <div class="table-responsive no-padding c-grid-container c-inline-space">
                                <asp:GridView runat="server" ID="Gv_AlertEventTag" CssClass="table"
                                    CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                    HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"                                  
                                    AllowPaging="true" PageSize="15"
                                    PagerStyle-CssClass="c-paging"
                                    AutoGenerateColumns="false" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tag Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_TagName" Text='<%#Eval("TagName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Remark" Text='<%#Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                                                       
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>




                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Attachment File Name"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="Tb_AttachmentFileName" runat="server" CssClass="form-control" Placeholder="Attachment File Name"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Lbl_Upload_Photo" runat="server" CssClass="Label" Text="Upload Attachment"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:FileUpload ID="Fu_Attachment" runat="server" CssClass="form-control"  />
                                </div>
                            </div>
                        </div>

                        
                        <div class="col-md-12 c-inline-space">
                            <div class="col-md-2 c-col-size-14 c-label-1">
                                <asp:Label ID="lbAttachment" runat="server" CssClass="Label" Text="Signature File Name"></asp:Label>
                            </div>
                            <div class="col-md-10 no-padding c-col-size-86">
                                <div class="col-md-4 c-col-size-40">
                                    <asp:TextBox ID="tb_SignatureFileName" runat="server" CssClass="form-control" Placeholder="Signature File Name"></asp:TextBox>
                                </div>
                                <div class="col-md-4 c-col-size-20 c-label-1 c-padding-left-1">
                                    <asp:Label ID="Lb_UploadSignature" runat="server" CssClass="Label" Text="Upload Signature"></asp:Label>
                                </div>
                                <div class="col-md-4 c-col-size-40">
                                    <asp:FileUpload ID="Fu_SignatureImage" runat="server" CssClass="form-control"  />
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 no-padding">
                            <div class="table-responsive no-padding c-grid-container c-inline-space">
                                <asp:GridView runat="server" ID="Gv_AttachmentGrid" CssClass="table"
                                    CellSpacing="1" BorderColor="#a5969e" BorderWidth="1px"
                                    HeaderStyle-CssClass="c-grid-header" AlternatingRowStyle-CssClass="c-grid-alternaterow"                                   
                                    AllowPaging="true" PageSize="15"
                                    PagerStyle-CssClass="c-paging"
                                    >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No." HeaderStyle-CssClass="c-col-size-8">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_SrNo" Text='<%#Eval("SrNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Attachment">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Lbl_Attachment" Text='<%#Eval("ReportPath") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="Btn_Delete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn bg-purple c-bg-blueish btn-flat c-btn-height btn-block" CommandArgument='<%#Eval("AlertEventId") %>'
                                                    OnClientClick="return confirm('Do you want to delete Attachment?')" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>

                        <div class="col-md-12 col-md-offset-2 c-button-box">
                            <div class="col-md-10 no-padding">
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Save" runat="server" Text="Save" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow"  OnClientClick="return Validate_Date();" OnClick="Btn_Save_Click" />
                                </div>
                                <div class="col-md-3 c-btn-widht-25">
                                    <asp:Button ID="Btn_New" runat="server" Text="New" CssClass="btn bg-purple c-bg-blueish btn-block btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_New_Click"  />
                                </div>
                                <div class="col-md-4 c-btn-widht-25">
                                    <asp:Button ID="Btn_Back" runat="server" Text="Back" CssClass="btn bg-purple c-bg-blueish btn-block  btn-flat c-with-shadow" formnovalidate="" OnClick="Btn_Back_Click" />
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
