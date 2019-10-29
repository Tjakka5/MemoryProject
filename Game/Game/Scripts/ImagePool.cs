using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Scripts
{
	public static class ImagePool
	{
		public enum FrontTypes
		{
			POKEMON,
		}

		public enum BackTypes
		{
			POKEMON,
		}

		public static Dictionary<FrontTypes, List<ImageSource>> frontImages = new Dictionary<FrontTypes, List<ImageSource>>();
		public static Dictionary<BackTypes, ImageSource> backImages = new Dictionary<BackTypes, ImageSource>();

		static ImagePool()
		{
			MakeFrontImages("Pokemon", FrontTypes.POKEMON, 22);

			MakeBackImage("Pokemon", BackTypes.POKEMON);
		}

		private static void MakeFrontImages(string name, FrontTypes type, int count)
		{
			List<ImageSource> pool = new List<ImageSource>();

			for (int i = 0; i < count; i++)
			{
				pool.Add(new BitmapImage(new Uri(@"pack://application:,,,/Game;component/Resources/FrontImages/" + name + "/" + (i + 1) + ".png")));
			}
			frontImages[type] = pool;
		}

		private static void MakeBackImage(string name, BackTypes type)
		{
			backImages[type] = new BitmapImage(new Uri(@"pack://application:,,,/Game;component/Resources/BackImages/Pokemon.png"));
		}
	}
}
