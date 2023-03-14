using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace easiplan.app.Extensions
{
    internal static class StringExt
    {
    internal static double AsDouble(this string str)    {

            if (string.IsNullOrEmpty(str)) return 0.0d;

            if (double.TryParse(str, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out var result))
            {
                return result;
            }

            Match m = Regex.Match(str, @"[0-9]+(\.[0-9]+)?");
            double number = Convert.ToDouble(m.Value, CultureInfo.InvariantCulture);
            return number;
    }

        internal static T FromJson<T>(this string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
