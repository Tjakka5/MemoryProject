using System.Collections.Generic;
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
		public enum Pages
		{
			MENU,
			GAME,
		}

		private Page menu = null;
		private Page game = null;

        public MainWindow()
		{
			InitializeComponent();

			menu = new PageMenu(this);
			game = new PageGame(this);

			ShowsNavigationUI = false;

			Switch(Pages.MENU);
        }

		public void Switch(Pages page)
		{
			switch (page)
			{
				case Pages.MENU:
					Navigate(menu);
					break;
				case Pages.GAME:
					Navigate(game);
					break;
			}
		}
    }
}