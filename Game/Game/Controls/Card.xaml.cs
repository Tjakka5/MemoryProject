using Framework.Scheduling;
using Game.Scripts;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

			imageFrontBase.Visibility = Visibility.Visible;
			imageFront.Visibility = Visibility.Visible;
			imageBack.Visibility = Visibility.Collapsed;

			ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, 20), TimeSpan.FromSeconds(0.2f));
			BeginAnimation(Rectangle.MarginProperty, thicknessAnimation);
		}

		public void Hide()
		{
			State = ViewState.BACK;

			imageFrontBase.Visibility = Visibility.Collapsed;
			imageFront.Visibility = Visibility.Collapsed;
			imageBack.Visibility = Visibility.Visible;

			ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2f));
			BeginAnimation(Rectangle.MarginProperty, thicknessAnimation);
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
