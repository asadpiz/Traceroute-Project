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
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;


public partial class vtr : System.Web.UI.Page
{
      
    String strResult;
    WebResponse objResponse;
    int uploaded = 2; 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
            GoogleMapForASPNet1.GoogleMapObject.APIVersion = "3";
            GoogleMapForASPNet1.GoogleMapObject.Width = "960px";
            GoogleMapForASPNet1.GoogleMapObject.Height = "500px";
            GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 3;
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("0", 0, 0);
        }
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        string dirname = dattime();
        Session["theText"] = dirname;

        System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/" + dirname));
        if (FileUploadControl.HasFile)
        {
            try
            {
                if (FileUploadControl.PostedFile.ContentType == "text/plain")
                {
                    if (FileUploadControl.PostedFile.ContentLength < 10240)
                    {
                        string filename = Path.GetFileName(FileUploadControl.FileName);
                        FileUploadControl.SaveAs(Server.MapPath("~/App_Data/" + dirname + "/tr.txt"));
                        StatusLabel.Text = "Upload status:Success- File uploaded!";
                        uploaded = 1;
                        Session["uploadflag"] = uploaded;
                    }
                    else
                        StatusLabel.Text = "Upload status:Failed- The file has to be less than 10 kb!";
                }
                else
                    StatusLabel.Text = "Upload status:Failed- Only .txt files are accepted!";
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Upload status:Failed- The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }

    }
    ///////// FTR Uploaded /////////////////////////////////////////
    protected void Button1_Click1(object sender, EventArgs e)
    {
        TextBox1.Text  = String.Empty; ///////// Empty The Text Box
        string dirname = (string)Session["theText"];
        int uploaded   = (int)Session["uploadflag"];
        string Ftext   = "";

        if (uploaded == 1)
        {
            System.IO.StreamReader FTrfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + dirname + "/tr.txt"));
            Ftext = FTrfile.ReadToEnd();
            FTrfile.Close();
            TextBox1.Text = Ftext;
        }
        
        string Flinepattern = @"(?<linegroup>[ \n]\d+  .*)";
        string Frttpattern = @"(?<rttgroup>\d*[.]\d* ms)|(\d+ ms)";

        List<string> FipS = new List<string>();
        List<string> Frttave = new List<string>();
        List<string> methodstr = new List<string>();

        /// !!!!!!!!!!!!!!!!!!! HOP FLAG !!!!!!!!!!!!!!!!!!!!!!!!!! ////////////////////////////////////////

        MatchCollection Ftrline = Regex.Matches(Ftext, Flinepattern);
        foreach (Match Flinematch in Ftrline)
        {
            float Fsumm = 0;
            int Fstar = 0;
            int Flength = 0;
            float FRave = 0;
            float Frtt11 = 0;

            string Fline = Flinematch.Value;

            Match method = Regex.Match(Fline, @"(( -?dst\r)|( -?sym\r)|( -?tr\r)|( -?rr\r)|( -?ts\r))");
            if (method.Success)
            {
                methodstr.Add(method.Value);
            }
            else
            {
                methodstr.Add("NA");
            }

            ////////////////////////////////////// Extract IPs Line-By-Line ///////////////////////////////////////////////////////////////////////

            Match Fmatchip = Regex.Match(Fline, @"((\[\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\])|(  \d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3} ))");
            if (Fmatchip.Success)
            {
                string Fiptemp = Fmatchip.Value;
                char Fendchar = ']';
                char Fstartchar = '[';
                string Ftrimen = Fiptemp.TrimEnd(Fendchar);
                string Ftrimst = Ftrimen.TrimStart(Fstartchar);
                FipS.Add(Ftrimst);
            }
            else
            {
                FipS.Add("Destination Unreachable");
                Frttave.Add("0");
                Fstar = 1;
            }
            //////////////////////////////////////////// Extract RTTs & Calculate Average RTT Line-by-Line ////////////
            if (Fstar != 1)
            {
                MatchCollection FtrRTT = Regex.Matches(Fline, Frttpattern);
                foreach (Match Frttmatch in FtrRTT)
                {
                    string Frtttemp = Frttmatch.Value;
                    char[] Fendchar = { ' ', 'm', 's' };
                    string Ftrimen = Frtttemp.TrimEnd(Fendchar);
                    Frtt11 = float.Parse(Ftrimen);
                    Flength = Flength + 1;
                    Fsumm = Fsumm + Frtt11;
                }
                FRave = Fsumm / Flength;
                string Frttaverage = FRave.ToString();
                Frttaverage = Frttaverage + " ms";
                Frttave.Add(Frttaverage);
            }

        }

        /////////////////////////// FINDING LONGITUDE & LATITUDE OF FORWARD IPS /////////////////////////////////////////////////
        List<string> Flonlat = new List<string>();
        List<string> FASno = new List<string>();
        List<string> FASna = new List<string>();
        string server = "v4.whois.cymru.com";
        string asno = "";
        string asna = "";
        int whoisnotworking = 0;
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
                if (whoisnotworking == 0)
                {
                    string whois = whoisinfo(server, item);
                    if (whois == "sNA")
                    {
                        FASna.Add("sNA");
                        FASno.Add("sNA");
                        whoisnotworking = 5;
                    }
                    else
                    {
                        whoisnotworking = 0;
                        whois = whois + "end";
                        Match asnummatch = Regex.Match(whois, @"AS Name(\d)+ ");
                        if (asnummatch.Success)
                        {
                            asno = asnummatch.Value;
                            char[] Fendchar = { 'A', 'S', ' ', 'N', 'a', 'm', 'e', '\n' };
                            asno = asno.TrimStart(Fendchar);
                            FASno.Add(asno);
                        }
                        else
                        { FASno.Add("NA"); }

                        Match asnamematch = Regex.Match(whois, @"(\| [A-z][A-z][A-z].+?[^\|]end)");
                        if (asnamematch.Success)
                        {
                            asna = asnamematch.Value;
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
                    FASna.Add("NA");
                    FASno.Add("NA");
                }

            }
            else
            {
                Flonlat.Add("0");
                Flonlat.Add("0");
                FASna.Add("NA");
                FASno.Add("NA");
            }
        }
       TextBox1.Visible= true;
       Session["AsName"] = FASna;
       Session["Rttave"] = Frttave;

        GenerateGMap(Flonlat, FipS, Frttave, dirname, methodstr, FASno, FASna);

    }
    //////////// ISSUE LIVE TRACERT     //////////////////////////
    protected void Button3_Click(object sender, EventArgs e)
    {
        TextBox1.Text = String.Empty;
        string dirname = dattime();
        System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/" + dirname));
        ////////////Insert whether valid data is entered or not
        string ipAddressOrHostName = TextBox2.Text;

        IPAddress ipAddress = Dns.GetHostEntry(ipAddressOrHostName).AddressList[0];
        StringBuilder traceResults = new StringBuilder();
        using (Ping pingSender = new Ping())
        {
            TextBox1.Text = "\n\n";
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
                TextBox1.Text = TextBox1.Text + string.Format("{0}\t{1} \t{2} ms\n", i, pingReply.Address, stopWatch.ElapsedMilliseconds);
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

        /////////////////////////// FINDING LONGITUDE & LATITUDE OF FORWARD IPS /////////////////////////////////////////////////
        List<string> Flonlat = new List<string>();
        List<string> FASno = new List<string>();
        List<string> FASna = new List<string>();
        string server = "v4.whois.cymru.com";
        string asno = "";
        string asna = "";
        int whoisnotworking = 0;
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
                if (whoisnotworking == 0)
                {
                    string whois = whoisinfo(server, item);
                    if (whois == "sNA")
                    {
                        FASna.Add("sNA");
                        FASno.Add("sNA");
                        whoisnotworking = 5;
                    }
                    else
                    {
                        whoisnotworking = 0;
                        whois = whois + "end";
                        Match asnummatch = Regex.Match(whois, @"AS Name(\d)+ ");
                        if (asnummatch.Success)
                        {
                            asno = asnummatch.Value;
                            char[] Fendchar = { 'A', 'S', ' ', 'N', 'a', 'm', 'e', '\n' };
                            asno = asno.TrimStart(Fendchar);
                            FASno.Add(asno);
                        }
                        else
                        { FASno.Add("NA"); }

                        Match asnamematch = Regex.Match(whois, @"(\| [A-z][A-z][A-z].+?[^\|]end)");
                        if (asnamematch.Success)
                        {
                            asna = asnamematch.Value;
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
                    FASna.Add("NA");
                    FASno.Add("NA");
                }

            }
            else
            {
                Flonlat.Add("0");
                Flonlat.Add("0");
                FASna.Add("NA");
                FASno.Add("NA");
            }
        }
        TextBox1.Visible = true;
        Session["AsName"] = FASna;
        Session["Rttave"] = Frttave;
        GenerateGMap(Flonlat, FipS, Frttave, dirname, methodstr, FASno, FASna);

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
    protected void Button4_Click(object sender, EventArgs e)
    {
        Server.Transfer("vtrasresults.aspx");
    }
}