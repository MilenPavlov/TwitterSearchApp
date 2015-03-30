using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using Android.Graphics;
using TwitterSearch.Portable.ViewModels;

namespace TwitterSearchApp
{
    public class TweetsAdapter : BaseAdapter<TweetViewModel>
    {
        private readonly Activity _context;
        private readonly ObservableCollection<TweetViewModel> _tweets;

        public TweetsAdapter(Activity context, ObservableCollection<TweetViewModel> tweets)
        {
            _context = context;
            _tweets = tweets;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var tweet = _tweets[position];

            View view = convertView ??
                        _context.LayoutInflater.Inflate(Resource.Layout.tweetlayout, null);

            view.FindViewById<TextView>(Resource.Id.textViewPostText).Text = tweet.Text;

            view.FindViewById<TextView>(Resource.Id.textViewPostAuthor).Text = tweet.User;
         
            return view;
        }

        public override int Count
        {
            get
            {
                return _tweets.Count;
            }
        }

        private bool IsValidUrl(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return false;
            }

            return true;
        }

        public override TweetViewModel this[int position] {
            get
            {
                return _tweets[position];
            }
        }
    }
}