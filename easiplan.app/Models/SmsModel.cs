using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finx.App.Models
{
    public class SmsModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string APIUrl { get; set; }

        public string Message { get; set; }
        public string SmsNumber { get; set; }
    }
}
