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
	public partial class MasterViewController : UIViewController
	{
		private Token _twitterToken;

		public MasterViewController(IntPtr handle) 
			: base(handle)
		{
		}
			
		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			using (var service = new RequestService())
			{
				_twitterToken = await service.GetAccessToken();
			}

			await GetTweets();
		}

		private async Task GetTweets()
		{
			using (var service = new RequestService())
			{
				if (string.IsNullOrEmpty(_twitterToken?.access_token))
				{
					return;
				}

				var viewModel = await service.SearchTweetsAsync(string.Empty, 5, _twitterToken.access_token);
				if (viewModel != null)
				{
					var tableViewController = viewModel.Tweets.GetController(CreateCommentCell, BindCommentCell);
					AddChildViewController(tableViewController);
					Add(tableViewController.View);
				}
			}
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
	}
}

