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

public partial class graphictraces : System.Web.UI.Page
{
    String strResult;
    WebResponse objResponse;
    int uploaded = 0;

    protected void Page_Load(object sender, EventArgs e)
    {


    }
   protected void UploadButton_Click(object sender, EventArgs e)
    {
        DateTime now = DateTime.Now;
        string timendate = DateTime.Now.ToString();
        Regex regex = new Regex(" ");
        timendate = regex.Replace(timendate, "_", 1);
        timendate = timendate.Replace("/", ".");
        timendate = timendate.Replace(":", "-");
        string dirname = timendate;
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
                        StatusLabel.Text = "Upload status: File uploaded!";
                        uploaded = 1;
                        Session["uploadflag"] = uploaded;
                    }
                    else
                        StatusLabel.Text = "Upload status: The file has to be less than 10 kb!";
                }
                else
                    StatusLabel.Text = "Upload status: Only .txt files are accepted!";
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
        
    }
    protected void Button1_Click1(object sender, EventArgs e)
   {
       
       string dirname = (string)Session["theText"];
       int uploaded = (int)Session["uploadflag"];
        string Ftext = "";
        if (uploaded == 1)
        {
            System.IO.StreamReader FTrfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + dirname + "/tr.txt"));
            Ftext = FTrfile.ReadToEnd();
            FTrfile.Close();
        }
        else
        {
            System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/" + dirname));
            Ftext = TextBox1.Text;
        }

        string Flinepattern = @"(?<linegroup>[ \n]\d+  .*)";
        string Frttpattern = @"(?<rttgroup>\d*[.]\d* ms)|(\d+ ms)";

        List<string> FipS = new List<string>();
        List<string> Frttave = new List<string>();

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
            ///////////////////////////////////////////// Case for '*' ///////////////////////////////

            //Match Rmatchstar = Regex.Match(Rline, @"(\d)+  \* \* \*");
            //if (Rmatchstar.Success)
            //{
            //    RipS.Add("Destination Unreachable");
            //    Rrttave.Add("0");
            //    Rstar = 1;
            //}

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
            }
            else
            {
                Flonlat.Add("0");
                Flonlat.Add("0");
            }
        }

       GenerateFMap(Flonlat, FipS, Frttave, dirname);

    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        string dirname = (string)Session["theText"];
        int uploaded = (int)Session["uploadflag"];

        string Rtext = "";
        if (uploaded == 1)
        {   
            System.IO.StreamReader RTrfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + dirname + "/tr.txt"));
            Rtext = RTrfile.ReadToEnd();
            RTrfile.Close();

        }
        else
        {
            System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/" + dirname));
            Rtext = TextBox1.Text;
        }

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
            else
            {
                RipS.Add("Destination Unreachable");
                Rrttave.Add("0");
                Rstar = 1;
            }
            ///////////////////////////////////////////// Case for '*' ///////////////////////////////

            //Match Rmatchstar = Regex.Match(Rline, @"(\d)+  \* \* \*");
            //if (Rmatchstar.Success)
            //{
            //    RipS.Add("Destination Unreachable");
            //    Rrttave.Add("0");
            //    Rstar = 1;
            //}

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


            }
            else
            {
                Rlonlat.Add("0");
                Rlonlat.Add("0");
            }
        }
        GenerateRMap(Rlonlat, RipS, Rrttave, dirname, methodstr);
    }
    void GenerateFMap(List<string> Flonlat, List<string> FipS, List<string> Frttave, string dirname)
    {
        List<double> test = Flonlat.Select(x => double.Parse(x)).ToList();
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        GoogleMapForASPNet1.GoogleMapObject.APIVersion = "3";
        GoogleMapForASPNet1.GoogleMapObject.Width = "1500px";
        GoogleMapForASPNet1.GoogleMapObject.Height = "400px";
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 14;

        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("0", 0, 0);


        int m = 0;
        int IDpp = 60;
        double longg = 0;
        double lat = 0;
        string ll1 = "0";
        string ll2 = "0";

        List<string> Flonglatlist = new List<string>();
        List<string> zeroballoonlist = new List<string>();
        List<double> rttcompare = new List<double>();

        int k = 0;
        int Frepeat = 0;
        int Frep2 = 1;
        int zeroflag = 0;


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
                Flonglatlist.Add(ll2);
                m = 0;
            }
        }
        for (int ind = 0; ind < Flonglatlist.Count; ind++)
        {
            Frep2 = 1;
            if (Flonglatlist[ind] == "0_0")
            {
                zeroballoonlist.Add("<tr><td width=15%>" + Convert.ToString(ind) + "</td><td width=50%>" + FipS[ind] + "</td><th width=20%>" + Frttave[ind] + "</td><td width=15%></td></tr>");
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
                                string nonzeroinfo = "<tr><td width=15%>" + Convert.ToString(ind) + "</td><td width=50%>" + FipS[ind] + "</td><th width=20%>" + Frttave[ind] + "</td><td width=15%></td></tr></table>";
                                File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), nonzeroinfo);
                                zeroflag = 0;
                            }
                            else
                            {
                                string ballooninfo = "<tr><td width=15%>" + Convert.ToString(ind) + "</td><td width=50%>" + FipS[ind] + "</td><th width=20%>" + Frttave[ind] + "</td><td width=15%></td></tr></table>";
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
                    string heading = "<table width=100% table border=1 cellspacing=5><tr><th width=10%>Hop No</th><th width=35%>IP Address</th><th width=45%>RTT Average</th><th width=10%>Method</th></tr>";
                    File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), heading);
                    if (zeroflag == 1)
                    {
                        foreach (string inf in zeroballoonlist)
                        {
                            File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), inf);
                        }
                        zeroballoonlist.Clear();
                        string nonzeroinfo = "<tr><td width=15%>" + Convert.ToString(ind) + "</td><td width=50%>" + FipS[ind] + "</td><th width=20%>" + Frttave[ind] + "</td><td width=15%></td></tr></table>";
                        File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), nonzeroinfo);
                        zeroflag = 0;
                    }
                    else
                    {
                        string ballooninfo = "<tr><td width=15%>" + Convert.ToString(ind) + "</td><td width=50%>" + FipS[ind] + "</td><th width=20%>" + Frttave[ind] + "</td><td width=15%></td></tr></table>";
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
    void GenerateRMap(List<string> Rlonlat, List<string> RipS, List<string> Rrttave, string dirname, List<string> methodstr)
    {
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        GoogleMapForASPNet1.GoogleMapObject.APIVersion = "3";
        GoogleMapForASPNet1.GoogleMapObject.Width = "1500px";
        GoogleMapForASPNet1.GoogleMapObject.Height = "400px";
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 14;

        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("0", 0, 0);

        List<double> test = Rlonlat.Select(x => double.Parse(x)).ToList();


        int m = 0;
        int IDpp = 60;
        double longg = 0;
        double lat = 0;
        string ll1 = "0";
        string ll2 = "0";

        List<string> Rlonglatlist = new List<string>();
        List<string> zeroballoonlist = new List<string>();
        List<double> Rrttcompare = new List<double>();

        int k = 0;
        int Rrepeat = 0;
        int Rrep2 = 1;
        int zeroflag = 0;


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
                Rlonglatlist.Add(ll2);
                m = 0;
            }
        }
        for (int ind = 0; ind < Rlonglatlist.Count; ind++)
        {
            Rrep2 = 1;
            if (Rlonglatlist[ind] == "0_0")
            {
                zeroballoonlist.Add("<tr><td width=15%>"+Convert.ToString(ind)+"</td><td width=50%>"+RipS[ind]+"</td><th width=20%>"+Rrttave[ind]+"</td><td width=15%>"+methodstr[ind]+"</td></tr>");
                zeroflag = 1;
            }
            else
            {
                if (ind > 0)
                {
                    k = 0;
                    while ((k < ind) & (Rrep2 > 0))
                    {
                        if (Rlonglatlist[ind] == Rlonglatlist[k])
                        {

                            string filename = "RDesc" + k.ToString() + ".txt";
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
                                string nonzeroinfo = "<tr><td width=15%>" + Convert.ToString(ind) + "</td><td width=50%>" + RipS[ind] + "</td><th width=20%>" + Rrttave[ind] + "</td><td width=15%>" + methodstr[ind] + "</td></tr></table>";
                                File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), nonzeroinfo);
                                zeroflag = 0;
                            }
                            else
                            {
                                string ballooninfo = "<tr><td width=15%>" + Convert.ToString(ind) + "</td><td width=50%>" + RipS[ind] + "</td><th width=20%>" + Rrttave[ind] + "</td><td width=15%>" + methodstr[ind] + "</td></tr></table>";
                                File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), ballooninfo);
                            }

                            System.IO.StreamReader descfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + dirname + "/" + filename));
                            string Rdesctext = descfile.ReadToEnd();
                            descfile.Close();

                            string[] words = Rlonglatlist[ind].Split('_');
                            longg = Convert.ToDouble(words[0]);
                            lat = Convert.ToDouble(words[1]);

                            string icon = "";
                            if (ind == 0)
                            {
                                icon = "icons/database.png";
                            }
                            else if (ind == (Rlonglatlist.Count - 1))
                            {
                                icon = "icons/database.png";
                            }
                            else
                            {
                                icon = "icons/wifiblue.png";
                            }

                            string idpps = Convert.ToString(IDpp);
                            GoogleMapForASPNet1.GoogleMapObject.Points.Add(new GooglePoint(idpps, lat, longg, icon, Rdesctext));
                            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint(idpps, lat, longg);
                            Rrepeat = Rrepeat + 1;
                            Rrep2 = 0;
                        }
                        else
                        {
                            k = k + 1;
                            Rrepeat = 0;
                        }
                    }
                }

                if (Rrepeat == 0)
                {

                    string filename = "RDesc" + ind.ToString() + ".txt";
                    //string heading = "<table width=" + "100%" + "table border=" + "0" + "cellspacing=" + "5" + "><tr><th width=" + "15%" + ">Hope No</th><th width=" + "50%" + ">IP Address</th><th width=" + "20" + ">RTT Average</th><th width=" + "15" + ">Method</th></tr></table>";
                    string heading = "<table width=100% table border=1 cellspacing=5><tr><th width=10%>Hop No</th><th width=35%>IP Address</th><th width=45%>RTT Average</th><th width=10%>Method</th></tr>";
                    
                    File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), heading);
                    if (zeroflag == 1)
                    {
                        foreach (string inf in zeroballoonlist)
                        {
                            File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), inf);
                        }
                        zeroballoonlist.Clear();
                        string nonzeroinfo = "<tr><td width=15%>"+Convert.ToString(ind)+"</td><td width=50%>"+RipS[ind]+"</td><th width=20%>"+Rrttave[ind]+"</td><td width=15%>"+methodstr[ind]+"</td></tr></table>";
                        File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), nonzeroinfo);
                        zeroflag = 0;
                    }
                    else
                    {
                        string ballooninfo = "<tr><td width=15%>" + Convert.ToString(ind) + "</td><td width=50%>" + RipS[ind] + "</td><th width=20%>" + Rrttave[ind] + "</td><td width=15%>" + methodstr[ind] + "</td></tr></table>";
                        File.AppendAllText(Server.MapPath("~/App_Data/" + dirname + "/" + filename), ballooninfo);
                    }

                    System.IO.StreamReader descfile = new System.IO.StreamReader(Server.MapPath("~/App_Data/" + dirname + "/" + filename));
                    string Rdesctext = descfile.ReadToEnd();
                    descfile.Close();

                    string[] words = Rlonglatlist[ind].Split('_');
                    longg = Convert.ToDouble(words[0]);
                    lat = Convert.ToDouble(words[1]);

                    string icon = "";
                    if (ind == 0)
                    {
                        icon = "icons/database.png";
                    }
                    else if (ind == (Rlonglatlist.Count - 1))
                    {
                        icon = "icons/database.png";
                    }
                    else
                    {
                        icon = "icons/wifiblue.png";
                    }

                    string idpps = Convert.ToString(IDpp);
                    GoogleMapForASPNet1.GoogleMapObject.Points.Add(new GooglePoint(idpps, lat, longg, icon, Rdesctext));
                    GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint(idpps, lat, longg);
                }
                IDpp = IDpp + 1;
            }


        }
        for (int indx = 0; indx < Rrttave.Count; indx++)
        {
            string Rrtttemp = Rrttave[indx];
            string[] numbers = Regex.Split(Rrtttemp, @"\D+");
            foreach (string value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    Rrttcompare.Add(i);
                }
            }
        }
        int idline = 0;

        for (int indx = 0; indx < Rlonglatlist.Count; indx++)
        {
            if (indx != 0)
            {
                if (Rlonglatlist[indx] == "0_0")
                {
                    continue;
                }
                else
                {
                    int prevll = indx;
                    prevll = prevll - 1;
                    while (prevll >= 0)
                    {
                        if (Rlonglatlist[prevll] == "0_0")
                        {
                            prevll = prevll - 1;
                        }
                        else
                        {
                            idline = idline + 1;
                            string idl = Convert.ToString(idline);
                            GooglePolyline PL2 = new GooglePolyline();
                            double sss = Rrttcompare[indx];
                            if (sss <= 100)
                            {
                                PL2.ColorCode = "#007814";
                            }
                            else if ((sss > 100) & (sss <= 200))
                            {
                                PL2.ColorCode = "#F0780A";
                            }
                            else if ((sss > 200) & (sss <= 300))
                            {
                                PL2.ColorCode = "#1400F0";
                            }
                            else if ((sss > 300) & (sss <= 400))
                            {
                                PL2.ColorCode = "#FA0014";
                            }
                            else if (sss > 400)
                            {
                                PL2.ColorCode = "#000000";
                            }

                            PL2.ID = "PL2";
                            PL2.Width = 5;
                            string[] words2 = Rlonglatlist[prevll].Split('_');
                            double longg0 = Convert.ToDouble(words2[0]);
                            double lat0 = Convert.ToDouble(words2[1]);
                            PL2.Points.Add(new GooglePoint(idl, lat0, longg0));

                            string[] words = Rlonglatlist[indx].Split('_');
                            double longg1 = Convert.ToDouble(words[0]);
                            double lat1 = Convert.ToDouble(words[1]);
                            PL2.Points.Add(new GooglePoint(idl, lat1, longg1));
                            GoogleMapForASPNet1.GoogleMapObject.Polylines.Add(PL2);
                            break;

                        }
                    }

                }



            }

        }
    }

}
