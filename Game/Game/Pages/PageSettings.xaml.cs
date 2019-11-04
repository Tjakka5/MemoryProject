using Game.Controls;
using Game.Scripts;
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
	
		private List<CustomTextbox> textBoxes = new List<CustomTextbox>(4);

		private CardSelect selectedForeCard = null;
		private CardSelect selectedBackCard = null;

		private ImageButton selectedGridSizeButton = null;

		public PageSettings(MainWindow window)
		{
			this.window = window;

			InitializeComponent();

			buttonTwoPlayers.MouseDown += OnButtonTwoPlayersClicked;
			buttonThreePlayers.MouseDown += OnButtonThreePlayersClicked;
			buttonFourPlayers.MouseDown += OnButtonFourPlayersClicked;

			cardForePokemon.Clicked += OnCardSelectForeClicked;
			cardForeAnimals.Clicked += OnCardSelectForeClicked;
			cardForeMario.Clicked += OnCardSelectForeClicked;

			cardBackPokemon.Clicked += OnCardSelectBackClicked;
			cardBackVintage.Clicked += OnCardSelectBackClicked;
			cardBackMario.Clicked += OnCardSelectBackClicked;

			buttonFourByFour.Clicked += OnButtonGridSizeClicked;
			buttonFiveByFive.Clicked += OnButtonGridSizeClicked;
			buttonSixByFour.Clicked += OnButtonGridSizeClicked;
			buttonSixBySix.Clicked += OnButtonGridSizeClicked;

			buttonBack.Clicked += OnBackButtonClicked;

			buttonPlay.Clicked += OnButtonPlayClicked;
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

		/// <summary>
		/// Makes or creates the desired amount of textboxes
		/// </summary>
		/// <param name="newCount">Amount of target textboxes</param>
		private void MakeTextBoxes(int newCount)
		{
			if (newCount > textBoxes.Count)
			{
				// Make new textboxes
				int toMake = newCount - textBoxes.Count;

				for (int i = 0; i < toMake; i++)
				{
					CustomTextbox textBox = new CustomTextbox();
					textBox.textbox.TextChanged += OnTextChanged;

					textBoxes.Add(textBox);
					textInputs.Children.Add(textBox);
				}
			}
			else if (newCount < textBoxes.Count)
			{
				// Delete textboxes
				int toDelete = textBoxes.Count - newCount;
				for (int i = 0; i < toDelete; i++)
				{
					CustomTextbox textBox = textBoxes[textBoxes.Count - 1];

					textBoxes.Remove(textBox);
					textInputs.Children.Remove(textBox);
				}
			}
			
			UpdateButtonPlayState();
		}

		/// <summary>
		/// Check if all inputs are properly set
		/// </summary>
		/// <returns></returns>
		private bool CheckValidStart()
		{
			// Check if each textbox is filled in
			foreach (CustomTextbox textBox in textBoxes)
			{
				if (string.IsNullOrWhiteSpace(textBox.textbox.Text))
					return false;
			}

			// Check if a foreground card was picked
			if (selectedForeCard == null)
				return false;

			// Check if a background card was picked
			if (selectedBackCard == null)
				return false;

			// Check if a board size was picked
			if (selectedGridSizeButton == null)
				return false;

			// Return true when it got through all the checks
			return true;
		}

		/// <summary>
		/// Updates play button state
		/// </summary>
		private void UpdateButtonPlayState()
		{
			buttonPlay.IsEnabled = CheckValidStart();
		}

		/// <summary>
		/// On textbutton text changed
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Events</param>
		private void OnTextChanged(object sender, RoutedEventArgs e)
		{
			UpdateButtonPlayState();
		}

		private void OnCardSelectForeClicked(object sender, MouseEventArgs e)
		{
			CardSelect cardSelect = sender as CardSelect;

			if (selectedForeCard != null)
				selectedForeCard.Deselect();

			selectedForeCard = cardSelect;
			cardSelect.Select();

			UpdateButtonPlayState();
		}
		

		private void OnCardSelectBackClicked(object sender, MouseEventArgs e)
		{
			CardSelect cardSelect = sender as CardSelect;

			if (selectedBackCard != null)
				selectedBackCard.Deselect();

			selectedBackCard = cardSelect;
			cardSelect.Select();

			UpdateButtonPlayState();
		}
		
		private void OnButtonGridSizeClicked(object sender, MouseEventArgs e)
		{
			ImageButton imageButton = sender as ImageButton;

			if (selectedGridSizeButton != null)
				selectedGridSizeButton.Deselect();

			selectedGridSizeButton = imageButton;
			imageButton.Select();

			UpdateButtonPlayState();
		}
		
		private void OnBackButtonClicked(object sender, MouseEventArgs e)
		{
			window.NavigateToMenu();
		}

		/// <summary>
		/// On play button clicked
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Events</param>
		private void OnButtonPlayClicked(object sender, MouseEventArgs e)
		{
			// Create lists of players
			List<string> playerNames = new List<string>();
			foreach (CustomTextbox textBox in textBoxes)
				playerNames.Add(textBox.textbox.Text);

			// Create front type
			ImagePool.FrontTypes frontType = default;

			if (selectedForeCard == cardForePokemon)
				frontType = ImagePool.FrontTypes.POKEMON;
			else if (selectedForeCard == cardForeAnimals)
				frontType = ImagePool.FrontTypes.ANIMALS;
			else if (selectedForeCard == cardForeMario)
				frontType = ImagePool.FrontTypes.MARIO;

			// Create back type
			ImagePool.BackTypes backType = default;

			if (selectedBackCard == cardBackPokemon)
				backType = ImagePool.BackTypes.POKEMON;
			else if (selectedBackCard == cardBackVintage)
				backType = ImagePool.BackTypes.VINTAGE;
			else if (selectedBackCard == cardBackMario)
				backType = ImagePool.BackTypes.MARIO;

			// Create boar layout
			Board.Layouts layout = default;

			if (selectedGridSizeButton == buttonFourByFour)
				layout = Board.Layouts.FourByFour;
			else if (selectedGridSizeButton == buttonFiveByFive)
				layout = Board.Layouts.FiveByFive;
			else if (selectedGridSizeButton == buttonSixByFour)
				layout = Board.Layouts.SixByFour;
			else if (selectedGridSizeButton == buttonSixBySix)
				layout = Board.Layouts.SixBySix;

			// Navigate
			window.NavigateToGame(playerNames, frontType, backType, layout);
		}
	}
}
