<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_Welcome.aspx.cs" Inherits="Form_Welcome" %>

<!DOCTYPE html>

<%@ Register Src="~/User Control/DefaultLinks.ascx" TagName="DefaultLinks" TagPrefix="DL" %>
<%@ Register Src="~/User Control/StudentHeader.ascx" TagName="StudentHeader" TagPrefix="SHD" %>
<%@ Register Src="~/User Control/StudentMenuNavigation.ascx" TagName="StudentMenuNavigation" TagPrefix="SMNU" %>
<%@ Register Src="~/User Control/Footer.ascx" TagName="Footer" TagPrefix="FT" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>UK_Karate</title>
    <DL:DefaultLinks runat="server" ID="DL_1" />
    <style>
        .c-grid-profile-pic {
            width: 120px;
            text-align: center;
            padding-top: 1% !important;
            padding-bottom: 1% !important;
            vertical-align: top;
        }

        .c-box-blueishwel {
            text-transform: uppercase !important;
            /*background: linear-gradient(45deg,#74B14E,#74B14E) !important;*/
             background: 	#8FBC8F;
        }

        .label-center {
            display: inline-block;
            text-align: right;
            float: left;
        }



        @media screen and (max-width:900px) {
            .c-btn-widht-15 {
                width: 100%;
                float: right;
            }

            .c-btn-widht-20 {
                width: 50%;
                float: right;
            }

            .c-col-size-40 {
                width: 100%;
            }

            .c-col-size-43 {
                width: 100%;
            }

            .c-col-size-8 {
                width: 8%;
                padding-left: 0px;
                padding-right: 0px;
            }
        }
    </style>

</head>
<body class="hold-transition skin-purple-light sidebar-mini fixed">
    <form id="form1" runat="server">
        <div class="wrapper">
            <SHD:StudentHeader runat="server" ID="SHD_1" />
            <SMNU:StudentMenuNavigation runat="server" ID="SMNU_1" />

            <%--content starts--%>
            <div class="content-wrapper">
                <div class="box box-solid box-info c-border-blueish c-container-box" style="margin-bottom: 0">


                    <%-- <div class="box-header c-box-blueish-new with-border">--%>

                    <div class="box-header c-box-blueish  with-border">
                        <asp:Label ID="Lbl_Header" runat="server" CssClass="box-title" Text="UKKGK"></asp:Label>
                    </div>

                    <%--box body start--%>
                    <div class="box-body c-padding-top-2">

                        <%--  <div class="col-md-12 c-inline-space">--%>
                        <div class="col-md-12">
                            <%--<asp:Label ID="Label3" runat="server" class="Label"><h1>Welcome</h1></asp:Label>--%>&nbsp;
                            <asp:Label ID="Lbl_Welcome" runat="server" CssClass="Label" Font-Bold="true" Font-Size="XX-Large"></asp:Label>

                        </div>

                        <div style="text-align: center">
                            <div class=" col-md-12 box box-solid box-info c-border-blueish c-box-blueishwel c-container-box">
                                <asp:Label ID="Label4" runat="server" Font-Size="Medium" ForeColor="WhiteSmoke"><b></b></asp:Label>
                            </div>

                        </div>
                        <%--  </div>--%>
                    </div>
                </div>
            </div>

            <FT:Footer runat="server" ID="FT_1" />
        </div>
    </form>
</body>
</html>
