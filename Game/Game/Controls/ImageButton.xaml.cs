using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Game.Controls
{
	/// <summary>
	/// Interaction logic for ImageButton.xaml
	/// </summary>
	public partial class ImageButton : UserControl
	{
		public delegate void ClickedHandler(object sender, MouseButtonEventArgs e);
		public event ClickedHandler Clicked;

		public static readonly DependencyProperty imageDefault = DependencyProperty.Register("ImageDefault", typeof(ImageSource), typeof(ImageButton));
		public ImageSource ImageDefault
		{
			get { return (ImageSource) GetValue(imageDefault); }
			set {
				
				SetValue(imageDefault, value);
			}
		}

		public static readonly DependencyProperty imageHover = DependencyProperty.Register("ImageHover", typeof(ImageSource), typeof(ImageButton));
		public ImageSource ImageHover
		{
			get { return (ImageSource)GetValue(imageHover); }
			set { SetValue(imageHover, value); }
		}

		public static readonly DependencyProperty imageClick = DependencyProperty.Register("ImageClick", typeof(ImageSource), typeof(ImageButton));
		public ImageSource ImageClick
		{
			get { return (ImageSource)GetValue(imageClick); }
			set { SetValue(imageClick, value); }
		}

		public static readonly DependencyProperty text = DependencyProperty.Register("Text", typeof(string), typeof(ImageButton));
		public string Text
		{
			get { return (string)GetValue(text); }
			set {
				SetValue(text, value);
			}
		}

		private bool hovering = false;
		private bool clicking = false;

		public ImageButton()
		{
			InitializeComponent();
		}

		public override void OnApplyTemplate()
		{
			label.Content = Text;
			UpdateVisual();

			base.OnApplyTemplate();
		}

		protected override void OnMouseEnter(MouseEventArgs e)
		{
			hovering = true;

			UpdateVisual();

			base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(MouseEventArgs e)
		{
			hovering = false;
			clicking = false;

			UpdateVisual();

			base.OnMouseLeave(e);
		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			clicking = true;

			UpdateVisual();

			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			if (clicking)
				Clicked?.Invoke(this, e);

			clicking = false;

			UpdateVisual();

			base.OnMouseUp(e);
		}

		private void UpdateVisual()
		{
			label.Margin = new Thickness(0, 0, 0, 0);
			image.Source = ImageDefault;

			if (hovering)
			{
				if (clicking)
				{
					image.Source = ImageClick;
					label.Margin = new Thickness(0, 7, 0, 0);
				}
				else
					image.Source = ImageHover;
			}
			else
			{
				if (clicking)
				{
					image.Source = ImageClick;
					label.Margin = new Thickness(0, 7, 0, 0);
				}
			}
		}
	}
}
