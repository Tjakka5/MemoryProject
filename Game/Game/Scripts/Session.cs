using System;
using System.Collections.Generic;
using System.Linq;
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
			get { return players[currentPlayerIndex]; }
		}

		private int currentPlayerIndex = 0;

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
			// Make temporary images
			List<ImageSource> tempFrontImages = new List<ImageSource>();
			for (int i = 0; i < 100; i++)
				tempFrontImages.Add(new BitmapImage(new Uri("Resources/Images/tempFrontImage.png", UriKind.Relative)));

			ImageSource tempBackImage = new BitmapImage(new Uri("Resources/Images/tempBackImage.png", UriKind.Relative));

			// Make board
			board.Setup(initialLayout, tempFrontImages, tempBackImage);
			board.CardClicked += OnCardClicked;
			
			// Make players and bind them to views
			players.Clear();
			
			for (int i = 0; i < initialPlayerNames.Count; i++)
			{
				Player player = new Player(initialPlayerNames[i]);
				playerViews[i].Bind(player);

				players.Add(player);
			}

			clickedCards.Clear();
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
			currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
		}

		public void Restart()
		{
			Stop();
			Start();
		}

		public void Stop()
		{
			board.Clear();
			board.CardClicked -= OnCardClicked;

			currentPlayerIndex = 0;
		}
	}
}
