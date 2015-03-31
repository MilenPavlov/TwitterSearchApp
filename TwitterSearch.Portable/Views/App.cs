using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterSearch.Portable.ViewModels;
using Xamarin.Forms;

namespace TwitterSearch.Portable.Views
{
	public class App
	{
		public static Page GetMainPage()
		{
			var viewModel = new TweetsViewModel();
			var mainPage = new TweetsListView(viewModel);
			return new NavigationPage(mainPage);
		}
	}
}
