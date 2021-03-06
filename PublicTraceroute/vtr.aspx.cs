﻿using System;
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


public partial class vtr : System.Web.UI.Page
{

    String strResult;
    WebResponse objResponse;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["AIzaSyD5wy_45yUBtsUlxasj6ms-mM55bjyTN_I"];
            GoogleMapForASPNet1.GoogleMapObject.APIVersion = "3";
            GoogleMapForASPNet1.GoogleMapObject.Width = "960px";
            GoogleMapForASPNet1.GoogleMapObject.Height = "400px";
            GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 3;
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("0", 0, 0);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {/////////////IGNORE THIS ///////////////////////////////////


        string dirname = dattime();
        System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/" + dirname));
        ////////////Insert whether valid data is entered or not
        string ipAddressOrHostName = TextBox2.Text;

        IPAddress ipAddress = Dns.GetHostEntry(ipAddressOrHostName).AddressList[0];
        StringBuilder traceResults = new StringBuilder();
        using (Ping pingSender = new Ping())
        {
           
            PingOptions pingOptions = new PingOptions();
            Stopwatch stopWatch = new Stopwatch();
            byte[] bytes = new byte[32];
            pingOptions.DontFragment = true;
            pingOptions.Ttl = 1;
            int maxHops = 30;
            traceResults.AppendLine(string.Format("Tracing route to {0}\n over a maximum of {1} hops:", ipAddress, maxHops));
            for (int i = 1; i < maxHops + 1; i++)
            {
                stopWatch.Reset();
                stopWatch.Start();
                PingReply pingReply = pingSender.Send(ipAddress, 5000, new byte[32], pingOptions);
                stopWatch.Stop();
                File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/ftr.txt"), string.Format("{0}\t{1}ms \t{2}\n", i, stopWatch.ElapsedMilliseconds, pingReply.Address + Environment.NewLine));
                if (pingReply.Status == IPStatus.Success)
                {
                    File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/ftr.txt"), "Trace complete.");
                    break;
                }
                pingOptions.Ttl++;
            }
        }
        System.IO.StreamReader FTrfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + dirname + "/ftr.txt"));
        string Ftext = FTrfile.ReadToEnd();
        FTrfile.Close();

        List<string> FipS = new List<string>(); List<string> Frttave = new List<string>(); List<string> methodstr = new List<string>();


        string Flinepattern = @"(\d+[\t].*)";
        string FIP = @"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
        string Frttpattern = @"[\t]\d+ms [\t]";

        MatchCollection Rtrline = Regex.Matches(Ftext, Flinepattern);
        foreach (Match Rlinematch in Rtrline)
        {
            string eline = Rlinematch.Value;
            int star = 0;
            Match method = Regex.Match(eline, @"(( -?dst\r)|( -?sym\r)|( -?tr\r)|( -?rr\r)|( -?ts\r))");
            if (method.Success)
            {
                methodstr.Add(method.Value);
            }
            else
            {
                methodstr.Add("NA");
            }
            Match Fmatchms = Regex.Match(eline, FIP);
            Match Frtt = Regex.Match(eline, Frttpattern);

            if (Fmatchms.Success)
            {
                FipS.Add(Fmatchms.ToString());
            }
            else
            {
                FipS.Add("Destination Unreachable");
                Frttave.Add("0");
                star = 1;
            }

            if (star != 1)
            {
                if (Frtt.Success)
                {
                    string Rrtttemp = Frtt.Value;
                    char Rendchar = '\t';
                    string Rtrimen = Rrtttemp.TrimStart(Rendchar);
                    string Rtrimst = Rtrimen.TrimEnd(Rendchar);
                    Frttave.Add(Rtrimst);
                }
                else
                { Frttave.Add("0"); }
            }
        }

/////////////////// IGNORE EVERYTHING ABOVE, we have just created a list of IP addresses FipS above //////////////////////////////////////////////////


/// Below we pick 1 IP from the created list and find its longitude latitude and store longitude and latitude in a list Flonlat
/// first element of list is longitude, next element latitude, then next longitude and so on.
        /////////////////////////// FINDING LONGITUDE & LATITUDE OF FORWARD IPS /////////////////////////////////////////////////
        List<string> Flonlat = new List<string>();
        List<string> FASno = new List<string>();
        List<string> FASna = new List<string>();
        string server = "v4.whois.cymru.com";
        string longi = "";
        string lati = "";
        int IDpp = 60;
        int k = 0;
        int Frepeat = 0;
        int Frep2 = 1;
        int zeroflag = 0;
        double longg = 0;
        double lat = 0;
        int ind = 0;

        List<string> Flonglatlist = new List<string>();
        List<string> zeroballoonlist = new List<string>();
        List<double> rttcompare = new List<double>();
        foreach (string item in FipS)
        {
            int lo = 0;
            if (item != "Destination Unreachable")
            {
                string URL1 = "http://www.geoiptool.com/en/?IP=IPADD";
                URL1 = URL1.Replace("IPADD", item);
                WebRequest objRequest = HttpWebRequest.Create(URL1);
                objResponse = objRequest.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
                File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/FLongLat.txt"), strResult);

                System.IO.StreamReader geoipfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + dirname + "/FLongLat.txt"));
                string FLLtext = geoipfile.ReadToEnd();
                geoipfile.Close();
                int found_match = 0;
                MatchCollection Flonglat = Regex.Matches(FLLtext, @"(>-?\d{1,3}<)|(>-?\d{1,3}[.]\d{0,4}<)");
                foreach (Match Fll in Flonglat)
                {
                    string lltemp = Fll.Value;
                    lltemp = lltemp.Replace("<", "");
                    lltemp = lltemp.Replace(">", "");
                    Flonlat.Add(lltemp);
                    if (lo == 0)
                    {
                        longi = lltemp;
                        lo++;
                    }
                    else
                    {
                        lati = lltemp;
                        Flonglatlist.Add(longi + "_" + lati);
                    }
                    found_match = found_match + 1;
                }
                if (found_match != 2) // If Longitude Latitude are not found by GEOIPtool
                {
                    Flonlat.Add("0");
                    Flonlat.Add("0");
                        longi = "0";
                        lati = "0";
                    Flonglatlist.Add(longi + "_" + lati);
                }
                //////////////////////////////////FINDING AS no & Name ////////////////////////////

                string whois = whoisinfo(server, item);
                if (whois == "sNA")
                { FASno.Add("sNA");
                FASna.Add("sNA");
                }
                else
                {
                    whois = whois + "end";
                    ////////////////////// AS NO ////////////////////
                    Match asnummatch = Regex.Match(whois, @"AS Name(\d)+ ");
                    if (asnummatch.Success)
                    {
                        string asno = asnummatch.Value;
                        char[] Fendchar = { 'A', 'S', ' ', 'N', 'a', 'm', 'e', '\n' };
                        asno = asno.TrimStart(Fendchar);
                        FASno.Add(asno);
                    }
                    else
                    { FASno.Add("NA"); }
                    ////////////////////////////////AS NAME ////////////////////////////////////////
                    Match asnamematch = Regex.Match(whois, @"(\| [A-z][A-z][A-z].+?[^\|]end)");
                    if (asnamematch.Success)
                    {
                        string asna = asnamematch.Value;
                        char[] Fendchar = { 'e', 'n', 'd', '\n' };
                        asna = asna.TrimEnd(Fendchar);
                        char Fstart = '|';
                        asna = asna.TrimStart(Fstart);
                        FASna.Add(asna);
                    }
                    else
                    { FASna.Add("NA"); }
                }
            }
            else
            {
                Flonlat.Add("0");
                Flonlat.Add("0");
                FASna.Add("NA");
                FASno.Add("NA");
                longi = "0";
                lati = "0";
               Flonglatlist.Add(longi + "_" + lati);

            }
///////////////// NOW WE WANT TO ADD POINTS TO GOOGLE MAP //////////
            Frep2 = 1;
            if (longi=="0" && lati =="0")
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

                                icon = "icons/wifi.png";
                                string idpps = Convert.ToString(IDpp);

///////////////// /////////////////THIS IS THE POINT WHERE MAP SHOULD BE UPDATED //////////////////////////
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

                    if (Frepeat==0)
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
                            icon = "icons/wifi.png";


                        string idpps = Convert.ToString(IDpp);

/////////////////THIS IS THE POINT WHERE MAP SHOULD BE UPDATED //////////////////////////

                        GoogleMapForASPNet1.GoogleMapObject.Points.Add(new GooglePoint(idpps, lat, longg, icon, Fdesctext));
                        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint(idpps, lat, longg);
                    }
                    IDpp = IDpp + 1;
                }

            ind++;
        }
        GenerateGMap(Flonlat, FipS, Frttave, dirname, methodstr, FASno, FASna);
    }
/// THIS FUNCTION JUST GENERATES THE LINES BETWEEN POINTS
    void GenerateGMap(List<string> LongLat, List<string> FipS, List<string> Frttave, string dirname, List<string> methodstr, List<string> FASno, List<string> FASna)
    {
        //GoogleMapForASPNet1.GoogleMapObject.Polylines.Clear();
        //GoogleMapForASPNet1.GoogleMapObject.Points.Clear();
        List<double> LonLat = LongLat.Select(x => double.Parse(x)).ToList();

        int m = 0;
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

    private string whoisinfo(string whoisServer, string url)
    {
        try
        {
            StringBuilder whoisresult = new StringBuilder();
            TcpClient whoisclient = new TcpClient(whoisServer, 43);
            NetworkStream whoisnetworkstream = whoisclient.GetStream();
            BufferedStream whoisbufferedstream = new BufferedStream(whoisnetworkstream);
            StreamWriter streamWriter = new StreamWriter(whoisbufferedstream);
            streamWriter.WriteLine(url);
            streamWriter.Flush();

            StreamReader streamReaderReceive = new StreamReader(whoisbufferedstream);

            while (!streamReaderReceive.EndOfStream)
            { whoisresult.Append(streamReaderReceive.ReadLine()); }
            return whoisresult.ToString();

        }
        catch (SocketException)
        {
            string whoisresult = "sNA";
            return whoisresult;
        }
    }
    private string dattime()
    {
        DateTime now = DateTime.Now;
        string timendate = DateTime.Now.ToString();
        Regex regex = new Regex(" ");
        timendate = regex.Replace(timendate, "_", 1);
        timendate = timendate.Replace("/", ".");
        timendate = timendate.Replace(":", "-");
        string dirname = timendate;
        return dirname;
    }
}