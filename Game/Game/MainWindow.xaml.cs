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

			ShowsNavigationUI = false;
			
			NavigateToMenu();
			//NavigateToGame(new List<string>() { "Player_1", "Player_2" });
        }

		protected override void OnClosed(EventArgs args)
		{
			if (currentPage.GetType() == typeof(PageGame))
				(currentPage as PageGame).Save();

			base.OnClosed(args);
		}

		public void NavigateToMenu()
		{
			PageMenu menu = new PageMenu(this);

			currentPage = menu;
			Navigate(menu);
		}

		public void NavigateToGame(List<string> playerNames, ImagePool.FrontTypes frontType, ImagePool.BackTypes backType, Board.Layouts layout)
		{
			PageGame game = new PageGame(this);

			currentPage = game;
			Navigate(game);
			game.Setup(playerNames, frontType, backType, layout);
		}

		public void NavigateToGame(Session.Data sessionData)
		{
			PageGame game = new PageGame(this);

			currentPage = game;
			Navigate(game);
			game.Load(sessionData);
		}

		public void NavigateToSettings()
		{
			PageSettings settings = new PageSettings(this);

			currentPage = settings;
			Navigate(settings);
		}

		public void NavigateToHighscores()
		{
			PageHighscores highscores = new PageHighscores(this);

			currentPage = highscores;
			Navigate(highscores);
		}
    }
}