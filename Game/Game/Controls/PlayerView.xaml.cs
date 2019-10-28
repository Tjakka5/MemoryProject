using Game.Scripts;
using System.Windows.Controls;

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
			labelName.Content = player.Name;
			
			player.OnScoreUpdate += UpdateScoreVisual;
			UpdateScoreVisual(player);
		}

		public void Unbind()
		{
			if (player == null)
				return;

			labelName.Content = "";
			player.OnScoreUpdate -= UpdateScoreVisual;
		}

		public void UpdateScoreVisual(Player player)
		{
			string displayFormat = player.Score == 1 ? displayFormatSingular : displayFormatPlural;

			labelScore.Content = string.Format(displayFormat, player.Name, player.Score);
		}
	}
}
