using System;

namespace Game.Scripts
{
	public class Player
	{
		[Serializable]
		public class Data
		{
			public readonly int score = 0;
			public readonly bool hasTurn = false;
			public readonly string name = string.Empty;

			public Data(int score, bool hasTurn, string name)
			{
				this.score = score;
				this.hasTurn = hasTurn;
				this.name = name;
			}
		}

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

		public Player(Data data)
		{
			Name = data.name;
			Score = data.score;
			HasTurn = data.hasTurn;
		}

		public Data GetData()
		{
			return new Data(
				Score,
				HasTurn,
				Name
			);
		}
	}
}
