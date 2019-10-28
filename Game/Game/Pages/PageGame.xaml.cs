using Game.Controls;
using Game.Scripts;
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

namespace Game.Pages
{
	/// <summary>
	/// Interaction logic for Game.xaml
	/// </summary>
	public partial class PageGame : Page
	{
		private MainWindow window = null;

		private Session currentSession = null;

		public PageGame(MainWindow window)
		{
			InitializeComponent();

			this.window = window;

			List<PlayerView> playerViews = new List<PlayerView>()
			{
				playerView_1,
				playerView_2,
				playerView_3,
				playerView_4,
			};

			List<string> players = new List<string>() {
				"Auke", "Maurice", "Hannah", "Justin",
			};

			currentSession = new Session(board, playerViews, players, Board.Layouts.FourByFour);

			buttonRestart.Click += ButtonRestartClicked;
		}

		~PageGame()
		{
			buttonRestart.Click -= ButtonRestartClicked;
		}

		public void ButtonRestartClicked(object sender, RoutedEventArgs e)
		{
			currentSession.Restart();
		}
	}
}
