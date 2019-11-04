using Game.Scripts;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Game.Controls
{
	/// <summary>
	/// Interaction logic for Card.xaml
	/// </summary>
	public partial class Card : UserControl
	{
		// Serializable data for this class
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

		/// <summary>
		/// Setup the card
		/// </summary>
		/// <param name="imageDefinition">Image definition for the card</param>
		public void Setup(ImageDefinition imageDefinition)
		{
			this.imageDefinition = imageDefinition;

			Id = imageDefinition.frontImageId;

			imageFront.Source = ImagePool.frontImages[imageDefinition.frontImageType][imageDefinition.frontImageId];
			imageBack.Source = ImagePool.backImages[imageDefinition.backImageType];

			Hide(0.0f);
		}

		/// <summary>
		/// Load the card from data
		/// </summary>
		/// <param name="data">Data to load from</param>
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

		/// <summary>
		/// Exports the object as serializable data
		/// </summary>
		/// <returns></returns>
		public Data GetData()
		{
			return new Data(
				isRemoved,
				Id,
				imageDefinition
			);
		}

		/// <summary>
		/// Shows the card
		/// </summary>
		/// <param name="animationDuration">Duration for the show animation</param>
		public void Show(float animationDuration = 0.2f)
		{
			State = ViewState.FRONT;

			imageFrontBase.Visibility = Visibility.Visible;
			imageFront.Visibility = Visibility.Visible;
			imageBack.Visibility = Visibility.Collapsed;

			ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, 20), TimeSpan.FromSeconds(animationDuration));
			BeginAnimation(Rectangle.MarginProperty, thicknessAnimation);
		}

		/// <summary>
		/// Hides the card
		/// </summary>
		/// <param name="animationDuration">Duration for the hide animation</param>
		public void Hide(float animationDuration = 0.2f)
		{
			State = ViewState.BACK;

			imageFrontBase.Visibility = Visibility.Collapsed;
			imageFront.Visibility = Visibility.Collapsed;
			imageBack.Visibility = Visibility.Visible;

			ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(animationDuration));
			BeginAnimation(Rectangle.MarginProperty, thicknessAnimation);
		}

		/// <summary>
		/// Removes the card from the playing field
		/// </summary>
		public void Remove()
		{
			isRemoved = true;
			Visibility = Visibility.Hidden;
		}

		/// <summary>
		/// On mouse down
		/// </summary>
		/// <param name="e">Event</param>
		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);

			// Check if clicked button was the left button
			if (e.ChangedButton != MouseButton.Left)
				return;

			// Emit a clicked event
			Clicked?.Invoke(this);
		}
	}
}
