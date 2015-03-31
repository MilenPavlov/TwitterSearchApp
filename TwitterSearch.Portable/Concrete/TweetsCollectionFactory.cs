using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TwitterSearch.Portable.Models;
using TwitterSearch.Portable.ViewModels;

namespace TwitterSearch.Portable.Concrete
{
    public class TweetsCollectionFactory
    {
        public static List<TweetViewModel> Create(TwitterSearchResponse response)
        {
	        return response.statuses.Select(status => new TweetViewModel
	        {
		        User = $"@{status.user.screen_name}",
				Text = status.text,
                GpsCoordinates = new GeoPoint
                {
	                Latitude = status.coordinates.coordinates.First(),
					Longitude = status.coordinates.coordinates.Last()
				}
            }).ToList();
        }
    }
}