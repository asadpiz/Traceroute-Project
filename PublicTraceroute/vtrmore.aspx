﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vtrmore.aspx.cs" Inherits="vtrmore" %>
<%@ Register src="GoogleMapForASPNet.ascx" tagname="GoogleMapForASPNet" tagprefix="uc1" %>
<script src="Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
<script src="Scripts/highcharts.js" type="text/javascript"></script>

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
    .style7
    {
        text-decoration: none;
    }
    .style8
    {
        text-align: center;
        height: 0px;
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
    .style14
    {
        font-family: "Britannic Bold";
        font-size: xx-large;
        color: #003399;
    }
    .style15
    {
        color: #000000;
    }
    .style17
    {
        color: #FF0000;
    }
</style>
</head>

<body onload="frames['content'].scrollTo(10,10);">
<div id="logo">
    <asp:Image ID="Image2" runat="server" Height="132px" 
        ImageUrl="~/images/logo-984520.jpg" Width="308px" />
        <span class="style14"><strong>&nbsp;&nbsp;&nbsp; Upload</strong></span><a 
            class="style7" href="http://revtr.cs.washington.edu/"><span class="style1"><strong> 
        <span class="style17">Probes</span></strong></span></a></div>
<div id="menu">
	<ul>
		<li class="first"><a href="http://traceroute.cognet.seecs.nust.edu.pk/index.aspx" accesskey="1" title="">
            Home
		<li><a href="http://traceroute.cognet.seecs.nust.edu.pk/vtr.aspx" accesskey="2" title="">Traceroute</a></li>
        <li><a href="http://traceroute.cognet.seecs.nust.edu.pk/vrtr.aspx" accesskey="3" title="">Reverse Traceroute</a></li>
		<li><a href="http://traceroute.cognet.seecs.nust.edu.pk/publicserver.aspx" accesskey="4" title="">AS Topology</a></li>
		<li><a href="http://traceroute.cognet.seecs.nust.edu.pk/TopSites.aspx"accesskey="5" title="">Top 50 Sites Analysis</a></li>
		<li><a href="http://traceroute.cognet.seecs.nust.edu.pk/vtrmore.aspx"accesskey="7" title="">Upload Probes</a></li>
        <li><a href="http://traceroute.cognet.seecs.nust.edu.pk/about.aspx"accesskey="8" title="">About Us</a></li>
	</ul>
</div>
    <div class="style8">
        <br />
    <%--<div id="banner"><img src="images/banner.jpg" alt="" width="960" height="200" /></div>--%>
    </div>
<div id="page">
    <form id="form1" runat="server">
        <div>
               <ul class="style9">
                    <li class="style18"><strong><span class="style12">Alternatively Upload </span>
                        <span class="style21">Traceroute</span><span class="style12">/ </span>
                        <span class="style3">
                        <asp:HyperLink 
                        ID="HyperLink1" runat="server" 
            NavigateUrl="http://revtr.cs.washington.edu/" CssClass="style4">Reverse Traceroute</asp:HyperLink>
                        </span></strong><span class="style3"><strong><span class="style4">&nbsp;</span></strong></span><strong><span 
                        class="style11"><span class="style10">Results (.txt) 
                    Or Paste them in the Box Below to show them 
                    Graphically</span></span></strong></li></ul>
                    <div class="style20">
                    <strong>(Click Upload, before clicking &quot;Show Route&quot;)<br />
                        </strong>
                    &nbsp;<strong><asp:FileUpload id="FileUploadControl" runat="server" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="UploadButton" runat="server" OnClick="UploadButton_Click" CssClass="button" 
                Text="Upload" Height="22px" Width="80px" />
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

}</style>
                        &nbsp;&nbsp;&nbsp; 
                        <asp:Label runat="server" id="StatusLabel" text="Upload status: " />
                        &nbsp;</strong>

&nbsp;<span 
                   style="color: rgb(110, 110, 110); font-family: 'Trebuchet MS', Arial, Helvetica, sans-serif; font-size: 13px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: -webkit-auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); display: inline !important; float: none; ">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                        </span>&nbsp;<br />
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" 
                            CssClass="button" Text="Show Route" Height="29px" 
                    Width="129px" />
                        <style type="text/css">


                            .button
{
    background: url('images/oval-orange-right.gif');
    font-family: Arial, sans-serif;
    font-size: 12px;
    font-weight: bold;
    color: #001563;
                                text-align: left;
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
                            .style20
                            {
                                text-align: center;
                            }
                            .style21
                            {
                                font-family: "Trebuchet MS";
                                color: #003366;
                            }
                        </style>&nbsp;&nbsp;&nbsp;&nbsp;


 


                        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" 
                            CssClass="button" Text="Show More" Height="29px" 
                    Width="129px" />


 


                </div>


 


                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <br />

                <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server"  />
                <br />
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/legend.jpg" />
            <asp:TextBox ID="TextBox1" runat="server" Rows="40" TextMode="MultiLine" 
        Width="960px" AutoCompleteType="Notes" BackColor="#24486C" 
        BorderColor="#FFFF66" BorderStyle="Dotted" BorderWidth="1px" 
        style="margin-top: 0px;text-align:left" ToolTip="Paste Trace Results Here" ForeColor="White"></asp:TextBox>
            <br />
            <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                </div> 
      <div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </div>
    </form>
    </div>
    <div id="footer">
	<p class="legal"><span class="style15">Copyright (c) 2012 
        </span> 
        <a href="http://traceroute.cognet.seecs.nust.edu.pk/index.aspx" class="style15">ViZiTrace</a><span 
            class="style15">.&nbsp;All rights reserved.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Designed by 
        </span> <a href="http://www.nodethirtythree.com/" class="style15">NodeThirtyThree</a><span 
            class="style15"> + </span> <a href="http://nicecsstemplates.com/" 
            title="nice free css templates" class="style15">CSS Templates For Free</a></p>&nbsp;</div>
</body>
</html>
