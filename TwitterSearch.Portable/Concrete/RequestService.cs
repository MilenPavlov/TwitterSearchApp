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

    using TwitterSearch.Portable.Abstract;

    public class RequestService : IRequestService
    {
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
