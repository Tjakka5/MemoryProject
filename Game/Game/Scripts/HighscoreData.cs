using System;

namespace Game.Scripts
{
	[Serializable]
	public class HighscoreData
	{
		public readonly string name = string.Empty;
		public readonly int score = 0;

		public HighscoreData(string name, int score)
		{
			this.name = name;
			this.score = score;
		}
	}
}
