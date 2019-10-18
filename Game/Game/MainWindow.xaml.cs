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
using Game.Scripts;
using Game.Scripts.Scheduler;
using Game.Scripts.Scheduler.YieldCommands;

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
			int a = 2;
			yield return new YieldForSeconds(3);
			a = a + 5;
			yield return new YieldForSeconds(5);
			a = 10;
		}
	}
}
