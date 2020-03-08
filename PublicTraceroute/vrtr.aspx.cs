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
using OpenPop.Common.Logging;
using OpenPop.Mime;
using OpenPop.Mime.Decode;
using OpenPop.Mime.Header;
using OpenPop.Pop3;
public partial class vrtr : System.Web.UI.Page
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
            GoogleMapForASPNet1.GoogleMapObject.Height = "500px";
            GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 3;
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("0", 0, 0);
        }  
    }
    void GenerateGMap(List<string> LongLat, List<string> FipS, List<string> Frttave, string dirname, List<string> methodstr, List<string> FASno, List<string> FASna)
    {
        GoogleMapForASPNet1.GoogleMapObject.Polylines.Clear();
        GoogleMapForASPNet1.GoogleMapObject.Points.Clear();

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
        string[] filePaths = Directory.GetFiles(Server.MapPath("~/App_Data/Email"));
        foreach (string filePath in filePaths)
        { File.Delete(filePath); }
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
        string url = "http://revtr.cs.washington.edu/measure.php";
        string dest = TextBox2.Text;
        string nod = DropDownList1.SelectedValue;
        string postData = string.Format("destination={0}&node={1}&email=vizitrace.reverse.traceroute%40gmail.com&prediction=If+you+believe+you+know+the+traceroute+%28for+instance%2C+if+you+are+trying+out+our+system+using+a+destination+that+you+actually+control%29%2C+please+cut-and-paste+the+traceroute+into+this+box+to+aid+us+in+improving+our+system.", dest, nod);

        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
        webRequest.Method = "POST";
        webRequest.ContentType = "application/x-www-form-urlencoded";
        webRequest.ContentLength = postData.Length;
        using (StreamWriter requestWriter2 = new StreamWriter(webRequest.GetRequestStream()))
        {
            requestWriter2.Write(postData);
        }
        HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
        string responseData = string.Empty;
        using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
        {
            responseData = responseReader.ReadToEnd();
        }
        Timer1.Enabled = true;
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        List<string> seenUids = new List<string>();
        List<Message> allmes = new List<Message>();
        allmes = FetchUnseenMessages("pop.gmail.com", 995, true, "vizitrace.reverse.traceroute@gmail.com", "vizitrace@12012", seenUids);
        int i = 0;
        int count = allmes.Count();
        if (allmes.Count == 0)
        {
            Label1.Text = "Waiting For Email";
        }
        else
        {
            Timer1.Enabled = false;
            string dirname = dattime();
                MessagePart html = allmes[allmes.Count()-1].FindFirstHtmlVersion();
                if (html != null)
                {
                    Label1.Text = "Measurement Recieved";
                    html.Save(new FileInfo(Server.MapPath("~/App_Data/Email" + dirname + ".txt")));
                    string emaildir = "~/App_Data/Email" + dirname + ".txt";
                    i++;
                    livertr(emaildir);
                }
        }
    }
    public static List<Message> FetchUnseenMessages(string hostname, int port, bool useSsl, string username, string password, List<string> seenUids)
    {
        using (Pop3Client client = new Pop3Client())
        {
            client.Connect(hostname, port, useSsl);
            client.Authenticate(username, password);
            List<string> uids = client.GetMessageUids();

            List<Message> newMessages = new List<Message>();
            for (int i = 0; i < uids.Count; i++)
            {
                string currentUidOnServer = uids[i];
                if (!seenUids.Contains(currentUidOnServer))
                {
                    Message unseenMessage = client.GetMessage(i + 1);
                    newMessages.Add(unseenMessage);
                    seenUids.Add(currentUidOnServer);
                }
            }
            return newMessages;
        }
    }
    private void livertr(string emaildir)
    {
        System.IO.StreamReader RTrfile = new System.IO.StreamReader(Server.MapPath(emaildir));
        string Rtext = RTrfile.ReadToEnd();
        RTrfile.Close();

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

            ////////////////////////////////////// Extract IPs Line-By-Line ///////////////////////////////////////////////////////////////////////
            Match method = Regex.Match(Rline, @"(( -?dst\r)|( -?sym\r)|( -?tr\r)|( -?rr\r)|( -?ts\r))");
            if (method.Success)
            {
                methodstr.Add(method.Value);
            }
            else
            {
                methodstr.Add("NA");
            }
            Match Rmatchip = Regex.Match(Rline, @"\(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\)");
            if (Rmatchip.Success)
            {
                string Riptemp = Rmatchip.Value;
                Riptemp = Riptemp.Replace(")", "");
                Riptemp = Riptemp.Replace("(", "");
                //char Rendchar = ')';
                //char Rstartchar = '(';
                //string Rtrimen = Riptemp.TrimEnd(Rendchar);
                //string Rtrimst = Rtrimen.TrimStart(Rstartchar);
                RipS.Add(Riptemp);
            }
            else
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
        /////////////////////////// FINDING LONGITUDE & LATITUDE OF Reverse IPS /////////////////////////////////////////////////
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
                File.WriteAllText(Server.MapPath("~/App_Data/Email" + "/RLongLat.txt"), strResult);

                System.IO.StreamReader geoipfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/Email" + "/RLongLat.txt"));
                string RLLtext = geoipfile.ReadToEnd();
                geoipfile.Close();
                int found_match = 0;
                MatchCollection Rlonglat = Regex.Matches(RLLtext, @"(>-?\d{1,3}<)|(>-?\d{1,3}[.]\d{0,4}<)");
                foreach (Match Rll in Rlonglat)
                {
                    string lltemp = Rll.Value;
                    lltemp = lltemp.Replace(">", "");
                    lltemp = lltemp.Replace("<", "");
                    //char Rendchar = '<';
                    //char Rstartchar = '>';
                    //string lltrimen = lltemp.TrimEnd(Rendchar);
                    //string lltrimst = lltrimen.TrimStart(Rstartchar);
                    Rlonlat.Add(lltemp);
                    found_match = found_match + 1;
                }
                if (found_match != 2) // If Longitude Latitude are not found by GEOIPtool
                {
                    Rlonlat.Add("0");
                    Rlonlat.Add("0");
                }
                //////////////////////////////////FINDING AS no & Name ////////////////////////////

                string whois = whoisinfo(server, item);
                if (whois == "sNA")
                {
                    RASno.Add("sNA");
                    RASna.Add("sNA");
                }
                else
                {
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
            }
            else
            {
                Rlonlat.Add("0");
                Rlonlat.Add("0");
                RASna.Add("NA");
                RASno.Add("NA");
            }
        }

        GenerateGMap(Rlonlat, RipS, Rrttave, "Email", methodstr, RASno, RASna);
    }
}