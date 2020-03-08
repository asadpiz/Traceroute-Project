<%@ Page Title="" Language="C#" MasterPageFile="~/sitemaster.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Default2" %>

<%-- Add content controls here --%>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div id="page">
	<!-- start content -->
		<div class="post">
			<h2 class="title"><a href="#">Welcome to </a>
                <span style="font-family: Georgia; color: #0000FF"><a href="#">
                <span style="color: #0000FF">V</span></a>isual</span>
                <span style="font-family: Georgia; color: #00FF00">Internet</span>
                <span style="font-family: Georgia; color: #FF9900">Trace</span></h2>
			<div class="entry">
				<h3 style="font-family: Georgia; color: #FF9900">A free Internet Traceroute/Reverse Traceroute Visualizer</h3>
				<img src="images/logo-984520.jpg" alt="" width="132" height="72" class="left" />
				<p>Here at <strong>ViZiTrace</strong>, You can Issue both Forward &amp; Reverse Traceroutes and display 
                    the results on Google Maps.</p>
				<p><strong>ViZiTrace</strong> Visualizes Internet Routes on Google Maps. Showing Exact Geographical 
                    Locations of Hops Encountered. You can Furhter Analyze the Internet Paths by 
                    showing the <strong>RTTs</strong>,<strong> VoIP possibility</strong> &amp; 
                    <strong>Jitter</strong>.</p>
                <p style="text-align: center; font-family: 'Trebuchet MS'">
                    <span class="style1">To better Understand &amp; Utilize our tool kindly go through the
                    </span>
                    <a href="http://traceroute.cognet.seecs.nust.edu.pk/usage.aspx" class="style1">Usage</a><span 
                        class="style1"> Page.</span></p>
                <p style="text-align: center; font-family: 'Trebuchet MS'">
                    &nbsp;</p>
			</div>
		</div>
	</div>
 

</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .style1
        {
            color: #669999;
        }
    </style>
</asp:Content>

