using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSearch.Portable.Abstract
{
    public interface IRequestService : IDisposable
    {
        void SetUpAuth(string accessToken, string accessTokenSecret, string consumerKey, string consumerKeySecret);

        Task<string> DoTwitterSearchAsync(string queryString, int radiusInMiles);
    }
}
