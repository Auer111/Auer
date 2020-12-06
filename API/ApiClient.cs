using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;


namespace Auer.Api
{
    public class ApiClient
    {

        public async static Task<string> CallWebService(Request RequestData)
        {
            try
            {
                var _uri = new Uri(RequestData.Url);

                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent("");
                    if (RequestData.Method != Method.GET) 
                    {
                        
                        if (!string.IsNullOrWhiteSpace(RequestData.Body)) {
                            content = new StringContent(RequestData.Body);
                        }
                        if (content.Headers.Any()) 
                        {
                            content.Headers.Clear();
                            foreach (Header header in RequestData.Headers)
                            {
                                content.Headers.Add(header.Name, header.Value);
                            }
                        }
                        content.Headers.ContentType = MediaTypeHeaderValue.Parse(SetMediaType(RequestData.Format.ToString()));
                    }

                    HttpResponseMessage response = null;
                    switch (RequestData.Method) {
                        case Method.POST: response = await client.PostAsync(_uri, content); break;
                        case Method.GET: response = await client.GetAsync(_uri); break;
                        default: throw new NotImplementedException();

                    }
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return response.ReasonPhrase;
                    }
                }
            }
            catch(Exception)
            {
                return HttpStatusCode.InternalServerError.ToString();
            }
            
        }



        private static string SetMediaType(string format)
        {
            switch (format.ToUpper())
            {
                case "XML": return "text/xml";
                case "JSON": return "application/json";
                default: return "text/plain";
            }

        }
    }



    
}
