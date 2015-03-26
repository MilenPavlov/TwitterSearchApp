using System;
using Foundation;
using UIKit;

namespace TwitterSearchApp.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			return true;
		}

		public override void OnResignActivation(UIApplication application)
		{
		}

		public override void DidEnterBackground(UIApplication application)
		{
		}

		public override void WillEnterForeground(UIApplication application)
		{
		}

		public override void WillTerminate(UIApplication application)
		{
		}
	}
}