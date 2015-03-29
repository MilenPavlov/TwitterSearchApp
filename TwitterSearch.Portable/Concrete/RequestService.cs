using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TwitterSearch.Portable.Abstract;
using TwitterSearch.Portable.Models;
using TwitterSearch.Portable.ViewModels;

namespace TwitterSearch.Portable.Concrete
{


    public class RequestService : IRequestService
    {
        private const string longitude = "-1.7889407873153";
        private const string latitude = "52.38200776784398";
 

        public async Task<Token> GetAccessToken()
        {
            using (var client = new HttpHelper())
            {
                return await client.GeTokenAsync();
            }
        }

        public async Task<TweetsViewModel> SearchTweetsAsync(string query, int radiusInMiles, string token)
        {
            var querystring = CreateQueryStrung(query, radiusInMiles);

            using (var helper = new HttpHelper())
            {
                var response = await helper.SearchTwitter(querystring, token);
                var viewModel = TweetsViewModelFactory.Create(response);

                return viewModel;
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
