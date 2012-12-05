using System;
using Android.Graphics;

namespace TestAndroidGame
{
	public interface IGameFieldLogic
	{
		void DoDraw(Canvas c);
		void DoLogic(float secFromLastTick);
	}
}

