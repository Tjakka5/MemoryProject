using System.Collections.Generic;
using System.Windows.Controls;

namespace Game.Scripts
{
	public class Scoreboard : ItemsControl
	{
		private readonly string displayFormat = "{0} heeft {1} punten";

		private Dictionary<Player, Label> labelLookup = new Dictionary<Player, Label>();
		private List<Player> players = null;

		public Scoreboard(List<Player> players)
		{
			this.players = players;

			foreach (Player player in players)
			{
				Label label = new Label();
				label.Content = string.Format(displayFormat, player.Name, player.Score);

				labelLookup[player] = label;

				AddChild(label);

				player.OnScoreUpdate += UpdateScoreVisual;
            }
        }

		~Scoreboard()
		{
			foreach (Player player in players)
			{
				player.OnScoreUpdate -= UpdateScoreVisual;
			}
		}

		private void UpdateScoreVisual(Player player)
		{
			Label label = labelLookup[player];
			label.Content = string.Format(displayFormat, player.Name, player.Score);
		}
	}
}
