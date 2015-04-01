using Foundation;
using TwitterSearch.Portable.Views;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace TwitterSearchApp.iOS.Forms
{
	[Register("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Xamarin.Forms.Forms.Init();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}