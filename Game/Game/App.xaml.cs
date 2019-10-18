using Game.Scripts.Scheduler;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace Game
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private DispatcherTimer timer = null;
		private Stopwatch stopwatch = null;

		public App()
		{
			timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromTicks(1)
			};
			timer.Tick += Tick;
			timer.Start();

			stopwatch = new Stopwatch();
			//stopwatch.Start();
		}

		~App()
		{
			timer.Tick -= Tick;
		}

		private void Tick(object sender, EventArgs e)
		{
			Scheduler.Update(stopwatch.ElapsedMilliseconds / 1000.0f);
			stopwatch.Restart();
		}
	}
}
