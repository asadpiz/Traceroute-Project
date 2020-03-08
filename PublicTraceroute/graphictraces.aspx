<%@ Page Language="C#" AutoEventWireup="true" CodeFile="graphictraces.aspx.cs" Inherits="graphictraces" %>

<%@ Register src="GoogleMapForASPNet.ascx" tagname="GoogleMapForASPNet" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-family: "Bauhaus 93";
            font-size: xx-large;
        }
        .style2
        {
            font-size: xx-large;
            font-family: "Bradley Hand ITC";
        }
    </style>
    <style type="text/css">
#container{
    width:400px;
    height:400px;
    border:1px solid #000; 
    overflow:hidden;
    margin:auto;
}
#container iframe {
    width:200px;
    height:750px;
    margin-left:-100px;
    margin-top:-350px;   
    border:0 solid;
 }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <span class="style1">Graphcial Forward/Reverse Traceroute</span><br />
        <br />
        Copy &amp; Paste the Traceroute/<asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="http://revtr.cs.washington.edu/">Reverse Traceroute</asp:HyperLink>
&nbsp;Results in the Box Below to show them Graphically<br />
        <br />
        <div id="container">
<iframe id="video" runat="server" title="YouTube video player" type="text/html" width="475" height="278" src="http://revtr.cs.washington.edu" frameborder="0"></iframe>
</div>
      <div align="center">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />
          <span class="style2">Paste Trace Results In this Box<br />
          </span>
          <asp:FileUpload id="FileUploadControl" runat="server" />
          <asp:Button runat="server" id="UploadButton" text="Upload" onclick="UploadButton_Click" />
    <br /><br />
    <asp:Label runat="server" id="StatusLabel" text="Upload status: " />
        </div>
    
    </div>
    <asp:TextBox ID="TextBox1" runat="server" Rows="40" TextMode="MultiLine" 
        Width="1081px" AutoCompleteType="Notes" BackColor="#00FF99" 
        BorderColor="#0066FF" BorderStyle="Solid" BorderWidth="5px" 
        style="margin-top: 0px" ToolTip="Paste Trace Results Here"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Height="45px" Text="Show Forward Route" 
        Width="165px" onclick="Button1_Click1" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server" Height="45px" Text="Show Reverse Route" 
        Width="165px" onclick="Button2_Click1" />
    <br />
    </form>
</body>
</html>
