using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

		public Board(Layouts layouts) {
			switch (layouts)
			{
				case Layouts.FourByFour:
					MakeFourByFour();
					break;
				case Layouts.FiveByFive:
					MakeFiveByFive();
					break;
				case Layouts.FourBySix:
					MakeFourBySix();
					break;
				case Layouts.SixBySix:
					MakeSixBySix();
					break;
			}
		}

		private void MakeGrid(int cols, int rows) {
			for (int i = 0; i < rows; i++)
				RowDefinitions.Add(new RowDefinition());
			
			for (int i = 0; i < cols; i++)
				ColumnDefinitions.Add(new ColumnDefinition());
		}

		private void MakeFourByFour() {
			MakeGrid(4, 4);
		}

		private void MakeFiveByFive() {
			MakeGrid(5, 5);
		}

		private void MakeFourBySix() {
			MakeGrid(4, 6);
		}

		private void MakeSixBySix() {
			MakeGrid(6, 6);
		}
	}
}
