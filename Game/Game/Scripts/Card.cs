using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Scripts
{
	public class Card : ItemsControl
	{
		private enum ViewState
		{
			FRONT,
			BACK,
		}

		private ViewState state = ViewState.FRONT;
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

			State = ViewState.FRONT;

			AddChild(frontImage);
			AddChild(backImage);
		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);

			if (State == ViewState.FRONT)
				State = ViewState.BACK;
			else if (State == ViewState.BACK)
				State = ViewState.FRONT;
		}
	}
}
