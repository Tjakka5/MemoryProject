using Framework;
using Game.Scripts;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game.Controls
{
	/// <summary>
	/// Interaction logic for Board.xaml
	/// </summary>
	public partial class Board : UserControl
	{
		public delegate void CardClickedHandler(Card card);
		public event CardClickedHandler CardClicked;

		public enum Layouts
		{
			FourByFour,
			FiveByFive,
			FourBySix,
			SixBySix,
		};

		private List<Card> cards = new List<Card>();

		public Board()
		{
			InitializeComponent();
		}
		public void Setup(Layouts layouts, ImagePool.FrontTypes frontTypes, ImagePool.BackTypes backTypes)
		{
			cards.Clear();
			
			switch (layouts)
			{
				case Layouts.FourByFour:
					MakeFourByFour(frontTypes, backTypes);
					break;
				case Layouts.FiveByFive:
					MakeFiveByFive(frontTypes, backTypes);
					break;
				case Layouts.FourBySix:
					MakeFourBySix(frontTypes, backTypes);
					break;
				case Layouts.SixBySix:
					MakeSixBySix(frontTypes, backTypes);
					break;
			}
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

		private void MakeCard(int row, int col, ImageDefinition imageDefinition)
		{
			Card card = new Card();
			card.Setup(imageDefinition);
			card.Width = 100;
			card.Height = 100;
			card.Clicked += OnCardClicked;
			
			Grid.SetRow(card, row);
			Grid.SetColumn(card, col);

			cards.Add(card);
			grid.Children.Add(card);
		}

		private void OnCardClicked(Card card)
		{
			CardClicked?.Invoke(card);
		}

		private void MakeFourByFour(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType)
		{
			List<ImageDefinition> imageDefinitions = PrepareImageDefinitions(frontType, backType, 16);

			MakeGrid(4, 4);

			for (int i = 0; i < 16; i++)
				MakeCard(i % 4, i / 4, imageDefinitions[i]);
		}

		private void MakeFiveByFive(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType)
		{
			List<ImageDefinition> imageDefinitions = PrepareImageDefinitions(frontType, backType, 24);

			MakeGrid(5, 5);

			for (int i = 0; i < 25; i++)
			{
				if (i == 12) continue; // Skip the center tile
				MakeCard(i % 5, i / 5, imageDefinitions[i]);
			}
		}

		private void MakeFourBySix(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType)
		{
			List<ImageDefinition> imageDefinitions = PrepareImageDefinitions(frontType, backType, 24);

			MakeGrid(4, 6);

			for (int i = 0; i < 24; i++)
				MakeCard(i % 6, i / 6, imageDefinitions[i]);
		}

		private void MakeSixBySix(ImagePool.FrontTypes frontType, ImagePool.BackTypes backType)
		{
			List<ImageDefinition> imageDefinitions = PrepareImageDefinitions(frontType, backType, 36);

			MakeGrid(6, 6);

			for (int i = 0; i < 36; i++)
				MakeCard(i % 6, i / 6, imageDefinitions[i]);
		}
	}
}
