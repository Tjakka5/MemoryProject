using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Framework.Scheduling;

namespace Game.Scripts
{
	public class Session
	{
		private Grid grid = null;
		private Label labelCurrentPlayer = null;

		private Board board = null;
		private Scoreboard scoreboard = null;

		private List<Player> players = new List<Player>();

		private List<Card> clickedCards = new List<Card>(2);

		private Player CurrentPlayer
		{
			get { return players[CurrentPlayerIndex]; }
		}

		private int currentPlayerIndex = 0;
		private int CurrentPlayerIndex {
			get { return currentPlayerIndex; }
			set {
				currentPlayerIndex = value;
				labelCurrentPlayer.Content = CurrentPlayer.Name;
			}
		}

		private List<string> initialPlayerNames = null;
        private Board.Layouts initialLayout = default;

		public Session(Grid grid, Label labelCurrentPlayer, List<string> playerNames, Board.Layouts layout)
		{
			this.grid = grid;
			this.labelCurrentPlayer = labelCurrentPlayer;

			initialPlayerNames = playerNames;
			initialLayout = layout;

			Start();
		}

		private void Start()
		{
			// Make temporary images
			List<ImageSource> tempFrontImages = new List<ImageSource>();
			for (int i = 0; i < 100; i++)
				tempFrontImages.Add(new BitmapImage(new Uri("Resources/Images/tempFrontImage.png", UriKind.Relative)));

			ImageSource tempBackImage = new BitmapImage(new Uri("Resources/Images/tempBackImage.png", UriKind.Relative));

			// Make board
			board = new Board(initialLayout, tempFrontImages, tempBackImage);
			board.CardClicked += OnCardClicked;
			
			grid.Children.Add(board);

			// Make players
			players.Clear();
			foreach (string playerName in initialPlayerNames)
				players.Add(new Player(playerName));

			clickedCards.Clear();

			scoreboard = new Scoreboard(players);
			grid.Children.Add(scoreboard);

			CurrentPlayerIndex = 0;
		}

		private void OnCardClicked(Card card)
		{
			card.Show();
			clickedCards.Add(card);

			// Check if threshold is reached
			if (clickedCards.Count != 2)
				return;

			Scheduler.Schedule(CheckCardsRoutine());
		}
		
		private IEnumerator<YieldCommand> CheckCardsRoutine()
		{
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

			clickedCards.Clear();
		}

		private void EndTurn()
		{
			CurrentPlayerIndex = (CurrentPlayerIndex + 1) % players.Count;
		}

		public void Restart()
		{
			Stop();
			Start();
		}

		public void Stop()
		{
			CurrentPlayerIndex = 0;

			board.CardClicked -= OnCardClicked;

			grid.Children.Remove(board);
			grid.Children.Remove(scoreboard);
		}
	}
}
