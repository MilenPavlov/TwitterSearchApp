using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterSearch.Portable.ViewModels;
using Xamarin.Forms;

namespace TwitterSearch.Portable.Views
{
	public partial class TweetsListView : ContentPage
	{
		private readonly TweetsViewModel _viewModel;

		public TweetsListView(TweetsViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = viewModel;

			_viewModel = viewModel;
			var task = viewModel.Initialise();
		}

		public void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var selectedItem = args.SelectedItem as TweetViewModel;
			if (selectedItem!= null)
			{
				var mapView = new TweetsMapView(selectedItem);
				Navigation.PushAsync(mapView, true);
			}
		}

		public async void OnValueChanged(object sender, TextChangedEventArgs args)
		{
			await _viewModel.GetTweets(args.NewTextValue);
		}

		public async void OnSearch(object sender, EventArgs args)
		{
			await _viewModel.GetTweets(Search.Text);
		}
	}
}
