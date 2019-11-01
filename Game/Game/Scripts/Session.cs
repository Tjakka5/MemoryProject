using System.Collections.Generic;
using System.Windows;
using Game.Controls;

namespace Game.Scripts
{
	public class Session
	{
		public delegate void GameFinishedHandler(List<Player> players);
		public event GameFinishedHandler GameFinished;

		private Board board = null;
		private List<PlayerView> playerViews = null;

		private List<Player> players = new List<Player>();

		private Player CurrentPlayer
		{
			get { return players[CurrentPlayerIndex]; }
		}

		private int currentPlayerIndex = 0;
		private int CurrentPlayerIndex {
			get { return currentPlayerIndex; }
			set
			{
				CurrentPlayer.HasTurn = false;
	
				currentPlayerIndex = value;
			
				CurrentPlayer.HasTurn = true;
			}
		}

		private List<string> initialPlayerNames = null;
        private Board.Layouts initialLayout = default;

		public Session(Board board, List<PlayerView> playerViews, List<string> playerNames, Board.Layouts layout)
		{
			this.board = board;
			this.playerViews = playerViews;

			initialPlayerNames = playerNames;
			initialLayout = layout;

			Start();
		}

		private void Start()
		{
			// Temp
			ImagePool.FrontTypes frontType = ImagePool.FrontTypes.POKEMON;
			ImagePool.BackTypes backType = ImagePool.BackTypes.POKEMON;

			// Make board
			board.Setup(initialLayout, frontType, backType);
			board.MatchMade += OnMatchMade;
			board.GameFinished += OnGameFinished;
			
			// Make players
			players.Clear();
			
			for (int i = 0; i < initialPlayerNames.Count; i++)
			{
				Player player = new Player(initialPlayerNames[i]);
				players.Add(player);
			}

			// Bind players to views
			for (int i = 0; i < playerViews.Count; i++)
			{
				PlayerView playerView = playerViews[i];

				// Only make views with a player visible
				if (i < players.Count)
				{
					playerView.Bind(players[i]);
					playerView.Visibility = Visibility.Visible;
				}
				else 
					playerView.Visibility = Visibility.Hidden;
			}

			CurrentPlayerIndex = 0;
		}

		private void OnMatchMade(bool success)
		{
			if (success)
				CurrentPlayer.Score += 1;
			else
				EndTurn();
		}

		private void OnGameFinished()
		{
			GameFinished?.Invoke(players);
		}

		/// <summary>
		/// Ends the current player's turn
		/// </summary>
		private void EndTurn()
		{
			CurrentPlayerIndex = (CurrentPlayerIndex + 1) % players.Count;
		}

		/// <summary>
		/// Restarts the session
		/// </summary>
		public void Restart()
		{
			Stop();
			Start();
		}

		/// <summary>
		/// Stops the session
		/// </summary>
		public void Stop()
		{
			board.Clear();
			board.MatchMade -= OnMatchMade;
			board.GameFinished -= OnGameFinished;
		
			CurrentPlayerIndex = 0;
		}
	}
}
