<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_PrintFinishedGradingEvents.aspx.cs" Inherits="Reports_Form_PrintFinishedGradingEvents" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" CssClass="c-report" Font-Names="Calibri" Font-Size="10pt" SizeToReportContent="True" ZoomMode="FullPage">
        </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>
