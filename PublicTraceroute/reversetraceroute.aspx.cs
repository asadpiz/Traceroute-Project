using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.Net;
using System.Collections.Specialized;


class WebPostRequest
{
    WebRequest theRequest;
    HttpWebResponse theResponse;
    ArrayList theQueryData;

    public WebPostRequest(string url)
    {
        theRequest = WebRequest.Create(url);
        theRequest.Method = "POST";
        theQueryData = new ArrayList();
    }

    public void Add(string key, string value)
    {
        theQueryData.Add(String.Format("{0}={1}", key, HttpUtility.UrlEncode(value)));
    }

    public string GetResponse()
    {
        // Set the encoding type
        theRequest.ContentType = "application/x-www-form-urlencoded";

        // Build a string containing all the parameters
        string Parameters = String.Join("&", (String[])theQueryData.ToArray(typeof(string)));
        theRequest.ContentLength = Parameters.Length;

        // We write the parameters into the request
        StreamWriter sw = new StreamWriter(theRequest.GetRequestStream());
        sw.Write(Parameters);
        sw.Close();

        // Execute the query
        theResponse = (HttpWebResponse)theRequest.GetResponse();
        StreamReader sr = new StreamReader(theResponse.GetResponseStream());
        return sr.ReadToEnd();
    }

}

public partial class reversetraceroute : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)

    {
    }
    void wc_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
    {
        //Submit Complete
    }
     protected void Button1_Click(object sender, EventArgs e)
     {
         WebPostRequest myPost = new WebPostRequest("https://docs.google.com/a/seecs.edu.pk/spreadsheet/formResponse?formkey=dFBTNzhNYzNDbUZwMnJJdUhBVUZMTnc6MQ&amp;ifq");
         myPost.Add("entry.0.single", "abcde");
         ListBox1.Items.Add(myPost.GetResponse());
     }
    }
