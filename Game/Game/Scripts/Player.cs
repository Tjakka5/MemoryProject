namespace Game.Scripts
{
	public class Player
	{
		public delegate void OnScoreUpdateHandler(Player player);
		public event OnScoreUpdateHandler OnScoreUpdate;

		private int score = 0;
		public int Score
		{
			get { return score; }
			set 
			{
				score = value;
				OnScoreUpdate?.Invoke(this);
			}
		}
		
		public string Name {
			get;
			private set;
		}

		public Player(string name, int initialScore = 0)
		{
			Name = name;
			Score = initialScore;
		}
	}
}
