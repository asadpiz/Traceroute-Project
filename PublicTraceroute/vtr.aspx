<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vtr.aspx.cs" Inherits="vtr" %>
<%@ Register src="GoogleMapForASPNet.ascx" tagname="GoogleMapForASPNet" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="content-type" content="text/html; charset=utf-8" />
<title>Visual Internet Trace</title>
<meta name="keywords" content="" />
<meta name="description" content="" />
<link href="style.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
        .style1
        {
            font-family: "Britannic Bold";
            font-size: xx-large;
            color: #000099;
        }
        </style>
<style type="text/css">
#outerdiv
{
width:620px;
height:190px;
overflow:hidden;
position:relative;
}

#inneriframe
{
position:absolute;
top:-350px;
left:-120px;
width:1280px;
height:1200px;
}
    .style2
    {
        font-family: sans-serif;
        color: #FF6666;
    }
    .style7
    {
        text-decoration: none;
    }
    .style8
    {
        text-align: center;
    }
    .style9
    {
        list-style-type: square;
    }
    .style10
    {
        color: #FF9900;
    }
    .style11
    {
        font-family: "Trebuchet MS";
    }
    .style12
    {
        font-family: "Trebuchet MS";
        color: #FF9900;
    }
    .style13
    {
        font-family: sans-serif;
        color: #FF9900;
    }
    .style14
    {
        font-family: "Britannic Bold";
        font-size: xx-large;
        color: #CCCCCC;
    }
    .style15
    {
        color: #000000;
    }
    .style16
    {
        font-family: sans-serif;
        color: #000000;
    }
</style>
</head>

<body onload="frames['content'].scrollTo(10,10);">
<div id="logo">
<h1><a href="#">ViZiTrace</a></h1>
</div>
<div id="menu">
	<ul>
		<li class="first"><a href="http://traceroute.cognet.seecs.nust.edu.pk/index.aspx" accesskey="1" title="">Home</a></li>
		<li><a href="http://traceroute.cognet.seecs.nust.edu.pk/vtr.aspx" accesskey="2" title="">Traceroute</a></li>
        <li><a href="http://traceroute.cognet.seecs.nust.edu.pk/vrtr.aspx" accesskey="3" title="">Reverse Traceroute</a></li>
		<li><a href="http://traceroute.cognet.seecs.nust.edu.pk/publicserver.aspx" accesskey="4" title="">AS Topology</a></li>
		<li><a href="http://traceroute.cognet.seecs.nust.edu.pk/TopSites.aspx"accesskey="5" title="">Top 50 Sites Analysis</a></li>
		<li><a href="http://traceroute.cognet.seecs.nust.edu.pk/usage.aspx" accesskey="6" title="">Usage</a></li>
        <li><a href="http://traceroute.cognet.seecs.nust.edu.pk/about.aspx"accesskey="7" title="">About Us</a></li>
	</ul>
</div>
    <div class="style8">
        <span class="style14"><strong>Visualize</strong></span><a 
            class="style7" href="http://revtr.cs.washington.edu/"><span class="style1"><strong> 
        Traceroute</strong></span></a><br />
    <%--<div id="banner"><img src="images/banner.jpg" alt="" width="960" height="200" /></div>--%>
    </div>
<div id="page">
    <form id="form1" runat="server">
   <div>
        <div>
            <ul class="style9">
                <li><span class="style13"><strong>Issue Traceroute To any Arbitrary Destination from our Server.</strong></span><br />&nbsp;<asp:TextBox ID="TextBox2" runat="server" Height="27px" Width="307px" 
             AutoCompleteType="Homepage" BackColor="Fuchsia"></asp:TextBox>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" Text="Show Trace" Width="74px" 
             Height="31px" BackColor="#FFCCFF" BorderColor="#FF99FF" BorderStyle="Solid" 
                CssClass="credit" EnableTheming="True" ForeColor="Black" SkinID="DodgerBlue" 
                ViewStateMode="Enabled" onclick="Button3_Click"  />
                    <br />
                    <br /></li>
            </ul>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
         <br />
    <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server"  />
            <br />
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/legend.jpg" />
            <ul class="style9">
                <li><strong><span class="style12">Alternatively Upload Traceroute </span><span 
                        class="style11"><span class="style10">Results (</span><span class="style15">.txt</span><span class="style10">) 
                    Or Paste them in the Box Below to show them 
                    Graphically</span></span></strong><br />
                    <br /></li>
            </ul>

      <div align="center">
          <span class="style2">&nbsp;</span><span class="style16">(Click &quot;<strong>Upload&quot;</strong> Before Clicking &quot;<strong>Show 
          Route Forward&quot;)</strong></span>
          <span class="style2"><br />
          </span>
          <asp:FileUpload id="FileUploadControl" runat="server" />
                  <asp:Button runat="server" id="UploadButton" text="Upload" onclick="UploadButton_Click" Width="74px" 
             Height="31px" BackColor="#666666" BorderColor="#669999" BorderStyle="Solid" 
                CssClass="credit" EnableTheming="True" ForeColor="Black" SkinID="DodgerBlue" 
                ViewStateMode="Enabled"/>
          <br />
    <asp:Label runat="server" id="StatusLabel" text="Upload status: " />
          <br />
          <br />
         <asp:Button ID="Button1" runat="server" Text="Show Forward Route" Width="128px" 
             Height="31px" BackColor="#666666" BorderColor="#669999" BorderStyle="Solid" 
                CssClass="credit" EnableTheming="True" ForeColor="Black" SkinID="DodgerBlue" 
                ViewStateMode="Enabled" onclick="Button1_Click1"/>
        </div>
    
    </div>
    <asp:TextBox ID="TextBox1" runat="server" Rows="40" TextMode="MultiLine" 
        Width="960px" AutoCompleteType="Notes" BackColor="#FFCC99" 
        BorderColor="#FFFF66" BorderStyle="Dotted" BorderWidth="1px" 
        style="margin-top: 0px" ToolTip="Paste Trace Results Here"></asp:TextBox>
    <br />
    <br />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
   </div>
    </form>
    </div>
    <div id="footer">
	<p class="legal"><span class="style15">Copyright (c) 2012 
        </span> 
        <a href="http://traceroute.cognet.seecs.nust.edu.pk/index.aspx" class="style15">ViZiTrace</a><span 
            class="style15">.&nbsp;All rights reserved.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Designed by 
        </span> <a href="http://www.nodethirtythree.com/" class="style15">NodeThirtyThree</a><span 
            class="style15"> + </span> <a href="http://nicecsstemplates.com/" 
            title="nice free css templates" class="style15">CSS Templates For Free</a></p>&nbsp;</div>
</body>
</html>
