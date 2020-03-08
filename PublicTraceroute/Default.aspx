<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="MainPage" %>
<%@ Register Src="~/GoogleMapForASPNet.ascx" TagName="GoogleMapForASPNet" TagPrefix="uc1" %>
<script runat="server">

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style3
        {
            height: 255px;
            width: 100%;
        }
        .style4
    {
        width: 100%;
    }
    .style5
    {
            width: 60%;
        }
    .style6
    {
        width: 1%;
    }
        .style7
        {
            height: 21px;
        }
    </style>
</asp:Content>

	  
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<!-- BEGIN geoiptool.com -->
<script language="JavaScript"
src="http://www.geoiptool.com/webjs.php?xl=en&xt=1&xw=200&xh=275"></script>
<noscript><a target="_blank" href="http://www.geoiptool.com/">Geo Web Tool</a></noscript>
<!-- END geoiptool.com -->   
    <table style="width:100%;">
    <tr>
        <td class="style3">
            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#003366" 
                Text="Select a server"></asp:Label>
            :
            <asp:DropDownList ID="DropDownList1" runat="server" Height="24px" Width="423px">
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
            <asp:Button ID="Button1" runat="server" ForeColor="#003366" 
                onclick="Button1_Click" Text="Show Route" Width="300px" />
            <br />
        </td>
        <td class="style4">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
             <br />

            </td>
    </tr>
    <tr>
        <td class="style5">
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Larger" 
                ForeColor="#003366" Text="Reverse Traceroute"></asp:Label>
        </td>
        <td class="style4">
            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Larger" 
                ForeColor="#003366" Text="Forward Traceroute"></asp:Label>
        </td>
        <td class="style6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3" class="style7">
        
      
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            </td>
    </tr>
    <tr>
        <td class="style5">
            <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple" 
                Visible="False" Width="200px" Height="500px" style="margin-bottom: 6px"></asp:ListBox>
        <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />

        </td>
        <td class="style4">
            <asp:ListBox ID="ListBox2" runat="server" SelectionMode="Multiple" 
                Visible="False" Width="200px" Height="500px" style="margin-left: 0px"></asp:ListBox>
        </td>
        <td class="style6">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style5">
        
      
    <h3><a href="Default.aspx">Back</a></h3>
    <h3>Internet Path</h3>
    <div>

    </div>
    
           </td>
        <td class="style4">
            &nbsp;</td>
    </tr>

</table>
</asp:Content>

