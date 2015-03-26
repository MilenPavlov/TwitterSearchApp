using System;
using System.Drawing;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace TwitterSearchApp.iOS
{
	public partial class MasterViewController : UITableViewController
	{
		private DataSource _dataSource;

		public DetailViewController DetailViewController { get; set; }

		public MasterViewController(IntPtr handle) 
			: base(handle)
		{
			Title = NSBundle.MainBundle.LocalizedString("Master", "Master");
		}

		void AddNewItem(object sender, EventArgs args)
		{
			_dataSource.Objects.Insert(0, DateTime.Now);

			using (var indexPath = NSIndexPath.FromRowSection(0, 0))
			{
				TableView.InsertRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			NavigationItem.LeftBarButtonItem = EditButtonItem;

			var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, AddNewItem);
			NavigationItem.RightBarButtonItem = addButton;

			TableView.Source = _dataSource = new DataSource(this);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "showDetail")
			{
				var indexPath = TableView.IndexPathForSelectedRow;
				var item = _dataSource.Objects[indexPath.Row];

				((DetailViewController)segue.DestinationViewController).SetDetailItem(item);
			}
		}
	}
}

