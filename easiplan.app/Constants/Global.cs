using easiplan.app.Services;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Finx.App
{
    

    internal class Global
    {
        internal static Color DFLT_BCK_COL = Color.WhiteSmoke;
        internal static Color DFLT_PRIM_CLR = ParseHtmlColor("#148e8f");
        internal static Color DFLT_SEC_CLR = ParseHtmlColor("#d65d2a");

        internal static string CurrentAppVersion = string.Empty;

        /// <summary>
        /// Global Unique Id for this application
        /// </summary>
        internal static string AppGuid = ((GuidAttribute)typeof(Program).Assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0]).Value;

        internal static readonly ApplicationProperties gApplicationProperties = new ApplicationProperties();
        /// <summary>
        /// Root registry key
        /// </summary>
#if PRODUCTION
        internal static string RegistryKey = "EasiWorxProd";   
        internal static string gLicenseUrl =  gApplicationProperties.GetApplicationProperties("ApiUrlProd");
        internal static string gWebUrl =  gApplicationProperties.GetApplicationProperties("WebUrlProd");
#elif PREVIEW
        internal static string RegistryKey ="EasiWorxPreview";
        internal static string gLicenseUrl = gApplicationProperties.GetApplicationProperties("ApiUrlProd"); 
         internal static string gWebUrl =  gApplicationProperties.GetApplicationProperties("WebUrlProd");
#elif STAGING
        internal static string RegistryKey = "EasiWorxStaging";
        internal static string gLicenseUrl = gApplicationProperties.GetApplicationProperties("ApiUrlStaging");
         internal static string gWebUrl =  gApplicationProperties.GetApplicationProperties("WebUrlStaging");
#elif MOJAFF
        internal static string RegistryKey = "EasiPlan";   
        internal static string gLicenseUrl =  gApplicationProperties.GetApplicationProperties("ApiUrlProd");
        internal static string gWebUrl =  gApplicationProperties.GetApplicationProperties("WebUrlProd");
#else //DEV
        internal static string RegistryKey = "EasiWorxDev";
        internal static string gLicenseUrl =  gApplicationProperties.GetApplicationProperties("ApiUrlDev");
        internal static string gWebUrl = gApplicationProperties.GetApplicationProperties("WebUrlDev");
#endif

        internal static Font SmallFont = new Font("verdana", 8);
        internal static Font NormalFont = new Font("verdana", 10);
        internal static Font LargeFont = new Font("verdana", 12);

        internal static Font SmallFontLable = new Font("Calibri Light", 9);
        internal static Font NormalFontLable = new Font("Calibri Dark", 12);
        internal static Font LargeFontLable = new Font("Calibri Light", 13);
        internal static Font XLargeFontLable = new Font("Calibri Light", 18,FontStyle.Bold);

        internal static Font SmallFontText = new Font("Calibri Light", 9);
        internal static Font NormalFontText = new Font("Calibri Light", 11);
        internal static Font LargeFontText = new Font("Calibri Light", 13);


        internal static Font SmallFontControl = new Font("Calibri Light", 9);
        internal static Font NormalFontControl = new Font("Calibri Light", 11);
        internal static Font LargeFontControl = new Font("Calibri Light", 13);

        internal static Font GridFont = NormalFont;
        internal static Font LableFont = NormalFontLable;
        internal static Font TextFont = NormalFontText;
        internal static Font ControlFont = NormalFontControl;

        internal static Color DefaultBackColor = Color.FromKnownColor(KnownColor.Control); // Color.FromArgb(245, 245, 245);//

        internal static Color ParseHtmlColor(string htmlColor) => Color.FromArgb(HtmlColorToArgb(htmlColor));

        internal static int HtmlColorToArgb(string htmlColor, bool requireHexSpecified = false, int defaultAlpha = 0xFF)
        {

            if (string.IsNullOrEmpty(htmlColor))
            {
                throw new ArgumentNullException(nameof(htmlColor));
            }

            if (!htmlColor.StartsWith("#") && requireHexSpecified)
            {
                throw new ArgumentException($"Provided parameter '{htmlColor}' is not valid");
            }

            htmlColor = htmlColor.TrimStart('#');


            // int[] symbols 
            var symbolCount = htmlColor.Length;
            var value = int.Parse(htmlColor, System.Globalization.NumberStyles.HexNumber);
            switch (symbolCount)
            {
                case 3: // RGB short hand
                    {
                        return defaultAlpha << 24
                            | (value & 0xF)
                            | (value & 0xF) << 4
                            | (value & 0xF0) << 4
                            | (value & 0xF0) << 8
                            | (value & 0xF00) << 8
                            | (value & 0xF00) << 12
                            ;
                    }
                case 4: // RGBA short hand
                    {
                        // Inline alpha swap
                        return (value & 0xF) << 24
                               | (value & 0xF) << 28
                               | (value & 0xF0) >> 4
                               | (value & 0xF0)
                               | (value & 0xF00)
                               | (value & 0xF00) << 4
                               | (value & 0xF000) << 4
                               | (value & 0xF000) << 8
                               ;
                    }
                case 6: // RGB complete definition
                    {
                        return defaultAlpha << 24 | value;
                    }
                case 8: // RGBA complete definition
                    {
                        // Alpha swap
                        return (value & 0xFF) << 24 | (value >> 8);
                    }
                default:
                    throw new FormatException("Invalid HTML Color");
            }
        }

        internal static Color GetChartSeriesColor(int iColor)
        {
            switch (iColor)
            {
                case 0:
                    return Color.Green;
                case 1:
                    return Color.Orange;
                case 2:
                    return Color.Red;
                case 3:
                    return Color.Blue;
                case 4:
                    return Color.Cyan;
                case 5:
                    return Color.DarkSeaGreen;
                default:
                    return Color.Gray;
            }
        }
    }

   
}
