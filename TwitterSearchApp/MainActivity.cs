using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TwitterSearch.Portable.Models;

namespace TwitterSearchApp
{
    using System.Threading.Tasks;

    using TwitterSearch.Portable.Concrete;

    [Activity(Label = "Twitter Search App", MainLauncher = true, Icon = "@drawable/icon")]

    public class MainActivity : Activity
    {
        private object twitterToken; 
        private EditText searchText, searchRadius;

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
                //service.SetUpAuth(Constants.ConsumerKey, Constants.AccessTokenSecret, Constants.ConsumerKey, Constants.ConsumerSecret);
                twitterToken =  await service.GetAccessToken();

                if (twitterToken != null)
                {
                    
                }
            }
        }

        private void AttachControls()
        {
            searchText = this.FindViewById<EditText>(Resource.Id.editTextSearchText);
            searchRadius = this.FindViewById<EditText>(Resource.Id.editTextSearchRadius);
            searchButton = this.FindViewById<Button>(Resource.Id.buttonSearch);
            searchButton.Click += async (sender, args) =>
            {
                using (var service = new RequestService())
                {
                    await service.DoTwitterSearchAsync(searchText.Text, Convert.ToInt32(searchRadius.Text));
                }

                
            };


        }
    }
}

