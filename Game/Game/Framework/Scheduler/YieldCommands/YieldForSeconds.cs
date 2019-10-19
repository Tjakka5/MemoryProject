namespace Framework.Scheduling
{
	public class YieldForSeconds : YieldCommand
	{
		private float timeLeft = 0;

		public YieldForSeconds(float delay)
		{
			timeLeft = delay;
		}

		public override void Update(float deltaTime)
		{
			timeLeft = timeLeft - deltaTime;
		}

		public override bool Check()
		{
			return timeLeft <= 0;
		}
	}
}
