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
        private TweetsViewModel viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
         
            SetContentView(Resource.Layout.Main);


            AttachControls();

            SetUpCredentials();

            //SetBundings();
        }

        private void SetBundings()
        {
            throw new NotImplementedException();
        }

        private async void SetUpCredentials()
        {
            viewModel = new TweetsViewModel();
            await viewModel.Initialise(() =>
            {
                
            }, true);
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

                //if(listViewData.Adapter != null)
                //{
                //    listViewData.Adapter = null;
                //}

                await viewModel.GetTweets(searchText.Text, Convert.ToInt32(searchRadius.Text));    
                
                PopulateListView();              
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

        private void PopulateListView()
        {
            DisplayLoading(false);
            
            if (viewModel.Tweets != null)
            {
                var adapter = new TweetsAdapter(this, viewModel.Tweets.ToList<TweetViewModel>());
                listViewData.Adapter = adapter;
            }
        }
    }
}

