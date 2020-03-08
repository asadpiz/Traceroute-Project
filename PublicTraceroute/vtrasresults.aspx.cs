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


public partial class vtrasresults : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var FASna = (List<string>)Session["AsName"];
        var Frtta = (List<string>)Session["Rttave"];
        List<int> Frttave = new List<int>();
        int Rrtt11 = 0;
        foreach (var item in Frtta)
        {
            string Rrtttemp = item;
            char[] Rendchar = { ' ', 'm', 's' };
            string Rtrimen = Rrtttemp.TrimEnd(Rendchar);
            Rrtt11 = Convert.ToInt16(Rtrimen);
            Frttave.Add(Rrtt11);
        }

        var g = FASna.GroupBy(i => i);
        int countLength = 0;
        foreach (var grp in g)
        {
            countLength++;
        }
        object[] data = new object[countLength];


        int index = 0;
        foreach (var grp in g)
        {
            data[index] = new object[] { grp.Key, grp.Count() };
            index++;
        }

        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart").InitChart(new Chart { DefaultSeriesType = ChartTypes.Pie, PlotShadow = false })
        .SetTitle(new Title { Text = "Province Based Institutions Registration" })
        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }" })
        .SetPlotOptions(new PlotOptions
        {
            Pie = new PlotOptionsPie
            {
                AllowPointSelect = true,
                Cursor = Cursors.Pointer,
                DataLabels = new PlotOptionsPieDataLabels
                {
                    Color = ColorTranslator.FromHtml("#000000"),
                    ConnectorColor = ColorTranslator.FromHtml("#000000"),
                    Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ Math.round(this.percentage) +' %'; }"
                },
            }
        })
             .SetSeries(new Series
             {
                 Data = new Data(data)
             });

        Literal1.Text = chart.ToHtmlString();

        //////////////////// RTT ////////////////////////
        Series[] series = new Series[Frttave.Count()];
        string[] array_DistrictNames = new string[Frttave.Count()];
        data = new object[Frttave.Count()];

        int index2 = 0;
        foreach (var district in Frttave)
        {
            series[index2] = new Series { Name = index2.ToString(), Data = new Data(new object[] { district }) };
            array_DistrictNames[index2] = index2.ToString();
            index2++;
        }

        DotNet.Highcharts.Highcharts chart2 = new DotNet.Highcharts.Highcharts("chart").InitChart(new Chart { DefaultSeriesType = ChartTypes.Column, Margin = new int[] { 50, 50, 100, 80 } })
        .SetTitle(new Title { Text = "District Based Registered Institutes" })
        .SetPlotOptions(new PlotOptions
        {
            Bar = new PlotOptionsBar
            {
                DataLabels = new PlotOptionsSeriesDataLabels
                {
                    Enabled = true
                }
            }
        }
        )
       .SetXAxis(new XAxis
            {
                Categories = new [] { "0","1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25","26","27","28","29","30"}  
            })
        .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Registered Institutions", Align = AxisTitleAligns.High } })
        ;

        chart2.SetSeries(series);

        Literal2.Text = chart2.ToHtmlString();
    }

    }
