using System;
using System.Text;
using System.Threading.Tasks;
using TwitterSearch.Portable.Abstract;
using TwitterSearch.Portable.Models;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace TwitterSearch.Portable.Concrete
{ 
    public class HttpHelper: IHttpHelper
    {
        private const string postBody = "grant_type=client_credentials";
        private const string oauth_url = "https://api.twitter.com/oauth2/token";

        public async Task<Token> GeTokenAsync()
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
                    var jwt = JsonConvert.DeserializeObject<Token>(rawJWt);

                    return jwt;
                }               
            };
        }

        public async Task<TwitterSearchResponse> SearchTwitter(string srchStr, string token)
        {
            var searchUrl = string.Format("https://api.twitter.com/1.1/search/tweets.json?q={0}", srchStr);
            var uri = new Uri(searchUrl);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));

                var response = await client.GetAsync(uri);

                string content =  await response.Content.ReadAsStringAsync();
                var tweets = JsonConvert.DeserializeObject<TwitterSearchResponse>(content);

                return tweets;
            }
        }

        public void Dispose()
        {            
        }
    }
}
