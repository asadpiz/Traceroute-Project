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

public partial class MainPage : System.Web.UI.Page

{
    String strResult;
    WebResponse objResponse;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
            GoogleMapForASPNet1.GoogleMapObject.APIVersion = "3";
            GoogleMapForASPNet1.GoogleMapObject.Width = "1400px";
            GoogleMapForASPNet1.GoogleMapObject.Height = "400px";
            GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 3;
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("0", 0, 0);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string IP = "115.186.131.102";
        //////////////////////////////////////////////////////////REVERSE TRACEROUTE ///////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        string Result = DropDownList1.SelectedItem.Value.ToString();
       
        //////////////////////////////////////////GENERATING Directory Name//////////////////////////////////////////////////
        DateTime now = DateTime.Now;
        string timendate = DateTime.Now.ToString();
        Regex regex = new Regex(" ");
        timendate = regex.Replace(timendate, "_", 1);
        timendate = timendate.Replace("/", ".");
        timendate = timendate.Replace(":", "-");
        string dirname = timendate;
        System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/" + dirname));
        
        
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
                File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/revtr.txt"), strResult);
                publicRtr(dirname);
                Traceroute("128.125.137.243", dirname);
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
                File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/revtr.txt"), strResult);
                publicRtr(dirname);
                Traceroute("128.95.155.134", dirname);
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
                File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/revtr.txt"), strResult);
                publicRtr(dirname);
                Traceroute("194.85.4.53", dirname);
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
                File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/revtr.txt"), strResult);
                publicRtr(dirname);
                Traceroute("130.246.135.153", dirname);
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
                File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/revtr.txt"), strResult);
                publicRtr(dirname);
                Traceroute("204.119.27.10", dirname);
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
                File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/revtr.txt"), strResult);
                publicRtr(dirname);
                Traceroute("152.3.104.250", dirname);
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
                File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/revtr.txt"), strResult);
                publicRtr(dirname);
                Traceroute("216.251.225.207", dirname);
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
                File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/revtr.txt"), strResult);
                publicRtr(dirname);
                Traceroute("216.19.223.20", dirname);
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
                File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/revtr.txt"), strResult);
                publicRtr(dirname);
                Traceroute("69.16.132.7", dirname);
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
                File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/revtr.txt"), strResult);
                publicRtr(dirname);
                Traceroute("198.145.19.196", dirname);
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
                File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/revtr.txt"), strResult);
                publicRtr(dirname);
                Traceroute("195.8.0.51", dirname);
                break;
        }

    }
    ///////////////////////////////////////// Public Server Reverse TRACEROUTE //////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void publicRtr (string dirname)
    {
        System.IO.StreamReader rTrfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + dirname + "/revtr.txt"));
        string Rtext = rTrfile.ReadToEnd();
        rTrfile.Close();

        string Rlinepattern = @"(?<linegroup>[ \n]\d+  .*)";
        string Rrttpattern = @"(?<rttgroup>\d*[.]\d* ms)|(\d+ ms)";

        ////////////// IP & STAR patterns declared inside the Loop /////////////

        List<string> RipS = new List<string>();
        List<string> Rrttave = new List<string>();
        List<string> methodstr = new List<string>();

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
            Match method = Regex.Match(Rline, @"(( -?dst\r)|( -?sym\r)|( -?tr\r)|( -?rr\r)|( -?ts\r))");
            if (method.Success)
            {
                methodstr.Add(method.Value);
            }
            else
            {
                methodstr.Add("NA");
            }

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
                RipS.Add("Destination Unreachable");
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
        /////////////////////////// FINDING LONGITUDE & LATITUDE OF FORWARD IPS /////////////////////////////////////////////////
        List<string> Rlonlat = new List<string>();
        List<string> RASno = new List<string>();
        List<string> RASna = new List<string>();
        string server = "v4.whois.cymru.com";
        foreach (string item in RipS)
        {
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
                File.WriteAllText(Server.MapPath("~/App_Data/" + dirname + "/RLongLat.txt"), strResult);

                System.IO.StreamReader geoipfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + dirname + "/RLongLat.txt"));
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
                //////////////////////////////////FINDING AS no & Name ////////////////////////////


                string whois = whoisinfo(server, item);
                whois = whois + "end";
                ////////////////////// AS NO ////////////////////
                Match asnummatch = Regex.Match(whois, @"AS Name(\d)+ ");
                if (asnummatch.Success)
                {
                    string asno = asnummatch.Value;
                    char[] Rendchar = { 'A', 'S', ' ', 'N', 'a', 'm', 'e', '\n' };
                    asno = asno.TrimStart(Rendchar);
                    RASno.Add(asno);
                }
                else
                { RASno.Add("NA"); }
                ////////////////////////////////AS NAME ////////////////////////////////////////
                Match asnamematch = Regex.Match(whois, @"(\| [A-z][A-z][A-z].+?[^\|]end)");
                if (asnamematch.Success)
                {
                    string asna = asnamematch.Value;
                    char[] Rendchar = { 'e', 'n', 'd', '\n' };
                    asna = asna.TrimEnd(Rendchar);
                    char Rstart = '|';
                    asna = asna.TrimStart(Rstart);
                    RASna.Add(asna);
                }
                else
                { RASna.Add("NA"); }
            }
            else
            {
                Rlonlat.Add("0");
                Rlonlat.Add("0");
                RASna.Add("NA");
                RASno.Add("NA");
            }
        }
        int forward = 0;
        GenerateGMap(Rlonlat, RipS, Rrttave, dirname, methodstr, RASno, RASna,forward);
        
    }


    ///////////////////////////////////////// FORWARD TRACEROUTE //////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void Traceroute(string ipAddressOrHostName, string dirname)
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
                ListBox2.Visible = true;
                ListBox2.Items.Add(string.Format("{0}\t{1} \t{2} ms\n", i, pingReply.Address, stopWatch.ElapsedMilliseconds));  
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
        List<string> FASno   = new List<string>();
        List<string> FASna   = new List<string>();
        string server = "v4.whois.cymru.com";
        foreach (string item in FipS)
        {
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
                //////////////////////////////////FINDING AS no & Name ////////////////////////////

                
                string whois = whoisinfo(server, item);
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
            else
            {
                Flonlat.Add("0");
                Flonlat.Add("0");
                FASna.Add("NA");
                FASno.Add("NA");
            }
        }

        int forward = 1;
        GenerateGMap(Flonlat, FipS, Frttave, dirname, methodstr,FASno,FASna,forward);
    }

    void GenerateGMap(List<string> LongLat, List<string> FipS, List<string> Frttave, string dirname, List<string> methodstr, List<string> FASno, List<string> FASna, int forward)
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
                zeroballoonlist.Add("<tr><td width=10%>" + Convert.ToString(ind) + "</td><td width=35%>" + FipS[ind] + "</td><th width=25%>" + Frttave[ind] + "</td><td width=5%>" + methodstr[ind] + "</td><td width=10%>" + FASno[ind] + "</td><td width=15%>" + FASna[ind] + "</td></tr></table>");
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
                            if (forward == 1)
                            {
                                if (ind == 0)
                                {
                                    icon = "icons/database.png";
                                }
                                else if (ind == (Flonglatlist.Count - 1))
                                {
                                    icon = "icons/database.png";
                                }
                                else
                                {
                                    icon = "icons/wifi.png";
                                }
                            }
                         else   if (forward == 0)
                            {
                                if (ind == 0)
                                {
                                    icon = "icons/revstart.png";
                                }
                                else if (ind == (Flonglatlist.Count - 1))
                                {
                                    icon = "icons/revend.png";
                                }
                                else
                                {
                                    icon = "icons/rev.png";
                                }
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
                    if (forward == 1)
                    {
                        if (ind == 0)
                        {
                            icon = "icons/database.png";
                        }
                        else if (ind == (Flonglatlist.Count - 1))
                        {
                            icon = "icons/database.png";
                        }
                        else
                        {
                            icon = "icons/wifi.png";
                        }
                    }
                    else if (forward == 0)
                    {
                        if (ind == 0)
                        {
                            icon = "icons/revstart.png";
                        }
                        else if (ind == (Flonglatlist.Count - 1))
                        {
                            icon = "icons/revend.png";
                        }
                        else
                        {
                            icon = "icons/rev.png";
                        }
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
                            if (sss <= 100)
                            {
                                PL1.ColorCode = "#FFF014";
                            }
                            else if ((sss > 100) & (sss <= 200))
                            {
                                PL1.ColorCode = "#00FF78";
                            }
                            else if ((sss > 200) & (sss <= 300))
                            {
                                PL1.ColorCode = "#FFF014";
                            }
                            else if ((sss > 300) & (sss <= 400))
                            {
                                PL1.ColorCode = "#FF7814";
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
}


