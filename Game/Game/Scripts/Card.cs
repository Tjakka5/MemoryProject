using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Game.Scripts
{
	public class Card : ItemsControl
	{
		public delegate void ClickHandler(Card card);
		public event ClickHandler Clicked;

		private enum ViewState
		{
			FRONT,
			BACK,
		}

		public int Id
		{
			get;
			private set;
		}

		private ViewState state = default;
		private ViewState State
		{
			get { return state; }
			set
			{
				if (value == ViewState.FRONT)
				{
					frontImage.Visibility = System.Windows.Visibility.Visible;
					backImage.Visibility = System.Windows.Visibility.Collapsed;
				}
				else if (value == ViewState.BACK)
				{
					frontImage.Visibility = System.Windows.Visibility.Collapsed;
					backImage.Visibility = System.Windows.Visibility.Visible;
				}

				state = value;
			}
		}

		private readonly Image frontImage = null;
		private readonly Image backImage = null;

		public Card(ImageSource frontImageSource, ImageSource backImageSource)
		{
			frontImage = new Image { Source = frontImageSource };
			backImage = new Image { Source = backImageSource };

			State = ViewState.BACK;

			AddChild(frontImage);
			AddChild(backImage);
		}

		public void Show()
		{
			State = ViewState.FRONT;
		}
		public void Hide()
		{
			State = ViewState.BACK;
		}

		public void Remove()
		{
			Visibility = System.Windows.Visibility.Hidden;
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
