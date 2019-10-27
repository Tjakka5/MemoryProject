using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Framework.Scheduling;
using Game.Scripts;

namespace Game
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private Session currentSession;

        public MainWindow()
		{
			InitializeComponent();

            List<string> players = new List<string>() {
                "Auke", "Maurice", "Hannah", "Justin", "Marijke", "Pixel"
            };

            currentSession = new Session(grid, LabelCurrentPlayer, players, Board.Layouts.FourByFour);

            ButtonRestart.Click += ButtonRestartClicked;
        }

        ~MainWindow()
        {
            ButtonRestart.Click -= ButtonRestartClicked;
        }

        public void ButtonRestartClicked(object sender, RoutedEventArgs e)
        {
            currentSession.Restart();
        }
    }
}