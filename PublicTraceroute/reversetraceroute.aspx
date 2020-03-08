<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reversetraceroute.aspx.cs" Inherits="reversetraceroute" %>

<%@ Register src="GoogleMapForASPNet.ascx" tagname="GoogleMapForASPNet" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-family: Andalus;
            font-weight: bold;
            background-color: #66FF33;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <span class="style1">
    
        <h1>Reverse Traceroute (UW)</h1></span><br />
        <br />
        Enter Any Host&#39;s Name To Find Reverse Route from Host To A Vantage Point:<br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Width="354px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Show Reverse Traceroute" 
            Width="215px" Font-Bold="True" onclick="Button1_Click" />
        <br />
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    </div>
    <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />
    <asp:ListBox ID="ListBox1" runat="server" Width="1076px"></asp:ListBox>
    </form>
</body>
</html>
