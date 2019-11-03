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

		private bool hasSessionData = false;
		private Session.Data sessionData = null;

		public PageMenu(MainWindow window)
		{
			InitializeComponent();

			this.window = window;

			buttonPlay.Clicked += OnButtonPlayClicked;
			buttonHighscores.Clicked += OnButtonHighscoresClicked;
			buttonResume.Clicked += OnButtonResumeClicked;

			buttonResume.IsEnabled = SessionData.Load(out sessionData);
		}

		private void OnButtonPlayClicked(object sender, RoutedEventArgs e)
		{
			window.NavigateToSettings();
		}

		private void OnButtonResumeClicked(object sender, RoutedEventArgs e)
		{
			window.NavigateToGame(sessionData);
		}

		private void OnButtonHighscoresClicked(object sender, RoutedEventArgs e)
		{
			window.NavigateToHighscores();
		}
	}
}
