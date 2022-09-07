using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Finx.App.Extensions
{
    public static class ChartExt
    {
        public static void Format(this Chart chart, string Title,string[] Legends=null)
        {
            try
            {
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                chart.Titles.Clear();

                chart.Titles.Add(Title);
                chart.Titles[0].Font = new Font("Consolas", 14, FontStyle.Bold);

                chart.Legends.Clear();
                if (Legends != null)
                {
                    foreach (var legend in Legends)
                    {
                        Legend l = new Legend() { Name = legend };
                        l.Docking = Docking.Right;
                        l.IsDockedInsideChartArea = true;

                        chart.Legends.Add(l);
                    }
                }
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }
            
        }
        public static void Format(this ChartArea chartArea,string AxisXTitle="", string AxisYTitle="", string AxisXLabelStyleFormat = "",string AxisYLabelStyleFormat="",bool RemoveGridLines=true)
        {
            try
            {
                //Position
                chartArea.Position.Auto = true;
                //chartArea.Position.X = 5;
                //chartArea.Position.Y = 5;
                //chartArea.Position.Width = 90;
                //chartArea.Position.Height = 100;

                //InnerPlot Position
                chartArea.InnerPlotPosition.Auto = true;
                //chartArea.InnerPlotPosition.X = 0;
                //chartArea.InnerPlotPosition.Y = 0;
                //chartArea.InnerPlotPosition.Width = 100;
                //chartArea.InnerPlotPosition.Height = 90;

                //Axis Titles
                chartArea.AxisX.Title = AxisXTitle;
                chartArea.AxisY.Title = AxisYTitle;


                chartArea.AxisX.IsLabelAutoFit = true;
                chartArea.AxisX.TextOrientation = TextOrientation.Horizontal;


                chartArea.AxisY.IsLabelAutoFit = true;
                chartArea.AxisY.TextOrientation = TextOrientation.Horizontal;

                //LabelStyle
                chartArea.AxisX.LabelStyle.Font = new Font("Consolas", 12, FontStyle.Bold);
                chartArea.AxisX.LabelStyle.Format = AxisXLabelStyleFormat;

                chartArea.AxisY.LabelStyle.Font = new Font("Consolas", 12, FontStyle.Bold);
                chartArea.AxisY.LabelStyle.Format = AxisYLabelStyleFormat;

                //Grid Lines
                chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
                chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;

                //Remove grid lines
                if (RemoveGridLines)
                {
                    chartArea.AxisX.MajorGrid.LineWidth = 0;
                    chartArea.AxisY.MajorGrid.LineWidth = 0;
                }

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }
        }

        public static void Format(this Series series, SeriesChartType chartType,string chartArea="", ChartValueType XValueType= ChartValueType.String)
        {
            try
            {
                series.Font = new Font("Consolas", 12, FontStyle.Bold);
                series.ChartType = chartType;
                series.ChartArea = chartArea;

                series.XValueType = XValueType;

            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
            }
        }
        public static void ChartData<P,T>(this Series series, IEnumerable<IGrouping<P,T>> list,IDictionary<string,Color> ColourScheme=null,bool IsValueShownAsLabel=true)
        {
            try
            {
                int i = 0;
                foreach (var l in list)
                {
                    series.Points.Add(new DataPoint(i, l.Count()));
                    series.Points[i].IsValueShownAsLabel = IsValueShownAsLabel;
                    series.Points[i].AxisLabel = l.Key.ToString()==string.Empty?" ": l.Key.ToString();
                   
                    if (ColourScheme != null)
                        try
                        {
                            series.Points[i].Color = ColourScheme[l.Key.ToString()]; 
                        }
                        catch (Exception)
                        {
                            series.Points[i].Color = Color.LightGray; 
                        }
                    i++;
                }
            }
            catch (Exception x)
            {
                Program.Logger.Error(x);
                //  series.Points.Add(new DataPoint(0, 0));
            }

        }
    }
}
