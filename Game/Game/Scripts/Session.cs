using System;
using System.Collections.Generic;
using System.Windows;
using Game.Controls;

namespace Game.Scripts
{
	public class Session
	{
		[Serializable]
		public class Data
		{
			public readonly List<string> initialPlayerNames = null;
			public readonly Board.Layouts initialLayout = default;
			public readonly int currentPlayerIndex = 0;
			public readonly Board.Data boardData = null;
			public readonly List<Player.Data> playerDatas = null;

			public Data(List<string> initialPlayerNames, Board.Layouts initialLayout, int currentPlayerIndex, Board.Data boardData, List<Player.Data> playerDatas)
			{
				this.initialPlayerNames = initialPlayerNames;
				this.initialLayout = initialLayout;
				this.currentPlayerIndex = currentPlayerIndex;
				this.boardData = boardData;
				this.playerDatas = playerDatas;
			}
		}

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

		public bool Done
		{
			get;
			private set;
		} = false;

		private List<string> initialPlayerNames = null;
		private ImagePool.FrontTypes initialFrontType = default;
		private ImagePool.BackTypes initialBackType = default;
		private Board.Layouts initialLayout = default;

		public Session(Board board, List<PlayerView> playerViews)
		{
			this.board = board;
			this.playerViews = playerViews;
		}

		public void Setup(List<string> playerNames, ImagePool.FrontTypes frontType, ImagePool.BackTypes backType, Board.Layouts layout)
		{
			Done = false;

			initialPlayerNames = playerNames;
			initialFrontType = frontType;
			initialBackType = backType;
			initialLayout = layout;

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

			BindPlayersToViews();
			
			CurrentPlayerIndex = 0;
		}

		public void Load(Data data)
		{
			Done = false;

			initialPlayerNames = data.initialPlayerNames;
			initialLayout = data.initialLayout;

			board.Load(data.boardData);
			board.MatchMade += OnMatchMade;
			board.GameFinished += OnGameFinished;

			foreach (Player.Data playerData in data.playerDatas)
			{
				Player player = new Player(playerData);
				players.Add(player);
			}

			BindPlayersToViews();

			CurrentPlayerIndex = data.currentPlayerIndex;
		}

		public Data GetData()
		{
			List<Player.Data> playerDatas = new List<Player.Data>();
			foreach (Player player in players)
				playerDatas.Add(player.GetData());

			return new Data(
				initialPlayerNames,
				initialLayout,
				currentPlayerIndex,
				board.GetData(),
				playerDatas
			);
		}

		private void BindPlayersToViews()
		{
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
			Done = true;

			foreach (Player player in players)
				HighscoreData.Add(new HighscoreData.Data(player.Name, player.Score, initialLayout));

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
			Setup(initialPlayerNames, initialFrontType, initialBackType, initialLayout);
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
