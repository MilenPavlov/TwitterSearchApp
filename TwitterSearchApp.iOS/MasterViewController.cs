using System;
using System.Drawing;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace TwitterSearchApp.iOS
{
	public partial class MasterViewController : UIViewController
	{
		public MasterViewController(IntPtr handle) 
			: base(handle)
		{
			Title = NSBundle.MainBundle.LocalizedString("Master", "Master");
		}
			
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}
	}
}

