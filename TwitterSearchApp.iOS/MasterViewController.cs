﻿using System;
using System.Drawing;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using TwitterSearch.Portable.ViewModels;
using UIKit;

namespace TwitterSearchApp.iOS
{
	public partial class MasterViewController : UIViewController, IUISearchResultsUpdating
	{
		private readonly TweetsViewModel _viewModel;
		private UISearchController _searchController;

		public MasterViewController(IntPtr handle) 
			: base(handle)
		{
			_viewModel = new TweetsViewModel();
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			var tableViewController = _viewModel.Tweets.GetController(CreateCommentCell, BindCommentCell);
			tableViewController.SelectionChanged += (s, e) =>
			{
				var viewController = new MapViewController {Tweet = tableViewController.SelectedItem};
				NavigationController.PushViewController(viewController, true);
			};
			AddChildViewController(tableViewController);
			AddSearchBar(tableViewController);
			AddView(tableViewController);

			await _viewModel.Initialise();
		}

		public async void UpdateSearchResultsForSearchController(UISearchController searchController)
		{
			var searchString = searchController.SearchBar.Text;
			await _viewModel.GetTweets(searchString);
		}

		private void AddSearchBar(UITableViewController tableViewController)
		{
			_searchController = new UISearchController(searchResultsController: null)
			{
				SearchResultsUpdater = this,
				DimsBackgroundDuringPresentation = true,
			};

			_searchController.SearchBar.SizeToFit();

			tableViewController.TableView.TableHeaderView = _searchController.SearchBar;
			DefinesPresentationContext = true;
		}

		private static UITableViewCell CreateCommentCell(NSString reuseId)
		{
			return new UITableViewCell(UITableViewCellStyle.Subtitle, null);
		}

		private static void BindCommentCell(UITableViewCell cell, TweetViewModel tweetViewModel, NSIndexPath path)
		{
			cell.TextLabel.Text = tweetViewModel.User;
			cell.DetailTextLabel.Text = tweetViewModel.Text;
			//cell.AccessoryView = new UIImageView(UIImage.FromBundle(tweetViewModel.ImageName));
		}

		private void AddView(UITableViewController viewController)
		{
			viewController.TableView.Frame = new RectangleF(
				(float)viewController.TableView.Frame.X,
				(float)ContentView.Frame.Y,
				(float)View.Frame.Width,
				(float)View.Frame.Height);

			ContentView.Add(viewController.TableView);
		}
	}
}

