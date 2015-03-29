using System;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using TwitterSearch.Portable.Concrete;
using TwitterSearch.Portable.Models;
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

			await _viewModel.Initialise(() =>
			{
				var tableViewController = _viewModel.Tweets.GetController(CreateCommentCell, BindCommentCell);
				AddSearchBar(tableViewController);
				AddChildViewController(tableViewController);
				AddView(tableViewController);
			});
		}

		public async void UpdateSearchResultsForSearchController(UISearchController searchController)
		{
			var searchString = searchController.SearchBar.Text;
			await _viewModel.GetTweets(searchString);
		}

		private void AddSearchBar(UITableViewController tableViewController)
		{
			_searchController = new UISearchController(tableViewController)
			{
				SearchResultsUpdater = this,
				DimsBackgroundDuringPresentation = false
			};

			_searchController.SearchBar.Frame = new RectangleF(
				(float) _searchController.SearchBar.Frame.X,
				(float) _searchController.SearchBar.Frame.Y,
				(float) _searchController.SearchBar.Frame.Width,
				44f);

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
				(float)viewController.View.Frame.Y,
				(float)View.Frame.Width,
				(float)View.Frame.Height);

			Add(viewController.TableView);
		}
	}
}

