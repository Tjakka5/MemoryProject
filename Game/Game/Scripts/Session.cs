using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Framework.Scheduling;
using Game.Controls;

namespace Game.Scripts
{
	public class Session
	{
		private Board board = null;
		private List<PlayerView> playerViews = null;

		private List<Player> players = new List<Player>();

		private List<Card> clickedCards = new List<Card>(2);

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

		private bool isChecking = false;

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
			board.CardClicked += OnCardClicked;
			
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

			clickedCards.Clear();
		}

		private void OnCardClicked(Card card)
		{
			// If we're already checking cards. Ignore this card
			if (isChecking)
				return;

			// If this card was already clicked. Ignore this card.
			if (clickedCards.Contains(card))
				return;

			// Accept the card input
			card.Show();
			clickedCards.Add(card);

			// Check if threshold is reached
			if (clickedCards.Count != 2)
				return;

			Scheduler.Schedule(CheckCardsRoutine());
		}

		private IEnumerator<YieldCommand> CheckCardsRoutine()
		{
			// Start checking
			isChecking = true;

			// Wait 1 second
			yield return new YieldForSeconds(1);

			// Check if all cards have same id
			int id = clickedCards[0].Id;
			if (!clickedCards.All(item => item.Id == id))
			{
				// Fail
				foreach (Card _card in clickedCards)
					_card.Hide();

				EndTurn();
			}
			else
			{
				// Success
				foreach (Card _card in clickedCards)
					_card.Remove();

				CurrentPlayer.Score += 1;
			}

			// Stop checking
			clickedCards.Clear();
			isChecking = false;
		}

		/// <summary>
		/// Ends the current player's turn
		/// </summary>
		private void EndTurn()
		{
			CurrentPlayerIndex++;

			// If at the last player. Go back to first one
			if (CurrentPlayerIndex > players.Count)
				currentPlayerIndex = 0;
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
			board.CardClicked -= OnCardClicked;

			CurrentPlayerIndex = 0;
		}
	}
}
