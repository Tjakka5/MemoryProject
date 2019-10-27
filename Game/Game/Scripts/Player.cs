using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Scheduling;

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
				OnScoreUpdate?.Invoke(this);
				score = value;
			}
		}

		public string Name {
			get;
			private set;
		}

		public Player(string name)
		{
			Name = name;
		}
	}
}
