using SecretsCredentials.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SecretsCredentials.Service
{
    public class StsService : IStsService
    {
        public string GetToken(string client_id, string client_secret)
        {
            string url = $"https://localhost:5001/api/Security/Token?ClientId={client_id}&ClientSecret={client_secret}";

            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetAsync(url);
            var contents = response.Result.Content.ReadAsStringAsync();
            return contents.Result;
            
        }

    }
}
