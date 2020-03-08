<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vrtr.aspx.cs" Inherits="vrtr" %>
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
    .style6
    {
        font-family: "Britannic Bold";
        font-size: xx-large;
        color: #003366;
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
    .style14
    {
        color: #000000;
    }
    .style15
    {
        font-family: "Britannic Bold";
        font-size: xx-large;
        color: #CCCCCC;
    }
    .style16
    {
        color: #003366;
    }
    .style17
    {
        font-family: "Britannic Bold";
        font-size: xx-large;
        color: #FF0000;
    }
</style>
</head>

<body onload="frames['content'].scrollTo(10,10);">
<div id="logo">
    <asp:Image ID="Image2" runat="server" Height="132px" 
        ImageUrl="~/images/logo-984520.jpg" Width="308px" />
        <strong>
        <span class="style15">&nbsp;&nbsp;&nbsp; <span class="style16">Visualize</span></span><span 
        class="style6"> </span> </strong><a 
            class="style7" href="http://revtr.cs.washington.edu/">
    <span class="style17"><strong>Reverse</strong></span><strong><span class="style1"> </span>
        <span class="style17">Traceroute</span></strong></a></div>
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
        <br />
    <%--<div id="banner"><img src="images/banner.jpg" alt="" width="960" height="200" /></div>--%>
    </div>
<div id="page">
    <form id="form1" runat="server">
   <div>
        <div class="style18">
            <ul class="style9">
                <li class="style19"><strong>Issue <a class="style7" 
                        href="http://revtr.cs.washington.edu/">Reverse Traceroute</a> From Any Arbitrary Destination towards anyone of the Following 3 Servers 
&nbsp;</strong></li>
                </ul>
            <div class="style18">
            <span class="style10"><strong>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            Destination:<asp:TextBox ID="TextBox2" runat="server" 
                        AutoCompleteType="Homepage" BackColor="White" BorderStyle="Solid" 
                        ForeColor="Black" Width = "300px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Source:</strong></span>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="22px" Width="300px">
                        <asp:ListItem Value="79">planetlab1.sfc.wide.ad.jp (203.178.143.10)</asp:ListItem>
                        <asp:ListItem Value="111">planetlab5.cs.uiuc.edu (72.36.112.78)</asp:ListItem>
                        <asp:ListItem Value="161">planetlab-2.cs.auckland.ac.nz (130.216.1.23)</asp:ListItem>
                    </asp:DropDownList>
                    <span class="style10"><strong>
                    <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <asp:Button ID="btnSearch" runat="server" OnClick="Button4_Click" CssClass="button" 
                Text="Request Measurement" Height="22px" Width="201px" />
      <style type="text/css">
          .button
{
    background: url('images/oval-orange-right.gif');
    font-family: Arial, sans-serif;
    font-size: 12px;
    font-weight: bold;
    color: #001563;
          }

.button:hover
{
    background: url('images/oval-orange-right.gif');
    border: solid 1px grey;
    font-family: Arial, sans-serif;
    font-size: 12px;
    font-weight: bold;
    color: Red;   
    height: 25px;

}
          .style18
          {
              text-align: left;
          }
          .style19
          {
              text-align: left;
              color: #FF9900;
          }
      </style>
                     </div>
                     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">     
     <Triggers>
        <asp:AsyncPostBackTrigger  ControlID="Timer1" EventName="Tick" />
    </Triggers>
       <ContentTemplate>
            <asp:Label ID="Label1" runat="server" Text="" Width = "300px" Height = "20px"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
         <asp:Timer ID="Timer1" runat="server" ontick="Timer1_Tick" Interval="5000" Enabled="false">
        </asp:Timer>
         <br />
    <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server"  />
            <br />
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/legend.jpg" />
    
    </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
    </form>
    </div>
    <div id="footer">
	<p class="legal"><span class="style14">Copyright (c) 2012 
        </span> 
        <a href="http://traceroute.cognet.seecs.nust.edu.pk/index.aspx" class="style14">ViZiTrace</a><span 
            class="style14">.&nbsp;All rights reserved.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Designed by 
        </span> <a href="http://www.nodethirtythree.com/" class="style14">NodeThirtyThree</a><span 
            class="style14"> + </span> <a href="http://nicecsstemplates.com/" 
            title="nice free css templates" class="style14">CSS Templates For Free</a></p>&nbsp;</div>
</body>
</html>
