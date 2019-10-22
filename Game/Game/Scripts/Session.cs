using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Scripts
{
    public class Session
    {
		private Board board = null;
		private List<Player> players = null;

		public Session(Grid grid, List<string> playerNames, Board.Layouts layout)
		{
			// Make temporary images
			List<ImageSource> tempFrontImages = new List<ImageSource>();
			for (int i = 0; i < 100; i++)
				tempFrontImages.Add(new BitmapImage(new Uri("Resources/Images/tempFrontImage.png", UriKind.Relative)));

			ImageSource tempBackImage = new BitmapImage(new Uri("Resources/Images/tempBackImage.png", UriKind.Relative));

            // Make board
            board = new Board(layout, tempFrontImages, tempBackImage);
			grid.Children.Add(board);

            // Make players
            players = new List<Player>();
            foreach (string playerName in playerNames)
                players.Add(new Player(playerName));
        }
    }
}
