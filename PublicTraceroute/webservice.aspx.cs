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

public partial class webservice : System.Web.UI.Page
{
    TraceRouteDataClassesDataContext _ObjDataClasses = new TraceRouteDataClassesDataContext();
    String strResult;
    WebResponse objResponse;
    protected void Page_Load(object sender, EventArgs e)
    {
        List<string> hostnames = new List<string>();
        hostnames.Add("www.google.com");
        hostnames.Add("www.facebook.com");
        hostnames.Add("www.wikipedia.org");
        hostnames.Add("www.youtube.com");
        hostnames.Add("www.yahoo.com");
        hostnames.Add("www.baidu.com");
        hostnames.Add("www.live.com");

        foreach (string item in hostnames)
        {
            routine(item);
        }

    }


    private void routine(string hostname)
    {
        string dirname = dattime();
        DateTime now = DateTime.Now;

        System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/" + dirname));
        string ipAddressOrHostName = hostname;

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

        List<string> FipS = new List<string>(); List<string> Frttave = new List<string>(); //List<string> methodstr = new List<string>();
        string Fip = ""; string Frtta = "";



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
                Fip = Fip + "_" + Fmatchms.ToString();

            }
            else
            {
                FipS.Add("Destination Unreachable");
                Fip = Fip + "_" + "Destination Unreachable";
                //Frttave.Add("0");
                Frtta = Frtta + "_" + "0";
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
                    //Frttave.Add(Rtrimst);
                    Frtta = Frtta + "_" + Rtrimst;
                }
                else
                { //Frttave.Add("0");
                Frtta = Frtta + "_" + "0";
                }
            }
        }

        /////////////////////////// FINDING LONGITUDE & LATITUDE OF FORWARD IPS /////////////////////////////////////////////////
        List<string> Flonlat = new List<string>(); string flonl = "";
        List<string> FASno = new List<string>(); string fasn = "";
        List<string> FASna = new List<string>(); string fasnam = "";
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
                    //Flonlat.Add(lltrimst);
                    flonl = flonl + "_" + lltrimst;
                    found_match = found_match + 1;
                }
                if (found_match != 2) // If Longitude Latitude are not found by GEOIPtool
                {
                    flonl = flonl + "_" + "0";
                    flonl = flonl + "_" + "0";
                }
                //////////////////////////////////FINDING AS no & Name ////////////////////////////

                string whois = whoisinfo(server, item);
                if (whois == "sNA")
                {
                    FASno.Add("sNA");
                    fasn = fasn + "_" + "sNA";
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
                        fasn = fasn + "_" + asno;
                    }
                    else
                    { fasn = fasn + "_" + "NA"; }
                    ////////////////////////////////AS NAME ////////////////////////////////////////
                    Match asnamematch = Regex.Match(whois, @"(\| [A-z][A-z][A-z].+?[^\|]end)");
                    if (asnamematch.Success)
                    {
                        string asna = asnamematch.Value;
                        char[] Fendchar = { 'e', 'n', 'd', '\n' };
                        asna = asna.TrimEnd(Fendchar);
                        char Fstart = '|';
                        asna = asna.TrimStart(Fstart);

                        fasnam = fasnam + "_" + asna;
                    }
                    else
                    {fasnam = fasnam + "_" + "NA";}
                }
            }
            else
            {

                flonl = flonl + "_" + "0";
                flonl = flonl + "_" + "0";

                fasnam = fasnam + "_" + "NA";
                fasn = fasn + "_" + "NA";
            }
        }

        string asd = Fip;
        string asdd = Frtta;
        string asddd = flonl;
        string asdddd = fasn;
        string addddddd = fasnam;

        Route objRoute = new Route();
        objRoute.ip = Fip;
        objRoute.longlat = flonl; objRoute.RTT = Frtta; objRoute.ASName = fasnam; objRoute.ASNumber = fasn; objRoute.WebsiteID = 1; objRoute.Date_Time = now;

        _ObjDataClasses.Routes.InsertOnSubmit(objRoute);
        _ObjDataClasses.SubmitChanges();

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
