using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game.Scripts
{
	public class Board : Grid
	{
		public enum Layouts {
			FourByFour,
			FiveByFive,
			FourBySix,
			SixBySix,
		};

		private List<Card> cards;

		public Board(Layouts layouts, List<ImageSource> frontImages, ImageSource backImage) {
			cards = new List<Card>();

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

		private void MakeGrid(int cols, int rows) {
			for (int i = 0; i < rows; i++)
				RowDefinitions.Add(new RowDefinition());
			
			for (int i = 0; i < cols; i++)
				ColumnDefinitions.Add(new ColumnDefinition());
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

		private Card MakeCard(ImageSource frontImage, ImageSource backImage)
		{
			Card card = new Card(frontImage, backImage);

			cards.Add(card);

			return card;
		}

		private void MakeFourByFour(List<ImageSource> frontImages, ImageSource backImage) {
			List<ImageSource> frontImagePool = PrepareImagePool(frontImages, 16);

			MakeGrid(4, 4);

			for (int i = 0; i < 16; i++)
				MakeCard(frontImagePool[i], backImage);
		}

		private void MakeFiveByFive(List<ImageSource> frontImages, ImageSource backImage) {
			List<ImageSource> frontImagePool = PrepareImagePool(frontImages, 25);

			MakeGrid(5, 5);

			for (int i = 0; i < 16; i++)
			{
				if (i == 12) continue; // Skip the center tile
				MakeCard(frontImagePool[i], backImage);
			}
		}

		private void MakeFourBySix(List<ImageSource> frontImages, ImageSource backImage) {
			List<ImageSource> frontImagePool = PrepareImagePool(frontImages, 24);

			MakeGrid(4, 6);

			for (int i = 0; i < 24; i++)
				MakeCard(frontImagePool[i], backImage);
		}

		private void MakeSixBySix(List<ImageSource> frontImages, ImageSource backImage) {
			List<ImageSource> frontImagePool = PrepareImagePool(frontImages, 36);

			MakeGrid(6, 6);

			for (int i = 0; i < 36; i++)
				MakeCard(frontImagePool[i], backImage);
		}
	}
}
