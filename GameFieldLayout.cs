
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace TestAndroidGame
{
	public class GameFieldLayout : FrameLayout
	{

		DrawAsyncTask _drawer;
		IGameFieldLogic _logic;

		Paint fpsPaint;

		public GameFieldLayout(IntPtr i, JniHandleOwnership j) : base(i,j){}

		public GameFieldLayout(Context context) :
			base (context)
		{
			Initialize();
		}

		public GameFieldLayout(Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize();
		}

		public GameFieldLayout(Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize();
		}

		private void Initialize()
		{
			_logic = new TestGameFieldLogic();

			SetBackgroundColor(Color.Red);

			fpsPaint = new Paint();
			fpsPaint.Color = Color.Blue;
			fpsPaint.TextSize = 18;
		}


		DateTime _lastDraw = DateTime.MinValue;
		int howOfhenMeasueFps = 10;

		int drawCounter = 0;
		int fps = 0;

		public override void Draw(Android.Graphics.Canvas canvas)
		{
			base.Draw(canvas);

			_logic.DoDraw(canvas);

			canvas.DrawText(fps.ToString(), 100,100, fpsPaint);

			if(drawCounter >= howOfhenMeasueFps)
			{
				var now = DateTime.Now;
				var betweenDraws = now - _lastDraw;
				fps = (int)((float)(new TimeSpan(0,0,1).Ticks)/ (((float)betweenDraws.Ticks) / (float)drawCounter ));
				//Console.WriteLine ( "fps" + fps );

				_lastDraw = now;
				drawCounter = 0;
			}

			drawCounter++;
		}

		private void DoLogic(float secFromLastTick)
		{
			_logic.DoLogic(secFromLastTick);
		}


		public void StartDrawing()
		{
			StopDrawing();
			_drawer = new DrawAsyncTask(this);
			_drawer.Execute();
		}

		public void StopDrawing()
		{
			if(_drawer == null) return;

			_drawer.StopMe();
			//_drawer.Dispose();
			_drawer = null;
		}

		class DrawAsyncTask : AsyncTask<int, int, int>
		{
			public DrawAsyncTask(IntPtr i, JniHandleOwnership j) : base(i,j){}

			GameFieldLayout _layout;
			bool _stopped = false;

			public DrawAsyncTask(GameFieldLayout layout) : base()
			{
				_layout = layout;
			}


			protected override int RunInBackground(params int[] @params)
			{
				DateTime lastTick = DateTime.Now;

				while(!_stopped)
				{
					DateTime now = DateTime.Now;
					var secFromLastTick = (now-lastTick).TotalSeconds;
					lastTick = now;

					_layout.DoLogic((float)secFromLastTick);

					PublishProgress();
				}

				return 0;
			}

			protected override void OnProgressUpdate(params int[] values)
			{
				base.OnProgressUpdate(values);

				_layout.Invalidate();
			}

			public void StopMe()
			{
				_stopped = true;
			}
		}
	}
}

