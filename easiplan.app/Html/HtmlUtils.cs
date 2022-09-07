using QSS.Components.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace Finx.App
{
    public class HtmlUtils
    {
        private const int Iterations = 20;

        /// <summary>
        /// The HTML text used in sample form for HtmlLabel.
        /// </summary>
        public static String SampleHtmlLabelText
        {
            get
            {
                return "This is an <b>HtmlLabel</b> on transparent background with <span style=\"color: red\">colors</span> and links: " +
                       "<a href=\"http://htmlrenderer.codeplex.com/\">HTML Renderer</a>";
            }
        }

        /// <summary>
        /// The HTML text used in sample form for HtmlPanel.
        /// </summary>
        public static String SampleHtmlPanelText
        {
            get
            {
                return "This is an <b>HtmlPanel</b> with <span style=\"color: red\">colors</span> and links: <a href=\"http://htmlrenderer.codeplex.com/\">HTML Renderer</a>" +
                       "<div style=\"font-size: 1.2em; padding-top: 10px;\" >If there is more text than the size of the control scrollbars will appear.</div>" +
                       "<br/>Click me to change my <code>Text</code> property.";
            }
        }

        /// <summary>
        /// Handle stylesheet resolve.
        /// </summary>
        public static void OnStylesheetLoad(object sender, HtmlStylesheetLoadEventArgs e)
        {
            var stylesheet = GetStylesheet(e.Src);
            if (stylesheet != null)
                e.SetStyleSheet = stylesheet;
        }

        /// <summary>
        /// Get stylesheet by given key.
        /// </summary>
        public static string GetStylesheet(string src)
        {
            if (src == "StyleSheet")
            {
                return @"h1, h2, h3 { color: navy; font-weight:normal; }
                    h1 { margin-bottom: .47em }
                    h2 { margin-bottom: .3em }
                    h3 { margin-bottom: .4em }
                    ul { margin-top: .5em }
                    ul li {margin: .25em}
                    body { 
                        font:12pt Verdana ;
                        top:-10px;
                        width:100%;
                        padding:10px 10px 10px 10px;
                        margin:0px 0px 0px 0px;
border:none;
                        //border: 1px #808080 solid;
                        //corner-radius:5px; 
                        height:100%;
                        background-size: cover;
                        background-color:transparent;
                    }
		            pre  { border:solid 1px gray; background-color:#eee; padding:1em }
                    a:link { text-decoration: none; }
                    a:hover { text-decoration: underline; }
                    .gray    { color:gray; }
                    .bordered {border: 1px #808080 solid;padding:8px 5px 8px 5px;}
                    .example { background-color:#efefef; corner-radius:5px; padding:0.5em; }
                    .whitehole { background-color:whitesmoke; corner-radius:10px; padding:15px; }
                    .warning { background-color:white; corner-radius:10px;  color:red }
                    .caption { font-size: 1.1em }
                    .comment { color: green; margin-bottom: 5px; margin-left: 3px; }
                    .comment2 { color: green; }
                     .autostyle0 {
                                text-align: left;
                                font-size:larger;
                                padding:0px 0px 0px 0px;
                                margin:0px 0px 0px 0px;
                            }
                    .autostyle1 {
                        text-align: left;
                        font-size:24px;
                        padding: 0px 0px 0px 0px;
                        margin: 2px 2px 2px 2px;
                        font-family:Verdana;
                        color: navy;
                        font-weight:bolder;
                            }
                    .autostyle2 {
                        text-align: left;
                        font-size:14px;
                        padding: 0px 0px 0px 0px;
                        margin: 2px 2px 2px 2px;
                        font-family:Verdana;
                        color: navy;
                        font-weight:bolder;
                    }
                    .autostyle3 {
                        text-align: left;
                        font-size:11px;
                        padding: 0px 0px 0px 0px;
                        margin: 2px 2px 2px 2px;
                        font-family:Verdana;
                        color: navy;
                        
                    }

                .g1, .g2, .g3, .g4, .g5 {
                    background-color: steelblue;
                    background-gradient: white;
                    padding: 22px;
                }
                .g1 { background-gradient-angle: 0; }

                .g2 { background-gradient-angle: 45; }

                .g3 { background-gradient-angle: 90; }

                .g4 { background-gradient-angle: 135; }

                .g5 { background-gradient-angle: 180; }

";
            }
            return null;
        }

        /// <summary>
        /// Get image by resource key.
        /// </summary>
        public static Stream GetImageStream(string src)
        {

            switch (src.ToLower())
            {
                case "logo":
                    return GetManifestResourceStream(easiplan.app.Properties.Resources.finworks1);
                case "registration":
                    return GetManifestResourceStream(easiplan.app.Properties.Resources.notes_32);
                case "configuration":
                    return GetManifestResourceStream(easiplan.app.Properties.Resources.config_32);
                case "information":
                    return GetManifestResourceStream(easiplan.app.Properties.Resources.info_32);
                case "imageicon":
                    return GetManifestResourceStream(easiplan.app.Properties.Resources.finworks1);
                case "methodicon":
                    return GetManifestResourceStream(easiplan.app.Properties.Resources.finworks1);
                case "propertyicon":
                    return GetManifestResourceStream(easiplan.app.Properties.Resources.finworks1);
                case "eventicon":
                    return GetManifestResourceStream(easiplan.app.Properties.Resources.finworks1);
                case "warning":
                    return GetManifestResourceStream(easiplan.app.Properties.Resources.info_32);
                case "error":
                    return GetManifestResourceStream(easiplan.app.Properties.Resources.icon_error);
            }
            return null;
        }
        private static Stream GetManifestResourceStream(string name)
        {
           
            return typeof(easiplan.app.Properties.Resources).Assembly.GetManifestResourceStream("easiplan.app.Resources." + name);
        }
        private static Stream GetManifestResourceStream(Icon icon)
        {
            MemoryStream ms = new MemoryStream();
           
                Image image = GdiHelpers.IconToAlphaBitmap(icon);
                image.Save(ms, ImageFormat.Png);
                return ms;
           


        }
        private static Stream GetManifestResourceStream(Bitmap bmp)
        {
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            return ms;



        }
        //public static string RunSamplesPerformanceTest(Action<String> setHtmlDelegate)
        //{
        //    GC.Collect();

        //    double baseMemory;
        //    var baseStopwatch = RunTest(setHtmlDelegate, false, out baseMemory);

        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    GC.Collect();

        //    double runMemory;
        //    var runStopwatch = RunTest(setHtmlDelegate, true, out runMemory);

        //    double memory = runMemory - baseMemory;
        //    var elapsedMilliseconds = runStopwatch.ElapsedMilliseconds - baseStopwatch.ElapsedMilliseconds;

        //    float htmlSize = 0;
        //    foreach (var sample in SamplesLoader.ShowcaseSamples)
        //        htmlSize += sample.Html.Length * 2;
        //    htmlSize = htmlSize / 1024f;

        //    var sampleCount = SamplesLoader.ShowcaseSamples.Count;
        //    var msg = string.Format("{0} HTMLs ({1:N0} KB)\r\n{2} Iterations", sampleCount, htmlSize, Iterations);
        //    msg += "\r\n\r\n";
        //    msg += string.Format("CPU:\r\nTotal: {0} msec\r\nIterationAvg: {1:N2} msec\r\nSingleAvg: {2:N2} msec",
        //        elapsedMilliseconds, elapsedMilliseconds / (double)Iterations, elapsedMilliseconds / (double)Iterations / sampleCount);

        //    if (Environment.Version.Major >= 4)
        //    {
        //        msg += "\r\n\r\n";
        //        msg += string.Format("Memory:\r\nTotal: {0:N0} KB\r\nIterationAvg: {1:N0} KB\r\nSingleAvg: {2:N0} KB\r\nOverhead: {3:N0}%",
        //            memory, memory / Iterations, memory / Iterations / sampleCount, 100 * (memory / Iterations) / htmlSize);
        //    }

        //    msg += "\r\n\r\n\r\n";
        //    msg += string.Format("Full CPU:\r\nTotal: {0} msec\r\nIterationAvg: {1:N2} msec\r\nSingleAvg: {2:N2} msec",
        //        runStopwatch.ElapsedMilliseconds, runStopwatch.ElapsedMilliseconds / (double)Iterations, runStopwatch.ElapsedMilliseconds / (double)Iterations / sampleCount);

        //    if (Environment.Version.Major >= 4)
        //    {
        //        msg += "\r\n\r\n";
        //        msg += string.Format("Full Memory:\r\nTotal: {0:N0} KB\r\nIterationAvg: {1:N0} KB\r\nSingleAvg: {2:N0} KB\r\nOverhead: {3:N0}%",
        //            runMemory, runMemory / Iterations, runMemory / Iterations / sampleCount, 100 * (runMemory / Iterations) / htmlSize);
        //    }

        //    return msg;
        //}

        //private static Stopwatch RunTest(Action<String> setHtmlDelegate, bool real, out double totalMem)
        //{
        //    totalMem = 0;
        //    long startMemory = 0;
        //    if (Environment.Version.Major >= 4)
        //    {
        //        typeof(AppDomain).GetProperty("MonitoringIsEnabled").SetValue(null, true, null);
        //        startMemory = (long)AppDomain.CurrentDomain.GetType().GetProperty("MonitoringTotalAllocatedMemorySize").GetValue(AppDomain.CurrentDomain, null);
        //    }

        //    var sw = Stopwatch.StartNew();

        //    for (int i = 0; i < Iterations; i++)
        //    {
        //        foreach (var sample in SamplesLoader.ShowcaseSamples)
        //        {
        //            setHtmlDelegate(real ? sample.Html : string.Empty);
        //        }
        //    }

        //    sw.Stop();

        //    if (Environment.Version.Major >= 4)
        //    {
        //        var endMemory = (long)AppDomain.CurrentDomain.GetType().GetProperty("MonitoringTotalAllocatedMemorySize").GetValue(AppDomain.CurrentDomain, null);
        //        totalMem = (endMemory - startMemory) / 1024f;
        //    }

        //    return sw;
        //}


        internal static void LoadHtmlPanel(HtmlPanel htmlPanel, string resource)
        {
            htmlPanel.StylesheetLoad += HtmlUtils.OnStylesheetLoad;
            htmlPanel.ImageLoad += HtmlRenderingHelper.OnImageLoad;
            using (StreamReader sreader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resource), Encoding.Default))
            {
                string html = sreader.ReadToEnd();
                htmlPanel.Text = html;
            }
        }
    }

    internal static class HtmlRenderingHelper
    {
        #region Fields/Consts

        /// <summary>
        /// Cache for resource images
        /// </summary>
        private static readonly Dictionary<string, Image> _imageCache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);

        #endregion


        /// <summary>
        /// Check if currently running in mono.
        /// </summary>
        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        /// <summary>
        /// Create image to be used to fill background so it will be clear that what's on top is transparent.
        /// </summary>
        public static Bitmap CreateImageForTransparentBackground()
        {
            var image = new Bitmap(10, 10);
            using (var g = Graphics.FromImage(image))
            {
                g.Clear(Color.White);
                g.FillRectangle(SystemBrushes.Control, new Rectangle(0, 0, 5, 5));
                g.FillRectangle(SystemBrushes.Control, new Rectangle(5, 5, 5, 5));
            }
            return image;
        }

        /// <summary>
        /// Get image by resource key.
        /// </summary>
        public static Image TryLoadResourceImage(string src)
        {
            Image image;
            if (!_imageCache.TryGetValue(src, out image))
            {
                using (var imageStream = HtmlUtils.GetImageStream(src))
                {
                    if (imageStream != null)
                    {
                        image = Image.FromStream(imageStream);
                        _imageCache[src] = image;
                    }
                }
            }
            return image;
        }

        /// <summary>
        /// Get image by resource key.
        /// </summary>
        //public static XImage TryLoadResourceXImage(string src)
        //{
        //    var img = TryLoadResourceImage(src);
        //    return img != null ? XImage.FromGdiPlusImage(img) : null;
        //}

        /// <summary>
        /// On image load in renderer set the image by event async.
        /// </summary>
        public static void OnImageLoad(object sender, HtmlImageLoadEventArgs e)
        {
            ImageLoad(e, false);
        }

        /// <summary>
        /// On image load in renderer set the image by event async.
        /// </summary>
        public static void OnImageLoadPdfSharp(object sender, HtmlImageLoadEventArgs e)
        {
            ImageLoad(e, true);
        }

        /// <summary>
        /// On image load in renderer set the image by event async.
        /// </summary>
        public static void ImageLoad(HtmlImageLoadEventArgs e, bool pdfSharp)
        {
            var img = TryLoadResourceImage(e.Src);
           // var xImg = img != null ? XImage.FromGdiPlusImage(img) : null;
            object imgObj;
            imgObj = img;
            //if (pdfSharp)
            //    imgObj = xImg;
            //else
            //    imgObj = img;

            if (!e.Handled && e.Attributes != null)
            {
                if (e.Attributes.ContainsKey("byevent"))
                {
                    int delay;
                    if (Int32.TryParse(e.Attributes["byevent"], out delay))
                    {
                        e.Handled = true;
                        ThreadPool.QueueUserWorkItem(state =>
                        {
                            Thread.Sleep(delay);
                            e.Callback("https://fbcdn-sphotos-a-a.akamaihd.net/hphotos-ak-snc7/c0.44.403.403/p403x403/318890_10151195988833836_1081776452_n.jpg");
                        });
                        return;
                    }
                    else
                    {
                        e.Callback("http://sphotos-a.xx.fbcdn.net/hphotos-ash4/c22.0.403.403/p403x403/263440_10152243591765596_773620816_n.jpg");
                        return;
                    }
                }
                else if (e.Attributes.ContainsKey("byrect"))
                {
                    var split = e.Attributes["byrect"].Split(',');
                    var rect = new Rectangle(Int32.Parse(split[0]), Int32.Parse(split[1]), Int32.Parse(split[2]), Int32.Parse(split[3]));
                    e.Callback(imgObj ?? TryLoadResourceImage("htmlicon"), rect.X, rect.Y, rect.Width, rect.Height);
                    return;
                }
            }

            if (img != null)
                e.Callback(imgObj);
        }
    }
}
