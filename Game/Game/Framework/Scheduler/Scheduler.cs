using System.Collections.Generic;
using System.Diagnostics;

namespace Framework.Scheduling
{
	public static class Scheduler
	{
		private static List<IEnumerator<YieldCommand>> enumerators = new List<IEnumerator<YieldCommand>>();

		public static void Schedule(IEnumerator<YieldCommand> enumerator)
		{
			if (enumerator.MoveNext())
				enumerators.Add(enumerator);
		}

		public static void Update(float deltaTime)
		{
			foreach (IEnumerator<YieldCommand> enumerator in enumerators.ToArray())
			{
				while (true)
				{
					enumerator.Current.Update(deltaTime);
					if (!enumerator.Current.Check())
						break;

					if (!enumerator.MoveNext())
					{
						enumerator.Dispose();
						enumerators.Remove(enumerator);

						break;
					}
				}
			}
		}
	}
}
