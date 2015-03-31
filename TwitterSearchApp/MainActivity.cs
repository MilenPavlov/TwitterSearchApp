﻿using System;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using Android.OS;
using GalaSoft.MvvmLight.Helpers;
using TwitterSearch.Portable.Models;
using TwitterSearch.Portable.ViewModels;

namespace TwitterSearchApp
{
    [Activity(Label = "Twitter Search App", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light.NoActionBar.Fullscreen", ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        private EditText searchText, searchRadius;
        private ListView listViewData;
        private TextView loading;
        private ImageView imageLoading;
        private Button searchButton;
        private TweetsViewModel viewModel;

        private ObservableAdapter<TweetViewModel> _observableAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
         
            SetContentView(Resource.Layout.Main);


            AttachControls();

            SetUpCredentials();

            SetBinding();
        }

        private void SetBinding()
        {
            viewModel.SetBinding(() => viewModel.Tweets)
                .WhenSourceChanges(this.RefreshData);
        }

        private void RefreshData()
        {
            _observableAdapter = viewModel.Tweets.GetAdapter(UpdateTemplate);
            listViewData.Adapter = _observableAdapter;
        }

        private View UpdateTemplate(int position, TweetViewModel tvm, View convertView)
        {
            View view = convertView ??
                        this.LayoutInflater.Inflate(Resource.Layout.tweetlayout, null);

            view.FindViewById<TextView>(Resource.Id.textViewPostText).Text = tvm.Text;

            view.FindViewById<TextView>(Resource.Id.textViewPostAuthor).Text = tvm.User;

            return view;
        }

        private async void SetUpCredentials()
        {
            viewModel = new TweetsViewModel();
            await viewModel.Initialise();
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

                await viewModel.GetTweets(searchText.Text, Convert.ToInt32(searchRadius.Text));
                this.DisplayLoading(false);
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
    }
}

