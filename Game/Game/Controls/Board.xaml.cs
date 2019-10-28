using Framework;
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
		public void Setup(Layouts layouts, List<ImageSource> frontImages, ImageSource backImage)
		{
			cards.Clear();
			
			switch (layouts)
			{
				case Layouts.FourByFour:
					MakeFourByFour(frontImages, backImage);
					break;
				case Layouts.FiveByFive:
					MakeFiveByFive(frontImages, backImage);
					break;
				case Layouts.FourBySix:
					MakeFourBySix(frontImages, backImage);
					break;
				case Layouts.SixBySix:
					MakeSixBySix(frontImages, backImage);
					break;
			}
		}

		~Board()
		{
			Clear();
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

		private List<ImageSource> PrepareImagePool(List<ImageSource> imagePool, int count)
		{
			List<ImageSource> newImagePool = new List<ImageSource>(count);

			for (int i = 0; i < count / 2; i++)
			{
				ImageSource image = Utilities.GetRandom(imagePool);
				newImagePool.Add(image);
				newImagePool.Add(image);
			}

			Utilities.Shuffle(newImagePool);

			return newImagePool;
		}

		private void MakeCard(int row, int col, ImageSource frontImage, ImageSource backImage)
		{
			Card card = new Card();
			card.Setup(frontImage, backImage);
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

		private void MakeFourByFour(List<ImageSource> frontImages, ImageSource backImage)
		{
			List<ImageSource> frontImagePool = PrepareImagePool(frontImages, 16);

			MakeGrid(4, 4);

			for (int i = 0; i < 16; i++)
				MakeCard(i % 4, i / 4, frontImagePool[i], backImage);
		}

		private void MakeFiveByFive(List<ImageSource> frontImages, ImageSource backImage)
		{
			List<ImageSource> frontImagePool = PrepareImagePool(frontImages, 25);

			MakeGrid(5, 5);

			for (int i = 0; i < 25; i++)
			{
				if (i == 12) continue; // Skip the center tile
				MakeCard(i % 5, i / 5, frontImagePool[i], backImage);
			}
		}

		private void MakeFourBySix(List<ImageSource> frontImages, ImageSource backImage)
		{
			List<ImageSource> frontImagePool = PrepareImagePool(frontImages, 24);

			MakeGrid(4, 6);

			for (int i = 0; i < 24; i++)
				MakeCard(i % 6, i / 6, frontImagePool[i], backImage);
		}

		private void MakeSixBySix(List<ImageSource> frontImages, ImageSource backImage)
		{
			List<ImageSource> frontImagePool = PrepareImagePool(frontImages, 36);

			MakeGrid(6, 6);

			for (int i = 0; i < 36; i++)
				MakeCard(i % 6, i / 6, frontImagePool[i], backImage);
		}
	}
}
