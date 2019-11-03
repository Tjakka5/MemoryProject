using System.Windows.Controls;
using System.Windows.Input;

namespace Game.Pages
{
	/// <summary>
	/// Interaction logic for PageHighscores.xaml
	/// </summary>
	public partial class PageHighscores : Page
	{
		private MainWindow window = null;

		public PageHighscores(MainWindow window)
		{
			InitializeComponent();

			this.window = window;

			buttonBack.Clicked += OnButtonBackClicked;
		}

		private void OnButtonBackClicked(object sender, MouseEventArgs e)
		{
			window.NavigateToMenu();
		}
	}
}
