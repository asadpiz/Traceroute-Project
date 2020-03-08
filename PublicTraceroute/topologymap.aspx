<%@ Page Language="C#" AutoEventWireup="true" CodeFile="topologymap.aspx.cs" Inherits="topologymap" %>

<%@ Register src="GoogleMapForASPNet.ascx" tagname="GoogleMapForASPNet" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
       <h1> Regional Topology Mapper</h1><br />
        <br />
        <br />
        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
            <asp:ListItem>ARICA (AFRInic)</asp:ListItem>
            <asp:ListItem>ASIA-Pacific (APNIC)</asp:ListItem>
            <asp:ListItem>North America (ARIN)</asp:ListItem>
            <asp:ListItem>South America (LACNIC)</asp:ListItem>
            <asp:ListItem>Europe (RIPE)</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Find Toplogy" 
            Width="154px" />
        <br />
        <br />
    
    </div>
        
      
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />
    </form>
</body>
</html>
