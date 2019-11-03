using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Game.Controls
{
    /// <summary>
    /// Interaction logic for Modal.xaml
    /// </summary>
    public partial class Modal : UserControl
    {
		private UserControl modelContent = null;

		public Modal()
        {
            InitializeComponent();

			ContainerContent.Margin = new Thickness(0, -700, 0, 700);
			Visibility = Visibility.Collapsed;
		}

		public virtual void Show(UserControl modelContent)
		{
			if (modelContent != null)
				ContainerContent.Children.Remove(modelContent);

			ContainerContent.Children.Add(modelContent);
			this.modelContent = modelContent;

			Visibility = Visibility.Visible;

			ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.8f));
			thicknessAnimation.EasingFunction = new ElasticEase()
			{
				Oscillations = 1,
			};

			ContainerContent.BeginAnimation(Rectangle.MarginProperty, thicknessAnimation);
		}

		public virtual void Hide()
		{
			ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, -700, 0, 700), TimeSpan.FromSeconds(0.8f));
			thicknessAnimation.EasingFunction = new ExponentialEase()
			{
				EasingMode = EasingMode.EaseOut,
			};
			thicknessAnimation.Completed += EndHide;

			ContainerContent.BeginAnimation(Rectangle.MarginProperty, thicknessAnimation);
		}

		private void EndHide(object sender, EventArgs e)
		{
			if (modelContent != null)
			{
				ContainerContent.Children.Remove(modelContent);
				modelContent = null;
			}

			Visibility = Visibility.Collapsed;
		}
    }
}
