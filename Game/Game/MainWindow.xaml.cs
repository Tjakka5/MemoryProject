using System.Collections.Generic;
using System.Windows;
using Game.Controls;
using Game.Scripts;

namespace Game
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Session currentSession;
		
        public MainWindow()
		{
			InitializeComponent();
		
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

        ~MainWindow()
        {
            buttonRestart.Click -= ButtonRestartClicked;
        }

        public void ButtonRestartClicked(object sender, RoutedEventArgs e)
        {
            currentSession.Restart();
        }
    }
}