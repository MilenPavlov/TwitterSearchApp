using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSearch.Portable.Abstract
{
    using TwitterSearch.Portable.Models;

    public interface IHttpHelper : IDisposable
    {
        Task<Token> GeTokenAsync();

        Task<TwitterSearchResponse> SearchTwitter(string srchStr, string token);
    }
}
