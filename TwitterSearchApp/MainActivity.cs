﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TwitterSearchApp
{
    using TwitterSearch.Portable.Concrete;

    [Activity(Label = "Twitter Search App", MainLauncher = true, Icon = "@drawable/icon")]

    public class MainActivity : Activity
    {
        private EditText searchText, searchRadius;

        private Button searchButton;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            AttachControls();

            SetUpCredentials();
        }

        private void SetUpCredentials()
        {
            using (var service = new RequestService())
            {
                service.SetUpAuth();
            }
        }

        private void AttachControls()
        {
            searchText = this.FindViewById<EditText>(Resource.Id.editTextSearchText);
            searchRadius = this.FindViewById<EditText>(Resource.Id.editTextSearchRadius);
            searchButton = this.FindViewById<Button>(Resource.Id.buttonSearch);
        }
    }
}

