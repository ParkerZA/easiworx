using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finx.App.Extensions
{

        public static class ColorExtensions
        {
            static Random randomizer = new Random();
            public static Color GetContrast(this Color source, bool preserveOpacity)
            {
                Color inputColor = source;
                //if RGB values are close to each other by a diff less than 10%, then if RGB values are lighter side, decrease the blue by 50% (eventually it will increase in conversion below), if RBB values are on darker side, decrease yellow by about 50% (it will increase in conversion)
                byte avgColorValue = (byte)((source.R + source.G + source.B) / 3);
                int diff_r = Math.Abs(source.R - avgColorValue);
                int diff_g = Math.Abs(source.G - avgColorValue);
                int diff_b = Math.Abs(source.B - avgColorValue);
                if (diff_r < 20 && diff_g < 20 && diff_b < 20) //The color is a shade of gray
                {
                    if (avgColorValue < 123) //color is dark
                    {
                    inputColor = Color.FromArgb(50, 230, 220);
                    }
                    else
                    {
                    inputColor = Color.FromArgb(255, 255, 50);
                    }
                }
                byte sourceAlphaValue = source.A;
                if (!preserveOpacity)
                {
                    sourceAlphaValue = Math.Max(source.A, (byte)127); //We don't want contrast color to be more than 50% transparent ever.
                }
                RGB rgb = new RGB { R = inputColor.R, G = inputColor.G, B = inputColor.B };
                HSB hsb = ConvertToHSB(rgb);
                hsb.H = hsb.H < 180? hsb.H + 180 : hsb.H - 180;
                //hsb.B = isColorDark ? 240 : 50; //Added to create dark on light, and light on dark
                rgb = ConvertToRGB(hsb);
                //return new Color { A = sourceAlphaValue, R = rgb.R, G = (byte)rgb.G, B = (byte)rgb.B };

                return Color.FromArgb(sourceAlphaValue, (byte)rgb.R, (byte)rgb.G,(byte)rgb.B);
        }
            internal static RGB ConvertToRGB(HSB hsb)
            {
                // By: <a href="http://blogs.msdn.com/b/codefx/archive/2012/02/09/create-a-color-picker-for-windows-phone.aspx" title="MSDN" target="_blank">Yi-Lun Luo</a>
                double chroma = hsb.S * hsb.B;
                double hue2 = hsb.H / 60;
                double x = chroma * (1 - Math.Abs(hue2 % 2 - 1));
                double r1 = 0d;
                double g1 = 0d;
                double b1 = 0d;
                if (hue2 >= 0 && hue2 < 1)
                {
                    r1 = chroma;
                    g1 = x;
                }
                else if (hue2 >= 1 && hue2 < 2)
                {
                    r1 = x;
                    g1 = chroma;
                }
                else if (hue2 >= 2 && hue2 < 3)
                {
                    g1 = chroma;
                    b1 = x;
                }
                else if (hue2 >= 3 && hue2 < 4)
                {
                    g1 = x;
                    b1 = chroma;
                }
                else if (hue2 >= 4 && hue2 < 5)
                {
                    r1 = x;
                    b1 = chroma;
                }
                else if (hue2 >= 5 && hue2 <= 6)
                {
                    r1 = chroma;
                    b1 = x;
                }
                double m = hsb.B - chroma;
                return new RGB()
                {
                    R = r1 + m,
                    G = g1 + m,
                    B = b1 + m
                };
            }
            internal static HSB ConvertToHSB(RGB rgb)
            {
                // By: <a href="http://blogs.msdn.com/b/codefx/archive/2012/02/09/create-a-color-picker-for-windows-phone.aspx" title="MSDN" target="_blank">Yi-Lun Luo</a>
                double r = rgb.R;
                double g = rgb.G;
                double b = rgb.B;

                double max = Max(r, g, b);
                double min = Min(r, g, b);
                double chroma = max - min;
                double hue2 = 0d;
                if (chroma != 0)
                {
                    if (max == r)
                    {
                        hue2 = (g - b) / chroma;
                    }
                    else if (max == g)
                    {
                        hue2 = (b - r) / chroma + 2;
                    }
                    else
                    {
                        hue2 = (r - g) / chroma + 4;
                    }
                }
                double hue = hue2 * 60;
                if (hue < 0)
                {
                    hue += 360;
                }
                double brightness = max;
                double saturation = 0;
                if (chroma != 0)
                {
                    saturation = chroma / brightness;
                }
                return new HSB()
                {
                    H = hue,
                    S = saturation,
                    B = brightness
                };
            }
            private static double Max(double d1, double d2, double d3)
            {
                if (d1 > d2)
                {
                    return Math.Max(d1, d3);
                }
                return Math.Max(d2, d3);
            }
            private static double Min(double d1, double d2, double d3)
            {
                if (d1 < d2)
                {
                    return Math.Min(d1, d3);
                }
                return Math.Min(d2, d3);
            }
            internal struct RGB
            {
                internal double R;
                internal double G;
                internal double B;
            }
            internal struct HSB
            {
                internal double H;
                internal double S;
                internal double B;
            }

       // static Random randomizer = new Random();
        /// <summary>
        /// Returns a pastel shade of the color
        /// </summary>
        /// <param name="source">Source  color</param>
        /// <returns></returns>
        public static Color GetPastelShade(this Color source)
        {
            return (generateColor(source, true, new HSB { H = 0, S = 0.2d, B = 255 }, new HSB { H = 360, S = 0.5d, B = 255 }));
        }
        /// <summary>
        /// Returns a random color
        /// </summary>
        /// <param name="source">Ignored(Use RandomShade to get a shade of given color)</param>
        /// <returns></returns>
        public static Color GetRandom(this Color source)
        {
            return (generateColor(source, false, new HSB { H = 0, S = 0, B = 0 }, new HSB { H = 360, S = 1, B = 255 }));
        }
        /// <summary>
        /// Returns a random color within a brightness boundry
        /// </summary>
        /// <param name="source">Ignored (Use GetRandomShade to get a random shade of the color)</param>
        /// <param name="minBrightness">A valued from 0.0 to 1.0, 0 is darkest and 1 is lightest</param>
        /// <param name="minBrightness">A valued from 0.0 to 1.0</param>
        /// <returns></returns>
        public static Color GetRandom(this Color source, double minBrightness, double maxBrightness)
        {
            if (minBrightness >= 0 && maxBrightness <= 1)
            {
                return (generateColor(source, false, new HSB { H = 0, S = 1 * minBrightness, B = 255 }, new HSB { H = 360, S = 1 * maxBrightness, B = 255 }));
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// Returns a random shade of the color
        /// </summary>
        /// <param name="source">Base color for the returned shade</param>
        /// <returns></returns>
        public static Color GetRandomShade(this Color source)
        {
            return (generateColor(source, true, new HSB { H = 0, S = 1, B = 0 }, new HSB { H = 360, S = 1, B = 255 }));
        }
        /// <summary>
        /// Returns a random color within a brightness boundry
        /// </summary>
        /// <param name="source">Base color for the returned shade</param>
        /// <param name="minBrightness">A valued from 0.0 to 1.0, 0 is brightest and 1 is lightest</param>
        /// <param name="minBrightness">A valued from 0.0 to 1.0</param>
        /// <returns></returns>
        public static Color GetRandomShade(this Color source, double minBrightness, double maxBrightness)
        {
            if (minBrightness >= 0 && maxBrightness <= 1)
            {
                return (generateColor(source, true, new HSB { H = 0, S = 1 * minBrightness, B = 255 }, new HSB { H = 360, S = 1 * maxBrightness, B = 255 }));
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// Process parameters and returns a color
        /// </summary>
        /// <param name="source">Color source</param>
        /// <param name="isaShadeOfSource">Should source be used to generate the new color</param>
        /// <param name="min">Minimum range for HSB</param>
        /// <param name="max">Maximum range for HSB</param>
        /// <returns></returns>
        private static Color generateColor(Color source, bool isaShadeOfSource, HSB min, HSB max)
        {
            HSB hsbValues = ConvertToHSB(new RGB { R = source.R, G = source.G, B = source.B });
            double h_double = randomizer.NextDouble();
            double s_double = randomizer.NextDouble();
            double b_double = randomizer.NextDouble();
            if (max.B - min.B == 0) b_double = 0; //do not change Brightness
            if (isaShadeOfSource)
            {
                min.H = hsbValues.H;
                max.H = hsbValues.H;
                h_double = 0;
            }
            hsbValues = new HSB
            {
                H = Convert.ToDouble(randomizer.Next(Convert.ToInt32(min.H), Convert.ToInt32(max.H))) + h_double,
                S = Convert.ToDouble((randomizer.Next(Convert.ToInt32(min.S * 100), Convert.ToInt32(max.S * 100))) / 100d),
                B = Convert.ToDouble(randomizer.Next(Convert.ToInt32(min.B), Convert.ToInt32(max.B))) + b_double
            };
           // Debug.WriteLine("H:{0} | S:{1} | B:{2} [Min_S:{3} | Max_S{4}]", hsbValues.H, _hsbValues.S, _hsbValues.B, min.S, max.S);
            RGB rgbvalues = ConvertToRGB(hsbValues);
           // return new Color { A = source.A, R = (byte)rgbvalues.R, G = (byte)rgbvalues.G, B = (byte)rgbvalues.B };

            return Color.FromArgb(source.A, (byte)rgbvalues.R, (byte)rgbvalues.G, (byte)rgbvalues.B);
        }
    }
        
  
}
