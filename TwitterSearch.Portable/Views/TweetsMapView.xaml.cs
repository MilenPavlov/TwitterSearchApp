using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterSearch.Portable.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TwitterSearch.Portable.Views
{
	public partial class TweetsMapView : ContentPage
	{
		public TweetsMapView(TweetViewModel tweet)
		{
			InitializeComponent();

			var pin = new Pin
			{
				Position = new Position(tweet.GpsCoordinates.Latitude, tweet.GpsCoordinates.Longitude),
				Label = tweet.User,
				Address = tweet.Text
			};

			Map.Pins.Add(pin);
		}
	}
}
