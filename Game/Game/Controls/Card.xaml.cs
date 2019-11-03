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
		[Serializable]
		public class Data
		{
			public readonly bool isRemoved = false;
			public readonly int id = 0;
			public readonly ImageDefinition imageDefinition = null;

			public Data(bool isRemoved, int id, ImageDefinition imageDefinition)
			{
				this.isRemoved = isRemoved;
				this.id = id;
				this.imageDefinition = imageDefinition;
			}
		}

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

		private bool isRemoved = false;

		public ViewState State
		{
			get;
			private set;
		}

		private ImageDefinition imageDefinition = null;

		public Card()
		{
			InitializeComponent();
		}

		public void Setup(ImageDefinition imageDefinition)
		{
			this.imageDefinition = imageDefinition;

			Id = imageDefinition.frontImageId;

			imageFront.Source = ImagePool.frontImages[imageDefinition.frontImageType][imageDefinition.frontImageId];
			imageBack.Source = ImagePool.backImages[imageDefinition.backImageType];

			Hide(0.0f);
		}

		public void Load(Data data)
		{
			imageDefinition = data.imageDefinition;
			isRemoved = data.isRemoved;
			Id = data.id;

			imageFront.Source = ImagePool.frontImages[imageDefinition.frontImageType][imageDefinition.frontImageId];
			imageBack.Source = ImagePool.backImages[imageDefinition.backImageType];

			Hide(0.0f);

			if (isRemoved)
				Remove();
		}

		public Data GetData()
		{
			return new Data(
				isRemoved,
				Id,
				imageDefinition
			);
		}

		public void Show(float animationDuration = 0.2f)
		{
			State = ViewState.FRONT;

			imageFrontBase.Visibility = Visibility.Visible;
			imageFront.Visibility = Visibility.Visible;
			imageBack.Visibility = Visibility.Collapsed;

			ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, 20), TimeSpan.FromSeconds(animationDuration));
			BeginAnimation(Rectangle.MarginProperty, thicknessAnimation);
		}

		public void Hide(float animationDuration = 0.2f)
		{
			State = ViewState.BACK;

			imageFrontBase.Visibility = Visibility.Collapsed;
			imageFront.Visibility = Visibility.Collapsed;
			imageBack.Visibility = Visibility.Visible;

			ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(animationDuration));
			BeginAnimation(Rectangle.MarginProperty, thicknessAnimation);
		}

		public void Remove()
		{
			isRemoved = true;
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
