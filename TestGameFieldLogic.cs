using System;
using Android.Graphics;

namespace TestAndroidGame
{
	public class TestGameFieldLogic : IGameFieldLogic
	{
		private float _offset = 0;
		Paint rectPaint;


		public TestGameFieldLogic()
		{
			rectPaint = new Paint();
			rectPaint.Color = Color.LawnGreen;
		}

		public void DoDraw(Android.Graphics.Canvas canvas)
		{
			canvas.DrawRect(0f,_offset,100,_offset+100,rectPaint);
		}

		public void DoLogic(float secFromLastTick)
		{
			float pixelsPerSec = 100;

			_offset+= (float)(pixelsPerSec * secFromLastTick);
			if(_offset > 100)
			{
				_offset = 0;
			}
		}
	}
}

