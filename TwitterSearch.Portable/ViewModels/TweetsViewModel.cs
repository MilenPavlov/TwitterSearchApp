using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace TwitterSearch.Portable.ViewModels
{
    public class TweetsViewModel : ViewModelBase
    {
        public ObservableCollection<TweetViewModel> Tweets { get; set; } = new ObservableCollection<TweetViewModel>();
    }
}
