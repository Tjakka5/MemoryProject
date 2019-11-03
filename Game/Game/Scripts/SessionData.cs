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
	public class SessionData
	{
		private static string rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MemoryGame_33");
		private static string fullPath = Path.Combine(rootPath, "savegame.txt");
		private static IFormatter formatter = new BinaryFormatter();

		public static bool Load(out Session.Data sessionData)
		{
			EnsureDirectory();

			try
			{
				using (Stream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
				{
					sessionData = (Session.Data)formatter.Deserialize(stream);

					stream.Close();

					return true;
				}
			}
			catch (IOException e)
			{
				sessionData = null;
				return false;
			}
		}

		public static bool Store(Session.Data sessionData)
		{
			Clear();

			EnsureDirectory();

			try
			{
				using (Stream stream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
				{
					formatter.Serialize(stream, sessionData);
				}
			}
			catch (IOException e)
			{
				return false;
			}

			return true;
		}

		public static void Clear()
		{
			EnsureDirectory();

			File.Delete(fullPath);
		}


		private static void EnsureDirectory()
		{
			if (!Directory.Exists(rootPath))
				Directory.CreateDirectory(rootPath);
		}
	}
}
