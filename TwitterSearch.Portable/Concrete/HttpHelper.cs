using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSearch.Portable.Concrete
{
    using System.IO;
    using System.IO.Compression;
    using System.Net.Http;
    using System.Net.Http.Headers;

    using Newtonsoft.Json;


    using TwitterSearch.Portable.Abstract;
    using TwitterSearch.Portable.Models;

    public class HttpHelper: IHttpHelper
    {
        private string BaseUrl;

        private const string postBody = "grant_type=client_credentials";

        private const string oauth_url = "https://api.twitter.com/oauth2/token";

        //todo Add base url 
        public HttpHelper()
        {
            BaseUrl = "";
        }

        public async Task<object> GeTokenAsync()
        {
          
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                                                                                "Basic",
                                                                                Convert.ToBase64String(
                                                                                    Encoding.UTF8
                                                                                    .GetBytes(
                                                                                        string.Format("{0}:{1}", Uri.EscapeDataString(Constants.ConsumerKey),
                                                                                                        Uri.EscapeDataString(Constants.ConsumerSecret)))));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Host", "api.twitter.com");
        
                var content = new StringContent(postBody);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await client.PostAsync(oauth_url, content);
                response.EnsureSuccessStatusCode();

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
                using (var streamReader = new StreamReader(decompressedStream))
                {
                    var rawJWt = streamReader.ReadToEnd();
                    var jwt = JsonConvert.DeserializeObject(rawJWt);

                    return jwt;
                }               
            };
        }

        public string ConvertToUrlParameters(Dictionary<string, string> keyValueCollection)
        {
            var builder = new StringBuilder();
            foreach (var item in keyValueCollection)
            {
                if (builder.Length > 0)
                    builder.Append("&");
                var param = ConvertToUrlParameter(item.Key, item.Value);
                builder.Append(param);
            }
            return builder.ToString();
        }

        public string ConvertToUrlParameter(string key, string value)
        {
            var encodedValue = Uri.EscapeDataString(value);
            var result = string.Format("{0}={1}", key, encodedValue);
            return result;
        }


        public async Task<T> GetAsync<T>(string relativeUrl, Dictionary<string, string> urlParameters) where T : class
        {
            var parameters = ConvertParameters(urlParameters);
            var validUrl = GetValidatedApiUrl(BaseUrl, relativeUrl, parameters);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(validUrl);
                var result =  await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            };
        }

        public async Task<T> GetAsync<T>(string relativeUrl) where T : class
        {
            var validUrl = GetValidatedApiUrl(BaseUrl, relativeUrl, null);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(validUrl);
                var result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(result);

            };
        }

        public Uri GetValidatedApiUrl(string baseUrl, string relativeApiRoute, string parameters)
        {
            var fullUrl = string.Format("{0}/{1}", BaseUrl, relativeApiRoute);
            if (!string.IsNullOrWhiteSpace(parameters))
                fullUrl = string.Format("{0}?{1}", fullUrl, parameters);

            return new Uri(fullUrl);
        }

        public string ConvertParameters(Dictionary<string, string> keyValueCollection)
        {
            var builder = new StringBuilder();
            foreach (var item in keyValueCollection)
            {
                if (builder.Length > 0)
                    builder.Append("&");
                var param = ConvertParameter(item.Key, item.Value);
                builder.Append(param);
            }
            return builder.ToString();
        }

        public string ConvertParameter(string key, string value)
        {
            var encodedValue = Uri.EscapeUriString(value);
            var result = string.Format("{0}={1}", key, encodedValue);
            return result;
        }

        public void Dispose()
        {
            
        }
    }
}
