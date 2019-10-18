using Game.Scripts.Scheduler.YieldCommands;
using System.Collections.Generic;

namespace Game.Scripts.Scheduler
{
	public static class Scheduler
	{
		private static List<IEnumerator<YieldCommand>> enumerators = new List<IEnumerator<YieldCommand>>();

		public static void Schedule(IEnumerator<YieldCommand> enumerator)
		{
			enumerators.Add(enumerator);
			enumerator.MoveNext();
		}

		public static void Update(float deltaTime)
		{
			foreach (IEnumerator<YieldCommand> enumerator in enumerators.ToArray())
			{
				YieldCommand yieldCommand = enumerator.Current;
				bool success = yieldCommand.Check(deltaTime);

				if (success)
				{
					bool inProgress = enumerator.MoveNext();
					if (!inProgress)
					{
						enumerator.Dispose();
						enumerators.Remove(enumerator);
					}
				}
			}
		}
	}
}
