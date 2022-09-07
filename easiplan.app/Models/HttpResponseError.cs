using System;
using System.Collections.Generic;

namespace Finx.App.Models
{
    public class HttpResponseError
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public List<FieldErrorModel> FieldErrors { get; set; }
        public long Timestamp { get; set; }
    }
}