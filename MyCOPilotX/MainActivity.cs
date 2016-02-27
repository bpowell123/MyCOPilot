using Android.App;
using Android.Widget;
using Android.OS;
using MyCOPilotX;
using Microsoft.Band.Portable;
//using Microsoft.Band;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyCOPilot
{
	[Activity (Label = "MyCOPilot", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		public Band myBand { get; set; }
		//int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			//base.OnCreate (savedInstanceState);
				base.OnCreate(bundle);
				SetContentView(Resource.Layout.Main);

			// Set our view from the "main" layout resource
			//SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			Button button2 = FindViewById<Button> (Resource.Id.button1); 

			SetupBand();

			button.Click += delegate
			{
				if (myBand != null && myBand.BandClient.IsConnected)
					button.Text = string.Format("Connected to {0}!", myBand.Name);
				else
					Toast.MakeText(this, "Band not yet connected or not found!", ToastLength.Long);
			};

			button2.Click += delegate
			{
				if (myBand != null && myBand.BandClient.IsConnected)
					button.Text = string.Format("Connected to {0}!", myBand.Name);
				else
					Toast.MakeText(this, "Band not yet connected or not found!", ToastLength.Long);
			};

			/*button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};*/
		}

		private void SetupBand() {
			// get instance
			myBand = Band.Instance;
			myBand.PairBand();
		}



		/*public async void GetBand()
		{
			var bandClientManager = Microsoft.Band.Portable.BandClientManager.Instance;
			var bandClientManagerAsync = Microsoft.Band.BandClientManager.Instance;

			var pairedBands = await bandClientManagerAsync.GetPairedBandsAsync();
			var bandInfo = pairedBands.FirstOrDefault();
			var bandClient = await bandClientManager.ConnectAsync (bandInfo);
		}*/

	}

	public class Band {
		private static Band BandDevice;
		public BandClient BandClient { get; set; }
		public String Name { get; set; }

		private Band() {

		}

		public async void PairBand() {
			var bandClientManager = Microsoft.Band.Portable.BandClientManager.Instance;
			var bandsFound = await bandClientManager.GetPairedBandsAsync();

			if (bandsFound != null && bandsFound.Count() == 1)
			{
				var bandFound = bandsFound.FirstOrDefault();
				BandDevice.BandClient = await bandClientManager.ConnectAsync(bandFound);
				BandDevice.Name = bandFound.Name;
			}
		}

		public static Band Instance {
			get {
				if (BandDevice == null)
				{
					BandDevice = new Band();
				}
				return BandDevice;
			}
		}
	}
}


