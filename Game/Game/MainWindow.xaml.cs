using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Framework.Scheduling;
using Game.Scripts;

namespace Game
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			List<ImageSource> tempFrontImages = new List<ImageSource>();
			for (int i = 0; i < 100; i++)
				tempFrontImages.Add(new BitmapImage());

			ImageSource tempBackImage = new BitmapImage();

			Board board = new Board(Board.Layouts.FourByFour, tempFrontImages, tempBackImage);
			grid.Children.Add(board);

			Scheduler.Schedule(MyRoutine());
		}

		private IEnumerator<YieldCommand> MyRoutine()
		{
			Promise<int> texturePromise = new Promise<int>();
			Scheduler.Schedule(LoadTexture(texturePromise));

			yield return new YieldForPromiseFinalize(texturePromise);

			if (texturePromise.IsFulFilled())
			{
				int value = texturePromise.Value;
			}
			else
			{
				// Do something error-y
			}
		}

		private IEnumerator<YieldCommand> LoadTexture(Promise<int> promise)
		{
			yield return new YieldForSeconds(2);

			promise.Fulfill(10);
		}
	}
}
