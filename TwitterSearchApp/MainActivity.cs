using System;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using Android.OS;
using TwitterSearch.Portable.Models;
using TwitterSearch.Portable.ViewModels;

namespace TwitterSearchApp
{
    using TwitterSearch.Portable.Concrete;

    [Activity(Label = "Twitter Search App", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light.NoActionBar.Fullscreen", ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        private Token twitterToken; 
        private EditText searchText, searchRadius;
        private ListView listViewData;
        private TextView loading;
        private ImageView imageLoading;

        private Button searchButton;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            AttachControls();

            SetUpCredentials();
        }

        private async void SetUpCredentials()
        {
            using (var service = new RequestService())
            {
                twitterToken =  await service.GetAccessToken();
            }
        }

        private void AttachControls()
        {
            searchText = this.FindViewById<EditText>(Resource.Id.editTextSearchText);
            searchRadius = this.FindViewById<EditText>(Resource.Id.editTextSearchRadius);
            searchButton = this.FindViewById<Button>(Resource.Id.buttonSearch);
            listViewData = FindViewById<ListView>(Resource.Id.listView1);
            loading = FindViewById<TextView>(Resource.Id.textViewLoading);
            imageLoading = FindViewById<ImageView>(Resource.Id.imageViewLoading);
            DisplayLoading(false);
            searchButton.Click += async (sender, args) =>
            {
                DisplayLoading(true);

                if(listViewData.Adapter != null)
                {
                    listViewData.Adapter = null;
                }
                using (var service = new RequestService())
                {
                    if (twitterToken != null)
                    {
                        if (!string.IsNullOrEmpty(twitterToken.access_token))
                        {
                            var result = await service.SearchTweetsAsync(
                                searchText.Text,
                                Convert.ToInt32(searchRadius.Text),
                                twitterToken.access_token);

                            if (result != null)
                            {
                                RunOnUiThread(()=>PopulateListView(result));
                            }
                        }                       
                    }
                }                
            };
        }

        private void DisplayLoading(bool showItems)
        {
            if (showItems)
            {
                loading.Visibility = ViewStates.Visible;
                imageLoading.Visibility = ViewStates.Visible;
            }
            else
            {
                loading.Visibility = ViewStates.Invisible;
                imageLoading.Visibility = ViewStates.Invisible;
            }
        }

        private void PopulateListView(TweetsViewModel result)
        {
            DisplayLoading(false);
            var tweets = result.Tweets.ToList<TweetViewModel>();

            if (tweets != null)
            {
                var adapter = new TweetsAdapter(this, tweets);
                listViewData.Adapter = adapter;
            }
        }
    }
}

