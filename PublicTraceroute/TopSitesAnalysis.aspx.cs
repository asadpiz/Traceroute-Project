using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TopSitesAnalysis : System.Web.UI.Page
{
    TraceRouteDataClassesDataContext _ObjDataClasses = new TraceRouteDataClassesDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        List<GetRoutesResult> routeList = _ObjDataClasses.GetRoutes().ToList<GetRoutesResult>();
        string ip = ""; string asName = ""; string asNumber = ""; string longilati = ""; DateTime routeDate; string rtt = "";
        
        string selection = RadioButtonList1.SelectedItem.ToString();
        switch (selection)
        {
            case "Show Last Day's Analysis":
            {
                string strYesterday = DateTime.Now.AddDays(-1).ToString("u");

                string[] words = strYesterday.Split(' ');

                DateTime dateYesterday = DateTime.Parse(words[0]);

                var lastDayResults = from obj in routeList where obj.Date_Time == dateYesterday  select obj;
                foreach (var route in lastDayResults)
                {
                    ip = route.ip;
                    asName = route.ASName;
                    asNumber = route.ASNumber;
                    longilati = route.longlat;
                    routeDate = (DateTime)route.Date_Time;
                    rtt = route.RTT;

                }
                ListBox1.Items.Add(ip);
                break;
            }
            case "Show Last Week's Analysis":
            {

                ListBox1.Items.Add("Week");
                string strYesterday = DateTime.Now.AddDays(-7).ToString("u");

                string[] words = strYesterday.Split(' ');

                DateTime dateYesterday = DateTime.Parse(words[0]);

                var lastDayResults = from obj in routeList where obj.Date_Time == dateYesterday select obj;
                foreach (var route in lastDayResults)
                {
                    ip = route.ip;
                    asName = route.ASName;
                    asNumber = route.ASNumber;
                    longilati = route.longlat;
                    routeDate = (DateTime)route.Date_Time;
                    rtt = route.RTT;

                }
                ListBox1.Items.Add(ip);
                break;
            }
            case "Show Last Month's Analysis":
            {
                string strYesterday = DateTime.Now.AddDays(-30).ToString("u");

                string[] words = strYesterday.Split(' ');

                DateTime dateYesterday = DateTime.Parse(words[0]);

                var lastDayResults = from obj in routeList where obj.Date_Time == dateYesterday select obj;
                foreach (var route in lastDayResults)
                {
                    ip = route.ip;
                    asName = route.ASName;
                    asNumber = route.ASNumber;
                    longilati = route.longlat;
                    routeDate = (DateTime)route.Date_Time;
                    rtt = route.RTT;

                }
                ListBox1.Items.Add(ip);
                break;
            }
        }
    }
}