<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Uploader.aspx.cs" Inherits="NCRVisual.Web.Uploader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblStatus" runat="server" 
                Text="Please choose a text file(you can browse output folder)"></asp:Label><br />
            <asp:FileUpload ID="fuInput" runat="server" /><br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                onclick="btnSubmit_Click" />
        </div>
        <br />
        <a href="NCRVisualTestPage.aspx">Go to visualization page</a>
    </form>
</body>
</html>
