using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace TwitterSearchApp.iOS
{
	public class Application
	{
		static void Main(string[] args)
		{
			try
			{
				UIApplication.Main(args, null, "AppDelegate");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}