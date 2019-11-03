using Game.Scripts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Game.Pages
{
	/// <summary>
	/// Interaction logic for PageMenu.xaml
	/// </summary>
	public partial class PageMenu : Page
	{
		private MainWindow window = null;

		public PageMenu(MainWindow window)
		{
			InitializeComponent();

			this.window = window;

			buttonPlay.Clicked += OnButtonPlayClicked;
			buttonHighscores.Clicked += OnButtonHighscoresClicked;
			buttonResume.Clicked += OnButtonResumeClicked;
		}

		private void OnButtonPlayClicked(object sender, RoutedEventArgs e)
		{
			window.NavigateToSettings();
		}

		private void OnButtonResumeClicked(object sender, RoutedEventArgs e)
		{
			Session.Data sessionData = null;
			if (SessionData.Load(out sessionData))
				window.NavigateToGame(sessionData);
		}

		private void OnButtonHighscoresClicked(object sender, RoutedEventArgs e)
		{
			window.NavigateToHighscores();
		}
	}
}
