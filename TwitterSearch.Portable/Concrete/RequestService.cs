using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TwitterSearch.Portable.Abstract;
using TwitterSearch.Portable.Models;
using TwitterSearch.Portable.ViewModels;

namespace TwitterSearch.Portable.Concrete
{
    public class RequestService : IRequestService
    {
        public async Task<Token> GetAccessToken()
        {
            using (var client = new HttpHelper())
            {
                return await client.GeTokenAsync();
            }
        }

        public async Task<List<TweetViewModel>> SearchTweetsAsync(string query, int radiusInMiles, string token, string resultCount)
        {
            var querystring = CreateQueryString(query, radiusInMiles, resultCount);

            using (var helper = new HttpHelper())
            {
                var response = await helper.SearchTwitter(querystring, token);
                var collection = TweetsCollectionFactory.Create(response);

                return collection;
            }
        }

        private string CreateQueryString(string query, int radiusInMiles, string resultsCount)
        {
            var builder = new StringBuilder();
            var encodedQuery = Uri.EscapeUriString(query);
            builder.Append(encodedQuery);
            var encodedRadius = Uri.EscapeUriString($"&geocode={Constants.Latitude},{Constants.Longitude},{radiusInMiles}mi&count={resultsCount}");
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
            {
                return;
            }

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
