using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace TwitterSearchApp.iOS
{
	public class DataSource : UITableViewSource
	{
		static readonly NSString CellIdentifier = new NSString("Cell");
		readonly List<object> objects = new List<object>();
		readonly MasterViewController controller;

		public DataSource(MasterViewController controller)
		{
			this.controller = controller;
		}

		public IList<object> Objects => objects;

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return objects.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);
			cell.TextLabel.Text = objects[indexPath.Row].ToString();

			return cell;
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				// Delete the row from the data source.
				objects.RemoveAt(indexPath.Row);
				controller.TableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
			}
			else if (editingStyle == UITableViewCellEditingStyle.Insert)
			{
				// Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
			}
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var detailViewController = (DetailViewController)controller.Storyboard.InstantiateViewController("DetailViewController");
			detailViewController.SetDetailItem(Objects[indexPath.Row]);

			var navigationController = new UINavigationController(detailViewController);
			navigationController.TopViewController.NavigationItem.LeftBarButtonItem = controller.SplitViewController.DisplayModeButtonItem;
			navigationController.TopViewController.NavigationItem.LeftItemsSupplementBackButton = true;
			controller.ShowDetailViewController(navigationController, controller);
		}
	}
}