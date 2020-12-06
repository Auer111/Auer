using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auer.Api
{

    public enum Method { GET, POST }
    public enum Format { JSON, XML }

    public partial class Request
    {
        public Method Method { get; set; }
        public Format Format { get; set; }
        public string Url { get; set; }
        public List<Header> Headers { get; set; }
        public string Body { get; set; }

    }

    public partial class Header
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }




    

}
