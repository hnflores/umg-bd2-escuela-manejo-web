using System;
using System.Collections.Generic;

namespace ESC_MANEJO.CORE.Entities.Services
{
    public class HttpServiceRequest
    {
        public Dictionary<string, string> Headers { get; set; }
        public Uri Uri { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
        public bool IsFormData { get; set; } = false;
    }
}
