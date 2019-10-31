﻿using Game.Controls;
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

        private ModalContentPause modalContentPause = null;

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

            modalContentPause = new ModalContentPause();
            modalContentPause.ButtonRestartClicked += Restart;
			modalContentPause.ButtonResumeClicked += UnPause;
			modalContentPause.ButtonMenuClicked += GoToMenu;

			buttonRestart.Click += OnRestartButtonClicked;
			buttonPause.Click += OnPauseButtonClicked;
		}

		~PageGame()
		{
			buttonRestart.Click -= OnRestartButtonClicked;
			buttonPause.Click += OnPauseButtonClicked;
		}

		/// <summary>
		/// Starts a game session
		/// </summary>
		/// <param name="playerNames">List of player names</param>
		public void Setup(List<string> playerNames)
		{
			currentSession = new Session(board, playerViews, playerNames, Board.Layouts.FourByFour);
		}

        private void Restart()
        {
			// Temp
			modal.Hide();

            currentSession.Restart();
        }

		private void UnPause()
		{
			modal.Hide();	
		}

		private void GoToMenu()
		{
			UnPause();

			currentSession.Stop();
			window.NavigateToMenu();
		}


		private void Pause()
        {
            modal.Show(modalContentPause);
        }

        private void OnRestartButtonClicked(object sender, RoutedEventArgs e)
        {
            currentSession.Restart();
        }

        private void OnPauseButtonClicked(object sender, RoutedEventArgs e)
        {
            Pause();
        }
    }
}
