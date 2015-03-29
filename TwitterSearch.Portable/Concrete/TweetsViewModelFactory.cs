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
                    Text = status.text
                };
                viewModel.Tweets.Add(tweet);
            }

            return viewModel;
        }
    }
}