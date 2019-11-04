using Framework;
using Framework.Scheduling;
using Game.Scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game.Controls
{
	/// <summary>
	/// Interaction logic for Board.xaml
	/// </summary>
	public partial class Board : UserControl
	{
		[Serializable]
		public class Data
		{
			public readonly List<Card.Data> cards = null;
			public readonly int cardsLeftCount = 0;
			public readonly Layouts layout = default;

			public Data(List<Card.Data> cards, int cardsLeftCount, Layouts layout)
			{
				this.cards = cards;
				this.cardsLeftCount = cardsLeftCount;
				this.layout = layout;
			}
		}

		public delegate void MatchMadeHandler(bool success);
		public event MatchMadeHandler MatchMade;

		public delegate void GameFinishedHandler();
		public event GameFinishedHandler GameFinished;

		public enum Layouts
		{
			FourByFour,
			FiveByFive,
			SixByFour,
			SixBySix,
		};

		public bool IsChecking
		{
			get;
			private set;
		}

		private List<Card> cards = new List<Card>();

		private List<Card> clickedCards = new List<Card>(2);
		private int cardsLeftCount = 0;

		private Layouts layout = default;

		public Board()
		{
			InitializeComponent();
		}

		public void Setup(Layouts layout, ImagePool.FrontTypes frontTypes, ImagePool.BackTypes backTypes)
		{
			cards.Clear();
			clickedCards.Clear();
			cardsLeftCount = 0;

			this.layout = layout;

			switch (layout)
			{
				case Layouts.FourByFour:
					MakeFourByFour(frontTypes, backTypes);
					break;
				case Layouts.FiveByFive:
					MakeFiveByFive(frontTypes, backTypes);
					break;
				case Layouts.SixByFour:
					MakeSixByFour(frontTypes, backTypes);
					break;
				case Layouts.SixBySix:
					MakeSixBySix(frontTypes, backTypes);
					break;
			}
		}

		public void Load(Data data)
		{
			cardsLeftCount = data.cardsLeftCount;
			layout = data.layout;

			switch (layout)
			{
				case Layouts.FourByFour:
					LoadFourByFour(data.cards);
					break;
				case Layouts.FiveByFive:
					LoadFiveByFive(data.cards);
					break;
				case Layouts.SixByFour:
					LoadSixByFour(data.cards);
					break;
				case Layouts.SixBySix:
					LoadSixBySix(data.cards);
					break;
			}
		}

		public Data GetData()
		{
			List<Card.Data> cardsData = new List<Card.Data>();
			foreach (Card card in cards)
				cardsData.Add(card.GetData());

			return new Data(
				cardsData,
				cardsLeftCount,
				layout
			);
		}

		~Board()
		{
			foreach (Card card in cards)
				card.Clicked -= OnCardClicked;
			cards.Clear();
		}

		public void Clear()
		{
			foreach (Card card in cards)
				card.Clicked -= OnCardClicked;
			cards.Clear();

			grid.Children.Clear();

			grid.RowDefinitions.Clear();
			grid.ColumnDefinitions.Clear();
		}

		private void MakeGrid(int cols, int rows)
		{
			for (int i = 0; i < rows; i++)
				grid.RowDefinitions.Add(new RowDefinition());

			for (int i = 0; i < cols; i++)
				grid.ColumnDefinitions.Add(new ColumnDefinition());
		}

		Random random = new Random();

		private List<ImageDefinition> PrepareImageDefinitions(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType, int count)
		{
			List<ImageDefinition> newImageDefintions = new List<ImageDefinition>(count);

			List<int> indexes = Utilities.GetRandomIntSet(ImagePool.frontImages[frontType].Count, count / 2);

			for (int i = 0; i < count / 2; i++)
			{
				int frontImageId = indexes[i];

				for (int j = 0; j < 2; j++)
					newImageDefintions.Add(new ImageDefinition(frontType, frontImageId, backType));
			}

			Utilities.Shuffle(newImageDefintions);

			return newImageDefintions;
		}

		private void MakeCard(int row, int col, int width, int height, ImageDefinition imageDefinition)
		{
			Card card = new Card()
			{
				Width = width,
				Height = height,
			};
			card.Setup(imageDefinition);
			card.Clicked += OnCardClicked;
			
			Grid.SetRow(card, row);
			Grid.SetColumn(card, col);

			cards.Add(card);
			grid.Children.Add(card);

			cardsLeftCount++;
		}

		private void LoadCard(Card.Data cardData, int row, int col, int width, int height)
		{
			Card card = new Card()
			{
				Width = width,
				Height = height,
			};
			card.Load(cardData);
			card.Clicked += OnCardClicked;

			Grid.SetRow(card, row);
			Grid.SetColumn(card, col);

			cards.Add(card);
			grid.Children.Add(card);
		}

		private void OnCardClicked(Card card)
		{
			// If we're already checking cards. Ignore this card
			if (IsChecking)
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

			CheckCards();
		}

		private async void CheckCards()
		{
			// Start checking
			IsChecking = true;

			// Wait 1 second
			await Task.Delay(1000);

			// Check if all cards have same id
			int id = clickedCards[0].Id;
			if (!clickedCards.All(item => item.Id == id))
			{
				// Fail
				foreach (Card _card in clickedCards)
					_card.Hide();

				MatchMade?.Invoke(false);
			}
			else
			{
				// Success
				foreach (Card _card in clickedCards)
					_card.Remove();

				MatchMade?.Invoke(true);

				cardsLeftCount -= 2;

				if (cardsLeftCount == 0)
					GameFinished?.Invoke();
			}

			// Stop checking
			clickedCards.Clear();
			IsChecking = false;
		}

		#region Create/load grid functions

		private void MakeFourByFour(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType)
		{
			List<ImageDefinition> imageDefinitions = PrepareImageDefinitions(frontType, backType, 16);

			MakeGrid(4, 4);

			for (int i = 0; i < 16; i++)
				MakeCard(i % 4, i / 4, 100, 100, imageDefinitions[i]);
		}

		private void LoadFourByFour(List<Card.Data> cardDatas)
		{
			MakeGrid(4, 4);

			for (int i = 0; i < 16; i++)
				LoadCard(cardDatas[i], i % 4, i / 4, 100, 100);
		}

		private void MakeFiveByFive(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType)
		{
			List<ImageDefinition> imageDefinitions = PrepareImageDefinitions(frontType, backType, 24);

			MakeGrid(5, 5);

			for (int i = 0; i < 25; i++)
			{
				int id = i;
				if (id == 12) continue; // Skip the center tile
				if (id > 12) id--;
				MakeCard(i % 5, i / 5, 80, 80, imageDefinitions[id]);
			}
		}

		private void LoadFiveByFive(List<Card.Data> cardDatas)
		{
			MakeGrid(5, 5);

			for (int i = 0; i < 25; i++)
			{
				int id = i;
				if (id == 12) continue; // Skip the center tile
				if (id > 12) id--;
				LoadCard(cardDatas[id], i % 5, i / 5, 80, 80);
			}
		}

		private void MakeSixByFour(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType)
		{
			List<ImageDefinition> imageDefinitions = PrepareImageDefinitions(frontType, backType, 24);

			MakeGrid(6, 4);

			for (int i = 0; i < 24; i++)
				MakeCard(i % 4, i / 4, 100, 100, imageDefinitions[i]);
		}

		private void LoadSixByFour(List<Card.Data> cardDatas)
		{
			MakeGrid(6, 4);

			for (int i = 0; i < 24; i++)
				LoadCard(cardDatas[i], i % 4, i / 4, 100, 100);
		}

		private void MakeSixBySix(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType)
		{
			List<ImageDefinition> imageDefinitions = PrepareImageDefinitions(frontType, backType, 36);

			MakeGrid(6, 6);

			for (int i = 0; i < 36; i++)
				MakeCard(i % 6, i / 6, 60, 60, imageDefinitions[i]);
		}

		private void LoadSixBySix(List<Card.Data> cardDatas)
		{
			MakeGrid(6, 6);

			for (int i = 0; i < 36; i++)
				LoadCard(cardDatas[i], i % 6, i / 6, 60, 60);
		}
		#endregion
	}
}
