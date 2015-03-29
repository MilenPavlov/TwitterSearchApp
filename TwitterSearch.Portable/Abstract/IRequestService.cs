using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterSearch.Portable.ViewModels;

namespace TwitterSearch.Portable.Abstract
{
    using TwitterSearch.Portable.Models;

    public interface IRequestService : IDisposable
    {
        Task<Token> GetAccessToken();
        Task<TweetsViewModel> SearchTweetsAsync(string query, int radiusInMiles, string token);
    }
}
