using System.Windows;
using System.Windows.Controls;

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

			Hide();
		}

		public virtual void Show(UserControl modelContent)
		{
			if (modelContent != null)
				ContainerContent.Children.Remove(modelContent);

			ContainerContent.Children.Add(modelContent);
			this.modelContent = modelContent;

			Visibility = Visibility.Visible;
		}

		public virtual void Hide()
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
