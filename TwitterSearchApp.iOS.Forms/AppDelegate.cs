using Foundation;
using TwitterSearch.Portable.Views;
using UIKit;
using Xamarin.Forms;

namespace TwitterSearchApp.iOS.Forms
{
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Xamarin.Forms.Forms.Init();

			window = new UIWindow(UIScreen.MainScreen.Bounds)
			{
				RootViewController = App.GetMainPage().CreateViewController()
			};

			window.MakeKeyAndVisible();

			return true;
		}
	}
}