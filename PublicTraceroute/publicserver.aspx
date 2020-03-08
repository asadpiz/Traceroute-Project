<%@ Page Title="" Language="C#" MasterPageFile="~/sitemaster.master" AutoEventWireup="true" CodeFile="publicserver.aspx.cs" Inherits="publicserver" %>
<%@ Register Src="~/GoogleMapForASPNet.ascx" TagName="GoogleMapForASPNet" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            color: #666666;
            font-family: "Trebuchet MS";
            font-size: medium;
            text-decoration: blink;
            font-weight: bold;
            text-align: center;
            background-color: #CCCCFF;
        }
        .style2
        {
            text-align: center;
            font-family: "Trebuchet MS";
            font-size: medium;
            color: #666666;
            background-color: #CCCCFF;
        }
        .style3
        {
            background-color: #00CCFF;
        }
        .style4
        {
            background-color: #CCCCFF;
        }
        .style5
        {
            background-color: #CCCCFF;
            width: 302px;
        }
        .style6
        {
            width: 571px;
        }
        .style7
        {
            width: 570px;
        }
        .style8
        {
            width: 405px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="form1" runat="server">
 <table>
<tr>
<td>
<tr>
        <td class="style3">
            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#003366" 
                Text="Select a server"></asp:Label>
           : <asp:DropDownList ID="DropDownList1" runat="server" Height="24px" Width="423px">
                <asp:ListItem>University of Southern California (AS47)	Los Angeles, CA, US</asp:ListItem>
                <asp:ListItem>University of Washington (AS73) Seattle, WA, US</asp:ListItem>
                <asp:ListItem>RUSnet (AS3277) Saint Petersburg, 66, RU</asp:ListItem>
                <asp:ListItem>Daresbury Laboratory (AS786) Didcot, K2, GB</asp:ListItem>
                <asp:ListItem>Willamette Valley Internet (AS2914) Stayton, OR, US</asp:ListItem>
                <asp:ListItem>Stanford University (AS3671) Durham, NC, US</asp:ListItem>
                <asp:ListItem>Conxion (AS4544) Newton Center, MA, US</asp:ListItem>
                <asp:ListItem>GetNet (AS6091) Phoenix, AZ, US</asp:ListItem>
                <asp:ListItem>Easynews (AS11588) Phoenix, AZ, US</asp:ListItem>
                <asp:ListItem>INFLOW (AS13756/19290) Vancouver, WA, US</asp:ListItem>
                <asp:ListItem>Companhia Portuguesa Radio Marconi (AS8657) Lisboa, 14, PT</asp:ListItem>
            </asp:DropDownList>
&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" ForeColor="#003366" 
                onclick="Button1_Click" Text="Show Route" Width="207px" />
            <br />
        </td>
        <td class="style4">
            &nbsp;</td>
        <td class="style4">
            &nbsp;</td>
    </tr>
</td>
</tr>
</table>  
    <table style="width:100%;">
    
   
    <tr>
        <td colspan="4" class="style7">
        
      <div align="center">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager></div>
            </td>
          
  
    </tr>
    <tr>
    <td colspan="4">
    <div align="center">
    <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />
    </div>
    
    </td>
    </tr>

    <tr>
   
        <td class="style8">
        <div align="right" class="style2">
            <strong><span class="style4">Reverse Route</span></strong></div>
       
        <div align="right">
            
            <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple" 
                Visible="False" Width="450px" Height="300px" 
                style="margin-bottom: 6px; margin-right: 0px;" ViewStateMode="Enabled"></asp:ListBox>
                </div>
        

        </td>
        <td class="style5">
        <div align="left" class="style1">
             Forward Route</div>
        <div align="left">
           
            <asp:ListBox ID="ListBox2" runat="server" SelectionMode="Multiple" 
                Visible="False" Width="450px" Height="300px" 
                style="margin-left: 0px; margin-bottom: 6px;" ViewStateMode="Enabled"></asp:ListBox>
                </div>
            
        </td>
        <td class="style6">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style8">
        
      
    <div>

        <div id="outerdiv">
        </div>

    </div>
    
           </td>
           </tr>
           <tr>
        <td class="style9" colspan="2">
        <div align="center">
        <!-- BEGIN geoiptool.com -->
<script language="JavaScript"
src="http://www.geoiptool.com/webjs.php?xl=en&xt=1&xw=200&xh=275"></script>
<noscript><a target="_blank" href="http://www.geoiptool.com/">Geo Web Tool</a></noscript>
<!-- END geoiptool.com --> </div>
            </td>
        
    </tr>

</table>
    </form>
</asp:Content>

