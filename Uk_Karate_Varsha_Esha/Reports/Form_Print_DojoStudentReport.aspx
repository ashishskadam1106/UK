<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_Print_DojoStudentReport.aspx.cs" Inherits="Reports_Form_Print_DojoStudentReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="c-report" Font-Names="Calibri"
                Font-Size="10pt" ShowPrintButton="true" AsyncRendering="false" ZoomMode="FullPage"
                SizeToReportContent="true">
            </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
