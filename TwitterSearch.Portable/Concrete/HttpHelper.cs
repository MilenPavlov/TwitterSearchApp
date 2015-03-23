using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSearch.Portable.Concrete
{
    using System.Net.Http;

    using Newtonsoft.Json;

    using TwitterSearch.Portable.Abstract;

    public class HttpHelper: IHttpHelper
    {
        private string BaseUrl;

        //todo Add base url 
        public HttpHelper()
        {
            BaseUrl = "";
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
    }
}
