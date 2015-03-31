using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLocation;
using Foundation;
using MapKit;
using TwitterSearch.Portable.Models;
using TwitterSearch.Portable.ViewModels;
using UIKit;

namespace TwitterSearchApp.iOS
{
	public class MapViewController : UIViewController
	{
		private MKMapView _map;
		private MapDelegate _delegate;

		public TweetViewModel Tweet { get; set; }

		public override void LoadView()
		{
			_map = new MKMapView(UIScreen.MainScreen.Bounds);
			View = _map;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_map.MapType = MKMapType.Standard;
			_map.ShowsUserLocation = true;
			_map.ZoomEnabled = true;
			_map.ScrollEnabled = true;

			var mapCenter = new CLLocationCoordinate2D(Tweet.GpsCoordinates.Latitude, Tweet.GpsCoordinates.Longitude);
			var mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 200, 00);
			_map.CenterCoordinate = mapCenter;
			_map.Region = mapRegion;

			_delegate = new MapDelegate();
			_map.Delegate = _delegate;

			var mkPointAnnotation = new MKPointAnnotation
			{
				Title = Tweet.User,
				Subtitle = Tweet.Text
			};
			mkPointAnnotation.SetCoordinate(new CLLocationCoordinate2D(Tweet.GpsCoordinates.Latitude, Tweet.GpsCoordinates.Longitude));
			_map.AddAnnotation(mkPointAnnotation);
		}
	}

	public class MapDelegate : MKMapViewDelegate
	{
		private const string pId = "PinAnnotation";

		public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
		{
			if (annotation is MKUserLocation)
			{
				return null;
			}

			MKAnnotationView pinView = mapView.DequeueReusableAnnotation(pId) as MKPinAnnotationView ??
			                           new MKPinAnnotationView(annotation, pId);

			((MKPinAnnotationView)pinView).PinColor = MKPinAnnotationColor.Red;
			pinView.CanShowCallout = true;

			return pinView;
		}
	}
}