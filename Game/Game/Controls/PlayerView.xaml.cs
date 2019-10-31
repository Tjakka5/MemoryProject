using Game.Scripts;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game.Controls
{
	/// <summary>
	/// Interaction logic for PlayerView.xaml
	/// </summary>
	public partial class PlayerView : UserControl
	{
		private readonly string displayFormatSingular = "{0} heeft {1} punt";
		private readonly string displayFormatPlural = "{0} heeft {1} punten";
		
		private Player player = null;

		public PlayerView()
		{
			InitializeComponent();
		}

		~PlayerView()
		{
			Unbind();
		}

		public void Bind(Player player)
		{
			this.player = player;

			labelName.Content = player.Name;
			
			player.OnScoreUpdate += UpdateScoreVisual;
			UpdateScoreVisual(player);

			player.OnTurnChanged += UpdateTurnVisual;
			UpdateTurnVisual(player);
		}

		public void Unbind()
		{
			if (player == null)
				return;
			
			labelName.Content = string.Empty;
			labelScore.Content = string.Empty;

			player.OnScoreUpdate -= UpdateScoreVisual;
			player.OnTurnChanged -= UpdateTurnVisual;
		}

		private void UpdateScoreVisual(Player player)
		{
			string displayFormat = player.Score == 1 ? displayFormatSingular : displayFormatPlural;

			labelScore.Content = string.Format(displayFormat, player.Name, player.Score);
		}

		private void UpdateTurnVisual(Player player)
		{
			if (player.HasTurn)
				Background = Brushes.HotPink;
			else
				Background = Brushes.AliceBlue;
		}
	}
}
