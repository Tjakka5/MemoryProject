using Game.Scripts;
using Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Game.Controls
{
	/// <summary>
	/// Interaction logic for Board.xaml
	/// </summary>
	public partial class Board : UserControl
	{
		/// <summary>
		/// Serializable data for this class
		/// </summary>
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

		private Random random = new Random();

		public Board()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Setup the board
		/// </summary>
		/// <param name="layout">Layout for the board</param>
		/// <param name="frontTypes">Fronttype for the cards</param>
		/// <param name="backTypes">Backtype for the cards</param>
		public void Setup(Layouts layout, ImagePool.FrontTypes frontTypes, ImagePool.BackTypes backTypes)
		{
			// Reset all variables
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

		/// <summary>
		/// Load state from data
		/// </summary>
		/// <param name="data">Data to load from</param>
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

		/// <summary>
		/// Exports object to serializable data
		/// </summary>
		/// <returns>Serializable data</returns>
		public Data GetData()
		{
			// Get all card's serializable data
			List<Card.Data> cardsData = new List<Card.Data>();
			foreach (Card card in cards)
				cardsData.Add(card.GetData());

			return new Data(
				cardsData,
				cardsLeftCount,
				layout
			);
		}

		/// <summary>
		/// Clears the object
		/// </summary>
		public void Clear()
		{
			// Remove all cards
			foreach (Card card in cards)
				card.Clicked -= OnCardClicked;
			cards.Clear();

			// Clear the crid
			grid.Children.Clear();
			grid.RowDefinitions.Clear();
			grid.ColumnDefinitions.Clear();
		}

		/// <summary>
		/// Makes a grid
		/// </summary>
		/// <param name="cols">Column count</param>
		/// <param name="rows">Row count</param>
		private void MakeGrid(int cols, int rows)
		{
			// Make rows
			for (int i = 0; i < rows; i++)
				grid.RowDefinitions.Add(new RowDefinition());

			// Make columns
			for (int i = 0; i < cols; i++)
				grid.ColumnDefinitions.Add(new ColumnDefinition());
		}

		/// <summary>
		/// Creates image definitions for the cards
		/// </summary>
		/// <param name="frontType">Fronttype for the cards</param>
		/// <param name="backType">Backtype for the cards</param>
		/// <param name="count">Amount of image definitions to prepare</param>
		/// <returns>List of image definition of size count. Each image definition is in the list twice</returns>
		private List<ImageDefinition> CreateImageDefinitions(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType, int count)
		{
			// Prepare list
			List<ImageDefinition> imageDefinitions = new List<ImageDefinition>(count);

			// Create list of random indexes of size count/2
			List<int> indexes = Utilities.GetRandomIntSet(ImagePool.frontImages[frontType].Count, count / 2);

			// Create two image definitions for each index
			foreach (int index in indexes)
			{
				imageDefinitions.Add(new ImageDefinition(frontType, index, backType));
				imageDefinitions.Add(new ImageDefinition(frontType, index, backType));
			}

			// Shuffle the image definitions list
			Utilities.Shuffle(imageDefinitions);

			return imageDefinitions;
		}

		/// <summary>
		/// Makes a card
		/// </summary>
		/// <param name="row">Row in the grid</param>
		/// <param name="col">Column in the grid</param>
		/// <param name="width">Width of the card</param>
		/// <param name="height">Height of the card</param>
		/// <param name="imageDefinition">Image definition for the card</param>
		private void MakeCard(int row, int col, int width, int height, ImageDefinition imageDefinition)
		{
			// Make card
			Card card = new Card()
			{
				Width = width,
				Height = height,
			};
			card.Setup(imageDefinition);
			card.Clicked += OnCardClicked;
			
			// Add it to the grid
			Grid.SetRow(card, row);
			Grid.SetColumn(card, col);
			grid.Children.Add(card);

			// Add it to cards list
			cards.Add(card);
			
			// Increment cards left counter
			cardsLeftCount++;
		}

		/// <summary>
		/// Loads a card from card data
		/// </summary>
		/// <param name="cardData">Card data to load from</param>
		/// <param name="row">Row of the card</param>
		/// <param name="col">Column of the card</param>
		/// <param name="width">Width of the card</param>
		/// <param name="height">Height of the card</param>
		private void LoadCard(Card.Data cardData, int row, int col, int width, int height)
		{
			// Make the card
			Card card = new Card()
			{
				Width = width,
				Height = height,
			};
			card.Load(cardData);
			card.Clicked += OnCardClicked;

			// Add it to the grid
			Grid.SetRow(card, row);
			Grid.SetColumn(card, col);
			grid.Children.Add(card);

			// Add it to the cards list
			cards.Add(card);
		}

		/// <summary>
		/// On card clicked
		/// </summary>
		/// <param name="card">Clicked card</param>
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

			// Start the card checking routine
			CheckCards();
		}

		/// <summary>
		/// Checks the current clicked cards. Acts accordingly
		/// </summary>
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

				// Hide each clicked card
				foreach (Card _card in clickedCards)
					_card.Hide();

				// Emit a match made event as failed
				MatchMade?.Invoke(false);
			}
			else
			{
				// Success

				// Remove each clicked card
				foreach (Card _card in clickedCards)
					_card.Remove();

				// Emit a match made event as success
				MatchMade?.Invoke(true);

				// Decrement cards left counter by amount of cards clicked
				cardsLeftCount -= clickedCards.Count;

				// If there are no cards left. Emit a game finished event
				if (cardsLeftCount == 0)
					GameFinished?.Invoke();
			}

			// Stop checking
			clickedCards.Clear();
			IsChecking = false;
		}

		/// <summary>
		/// Sets up the board as four by four
		/// </summary>
		/// <param name="frontType">Frontype for the cards</param>
		/// <param name="backType">Backtype for the card</param>
		private void MakeFourByFour(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType)
		{
			List<ImageDefinition> imageDefinitions = CreateImageDefinitions(frontType, backType, 16);

			MakeGrid(4, 4);

			for (int i = 0; i < 16; i++)
				MakeCard(i % 4, i / 4, 100, 100, imageDefinitions[i]);
		}

		/// <summary>
		/// Loads a four by four board from card datas
		/// </summary>
		/// <param name="cardDatas">List of card datas to load from</param>
		private void LoadFourByFour(List<Card.Data> cardDatas)
		{
			MakeGrid(4, 4);

			for (int i = 0; i < 16; i++)
				LoadCard(cardDatas[i], i % 4, i / 4, 100, 100);
		}

		/// <summary>
		/// Sets up the board as five by five
		/// </summary>
		/// <param name="frontType">Fronttypes for the cards</param>
		/// <param name="backType">Backtypes for the cards</param>
		private void MakeFiveByFive(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType)
		{
			List<ImageDefinition> imageDefinitions = CreateImageDefinitions(frontType, backType, 24);

			MakeGrid(5, 5);

			for (int i = 0; i < 25; i++)
			{
				int id = i;
				if (id == 12) continue; // Skip the center tile
				if (id > 12) id--; // Decrement the id when over 12
				MakeCard(i % 5, i / 5, 80, 80, imageDefinitions[id]);
			}
		}

		/// <summary>
		/// Loads a five by five grid from card datas
		/// </summary>
		/// <param name="cardDatas">List of card data to load from</param>
		private void LoadFiveByFive(List<Card.Data> cardDatas)
		{
			MakeGrid(5, 5);

			for (int i = 0; i < 25; i++)
			{
				int id = i;
				if (id == 12) continue; // Skip the center tile
				if (id > 12) id--; // Decrement the id when over 12
				LoadCard(cardDatas[id], i % 5, i / 5, 80, 80);
			}
		}

		/// <summary>
		/// Sets up the board as six by four
		/// </summary>
		/// <param name="frontType">Fronttype of the cards</param>
		/// <param name="backType">Backtype of the cards</param>
		private void MakeSixByFour(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType)
		{
			List<ImageDefinition> imageDefinitions = CreateImageDefinitions(frontType, backType, 24);

			MakeGrid(6, 4);

			for (int i = 0; i < 24; i++)
				MakeCard(i % 4, i / 4, 100, 100, imageDefinitions[i]);
		}

		/// <summary>
		/// Loads a six by four board from card datas
		/// </summary>
		/// <param name="cardDatas">List of card data to load from</param>
		private void LoadSixByFour(List<Card.Data> cardDatas)
		{
			MakeGrid(6, 4);

			for (int i = 0; i < 24; i++)
				LoadCard(cardDatas[i], i % 4, i / 4, 100, 100);
		}

		/// <summary>
		/// Sets up the board as six by six
		/// </summary>
		/// <param name="frontType">Fronttypes of the cards</param>
		/// <param name="backType">Backtypes of the cards</param>
		private void MakeSixBySix(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType)
		{
			List<ImageDefinition> imageDefinitions = CreateImageDefinitions(frontType, backType, 36);

			MakeGrid(6, 6);

			for (int i = 0; i < 36; i++)
				MakeCard(i % 6, i / 6, 60, 60, imageDefinitions[i]);
		}

		/// <summary>
		/// Loads a six by six board from card datas
		/// </summary>
		/// <param name="cardDatas">List of card datas to load from</param>
		private void LoadSixBySix(List<Card.Data> cardDatas)
		{
			MakeGrid(6, 6);

			for (int i = 0; i < 36; i++)
				LoadCard(cardDatas[i], i % 6, i / 6, 60, 60);
		}
	}
}
