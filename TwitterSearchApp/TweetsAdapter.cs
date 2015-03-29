using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Android.Graphics;
using TwitterSearch.Portable.ViewModels;

namespace TwitterSearchApp
{
    public class TweetsAdapter : BaseAdapter<TweetViewModel>
    {
        private readonly Activity _context;
        private readonly List<TweetViewModel> _tweets;

        public TweetsAdapter(Activity context, List<TweetViewModel> tweets)
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

            if (IsValidUrl(tweet.ImageUrl))
            {
                var image = GetImageBitmapFromUrl(tweet.ImageUrl);
                view.FindViewById<ImageView>(Resource.Id.imageViewPostImage).SetImageBitmap(image);
            }
            else
            {
                view.FindViewById<ImageView>(Resource.Id.imageViewPostImage).Visibility = ViewStates.Invisible;
            }
         
            return view;
        }

        private bool IsValidUrl(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return false;
            }

            return true;
        }

        private  Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }


        public override int Count => _tweets.Count;

        public override TweetViewModel this[int position] => _tweets[position];
    }
}