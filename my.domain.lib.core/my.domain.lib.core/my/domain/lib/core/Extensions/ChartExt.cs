// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Extensions.ChartExt
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace my.domain.lib.core.Extensions
{
    public static class ChartExt
    {
        public static Bitmap GetChartAsBitMap(this Chart chart)
        {
            int width1 = chart.Width;
            int height1 = chart.Height;
            int width2 = width1;
            int height2 = height1;
            Bitmap bitmap = new Bitmap(width2, height2, PixelFormat.Format32bppRgb);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.ScaleTransform(1f, 1f);
            bitmap.SetResolution(1200f, 1200f);
            chart.Printing.PrintPaint(graphics, new Rectangle(0, 0, width2, height2));
            return bitmap;
        }

        public static byte[] GetChartAsBytes(this Chart chart)
        {
            Bitmap chartAsBitMap = chart.GetChartAsBitMap();
            MemoryStream memoryStream = new MemoryStream();
            chartAsBitMap.Save((Stream)memoryStream, ImageFormat.Png);
            memoryStream.Position = 0L;
            return memoryStream.ToArray();
        }

        public static byte[] GetImageAsBytes(this Image image)
        {
            int width = image.Width;
            int height = image.Height;
            Bitmap bitmap = new Bitmap(image, width, height);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.ScaleTransform(1f, 1f);
            bitmap.SetResolution(1200f, 1200f);
            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save((Stream)memoryStream, ImageFormat.Png);
            memoryStream.Position = 0L;
            return memoryStream.ToArray();
        }

        public static Bitmap GetBytesAsImage(this byte[] bytes)
        {
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                memoryStream.Write(bytes, 0, Convert.ToInt32(bytes.Length));
                Bitmap bitmap = new Bitmap((Stream)memoryStream, false);
                memoryStream.Dispose();
                return bitmap;
            }
            catch (Exception ex)
            {
            }
            return (Bitmap)null;
        }
    }
}
