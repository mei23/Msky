using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net;

namespace Msky
{
    public class Credential
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public HttpClient Client { get; set; }

        public Credential(string baseUrl, string apiKey = null)
        {
            BaseUrl = baseUrl;
            ApiKey = apiKey;

            Client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                UseCookies = false
            });
        }
    }
}
