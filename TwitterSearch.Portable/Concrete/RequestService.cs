using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TwitterSearch.Portable.Concrete
{
    using Tweetinvi;
    using Tweetinvi.Core.Enum;
    using Tweetinvi.Core.Interfaces.Credentials;

    using TwitterSearch.Portable.Abstract;
    using TwitterSearch.Portable.Models;

    public class RequestService : IRequestService
    {
        private const string longitude = "-1.8939172029495";

        private const string latitude = "52.47794740466092";
 
        public void SetUpAuth(string accessToken, string accessTokenSecret, string consumerKey, string consumerKeySecret)
        {
            TwitterCredentials.SetCredentials(accessToken, accessTokenSecret, consumerKey, consumerKeySecret);
        }

        public async Task<string> DoTwitterSearchAsync(string queryString, int radiusInMiles)
        {
            // Complex search
            var searchParameter = Search.GenerateTweetSearchParameter("tweetinvi");
            searchParameter.SetGeoCode(-122.398720, 37.781157, 1, DistanceMeasure.Miles);
            searchParameter.Lang = Language.English;
            searchParameter.SearchType = SearchResultType.Popular;
            searchParameter.MaximumNumberOfResults = 100;
            searchParameter.Until = new DateTime(2013, 12, 1);
            searchParameter.SinceId = 399616835892781056;
            searchParameter.MaxId = 405001488843284480;
            var tweets = await SearchAsync.SearchTweets(searchParameter);

            try
            {
                return JsonConvert.SerializeObject(tweets, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });
            }
            catch (Exception ex)
            {
                
                throw;
            }
  
        }

        public async Task<Token> GetAccessToken()
        {
            using (var client = new HttpHelper())
            {
                return await client.GeTokenAsync();
            }
        }

        public async Task<string> SearchTweetsAsync(string query, int radiusInMiles, string token)
        {
            var querystring = CreateQueryStrung(query, radiusInMiles);

            using (var helper = new HttpHelper())
            {
                var response = await helper.SearchTwitter(querystring, token);

                return response;
            }
        }

        private string CreateQueryStrung(string query, int radiusInMiles)
        {
            var builder = new StringBuilder();
            var encodedQuery = Uri.EscapeUriString(query);
            builder.Append(encodedQuery);
            var encodedRadius =
                Uri.EscapeUriString(string.Format("&geocode={0},{1},{2}mi", latitude, longitude, radiusInMiles));
            builder.Append(encodedRadius);

            return builder.ToString();
        }


        private bool disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                //
            }

            // Free any unmanaged objects here. 
            //
            disposed = true;
            // Call base class implementation.         
        }
    }
}
