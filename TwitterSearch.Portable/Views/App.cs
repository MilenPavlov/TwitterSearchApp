using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterSearch.Portable.ViewModels;
using Xamarin.Forms;

namespace TwitterSearch.Portable.Views
{
	public class App : Application
	{
	    public App()
	    {
            var viewModel = new TweetsViewModel();
            MainPage = new NavigationPage(new TweetsListView(viewModel));
	    }
	}
}
