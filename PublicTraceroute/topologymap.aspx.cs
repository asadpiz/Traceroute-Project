using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class topologymap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {     
        string Result = RadioButtonList1.SelectedItem.ToString();
        ////dEPENDING uPON REsult Take An AS from database and select 3 IP's from that AS and issue tr towards them
    }
}