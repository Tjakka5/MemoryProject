using Game.Scripts;
using System;
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

		public void Setup(ImageDefinition imageDefinition)
		{
			Id = imageDefinition.frontImageId;

			imageFront.Source = ImagePool.frontImages[imageDefinition.frontImageType][imageDefinition.frontImageId];
			imageBack.Source = ImagePool.backImages[imageDefinition.backImageType];
		}

		public void Show()
		{
			State = ViewState.FRONT;

			imageFront.Visibility = Visibility.Visible;
			imageBack.Visibility = Visibility.Collapsed;
		}

		public void Hide()
		{
			State = ViewState.BACK;

			imageFront.Visibility = Visibility.Collapsed;
			imageBack.Visibility = Visibility.Visible;
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
