using System.Windows;
using System.Windows.Controls;

namespace Game.Controls.ModalContent
{
    /// <summary>
    /// Interaction logic for ModalContentPause.xaml
    /// </summary>
    public partial class ModalContentPause : UserControl
    {
        public delegate void ButtonRestartClickedHandler();
        public event ButtonRestartClickedHandler ButtonRestartClicked;

        public delegate void ButtonResumeClickedHandler();
        public event ButtonResumeClickedHandler ButtonResumeClicked;

        public delegate void ButtonMenuClickedHandler();
        public event ButtonMenuClickedHandler ButtonMenuClicked;

        public ModalContentPause()
        {
            InitializeComponent();

			buttonRestart.Clicked += OnButtonRestartClicked;
			buttonResume.Clicked += OnButtonResumeClicked;
			buttonMenu.Clicked += OnButtonMenuClicked;
        }

		/// <summary>
		/// On restart button clicked
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event</param>
        private void OnButtonRestartClicked(object sender, RoutedEventArgs e)
        {
            ButtonRestartClicked?.Invoke();
        }

		/// <summary>
		/// On resume button clicked
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event</param>
        private void OnButtonResumeClicked(object sender, RoutedEventArgs e)
        {
            ButtonResumeClicked?.Invoke();
        }

		/// <summary>
		/// On menu button clicked
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event</param>
        private void OnButtonMenuClicked(object sender, RoutedEventArgs e)
        {
            ButtonMenuClicked?.Invoke();
        }
    }
}
