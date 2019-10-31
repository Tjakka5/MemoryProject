using Game.Scripts;
using System;
using System.Collections.Generic;
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

namespace Game.Controls.ModalContent
{
	/// <summary>
	/// Interaction logic for ModalContentEndResult.xaml
	/// </summary>
	public partial class ModalContentEndResult : UserControl
	{
		private readonly string displayFormatSingular = "{0} heeft {1} punt";
		private readonly string displayFormatPlural = "{0} heeft {1} punten";

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
		}

		public void Setup(List<Player> players)
		{
			// Sort players by score
			players = players.OrderByDescending(player => player.Score).ToList();
			
			for (int i = 0; i < labelPlayerResults.Count; i++)
			{
				Label label = labelPlayerResults[i];

				if (i < players.Count)
				{
					Player player = players[i];
					
					string displayFormat = player.Score == 1 ? displayFormatSingular : displayFormatPlural;

					label.Visibility = Visibility.Visible;
					label.Content = string.Format(displayFormat, player.Name, player.Score);
				}
				else
					label.Visibility = Visibility.Collapsed;
			}
		}
	}
}
