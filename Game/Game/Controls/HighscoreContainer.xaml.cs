using Game.Scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game.Controls
{
	/// <summary>
	/// Interaction logic for HighscoreContainer.xaml
	/// </summary>
	public partial class HighscoreContainer : UserControl
	{
		private static readonly string labelFormat = "{0}: {1} punten";
		private static readonly string noScoresMessage = "Nog geen scores";

		public static readonly DependencyProperty layout = DependencyProperty.Register("Layout", typeof(Board.Layouts), typeof(HighscoreContainer));
		public Board.Layouts Layout
		{
			get { return (Board.Layouts)GetValue(layout); }
			set
			{
				SetValue(layout, value);
			}
		}

		private List<Label> labels = null;

		public HighscoreContainer()
		{
			InitializeComponent();

			labels = new List<Label>()
			{
				player_1,
				player_2,
				player_3,
				player_4,
				player_5,
			};
		}

		public override void OnApplyTemplate()
		{
			// Set title
			switch (Layout)
			{
				case Board.Layouts.FourByFour:
					title.Content = "4 x 4";
					break;
				case Board.Layouts.FiveByFive:
					title.Content = "5 x 5";
					break;
				case Board.Layouts.SixByFour:
					title.Content = "6 x 4";
					break;
				case Board.Layouts.SixBySix:
					title.Content = "6 x 6";
					break;
			}

			// Get all scores...
			List<HighscoreData.Data> highscoreDatas = HighscoreData.Load()
				.Where(score => score.layout == Layout).ToList() // ...Of this layout type...
				.OrderByDescending(score => score.score).ToList(); // ...And order it from highest to lowest

			// Set all label's content to the data or hide it
			for (int i = 0; i < labels.Count; i++)
			{
				Label label = labels[i];

				if (i < highscoreDatas.Count)
				{
					HighscoreData.Data highscoreData = highscoreDatas[i];

					label.Content = string.Format(labelFormat, highscoreData.name, highscoreData.score);
					label.Visibility = Visibility.Visible;
				}
				else
					label.Visibility = Visibility.Hidden;
			}

			// If there's no scores for this layout. Show a message instead
			if (highscoreDatas.Count == 0)
			{
				labels[0].Visibility = Visibility.Visible;
				labels[0].Content = noScoresMessage;
			}

			base.OnApplyTemplate();
		}
	}
}
