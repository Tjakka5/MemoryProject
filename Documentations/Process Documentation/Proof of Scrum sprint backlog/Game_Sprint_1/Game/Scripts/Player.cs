namespace Game.Scripts
{
	public class Player
	{
		public delegate void OnScoreUpdateHandler(Player player);
		public event OnScoreUpdateHandler OnScoreUpdate;

		public delegate void OnTurnChangedHandler(Player player);
		public event OnTurnChangedHandler OnTurnChanged;

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

		private bool hasTurn = false;

		public bool HasTurn
		{
			get { return hasTurn; }
			set
			{
				hasTurn = value;
				OnTurnChanged?.Invoke(this);
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
