﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Game.Controls;
using Game.Pages;
using Game.Scripts;

namespace Game
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : NavigationWindow
	{
		private PageMenu menu = null;
		private PageGame game = null;
		private PageSettings settings = null;
		private PageHighscores highscores = null;

        public MainWindow()
		{
			InitializeComponent();

			menu = new PageMenu(this);
			game = new PageGame(this);
			settings = new PageSettings(this);
			highscores = new PageHighscores(this);

			ShowsNavigationUI = false;
			
			NavigateToMenu();
			//NavigateToGame(new List<string>() { "Player_1", "Player_2" });
        }

		public void NavigateToMenu()
		{
			Navigate(menu);
		}

		public void NavigateToGame(List<string> playerNames)
		{
			Navigate(game);
			game.Setup(playerNames);
		}

		public void NavigateToSettings()
		{
			Navigate(settings);
		}

		public void NavigateToHighscores()
		{
			Navigate(highscores);
		}
    }
}