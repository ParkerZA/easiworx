using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finx.App.Extensions
{
    public static class DateTimeExt
    {
        public static string ToUTCString(this DateTime date)
        {
            DateTime _utcDate = date.ToUniversalTime();

            return string.Format("{0}-{1}-{2} {3}:{4}:{5}", _utcDate.Year, _utcDate.Month.ToString("0#"), _utcDate.Day.ToString("0#"), _utcDate.Hour.ToString("0#"), _utcDate.Minute.ToString("0#"), _utcDate.Second.ToString("0#"));
        }

    }
}
