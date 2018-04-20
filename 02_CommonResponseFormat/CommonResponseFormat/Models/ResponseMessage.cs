using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPISamples.Models
{
    public class ResponseMessage
    {
        public bool IsSuccess { get; set; }
        public int HttpStatusCode { get; set; }        
        public string Message { get; set; }
        public string DebugMessage { get; set; }
        public object Content { get; set; }
    }
}
