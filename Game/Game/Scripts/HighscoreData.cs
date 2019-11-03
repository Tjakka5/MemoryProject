using Game.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game.Scripts
{
	public static class HighscoreData
	{
		private static string rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MemoryGame_33");
		private static string fullPath = Path.Combine(rootPath, "scores.txt");
		private static IFormatter formatter = new BinaryFormatter();

		[Serializable]
		public class Data
		{
			public readonly string name = string.Empty;
			public readonly int score = 0;
			public readonly Board.Layouts layout = default;

			public Data(string name, int score, Board.Layouts layout)
			{
				this.name = name;
				this.score = score;
				this.layout = layout;
			}
		}

		public static List<Data> Load()
		{
			EnsureDirectory();

			List<Data> highscoreDatas = null;

			try
			{
				using (Stream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
				{
					if (stream.Length == 0)
						return new List<Data>();

					highscoreDatas = (List<Data>)formatter.Deserialize(stream);

					stream.Close();

					return highscoreDatas;
				}
			}
			catch (IOException e)
			{
				return new List<Data>();
			}
		}

		public static bool Store(List<Data> highscoreDatas)
		{
			EnsureDirectory();

			try
			{
				using (Stream stream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
				{
					formatter.Serialize(stream, highscoreDatas);
				}
			}
			catch (IOException e)
			{
				return false;
			}

			return true;
		}

		public static void Add(Data highscoreData)
		{
			List<Data> highscoreDatas = Load();
			highscoreDatas.Add(highscoreData);
			Store(highscoreDatas);
		}

		private static void EnsureDirectory()
		{
			if (!Directory.Exists(rootPath))
				Directory.CreateDirectory(rootPath);
		}
	}
}
