using Game.Scripts;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Game.Controls.ModalContent
{
	/// <summary>
	/// Interaction logic for ModalContentEndResult.xaml
	/// </summary>
	public partial class ModalContentEndResult : UserControl
	{
		private readonly string displayFormatSingular = "{0} heeft {1} punt";
		private readonly string displayFormatPlural = "{0} heeft {1} punten";

		public delegate void ButtonPlayAgainClickedHandler();
		public event ButtonPlayAgainClickedHandler ButtonPlayAgainClicked;

		public delegate void ButtonMenuClickedHandler();
		public event ButtonMenuClickedHandler ButtonMenuClicked;

		private List<Label> labelPlayerResults = null;

		public ModalContentEndResult()
		{
			InitializeComponent();

			labelPlayerResults = new List<Label>()
			{
				labelPlayerOneResult,
				labelPlayerTwoResult,
				labelPlayerThreeResult,
				labelPlayerFourResult,
			};

			buttonPlayAgain.Clicked += OnButtonPlayAgainClicked;
			buttonMenu.Clicked += OnButtonMenuClicked;
		}

		/// <summary>
		/// Setup the object
		/// </summary>
		/// <param name="players">List of players</param>
		public void Setup(List<Player> players)
		{
			// Sort players by score
			players = players.OrderByDescending(player => player.Score).ToList();
			
			// Loop over all labels
			for (int i = 0; i < labelPlayerResults.Count; i++)
			{
				Label label = labelPlayerResults[i];

				if (i < players.Count) // If there's a player for this label...
				{
					Player player = players[i];
					
					string displayFormat = player.Score == 1 ? displayFormatSingular : displayFormatPlural;

					// Show the label with content
					label.Visibility = Visibility.Visible;
					label.Content = string.Format(displayFormat, player.Name, player.Score);
				}
				else 
					// Else. Hide the label
					label.Visibility = Visibility.Hidden;
			}
		}

		/// <summary>
		/// On play button clicked
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event</param>
		private void OnButtonPlayAgainClicked(object sender, RoutedEventArgs e)
		{
			ButtonPlayAgainClicked?.Invoke();
		}

		/// <summary>
		/// On menu button clicked
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event</param>
		private void OnButtonMenuClicked(object sender, RoutedEventArgs e)
		{
			ButtonMenuClicked?.Invoke();
		}
	}
}
