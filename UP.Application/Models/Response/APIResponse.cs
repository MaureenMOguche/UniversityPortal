using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UP.Application.Models
{
    public class APIResponse
    {
        public bool isSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public object Result { get; set; }
    }
}
