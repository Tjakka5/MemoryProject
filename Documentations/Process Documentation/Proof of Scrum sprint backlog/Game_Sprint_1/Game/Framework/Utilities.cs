using System;
using System.Collections.Generic;

namespace Framework
{
	public static class Utilities
	{
		private static Random random = new Random();

		public static T GetRandom<T>(List<T> source)
		{
			return source[random.Next(source.Count)];
		}

		public static List<int> GetRandomIntSet(int range, int count)
		{
			if (count > range)
			{
				throw new SystemException();
			}

			List<int> pool = new List<int>(range);
			List<int> set = new List<int>(count);

			for (int i = 0; i < range; i++)
				pool.Add(i);

			for (int i = 0; i < count; i++)
			{
				int index = random.Next(pool.Count);
				set.Add(pool[index]);
				pool.RemoveAt(index);
			}

			return set;
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
