﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TopSitesAnalysis.aspx.cs" Inherits="TopSitesAnalysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:DropDownList ID="DropDownList1" runat="server" Height="25px" Width="304px">
            <asp:ListItem>www.google.com</asp:ListItem>
            <asp:ListItem>www.facebook.com</asp:ListItem>
            <asp:ListItem>www.wikipedia.org</asp:ListItem>
            <asp:ListItem>www.youtube.com</asp:ListItem>
            <asp:ListItem>www.yahoo.com</asp:ListItem>
            <asp:ListItem>www.baidu.com</asp:ListItem>
            <asp:ListItem>www.live.com</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
    
    </div>
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
        Width="301px">
        <asp:ListItem>Show Last Day&#39;s Analysis</asp:ListItem>
        <asp:ListItem>Show Last Week&#39;s Analysis</asp:ListItem>
        <asp:ListItem>Show Last Month&#39;s Analysis</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    <br />
    <asp:ListBox ID="ListBox1" runat="server" Width="200"></asp:ListBox>
    </form>
</body>
</html>
