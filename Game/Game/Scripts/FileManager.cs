using Game.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
	public static class FileManager
	{
		private static string root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		private static IFormatter formatter = new BinaryFormatter();

		[Serializable]
		private class HighscoreDatas
		{
			public readonly Board.Layouts layout = default;
			public readonly List<HighscoreData> highscoreDatas = null;

			public HighscoreDatas(Board.Layouts layout, List<HighscoreData> highscoreDatas)
			{
				this.layout = layout;
				this.highscoreDatas = highscoreDatas;
			}
		}

		public static void WriteHighscore(HighscoreData highscoreData)
		{

		}

		public static List<HighscoreData> ReadHighscores()
		{
			string path = Path.Combine(root, "highscores.txt");

			List<HighscoreData> highscoreDatas = null;

			try
			{
				Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

				highscoreDatas = (List<HighscoreData>) formatter.Deserialize(stream);

			}
			catch (IOException e)
			{
				highscoreDatas = new List<HighscoreData>();
			}

			return highscoreDatas;
		}
	}
}
