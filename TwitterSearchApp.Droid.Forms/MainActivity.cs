using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TwitterSearch.Portable.Views;
using Xamarin.Forms.Platform.Android;

namespace TwitterSearchApp.Droid.Forms
{
    [Activity(Label = "TwitterSearchApp.Droid.Forms", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FormsApplicationActivity
    {        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
        }
    }
}

