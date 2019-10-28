using System.Windows;
using System.Windows.Controls;

namespace Game.Pages
{
	/// <summary>
	/// Interaction logic for PageMenu.xaml
	/// </summary>
	public partial class PageMenu : Page
	{
		MainWindow window = null;

		public PageMenu(MainWindow window)
		{
			InitializeComponent();

			this.window = window;
		}

		private void OnButtonPlayClicked(object sender, RoutedEventArgs e)
		{
			window.Switch(MainWindow.Pages.GAME);
		}
	}
}
