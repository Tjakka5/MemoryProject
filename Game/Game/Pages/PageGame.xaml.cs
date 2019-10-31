using Game.Controls;
using Game.Controls.ModalContent;
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

		private List<PlayerView> playerViews = null;
		private Session currentSession = null;

		public PageGame(MainWindow window)
		{
			InitializeComponent();

			this.window = window;

			playerViews = new List<PlayerView>()
			{
				playerView_1,
				playerView_2,
				playerView_3,
				playerView_4,
			};
			
			buttonRestart.Click += ButtonRestartClicked;
			buttonPause.Click += ButtonPauseClicked;
		}

		~PageGame()
		{
			buttonRestart.Click -= ButtonRestartClicked;
			buttonPause.Click += ButtonPauseClicked;
		}

		/// <summary>
		/// Starts a game session
		/// </summary>
		/// <param name="playerNames">List of player names</param>
		public void Setup(List<string> playerNames)
		{
			currentSession = new Session(board, playerViews, playerNames, Board.Layouts.FourByFour);
		}

		/// <summary>
		/// On restart button clicked
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Events</param>
		public void ButtonRestartClicked(object sender, RoutedEventArgs e)
		{
			currentSession.Restart();
		}

		public void ButtonPauseClicked(object sender, RoutedEventArgs e)
		{
			UserControl modalContentPause = new ModalContentPause();
			modal.Show(modalContentPause);
		}
	}
}
