using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Controls
{
	/// <summary>
	/// Interaction logic for Card.xaml
	/// </summary>
	public partial class Card : UserControl
	{
		public delegate void ClickHandler(Card card);
		public event ClickHandler Clicked;

		public enum ViewState
		{
			FRONT,
			BACK,
		}

		public int Id
		{
			get;
			private set;
		}

		public ViewState State
		{
			get;
			private set;
		}

		public Card()
		{
			InitializeComponent();

			Hide();
		}

		public void Setup(ImageSource frontImageSource, ImageSource backImageSource)
		{
			imageFront.Source = new BitmapImage(new System.Uri("https://assets.pokemon.com/assets/cms2/img/pokedex/full/006.png"));
			imageBack.Source = new BitmapImage(new System.Uri("https://assets.pokemon.com/assets/cms2/img/pokedex/full/003.png"));

			//imageFront.Source = frontImageSource;
			//imageBack.Source = backImageSource;
		}

		public void Show()
		{
			State = ViewState.FRONT;

			imageFront.Visibility = Visibility.Collapsed;
			imageBack.Visibility = Visibility.Visible;
		}

		public void Hide()
		{
			State = ViewState.BACK;

			imageFront.Visibility = Visibility.Visible;
			imageBack.Visibility = Visibility.Collapsed;
		}

		public void Remove()
		{
			Visibility = Visibility.Hidden;
		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.ChangedButton != MouseButton.Left)
				return;

			Clicked?.Invoke(this);
		}
	}
}
