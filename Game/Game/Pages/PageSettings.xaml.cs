using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	/// Interaction logic for PageSettings.xaml
	/// </summary>
	public partial class PageSettings : Page
	{
		private MainWindow window;

		private List<TextBox> textBoxes = new List<TextBox>(4);
		public PageSettings(MainWindow window)
		{
			this.window = window;

			InitializeComponent();

			buttonPlay.IsEnabled = false;

			MakeTextBoxes(2);
		}

		private void OnButtonTwoPlayersClicked(object sender, RoutedEventArgs e)
		{
			MakeTextBoxes(2);
		}

		private void OnButtonThreePlayersClicked(object sender, RoutedEventArgs e)
		{
			MakeTextBoxes(3);
		}

		private void OnButtonFourPlayersClicked(object sender, RoutedEventArgs e)
		{
			MakeTextBoxes(4);
		}

		private void MakeTextBoxes(int newCount)
		{
			if (newCount > textBoxes.Count)
			{
				int toMake = newCount - textBoxes.Count;

				for (int i = 0; i < toMake; i++)
				{
					TextBox textBox = new TextBox
					{
						Width = 150,
						Margin = new Thickness(10, 0, 10, 0),
						MaxLength = 16,
					};
					textBox.TextChanged += OnTextChanged;

					textBoxes.Add(textBox);
					textInputs.Children.Add(textBox);
				}
			}
			else if (newCount < textBoxes.Count)
			{
				int toDelete = textBoxes.Count - newCount;
				for (int i = 0; i < toDelete; i++)
				{
					TextBox textBox = textBoxes[textBoxes.Count - 1];

					textBoxes.Remove(textBox);
					textInputs.Children.Remove(textBox);
				}
			}

			UpdateButtonPlayState();
		}

		private bool CheckValidStart()
		{
			foreach (TextBox textBox in textBoxes)
			{
				if (textBox.Text == string.Empty)
					return false;
			}

			return true;
		}

		private void UpdateButtonPlayState()
		{
			buttonPlay.IsEnabled = CheckValidStart();
		}

		private void OnTextChanged(object sender, RoutedEventArgs e)
		{
			UpdateButtonPlayState();
		}

		private void OnButtonPlayClicked(object sender, RoutedEventArgs e)
		{
			List<string> playerNames = new List<string>();
			foreach (TextBox textBox in textBoxes)
				playerNames.Add(textBox.Text);

			window.NavigateToGame(playerNames);
		}
	}
}
