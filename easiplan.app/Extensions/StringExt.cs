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

        //Method to convert string to double
        internal static double AsDouble(this string str)    
        {

            if (string.IsNullOrEmpty(str)) return 0.0d;

            if (double.TryParse(str, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out var result))
            {
                return result;
            }

            Match m = Regex.Match(str, @"[0-9]+(\.[0-9]+)?");
            double number = Convert.ToDouble(m.Value, CultureInfo.InvariantCulture);
            return number;
        }

        //Method to capitilze the first letter of each word in a string of words
        internal static string FormatEasiworxString(this string str)
        {
            str = str.ToLower().Trim();
            string[] words = str.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (word.Length > 0)
                {
                    char firstLetter = char.ToUpper(word[0]);
                    string restOfWord = word.Substring(1);
                    words[i] = firstLetter + restOfWord;
                }
            }
            return string.Join(" ", words);
        }


        internal static T FromJson<T>(this string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
