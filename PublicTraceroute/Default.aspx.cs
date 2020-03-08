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

public partial class MainPage : System.Web.UI.Page
{
    String strResult;
    WebResponse objResponse;
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void Button1_Click(object sender, EventArgs e)
    {

        string IP = "39.47.91.63";
        //////////////////////////////////////////////////////////REVERSE TRACEROUTE ///////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        string Result = DropDownList1.SelectedItem.Value.ToString();
        switch (Result)
        {

            case "University of Southern California (AS47)	Los Angeles, CA, US":
                string PsURL3 = "http://www.usc.edu/cgi-bin/traceroute?target=myIP";
                PsURL3 = PsURL3.Replace("myIP", IP);
                WebRequest objRequest3 = HttpWebRequest.Create(PsURL3);
                objResponse = objRequest3.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }

                File.WriteAllText(Server.MapPath("~/App_Data/revtr.txt"), strResult);
                Traceroute("64.81.30.194");
                break;
            case "University of Washington (AS73) Seattle, WA, US":
                string PsURL4 = "http://www.washington.edu/networking/tools/traceroute?search_address=myIP&max_hop_value=30&wait_time_value=2&number_query_value=3&number_out_value=3&Submit=Submit&.cgifields=number_query&.cgifields=wait_time&.cgifields=numeric_address&.cgifields=number_out&.cgifields=max_hop";
                PsURL4 = PsURL4.Replace("myIP", IP);
                WebRequest objRequest4 = HttpWebRequest.Create(PsURL4);
                objResponse = objRequest4.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
                File.WriteAllText(Server.MapPath("~/App_Data/revtr.txt"), strResult);
                Traceroute("140.142.5.5");
                break;
            case "RUSnet (AS3277) Saint Petersburg, 66, RU":
                string PsURL5 = "http://traceroute.rusnet.ru/?myIP";
                PsURL5 = PsURL5.Replace("myIP", IP);
                WebRequest objRequest5 = HttpWebRequest.Create(PsURL5);
                objResponse = objRequest5.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
                File.WriteAllText(Server.MapPath("~/App_Data/revtr.txt"), strResult);
                Traceroute("194.85.4.53");
                break;
            case "Daresbury Laboratory (AS786) Didcot, K2, GB":
                string PsURL6 = "http://icfamon.dl.ac.uk/cgi-bin/traceroute.pl?target=myIP";
                PsURL6 = PsURL6.Replace("myIP", IP);
                WebRequest objRequest6 = HttpWebRequest.Create(PsURL6);
                objResponse = objRequest6.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
                File.WriteAllText(Server.MapPath("~/App_Data/revtr.txt"), strResult);
                Traceroute("130.246.135.153");
                break;
            case "Willamette Valley Internet (AS2914) Stayton, OR, US":
                string PsURL7 = "http://www.wvi.com/cgi-bin/trace?myIP";
                PsURL7 = PsURL7.Replace("myIP", IP);
                WebRequest objRequest7 = HttpWebRequest.Create(PsURL7);
                objResponse = objRequest7.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
                File.WriteAllText(Server.MapPath("~/App_Data/revtr.txt"), strResult);
                Traceroute("204.119.27.10");
                break;
            case "Stanford University (AS3671) Durham, NC, US":
                string PsURL8 = "http://www.slac.stanford.edu/cgi-bin/nph-traceroute.pl?target=myIP&function=traceroute";
                PsURL8 = PsURL8.Replace("myIP", IP);
                WebRequest objRequest8 = HttpWebRequest.Create(PsURL8);
                objResponse = objRequest8.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
                File.WriteAllText(Server.MapPath("~/App_Data/revtr.txt"), strResult);
                Traceroute("152.3.104.250");
                break;
            case "Conxion (AS4544) Newton Center, MA, US":
                string PsURL9 = "http://unixvirt-svca.www.conxion.com/cgi-bin/traceroute?source=svca&target=myIP&probes=3&hops=20&timeout=2&format=graphic&.submit=Trace+another+connection";
                PsURL9 = PsURL9.Replace("myIP", IP);
                WebRequest objRequest9 = HttpWebRequest.Create(PsURL9);
                objResponse = objRequest9.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
                File.WriteAllText(Server.MapPath("~/App_Data/revtr.txt"), strResult);
                Traceroute("216.251.225.207");
                break;
            case "GetNet (AS6091) Phoenix, AZ, US":
                string PsURL10 = "http://www.getnet.net/cgi-bin/trace?myIP";
                PsURL10 = PsURL10.Replace("myIP", IP);
                WebRequest objRequest10 = HttpWebRequest.Create(PsURL10);
                objResponse = objRequest10.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
                File.WriteAllText(Server.MapPath("~/App_Data/revtr.txt"), strResult);
                Traceroute("216.19.223.20");
                break;
            case "Easynews (AS11588) Phoenix, AZ, US":
                string PsURL11 = "http://www.easynews.com/trace.html?ip=myIP";
                PsURL11 = PsURL11.Replace("myIP", IP);
                WebRequest objRequest11 = HttpWebRequest.Create(PsURL11);
                objResponse = objRequest11.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
                File.WriteAllText(Server.MapPath("~/App_Data/revtr.txt"), strResult);
                Traceroute("69.16.132.7");
                break;
            case "INFLOW (AS13756/19290) Vancouver, WA, US":
                string PsURL12 = "http://vger.kernel.org/cgi-bin/nph-traceroute?ASQ=on&OWN=on&MODE=ICMP&DOMAIN=myIP";
                PsURL12 = PsURL12.Replace("myIP", IP);
                WebRequest objRequest12 = HttpWebRequest.Create(PsURL12);
                objResponse = objRequest12.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
                File.WriteAllText(Server.MapPath("~/App_Data/revtr.txt"), strResult);
                Traceroute("198.145.19.196");
                break;
            case "Companhia Portuguesa Radio Marconi (AS8657) Lisboa, 14, PT":
                string PsURL13 = "http://glass.cprm.net/cgi-bin/traceroute.cgi?myIP";
                PsURL13 = PsURL13.Replace("myIP", IP);
                WebRequest objRequest13 = HttpWebRequest.Create(PsURL13);
                objResponse = objRequest13.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
                File.WriteAllText(Server.MapPath("~/App_Data/revtr.txt"), strResult);
                Traceroute("195.8.0.51");
                break;

        }


        System.IO.StreamReader rTrfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/revtr.txt"));
        string Rtext = rTrfile.ReadToEnd();
        rTrfile.Close();

        string Rlinepattern = @"(?<linegroup>[ \n]\d+  .*)";
        string Rrttpattern = @"(?<rttgroup>\d*[.]\d* ms)|(\d+ ms)";

        ////////////// IP & STAR patterns declared inside the Loop /////////////

        List<string> RipS = new List<string>();
        List<string> Rrttave = new List<string>();

        /// !!!!!!!!!!!!!!!!!!! HOP FLAG !!!!!!!!!!!!!!!!!!!!!!!!!! ////////////////////////////////////////

        MatchCollection Rtrline = Regex.Matches(Rtext, Rlinepattern);
        foreach (Match Rlinematch in Rtrline)
        {
            float Rsumm = 0;
            int Rstar = 0;
            int Rlength = 0;
            float RRave = 0;
            float Rrtt11 = 0;

            string Rline = Rlinematch.Value;
            ListBox1.Visible = true;
            ListBox1.Items.Add(Rline);

            ////////////////////////////////////// Extract IPs Line-By-Line ///////////////////////////////////////////////////////////////////////

            Match Rmatchip = Regex.Match(Rline, @"\(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\)");
            if (Rmatchip.Success)
            {
                string Riptemp = Rmatchip.Value;
                char Rendchar = ')';
                char Rstartchar = '(';
                string Rtrimen = Riptemp.TrimEnd(Rendchar);
                string Rtrimst = Rtrimen.TrimStart(Rstartchar);
                RipS.Add(Rtrimst);
            }
            /////////////////////////////////////////// Case for '*' ///////////////////////////////

            Match Rmatchstar = Regex.Match(Rline, @"(\d)+  \* \* \*");
            if (Rmatchstar.Success)
            {
                RipS.Add("0");
                Rrttave.Add("0");
                Rstar = 1;
            }

            //////////////////////////////////////////// Extract RTTs & Calculate Average RTT Line-by-Line ////////////
            if (Rstar != 1)
            {
                MatchCollection RtrRTT = Regex.Matches(Rline, Rrttpattern);
                foreach (Match Rrttmatch in RtrRTT)
                {
                    string Rrtttemp = Rrttmatch.Value;
                    char[] Rendchar = { ' ', 'm', 's' };
                    string Rtrimen = Rrtttemp.TrimEnd(Rendchar);
                    Rrtt11 = float.Parse(Rtrimen);
                    Rlength = Rlength + 1;
                    Rsumm = Rsumm + Rrtt11;
                }
                RRave = Rsumm / Rlength;
                string Rrttaverage = RRave.ToString();
                Rrttaverage = Rrttaverage + " ms";
                Rrttave.Add(Rrttaverage);
            }

        }
        //////////////////////////// FINDING LONGITUDE & LATITUDE OF EACH IP ////////////////////////////////////////////////////
        List<string> Rlonlat = new List<string>();
        foreach (string item in RipS)
        {
            if (item != "0")
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
                File.WriteAllText(Server.MapPath("~/App_Data/RLongLat.txt"), strResult);

                System.IO.StreamReader geoipfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/RLongLat.txt"));
                string RLLtext = geoipfile.ReadToEnd();
                geoipfile.Close();
                int found_match = 0;
                MatchCollection Rlonglat = Regex.Matches(RLLtext, @"(>-?\d{1,3}<)|(>-?\d{1,3}[.]\d{0,4}<)");
                foreach (Match Rll in Rlonglat)
                {
                        string lltemp = Rll.Value;
                        char Rendchar = '<';
                        char Rstartchar = '>';
                        string lltrimen = lltemp.TrimEnd(Rendchar);
                        string lltrimst = lltrimen.TrimStart(Rstartchar);
                        Rlonlat.Add(lltrimst);
                        found_match = found_match + 1;
                }
                if (found_match != 2) // If Longitude Latitude are not found by GEOIPtool
                {
                    Rlonlat.Add("0");
                    Rlonlat.Add("0");
                }


            }
            else
            {
                Rlonlat.Add("0");
                Rlonlat.Add("0");
            }
        }

        //System.IO.StreamReader geoipfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/RLongLat.txt"));
        //string RLLtext = geoipfile.ReadToEnd();
        //geoipfile.Close();

        //MatchCollection Rlonglat = Regex.Matches(RLLtext, @"(?<linegroup>[\n].*)");
        //foreach (Match Rlinematch in Rlonglat)
        //{
        //    string eline = Rlinematch.Value;
        //    Match Fmatchms = Regex.Match(eline, @"(>-?\d{1,3}<)|(>-?\d{1,3}[.]\d{0,4}<)");
        //    if (Fmatchms.Success)
        //    {
        //        string lltemp = Fmatchms.Value;
        //        char Rendchar = '<';
        //        char Rstartchar = '>';
        //        string lltrimen = lltemp.TrimEnd(Rendchar);
        //        string lltrimst = lltrimen.TrimStart(Rstartchar);
        //        Rlonlat.Add(lltrimst);
        //    }

        //}
        GenerateRMap(Rlonlat, RipS, Rrttave);
    }


    /////////////////////////////////////////// FORWARD TRACEROUTE //////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void Traceroute(string ipAddressOrHostName)
    {
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
                ListBox2.Visible = true;
                ListBox2.Items.Add(string.Format("{0}\t{1} ms\t{2}\n", i, stopWatch.ElapsedMilliseconds, pingReply.Address));
                File.AppendAllText(Server.MapPath("~/App_Data/ftr.txt"), string.Format("{0}\t{1}ms \t{2}\n", i, stopWatch.ElapsedMilliseconds, pingReply.Address + Environment.NewLine));
                if (pingReply.Status == IPStatus.Success)
                {
                    File.AppendAllText(Server.MapPath("~/App_Data/ftr.txt"), "Trace complete.");
                    break;
                }
                pingOptions.Ttl++;
            }
        }

        System.IO.StreamReader FTrfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/ftr.txt"));
        string Ftext = FTrfile.ReadToEnd();
        FTrfile.Close();

        List<string> FipS = new List<string>(); List<string> Frttave = new List<string>();


        string Flinepattern = @"(\d+[\t].*)";
        string FIP = @"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
        string Frttpattern = @"[\t]\d+ms [\t]";

        MatchCollection Rtrline = Regex.Matches(Ftext, Flinepattern);
        foreach (Match Rlinematch in Rtrline)
        {
            string eline = Rlinematch.Value;
            int star = 0;

            Match Fmatchms = Regex.Match(eline, FIP);
            Match Frtt = Regex.Match(eline, Frttpattern);

            if (Fmatchms.Success)
            {
                FipS.Add(Fmatchms.ToString());
                // ListBox1.Items.Add(Fmatchms.ToString());
            }
            else
            {
                FipS.Add("0");
                Frttave.Add("0");
                // ListBox2.Items.Add("NO RTT FOR U");
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
            }
        }

        /////////////////////////// FINDING LONGITUDE & LATITUDE OF FORWARD IPS /////////////////////////////////////////////////
        List<string> Flonlat = new List<string>();
        foreach (string item in FipS)
        {
            if (item != "0")
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
                File.WriteAllText(Server.MapPath("~/App_Data/FLongLat.txt"), strResult);

                System.IO.StreamReader geoipfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/FLongLat.txt"));
                string FLLtext = geoipfile.ReadToEnd();
                geoipfile.Close();
                int found_match = 0;
                MatchCollection Flonglat = Regex.Matches(FLLtext, @"(>-?\d{1,3}<)|(>-?\d{1,3}[.]\d{0,4}<)");
                foreach (Match Fll in Flonglat)
                {
                    string lltemp = Fll.Value;
                    char Fendchar = '<';
                    char Fstartchar = '>';
                    string lltrimen = lltemp.TrimEnd(Fendchar);
                    string lltrimst = lltrimen.TrimStart(Fstartchar);
                    Flonlat.Add(lltrimst);
                    found_match = found_match + 1;
                }
                if (found_match != 2) // If Longitude Latitude are not found by GEOIPtool
                {
                    Flonlat.Add("0");
                    Flonlat.Add("0");
                }


            }
            else
            {
                Flonlat.Add("0");
                Flonlat.Add("0");
            }
        }


        GenerateFMap(Flonlat, FipS, Frttave);
    }


    void GenerateFMap(List<string> Flonlat, List<string> FipS, List<string> Frttave)
    {
        List<double> test = Flonlat.Select(x => double.Parse(x)).ToList();
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        GoogleMapForASPNet1.GoogleMapObject.APIVersion = "3";
        GoogleMapForASPNet1.GoogleMapObject.Width = "800px";
        GoogleMapForASPNet1.GoogleMapObject.Height = "400px";
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 14;

        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 43.65669, -79.45278);

        GooglePolyline PL1 = new GooglePolyline();

        PL1.ID = "PL1";
        PL1.ColorCode = "#000000";
        PL1.Width = 5;


        int m = 0;
        int IDpp = 0;
        double longg = 0;
        double lat = 0;

        string ll1 = "0";
        string ll2 = "0";
        List<string> longlatlist = new List<string>();

        int k = 0;
        int repeat = 0;
        int rep2 = 1;


        foreach (double prime in test)
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
                longlatlist.Add(ll2);
                m = 0;
            }
        }
        for (int ind = 0; ind < longlatlist.Count; ind++)
        {
            rep2 = 1;
            if (ind > 0)
            {
                k = 0;
                while ((k < ind) & (rep2 > 0))
                {
                    if (longlatlist[ind] == longlatlist[k])
                    {
                        string filename = "Desc" + k.ToString() + ".txt";
                        string ballooninfo = "<pre>" + Convert.ToString(ind) + "\t\t\t" + FipS[ind] + "\t\t\t" + "\t\t\t" + Frttave[ind] + "\n</pre>";
                        File.AppendAllText(Server.MapPath("~/App_Data/" + filename), ballooninfo);

                        System.IO.StreamReader descfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + filename));
                        string Fdesctext = descfile.ReadToEnd();
                        descfile.Close();

                        string[] words = longlatlist[ind].Split('_');
                        longg = Convert.ToDouble(words[0]);
                        lat = Convert.ToDouble(words[1]);

                        string idpps = Convert.ToString(IDpp);
                        GoogleMapForASPNet1.GoogleMapObject.Points.Add(new GooglePoint(idpps, lat, longg, "", Fdesctext));
                        PL1.Points.Add(new GooglePoint(idpps, lat, longg));
                        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint(idpps, lat, longg);
                        repeat = repeat + 1;
                        rep2 = 0;
                    }
                    else
                    {
                        k = k + 1;
                        repeat = 0;
                    }
                }
            }

            if (repeat == 0)
            {
                string filename = "Desc" + ind.ToString() + ".txt";
                string heading = "<pre>\nHop No.\t\t\tIP Address\t\t\tAverage RTT\n";
                string ballooninfo = "<pre>" + Convert.ToString(ind) + "\t\t\t" + FipS[ind] + "\t\t\t" + Frttave[ind] + "\n</pre>";
                File.AppendAllText(Server.MapPath("~/App_Data/" + filename), heading);
                File.AppendAllText(Server.MapPath("~/App_Data/" + filename), ballooninfo);

                System.IO.StreamReader descfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + filename));
                string Fdesctext = descfile.ReadToEnd();
                descfile.Close();

                string[] words = longlatlist[ind].Split('_');
                longg = Convert.ToDouble(words[0]);
                lat = Convert.ToDouble(words[1]);

                string idpps = Convert.ToString(IDpp);
                GoogleMapForASPNet1.GoogleMapObject.Points.Add(new GooglePoint(idpps, lat, longg, "", Fdesctext));
                PL1.Points.Add(new GooglePoint(idpps, lat, longg));
                GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint(idpps, lat, longg);
            }
            IDpp = IDpp + 1;

        }

        GoogleMapForASPNet1.GoogleMapObject.Polylines.Add(PL1);
    }

    void GenerateRMap(List<string> Rlonlat, List<string> RipS, List<string> Rrttave)
    {
        List<double> test = Rlonlat.Select(x => double.Parse(x)).ToList();

        GooglePolyline PL2 = new GooglePolyline();
        PL2.ID = "PL2";
        //Give Hex code for line color
        PL2.ColorCode = "#0000FF";
        //Specify width for line
        PL2.Width = 10;


        int m = 0;
        int km = 0;
        int IDpp = 60;
        double longg = 0;
        double lat = 0;
        foreach (double prime in test) // Loop through List with foreach
        {
            if (m < 1)
            {
                longg = prime;
                m = 1;
            }
            else if (m > 0)
            {
                lat = prime;
                m = 0;
                km = 1;
            }
            if (km > 0)
            {
                string idpps = Convert.ToString(IDpp);
                GoogleMapForASPNet1.GoogleMapObject.Points.Add(new GooglePoint(idpps, lat, longg, "", ""));
                PL2.Points.Add(new GooglePoint(idpps, lat, longg));
                GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint(idpps, lat, longg);
                km = 0;
                IDpp = IDpp - 1;
            }

        }
        GoogleMapForASPNet1.GoogleMapObject.Polylines.Add(PL2);
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
}


