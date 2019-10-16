using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Scripts
{
	public static class Utilities
	{
		private static Random random = new Random();

		public static T GetRandom<T>(List<T> source)
		{
			return source[random.Next(source.Count)];
		}

		public static void Shuffle<T>(List<T> source)
		{
			int n = source.Count;
			while (n > 1)
			{
				n--;
				int k = random.Next(n + 1);
				T value = source[k];
				source[k] = source[n];
				source[n] = value;
			}
		}
	}
}
