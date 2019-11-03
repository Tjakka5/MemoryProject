using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game.Controls
{
	/// <summary>
	/// Interaction logic for CardSelect.xaml
	/// </summary>
	public partial class CardSelect : UserControl
	{
		public delegate void ClickedHandler(object sender, MouseButtonEventArgs e);
		public event ClickedHandler Clicked;

		public static readonly DependencyProperty shownImage = DependencyProperty.Register("ShownImage", typeof(ImageSource), typeof(CardSelect));
		public ImageSource ShownImage
		{
			get { return (ImageSource)GetValue(shownImage); }
			set
			{
				SetValue(shownImage, value);
			}
		}

		public static readonly DependencyProperty shownImageSecondary = DependencyProperty.Register("ShownImageSecondary", typeof(ImageSource), typeof(CardSelect));
		public ImageSource ShownImageSecondary
		{
			get { return (ImageSource)GetValue(shownImageSecondary); }
			set
			{
				SetValue(shownImageSecondary, value);
			}
		}

		private DropShadowEffect effect = new DropShadowEffect()
		{
			ShadowDepth = 15,
			BlurRadius = 3,
			Opacity = 0.2f,
		};

		public CardSelect()
		{
			InitializeComponent();
		}

		public override void OnApplyTemplate()
		{
			image.Source = ShownImage;
			imageSecondary.Source = ShownImageSecondary;

			base.OnApplyTemplate();
		}

		public void Select()
		{
			ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(10, 0, 10, 20), TimeSpan.FromSeconds(0.2f));
			BeginAnimation(Rectangle.MarginProperty, thicknessAnimation);

			imageSecondary.Effect = effect;
		}

		public void Deselect()
		{
			ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(10, 0, 10, 0), TimeSpan.FromSeconds(0.2f));
			BeginAnimation(Rectangle.MarginProperty, thicknessAnimation);

			imageSecondary.Effect = null;
		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			Clicked?.Invoke(this, e);

			base.OnMouseUp(e);
		}
	}
}
