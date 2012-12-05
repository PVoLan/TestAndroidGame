using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TestAndroidGame
{
	[Activity (Label = "TestAndroidGame", MainLauncher = true)]
	public class Activity1 : Activity
	{
		int count = 1;


		GameFieldLayout _layout;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			/*
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);
			
			button.Click += delegate
			{
				button.Text = string.Format("{0} clicks!", count++);
			};*/

			_layout = new GameFieldLayout(this);

			SetContentView(_layout);
		}


		protected override void OnResume()
		{
			base.OnResume();
			_layout.StartDrawing();
		}

		protected override void OnPause()
		{
			base.OnPause();
			_layout.StopDrawing();
		}
	}
}


