using Game.Scripts;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace Game.Controls
{
	/// <summary>
	/// Interaction logic for PlayerView.xaml
	/// </summary>
	public partial class PlayerView : UserControl
	{
		private readonly string displayFormatSingular = "{0} heeft {1} punt";
		private readonly string displayFormatPlural = "{0} heeft {1} punten";

		private DropShadowEffect effect = new DropShadowEffect()
		{
			ShadowDepth = 15,
			BlurRadius = 3,
			Opacity = 0.2f,
		};

		private Player player = null;

		public PlayerView()
		{
			InitializeComponent();
		}

		public void Bind(Player player)
		{
			this.player = player;

			player.OnScoreUpdate += UpdateScoreVisual;
			UpdateScoreVisual(player);

			player.OnTurnChanged += UpdateTurnVisual;
			UpdateTurnVisual(player);
		}

		public void Unbind()
		{
			if (player == null)
				return;
			
			label.Text = string.Empty;

			player.OnScoreUpdate -= UpdateScoreVisual;
			player.OnTurnChanged -= UpdateTurnVisual;
		}

		private void UpdateScoreVisual(Player player)
		{
			string displayFormat = player.Score == 1 ? displayFormatSingular : displayFormatPlural;

			label.Text = string.Format(displayFormat, player.Name, player.Score);
		}

		private void UpdateTurnVisual(Player player)
		{
			if (player.HasTurn)
			{
				ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, -10, 0, 10), TimeSpan.FromSeconds(0.2f));
				grid.BeginAnimation(Rectangle.MarginProperty, thicknessAnimation);

				image.Effect = effect;
			}
			else
			{
				ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2f));
				grid.BeginAnimation(Rectangle.MarginProperty, thicknessAnimation);

				image.Effect = null;
			}
		}
	}
}
