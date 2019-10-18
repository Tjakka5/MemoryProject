namespace Game.Scripts.Scheduler.YieldCommands
{
	public class YieldCommand
	{
		public virtual bool Check(float deltaTime)
		{
			return true;
		}
	}
}
