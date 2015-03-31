using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using TwitterSearch.Portable.Concrete;
using TwitterSearch.Portable.Models;

namespace TwitterSearch.Portable.ViewModels
{
    public class TweetsViewModel : ViewModelBase
    {
        public ObservableCollection<TweetViewModel> Tweets { get; set; } = new ObservableCollection<TweetViewModel>();

		private Token _twitterToken;

		public async Task Initialise()
	    {
			using (var service = new RequestService())
			{
				_twitterToken = await service.GetAccessToken();
			}

			await GetTweets();
		}

	    public async Task GetTweets(string searchString = "", int radiusInMiles = 5)
	    {
			using (var service = new RequestService())
			{
				if (string.IsNullOrEmpty(_twitterToken?.access_token))
				{
					return;
				}

				var list = await service.SearchTweetsAsync(searchString, radiusInMiles, _twitterToken?.access_token);

				Tweets.Clear();
				foreach (var tweet in list)
				{
					Tweets.Add(tweet);
				}
			}
		}
    }
}
