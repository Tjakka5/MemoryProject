using System;
using System.Collections.Generic;
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
		private Page currentPage = null;

        public MainWindow()
		{
			InitializeComponent();

			// Turn off UI for navigation ( Page back, Page forward )
			ShowsNavigationUI = false;
			
			// Start in the menu screen
			NavigateToMenu();
        }

		/// <summary>
		/// Called on window close
		/// </summary>
		/// <param name="args">EventArgs</param>
		protected override void OnClosed(EventArgs args)
		{
			// Hacky code
			// If the current page is a game page. Tell it to save
			if (currentPage.GetType() == typeof(PageGame))
				(currentPage as PageGame).Save();

			base.OnClosed(args);
		}

		/// <summary>
		/// Navigates to the menu
		/// </summary>
		public void NavigateToMenu()
		{
			PageMenu menu = new PageMenu(this);

			currentPage = menu;
			Navigate(menu);
		}

		/// <summary>
		/// Navigate to the game
		/// </summary>
		/// <param name="playerNames">List of all player names</param>
		/// <param name="frontType">Fronttype for the cards</param>
		/// <param name="backType">Backtype for the cards</param>
		/// <param name="layout">Layout for the board</param>
		public void NavigateToGame(List<string> playerNames, ImagePool.FrontTypes frontType, ImagePool.BackTypes backType, Board.Layouts layout)
		{
			PageGame game = new PageGame(this);

			currentPage = game;
			Navigate(game);
			game.Setup(playerNames, frontType, backType, layout);
		}

		/// <summary>
		/// Navigates to game
		/// </summary>
		/// <param name="sessionData">Savedata of a session to load</param>
		public void NavigateToGame(Session.Data sessionData)
		{
			PageGame game = new PageGame(this);

			currentPage = game;
			Navigate(game);
			game.Load(sessionData);
		}

		/// <summary>
		/// Navigates to settings
		/// </summary>
		public void NavigateToSettings()
		{
			PageSettings settings = new PageSettings(this);

			currentPage = settings;
			Navigate(settings);
		}

		/// <summary>
		/// Navigates to highscores
		/// </summary>
		public void NavigateToHighscores()
		{
			PageHighscores highscores = new PageHighscores(this);

			currentPage = highscores;
			Navigate(highscores);
		}
    }
}