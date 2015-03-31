using System.Collections.Generic;
using System.Reflection;

namespace TwitterSearch.Portable.ViewModels
{
    public class TweetViewModel
    {
        public string User { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }

        public List<double> GpsCoordinates { get; set; }
    }

    public class GeoPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}