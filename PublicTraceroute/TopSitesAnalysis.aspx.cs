using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Net.NetworkInformation;
using VRK.Net;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

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
                    ///////////// SPLIT STRING Send to Gmap

                }
                ListBox1.Items.Add(ip);
                break;
            }
            case "Show Last Week's Analysis":
            {

                ListBox1.Items.Add("Week");
                for (int wee = 0; wee < 7; wee++)
                {
                    string strYesterday = DateTime.Now.AddDays(-wee).ToString("u");

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
                }

                break;
            }
            case "Show Last Month's Analysis":
            {
                for (int wee = 0; wee < 30; wee++)
                {
                    string strYesterday = DateTime.Now.AddDays(-wee).ToString("u");

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
                }

                break;
            }
        }
    }
    void GenerateGMap(List<string> LongLat, List<string> FipS, List<string> Frttave, string dirname, List<string> methodstr, List<string> FASno, List<string> FASna)
    {

        List<double> LonLat = LongLat.Select(x => double.Parse(x)).ToList();

        int m = 0;
        int IDpp = 60;
        int k = 0;
        int Frepeat = 0;
        int Frep2 = 1;
        int zeroflag = 0;
        double longg = 0;
        double lat = 0;
        string ll1 = "0";
        string ll2 = "0";

        List<string> Flonglatlist = new List<string>();
        List<string> zeroballoonlist = new List<string>();
        List<double> rttcompare = new List<double>();

        foreach (double prime in LonLat)
        {
            if (m < 1)
            {
                longg = prime;
                m = 1;
                ll1 = Convert.ToString(longg);
            }
            else if (m > 0)
            {
                lat = prime;
                ll2 = Convert.ToString(lat);
                ll2 = ll1 + "_" + ll2;
                Flonglatlist.Add(ll2);
                m = 0;
            }
        }

        for (int ind = 0; ind < Flonglatlist.Count; ind++)
        {
            Frep2 = 1;
            if (Flonglatlist[ind] == "0_0")
            {
                zeroballoonlist.Add("<tr><td width=10%>" + Convert.ToString(ind) + "</td><td width=35%>" + FipS[ind] + "</td><th width=25%>" + Frttave[ind] + "</td><td width=5%>" + methodstr[ind] + "</td><td width=10%>" + FASno[ind] + "</td><td width=15%>" + FASna[ind] + "</td></tr>");
                zeroflag = 1;
            }
            else
            {
                if (ind > 0)
                {
                    k = 0;
                    while ((k < ind) & (Frep2 > 0))
                    {
                        if (Flonglatlist[ind] == Flonglatlist[k])
                        {

                            string filename = "FDesc" + k.ToString() + ".txt";
                            System.IO.StreamReader replac = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + dirname + "/" + filename));
                            string replactabl = replac.ReadToEnd();
                            replactabl = replactabl.Replace("</table>", "");
                            replac.Close();
                            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), replactabl);
                            if (zeroflag == 1)
                            {
                                foreach (string inf in zeroballoonlist)
                                {
                                    File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), inf);
                                }
                                zeroballoonlist.Clear();
                                string nonzeroinfo = "<tr><td width=10%>" + Convert.ToString(ind) + "</td><td width=35%>" + FipS[ind] + "</td><th width=25%>" + Frttave[ind] + "</td><td width=5%>" + methodstr[ind] + "</td><td width=10%>" + FASno[ind] + "</td><td width=15%>" + FASna[ind] + "</td></tr></table>";
                                File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), nonzeroinfo);
                                zeroflag = 0;
                            }
                            else
                            {
                                string ballooninfo = "<tr><td width=10%>" + Convert.ToString(ind) + "</td><td width=35%>" + FipS[ind] + "</td><th width=25%>" + Frttave[ind] + "</td><td width=5%>" + methodstr[ind] + "</td><td width=10%>" + FASno[ind] + "</td><td width=15%>" + FASna[ind] + "</td></tr></table>";
                                File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), ballooninfo);
                            }

                            System.IO.StreamReader descfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + dirname + "/" + filename));
                            string Fdesctext = descfile.ReadToEnd();
                            descfile.Close();

                            string[] words = Flonglatlist[ind].Split('_');
                            longg = Convert.ToDouble(words[0]);
                            lat = Convert.ToDouble(words[1]);
                            string icon = "";
                            if (ind == 0)
                            {
                                icon = "icons/greenflag3.png";
                            }
                            else if (ind == (Flonglatlist.Count - 1))
                            {
                                icon = "icons/greenflag3.png";
                            }
                            else
                            {
                                icon = "icons/wifi.png";
                            }
                            string idpps = Convert.ToString(IDpp);
                            GoogleMapForASPNet1.GoogleMapObject.Points.Add(new GooglePoint(idpps, lat, longg, icon, Fdesctext));
                            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint(idpps, lat, longg);
                            Frepeat = Frepeat + 1;
                            Frep2 = 0;
                        }
                        else
                        {
                            k = k + 1;
                            Frepeat = 0;
                        }
                    }
                }

                if (Frepeat == 0)
                {

                    string filename = "FDesc" + ind.ToString() + ".txt";
                    string heading = "<table width=100% table border=1 cellspacing=3><tr><th width=10%>Hop No</th><th width=35%>IP Address</th><th width=25%>RTT Average</th><th width=5%>Method</th><th width=10%>AS No</th><th width=15%>AS</th></tr>";
                    File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), heading);
                    if (zeroflag == 1)
                    {
                        foreach (string inf in zeroballoonlist)
                        {
                            File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), inf);
                        }
                        zeroballoonlist.Clear();
                        string nonzeroinfo = "<tr><td width=10%>" + Convert.ToString(ind) + "</td><td width=35%>" + FipS[ind] + "</td><th width=25%>" + Frttave[ind] + "</td><td width=5%>" + methodstr[ind] + "</td><td width=10%>" + FASno[ind] + "</td><td width=15%>" + FASna[ind] + "</td></tr></table>";
                        File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), nonzeroinfo);
                        zeroflag = 0;
                    }
                    else
                    {
                        string ballooninfo = "<tr><td width=10%>" + Convert.ToString(ind) + "</td><td width=35%>" + FipS[ind] + "</td><th width=25%>" + Frttave[ind] + "</td><td width=5%>" + methodstr[ind] + "</td><td width=10%>" + FASno[ind] + "</td><td width=15%>" + FASna[ind] + "</td></tr></table>";
                        File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), ballooninfo);
                    }

                    System.IO.StreamReader descfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + dirname + "/" + filename));
                    string Fdesctext = descfile.ReadToEnd();
                    descfile.Close();

                    string[] words = Flonglatlist[ind].Split('_');
                    longg = Convert.ToDouble(words[0]);
                    lat = Convert.ToDouble(words[1]);
                    string icon = "";
                    if (ind == 0)
                    {
                        icon = "icons/greenflag3.png";
                    }
                    else if (ind == (Flonglatlist.Count - 1))
                    {
                        icon = "icons/greenflag3.png";
                    }
                    else
                    {
                        icon = "icons/wifi.png";
                    }

                    string idpps = Convert.ToString(IDpp);
                    GoogleMapForASPNet1.GoogleMapObject.Points.Add(new GooglePoint(idpps, lat, longg, icon, Fdesctext));
                    GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint(idpps, lat, longg);
                }
                IDpp = IDpp + 1;
            }


        }
        for (int indx = 0; indx < Frttave.Count; indx++)
        {
            string Rrtttemp = Frttave[indx];
            string[] numbers = Regex.Split(Rrtttemp, @"\D+");
            foreach (string value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    rttcompare.Add(i);
                }
            }
        }

        int idline = 0;

        for (int indx = 0; indx < Flonglatlist.Count; indx++)
        {
            if (indx != 0)
            {
                if (Flonglatlist[indx] == "0_0")
                {
                    continue;
                }
                else
                {
                    int prevll = indx;
                    prevll = prevll - 1;
                    while (prevll >= 0)
                    {
                        if (Flonglatlist[prevll] == "0_0")
                        {
                            prevll = prevll - 1;
                        }
                        else
                        {
                            idline = idline + 1;
                            string idl = Convert.ToString(idline);
                            GooglePolyline PL1 = new GooglePolyline();
                            double sss = rttcompare[indx];
                            if (sss <= 80)
                            {
                                PL1.ColorCode = "#FFF014";
                            }
                            else if ((sss > 80) & (sss <= 160))
                            {
                                PL1.ColorCode = "#00FF78";
                            }
                            else if ((sss > 160) & (sss <= 240))
                            {
                                PL1.ColorCode = "#3C78FF";
                            }
                            else if ((sss > 240) & (sss <= 320))
                            {
                                PL1.ColorCode = "#FF7814";
                            }
                            else if ((sss > 320) & (sss <= 400))
                            {
                                PL1.ColorCode = "#14FFF0";
                            }
                            else if (sss > 400)
                            {
                                PL1.ColorCode = "#FF0014";
                            }

                            PL1.ID = "PL1";
                            PL1.Width = 5;
                            string[] words2 = Flonglatlist[prevll].Split('_');
                            double longg0 = Convert.ToDouble(words2[0]);
                            double lat0 = Convert.ToDouble(words2[1]);
                            PL1.Points.Add(new GooglePoint(idl, lat0, longg0));

                            string[] words = Flonglatlist[indx].Split('_');
                            double longg1 = Convert.ToDouble(words[0]);
                            double lat1 = Convert.ToDouble(words[1]);
                            PL1.Points.Add(new GooglePoint(idl, lat1, longg1));
                            GoogleMapForASPNet1.GoogleMapObject.Polylines.Add(PL1);
                            break;

                        }
                    }
                }
            }
        }

    }
}