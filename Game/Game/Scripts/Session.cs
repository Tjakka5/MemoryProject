using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Scripts
{
    public class Session
    {
		private Player currentPlayer = null;

        private Grid grid = null;

        private Board board = null;
        private List<Player> players = null;

        private List<string> initialPlayerNames = null;
        private Board.Layouts initialLayout = default;


        public Session(Grid grid, List<string> playerNames, Board.Layouts layout)
        {
            this.grid = grid;

            initialPlayerNames = playerNames;
            initialLayout = layout;

            Start();
        }

        private void Start()
        {
            // Make temporary images
            List<ImageSource> tempFrontImages = new List<ImageSource>();
            for (int i = 0; i < 100; i++)
                tempFrontImages.Add(new BitmapImage(new Uri("Resources/Images/tempFrontImage.png", UriKind.Relative)));

            ImageSource tempBackImage = new BitmapImage(new Uri("Resources/Images/tempBackImage.png", UriKind.Relative));

            // Make board
            board = new Board(initialLayout, tempFrontImages, tempBackImage);
            grid.Children.Add(board);

            // Make players
            players = new List<Player>();
            foreach (string playerName in initialPlayerNames)
                players.Add(new Player(playerName));

            Scoreboard scoreboard = new Scoreboard(players);
            grid.Children.Add(scoreboard);

            
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        public void Stop()
        {
            grid.Children.Remove(board);
        }
    }
}
