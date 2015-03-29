using System.Linq;
using TwitterSearch.Portable.Models;
using TwitterSearch.Portable.ViewModels;

namespace TwitterSearch.Portable.Concrete
{
    public class TweetsViewModelFactory
    {
        public static TweetsViewModel Create(TwitterSearchResponse response)
        {
            var viewModel = new TweetsViewModel();

            foreach (var status in response.statuses)
            {
                var tweet = new TweetViewModel
                {
                    User = $"@{status.user.screen_name}",
                    Text = status.text,
                    //ImageUrl = status.user.entities.url.urls.FirstOrDefault() == null ? string.Empty : status.user.entities.url.urls.FirstOrDefault().expanded_url
                };
                viewModel.Tweets.Add(tweet);
            }

            return viewModel;
        }
    }
}