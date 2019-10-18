namespace Game.Scripts.Scheduler.YieldCommands
{
	public class YieldForSeconds : YieldCommand
	{
		private float timeLeft = 0;

		public YieldForSeconds(float delay)
		{
			timeLeft = delay;
		}

		public override bool Check(float deltaTime)
		{
			timeLeft = timeLeft - deltaTime;

			return timeLeft <= 0;
		}
	}
}
