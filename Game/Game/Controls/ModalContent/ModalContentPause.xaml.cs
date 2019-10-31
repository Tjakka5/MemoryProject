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
        }

        private void OnButtonRestartClicked(object sender, RoutedEventArgs e)
        {
            ButtonRestartClicked?.Invoke();
        }

        private void OnButtonResumeClicked(object sender, RoutedEventArgs e)
        {
            ButtonResumeClicked?.Invoke();
        }

        private void OnButtonMenuClicked(object sender, RoutedEventArgs e)
        {
            ButtonMenuClicked?.Invoke();
        }
    }
}
